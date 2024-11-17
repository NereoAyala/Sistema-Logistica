using SistemaEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDTO
{
    public class ProductoDTO
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La marca del producto es obligatoria.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El stock disponible es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock disponible debe ser un número positivo.")]
        public int StockDisponible { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser un número positivo mayor que 0.")]
        public double PrecioUnitario { get; set; }

        [Required(ErrorMessage = "La altura de la caja es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La altura de la caja debe ser un número positivo.")]
        public int AltoCaja { get; set; }

        [Required(ErrorMessage = "El ancho de la caja es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ancho de la caja debe ser un número positivo.")]
        public int AnchoCaja { get; set; }

        [Required(ErrorMessage = "La profundidad de la caja es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La profundidad de la caja debe ser un número positivo.")]
        public int ProfundidadCaja { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo debe ser un número positivo.")]
        public int StockMinimo { get; set; }
        //public void ValidarProducto(ResultadoEntity resultado) 
        //{
        //    if (string.IsNullOrEmpty(Nombre))
        //    {
        //        resultado.Errores.Add("El Nombre del Producto no es Valido");
        //    }
        //    if (string.IsNullOrEmpty(Marca))
        //    {
        //        resultado.Errores.Add("La Marca del Producto no es Valida");
        //    }
        //    if (StockDisponible<=0)
        //    {
        //        resultado.Errores.Add("El Stock del Producto no es Valido");
        //    }
        //    if (PrecioUnitario <= 0)
        //    {
        //        resultado.Errores.Add("El Precio Unitario del Producto no es Valido");
        //    }
        //    if (AltoCaja <= 0)
        //    {
        //        resultado.Errores.Add("El Alto de la Caja no es Valido");
        //    }
        //    if (AnchoCaja <= 0)
        //    {
        //        resultado.Errores.Add("El Ancho de la Caja no es Valido");
        //    }
        //    if (ProfundidadCaja <= 0)
        //    {
        //        resultado.Errores.Add("La Profundidad de la caja no es Valida");
        //    }
        //    if (StockMinimo<=0)
        //    {
        //        resultado.Errores.Add("El Stock Minimo no es Valido");
        //    }
        //}
    }
}
