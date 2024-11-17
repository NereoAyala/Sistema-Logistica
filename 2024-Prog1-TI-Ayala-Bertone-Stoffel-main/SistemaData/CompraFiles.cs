using Newtonsoft.Json;
using SistemaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaData
{
    public class CompraFiles
    {
        private static string CompraFile = Path.GetFullPath("..//SistemaData//Listas//Compra.json");
        public static List<CompraEntity> LeerCompraDesdeJson()
        {
            if (File.Exists($"{CompraFile}"))
            {
                var json = File.ReadAllText($"{CompraFile}");
                return JsonConvert.DeserializeObject<List<CompraEntity>>(json);
            }
            else
            {
                return new List<CompraEntity>();
            }
        }
        public static void EscribirCompra(CompraEntity compra)
        {
            List<CompraEntity> compras = LeerCompraDesdeJson();

            if (compra.IdCompra == 0)
            {
                compra.IdCompra = compras.Any() ? compras.Max(x => x.IdCompra) + 1 : 1;
            }
            else
            {
                compras.RemoveAll(x => x.IdCompra == compra.IdCompra);
            }
            compras.Add(compra);
            compras = compras.OrderBy(x => x.IdCompra).ToList();
            string json = JsonConvert.SerializeObject(compras, Formatting.Indented);
            File.WriteAllText(CompraFile, json);
        }
    }
}
