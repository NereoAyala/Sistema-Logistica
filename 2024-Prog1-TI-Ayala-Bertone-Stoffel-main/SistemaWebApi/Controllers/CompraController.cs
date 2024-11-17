using Microsoft.AspNetCore.Mvc;
using SistemaData;
using SistemaDTO;
using SistemaEntities;
using SistemaServices;

namespace SistemaWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompraController : ControllerBase
    {
        private CompraService compraService = new CompraService();
       
        public CompraController()
        {
            compraService = new CompraService();
        }
        [HttpPost]
        public IActionResult AgregarCompra([FromBody] CompraDTO compraDto)
        {
            List<ProductoEntity> productos = ProductoFiles.LeerProductosDesdeJson();
            var producto = productos.Find(x => x.IdProducto == compraDto.CodProducto);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado", producto });
            }
            if ((producto.StockDisponible - compraDto.CantidadComprado) < 0)
            {
                return BadRequest(new { message = "No se puede realizar la compra no hay stock suficiente", producto });
            }
            List<ClienteEntity> clientes = ClienteFiles.LeerClientesDesdeJson();
            var cliente = clientes.Find(x => x.DniCliente == compraDto.DniCliente);
            if (cliente == null)
            {
                return NotFound(new { message = "Cliente no encontrado", cliente });
            }
            compraService.CrearCompra(compraDto);
            return Ok(new { message = "Compra agregada con éxito", compra = compraDto });
        }
        [HttpGet]
        public IActionResult ObtenerCompras() {
            List<CompraDTO> compras = compraService.ObtenerCompras();
            return Ok(compras);
        }
    }
}

