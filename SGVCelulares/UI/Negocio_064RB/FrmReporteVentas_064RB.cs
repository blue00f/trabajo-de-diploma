using BLL_064RB.Negocio_064RB;
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
    public partial class FrmReporteVentas_064RB : Form
    {
        BLL_Reporte_064RB bll_reporte;
        public FrmReporteVentas_064RB()
        {
            InitializeComponent();
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
    }
}
