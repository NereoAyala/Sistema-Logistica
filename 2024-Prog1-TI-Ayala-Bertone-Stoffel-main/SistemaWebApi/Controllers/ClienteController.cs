using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemaData;
using SistemaDTO;
using SistemaEntities;
using SistemaServices;

namespace SistemaWebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        ClienteService clienteService = new ClienteService();

        [HttpPost]
        public IActionResult AgregarCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (clienteDTO.DniCliente <= 0)
            {
                return BadRequest(new { message = "El DNI del Cliente No puede ser menor o igual a 0", cliente = clienteDTO });
            }
            if (clienteDTO.FechaNacimiento > DateTime.Now)
            {
                return BadRequest(new { message = "La Fecha de Nacimiento No es Valida", cliente = clienteDTO });
            }
            try
            {
                var clientes = ClienteFiles.LeerClientesDesdeJson();
                if (clientes.Any(c => c.DniCliente == clienteDTO.DniCliente))
                {
                    return Conflict(new { message = "El Cliente con este Dni ya existe", cliente = clienteDTO });
                }

                clienteService.AgregarCliente(clienteDTO);
                return Ok(new { message = "Cliente agregado con Éxito", cliente = clienteDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrio un error al procesar la Solicitud", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult EliminarCliente(int id)
        {
            if (id < 0)
            {
                return BadRequest(new { message = "El ID debe ser un número Positivo." });
            }
            try
            {
                var cliente = clienteService.EliminarCliente(id);
                if (cliente == null)
                {
                    return NotFound(new { message = "Cliente no encontrado", clienteEliminado = cliente });
                }
                return Ok(new { message = "Cliente eliminado con éxito", clienteEliminado = cliente });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al Procesar la Solicitud", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(int id, [FromBody] ClienteDTO cliente)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "El ID debe ser un número Positivo." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Cliente = clienteService.ActualizarCliente(id, cliente);
                if (Cliente == null)
                {
                    return NotFound(new { message = "Cliente No encontrado", clienteActualizado = Cliente });
                }
                return Ok(new { message = "Cliente actualizado con éxito", clienteActualizado = Cliente });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al Procesar la Solicitud", error = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult ObtenerClientes()
        {
            try
            {
                var clientes = clienteService.ObtenerListaClientes();
                if (clientes == null || !clientes.Any())
                {
                    return NotFound(new { message = "No se encontraron Clientes" });
                }
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al Procesar la Solicitud", error = ex.Message });
            }
        }
    }
}
