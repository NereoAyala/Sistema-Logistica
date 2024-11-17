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

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder de 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La Latitud del cliente es obligatoria.")]
        public double Latitud { get; set; }

        [Required(ErrorMessage = "La Longitud del cliente es obligatoria.")]
        public double Longitud { get; set; }

        //public void Validar(ResultadoEntity resultado)
        //{
        //    if (DniCliente == 0)
        //    {
        //        resultado.Errores.Add("El Dni del Cliente no es Valido");
        //    }
        //    if (string.IsNullOrEmpty(Nombre))
        //    {
        //        resultado.Errores.Add("El Nombre del Cliente No es Valido");
        //    }
        //    if (string.IsNullOrEmpty(Apellido))
        //    {
        //        resultado.Errores.Add("El Apellido del Cliente No es Valido");
        //    }
        //    if (string.IsNullOrEmpty(Email))
        //    {
        //        resultado.Errores.Add("El Email del Cliente No es Valido");
        //    }
        //    if (string.IsNullOrEmpty(Telefono))
        //    {
        //        resultado.Errores.Add("El Telefono del Cliente No es Valido");
        //    }
        //    if (FechaNacimiento > DateTime.Now || FechaNacimiento==DateTime.MinValue)
        //    {
        //        resultado.Errores.Add("La Fecha de Nacimiento no Puede ser Futura y es obligatoria");
        //    }
        //}
    }
}
