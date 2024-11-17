using System.ComponentModel.DataAnnotations;
namespace SistemaDTO
{
    public class CamionetaDTO
    {
        [Required(ErrorMessage = "La patente es obligatoria.")]
        [StringLength(10, ErrorMessage = "La patente no puede exceder de 10 caracteres.")]
        public string Patente { get; set; }

        [Required(ErrorMessage = "El tamaño de carga es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El tamaño de carga debe ser un número positivo.")]
        public double TamañoCarga { get; set; }

        [Required(ErrorMessage = "La distancia máxima es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La distancia máxima debe ser un número positivo.")]
        public double DistanciaMax { get; set; }
    }
}
