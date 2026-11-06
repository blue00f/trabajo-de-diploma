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

namespace UI_064RB.Negocio_064RB
{
    public partial class FrmAccesoriosCopia_064RB : Form
    {
        BLL_Accesorio_C_064RB bll_accesorio_c;
        public FrmAccesoriosCopia_064RB()
        {
            InitializeComponent();
        }

        private void FrmAccesoriosCopia_064RB_Load(object sender, EventArgs e)
        {
            bll_accesorio_c = new();
            dtpInicio.MaxDate = DateTime.Today;
            dtpFin.MaxDate = DateTime.Today;
            grillaAccesorios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaAccesorios.MultiSelect = false;
            grillaAccesorios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Mostrar(bll_accesorio_c.ConsultarGrilla_064RB());
        }
        private void Mostrar(object datos)
        {
            grillaAccesorios.DataSource = null;
            grillaAccesorios.DataSource = datos;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? fechaInicio, fechaFin;
                ObtenerFechas(out fechaInicio, out fechaFin);
                if (fechaInicio > fechaFin) throw new Exception("La fecha de inicio no puede ser mayor a la fecha de fin!!");
                Mostrar(bll_accesorio_c.ConsultarFiltrado_064RB(txtSku.Text.Trim(), txtMarca.Text.Trim(), txtModelo.Text.Trim(), fechaInicio, fechaFin));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ObtenerFechas(out DateTime? fechaInicio, out DateTime? fechaFin)
        {
            fechaInicio = dtpInicio.Value.Date;
            fechaFin = dtpFin.Value.Date.AddDays(1).AddTicks(-1);
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaAccesorios.Rows.Count == 0) throw new Exception("No hay filas para filtrar!");
                DateTime? fechaInicio, fechaFin;
                ObtenerFechas(out fechaInicio, out fechaFin);
                bll_accesorio_c.Activar_064RB(Convert.ToInt16(grillaAccesorios.CurrentRow.Cells[0].Value));
                Mostrar(bll_accesorio_c.ConsultarFiltrado_064RB(txtSku.Text.Trim(), txtMarca.Text.Trim(), txtModelo.Text.Trim(), fechaInicio, fechaFin));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            Mostrar(bll_accesorio_c.ConsultarGrilla_064RB());
        }

        private void LimpiarControles()
        {
            txtSku.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
