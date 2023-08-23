using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;
using TestAplication.Repository;

namespace TestAplication.Strategies
{
    public interface IMovimientoStrategy
    {
            public void Add(Movimiento movimiento, IUnitOfWork unitOfWork);
    }
}
