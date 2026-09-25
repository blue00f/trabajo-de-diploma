namespace Servicio_064RB
{
    public class SER_Bitacora_064RB
    {
        public SER_Usuario_064RB Usuario_064RB { get; set; }
        public DateTime Fecha_064RB { get; set; }
        public string Evento_064RB { get; set; }
        public string Modulo_064RB { get; set; }
        public int Criticidad_064RB { get; set; }
        public SER_Bitacora_064RB(SER_Usuario_064RB usuario, DateTime fecha, string modulo, string evento, int criticidad)
        {
            Usuario_064RB = usuario;
            Fecha_064RB = fecha;
            Modulo_064RB = modulo;
            Evento_064RB = evento;
            Criticidad_064RB = criticidad;
        }
    }
}
