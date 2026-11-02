using BE_064RB;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_ReporteReposicionPdf_064RB : BLL_ReportePdf_064RB
    {
        public byte[] Generar_064RB(BE_ReporteReposicion_064RB r)
        {
            // gráfico 1: estado de las solicitudes (dona), en el orden del proceso
            string[] ordenEstados =
            {
            EstadoSolicitud_064RB.PendienteDeCompra_064RB, EstadoSolicitud_064RB.Cotizada_064RB, EstadoSolicitud_064RB.Aprobada_064RB,
            EstadoSolicitud_064RB.PendienteDeRecepcion_064RB, EstadoSolicitud_064RB.Recibida_064RB, EstadoSolicitud_064RB.Rechazada_064RB
        };
            List<string> etiquetasEstado = new List<string>();
            List<double> valoresEstado = new List<double>();
            foreach (string estado in ordenEstados)
            {
                if (r.SolicitudesPorEstado_064RB.ContainsKey(estado))
                {
                    etiquetasEstado.Add(estado);
                    valoresEstado.Add(r.SolicitudesPorEstado_064RB[estado]);
                }
            }
            string svgEstados = CrearGraficoDona_064RB("Estado de las solicitudes", etiquetasEstado.ToArray(), valoresEstado.ToArray());

            // gráfico 2: solicitudes en el tiempo (línea)
            int cantidadPuntos = r.SolicitudesPorPeriodo_064RB.Count;
            DateTime[] fechas = new DateTime[cantidadPuntos];
            double[] cantidades = new double[cantidadPuntos];
            for (int i = 0; i < cantidadPuntos; i++)
            {
                fechas[i] = r.SolicitudesPorPeriodo_064RB[i].Fecha_064RB;
                cantidades[i] = r.SolicitudesPorPeriodo_064RB[i].Ventas_064RB;   // aquí "Ventas" cuenta solicitudes
            }
            string svgTiempo = CrearGraficoLinea_064RB("Solicitudes por " + r.Agrupacion_064RB, fechas, cantidades);

            // gráfico 3: monto pagado por proveedor (barras horizontales, hasta 8; al revés para que el mayor quede arriba)
            int cantidadProveedores = Math.Min(8, r.Proveedores_064RB.Count);
            string[] etiquetasProveedor = new string[cantidadProveedores];
            double[] montosProveedor = new double[cantidadProveedores];
            for (int i = 0; i < cantidadProveedores; i++)
            {
                etiquetasProveedor[cantidadProveedores - 1 - i] = r.Proveedores_064RB[i].RazonSocial_064RB;
                montosProveedor[cantidadProveedores - 1 - i] = (double)r.Proveedores_064RB[i].MontoPagado_064RB;
            }
            string svgProveedores = "";
            if (cantidadProveedores > 0)
                svgProveedores = CrearGraficoBarras_064RB("Monto pagado por proveedor ($)", etiquetasProveedor, montosProveedor, true);

            return Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(15, Unit.Millimetre);
                    page.DefaultTextStyle(t => t.FontSize(9));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("SAN TSUNG - Reporte de reposición de stock").Bold().FontSize(16);
                        col.Item().Text("Solicitudes realizadas del " + r.Desde_064RB.ToString("dd/MM/yyyy") + " al " + r.Hasta_064RB.ToString("dd/MM/yyyy"));
                        col.Item().Text("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    });

                    page.Content().PaddingVertical(8).Column(col =>
                    {
                        col.Spacing(8);

                        col.Item().Row(row =>
                        {
                            row.Spacing(5);
                            row.RelativeItem().Element(c => Recuadro_064RB(c, "Solicitudes", r.CantidadSolicitudes_064RB.ToString()));
                            row.RelativeItem().Element(c => Recuadro_064RB(c, "Unidades solicitadas", r.UnidadesSolicitadas_064RB.ToString()));
                            row.RelativeItem().Element(c => Recuadro_064RB(c, "Monto pagado", r.MontoPagado_064RB.ToString("C0")));
                            row.RelativeItem().Element(c => Recuadro_064RB(c, "Pendientes de recepción", r.PendientesDeRecepcion_064RB.ToString()));
                        });

                        col.Item().Height(170).Row(row =>
                        {
                            row.RelativeItem().Svg(svgEstados);
                            row.RelativeItem().Svg(svgTiempo);
                        });
                        if (svgProveedores != "") col.Item().Height(170).Svg(svgProveedores);

                        col.Item().Text("Desempeño de los proveedores").Bold().FontSize(11);
                        col.Item().Text("Órdenes de compra de las solicitudes del período. Demora: días entre la orden y la recepción.").FontSize(8).Italic();
                        col.Item().Element(c => TablaProveedores_064RB(c, r.Proveedores_064RB));

                        col.Item().Text("Reposición por producto").Bold().FontSize(11);
                        col.Item().Text("Stock al " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " (momento en que se generó el reporte).").FontSize(8).Italic();
                        col.Item().Element(c => TablaProductos_064RB(c, r.Productos_064RB));

                        col.Item().Text("Productos que hoy necesitan reposición").Bold().FontSize(11);
                        col.Item().Text("Con stock por debajo del mínimo y sin una solicitud en curso.").FontSize(8).Italic();
                        col.Item().Element(c => TablaAReponer_064RB(c, r.ProductosAReponerHoy_064RB));

                        col.Item().Text("Detalle de las solicitudes").Bold().FontSize(11);
                        col.Item().Element(c => TablaDetalle_064RB(c, r.Detalle_064RB));
                    });

                    page.Footer().AlignCenter().Text(t =>
                    {
                        t.Span("Página ");
                        t.CurrentPageNumber();
                    });
                });
            }).GeneratePdf();
        }

        private void TablaProveedores_064RB(IContainer contenedor, List<BE_ReporteProveedor_064RB> proveedores)
        {
            if (proveedores.Count == 0)
            {
                contenedor.Text("Ninguna solicitud del período llegó a orden de compra.");
                return;
            }
            contenedor.Table(tabla =>
            {
                tabla.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(3);     // proveedor
                    c.RelativeColumn(1);     // órdenes
                    c.RelativeColumn(2);     // monto pagado
                    c.RelativeColumn(1);     // recibidas
                    c.RelativeColumn(1.5f);  // demora
                    c.RelativeColumn(1.5f);  // remitos no conformes
                    c.RelativeColumn(1);     // faltantes
                    c.RelativeColumn(1);     // dañadas
                });
                tabla.Header(h =>
                {
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Proveedor").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Órdenes").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Monto pagado").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Recibidas").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Demora (días)").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Remitos no conf.").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Faltantes").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Dañadas").Bold();
                });
                foreach (BE_ReporteProveedor_064RB p in proveedores)
                {
                    string demora = p.DemoraPromedioDias_064RB.HasValue ? p.DemoraPromedioDias_064RB.Value.ToString("0.0") : "-";
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.RazonSocial_064RB);
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Ordenes_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.MontoPagado_064RB.ToString("C0"));
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Recibidas_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(demora);
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.RemitosNoConformes_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Faltantes_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Danadas_064RB.ToString());
                }
            });
        }

        private void TablaProductos_064RB(IContainer contenedor, List<BE_ReporteProductoReposicion_064RB> productos)
        {
            contenedor.Table(tabla =>
            {
                tabla.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(3);
                    c.RelativeColumn(1);
                    c.RelativeColumn(1.5f);
                    c.RelativeColumn(1.5f);
                    c.RelativeColumn(1);
                    c.RelativeColumn(1);
                    c.RelativeColumn(2);
                });
                tabla.Header(h =>
                {
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Producto").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Solicitudes").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Unid. pedidas").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Unid. recibidas conf.").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Stock").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Mínimo").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Estado").Bold();
                });
                foreach (BE_ReporteProductoReposicion_064RB p in productos)
                {
                    var colorEstado = p.Estado_064RB == "Normal" ? Colors.Black : Colors.Red.Medium;
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Descripcion_064RB);
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Solicitudes_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.UnidadesSolicitadas_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.UnidadesRecibidas_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.StockActual_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.StockMinimo_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Estado_064RB).FontColor(colorEstado);
                }
            });
        }

        private void TablaAReponer_064RB(IContainer contenedor, List<BE_ReporteProducto_064RB> productos)
        {
            if (productos.Count == 0)
            {
                contenedor.Text("Ningún producto está por debajo del stock mínimo sin solicitud en curso.");
                return;
            }
            contenedor.Table(tabla =>
            {
                tabla.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(3);
                    c.RelativeColumn(1.5f);
                    c.RelativeColumn(1);
                    c.RelativeColumn(1);
                    c.RelativeColumn(2);
                });
                tabla.Header(h =>
                {
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Producto").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Tipo").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Stock").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Mínimo").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Estado").Bold();
                });
                foreach (BE_ReporteProducto_064RB p in productos)
                {
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Descripcion_064RB);
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Tipo_064RB);
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.StockActual_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.StockMinimo_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(p.Estado_064RB).FontColor(Colors.Red.Medium);
                }
            });
        }

        private void TablaDetalle_064RB(IContainer contenedor, List<BE_ReporteSolicitud_064RB> solicitudes)
        {
            contenedor.Table(tabla =>
            {
                tabla.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(1.5f);  // fecha
                    c.RelativeColumn(3);     // producto
                    c.RelativeColumn(2.5f);  // proveedor
                    c.RelativeColumn(1);     // cantidad
                    c.RelativeColumn(2);     // estado
                    c.RelativeColumn(2);     // monto cotizado
                    c.RelativeColumn(2);     // monto pagado
                });
                tabla.Header(h =>
                {
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Fecha").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Producto").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Proveedor").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Cant.").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Estado").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Cotizado").Bold();
                    h.Cell().Element(CeldaEncabezado_064RB).Text("Pagado").Bold();
                });
                foreach (BE_ReporteSolicitud_064RB s in solicitudes)
                {
                    string proveedor = s.Proveedor_064RB != null ? s.Proveedor_064RB : "-";
                    string cotizado = s.MontoCotizado_064RB.HasValue ? s.MontoCotizado_064RB.Value.ToString("C0") : "-";
                    string pagado = s.MontoPagado_064RB.HasValue ? s.MontoPagado_064RB.Value.ToString("C0") : "-";

                    tabla.Cell().Element(CeldaDato_064RB).Text(s.FechaSolicitud_064RB.ToString("dd/MM/yyyy"));
                    tabla.Cell().Element(CeldaDato_064RB).Text(s.Descripcion_064RB);
                    tabla.Cell().Element(CeldaDato_064RB).Text(proveedor);
                    tabla.Cell().Element(CeldaDato_064RB).Text(s.Cantidad_064RB.ToString());
                    tabla.Cell().Element(CeldaDato_064RB).Text(s.Estado_064RB);
                    tabla.Cell().Element(CeldaDato_064RB).Text(cotizado);
                    tabla.Cell().Element(CeldaDato_064RB).Text(pagado);
                }
            });
        }
    }
}
