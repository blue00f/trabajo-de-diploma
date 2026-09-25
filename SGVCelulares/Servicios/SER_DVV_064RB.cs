namespace Servicio_064RB
{
    public class SER_DVV_064RB
    {
        public string NombreTabla_064RB { get; set; }
        public string Valor_064RB { get; set; }
        public SER_DVV_064RB(string nombreTabla, string valor)
        {
            NombreTabla_064RB = nombreTabla;
            Valor_064RB = valor;
        }
        public SER_DVV_064RB(object[] datos)
        {
            NombreTabla_064RB = datos[0].ToString();
            Valor_064RB = datos[1].ToString();
        }
    }
}
