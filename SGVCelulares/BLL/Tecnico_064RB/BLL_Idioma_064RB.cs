using MAP_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace BLL_064RB.Tecnico_064RB
{
    public class BLL_Idioma_064RB
    {
        private static readonly List<IObservadorIdioma_064RB> observadores_064RB = new List<IObservadorIdioma_064RB>();
        private static SER_Idioma_064RB idiomaActual_064RB = new SER_Idioma_064RB("es-AR");
        MAP_Idioma_064RB map_idioma;
        public string IdiomaActual => idiomaActual_064RB.Codigo_064RB;
        public BLL_Idioma_064RB()
        {
            map_idioma = new MAP_Idioma_064RB();  
        }
        public void Suscribir_064RB(IObservadorIdioma_064RB observador)
        {
            if (!observadores_064RB.Contains(observador)) observadores_064RB.Add(observador);
        }
        public void Desuscribir_064RB(IObservadorIdioma_064RB observador)
        {
            observadores_064RB.Remove(observador);
        }
        public void CambiarIdioma_064RB(string codigo)
        {
            Dictionary<string, string> textos = map_idioma.ObtenerTraducciones_064RB(codigo);
            idiomaActual_064RB = new SER_Idioma_064RB(codigo, textos);
            Notificar_064RB();
        }
        public string Traducir_064RB(string clave)
        {
            return idiomaActual_064RB.Textos_064RB.TryGetValue(clave, out string valor) ? valor : clave;
        }
        public List<SER_Idioma_064RB> ObtenerIdiomasDisponibles_064RB()
        {
            return map_idioma.ObtenerIdiomasDisponibles_064RB();
        }
        private void Notificar_064RB()
        {
            foreach (IObservadorIdioma_064RB observador in observadores_064RB.ToArray())
            {
                observador.ActualizarIdioma_064RB();
            }
        }
    }
}
