namespace Servicio_064RB
{
    public class SER_Permiso_064RB : SER_Componente_064RB
    {
        public SER_Permiso_064RB(int id, string nombre) : base(id, nombre)
        {
        }

        public SER_Permiso_064RB(object[] datos) : base(Convert.ToInt16(datos[0]), Convert.ToString(datos[1]))
        {
        }
        public override bool TienePermiso(string nombrePermiso)
        {
            return this.Nombre_064RB == nombrePermiso;
        }
    }
}
