using BE_064RB;
using DAL_064RB.Negocio_064RB;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_RemitoCompra_064RB
    {
        DAO_RemitoCompra_064RB dao_remito;
        public MAP_RemitoCompra_064RB() => dao_remito = new();
        public bool ExisteImei_064RB(string imei) => dao_remito.ContarImei_064RB(imei) > 0;

        public int Agregar_064RB(BE_RemitoCompra_064RB remito, BE_Discrepancia_064RB? discrepancia, List<string> imeis, int cantidadConforme)
        {
            BE_SolicitudCompra_064RB solicitud = remito.OrdenCompra_064RB.SolicitudCompra_064RB;

            bool hayDiscrepancia = false;
            int faltante = 0;
            int danada = 0;
            string observaciones = "";
            if (discrepancia != null)
            {
                hayDiscrepancia = true;
                faltante = discrepancia.CantidadFaltante_064RB;
                danada = discrepancia.CantidadDanada_064RB;
                observaciones = discrepancia.Observaciones_064RB;
            }

            return dao_remito.Agregar_064RB(remito.OrdenCompra_064RB.NumOrden_064RB, solicitud.IdSolicitud_064RB,
                remito.NumRemito_064RB, remito.Estado_064RB, remito.FechaRecepcion_064RB,
                hayDiscrepancia, faltante, danada, observaciones,
                solicitud.Producto_064RB.Sku_064RB, solicitud.Producto_064RB is BE_Celular_064RB, cantidadConforme, imeis,
                EstadoSolicitud_064RB.Recibida_064RB, EstadoSolicitud_064RB.PendienteDeRecepcion_064RB);
        }
    }
}
