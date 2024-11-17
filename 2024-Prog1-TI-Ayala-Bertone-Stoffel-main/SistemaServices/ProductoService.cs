using SistemaData;
using SistemaDTO;
using SistemaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaServices
{
    public class ProductoService
    {
        public void AgregarProducto(ProductoDTO producto)
        {
            List<ProductoEntity> productos = ProductoFiles.LeerProductosDesdeJson();
            var ProductoEntity = new ProductoEntity
            {
                Nombre = producto.Nombre,
                Marca = producto.Marca,
                AltoCaja = producto.AltoCaja,
                AnchoCaja = producto.AnchoCaja,
                ProfundidadCaja = producto.ProfundidadCaja,
                PrecioUnitario = producto.PrecioUnitario,
                StockDisponible = producto.StockDisponible,
                StockMinimo = producto.StockMinimo,
                FechaCreacion = DateTime.Now
            };
            productos.Add(ProductoEntity);
            ProductoFiles.EscribirProducto(ProductoEntity);
        }
        public ProductoDTO ActualizarStockProducto(int id, int stockNuevo)
        {
            List<ProductoEntity> productos = ProductoFiles.LeerProductosDesdeJson();
            ProductoEntity producto = productos.FirstOrDefault(x => x.IdProducto == id);
            if (producto == null)
            {
                return null;
            }
            ProductoDTO productoDTO = new ProductoDTO
            {
                Nombre = producto.Nombre,
                Marca = producto.Marca,
                PrecioUnitario = producto.PrecioUnitario,
                StockMinimo = producto.StockMinimo,
                AltoCaja = producto.AltoCaja,
                AnchoCaja = producto.AnchoCaja,
                ProfundidadCaja = producto.ProfundidadCaja
            };
            producto.StockDisponible += stockNuevo;
            productoDTO.StockDisponible = producto.StockDisponible;
            producto.FechaActualizacion = DateTime.Now;
            ProductoFiles.EscribirProducto(producto);
            return productoDTO;
        }
        public List<ProductoDTO> ObtenerListaProductos()
        {
            List<ProductoEntity> productos = ProductoFiles.LeerProductosDesdeJson().Where(x => x.FechaEliminacion == null).ToList();
            List<ProductoDTO> productosDTO = new List<ProductoDTO>();
            foreach (var producto in productos)
            {
                ProductoDTO productoDTO = new ProductoDTO
                {

                    Nombre = producto.Nombre,
                    Marca = producto.Marca,
                    AltoCaja = producto.AltoCaja,
                    AnchoCaja = producto.AnchoCaja,
                    ProfundidadCaja = producto.ProfundidadCaja,
                    PrecioUnitario = producto.PrecioUnitario,
                    StockDisponible = producto.StockDisponible,
                    StockMinimo = producto.StockMinimo,

                };
                productosDTO.Add(productoDTO);
            }
            return productosDTO;
        }
        public List<ProductoDTO> FiltrarProductosPorStock(int stock)
        {
            List<ProductoEntity> productos = ProductoFiles.LeerProductosDesdeJson().Where(x => x.FechaEliminacion == null).ToList();
            List<ProductoDTO> productosDTO = new List<ProductoDTO>();
            foreach (var producto in productos)
            {
                if (producto.StockDisponible <= stock)
                {
                    ProductoDTO productoDTO = new ProductoDTO
                    {
                        Nombre = producto.Nombre,
                        Marca = producto.Marca,
                        AltoCaja = producto.AltoCaja,
                        AnchoCaja = producto.AnchoCaja,
                        ProfundidadCaja = producto.ProfundidadCaja,
                        PrecioUnitario = producto.PrecioUnitario,
                        StockDisponible = producto.StockDisponible,
                        StockMinimo = producto.StockMinimo,
                    };
                    productosDTO.Add(productoDTO);
                }
            }
            return productosDTO;
        }
    }
}
