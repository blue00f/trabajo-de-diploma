namespace Servicio_064RB
{
    public class SER_DVRegistro_064RB
    {
        public string Tabla_064RB { get; set; }
        public string IdRegistro_064RB { get; set; }
        public string DVH_064RB { get; set; }
        public string Datos_064RB { get; set; }
        public SER_DVRegistro_064RB() { }
        public SER_DVRegistro_064RB(string tabla, string idRegistro, string dvh, string datos)
        {
            Tabla_064RB = tabla;
            IdRegistro_064RB = idRegistro;
            DVH_064RB = dvh;
            Datos_064RB = datos;
        }
    }
}
