namespace Servicio_064RB
{
    public class SER_Inconsistencia_064RB
    {
        public string NombreTabla_064RB { get; set; }
        public string IdRegistro_064RB { get; set; }
        public string Mensaje_064RB { get; set; }

        public SER_Inconsistencia_064RB(string nombreTabla, string idRegistro, string mensaje)
        {
            NombreTabla_064RB = nombreTabla;
            IdRegistro_064RB = idRegistro;
            Mensaje_064RB = mensaje;
        }
    }
}
