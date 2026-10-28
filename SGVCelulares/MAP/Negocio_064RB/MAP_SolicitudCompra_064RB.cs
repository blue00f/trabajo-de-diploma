using BE_064RB;
using DAL_064RB.Negocio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_SolicitudCompra_064RB
    {
        DAO_SolicitudCompra_064RB dao_solicitud;
        public MAP_SolicitudCompra_064RB() => dao_solicitud = new();
        public List<string> ConsultarSkusConSolicitudEnCurso_064RB() => dao_solicitud.ConsultarSkusConSolicitudEnCurso_064RB().Rows.Cast<DataRow>().Select(f => f["SkuProducto_064RB"].ToString()!).ToList();
        public void Agregar_064RB(BE_SolicitudCompra_064RB s) => dao_solicitud.Agregar_064RB(s.Producto_064RB.Sku_064RB, s.Cantidad_064RB, s.Estado_064RB, s.FechaSolicitud_064RB);

        public List<BE_SolicitudCompra_064RB> ConsultarPorEstado_064RB(string estado, Dictionary<string, BE_Producto_064RB> productosPorSku, Dictionary<string, BE_Proveedor_064RB> proveedoresPorCuit) => dao_solicitud.ConsultarPorEstado_064RB(estado).Rows.Cast<DataRow>().Select(f => MapearSolicitud_064RB(f, productosPorSku, proveedoresPorCuit)).ToList();
        public BE_SolicitudCompra_064RB? ConsultarPorId_064RB(int idSolicitud, Dictionary<string, BE_Producto_064RB> productosPorSku, Dictionary<string, BE_Proveedor_064RB> proveedoresPorCuit)
        {
            DataTable dt = dao_solicitud.ConsultarPorId_064RB(idSolicitud);
            return dt.Rows.Count == 0 ? null : MapearSolicitud_064RB(dt.Rows[0], productosPorSku, proveedoresPorCuit);
        }
        public bool Cotizar_064RB(BE_SolicitudCompra_064RB s) => dao_solicitud.Cotizar_064RB(s.IdSolicitud_064RB, s.Proveedor_064RB!.Cuit_064RB, s.PrecioUnitario_064RB!.Value, s.MontoTotal_064RB!.Value, EstadoSolicitud_064RB.Cotizada_064RB, EstadoSolicitud_064RB.PendienteDeCompra_064RB) == 1;
        public bool Evaluar_064RB(BE_SolicitudCompra_064RB s) => dao_solicitud.Evaluar_064RB(s.IdSolicitud_064RB, s.Estado_064RB, s.FechaDecision_064RB!.Value, s.Motivo_064RB, EstadoSolicitud_064RB.Cotizada_064RB) == 1;
        private BE_SolicitudCompra_064RB MapearSolicitud_064RB(DataRow f, Dictionary<string, BE_Producto_064RB> productosPorSku, Dictionary<string, BE_Proveedor_064RB> proveedoresPorCuit)
        {
            BE_Proveedor_064RB? proveedor = null;
            if (f["CuitProveedor_064RB"] != DBNull.Value) proveedoresPorCuit.TryGetValue(f["CuitProveedor_064RB"].ToString()!, out proveedor);

            return new BE_SolicitudCompra_064RB
            {
                IdSolicitud_064RB = Convert.ToInt32(f["IdSolicitud_064RB"]),
                Producto_064RB = productosPorSku[f["SkuProducto_064RB"].ToString()!],
                Cantidad_064RB = Convert.ToInt32(f["Cantidad_064RB"]),
                Estado_064RB = f["Estado_064RB"].ToString()!,
                FechaSolicitud_064RB = Convert.ToDateTime(f["FechaSolicitud_064RB"]),
                Proveedor_064RB = proveedor,
                PrecioUnitario_064RB = f["PrecioUnitario_064RB"] == DBNull.Value ? null : Convert.ToDecimal(f["PrecioUnitario_064RB"]),
                MontoTotal_064RB = f["MontoTotal_064RB"] == DBNull.Value ? null : Convert.ToDecimal(f["MontoTotal_064RB"]),
                FechaDecision_064RB = f["FechaDecision_064RB"] == DBNull.Value ? null : Convert.ToDateTime(f["FechaDecision_064RB"]),
                Motivo_064RB = f["Motivo_064RB"] == DBNull.Value ? null : f["Motivo_064RB"].ToString()
            };
        }
    }
}
