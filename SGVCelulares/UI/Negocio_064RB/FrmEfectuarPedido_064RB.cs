using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI_064RB
{
    public partial class FrmEfectuarPedido_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_SolicitudCompra_064RB bll_solicitud;
        BLL_OrdenCompra_064RB bll_orden;
        BLL_Idioma_064RB bll_idioma = new();
        public FrmEfectuarPedido_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
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
            TraducirColumnas(grillaSolicitudes);
        }
        private void btnEfectuarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaSolicitudes.Rows.Count == 9) throw new Exception("No hay solicitudes aprobadas en la grilla!");
                var fila = grillaSolicitudes.CurrentRow;
                int idSolicitud = Convert.ToInt32(fila.Cells[0].Value);
                string proveedor = fila.Cells[5].Value?.ToString() ?? "-";
                decimal monto = Convert.ToDecimal(fila.Cells[8].Value);
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

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmEfectuarPedido.Form");
            lblSolicitudes.Text = bll_idioma.Traducir_064RB("FrmEfectuarPedido.lblSolicitudes");
            btnEfectuarPedido.Text = bll_idioma.Traducir_064RB("FrmEfectuarPedido.btnEfectuarPedido");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmEfectuarPedido.btnSalir");
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns[0].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colSolicitud");
            grilla.Columns[1].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colSku");
            grilla.Columns[2].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colMarca");
            grilla.Columns[3].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colModelo");
            grilla.Columns[4].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colCantidad");
            grilla.Columns[5].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colProveedor");
            grilla.Columns[6].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colCuit");
            grilla.Columns[7].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colPrecioUnitario");
            grilla.Columns[8].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colMontoTotal");
            grilla.Columns[9].HeaderText = bll_idioma.Traducir_064RB("FrmEfectuarPedido.colAprobada");
        }
    }
}
