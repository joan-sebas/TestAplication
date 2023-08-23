using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;

namespace TestAplication.Repository
{
    public interface IUnitOfWork
    {
        public IRepository<Cliente> Clientes { get; }
        public IRepository<Cuenta> Cuentas { get; }
        public IRepository<Movimiento> Movimientos { get; }
        public IRepository<ReporteMovimiento> ReporteMovimiento { get; }
        public bool Save();
    }
}
