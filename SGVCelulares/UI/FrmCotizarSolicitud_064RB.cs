using BLL_064RB.Negocio_064RB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_064RB
{
    public partial class FrmCotizarSolicitud_064RB : Form
    {
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_Proveedor_064RB bll_proveedor;
        public FrmCotizarSolicitud_064RB()
        {
            InitializeComponent();
        }

        private void FrmCotizarSolicitud_064RB_Load(object sender, EventArgs e)
        {
            bll_solicitud = new();
            bll_proveedor = new();
            foreach (var control in Controls)
            {
                if (control is DataGridView grilla)
                {
                    grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grilla.MultiSelect = false;
                    grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            Mostrar(grillaSolicitudes, bll_solicitud.ConsultarPendientesDeCompraGrilla_064RB());
            Mostrar(grillaProveedores, bll_proveedor.ConsultarGrilla_064RB());
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            txtPrecioUnitario.Clear();
        }
        private void btnCotizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaSolicitudes.CurrentRow == null) throw new Exception("Seleccione una solicitud del listado.");
                if (grillaProveedores.CurrentRow == null) throw new Exception("Seleccione un proveedor.");
                if (!decimal.TryParse(txtPrecioUnitario.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out decimal precio)) throw new Exception("Ingrese un precio unitario válido, con coma para los decimales (por ejemplo 1500,50).");

                int idSolicitud = Convert.ToInt32(grillaSolicitudes.CurrentRow.Cells["Solicitud"].Value);
                string cuit = grillaProveedores.CurrentRow.Cells[0].Value.ToString()!;

                bll_solicitud.Cotizar_064RB(idSolicitud, cuit, precio);

                MessageBox.Show("Cotización registrada.", "Cotizar solicitud de compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mostrar(grillaSolicitudes, bll_solicitud.ConsultarPendientesDeCompraGrilla_064RB());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
