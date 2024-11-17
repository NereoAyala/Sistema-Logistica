using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEntities
{
    public class ResultadoEntity
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errores = new List<string>();
    }
}
