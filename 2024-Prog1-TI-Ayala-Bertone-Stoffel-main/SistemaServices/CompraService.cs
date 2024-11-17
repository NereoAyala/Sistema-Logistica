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
    public class CompraService
    {
        public void CrearCompra(CompraDTO compraDto)
        {
            List<ProductoEntity> productos = ProductoFiles.LeerProductosDesdeJson();
            var producto = productos.Find(x => x.IdProducto == compraDto.CodProducto);
            List<ClienteEntity> clientes = ClienteFiles.LeerClientesDesdeJson();
            var cliente = clientes.Find(x => x.DniCliente == compraDto.DniCliente);
            var monto = producto.PrecioUnitario * compraDto.CantidadComprado;
            monto = monto + (monto * 0.21);
            if (producto.StockDisponible > 4)
            {
                monto = monto - (monto * 0.25);
            }
            producto.StockDisponible -= compraDto.CantidadComprado;
            ProductoFiles.EscribirProducto(producto);
            List<CompraEntity> compras = CompraFiles.LeerCompraDesdeJson();
            CompraEntity compra = new CompraEntity
            {
                CodProducto = producto.IdProducto,
                DniCliente = compraDto.DniCliente,
                CantidadComprado = compraDto.CantidadComprado,
                FechaEntrega = compraDto.FechaEntrega,
                EstadoCompra = Enums.EstadoCompra.Open,
                MontoCompra = monto,
                FechaCreacion = DateTime.Now,
                TamañoCajaTotal = producto.CalcularVolumenUnidad() * compraDto.CantidadComprado,
                PuntoDestino = new Localizacion()
                {
                    LatitudCliente = cliente.localizacionCliente.LatitudCliente,
                    LongitudCliente = cliente.localizacionCliente.LongitudCliente,
                }
            };
            compras.Add(compra);
            CompraFiles.EscribirCompra(compra);
        }
        public List<CompraDTO> ObtenerCompras()
        {
            List<CompraEntity> compras = CompraFiles.LeerCompraDesdeJson().Where(x => x.FechaEliminacion == null).ToList();
            List<CompraDTO> compraDTOs = new List<CompraDTO>();
            foreach (CompraEntity compra in compras)
            {
                CompraDTO compraDto = new CompraDTO()
                {
                    CantidadComprado = compra.CantidadComprado,
                    DniCliente = compra.DniCliente,
                    CodProducto = compra.CodProducto,
                    FechaEntrega = compra.FechaEntrega,
                };
                compraDTOs.Add(compraDto);
            }
            return compraDTOs;
        }
    }
}
