namespace Servicio_064RB
{
    public class SER_SessionManager_064RB
    {
        public SER_Usuario_064RB Usuario_064RB { get; set; }
        private static SER_SessionManager_064RB sesion_064RB;
        private SER_SessionManager_064RB() { }
        static public SER_SessionManager_064RB ObtenerSesion_064RB()
        {
            if (sesion_064RB == null)
            {
                sesion_064RB = new SER_SessionManager_064RB();
            }
            return sesion_064RB;
        }
        static public void CerrarSesion_064RB() => sesion_064RB = null;
    }
}
