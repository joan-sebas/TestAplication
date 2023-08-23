using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels
{
    public class ClienteApiModel : PersonaApiModel
    {
      
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string Contraseña { get; set; }
        [Required(ErrorMessage = "El campo Estado es obligatorio")]
        public string Estado { get; set; }

    }
}
