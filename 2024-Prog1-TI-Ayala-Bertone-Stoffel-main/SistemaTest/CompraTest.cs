using System;
using SistemaServices;
using NUnit.Framework;
using SistemaDTO;
using SistemaEntities;
using Microsoft.AspNetCore.Mvc;
using SistemaData;
using SistemaWebApi.Controllers;


namespace SistemaTest
{
    public class CompraTest
    {
        CompraService compraservice = new CompraService();
        ProductoService productoService = new ProductoService();
        [SetUp]
        public void Setup()
        {
            CargarProductos();
            CargarClientes();
        }
        private void CargarClientes()
        {
            ClienteService clienteService = new ClienteService();
            clienteService.AgregarCliente(new ClienteDTO()
            {
                DniCliente = 46218295,
                Nombre = "Francisco",
                Apellido = "Stoffel",
                Email = "Franstoffel@gmail.com",
                Telefono = "4566732",
                Latitud = 123421,
                Longitud = 431335,
                FechaNacimiento = new DateTime(2004, 05, 28)
            });

            clienteService.AgregarCliente(new ClienteDTO()
            {
                DniCliente = 43955732,
                Nombre = "Nereo",
                Apellido = "Ayala",
                Email = "NereAyala@gmail.com",
                Telefono = "206289",
                Latitud = 187651,
                Longitud = 424635,
                FechaNacimiento = new DateTime(2002, 05, 03)
            });
        }
        private void CargarProductos()
        {
            ProductoService productoService = new ProductoService();
            productoService.AgregarProducto(new ProductoDTO
            {
                Nombre = "ProductoTest",
                Marca = "MarcaTest",
                AltoCaja = 1,
                AnchoCaja = 1,
                ProfundidadCaja = 1,
                PrecioUnitario = 1,
                StockMinimo = 1,
                StockDisponible = 25
            });
            productoService.AgregarProducto(new ProductoDTO
            {
                Nombre = "ProductoTest2",
                Marca = "MarcaTest2",
                AltoCaja = 2,
                AnchoCaja = 2,
                ProfundidadCaja = 2,
                PrecioUnitario = 2,
                StockMinimo = 2,
                StockDisponible = 20
            });
            productoService.AgregarProducto(new ProductoDTO
            {
                Nombre = "ProductoTest3",
                Marca = "MarcaTest3",
                AltoCaja = 3,
                AnchoCaja = 3,
                ProfundidadCaja = 3,
                PrecioUnitario = 3,
                StockMinimo = 3,
                StockDisponible = 30
            });
        }
        [Test]
        public void TestCrearCompra_Ok_DeberiaAgregarCompra()
        {
            var controller = new CompraController();
            CargarProductos();
            var compraDTO = new CompraDTO
            {
                DniCliente = 46218295,
                CantidadComprado = 2,
                FechaEntrega = DateTime.Now
            };
            List<ProductoDTO> listaProductosDto = productoService.ObtenerListaProductos();
            int id = listaProductosDto.Count();
            compraDTO.CodProducto = id;
            var resultado = controller.AgregarCompra(compraDTO) as OkObjectResult;
            var CompraDevuelto = resultado.Value.GetType().GetProperty("compra").GetValue(resultado.Value) as CompraDTO;
            var message = resultado.Value.GetType().GetProperty("message").GetValue(resultado.Value).ToString();
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.AreEqual(compraDTO.CodProducto, CompraDevuelto.CodProducto);
            Assert.AreEqual(compraDTO.DniCliente, CompraDevuelto.DniCliente);
            Assert.AreEqual(compraDTO.CantidadComprado, CompraDevuelto.CantidadComprado);
            Assert.AreEqual(compraDTO.FechaEntrega, CompraDevuelto.FechaEntrega);
            Assert.AreEqual("Compra agregada con éxito", message);

            var compras = CompraFiles.LeerCompraDesdeJson();
            var compraAgregada = compras.FirstOrDefault(x => x.DniCliente == compraDTO.DniCliente && x.CodProducto == compraDTO.CodProducto);

            Assert.IsNotNull(compraAgregada);
            Assert.AreEqual(compraDTO.CantidadComprado, compraAgregada.CantidadComprado);
        }
        [Test]
        public void TestCrearCompra_NotFound_NoDeberiaAgregarCompra()
        {
            var controller = new CompraController();
            CargarProductos();
            var compraDTO = new CompraDTO
            {
                DniCliente = 46218295,
                CantidadComprado = 2,
                FechaEntrega = DateTime.Now
            };
            List<ProductoDTO> listaProductosDto = productoService.ObtenerListaProductos();
            int id = listaProductosDto.Count();
            compraDTO.CodProducto = id + 10;
            var resultado = controller.AgregarCompra(compraDTO) as NotFoundObjectResult;
            var productoDevuelto = resultado.Value.GetType().GetProperty("producto").GetValue(resultado.Value) as ProductoDTO;
            var message = resultado.Value.GetType().GetProperty("message").GetValue(resultado.Value).ToString();
            Assert.IsNotNull(resultado);
            Assert.AreEqual(404, resultado.StatusCode);
            Assert.AreEqual(null, productoDevuelto);
            Assert.AreEqual("Producto no encontrado", message);
        }
        [Test]
        public void TestCrearCompra_BadRequest_NoDeberiaAgregarCompra()
        {
            var controller = new CompraController();
            CargarProductos();
            var compraDTO = new CompraDTO
            {
                DniCliente = 46218295,
                CantidadComprado = 10000,
                FechaEntrega = DateTime.Now
            };
            List<ProductoDTO> listaProductosDto = productoService.ObtenerListaProductos();
            int id = listaProductosDto.Count();
            compraDTO.CodProducto = id;
            var resultado = controller.AgregarCompra(compraDTO) as BadRequestObjectResult;
            var productoDevuelto = resultado.Value.GetType().GetProperty("producto").GetValue(resultado.Value) as ProductoDTO;
            var message = resultado.Value.GetType().GetProperty("message").GetValue(resultado.Value).ToString();
            Assert.IsNotNull(resultado);
            Assert.AreEqual(400, resultado.StatusCode);
            Assert.AreEqual(null, productoDevuelto);
            Assert.AreEqual("No se puede realizar la compra no hay stock suficiente", message);
        }
        [Test]
        public void TestCrearCompra_NotFoundCliente_NoDeberiaAgregarCompra()
        {
            var controller = new CompraController();
            CargarProductos();
            var compraDTO = new CompraDTO
            {
                DniCliente = 541144141,
                CantidadComprado = 2,
                FechaEntrega = DateTime.Now
            };
            List<ProductoDTO> listaProductosDto = productoService.ObtenerListaProductos();
            int id = listaProductosDto.Count();
            compraDTO.CodProducto = id;
            var resultado = controller.AgregarCompra(compraDTO) as NotFoundObjectResult;
            var clienteDevuelto = resultado.Value.GetType().GetProperty("cliente").GetValue(resultado.Value) as ClienteDTO;
            var message = resultado.Value.GetType().GetProperty("message").GetValue(resultado.Value).ToString();
            Assert.IsNotNull(resultado);
            Assert.AreEqual(404, resultado.StatusCode);
            Assert.AreEqual(null, clienteDevuelto);
            Assert.AreEqual("Cliente no encontrado", message);
        }
    }
}