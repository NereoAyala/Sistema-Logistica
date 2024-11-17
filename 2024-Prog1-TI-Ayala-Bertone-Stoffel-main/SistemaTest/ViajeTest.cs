using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using SistemaData;
using SistemaDTO;
using SistemaEntities;
using SistemaServices;
using SistemaShareds;
using SistemaWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTest
{
    public class ViajeTest
    {
        ViajeController ViajeController = new ViajeController();
        [SetUp]
        public void Setup()
        {
            ViajeController = new ViajeController();
            var controllercompra = new CompraController();
            var compras = CompraFiles.LeerCompraDesdeJson();

            CompraDTO compra1 = new CompraDTO()
            {
                CodProducto = 1,
                DniCliente = 240,
                CantidadComprado = 5,
                FechaEntrega = DateTime.Now.AddDays(5),
            };
            CompraDTO compra2 = new CompraDTO()
            {
                CodProducto = 3,
                DniCliente = 240,
                CantidadComprado = 1,
                FechaEntrega = DateTime.Now.AddDays(3),
            };
            CompraDTO compra3 = new CompraDTO()
            {
                CodProducto = 4,
                DniCliente = 240,
                CantidadComprado = 3,
                FechaEntrega = DateTime.Now.AddDays(2),
            };
            controllercompra.AgregarCompra(compra1);
            controllercompra.AgregarCompra(compra2);
            controllercompra.AgregarCompra(compra3);

            CamionetaEntity camion1 = new CamionetaEntity()
            {
                IdCamioneta = 1,
                Patente = "AA0030D",
                DistanciaMax = 350,
                TamañoCarga = 6300,
                FechaCreacion = DateTime.Now,
            };
            CamionetaEntity camion2 = new CamionetaEntity()
            {
                IdCamioneta = 2,
                Patente = "Ae7180j",
                DistanciaMax = 550,
                TamañoCarga = 5800,
                FechaCreacion = DateTime.Now,
            };
            CamionetaEntity camion3 = new CamionetaEntity()
            {
                IdCamioneta = 3,
                Patente = "AB5780E",
                DistanciaMax = 780,
                TamañoCarga = 6700,
                FechaCreacion = DateTime.Now,
            };
            CamionetaFiles.EscribirCamioneta(camion1);
            CamionetaFiles.EscribirCamioneta(camion2);
            CamionetaFiles.EscribirCamioneta(camion3);
        }
        [Test]
        public void AgregarViaje_OK_DeberiaAgregarViaje()
        {
            var controller = new ViajeController();
            var viajeDTO = new ViajeDTO
            {
                FechaEntregaDesde = DateTime.Now.AddDays(40),
                FechaEntregaHasta = DateTime.Now.AddDays(45)
            };
            var camionetas = CamionetaFiles.LeerCamionetasDesdeJson();
            var resultado = controller.AgregarViaje(viajeDTO) as OkObjectResult;
            var viajeDevuelto = resultado.Value.GetType().GetProperty("viaje").GetValue(resultado.Value) as ViajeDTO;
            Assert.IsNotNull(resultado);
            var message = resultado.Value.GetType().GetProperty("message").GetValue(resultado.Value).ToString();
            Assert.AreEqual(viajeDTO.FechaEntregaDesde, viajeDevuelto.FechaEntregaDesde);
            Assert.AreEqual(viajeDTO.FechaEntregaHasta, viajeDevuelto.FechaEntregaHasta);
            Assert.AreEqual("Viaje agregado con éxito", message);
        }
    }
}

