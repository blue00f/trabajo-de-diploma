namespace Servicio_064RB
{
    public class SER_Idioma_064RB
    {
        public string Codigo_064RB { get; set; }
        public string Nombre_064RB { get; set; }
        public Dictionary<string, string> Textos_064RB { get; set; }
        public SER_Idioma_064RB()
        {
            Textos_064RB = new Dictionary<string, string>();
        }

        public SER_Idioma_064RB(string codigo)
        {
            Codigo_064RB = codigo;
            Textos_064RB = new Dictionary<string, string>();
        }

        public SER_Idioma_064RB(string codigo, Dictionary<string, string> textos)
        {
            Codigo_064RB = codigo;
            Textos_064RB = textos ?? new Dictionary<string, string>();
        }
        public SER_Idioma_064RB(string codigo, string nombre)
        {
            Codigo_064RB = codigo;
            Nombre_064RB = nombre;
            Textos_064RB = new Dictionary<string, string>();
        }

    }
}
