using BE_064RB;
using BLL_064RB.Tecnico_064RB;
using MAP_064RB.Negocio_064RB;
using Servicio_064RB;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Reporte_064RB
    {
        const int TopProductos = 10;

        MAP_Reporte_064RB map_reporte;
        BLL_Producto_064RB bll_producto;
        BLL_ReporteVentasPdf_064RB bll_reporte;
        BLL_Bitacora_064RB bll_bitacora;
        public BLL_Reporte_064RB()
        {
            map_reporte = new();
            bll_producto = new();
            bll_reporte = new();
            bll_bitacora = new();
        }
        public byte[] GenerarReporteVentas_064RB(DateTime desde, DateTime hasta)
        {
            BE_ReporteVentas_064RB reporte = ObtenerReporteVentas_064RB(desde, hasta);
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Reportes", "Generar reporte de ventas", (int)Criticidad_064RB.Baja));
            return bll_reporte.Generar_064RB(reporte);
        }
        
        public BE_ReporteVentas_064RB ObtenerReporteVentas_064RB(DateTime desde, DateTime hasta)
        {
            if (hasta.Date < desde.Date) throw new Exception("La fecha hasta no puede ser anterior a la fecha desde.");

            DateTime hastaExclusivo = hasta.Date.AddDays(1);
            int diasDelPeriodo = (hasta.Date - desde.Date).Days + 1;

            BE_ReporteVentas_064RB reporte = new BE_ReporteVentas_064RB();
            reporte.Desde_064RB = desde.Date;
            reporte.Hasta_064RB = hasta.Date;

            // ventas por día y totales
            List<BE_ReporteDia_064RB> diario = map_reporte.ConsultarVentasPorDia_064RB(desde.Date, hastaExclusivo);
            if (diario.Count == 0) throw new Exception("No hay ventas registradas en el período seleccionado.");

            foreach (BE_ReporteDia_064RB dia in diario)
            {
                reporte.CantidadVentas_064RB += dia.Ventas_064RB;
                reporte.MontoTotal_064RB += dia.Monto_064RB;
            }
            reporte.TicketPromedio_064RB = reporte.MontoTotal_064RB / reporte.CantidadVentas_064RB;

            // para que el gráfico se pueda leer, se agrupa según el largo del período
            reporte.Agrupacion_064RB = ElegirAgrupacion_064RB(diasDelPeriodo);
            reporte.VentasPorDia_064RB = Agrupar_064RB(diario, desde.Date, hasta.Date, reporte.Agrupacion_064RB);

            // formas de pago
            reporte.VentasPorFormaPago_064RB = map_reporte.ConsultarVentasPorFormaPago_064RB(desde.Date, hastaExclusivo);

            // catálogo por SKU, para completar descripción y stock
            Dictionary<string, BE_Producto_064RB> catalogo = new Dictionary<string, BE_Producto_064RB>();
            foreach (BE_Producto_064RB p in bll_producto.Consultar_064RB())
            {
                catalogo.Add(p.Sku_064RB, p);
            }

            // productos vendidos, con su stock y cobertura
            List<BE_ReporteProducto_064RB> vendidos = map_reporte.ConsultarVentasPorProducto_064RB(desde.Date, hastaExclusivo);
            List<string> skusVendidos = new List<string>();
            foreach (BE_ReporteProducto_064RB vendido in vendidos)
            {
                reporte.UnidadesVendidas_064RB += vendido.UnidadesVendidas_064RB;
                skusVendidos.Add(vendido.Sku_064RB);

                if (catalogo.ContainsKey(vendido.Sku_064RB))
                {
                    CompletarProducto_064RB(vendido, catalogo[vendido.Sku_064RB]);
                    // con el ritmo de ventas del período, cuántos días dura el stock actual
                    double unidadesPorDia = (double)vendido.UnidadesVendidas_064RB / diasDelPeriodo;
                    vendido.CoberturaDias_064RB = Math.Round(vendido.StockActual_064RB / unidadesPorDia, 1);
                }
            }
            // el gráfico muestra solo los más vendidos (la lista ya viene ordenada por unidades)
            if (vendidos.Count > TopProductos) reporte.MasVendidos_064RB = vendidos.GetRange(0, TopProductos);
            else reporte.MasVendidos_064RB = new List<BE_ReporteProducto_064RB>(vendidos);

            // la tabla de stock muestra todos los vendidos, primero los que menos días de stock tienen
            reporte.ProductosVendidos_064RB = vendidos.OrderBy(p => p.CoberturaDias_064RB ?? double.MaxValue).ToList();

            // productos con stock que no se vendieron en el período
            List<BE_ReporteProducto_064RB> sinMovimiento = new List<BE_ReporteProducto_064RB>();
            foreach (BE_Producto_064RB p in catalogo.Values)
            {
                if (bll_producto.ObtenerStock_064RB(p) > 0 && !skusVendidos.Contains(p.Sku_064RB))
                {
                    BE_ReporteProducto_064RB item = new BE_ReporteProducto_064RB();
                    item.Sku_064RB = p.Sku_064RB;
                    CompletarProducto_064RB(item, p);
                    sinMovimiento.Add(item);
                }
            }
            reporte.SinMovimiento_064RB = sinMovimiento.OrderByDescending(x => x.StockActual_064RB).Take(TopProductos).ToList();

            return reporte;
        }

        private void CompletarProducto_064RB(BE_ReporteProducto_064RB item, BE_Producto_064RB producto)
        {
            item.Descripcion_064RB = producto.Marca_064RB + " " + producto.Modelo_064RB;
            item.Tipo_064RB = producto is BE_Celular_064RB ? "Celular" : "Accesorio";
            item.StockActual_064RB = bll_producto.ObtenerStock_064RB(producto);
            item.StockMinimo_064RB = producto.StockMinimo_064RB;

            if (item.StockActual_064RB == 0) item.Estado_064RB = "Sin stock";
            else if (item.StockActual_064RB < item.StockMinimo_064RB) item.Estado_064RB = "Bajo el mínimo";
            else item.Estado_064RB = "Normal";
        }
        private string ElegirAgrupacion_064RB(int diasDelPeriodo)
        {
            if (diasDelPeriodo <= 31) return "día";
            if (diasDelPeriodo <= 180) return "semana";
            return "mes";
        }

        private DateTime InicioDeGrupo_064RB(DateTime fecha, string agrupacion)
        {
            if (agrupacion == "día") return fecha.Date;
            if (agrupacion == "semana")
            {
                int diasDesdeElLunes = ((int)fecha.DayOfWeek + 6) % 7;   // el lunes vale 0
                return fecha.Date.AddDays(-diasDesdeElLunes);
            }
            return new DateTime(fecha.Year, fecha.Month, 1);
        }
        private DateTime SiguienteGrupo_064RB(DateTime inicio, string agrupacion)
        {
            if (agrupacion == "día") return inicio.AddDays(1);
            if (agrupacion == "semana") return inicio.AddDays(7);
            return inicio.AddMonths(1);
        }
        private List<BE_ReporteDia_064RB> Agrupar_064RB(List<BE_ReporteDia_064RB> diario, DateTime desde, DateTime hasta, string agrupacion)
        {
            Dictionary<DateTime, BE_ReporteDia_064RB> grupos = new Dictionary<DateTime, BE_ReporteDia_064RB>();
            foreach (BE_ReporteDia_064RB dia in diario)
            {
                DateTime inicio = InicioDeGrupo_064RB(dia.Fecha_064RB, agrupacion);
                if (!grupos.ContainsKey(inicio))
                {
                    BE_ReporteDia_064RB nuevo = new BE_ReporteDia_064RB();
                    nuevo.Fecha_064RB = inicio;
                    grupos.Add(inicio, nuevo);
                }
                grupos[inicio].Ventas_064RB += dia.Ventas_064RB;
                grupos[inicio].Monto_064RB += dia.Monto_064RB;
            }

            // se recorre todo el período para que los grupos sin ventas aparezcan en cero
            List<BE_ReporteDia_064RB> resultado = new List<BE_ReporteDia_064RB>();
            DateTime actual = InicioDeGrupo_064RB(desde, agrupacion);
            DateTime ultimo = InicioDeGrupo_064RB(hasta, agrupacion);
            while (actual <= ultimo)
            {
                if (grupos.ContainsKey(actual))
                {
                    resultado.Add(grupos[actual]);
                }
                else
                {
                    BE_ReporteDia_064RB vacio = new BE_ReporteDia_064RB();
                    vacio.Fecha_064RB = actual;
                    resultado.Add(vacio);
                }
                actual = SiguienteGrupo_064RB(actual, agrupacion);
            }
            return resultado;
        }
    }
}
