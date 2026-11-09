using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Globalization;

namespace UI_064RB
{
    public partial class FrmCotizarSolicitud_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_Proveedor_064RB bll_proveedor;
        BLL_Idioma_064RB bll_idioma = new();
        public FrmCotizarSolicitud_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
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
            TraducirColumnasSolicitudes(grillaSolicitudes);
            TraducirColumnasProveedores(grillaProveedores);
        }
        private void btnCotizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaSolicitudes.CurrentRow == null) throw new Exception("Seleccione una solicitud del listado.");
                if (grillaProveedores.CurrentRow == null) throw new Exception("Seleccione un proveedor.");
                if (!decimal.TryParse(txtPrecioUnitario.Text, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out decimal precio)) throw new Exception("Ingrese un precio unitario válido, con coma para los decimales (por ejemplo 1500,50).");

                int idSolicitud = Convert.ToInt32(grillaSolicitudes.CurrentRow.Cells[0].Value);
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

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.Form");
            lblSolicitudesReposicion.Text = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.lblSolicitudesReposicion");
            lblProveedoresRegistrados.Text = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.lblProveedoresRegistrados");
            btnCotizar.Text = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.btnCotizar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.btnSalir");
            lblPrecioUnitario.Text = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.lblPrecioUnitario");
        }
        private void TraducirColumnasSolicitudes(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns[0].HeaderText = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.colSolicitud");
            grilla.Columns[1].HeaderText = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.colSku");
            grilla.Columns[2].HeaderText = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.colMarca");
            grilla.Columns[3].HeaderText = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.colModelo");
            grilla.Columns[4].HeaderText = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.colCantidad");
            grilla.Columns[5].HeaderText = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.colFecha");
        }
        private void TraducirColumnasProveedores(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns[0].HeaderText = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.colCuit");
            grilla.Columns[1].HeaderText = bll_idioma.Traducir_064RB("FrmCotizarSolicitud.colRazonSocial");
        }
    }
}
