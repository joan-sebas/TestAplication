using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;

namespace TestAplication.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TestContext _context;
        private IRepository<Cliente> _clientes;
        private IRepository<Cuenta> _cuentas;
        private IRepository<Movimiento> _movimientos;
        private IRepository<ReporteMovimiento> _reporteMovimientos;
        public IRepository<Cliente> Clientes
        {
            get
            {
                return _clientes == null ?
                    _clientes = new Repository<Cliente>(_context) :
                    _clientes;
            }
        }
        public IRepository<Cuenta> Cuentas
        {
            get
            {
                return _cuentas == null ?
                    _cuentas = new Repository<Cuenta>(_context) :
                    _cuentas;
            }
        }
        public IRepository<Movimiento> Movimientos
        {
            get
            {
                return _movimientos == null ?
                    _movimientos = new Repository<Movimiento>(_context) :
                    _movimientos;
            }
        }
        public IRepository<ReporteMovimiento> ReporteMovimiento
        {
            get
            {
                return _reporteMovimientos == null ?
                    _reporteMovimientos = new Repository<ReporteMovimiento>(_context) :
                    _reporteMovimientos;
            }
        }

        public UnitOfWork(TestContext context)
        {
            this._context = context;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
