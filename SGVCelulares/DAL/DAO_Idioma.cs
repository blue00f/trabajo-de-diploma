using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAO_Idioma : Conexion
    {
        SqlCommand cm;
        private readonly string carpeta =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas");
        public DAO_Idioma() : base()
        {
            cm = new SqlCommand();
            cm.Connection = con;
        }
        public SqlDataReader ObtenerIdiomasDisponibles()
        {
            cm.Parameters.Clear();
            cm.CommandText = "SELECT idIdioma, Nombre FROM idioma ORDER BY CASE WHEN idIdioma = 'es-AR' THEN 0 ELSE 1 END, Nombre";
            con.Open();
            return cm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public Dictionary<string, string> ObtenerTraducciones(string codigo)
        {
            string ruta = Path.Combine(carpeta, codigo + ".json");

            if (!File.Exists(ruta))
                throw new Exception("No se encontró el archivo del idioma seleccionado.");

            string contenido = File.ReadAllText(ruta);

            return System.Text.Json.JsonSerializer
                       .Deserialize<Dictionary<string, string>>(contenido)
                   ?? new Dictionary<string, string>();
        }
    }
}
