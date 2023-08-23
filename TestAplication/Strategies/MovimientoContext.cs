using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;
using TestAplication.Repository;

namespace TestAplication.Strategies
{
    public class MovimientoContext
    {
        private IMovimientoStrategy _strategy;

        public IMovimientoStrategy Strategy
        {
            set
            {
                _strategy = value;
            }
        }
        public MovimientoContext(IMovimientoStrategy strategy)
        {
            _strategy = strategy;

        }
        public void Add(Movimiento movimiento, IUnitOfWork unitOfWork)
        {
            _strategy.Add(movimiento, unitOfWork);
        }
    }
}
