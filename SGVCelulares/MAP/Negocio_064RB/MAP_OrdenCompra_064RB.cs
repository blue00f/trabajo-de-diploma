using BE_064RB;
using DAL_064RB.Negocio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_OrdenCompra_064RB
    {
        DAO_OrdenCompra_064RB dao_orden;
        public MAP_OrdenCompra_064RB() => dao_orden = new();
        public int Agregar_064RB(BE_OrdenCompra_064RB orden) => dao_orden.Agregar_064RB(orden.SolicitudCompra_064RB.IdSolicitud_064RB, orden.Fecha_064RB, orden.MontoPagado_064RB, EstadoSolicitud_064RB.PendienteDeRecepcion_064RB, EstadoSolicitud_064RB.Aprobada_064RB);
        public List<BE_OrdenCompra_064RB> ConsultarPendientesDeRecepcion_064RB(List<BE_SolicitudCompra_064RB> solicitudes)
        {
            List<BE_OrdenCompra_064RB> ordenes = new List<BE_OrdenCompra_064RB>();
            DataTable dt = dao_orden.ConsultarPorEstadoDeSolicitud_064RB(EstadoSolicitud_064RB.PendienteDeRecepcion_064RB);

            foreach (DataRow fila in dt.Rows)
            {
                int idSolicitud = Convert.ToInt32(fila["IdSolicitud_064RB"]);
                foreach (BE_SolicitudCompra_064RB solicitud in solicitudes)
                {
                    if (solicitud.IdSolicitud_064RB == idSolicitud)
                    {
                        BE_OrdenCompra_064RB orden = new BE_OrdenCompra_064RB();
                        orden.NumOrden_064RB = Convert.ToInt32(fila["NumOrden_064RB"]);
                        orden.Fecha_064RB = Convert.ToDateTime(fila["Fecha_064RB"]);
                        orden.MontoPagado_064RB = Convert.ToDecimal(fila["MontoPagado_064RB"]);
                        orden.SolicitudCompra_064RB = solicitud;
                        ordenes.Add(orden);
                    }
                }
            }
            return ordenes;
        }
    }
}
