using System;
using System.Collections.Generic;

#nullable disable

namespace TestAplication.EntityFramework.Data
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int? Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
