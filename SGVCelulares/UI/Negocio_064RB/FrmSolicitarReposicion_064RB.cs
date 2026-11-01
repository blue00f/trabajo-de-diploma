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
    public partial class FrmSolicitarReposicion_064RB : Form
    {
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_Producto_064RB bll_producto;
        public FrmSolicitarReposicion_064RB()
        {
            InitializeComponent();
        }

        private void FrmSolicitarReposicion_064RB_Load(object sender, EventArgs e)
        {
            bll_solicitud = new();
            bll_producto = new();
            grillaProductosAReponer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaProductosAReponer.MultiSelect = false;
            grillaProductosAReponer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CargarListado();
        }
        private void CargarListado()
        {
            grillaProductosAReponer.DataSource = null;
            grillaProductosAReponer.DataSource = bll_solicitud.ConsultarProductosAReponerGrilla_064RB();
            nudCantidad.Value = nudCantidad.Minimum;
        }

        private void btnSolicitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaProductosAReponer.Rows.Count == 0) throw new Exception("No hay productos en el listado!");
                string sku = grillaProductosAReponer.CurrentRow.Cells["SKU"].Value.ToString();
                var producto = bll_producto.ConsultarPorSku_064RB(sku);
                bll_solicitud.Solicitar_064RB(producto, (int)nudCantidad.Value);
                MessageBox.Show("Solicitud de reposición registrada", "Solicitar reposición", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
