using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels
{
    public  class PersonaApiModel
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Género es obligatorio")]
        public string Genero { get; set; }
        [Required(ErrorMessage = "El campo Edad es obligatorio")]
        public int Edad { get; set; }
        [Required(ErrorMessage = "El campo Identificación es obligatorio")]
        public string Identificacion { get; set; }
        [Required(ErrorMessage = "El campo Dirección es obligatorio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo Teléfono es obligatorio")]
        public string Telefono { get; set; }
    }
}
