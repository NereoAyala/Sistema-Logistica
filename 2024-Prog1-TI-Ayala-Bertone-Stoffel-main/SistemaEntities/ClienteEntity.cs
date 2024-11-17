using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEntities
{
    public class ClienteEntity : FechaBase
    {
        public int IdCliente { get; set; }
        public int DniCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Localizacion localizacionCliente { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
