using BE_064RB;
using DAL_064RB.Negocio_064RB;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_OrdenCompra_064RB
    {
        DAO_OrdenCompra_064RB dao_orden;
        public MAP_OrdenCompra_064RB() => dao_orden = new();
        public int Agregar_064RB(BE_OrdenCompra_064RB orden) => dao_orden.Agregar_064RB(orden.SolicitudCompra_064RB.IdSolicitud_064RB, orden.Fecha_064RB, orden.MontoPagado_064RB, EstadoSolicitud_064RB.PendienteDeRecepcion_064RB, EstadoSolicitud_064RB.Aprobada_064RB);
    }
}
