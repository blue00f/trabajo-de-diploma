using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Diagnostics;

namespace UI_064RB.Negocio_064RB
{
    public partial class FrmReporteVentas_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Reporte_064RB bll_reporte;
        BLL_Idioma_064RB bll_idioma = new();
        public FrmReporteVentas_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }
        private void FrmReporteVentas_064RB_Load(object sender, EventArgs e)
        {
            bll_reporte = new();
            dtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpHasta.Value = DateTime.Today;
        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                using SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF (*.pdf)|*.pdf";
                saveDialog.FileName = $"reporte_ventas_{DateTime.Now:ddMMyyyy_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                { 
                    byte[] pdf = bll_reporte.GenerarReporteVentas_064RB(dtpDesde.Value, dtpHasta.Value);
                    File.WriteAllBytes(saveDialog.FileName, pdf);
                    Process.Start(new ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmReporteVentas.Form");
            lblDesde.Text = bll_idioma.Traducir_064RB("FrmReporte.lblDesde");
            lblHasta.Text = bll_idioma.Traducir_064RB("FrmReporte.lblHasta");
            btnGenerar.Text = bll_idioma.Traducir_064RB("FrmReporte.btnGenerar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmReporte.btnSalir");
        }
    }
}
