using SistemaEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDTO
{
    public class CompraDTO
    {
        [Required(ErrorMessage = "El código del producto es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El código del producto debe ser un número positivo.")]
        public int CodProducto { get; set; }

        [Required(ErrorMessage = "El DNI del cliente es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El DNI debe ser un número positivo.")]
        public int DniCliente { get; set; }

        [Required(ErrorMessage = "La cantidad comprada es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad comprada debe ser al menos 1.")]
        public int CantidadComprado { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de entrega no es válida.")]
        public DateTime FechaEntrega { get; set; }

        //public void Validacion(ResultadoEntity resultado) {
        //    if (CodProducto <= 0) {

        //        resultado.Errores.Add("Codigo de producto no valido.");
        //    }
        //    if (DniCliente <= 0)
        //    {
        //        resultado.Errores.Add("El dni del cliente no es valido.");
        //    }
        //    if (CantidadComprado <= 0)
        //    {
        //        resultado.Errores.Add("La cantidad ingresada no es valida.");
        //    }
        //    if (FechaEntrega <= DateTime.Now)
        //    {
        //        resultado.Errores.Add("La fecha de entrega no es valida.");
        //    }
        //}
    }
}
