using DAL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;

namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_Idioma_064RB
    {
        DAO_Idioma_064RB dao_idioma;
        public MAP_Idioma_064RB() => dao_idioma = new();
        public List<SER_Idioma_064RB> ObtenerIdiomasDisponibles_064RB()
        {
            var idiomas = new List<SER_Idioma_064RB>();
            DataTable tabla = dao_idioma.ObtenerIdiomasDisponibles_064RB();
            foreach (DataRow fila in tabla.Rows)
            {
                idiomas.Add(Mapear_064RB(fila));
            }
            return idiomas;
        }
        public Dictionary<string, string> ObtenerTraducciones_064RB(string codigo) => dao_idioma.ObtenerTraducciones_064RB(codigo);
        private SER_Idioma_064RB Mapear_064RB(DataRow fila) => new SER_Idioma_064RB(Convert.ToString(fila[0]), Convert.ToString(fila[1]));
    }
}
