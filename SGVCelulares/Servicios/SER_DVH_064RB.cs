namespace Servicio_064RB
{
    public class SER_DVH_064RB
    {
        public string NombreTabla_064RB { get; set; }
        public string IdRegistro_064RB { get; set; }
        public string Valor_064RB { get; set; }
        public SER_DVH_064RB(string nombreTabla, string idRegistro, string valor)
        {
            NombreTabla_064RB = nombreTabla;
            IdRegistro_064RB = idRegistro;
            Valor_064RB = valor;
        }
        public SER_DVH_064RB(object[] datos)
        {
            NombreTabla_064RB = datos[0].ToString();
            IdRegistro_064RB = datos[1].ToString();
            Valor_064RB = datos[2].ToString();
        }
    }
}
