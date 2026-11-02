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
    }
}
