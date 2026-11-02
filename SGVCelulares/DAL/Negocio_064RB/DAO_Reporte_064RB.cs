using System.Data;

namespace DAL_064RB.Negocio_064RB
{
    public class DAO_Reporte_064RB
    {
        ComandoBD_064RB com;
        public DAO_Reporte_064RB() => com = new();
        private List<ParametroBD_064RB> Periodo_064RB(DateTime desde, DateTime hastaExclusivo)
        {
            return new List<ParametroBD_064RB>
            {
                new("@desde", SqlDbType.DateTime, desde),
                new("@hasta", SqlDbType.DateTime, hastaExclusivo)
            };
        }
        public DataTable ConsultarVentasPorDia_064RB(DateTime desde, DateTime hastaExclusivo)
        {
            return com.Consultar_064RB(
                @"select cast(Fecha_064RB as date) as Dia, count(*) as Ventas, sum(MontoTotal_064RB) as Monto
                  from Venta_064RB
                  where Fecha_064RB >= @desde and Fecha_064RB < @hasta
                  group by cast(Fecha_064RB as date)
                  order by Dia", Periodo_064RB(desde, hastaExclusivo));
        }
        public DataTable ConsultarVentasPorFormaPago_064RB(DateTime desde, DateTime hastaExclusivo)
        {
            return com.Consultar_064RB(
                @"select FormaPago_064RB as FormaPago, count(*) as Ventas, sum(MontoTotal_064RB) as Monto
                  from Venta_064RB
                  where Fecha_064RB >= @desde and Fecha_064RB < @hasta
                  group by FormaPago_064RB
                  order by Monto desc", Periodo_064RB(desde, hastaExclusivo));
        }
        public DataTable ConsultarVentasPorProducto_064RB(DateTime desde, DateTime hastaExclusivo)
        {
            return com.Consultar_064RB(
                @"select d.SkuProducto_064RB as Sku, sum(d.Cantidad_064RB) as Unidades, sum(d.Subtotal_064RB) as Monto
                  from DetalleVenta_064RB d
                  inner join Venta_064RB v on v.NumVenta_064RB = d.NumVenta_064RB
                  where v.Fecha_064RB >= @desde and v.Fecha_064RB < @hasta
                  group by d.SkuProducto_064RB
                  order by Unidades desc", Periodo_064RB(desde, hastaExclusivo));
        }

        public DataTable ConsultarSolicitudes_064RB(DateTime desde, DateTime hastaExclusivo)
        {
            return com.Consultar_064RB(
                @"select s.SkuProducto_064RB as Sku, s.Cantidad_064RB as Cantidad, s.Estado_064RB as Estado,
                s.FechaSolicitud_064RB as FechaSolicitud, s.MontoTotal_064RB as MontoCotizado,
                s.CuitProveedor_064RB as Cuit, pr.RazonSocial_064RB as Proveedor,
                o.Fecha_064RB as FechaOrden, o.MontoPagado_064RB as MontoPagado,
                r.Estado_064RB as EstadoRemito, r.FechaRecepcion_064RB as FechaRecepcion,
                isnull(d.CantidadFaltante_064RB, 0) as Faltante, isnull(d.CantidadDanada_064RB, 0) as Danada
                from SolicitudCompra_064RB s
                left join Proveedor_064RB pr on pr.Cuit_064RB = s.CuitProveedor_064RB
                left join OrdenCompra_064RB o on o.IdSolicitud_064RB = s.IdSolicitud_064RB
                left join RemitoCompra_064RB r on r.NumOrden_064RB = o.NumOrden_064RB
                left join Discrepancia_064RB d on d.IdRemito_064RB = r.IdRemito_064RB
                where s.FechaSolicitud_064RB >= @desde and s.FechaSolicitud_064RB < @hasta
                order by s.FechaSolicitud_064RB", Periodo_064RB(desde, hastaExclusivo));
        }
    }
}
