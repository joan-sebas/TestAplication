using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestAplication.Repository
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        IEnumerable<TEntity> GetRangoFechas(DateTime? fechaIni, DateTime? fechaFin, string cliente);
        bool Existe(int id);
        bool EstaActivo(string property, int value);
        bool Existe(string property, string value);
        void Add(TEntity data);
        void Delete(int id);
        void Update(TEntity data, string idProperty);



    }
}
