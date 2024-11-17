using Newtonsoft.Json;
using SistemaEntities;

namespace SistemaData
{
    public class CamionetaFiles
    {
        private static string CamionetaFile = Path.GetFullPath("..//SistemaData//Listas//Camioneta.json");
        public static List<CamionetaEntity> LeerCamionetasDesdeJson()
        {
            if (File.Exists($"{CamionetaFile}"))
            {
                var json = File.ReadAllText($"{CamionetaFile}");
                return JsonConvert.DeserializeObject<List<CamionetaEntity>>(json);
            }
            else
            {
                return new List<CamionetaEntity>();
            }
        }
        public static void EscribirCamioneta(CamionetaEntity camioneta)
        {
            List<CamionetaEntity> camionetas = LeerCamionetasDesdeJson();

            if (camioneta.IdCamioneta == 0)
            {
                camioneta.IdCamioneta = camionetas.Any() ? camionetas.Max(x => x.IdCamioneta) + 1 : 1;
            }
            else
            {
                camionetas.RemoveAll(x => x.IdCamioneta == camioneta.IdCamioneta);
            }
            camionetas.Add(camioneta);
            camionetas = camionetas.OrderBy(x => x.IdCamioneta).ToList();
            string json = JsonConvert.SerializeObject(camionetas, Formatting.Indented);
            File.WriteAllText(CamionetaFile, json);
        }
    }
}
