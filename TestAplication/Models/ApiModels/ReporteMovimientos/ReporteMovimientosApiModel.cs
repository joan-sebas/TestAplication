using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels.ReporteMovimientos
{
    public class ReporteMovimientosApiModel
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
