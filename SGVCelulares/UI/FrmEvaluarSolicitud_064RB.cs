using BLL_064RB.Negocio_064RB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_064RB
{
    public partial class FrmEvaluarSolicitud_064RB : Form
    {
        BLL_SolicitudCompra_064RB bll_solicitud;
        public FrmEvaluarSolicitud_064RB()
        {
            InitializeComponent();
        }

        private void FrmEvaluarSolicitud_064RB_Load(object sender, EventArgs e)
        {
            bll_solicitud = new();
            grillaSolicitudes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaSolicitudes.MultiSelect = false;
            grillaSolicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CargarGrilla();
        }
        private void CargarGrilla()
        {
            grillaSolicitudes.DataSource = null;
            grillaSolicitudes.DataSource = bll_solicitud.ConsultarCotizadasGrilla_064RB();
            txtMotivo.Clear();
        }
        private void Evaluar(bool aprobar)
        {
            try
            {
                if (grillaSolicitudes.Rows.Count == 0) throw new Exception("No hay solicitudes para evaluar!");
                int idSolicitud = Convert.ToInt16(grillaSolicitudes.CurrentRow.Cells[0].Value);
                bll_solicitud.Evaluar_064RB(idSolicitud, aprobar, txtMotivo.Text);
                MessageBox.Show(aprobar ? "Solicitud aprobada." : "Solicitud rechazada.", "Evaluar solicitud de compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAprobar_Click(object sender, EventArgs e) => Evaluar(true);

        private void btnRechazar_Click(object sender, EventArgs e) => Evaluar(false);

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
