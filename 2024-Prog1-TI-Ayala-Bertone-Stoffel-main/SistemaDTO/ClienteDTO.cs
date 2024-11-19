using SistemaEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDTO
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "El DNI del cliente es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El DNI debe ser un número positivo.")]
        public int DniCliente { get; set; }

        [Required(ErrorMessage = "El Nombre es Obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es Obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder de 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Email es Obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Teléfono es Obligatorio.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es Obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La Latitud del cliente es obligatoria.")]
        public double Latitud { get; set; }

        [Required(ErrorMessage = "La Longitud del Cliente es Obligatoria.")]
        public double Longitud { get; set; }
    }
}
