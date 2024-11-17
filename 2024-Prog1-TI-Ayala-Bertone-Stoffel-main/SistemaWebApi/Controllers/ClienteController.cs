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
            if (clienteDTO.DniCliente <= 0)
            {
                return BadRequest(new { message = "El DNI del cliente no puede ser menor o igual a 0", cliente = clienteDTO });
            }
            var clientes = ClienteFiles.LeerClientesDesdeJson();
            if (clientes.Any(c => c.DniCliente == clienteDTO.DniCliente))
            {
                return BadRequest(new { message = "El cliente ya existe", cliente = clienteDTO });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            clienteService.AgregarCliente(clienteDTO);
            return Ok(new { message = "Cliente agregado con éxito", cliente = clienteDTO });
        }
        [HttpDelete("{id}")]
        public IActionResult EliminarCliente(int id)
        {
            var cliente = clienteService.EliminarCliente(id);
            if (cliente == null)
            {
                return NotFound(new { message = "Cliente no encontrado", clienteEliminado = cliente });
            }
            return Ok(new { message = "Cliente eliminado con éxito", clienteEliminado = cliente });
        }
        [HttpPut("{id}")]
        public IActionResult ActualizarCliente(int id, [FromBody] ClienteDTO cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Cliente = clienteService.ActualizarCliente(id, cliente);
            if (Cliente == null)
            {
                return NotFound(new { message = "Cliente no encontrado", clienteActualizado = Cliente });
            }
            return Ok(new { message = "Cliente actualizado con éxito", clienteActualizado = Cliente });
        }
        [HttpGet]
        public IActionResult ObtenerClientes()
        {
            List<ClienteDTO> clientes = clienteService.ObtenerListaClientes();
            return Ok(clientes);
        }
    }
}
