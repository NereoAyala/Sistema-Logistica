using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEntities
{
    public class ViajeEntity : FechaBase
    {
        public int IdViaje { get; set; }
        public int IdCamioneta { get; set; }
        public DateTime FechaEntregaDesde { get; set; }
        public DateTime FechaEntregaHasta { get; set; }
        public int PorcentajeCarga { get; set; }
        public List<int> ListadoCodigosCompras = new List<int>(); //ESTAS COMPRAS DEBEN PASAR A READYTODISPACH

       
    }
}
