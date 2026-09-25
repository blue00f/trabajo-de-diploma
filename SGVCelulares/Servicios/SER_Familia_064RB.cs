namespace Servicio_064RB
{
    public class SER_Familia_064RB : SER_Componente_064RB
    {
        public List<SER_Componente_064RB> Componentes_064RB { get; set; }
        public SER_Familia_064RB(int id, string nombre) : base(id, nombre)
        {
            Componentes_064RB = new List<SER_Componente_064RB>();
        }

        public SER_Familia_064RB(string nombre) : base(nombre)
        {
            Nombre_064RB = nombre;
            Componentes_064RB = new List<SER_Componente_064RB>();
        }
        public SER_Familia_064RB(object[] datos) : base(Convert.ToInt16(datos[0]), Convert.ToString(datos[1]))
        {
            Componentes_064RB = new List<SER_Componente_064RB>();
        }
        public override bool TienePermiso(string nombrePermiso)
        {
            foreach (SER_Componente_064RB hijo in Componentes_064RB)
            {
                if (hijo.TienePermiso(nombrePermiso))
                    return true;
            }
            return false;
        }
    }
}
