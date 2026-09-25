using DAL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;

namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_Permiso_064RB : IProtegidoPorDV_064RB
    {
        DAO_Permiso_064RB dao_permiso;
        public MAP_Permiso_064RB() => dao_permiso = new();
        public string NombreTabla_064RB => "Permiso_064RB";
        public List<object> ObtenerRegistrosParaDV_064RB() => Consultar_064RB().Cast<object>().ToList();
        public string ObtenerIdParaDV_064RB(object registro) => Convert.ToString(((SER_Permiso_064RB)registro).Id_064RB);
        public List<SER_Permiso_064RB> Consultar_064RB()
        {
            var lista = new List<SER_Permiso_064RB>();
            DataTable tabla = dao_permiso.Consultar_064RB();
            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new SER_Permiso_064RB(fila.ItemArray));
            }
            return lista;
        }
    }
}
