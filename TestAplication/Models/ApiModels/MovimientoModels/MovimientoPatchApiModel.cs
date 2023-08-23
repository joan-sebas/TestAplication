using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels.MovimientoModels
{
    public class MovimientoPatchApiModel
    {
        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
