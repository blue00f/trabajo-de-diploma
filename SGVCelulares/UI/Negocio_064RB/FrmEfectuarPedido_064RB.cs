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
    public partial class FrmEfectuarPedido_064RB : Form
    {
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_OrdenCompra_064RB bll_orden;
        public FrmEfectuarPedido_064RB()
        {
            InitializeComponent();
        }

        private void FrmEfectuarPedido_064RB_Load(object sender, EventArgs e)
        {
            bll_solicitud = new();
            bll_orden = new();
            grillaSolicitudes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaSolicitudes.MultiSelect = false;
            grillaSolicitudes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CargarGrilla();
        }
        private void CargarGrilla()
        {
            grillaSolicitudes.DataSource = null;
            grillaSolicitudes.DataSource = bll_solicitud.ConsultarAprobadasGrilla_064RB();
        }
        private void btnEfectuarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaSolicitudes.Rows.Count == 9) throw new Exception("No hay solicitudes aprobadas en la grilla!");
                var fila = grillaSolicitudes.CurrentRow;
                int idSolicitud = Convert.ToInt32(fila.Cells["Solicitud"].Value);
                string proveedor = fila.Cells["Proveedor"].Value?.ToString() ?? "-";
                decimal monto = Convert.ToDecimal(fila.Cells["MontoTotal"].Value);
                var respuesta = MessageBox.Show($"¿Confirma que efectuó el pedido a {proveedor} y que abonó {monto:C2}?", "Efectuar pedido al proveedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta != DialogResult.Yes) return;

                int numOrden = bll_orden.Efectuar_064RB(idSolicitud);
                MessageBox.Show($"Orden de compra N° {numOrden} registrada.", "Efectuar pedido al proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
