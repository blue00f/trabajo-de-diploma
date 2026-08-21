using DAL;
using Microsoft.Data.SqlClient;
using Servicio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP
{
    public class MAP_Idioma
    {
        DAO_Idioma dao_idioma;

        public MAP_Idioma()
        {
            dao_idioma = new DAO_Idioma();
        }
        public List<SER_Idioma> ObtenerIdiomasDisponibles()
        {
            List<SER_Idioma> idiomas = new List<SER_Idioma>();
            SqlDataReader reader = dao_idioma.ObtenerIdiomasDisponibles();
            while (reader.Read())
            {
                string codigo = reader.GetString(0);   
                string nombre = reader.GetString(1);   
                idiomas.Add(new SER_Idioma(codigo, nombre));
            }
            reader.Close();
            return idiomas;
        }
        public Dictionary<string, string> ObtenerTraducciones(string codigo)
        {
            return dao_idioma.ObtenerTraducciones(codigo);
        }
    }
}
