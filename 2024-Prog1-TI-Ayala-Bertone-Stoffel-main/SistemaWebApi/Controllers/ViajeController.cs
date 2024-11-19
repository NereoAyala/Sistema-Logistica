using Microsoft.AspNetCore.Mvc;
using SistemaData;
using SistemaDTO;
using SistemaEntities;
using SistemaServices;

namespace SistemaWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViajeController : ControllerBase
    {
        private ViajeService ViajeService = new ViajeService();
        [HttpPost]
        public IActionResult AgregarViaje([FromBody] ViajeDTO viajeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (viajeDTO.FechaEntregaDesde == viajeDTO.FechaEntregaHasta)
                {
                    return BadRequest(new { message = "La fecha de inicio y la fecha de finalización no pueden ser iguales.", viajeDTO });
                }

                if (viajeDTO.FechaEntregaDesde < DateTime.Now)
                {
                    return BadRequest(new { message = "La fecha de inicio no puede ser menor a la fecha actual.", viajeDTO });
                }

                if ((viajeDTO.FechaEntregaHasta - viajeDTO.FechaEntregaDesde).TotalDays > 7)
                {
                    return BadRequest(new { message = "La fecha de finalización solo puede ser como máximo 7 días después de la fecha de inicio.", viajeDTO });
                }
                List<ViajeEntity> viajes = ViajeFiles.LeerViajesDesdeJson();
                foreach (var item in viajes)
                {
                    if ((viajeDTO.FechaEntregaDesde >= item.FechaEntregaDesde && viajeDTO.FechaEntregaDesde <= item.FechaEntregaHasta) ||
                         (viajeDTO.FechaEntregaHasta >= item.FechaEntregaDesde && viajeDTO.FechaEntregaHasta <= item.FechaEntregaHasta) ||
                         (viajeDTO.FechaEntregaDesde <= item.FechaEntregaDesde && viajeDTO.FechaEntregaHasta >= item.FechaEntregaHasta))
                    {
                        return BadRequest(new { message = "Ya hay un viaje asignado en estas fechas.", viajeDTO });
                    }
                }
                ViajeService.AgregarViaje(viajeDTO);
                return Ok(new { message = "Viaje agregado con éxito", viaje = viajeDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al procesar la solicitud.", error = ex.Message });
            }
        }
    }
}
