using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAplication.Models.ApiModels
{
    public class ClientePatchApiModel: PersonaPacthApiModel
    {
        public string Contraseña { get; set; }
        public string Estado { get; set; }
    }
}
