using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEntities
{
    public class ProductoEntity : FechaBase
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public int AltoCaja { get; set; }
        public int AnchoCaja { get; set; }
        public int ProfundidadCaja { get; set; }
        public double PrecioUnitario { get; set; }
        public int StockDisponible { get; set; }
        public int StockMinimo { get; set; }
        public int CalcularVolumenUnidad()
        {
            return AltoCaja * AnchoCaja * ProfundidadCaja;
        }
    }
}
