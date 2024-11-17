using Newtonsoft.Json;
using SistemaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaData
{
    public class ClienteFiles
    {
        private static string ClienteFile = Path.GetFullPath("..//SistemaData//Listas//Cliente.json");
        public static List<ClienteEntity> LeerClientesDesdeJson()
        {
            if (File.Exists($"{ClienteFile}"))
            {
                var json = File.ReadAllText($"{ClienteFile}");
                return JsonConvert.DeserializeObject<List<ClienteEntity>>(json);
            }
            else
            {
                return new List<ClienteEntity>();
            }
        }
        public static void EscribirClienteaJson(ClienteEntity cliente)
        {
            List<ClienteEntity> clientes = LeerClientesDesdeJson();

            if (cliente.IdCliente == 0)
            {
                cliente.IdCliente = clientes.Any() ? clientes.Max(x => x.IdCliente) + 1 : 1;
            }
            else
            {
                clientes.RemoveAll(x => x.IdCliente == cliente.IdCliente);
            }
            clientes.Add(cliente);
            clientes = clientes.OrderBy(x => x.IdCliente).ToList();
            string json = JsonConvert.SerializeObject(clientes, Formatting.Indented);
            File.WriteAllText(ClienteFile, json);
        }
    }
}
