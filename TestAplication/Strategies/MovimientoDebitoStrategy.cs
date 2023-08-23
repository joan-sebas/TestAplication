using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAplication.EntityFramework.Data;
using TestAplication.Repository;

namespace TestAplication.Strategies
{
    public class MovimientoDebitoStrategy : IMovimientoStrategy
    {
        public void Add(Movimiento movimiento, IUnitOfWork unitOfWork)
        {
            //Validar si se puede hacer debito (dinero suficiente en la cuenta)
            var cuenta = unitOfWork.Cuentas.Get((int)movimiento.CuentaId);
            
            if (!(unitOfWork.Cuentas.EstaActivo("CuentaId", cuenta.CuentaId)))
            {
                throw new Exception("No se puede debitar el valor. Cuenta Inactiva");
            }
            if (!(unitOfWork.Clientes.EstaActivo("ClienteId", (int)cuenta.ClienteId)))
            {
                throw new Exception("No se puede debitar el valor. Cliente Inactivo");
            }

            movimiento.Saldo = cuenta.SaldoInicial;
            if (cuenta.SaldoInicial + movimiento.Valor < 0)
            {
                throw new Exception("No se puede debitar el valor. Fondos insuficientes");
            }
            unitOfWork.Movimientos.Add(movimiento);
            cuenta.SaldoInicial = cuenta.SaldoInicial + movimiento.Valor;
            unitOfWork.Cuentas.Update(cuenta, "CuentaId");
            unitOfWork.Save();
        }
    }
}
