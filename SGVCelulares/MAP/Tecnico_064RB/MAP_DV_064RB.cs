using DAL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;
namespace MAP_064RB.Tecnico_064RB
{
    public class MAP_DV_064RB
    {
        public const string FilaBD_064RB = DAO_DV_064RB.FilaBD_064RB;
        DAO_DV_064RB dao_dv;
        public MAP_DV_064RB() => dao_dv = new();
        public List<string> ObtenerTablas_064RB() => dao_dv.ObtenerTablas_064RB();
        public List<string> ObtenerClavePrimaria_064RB(string tabla) => dao_dv.ObtenerClavePrimaria_064RB(tabla);
        public DataTable ObtenerRegistros_064RB(string tabla) => dao_dv.ObtenerRegistros_064RB(tabla);
        public List<SER_DV_064RB> ObtenerDV_064RB() => dao_dv.ObtenerDV_064RB().AsEnumerable().Select(r => new SER_DV_064RB(r.Field<string>("Tabla_064RB")!, r.Field<string>("DVH_064RB")!, r.Field<string>("DVV_064RB")!)).ToList();
        public List<SER_DVRegistro_064RB> ObtenerRegistrosDV_064RB() => MapearRegistros_064RB(dao_dv.ObtenerRegistrosDV_064RB());
        public List<SER_DVRegistro_064RB> ObtenerRegistrosDV_064RB(string tabla) => MapearRegistros_064RB(dao_dv.ObtenerRegistrosDV_064RB(tabla));
        public void Guardar_064RB(List<SER_DV_064RB> dvs, List<SER_DVRegistro_064RB> registrosUpsert, List<SER_DVRegistro_064RB> registrosEliminar) =>
            dao_dv.Guardar_064RB(
                dvs.Select(d => (d.Tabla_064RB, d.DVH_064RB, d.DVV_064RB)).ToList(),
                registrosUpsert.Select(r => (r.Tabla_064RB, r.IdRegistro_064RB, r.DVH_064RB, r.Datos_064RB)).ToList(),
                registrosEliminar.Select(r => (r.Tabla_064RB, r.IdRegistro_064RB)).ToList());
        public void ReemplazarTodo_064RB(List<SER_DV_064RB> dvs, List<SER_DVRegistro_064RB> registros) =>
            dao_dv.ReemplazarTodo_064RB(
                dvs.Select(d => (d.Tabla_064RB, d.DVH_064RB, d.DVV_064RB)).ToList(),
                registros.Select(r => (r.Tabla_064RB, r.IdRegistro_064RB, r.DVH_064RB, r.Datos_064RB)).ToList());
        private static List<SER_DVRegistro_064RB> MapearRegistros_064RB(DataTable dt) =>
            dt.AsEnumerable()
                .Select(r => new SER_DVRegistro_064RB(r.Field<string>("Tabla_064RB")!, r.Field<string>("IdRegistro_064RB")!, r.Field<string>("DVH_064RB")!, r.Field<string>("Datos_064RB")!))
                .ToList();
    }
}
