using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI_064RB
{
    public partial class FrmSolicitarReposicion_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_Producto_064RB bll_producto;
        BLL_Idioma_064RB bll_idioma = new();
        public FrmSolicitarReposicion_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
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
            ActualizarIdioma_064RB();
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

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.Form");
            lblReposicion.Text = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.lblReposicion");
            btnSolicitar.Text = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.btnSolicitar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.btnSalir");
            lblCantidad.Text = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.lblCantidad");
            TraducirColumnas(grillaProductosAReponer);
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns["SKU"].HeaderText = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.colSku");
            grilla.Columns["Marca"].HeaderText = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.colMarca");
            grilla.Columns["Modelo"].HeaderText = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.colModelo");
            grilla.Columns["Tipo"].HeaderText = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.colTipo");
            grilla.Columns["StockActual"].HeaderText = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.colStockActual");
            grilla.Columns["StockMinimo"].HeaderText = bll_idioma.Traducir_064RB("FrmSolicitarReposicion.colStockMinimo");
        }
    }
}
