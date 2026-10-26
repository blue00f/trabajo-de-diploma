using System.Data;

namespace DAL_064RB.Tecnico_064RB
{
    public class DAO_Idioma_064RB
    {
        ComandoBD_064RB com;
        private readonly string carpeta_064RB = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas");
        public DAO_Idioma_064RB() => com = new();
        public DataTable ObtenerIdiomasDisponibles_064RB() => com.Consultar_064RB("SELECT IdIdioma_064RB, Nombre_064RB FROM Idioma_064RB ORDER BY CASE WHEN IdIdioma_064RB = 'es-AR' THEN 0 ELSE 1 END, Nombre_064RB");
        public Dictionary<string, string> ObtenerTraducciones_064RB(string codigo)
        {
            string ruta = Path.Combine(carpeta_064RB, codigo + ".json");
            if (!File.Exists(ruta)) throw new Exception("No se encontró el archivo del idioma seleccionado.");
            string contenido = File.ReadAllText(ruta);
            return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(contenido) ?? new Dictionary<string, string>();
        }
    }
}
