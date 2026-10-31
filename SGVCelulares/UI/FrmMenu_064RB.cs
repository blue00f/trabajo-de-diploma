using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;
using UI_064RB;

namespace UI
{
    public partial class FrmMenu_064RB : Form, IObservadorIdioma_064RB
    {
        bool esRelogin = false;
        BLL_Usuario_064RB bll_usuario;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        bool cargandoIdiomas;
        public FrmMenu_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            CargarIdiomas();
            ActualizarIdioma_064RB();
        }
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            bll_usuario = new BLL_Usuario_064RB();
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;

            submenuGestionUsuarios.Visible = TieneAlgun(usuario, "Crear Usuario", "Modificar Usuario", "Desbloquear Usuario", "Activar Usuario");
            submenuGestionRoles.Visible = TieneAlgun(usuario, "Crear Rol", "Crear Familia", "Eliminar Rol", "Eliminar Familia");
            submenuBitacora.Visible = TieneAlgun(usuario, "Consultar Bitácora");
            submenuGestionRespaldo.Visible = TieneAlgun(usuario, "Realizar Backup", "Realizar Restore");
            submenuProductos.Visible = TieneAlgun(usuario, "Crear Producto", "Borrar Producto", "Modificar Producto");
            submenuUnidadCelular.Visible = TieneAlgun(usuario, "Crear Unidad Celular", "Borrar Unidad Celular", "Modificar Unidad Celular");
            submenuClientes.Visible = TieneAlgun(usuario, "Crear Cliente", "Borrar Cliente", "Modificar Cliente");
            submenuVenta.Visible = TieneAlgun(usuario, "Realizar Venta");

            submenuProveedores.Visible = TieneAlgun(usuario, "Crear Proveedor", "Borrar Proveedor", "Modificar Proveedor");
            submenuSolicitarReposicion.Visible = TieneAlgun(usuario, "Solicitar Reposicion");
            submenuCotizarSolicitud.Visible = TieneAlgun(usuario, "Cotizar Solicitud");
            submenuEvaluarSolicitud.Visible = TieneAlgun(usuario, "Evaluar Solicitud");
            submenuEfectuarPedido.Visible = TieneAlgun(usuario, "Efectuar Pedido al Proveedor");
            submenuRecibirMercaderia.Visible = TieneAlgun(usuario, "Recibir Mercadería");
            menuAdmin.Visible = submenuGestionUsuarios.Visible || submenuGestionRoles.Visible || submenuBitacora.Visible || submenuGestionRespaldo.Visible || submenuProductos.Visible || submenuUnidadCelular.Visible || submenuClientes.Visible ||
                submenuVenta.Visible || submenuProveedores.Visible || submenuSolicitarReposicion.Visible || submenuCotizarSolicitud.Visible || submenuEvaluarSolicitud.Visible || submenuEfectuarPedido.Visible || submenuRecibirMercaderia.Visible;
            menuAdmin.Visible = true;

            try
            {
                lblRama.Text = $"Rama actual: {GitInfo_064RB.ObtenerRama_064RB()}";
                lblUltimoCommit.Text = $"Último commit: {GitInfo_064RB.ObtenerFechaCommit_064RB()}";
                lblHashCommit.Text = $"Hash del último commit: {GitInfo_064RB.ObtenerHashCommit_064RB()}";
            }
            catch (Exception)
            {
                lblRama.Text = "Rama actual: no disponible";
                lblUltimoCommit.Text = "Último commit: no disponible";
                lblHashCommit.Text = "Hash del último commit: no disponible";
            }
        }
        private bool TieneAlgun(SER_Usuario_064RB usuario, params string[] permisos)
        {
            foreach (string permiso in permisos)
            {
                if (usuario.Rol_064RB.TienePermiso(permiso))
                    return true;
            }
            return false;
        }

        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!esRelogin) bll_usuario.Logout_064RB();
        }
        private void submenuGestionUsuarios_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmGestionUsuarios_064RB"];
            if (frm == null)
            {
                FrmGestionUsuarios_064RB frmUsuarios = new FrmGestionUsuarios_064RB();
                frmUsuarios.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }

        private void submenuBitacora_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmBitacora_064RB"];
            if (frm == null)
            {
                FrmBitacora_064RB frmBitacora = new FrmBitacora_064RB();
                frmBitacora.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }

        private void submenuGestionRoles_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmGestionRoles_064RB"];
            if (frm == null)
            {
                FrmGestionRoles_064RB frmRoles = new FrmGestionRoles_064RB();
                frmRoles.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }

        private void submenuGestionRespaldo_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmBackup_064RB"];
            if (frm == null)
            {
                FrmBackup_064RB frmBackup = new FrmBackup_064RB();
                frmBackup.ShowDialog();
                if (frmBackup.RestoreRealizado)
                {
                    bll_usuario.Logout_064RB();
                    FrmLogin_064RB login = new FrmLogin_064RB();
                    this.Close();
                }
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }

        private void submenuRelogin_Click(object sender, EventArgs e)
        {
            string nombreUsuarioActual = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.NombreUsuario_064RB;
            (this.Owner as FrmLogin_064RB)?.RestringirProximoLoginA(nombreUsuarioActual);
            esRelogin = true;
            this.Close();
        }

        private void submenuCambiarClave_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmCambiarClave_064RB"];
            if (frm == null)
            {
                FrmCambiarClave_064RB frmCambiarClave = new FrmCambiarClave_064RB();
                frmCambiarClave.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(bll_idioma.Traducir_064RB("FrmMenu.msgCerrarSesion"), bll_idioma.Traducir_064RB("FrmMenu.msgConfirmacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) this.Close();
        }
        private void submenuProductos_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmProductos_064RB"];
            if (frm == null)
            {
                FrmProductos_064RB frmProductos = new();
                frmProductos.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuClientes_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmClientes_064RB"];
            if (frm == null)
            {
                FrmClientes_064RB frmClientes = new();
                frmClientes.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuVenta_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmVenta_064RB"];
            if (frm == null)
            {
                FrmVenta_064RB frmVenta = new();
                frmVenta.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuUnidadCelular_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmUnidadCelular_064RB"];
            if (frm == null)
            {
                FrmUnidadCelular_064RB frmUnidadCelular = new();
                frmUnidadCelular.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuProveedores_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmProveedores_064RB"];
            if (frm == null)
            {
                FrmProveedores_064RB frmProveedores_064RB = new();
                frmProveedores_064RB.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuSolicitarReposicion_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmSolicitarReposicion_064RB"];
            if (frm == null)
            {
                FrmSolicitarReposicion_064RB frmSolicitarReposicion_064RB = new();
                frmSolicitarReposicion_064RB.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuCotizarSolicitud_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmCotizarSolicitud_064RB"];
            if (frm == null)
            {
                FrmCotizarSolicitud_064RB frmCotizarSolicitud_064RB = new();
                frmCotizarSolicitud_064RB.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuEvaluarSolicitud_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmEvaluarSolicitud_064RB"];
            if (frm == null)
            {
                FrmEvaluarSolicitud_064RB frmEvaluarSolicitud_064RB = new();
                frmEvaluarSolicitud_064RB.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuEfectuarPedido_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmEfectuarPedido_064RB"];
            if (frm == null)
            {
                FrmEfectuarPedido_064RB frmEfectuarPedido_064RB = new();
                frmEfectuarPedido_064RB.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuRecibirMercaderia_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms["FrmRecibirMercaderia_064RB"];
            if (frm == null)
            {
                FrmRecibirMercaderia_064RB frmRecibirMercaderia_064RB = new();
                frmRecibirMercaderia_064RB.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void CargarIdiomas()
        {
            cargandoIdiomas = true;
            cboIdioma.DisplayMember = "Nombre_064RB";
            cboIdioma.ValueMember = "Codigo_064RB";
            cboIdioma.DataSource = bll_idioma.ObtenerIdiomasDisponibles_064RB();
            cboIdioma.SelectedValue = bll_idioma.IdiomaActual;
            cargandoIdiomas = false;
        }

        private void cboIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoIdiomas) return;
            try
            {
                bll_idioma.CambiarIdioma_064RB(cboIdioma.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmMenu.Form");
            lblTituloTopbar.Text = bll_idioma.Traducir_064RB("FrmMenu.lblTituloTopbar");
            ActualizarBienvenida();
            submenuGestionUsuarios.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuGestionUsuarios");
            submenuBitacora.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuBitacora");
            submenuGestionRoles.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuGestionRoles");
            submenuGestionRespaldo.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuGestionRespaldo");

            menuReportes.Text = bll_idioma.Traducir_064RB("FrmMenu.menuReportes");

            menuAyuda.Text = bll_idioma.Traducir_064RB("FrmMenu.menuAyuda");

            menuSesion.Text = bll_idioma.Traducir_064RB("FrmMenu.menuSesion");
            submenuRelogin.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuRelogin");
            submenuCambiarClave.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuCambiarClave");
            submenuCerrarSesion.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuCerrarSesion");

            menuMaestros.Text = bll_idioma.Traducir_064RB("FrmMenu.menuMaestros");
            submenuProductos.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuProductos");
            submenuUnidadCelular.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuUnidadCelular");
            submenuClientes.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuClientes");
        }
        private void ActualizarBienvenida()
        {
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            if (usuario != null)
            {
                string saludo = bll_idioma.Traducir_064RB("FrmMenu.lblBienvenida");
                lblBienvenida.Text = $"{saludo} {usuario.Nombre_064RB} {usuario.Apellido_064RB}!";
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_064RB(this);
            base.OnFormClosed(e);
        }
    }
}
