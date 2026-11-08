using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;
using UI_064RB;
using UI_064RB.Negocio_064RB;

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
            CargarMenu();
        }

        private void CargarMenu()
        {
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bool puedeGestionarUsuarios = TieneAlgun(usuario, "Crear Usuario", "Modificar Usuario", "Desbloquear Usuario", "Activar Usuario");
            bool puedeGestionarRoles = TieneAlgun(usuario, "Crear Rol", "Crear Familia", "Eliminar Rol", "Eliminar Familia");
            bool puedeConsultarBitacora = TieneAlgun(usuario, "Consultar Bitácora", "Exportar Bitácora");
            bool puedeGestionarRespaldo = TieneAlgun(usuario, "Realizar Backup", "Realizar Restore");

            // MAESTROS
            bool puedeGestionarProductos = TieneAlgun(usuario, "Crear Producto", "Borrar Producto", "Modificar Producto");
            bool puedeGestionarUnidades = TieneAlgun(usuario, "Crear Unidad Celular", "Borrar Unidad Celular", "Modificar Unidad Celular");
            bool puedeGestionarClientes = TieneAlgun(usuario, "Crear Cliente", "Borrar Cliente", "Modificar Cliente");
            bool puedeGestionarProveedores = TieneAlgun(usuario, "Crear Proveedor", "Borrar Proveedor", "Modificar Proveedor");
            bool puedeAuditarCambios = TieneAlgun(usuario, "Modificar Accesorio Copia");

            // VENTA
            bool puedeRealizarVenta = TieneAlgun(usuario, "Realizar Venta");

            // STOCK
            bool puedeSolicitarReposicion = TieneAlgun(usuario, "Solicitar Reposicion");
            bool puedeCotizarSolicitud = TieneAlgun(usuario, "Cotizar Solicitud");
            bool puedeEvaluarSolicitud = TieneAlgun(usuario, "Evaluar Solicitud");
            bool puedeEfectuarPedido = TieneAlgun(usuario, "Efectuar Pedido al Proveedor");
            bool puedeRecibirMercaderia = TieneAlgun(usuario, "Recibir Mercadería", "Registrar Discrepancia");

            // REPORTES
            bool puedeGenerarReporteVentas = TieneAlgun(usuario, "Generar Reporte de Ventas");
            bool puedeGenerarReporteStock = TieneAlgun(usuario, "Generar Reporte de Stock");

            submenuGestionUsuarios.Visible = puedeGestionarUsuarios;
            submenuGestionRoles.Visible = puedeGestionarRoles;
            submenuBitacora.Visible = puedeConsultarBitacora;
            submenuGestionRespaldo.Visible = puedeGestionarRespaldo;

            submenuProductos.Visible = puedeGestionarProductos;
            submenuUnidadCelular.Visible = puedeGestionarUnidades;
            submenuClientes.Visible = puedeGestionarClientes;
            submenuProveedores.Visible = puedeGestionarProveedores;
            submenuAccesoriosCopia.Visible = puedeAuditarCambios;

            submenuVenta.Visible = puedeRealizarVenta;

            submenuSolicitarReposicion.Visible = puedeSolicitarReposicion;
            submenuCotizarSolicitud.Visible = puedeCotizarSolicitud;
            submenuEvaluarSolicitud.Visible = puedeEvaluarSolicitud;
            submenuEfectuarPedido.Visible = puedeEfectuarPedido;
            submenuRecibirMercaderia.Visible = puedeRecibirMercaderia;

            submenuReporteVentas.Visible = puedeGenerarReporteVentas;
            submenuReporteControlStock.Visible = puedeGenerarReporteStock;

            menuAdmin.Visible =
                puedeGestionarUsuarios ||
                puedeGestionarRoles ||
                puedeConsultarBitacora ||
                puedeGestionarRespaldo;

            menuMaestros.Visible =
                puedeGestionarProductos ||
                puedeGestionarUnidades ||
                puedeGestionarClientes ||
                puedeGestionarProveedores ||
                puedeAuditarCambios;

            menuVenta.Visible = puedeRealizarVenta;

            menuStock.Visible =
                puedeSolicitarReposicion ||
                puedeCotizarSolicitud ||
                puedeEvaluarSolicitud ||
                puedeEfectuarPedido ||
                puedeRecibirMercaderia;

            menuReportes.Visible =
                puedeGenerarReporteVentas ||
                puedeGenerarReporteStock;
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
        private void AbrirOEnfocarFormulario<T>() where T : Form, new()
        {
            Form frm = Application.OpenForms[typeof(T).Name];
            if (frm == null)
            {
                T nuevoForm = new();
                nuevoForm.ShowDialog();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.BringToFront();
                frm.Focus();
            }
        }
        private void submenuGestionUsuarios_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmGestionUsuarios_064RB>();
        private void submenuBitacora_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmBitacora_064RB>();
        private void submenuGestionRoles_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmGestionRoles_064RB>();

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

        private void submenuCambiarClave_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmCambiarClave_064RB>();
        private void submenuCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(bll_idioma.Traducir_064RB("FrmMenu.msgCerrarSesion"), bll_idioma.Traducir_064RB("FrmMenu.msgConfirmacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) this.Close();
        }
        private void submenuProductos_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmProductos_064RB>();
        private void submenuClientes_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmClientes_064RB>();
        private void submenuVenta_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmVenta_064RB>();
        private void submenuUnidadCelular_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmUnidadCelular_064RB>();
        private void submenuProveedores_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmProveedores_064RB>();
        private void submenuSolicitarReposicion_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmSolicitarReposicion_064RB>();
        private void submenuCotizarSolicitud_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmCotizarSolicitud_064RB>();
        private void submenuEvaluarSolicitud_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmEvaluarSolicitud_064RB>();
        private void submenuEfectuarPedido_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmEfectuarPedido_064RB>();
        private void submenuRecibirMercaderia_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmRecibirMercaderia_064RB>();
        private void submenuReporteVentas_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmReporteVentas_064RB>();
        private void submenuReporteControlStock_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmReporteReposicion_064RB>();
        private void accesoriosCopiaToolStripMenuItem_Click(object sender, EventArgs e) => AbrirOEnfocarFormulario<FrmAccesoriosCopia_064RB>();
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
            menuAdmin.Text = bll_idioma.Traducir_064RB("FrmMenu.menuAdmin");
            submenuGestionUsuarios.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuGestionUsuarios");
            submenuBitacora.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuBitacora");
            submenuGestionRoles.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuGestionRoles");
            submenuGestionRespaldo.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuGestionRespaldo");

            menuVenta.Text = bll_idioma.Traducir_064RB("FrmMenu.menuVenta");
            submenuVenta.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuVenta");
            menuStock.Text = bll_idioma.Traducir_064RB("FrmMenu.menuStock");
            submenuSolicitarReposicion.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuSolicitarReposicion");
            submenuCotizarSolicitud.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuCotizarSolicitud");
            submenuEvaluarSolicitud.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuEvaluarSolicitud");
            submenuEfectuarPedido.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuEfectuarPedido");
            submenuRecibirMercaderia.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuRecibirMercaderia");

            menuReportes.Text = bll_idioma.Traducir_064RB("FrmMenu.menuReportes");
            submenuReporteVentas.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuReporteVentas");
            submenuReporteControlStock.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuReporteControlStock");

            menuAyuda.Text = bll_idioma.Traducir_064RB("FrmMenu.menuAyuda");

            menuSesion.Text = bll_idioma.Traducir_064RB("FrmMenu.menuSesion");
            submenuRelogin.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuRelogin");
            submenuCambiarClave.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuCambiarClave");
            submenuCerrarSesion.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuCerrarSesion");

            menuMaestros.Text = bll_idioma.Traducir_064RB("FrmMenu.menuMaestros");
            submenuProductos.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuProductos");
            submenuUnidadCelular.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuUnidadCelular");
            submenuClientes.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuClientes");
            submenuProveedores.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuProveedores");
            submenuAccesoriosCopia.Text = bll_idioma.Traducir_064RB("FrmMenu.submenuAccesoriosCopia");
        }
        private void ActualizarBienvenida()
        {
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            if (usuario != null)
            {
                string saludo = bll_idioma.Traducir_064RB("FrmMenu.lblBienvenida");
                string rolTexto = bll_idioma.Traducir_064RB("FrmMenu.lblRol");
                string ramaTexto = bll_idioma.Traducir_064RB("FrmMenu.lblRama");
                string ultimoCommitTexto = bll_idioma.Traducir_064RB("FrmMenu.lblUltimoCommit");
                string hashCommitTexto = bll_idioma.Traducir_064RB("FrmMenu.lblHashCommit");
                lblBienvenida.Text = $"{saludo} {usuario.Nombre_064RB} {usuario.Apellido_064RB}!";
                lblRol.Text = $"{rolTexto}: {usuario.Rol_064RB.ToString()}";
                lblRama.Text = $"{ramaTexto}: {GitInfo_064RB.ObtenerRama_064RB()}";
                lblUltimoCommit.Text = $"{ultimoCommitTexto}: {GitInfo_064RB.ObtenerFechaCommit_064RB()}";
                lblHashCommit.Text = $"{hashCommitTexto} {GitInfo_064RB.ObtenerHashCommit_064RB()}";
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_064RB(this);
            base.OnFormClosed(e);
        }
    }
}
