using BE_064RB;
using DAL_064RB.Negocio_064RB;
using System.Data;

namespace MAP_064RB.Negocio_064RB
{
    public class MAP_Reporte_064RB
    {
        DAO_Reporte_064RB dao_reporte;
        public MAP_Reporte_064RB() => dao_reporte = new();
        public List<BE_ReporteDia_064RB> ConsultarVentasPorDia_064RB(DateTime desde, DateTime hastaExclusivo)
        {
            List<BE_ReporteDia_064RB> lista = new List<BE_ReporteDia_064RB>();
            foreach (DataRow fila in dao_reporte.ConsultarVentasPorDia_064RB(desde, hastaExclusivo).Rows)
            {
                BE_ReporteDia_064RB dia = new BE_ReporteDia_064RB();
                dia.Fecha_064RB = Convert.ToDateTime(fila["Dia"]);
                dia.Ventas_064RB = Convert.ToInt32(fila["Ventas"]);
                dia.Monto_064RB = Convert.ToDecimal(fila["Monto"]);
                lista.Add(dia);
            }
            return lista;
        }
        public List<BE_ReporteFormaPago_064RB> ConsultarVentasPorFormaPago_064RB(DateTime desde, DateTime hastaExclusivo)
        {
            List<BE_ReporteFormaPago_064RB> lista = new List<BE_ReporteFormaPago_064RB>();
            foreach (DataRow fila in dao_reporte.ConsultarVentasPorFormaPago_064RB(desde, hastaExclusivo).Rows)
            {
                BE_ReporteFormaPago_064RB forma = new BE_ReporteFormaPago_064RB();
                forma.FormaPago_064RB = fila["FormaPago"].ToString();
                forma.Ventas_064RB = Convert.ToInt32(fila["Ventas"]);
                forma.Monto_064RB = Convert.ToDecimal(fila["Monto"]);
                lista.Add(forma);
            }
            return lista;
        }
        public List<BE_ReporteProducto_064RB> ConsultarVentasPorProducto_064RB(DateTime desde, DateTime hastaExclusivo)
        {
            List<BE_ReporteProducto_064RB> lista = new List<BE_ReporteProducto_064RB>();
            foreach (DataRow fila in dao_reporte.ConsultarVentasPorProducto_064RB(desde, hastaExclusivo).Rows)
            {
                BE_ReporteProducto_064RB producto = new BE_ReporteProducto_064RB();
                producto.Sku_064RB = fila["Sku"].ToString();
                producto.UnidadesVendidas_064RB = Convert.ToInt32(fila["Unidades"]);
                producto.MontoVendido_064RB = Convert.ToDecimal(fila["Monto"]);
                lista.Add(producto);
            }
            return lista;
        }
    }
}
