using Microsoft.AspNetCore.Mvc;
using SistemaDTO;
using SistemaServices;
using SistemaWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTest
{
    public class ProductoTest
    {
        private ProductoService productoService;
        [SetUp]
        public void Setup()
        {
            productoService = new ProductoService();
        }
        private ProductoDTO CrearProductoValido()
        {
            return new ProductoDTO
            {
                Nombre = "Hamburguesa",
                Marca = "Paty",
                StockDisponible = 10,
                StockMinimo = 5,
                PrecioUnitario = 2000,
                AltoCaja = 5,
                AnchoCaja = 5,
                ProfundidadCaja = 5
            };
        }
        private ProductoDTO CrearProductoInvalido()
        {
            return new ProductoDTO
            {
                Nombre = string.Empty,
                Marca = "Paty",
                StockDisponible = 10,
                StockMinimo = 5,
                PrecioUnitario = 2000,
                AltoCaja = 5,
                AnchoCaja = 5,
                ProfundidadCaja = 5
            };
        }

        [Test]
        public void AgregarProducto_OK_DeberiaAgregarseProducto()
        {
            var controller = new ProductoController();
            var productoDTO = CrearProductoValido();

            var resultado = controller.AgregarProducto(productoDTO) as OkObjectResult;
            var productoDevuelto = resultado.Value.GetType().GetProperty("productoDTO").GetValue(resultado.Value) as ProductoDTO;
            Assert.AreEqual(productoDTO, productoDevuelto);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(true, (bool)resultado.Value.GetType().GetProperty("success").GetValue(resultado.Value));
        }
        [Test]
        public void ActualizarStock_OK_DeberiaActualizarStock()
        {
            var controller = new ProductoController();
            var productoDTO = CrearProductoValido();
            controller.AgregarProducto(productoDTO);
            List<ProductoDTO> listaProductosDTO = productoService.ObtenerListaProductos();
            int index = listaProductosDTO.Count();
            var resultado = controller.ActualizarStock(index, 5) as OkObjectResult;
            productoDTO.StockDisponible += 5;
            var productoDevuelto = resultado.Value.GetType().GetProperty("Producto").GetValue(resultado.Value) as ProductoDTO;
            Assert.AreEqual(productoDTO.StockDisponible, productoDevuelto.StockDisponible);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(true, (bool)resultado.Value.GetType().GetProperty("success").GetValue(resultado.Value));
        }
        [Test]
        public void ActualizarStock_NotFound_NoDeberiaActualizarStock()
        {
            var controller = new ProductoController();
            var productoDTO = CrearProductoValido();
            controller.AgregarProducto(productoDTO);
            List<ProductoDTO> listaProductosDTO = productoService.ObtenerListaProductos();
            int index = listaProductosDTO.Count();
            var resultado = controller.ActualizarStock(index + 1000, 5) as NotFoundObjectResult;
            var productoDevuelto = resultado.Value.GetType().GetProperty("Producto").GetValue(resultado.Value) as ProductoDTO;
            Assert.AreEqual(null, productoDevuelto);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(404, resultado.StatusCode);
            Assert.AreEqual(false, (bool)resultado.Value.GetType().GetProperty("success").GetValue(resultado.Value));
        }
    }
}
