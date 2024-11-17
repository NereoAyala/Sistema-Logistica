using SistemaData;
using SistemaDTO;
using SistemaEntities;
using SistemaShareds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaServices
{
    public class ViajeService
    {
        public void AgregarViaje(ViajeDTO viaje)
        {
            List<CompraEntity> compras = CompraFiles.LeerCompraDesdeJson();
            List<CamionetaEntity> camionetas = CamionetaFiles.LeerCamionetasDesdeJson().OrderBy(x => x.DistanciaMax).ThenBy(x => x.TamañoCarga).ToList();
            List<int> comprasYaAsignadas = new List<int>();
            foreach (var camioneta in camionetas)
            {
                double cargaActual = 0;
                List<int> codigosComprasAsignadas = new List<int>();
                var comprasDisponibles = compras.Where(x => x.EstadoCompra == Enums.EstadoCompra.Open && !comprasYaAsignadas.Contains(x.IdCompra)).ToList();

                foreach (var compra in comprasDisponibles)
                {
                    double distancia = compra.ObtenerDistancia();
                    double capacidad = compra.TamañoCajaTotal * compra.CantidadComprado;

                    if (camioneta.DistanciaMax >= distancia && (camioneta.TamañoCarga - cargaActual) >= capacidad)
                    {
                        compra.EstadoCompra = Enums.EstadoCompra.ReadyToDispach;
                        cargaActual += capacidad;
                        codigosComprasAsignadas.Add(compra.IdCompra);
                        comprasYaAsignadas.Add(compra.IdCompra);
                        CompraFiles.EscribirCompra(compra);
                    }
                }
                if (codigosComprasAsignadas.Any())
                {
                    var viajeTemp = new ViajeEntity()
                    {
                        IdCamioneta = camioneta.IdCamioneta,
                        FechaEntregaDesde = viaje.FechaEntregaDesde,
                        FechaEntregaHasta = viaje.FechaEntregaHasta,
                        PorcentajeCarga = (int)((cargaActual / camioneta.TamañoCarga) * 100),
                        ListadoCodigosCompras = codigosComprasAsignadas,
                        FechaCreacion = DateTime.Now,
                    };
                    ViajeFiles.EscribirViaje(viajeTemp);
                }
            }
            foreach (var compra in compras.Where(x => x.EstadoCompra == Enums.EstadoCompra.Open))
            {
                compra.FechaEntrega = compra.FechaEntrega.AddDays(14);
            }
        }
    }
}
