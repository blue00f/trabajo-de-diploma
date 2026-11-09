using BE_064RB;
using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI_064RB
{
    public partial class FrmProveedores_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Proveedor_064RB bll_proveedores;
        BLL_Idioma_064RB bll_idioma = new();
        public FrmProveedores_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmProveedores_064RB_Load(object sender, EventArgs e)
        {
            grillaProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaProveedores.MultiSelect = false;
            grillaProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            btnAgregar.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Crear Proveedor");
            btnBorrar.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Borrar Proveedor");
            btnModificar.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Modificar Proveedor");

            bll_proveedores = new();
            CargarListado();
        }
        private void CargarListado()
        {
            grillaProveedores.DataSource = null;
            grillaProveedores.DataSource = bll_proveedores.ConsultarGrilla_064RB();
            TraducirColumnas(grillaProveedores);
        }
        private void Limpiar()
        {
            txtCuit.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCuit.Text.Length != 11) throw new Exception("El formato del CUIT debe ser de 11 números sin guiones!");
                if (txtRazonSocial.Text.Length == 0) throw new Exception("La razón social no puede estar vacía!");
                BE_Proveedor_064RB proveedor = new(txtCuit.Text, txtRazonSocial.Text);

                bll_proveedores.Agregar_064RB(proveedor);
                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaProveedores.Rows.Count == 0) throw new Exception("No hay proveedores para borrar");
                string cuit = grillaProveedores.CurrentRow.Cells[0].Value.ToString();
                BE_Proveedor_064RB proveedor = new(cuit);
                bll_proveedores.Borrar_064RB(proveedor);
                CargarListado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaProveedores.Rows.Count == 0) throw new Exception("No hay proveedores para borrar");
                if (txtRazonSocial.Text.Length == 0) throw new Exception("La razón social no puede estar vacía!");
                BE_Proveedor_064RB proveedor = new(grillaProveedores.CurrentRow.Cells[0].Value.ToString(), txtRazonSocial.Text);
                bll_proveedores.Modificar_064RB(proveedor);
                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmProveedores.Form");
            lblProveedores.Text = bll_idioma.Traducir_064RB("FrmProveedores.lblProveedores");
            btnAgregar.Text = bll_idioma.Traducir_064RB("FrmProveedores.btnAgregar");
            btnBorrar.Text = bll_idioma.Traducir_064RB("FrmProveedores.btnBorrar");
            btnModificar.Text = bll_idioma.Traducir_064RB("FrmProveedores.btnModificar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmProveedores.btnSalir");
            lblCuit.Text = bll_idioma.Traducir_064RB("FrmProveedores.lblCuit");
            lblRazonSocial.Text = bll_idioma.Traducir_064RB("FrmProveedores.lblRazonSocial");
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns[0].HeaderText = bll_idioma.Traducir_064RB("FrmProveedores.colCuit");
            grilla.Columns[1].HeaderText = bll_idioma.Traducir_064RB("FrmProveedores.colRazonSocial");
        }
    }
}
