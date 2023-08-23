using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;
using TestAplication.Models.ApiModels;
using TestAplication.Repository;

namespace TestAplication.Strategies
{
    public class MovimientoCreditoStrategy : IMovimientoStrategy
    {
        public void Add(Movimiento movimiento, IUnitOfWork unitOfWork)
        {
            var cuenta = unitOfWork.Cuentas.Get((int)movimiento.CuentaId);
            if (!(unitOfWork.Cuentas.EstaActivo("CuentaId", cuenta.CuentaId)))
            {
                throw new Exception("No se puede acreditar el valor. Cuenta Inactiva");
            }
            if (!(unitOfWork.Clientes.EstaActivo("ClienteId", (int)cuenta.ClienteId)))
            {
                throw new Exception("No se puede acreditar el valor. Cliente Inactivo");
            }

            movimiento.Saldo = cuenta.SaldoInicial;
            unitOfWork.Movimientos.Add(movimiento);
           
            cuenta.SaldoInicial= cuenta.SaldoInicial + movimiento.Valor;
            unitOfWork.Cuentas.Update(cuenta, "CuentaId");
            unitOfWork.Save();
        }
    }
}
