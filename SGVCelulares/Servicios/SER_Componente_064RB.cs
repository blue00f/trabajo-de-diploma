namespace Servicio_064RB
{
    abstract public class SER_Componente_064RB
    {
        public int Id_064RB { get; set; }
        public string Nombre_064RB { get; set; }
        public SER_Componente_064RB(int id, string nombre)
        {
            Id_064RB = id;
            Nombre_064RB = nombre;
        }
        public SER_Componente_064RB(string nombre)
        {
            Nombre_064RB = nombre;
        }
        public SER_Componente_064RB(object[] datos)
        {
            Id_064RB = Convert.ToInt16(datos[0]);
            Nombre_064RB = Convert.ToString(datos[1]);
        }
        public override string ToString()
        {
            return $"{this.Nombre_064RB}";
        }
        public abstract bool TienePermiso(string nombrePermiso);
    }
}
