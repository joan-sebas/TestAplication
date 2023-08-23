using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels.ReporteMovimientos
{
    public class FormReporteMovimientosApiModel
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Cliente { get; set; }
    }
}
