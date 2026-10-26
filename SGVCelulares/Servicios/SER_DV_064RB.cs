namespace Servicio_064RB
{
    public class SER_DV_064RB
    {
        public string Tabla_064RB { get; set; }
        public string DVH_064RB { get; set; }
        public string DVV_064RB { get; set; }

        public SER_DV_064RB() { }
        public SER_DV_064RB(string tabla, string dvh, string dvv)
        {
            Tabla_064RB = tabla;
            DVH_064RB = dvh;
            DVV_064RB = dvv;
        }
    }
}
