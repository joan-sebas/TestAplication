using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels
{
    public class MovimientoApiModel
    {
       
        [Required(ErrorMessage = "El campo CuentaId es obligatorio")]
        public int CuentaId { get; set; }
        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo TipoMovimiento es obligatorio")]
        public string TipoMovimiento { get; set; }
        [Required(ErrorMessage = "El campo Valor es obligatorio")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "El campo Saldo es obligatorio")]
        public decimal Saldo { get; set; }
    }
}
