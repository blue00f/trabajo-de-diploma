namespace BLL_064RB.Tecnico_064RB
{
    public class TablaDV_064RB
    {
        public string NombreTabla_064RB { get; set; }
        public Func<List<object>> ObtenerRegistros_064RB { get; set; }
        public Func<object, string> ObtenerId_064RB { get; set; }
    }
}
