using Newtonsoft.Json;
using SistemaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaData
{
    public class ProductoFiles
    {
        private static string ProductoFile = Path.GetFullPath("..//SistemaData//Listas//Producto.json");
        public static List<ProductoEntity> LeerProductosDesdeJson()
        {
            if (File.Exists($"{ProductoFile}"))
            {
                var json = File.ReadAllText($"{ProductoFile}");
                return JsonConvert.DeserializeObject<List<ProductoEntity>>(json);
            }
            else
            {
                return new List<ProductoEntity>();
            }
        }
        public static void EscribirProducto(ProductoEntity producto)
        {
            List<ProductoEntity> productos = LeerProductosDesdeJson();

            if (producto.IdProducto == 0)
            {
                producto.IdProducto = productos.Any() ? productos.Max(x => x.IdProducto) + 1 : 1;
            }
            else
            {
                productos.RemoveAll(x => x.IdProducto == producto.IdProducto);
            }
            productos.Add(producto);
            productos = productos.OrderBy(x => x.IdProducto).ToList();
            string json = JsonConvert.SerializeObject(productos, Formatting.Indented);
            File.WriteAllText(ProductoFile, json);
        }
    }
}
