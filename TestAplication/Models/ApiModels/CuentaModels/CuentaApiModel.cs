using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels
{
    public class CuentaApiModel
    {
      
        [Required(ErrorMessage = "El campo ClienteId es obligatorio")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El campo NumeroCuenta es obligatorio")]
        public string NumeroCuenta { get; set; }
        [Required(ErrorMessage = "El campo TipoCuenta es obligatorio")]
        public string TipoCuenta { get; set; }
        [Required(ErrorMessage = "El campo SaldoInicial es obligatorio")]
        public decimal SaldoInicial { get; set; }
        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public string Estado { get; set; }
    }
}
