using Newtonsoft.Json;
using SistemaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaData
{
    public class ViajeFiles
    {
        private static string ViajeFile = Path.GetFullPath("..//SistemaData//Listas//Viaje.json");
        public static List<ViajeEntity> LeerViajesDesdeJson()
        {
            if (File.Exists($"{ViajeFile}"))
            {
                var json = File.ReadAllText($"{ViajeFile}");
                return JsonConvert.DeserializeObject<List<ViajeEntity>>(json);
            }
            else
            {
                return new List<ViajeEntity>();
            }
        }
        public static void EscribirViaje(ViajeEntity viaje)
        {
            List<ViajeEntity> viajes = LeerViajesDesdeJson();

            if (viaje.IdViaje == 0)
            {
                viaje.IdViaje = viajes.Any() ? viajes.Max(x => x.IdViaje) + 1 : 1;
            }
            else
            {
                viajes.RemoveAll(x => x.IdViaje == viaje.IdViaje);
            }
            viajes.Add(viaje);
            viajes = viajes.OrderBy(x => x.IdViaje).ToList();
            string json = JsonConvert.SerializeObject(viajes, Formatting.Indented);
            File.WriteAllText(ViajeFile, json);
        }
    }
}
