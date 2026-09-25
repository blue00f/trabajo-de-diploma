namespace Servicio_064RB
{
    public class SER_Rol_064RB 
    {
        public int Id_064RB { get; set; }
        public string Nombre_064RB { get; set; }
        public List<SER_Componente_064RB> Componentes_064RB { get; set; }
        public SER_Rol_064RB(int id, string nombre) 
        {
            Id_064RB = id;
            Nombre_064RB = nombre;
            Componentes_064RB = new List<SER_Componente_064RB>();
        }
        public SER_Rol_064RB(string nombre)
        {
            Nombre_064RB = nombre;
            Componentes_064RB = new List<SER_Componente_064RB>();
        }
        public SER_Rol_064RB(object[] datos)
        {
            Id_064RB = Convert.ToInt16(datos[0]);
            Nombre_064RB = Convert.ToString(datos[1]);
            Componentes_064RB = new List<SER_Componente_064RB>();
        }
        public override string ToString()
        {
            return $"{this.Nombre_064RB}";
        }
        public bool TienePermiso(string nombrePermiso)
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
