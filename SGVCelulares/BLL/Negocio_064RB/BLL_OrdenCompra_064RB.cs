using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_OrdenCompra_064RB
    {
        MAP_OrdenCompra_064RB map_orden;
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_Bitacora_064RB bll_bitacora;
        BLL_DV_064RB bll_dv;

        public BLL_OrdenCompra_064RB()
        {
            map_orden = new();
            bll_solicitud = new();
            bll_bitacora = new();
            bll_dv = new();
        }
        public int Efectuar_064RB(int idSolicitud)
        {
            if (idSolicitud <= 0) throw new Exception("Seleccione una solicitud del listado.");
            var solicitud = bll_solicitud.ObtenerPorId_064RB(idSolicitud) ?? throw new Exception("No se encontró la solicitud seleccionada.");
            if (solicitud.Estado_064RB != EstadoSolicitud_064RB.Aprobada_064RB) throw new Exception("La solicitud seleccionada ya no está en estado aprobada.");

            var orden = new BE_OrdenCompra_064RB
            {
                SolicitudCompra_064RB = solicitud,
                Fecha_064RB = DateTime.Now,
                MontoPagado_064RB = solicitud.MontoTotal_064RB ?? throw new Exception("La solicitud no tiene una cotización registrada.")
            };

            int numOrden = map_orden.Agregar_064RB(orden);
            if (numOrden == 0) throw new Exception("La solicitud seleccionada ya no está en estado aprobada.");
            orden.NumOrden_064RB = numOrden;
            SER_Usuario_064RB usu = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(usu, DateTime.Now, "Órdenes de compra", "Alta", 3));
            bll_dv.ActualizarDV_064RB("OrdenCompra_064RB", "SolicitudCompra_064RB");
            return numOrden;
        }
    }
}
