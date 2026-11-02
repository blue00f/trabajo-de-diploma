using BE_064RB;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_ReporteVentasPdf_064RB : BLL_ReportePdf_064RB
    {
        public byte[] Generar_064RB(BE_ReporteVentas_064RB r)
        {
            // Monto por dia
            string[] etiquetasDias = new string[r.VentasPorDia_064RB.Count];
            double[] montosDias = new double[r.VentasPorDia_064RB.Count];
            string formato = r.Agrupacion_064RB == "mes" ? "MM/yyyy" : "dd/MM";
            for (int i = 0; i < r.VentasPorDia_064RB.Count; i++)
            {
                etiquetasDias[i] = r.VentasPorDia_064RB[i].Fecha_064RB.ToString(formato);
                montosDias[i] = (double)r.VentasPorDia_064RB[i].Monto_064RB;
            }
            string svgDias = CrearGraficoBarras_064RB("Monto vendido por " + r.Agrupacion_064RB + " ($)", etiquetasDias, montosDias, false);

            // Formas de pago
            string[] etiquetasPago = new string[r.VentasPorFormaPago_064RB.Count];
            double[] montosPago = new double[r.VentasPorFormaPago_064RB.Count];
            for (int i = 0; i < r.VentasPorFormaPago_064RB.Count; i++)
            {
                etiquetasPago[i] = r.VentasPorFormaPago_064RB[i].FormaPago_064RB;
                montosPago[i] = (double)r.VentasPorFormaPago_064RB[i].Monto_064RB;
            }
            string svgPago = CrearGraficoBarras_064RB("Monto vendido por forma de pago ($)", etiquetasPago, montosPago, false);

            // Productos mas vendidos
            int cantidad = r.MasVendidos_064RB.Count;
            string[] etiquetasProd = new string[cantidad];
            double[] unidadesProd = new double[cantidad];
            for (int i = 0; i < cantidad; i++)
            {
                etiquetasProd[cantidad - 1 - i] = r.MasVendidos_064RB[i].Descripcion_064RB;
                unidadesProd[cantidad - 1 - i] = r.MasVendidos_064RB[i].UnidadesVendidas_064RB;
            }
            string svgProd = CrearGraficoBarras_064RB("Productos más vendidos (unidades)", etiquetasProd, unidadesProd, true);

            return Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(15, Unit.Millimetre);
                    page.DefaultTextStyle(t => t.FontSize(9));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("SAN TSUNG - Reporte de ventas").Bold().FontSize(16);
                        col.Item().Text("Período: " + r.Desde_064RB.ToString("dd/MM/yyyy") + " al " + r.Hasta_064RB.ToString("dd/MM/yyyy"));
                        col.Item().Text("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    });

                    page.Content().PaddingVertical(8).Column(col =>
                    {
                        col.Spacing(8);

                        col.Item().Row(row =>
                        {
                            row.Spacing(5);
                            row.RelativeItem().Element(c => Recuadro_064RB(c, "Ventas", r.CantidadVentas_064RB.ToString()));
                            row.RelativeItem().Element(c => Recuadro_064RB(c, "Unidades vendidas", r.UnidadesVendidas_064RB.ToString()));
                            row.RelativeItem().Element(c => Recuadro_064RB(c, "Monto total", r.MontoTotal_064RB.ToString("C0")));
                            row.RelativeItem().Element(c => Recuadro_064RB(c, "Ticket promedio", r.TicketPromedio_064RB.ToString("C0")));
                        });

                        col.Item().Height(190).Svg(svgDias);
                        if (r.Agrupacion_064RB == "semana") col.Item().Text("Cada barra es una semana; la fecha indica el lunes en que empieza.").FontSize(8).Italic();

                        col.Item().Height(160).Svg(svgPago);
                        col.Item().Height(190).Svg(svgProd);


                        col.Item().Text("Stock actual de los productos vendidos").Bold().FontSize(11);
                        col.Item().Text("Stock al " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " (momento en que se generó el reporte).").FontSize(8).Italic();
                        col.Item().Element(c => TablaVendidos_064RB(c, r.ProductosVendidos_064RB));

                        col.Item().Text("Productos con stock y sin ventas en el período").Bold().FontSize(11);
                        col.Item().Element(c => TablaSinMovimiento_064RB(c, r.SinMovimiento_064RB));
                    });

                    page.Footer().AlignCenter().Text(t =>
                    {
                        t.Span("Página ");
                        t.CurrentPageNumber();
                    });
                });
            }).GeneratePdf();
        }

        private void TablaVendidos_064RB(IContainer contenedor, List<BE_ReporteProducto_064RB> productos)
        {
            contenedor.Table(tabla =>
            {
                tabla.ColumnsDefinition(c =>
                {
                    c.RelativeColumn(3); // producto
                    c.RelativeColumn(1); // vendidas
                    c.RelativeColumn(2); // monto
                    c.RelativeColumn(1); // stock
                    c.RelativeColumn(1); // mínimo
                    c.RelativeColumn(1.5f); // cobertura
                    c.RelativeColumn(2); // estado
                });
                tabla.Header(h =>
                {
                    h.Cell().BorderBottom(1).Padding(2).Text("Producto").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Vendidas").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Monto").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Stock").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Mínimo").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Cobertura (días)").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Estado").Bold();
                });
                foreach (BE_ReporteProducto_064RB p in productos)
                {
                    string cobertura = p.CoberturaDias_064RB.HasValue ? p.CoberturaDias_064RB.Value.ToString("0.0") : "-";
                    var colorEstado = p.Estado_064RB == "Normal" ? Colors.Black : Colors.Red.Medium;

                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.Descripcion_064RB);
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.UnidadesVendidas_064RB.ToString());
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.MontoVendido_064RB.ToString("C0"));
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.StockActual_064RB.ToString());
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.StockMinimo_064RB.ToString());
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(cobertura);
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.Estado_064RB).FontColor(colorEstado);
                }
            });
        }

        private void TablaSinMovimiento_064RB(IContainer contenedor, List<BE_ReporteProducto_064RB> productos)
        {
            if (productos.Count == 0)
            {
                contenedor.Text("Todos los productos con stock tuvieron ventas en el período.");
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
                });
                tabla.Header(h =>
                {
                    h.Cell().BorderBottom(1).Padding(2).Text("Producto").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Tipo").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Stock").Bold();
                    h.Cell().BorderBottom(1).Padding(2).Text("Mínimo").Bold();
                });
                foreach (BE_ReporteProducto_064RB p in productos)
                {
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.Descripcion_064RB);
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.Tipo_064RB);
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.StockActual_064RB.ToString());
                    tabla.Cell().BorderBottom(0.25f).Padding(2).Text(p.StockMinimo_064RB.ToString());
                }
            });
        }
    }
}
