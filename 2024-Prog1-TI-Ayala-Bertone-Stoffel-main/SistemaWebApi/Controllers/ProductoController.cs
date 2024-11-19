using Microsoft.AspNetCore.Mvc;
using SistemaDTO;
using SistemaEntities;
using SistemaServices;

namespace SistemaWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        ProductoService productoService = new ProductoService();

        [HttpPost]
        public IActionResult AgregarProducto([FromBody] ProductoDTO productoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (productoDTO.PrecioUnitario <= 0)
                {
                    return BadRequest(new { message = "El Precio Unitario debe ser Mayor a Cero" });
                }
                if (productoDTO.StockDisponible <= 0)
                {
                    return BadRequest(new { message = "El Stock Disponible debe ser Mayor a Cero" });
                }
                if (productoDTO.StockMinimo <= 0)
                {
                    return BadRequest(new { message = "El Stock Minimo debe ser Mayor a Cero" });
                }
                productoService.AgregarProducto(productoDTO);
                return Ok(new { success = true, productoDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al Procesar la Solicitud", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult ActualizarStock(int id, [FromBody] int stockNuevo)
        {
            if (stockNuevo < 0)
            {
                return BadRequest(new { message = "El stock no puede ser negativo." });
            }
            try
            {
                var producto = productoService.ActualizarStockProducto(id, stockNuevo);

                if (producto == null)
                {
                    return NotFound(new { message = "Producto no encontrado." });
                }

                return Ok(new { success = true, message = "Stock actualizado con éxito.", producto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al procesar la solicitud.", error = ex.Message });
            }
        }
        [HttpGet()]
        public IActionResult FiltrarProductos([FromQuery] int limite)
        {
            if (limite <= 0)
            {
                return BadRequest(new { message = "El límite debe ser mayor a cero." });
            }
            try
            {
                var productos = productoService.FiltrarProductosPorStock(limite);

                if (productos == null || !productos.Any())
                {
                    return NotFound(new { message = "No se encontraron productos con el criterio especificado." });
                }

                return Ok(new { success = true, message = "Productos filtrados con éxito.", productos });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al procesar la solicitud.", error = ex.Message });
            }
        }
    }
}
