using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels.CuentaModels
{
    public class CuentaPatchApiModel
    {
        public int CuentaId { get; set; }
        public int ClienteId { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; }
    }
}
