using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BLL_064RB.Negocio_064RB
{
    public abstract class BLL_ReportePdf_064RB
    {
        protected IContainer CeldaEncabezado_064RB(IContainer contenedor) => contenedor.BorderBottom(1).Padding(2);
        protected IContainer CeldaDato_064RB(IContainer contenedor) => contenedor.BorderBottom(0.25f).Padding(2);
        protected void Recuadro_064RB(IContainer contenedor, string titulo, string valor)
        {
            contenedor.Border(0.5f).Padding(5).Column(col =>
            {
                col.Item().Text(titulo).FontSize(8);
                col.Item().Text(valor).Bold().FontSize(12);
            });
        }
        protected string CrearGraficoBarras_064RB(string titulo, string[] etiquetas, double[] valores, bool horizontal)
        {

            ScottPlot.Plot grafico = new ScottPlot.Plot();
            grafico.Title(titulo);

            var barras = grafico.Add.Bars(valores);
            barras.Horizontal = horizontal;

            int paso = (etiquetas.Length + 11) / 12;
            ScottPlot.Tick[] marcas = new ScottPlot.Tick[etiquetas.Length];
            for (int i = 0; i < etiquetas.Length; i++)
            {
                string texto = (i % paso == 0) ? etiquetas[i] : "";
                marcas[i] = new ScottPlot.Tick(i, texto);
            }
            if (horizontal)
            {
                grafico.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericManual(marcas);
                grafico.Axes.Margins(left: 0);
            }
            else
            {
                grafico.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(marcas);
                grafico.Axes.Margins(bottom: 0);
            }
            return grafico.GetSvgXml(600, 300);
        }
        protected string CrearGraficoDona_064RB(string titulo, string[] etiquetas, double[] valores)
        {
            ScottPlot.Plot grafico = new ScottPlot.Plot();
            grafico.Title(titulo);

            ScottPlot.IPalette paleta = new ScottPlot.Palettes.Category10();
            List<ScottPlot.PieSlice> porciones = new List<ScottPlot.PieSlice>();
            for (int i = 0; i < valores.Length; i++)
            {
                ScottPlot.PieSlice porcion = new ScottPlot.PieSlice();
                porcion.Value = valores[i];
                porcion.FillColor = paleta.GetColor(i);
                porcion.LegendText = etiquetas[i] + " (" + valores[i] + ")";
                porciones.Add(porcion);
            }
            var dona = grafico.Add.Pie(porciones);
            dona.DonutFraction = 0.5;

            grafico.Axes.Frameless();
            grafico.HideGrid();
            grafico.ShowLegend();
            return grafico.GetSvgXml(600, 300);
        }

        protected string CrearGraficoLinea_064RB(string titulo, DateTime[] fechas, double[] valores)
        {
            double[] posiciones = new double[fechas.Length];
            double maximo = 0;
            for (int i = 0; i < fechas.Length; i++)
            {
                posiciones[i] = fechas[i].ToOADate();
                if (valores[i] > maximo) maximo = valores[i];
            }

            ScottPlot.Plot grafico = new ScottPlot.Plot();
            grafico.Title(titulo);
            var linea = grafico.Add.Scatter(posiciones, valores);
            linea.LineWidth = 2;
            linea.MarkerSize = 7;
            grafico.Axes.DateTimeTicksBottom();
            grafico.Axes.SetLimitsY(0, maximo * 1.2 + 1);   // el eje arranca en cero
            return grafico.GetSvgXml(600, 300);
        }
    }
}
