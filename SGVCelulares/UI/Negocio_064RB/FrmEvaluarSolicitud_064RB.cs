using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI_064RB
{
    public partial class FrmEvaluarSolicitud_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_Idioma_064RB bll_idioma = new();
        public FrmEvaluarSolicitud_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
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
            TraducirColumnas(grillaSolicitudes);
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

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.Form");
            lblSolicitudes.Text = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.lblSolicitudes");
            btnAprobar.Text = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.btnAprobar");
            btnRechazar.Text = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.btnRechazar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.btnSalir");
            lblMotivo.Text = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.lblMotivo");
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns[0].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colSolicitud");
            grilla.Columns[1].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colSku");
            grilla.Columns[2].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colMarca");
            grilla.Columns[3].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colModelo");
            grilla.Columns[4].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colCantidad");
            grilla.Columns[5].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colProveedor");
            grilla.Columns[6].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colCuit");
            grilla.Columns[7].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colPrecioUnitario");
            grilla.Columns[8].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colMontoTotal");
            grilla.Columns[9].HeaderText = bll_idioma.Traducir_064RB("FrmEvaluarSolicitud.colFecha");
        }
    }
}
