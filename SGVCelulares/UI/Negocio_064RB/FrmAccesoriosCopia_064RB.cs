using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI_064RB.Negocio_064RB
{
    public partial class FrmAccesoriosCopia_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Accesorio_C_064RB bll_accesorio_c;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        public FrmAccesoriosCopia_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
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
            TraducirColumnas(grillaAccesorios);
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

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.Form");
            lblAuditoriaCambios.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblAuditoriaCambios");
            lblSku.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblSku");
            lblMarca.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblMarca");
            lblModelo.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblModelo");
            lblFechaInicio.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblFechaInicio");
            lblFechaFin.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblFechaFin");
            btnAplicar.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.btnAplicar");
            btnActivar.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.btnActivar");
            btnLimpiar.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.btnLimpiar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.btnSalir");
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns[0].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colId");
            grilla.Columns[1].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblSku");
            grilla.Columns[2].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblMarca");
            grilla.Columns[3].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.lblModelo");
            grilla.Columns[4].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colPrecio");
            grilla.Columns[5].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colDescripcion");
            grilla.Columns[6].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colStockMinimo");
            grilla.Columns[7].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colCategoria");
            grilla.Columns[8].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colStock");
            grilla.Columns[9].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colEstadoProducto");
            grilla.Columns[10].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colFecha");
            grilla.Columns[11].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colHora");
            grilla.Columns[12].HeaderText = bll_idioma.Traducir_064RB("FrmAccesoriosCopia.colActivo");
        }
    }
}
