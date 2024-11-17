namespace SistemaEntities
{
    public class CamionetaEntity : FechaBase
    {
        public int IdCamioneta { get; set; }
        public string Patente { get; set; }
        public double TamañoCarga { get; set; }
        public int DistanciaMax { get; set; }
    }
}
