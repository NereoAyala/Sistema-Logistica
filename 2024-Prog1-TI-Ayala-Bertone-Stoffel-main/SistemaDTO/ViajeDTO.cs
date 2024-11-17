using SistemaEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDTO
{
    public class ViajeDTO
    {
        [Required(ErrorMessage = "La fecha de entrega desde es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de entrega desde no es válida.")]
        public DateTime FechaEntregaDesde { get; set; }

        [Required(ErrorMessage = "La fecha de entrega hasta es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de entrega hasta no es válida.")]
        public DateTime FechaEntregaHasta { get; set; }
    }
}
