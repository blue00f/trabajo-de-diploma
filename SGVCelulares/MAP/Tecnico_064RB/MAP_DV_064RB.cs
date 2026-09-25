using DAL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;
namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_DV_064RB
    {
        DAO_DV_064RB dao_dv;
        public MAP_DV_064RB() => dao_dv = new();
        public void AgregarDVH_064RB(SER_DVH_064RB dvh) => dao_dv.AgregarDVH_064RB(dvh.NombreTabla_064RB, dvh.IdRegistro_064RB, dvh.Valor_064RB);
        public void BorrarDVH_064RB(string nombreTabla, string idRegistro) => dao_dv.BorrarDVH_064RB(nombreTabla, idRegistro);
        public void ModificarDVH_064RB(SER_DVH_064RB dvh) => dao_dv.ModificarDVH_064RB(dvh.NombreTabla_064RB, dvh.IdRegistro_064RB, dvh.Valor_064RB);
        public void AgregarDVV_064RB(SER_DVV_064RB dvv) => dao_dv.AgregarDVV_064RB(dvv.NombreTabla_064RB, dvv.Valor_064RB);
        public void BorrarDVV_064RB(string nombreTabla) => dao_dv.BorrarDVV_064RB(nombreTabla);
        public void ModificarDVV_064RB(SER_DVV_064RB dvv) => dao_dv.ModificarDVV_064RB(dvv.NombreTabla_064RB, dvv.Valor_064RB);
        public bool ExisteDVH_064RB(string nombreTabla, string id) => dao_dv.ExisteDVH_064RB(nombreTabla, id);
        public bool ExisteDVV_064RB(string nombreTabla) => dao_dv.ExisteDVV_064RB(nombreTabla);
        public SER_DVH_064RB ObtenerDVH_064RB(string nombreTabla, string idRegistro)
        {
            DataTable tabla = dao_dv.ObtenerDVH_064RB(nombreTabla, idRegistro);
            return tabla.Rows.Count > 0 ? new SER_DVH_064RB(tabla.Rows[0].ItemArray) : null;
        }
        public SER_DVV_064RB ObtenerDVV_064RB(string nombreTabla)
        {
            DataTable tabla = dao_dv.ObtenerDVV_064RB(nombreTabla);
            return tabla.Rows.Count > 0 ? new SER_DVV_064RB(tabla.Rows[0].ItemArray) : null;
        }
        public List<string> ConsultarDVHValores_064RB(string nombreTabla)
        {
            var valores = new List<string>();
            DataTable tabla = dao_dv.ConsultarDVH_064RB(nombreTabla);
            foreach (DataRow fila in tabla.Rows)
            {
                valores.Add(fila["valor"].ToString());
            }
            return valores;
        }
        public List<string> ConsultarDVHIds_064RB(string nombreTabla)
        {
            var ids = new List<string>();
            DataTable tabla = dao_dv.ConsultarDVH_064RB(nombreTabla);
            foreach (DataRow fila in tabla.Rows)
            {
                ids.Add(fila["idRegistro"].ToString());
            }
            return ids;
        }
    }
}
