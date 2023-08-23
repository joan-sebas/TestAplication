using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;

namespace TestAplication.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private TestContext _context;
        private DbSet<TEntity> _dbSet;
        public Repository(TestContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get() => _dbSet.ToList();

        public TEntity Get(int id) => _dbSet.Find(id);

        public IEnumerable<TEntity> GetRangoFechas(DateTime? fechaIni, DateTime? fechaFin, string cliente)
        =>
            _dbSet.Where(x => (fechaIni != null ?
                                EF.Property<DateTime>(x, "Fecha") >= fechaIni :
                                EF.Property<DateTime>(x, "Fecha") == EF.Property<DateTime>(x, "Fecha"))
                            && (fechaFin != null ?
                                EF.Property<DateTime>(x, "Fecha") <= fechaFin :
                                EF.Property<DateTime>(x, "Fecha") == EF.Property<DateTime>(x, "Fecha"))
                            && (cliente!=null  ?
                                EF.Property<string>(x, "Cliente").ToLower().Trim() == cliente.ToLower().Trim() :
                                EF.Property<int>(x, "Cliente") == EF.Property<int>(x, "Cliente"))
                                );
        
        public void Delete(int id)
        {
            var dataToDelete = _dbSet.Find(id);
            _dbSet.Remove(dataToDelete);
        }
        public bool Existe(int id)
        {
            return _dbSet.Find(id) != null;
        }
        public bool Existe(string property,string value)
        {
           
            var hasNameProperty = typeof(TEntity).GetProperty(property) != null;

            if (!hasNameProperty)
            {
                throw new InvalidOperationException($"{typeof(TEntity)}no tiene una propiedad: {property}.");
            }

            bool valor = _dbSet.Any( entity => EF.Property<string>(entity, property).ToLower().Trim() == value.ToLower().Trim());
            return valor;
        }
        public bool EstaActivo(string property, int value)
        {

            var hasNameProperty = typeof(TEntity).GetProperty(property) != null;

            if (!hasNameProperty)
            {
                throw new InvalidOperationException($"{typeof(TEntity)}no tiene una propiedad: {property}.");
            }
            
            bool valor = _dbSet.Any(entity => EF.Property<int>(entity, property) == value 
                         && EF.Property<string>(entity, "Estado").ToLower().Trim()=="activo");
            return valor;
        }

        public void Add(TEntity data) => _dbSet.Add(data);


        public void Update(TEntity data, string idProperty)
        {
            _dbSet.Attach(data);

            var entry = _context.Entry(data);

            foreach (var property in entry.CurrentValues.Properties)
            {
                var propertyName = property.Name;
                if (propertyName == idProperty)
                {
                    continue;
                }
                var propertyValue = entry.CurrentValues[propertyName];

                if (propertyValue != null)
                {
                    entry.Property(propertyName).IsModified = true;
                }
            }
        }

    }
}
