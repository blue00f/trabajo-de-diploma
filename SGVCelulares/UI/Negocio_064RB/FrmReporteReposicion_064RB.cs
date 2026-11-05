using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_064RB.Negocio_064RB
{
    public partial class FrmReporteReposicion_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Reporte_064RB bll_reporte;
        BLL_Idioma_064RB bll_idioma = new();
        public FrmReporteReposicion_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmReporteReposicion_064RB_Load(object sender, EventArgs e)
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
                saveDialog.FileName = $"reporte_reposicion_{DateTime.Now:ddMMyyyy_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] pdf = bll_reporte.GenerarReporteReposicion_064RB(dtpDesde.Value, dtpHasta.Value);
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
            this.Text = bll_idioma.Traducir_064RB("FrmReporteReposicion.Form");
            lblDesde.Text = bll_idioma.Traducir_064RB("FrmReporte.lblDesde");
            lblHasta.Text = bll_idioma.Traducir_064RB("FrmReporte.lblHasta");
            btnGenerar.Text = bll_idioma.Traducir_064RB("FrmReporte.btnGenerar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmReporte.btnSalir");
        }
    }
}
