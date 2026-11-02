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
        BLL_ReporteVentasPdf_064RB bll_reporteVentaPdf;
        BLL_Bitacora_064RB bll_bitacora;


        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_ReporteReposicionPdf_064RB bll_reporteReposicionPdf;
        public BLL_Reporte_064RB()
        {
            map_reporte = new();
            bll_producto = new();
            bll_reporteVentaPdf = new();
            bll_bitacora = new();

            bll_solicitud = new();
            bll_reporteReposicionPdf = new();
        }
        public byte[] GenerarReporteVentas_064RB(DateTime desde, DateTime hasta)
        {
            BE_ReporteVentas_064RB reporte = ObtenerReporteVentas_064RB(desde, hasta);
            bll_bitacora.RegistrarBitacora_064RB(new SER_Bitacora_064RB(SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB, DateTime.Now, "Reportes", "Generar reporte de ventas", (int)Criticidad_064RB.Baja));
            return bll_reporteVentaPdf.Generar_064RB(reporte);
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


        public byte[] GenerarReporteReposicion_064RB(DateTime desde, DateTime hasta)
        {
            BE_ReporteReposicion_064RB reporte = ObtenerReporteReposicion_064RB(desde, hasta);
            return bll_reporteReposicionPdf.Generar_064RB(reporte);
        }

        public BE_ReporteReposicion_064RB ObtenerReporteReposicion_064RB(DateTime desde, DateTime hasta)
        {
            if (hasta.Date < desde.Date) throw new Exception("La fecha hasta no puede ser anterior a la fecha desde.");

            DateTime hastaExclusivo = hasta.Date.AddDays(1);
            int diasDelPeriodo = (hasta.Date - desde.Date).Days + 1;

            List<BE_ReporteSolicitud_064RB> solicitudes = map_reporte.ConsultarSolicitudes_064RB(desde.Date, hastaExclusivo);
            if (solicitudes.Count == 0)
                throw new Exception("No hay solicitudes de compra realizadas en el período seleccionado.");

            Dictionary<string, BE_Producto_064RB> catalogo = ObtenerCatalogo_064RB();

            BE_ReporteReposicion_064RB reporte = new BE_ReporteReposicion_064RB();
            reporte.Desde_064RB = desde.Date;
            reporte.Hasta_064RB = hasta.Date;
            reporte.CantidadSolicitudes_064RB = solicitudes.Count;
            reporte.Detalle_064RB = solicitudes;

            Dictionary<DateTime, BE_ReporteDia_064RB> porDia = new Dictionary<DateTime, BE_ReporteDia_064RB>();
            Dictionary<string, BE_ReporteProveedor_064RB> porProveedor = new Dictionary<string, BE_ReporteProveedor_064RB>();
            Dictionary<string, BE_ReporteProductoReposicion_064RB> porProducto = new Dictionary<string, BE_ReporteProductoReposicion_064RB>();

            foreach (BE_ReporteSolicitud_064RB s in solicitudes)
            {
                // descripción del producto, para el detalle
                if (catalogo.ContainsKey(s.Sku_064RB))
                    s.Descripcion_064RB = catalogo[s.Sku_064RB].Marca_064RB + " " + catalogo[s.Sku_064RB].Modelo_064RB;
                else
                    s.Descripcion_064RB = s.Sku_064RB;

                // totales
                reporte.UnidadesSolicitadas_064RB += s.Cantidad_064RB;
                if (s.MontoPagado_064RB.HasValue) reporte.MontoPagado_064RB += s.MontoPagado_064RB.Value;

                // por estado
                if (!reporte.SolicitudesPorEstado_064RB.ContainsKey(s.Estado_064RB))
                    reporte.SolicitudesPorEstado_064RB.Add(s.Estado_064RB, 0);
                reporte.SolicitudesPorEstado_064RB[s.Estado_064RB]++;

                // por día (en este reporte, "Ventas" de BE_ReporteDia cuenta solicitudes)
                DateTime dia = s.FechaSolicitud_064RB.Date;
                if (!porDia.ContainsKey(dia))
                {
                    BE_ReporteDia_064RB nuevoDia = new BE_ReporteDia_064RB();
                    nuevoDia.Fecha_064RB = dia;
                    porDia.Add(dia, nuevoDia);
                }
                porDia[dia].Ventas_064RB++;

                // unidades recibidas en condiciones conformes (solo cuando ya se recibió)
                int recibidasConformes = 0;
                if (s.Estado_064RB == EstadoSolicitud_064RB.Recibida_064RB)
                    recibidasConformes = s.Cantidad_064RB - s.Faltante_064RB - s.Danada_064RB;

                // por producto
                if (!porProducto.ContainsKey(s.Sku_064RB))
                {
                    BE_ReporteProductoReposicion_064RB nuevoProducto = new BE_ReporteProductoReposicion_064RB();
                    nuevoProducto.Sku_064RB = s.Sku_064RB;
                    nuevoProducto.Descripcion_064RB = s.Descripcion_064RB;
                    porProducto.Add(s.Sku_064RB, nuevoProducto);
                }
                BE_ReporteProductoReposicion_064RB producto = porProducto[s.Sku_064RB];
                producto.Solicitudes_064RB++;
                producto.UnidadesSolicitadas_064RB += s.Cantidad_064RB;
                producto.UnidadesRecibidas_064RB += recibidasConformes;

                // por proveedor (solo las solicitudes que llegaron a orden de compra)
                if (s.MontoPagado_064RB.HasValue && s.Cuit_064RB != null)
                {
                    if (!porProveedor.ContainsKey(s.Cuit_064RB))
                    {
                        BE_ReporteProveedor_064RB nuevoProveedor = new BE_ReporteProveedor_064RB();
                        nuevoProveedor.Cuit_064RB = s.Cuit_064RB;
                        nuevoProveedor.RazonSocial_064RB = s.Proveedor_064RB ?? s.Cuit_064RB;
                        porProveedor.Add(s.Cuit_064RB, nuevoProveedor);
                    }
                    BE_ReporteProveedor_064RB proveedor = porProveedor[s.Cuit_064RB];
                    proveedor.Ordenes_064RB++;
                    proveedor.MontoPagado_064RB += s.MontoPagado_064RB.Value;
                    if (s.FechaRecepcion_064RB.HasValue && s.FechaOrden_064RB.HasValue)
                    {
                        proveedor.Recibidas_064RB++;
                        proveedor.DiasDemoraTotal_064RB += (s.FechaRecepcion_064RB.Value.Date - s.FechaOrden_064RB.Value.Date).Days;
                    }
                    if (s.EstadoRemito_064RB == EstadoRemito_064RB.NoConforme_064RB) proveedor.RemitosNoConformes_064RB++;
                    proveedor.Faltantes_064RB += s.Faltante_064RB;
                    proveedor.Danadas_064RB += s.Danada_064RB;
                }
            }

            if (reporte.SolicitudesPorEstado_064RB.ContainsKey(EstadoSolicitud_064RB.PendienteDeRecepcion_064RB))
                reporte.PendientesDeRecepcion_064RB = reporte.SolicitudesPorEstado_064RB[EstadoSolicitud_064RB.PendienteDeRecepcion_064RB];

            // solicitudes en el tiempo, agrupadas como en el reporte de ventas
            reporte.Agrupacion_064RB = ElegirAgrupacion_064RB(diasDelPeriodo);
            reporte.SolicitudesPorPeriodo_064RB = Agrupar_064RB(new List<BE_ReporteDia_064RB>(porDia.Values), desde.Date, hasta.Date, reporte.Agrupacion_064RB);

            // proveedores: demora promedio y orden por monto pagado
            foreach (BE_ReporteProveedor_064RB proveedor in porProveedor.Values)
            {
                if (proveedor.Recibidas_064RB > 0)
                    proveedor.DemoraPromedioDias_064RB = Math.Round((double)proveedor.DiasDemoraTotal_064RB / proveedor.Recibidas_064RB, 1);
            }
            reporte.Proveedores_064RB = porProveedor.Values.OrderByDescending(p => p.MontoPagado_064RB).ToList();

            // productos: stock actual contra el mínimo
            foreach (BE_ReporteProductoReposicion_064RB item in porProducto.Values)
            {
                if (catalogo.ContainsKey(item.Sku_064RB))
                {
                    BE_Producto_064RB productoCatalogo = catalogo[item.Sku_064RB];
                    item.StockActual_064RB = bll_producto.ObtenerStock_064RB(productoCatalogo);
                    item.StockMinimo_064RB = productoCatalogo.StockMinimo_064RB;
                }
                item.Estado_064RB = EstadoDeStock_064RB(item.StockActual_064RB, item.StockMinimo_064RB);
            }
            reporte.Productos_064RB = porProducto.Values.OrderByDescending(p => p.UnidadesSolicitadas_064RB).ToList();

            // lo que hoy necesita reposición: es el mismo listado de CU-NEG-4
            foreach (BE_Producto_064RB p in bll_solicitud.ConsultarProductosAReponer_064RB())
            {
                BE_ReporteProducto_064RB item = new BE_ReporteProducto_064RB();
                item.Sku_064RB = p.Sku_064RB;
                CompletarProducto_064RB(item, p);
                reporte.ProductosAReponerHoy_064RB.Add(item);
            }

            return reporte;
        }

        private Dictionary<string, BE_Producto_064RB> ObtenerCatalogo_064RB()
        {
            Dictionary<string, BE_Producto_064RB> catalogo = new Dictionary<string, BE_Producto_064RB>();
            foreach (BE_Producto_064RB p in bll_producto.Consultar_064RB())
            {
                catalogo.Add(p.Sku_064RB, p);
            }
            return catalogo;
        }

        private string EstadoDeStock_064RB(int stock, int minimo)
        {
            if (stock == 0) return "Sin stock";
            if (stock < minimo) return "Bajo el mínimo";
            return "Normal";
        }
    }
}
