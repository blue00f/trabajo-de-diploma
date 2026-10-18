using BE_064RB;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BLL_064RB.Negocio_064RB
{
    public class BLL_Comprobante_064RB
    {
        BLL_Venta_064RB bll_venta;
        public BLL_Comprobante_064RB()
        {
            bll_venta = new();
        }
        public byte[] Generar_064RB(int numVenta)
        {
            var comprobante = bll_venta.ObtenerComprobante(numVenta);
            return GenerarTicket_064RB(comprobante);
        }
        private byte[] GenerarTicket_064RB(BE_Comprobante_064RB c)
        {
            return Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.ContinuousSize(80, Unit.Millimetre);
                    page.Margin(4, Unit.Millimetre);
                    page.DefaultTextStyle(t => t.FontSize(8));

                    page.Content().Column(col =>
                    {
                        col.Spacing(2);
                        col.Item().AlignCenter().Text("SAN TSUNG").Bold().FontSize(12);
                        col.Item().AlignCenter().Text("Comprobante de venta");
                        col.Item().LineHorizontal(0.5f);
                        col.Item().Text($"N° {c.NumVenta_064RB}");
                        col.Item().Text($"Fecha: {c.Fecha_064RB:dd/MM/yyyy HH:mm}");
                        col.Item().Text($"Cliente: {c.NombreCliente_064RB} (DNI {c.DniCliente_064RB})");
                        col.Item().LineHorizontal(0.5f);

                        foreach (var item in c.Items_064RB)
                        {
                            col.Item().Text(item.Descripcion_064RB);
                            col.Item().Row(row =>
                            {
                                row.RelativeItem().Text($"{item.Cantidad_064RB} x {item.PrecioUnitario_064RB:C2}");
                                row.ConstantItem(70).AlignRight().Text($"{item.Subtotal_064RB:C2}");
                            });
                        }

                        col.Item().LineHorizontal(0.5f);
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Text("TOTAL").Bold();
                            row.ConstantItem(70).AlignRight().Text($"{c.MontoTotal_064RB:C2}").Bold();
                        });
                        col.Item().Text($"Forma de pago: {c.FormaPago_064RB}");
                    });
                });
            }).GeneratePdf();
        }
    }
}
