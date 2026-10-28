namespace UI
{
    partial class FrmMenu_064RB
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlTopbar = new Panel();
            lblBienvenida = new Label();
            cboIdioma = new ComboBox();
            lblTituloTopbar = new Label();
            lblUsuario = new Label();
            menuStrip1 = new MenuStrip();
            menuAdmin = new ToolStripMenuItem();
            submenuGestionUsuarios = new ToolStripMenuItem();
            submenuBitacora = new ToolStripMenuItem();
            submenuGestionRoles = new ToolStripMenuItem();
            submenuGestionRespaldo = new ToolStripMenuItem();
            menuVendedor = new ToolStripMenuItem();
            submenuVenta = new ToolStripMenuItem();
            menuStock = new ToolStripMenuItem();
            submenuSolicitarReposicion = new ToolStripMenuItem();
            submenuCotizarSolicitud = new ToolStripMenuItem();
            menuReportes = new ToolStripMenuItem();
            menuAyuda = new ToolStripMenuItem();
            menuSesion = new ToolStripMenuItem();
            submenuRelogin = new ToolStripMenuItem();
            submenuCambiarClave = new ToolStripMenuItem();
            submenuCerrarSesion = new ToolStripMenuItem();
            menuMaestros = new ToolStripMenuItem();
            submenuProductos = new ToolStripMenuItem();
            submenuUnidadCelular = new ToolStripMenuItem();
            submenuClientes = new ToolStripMenuItem();
            submenuProveedores = new ToolStripMenuItem();
            pnlSidebar = new Panel();
            pnlContenido = new Panel();
            lblHashCommit = new Label();
            lblUltimoCommit = new Label();
            lblRama = new Label();
            submenuEvaluarSolicitud = new ToolStripMenuItem();
            pnlTopbar.SuspendLayout();
            menuStrip1.SuspendLayout();
            pnlContenido.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblBienvenida);
            pnlTopbar.Controls.Add(cboIdioma);
            pnlTopbar.Controls.Add(lblTituloTopbar);
            pnlTopbar.Controls.Add(lblUsuario);
            pnlTopbar.Controls.Add(menuStrip1);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(0, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(960, 81);
            pnlTopbar.TabIndex = 0;
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 11F);
            lblBienvenida.ForeColor = Color.FromArgb(226, 232, 240);
            lblBienvenida.Location = new Point(193, 44);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(83, 20);
            lblBienvenida.TabIndex = 3;
            lblBienvenida.Text = "Bienvenido";
            // 
            // cboIdioma
            // 
            cboIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIdioma.FormattingEnabled = true;
            cboIdioma.Location = new Point(735, 38);
            cboIdioma.Name = "cboIdioma";
            cboIdioma.Size = new Size(213, 25);
            cboIdioma.TabIndex = 2;
            cboIdioma.SelectedIndexChanged += cboIdioma_SelectedIndexChanged;
            // 
            // lblTituloTopbar
            // 
            lblTituloTopbar.AutoSize = true;
            lblTituloTopbar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloTopbar.ForeColor = Color.FromArgb(226, 232, 240);
            lblTituloTopbar.Location = new Point(12, 43);
            lblTituloTopbar.Name = "lblTituloTopbar";
            lblTituloTopbar.Size = new Size(155, 21);
            lblTituloTopbar.TabIndex = 0;
            lblTituloTopbar.Text = "Sistema de Gestión";
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 9F);
            lblUsuario.ForeColor = Color.FromArgb(148, 163, 184);
            lblUsuario.Location = new Point(1600, 16);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(53, 15);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "● Admin";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuAdmin, menuVendedor, menuStock, menuReportes, menuAyuda, menuSesion, menuMaestros });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(960, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuAdmin
            // 
            menuAdmin.DropDownItems.AddRange(new ToolStripItem[] { submenuGestionUsuarios, submenuBitacora, submenuGestionRoles, submenuGestionRespaldo });
            menuAdmin.Name = "menuAdmin";
            menuAdmin.Size = new Size(55, 20);
            menuAdmin.Text = "Admin";
            // 
            // submenuGestionUsuarios
            // 
            submenuGestionUsuarios.Name = "submenuGestionUsuarios";
            submenuGestionUsuarios.Size = new Size(162, 22);
            submenuGestionUsuarios.Text = "Gestión usuarios";
            submenuGestionUsuarios.Click += submenuGestionUsuarios_Click;
            // 
            // submenuBitacora
            // 
            submenuBitacora.Name = "submenuBitacora";
            submenuBitacora.Size = new Size(162, 22);
            submenuBitacora.Text = "Bitácora";
            submenuBitacora.Click += submenuBitacora_Click;
            // 
            // submenuGestionRoles
            // 
            submenuGestionRoles.Name = "submenuGestionRoles";
            submenuGestionRoles.Size = new Size(162, 22);
            submenuGestionRoles.Text = "Gestión roles";
            submenuGestionRoles.Click += submenuGestionRoles_Click;
            // 
            // submenuGestionRespaldo
            // 
            submenuGestionRespaldo.Name = "submenuGestionRespaldo";
            submenuGestionRespaldo.Size = new Size(162, 22);
            submenuGestionRespaldo.Text = "Gestión respaldo";
            submenuGestionRespaldo.Click += submenuGestionRespaldo_Click;
            // 
            // menuVendedor
            // 
            menuVendedor.DropDownItems.AddRange(new ToolStripItem[] { submenuVenta });
            menuVendedor.Name = "menuVendedor";
            menuVendedor.Size = new Size(69, 20);
            menuVendedor.Text = "Vendedor";
            // 
            // submenuVenta
            // 
            submenuVenta.Name = "submenuVenta";
            submenuVenta.Size = new Size(146, 22);
            submenuVenta.Text = "Realizar venta";
            submenuVenta.Click += submenuVenta_Click;
            // 
            // menuStock
            // 
            menuStock.DropDownItems.AddRange(new ToolStripItem[] { submenuSolicitarReposicion, submenuCotizarSolicitud, submenuEvaluarSolicitud });
            menuStock.Name = "menuStock";
            menuStock.Size = new Size(48, 20);
            menuStock.Text = "Stock";
            // 
            // submenuSolicitarReposicion
            // 
            submenuSolicitarReposicion.Name = "submenuSolicitarReposicion";
            submenuSolicitarReposicion.Size = new Size(180, 22);
            submenuSolicitarReposicion.Text = "Solicitar reposición";
            submenuSolicitarReposicion.Click += submenuSolicitarReposicion_Click;
            // 
            // submenuCotizarSolicitud
            // 
            submenuCotizarSolicitud.Name = "submenuCotizarSolicitud";
            submenuCotizarSolicitud.Size = new Size(180, 22);
            submenuCotizarSolicitud.Text = "Cotizar solicitud";
            submenuCotizarSolicitud.Click += submenuCotizarSolicitud_Click;
            // 
            // menuReportes
            // 
            menuReportes.Name = "menuReportes";
            menuReportes.Size = new Size(65, 20);
            menuReportes.Text = "Reportes";
            // 
            // menuAyuda
            // 
            menuAyuda.Name = "menuAyuda";
            menuAyuda.Size = new Size(53, 20);
            menuAyuda.Text = "Ayuda";
            // 
            // menuSesion
            // 
            menuSesion.DropDownItems.AddRange(new ToolStripItem[] { submenuRelogin, submenuCambiarClave, submenuCerrarSesion });
            menuSesion.Name = "menuSesion";
            menuSesion.Size = new Size(53, 20);
            menuSesion.Text = "Sesión";
            // 
            // submenuRelogin
            // 
            submenuRelogin.Name = "submenuRelogin";
            submenuRelogin.Size = new Size(180, 22);
            submenuRelogin.Text = "Re-login";
            submenuRelogin.Click += submenuRelogin_Click;
            // 
            // submenuCambiarClave
            // 
            submenuCambiarClave.Name = "submenuCambiarClave";
            submenuCambiarClave.Size = new Size(180, 22);
            submenuCambiarClave.Text = "Cambiar clave";
            submenuCambiarClave.Click += submenuCambiarClave_Click;
            // 
            // submenuCerrarSesion
            // 
            submenuCerrarSesion.Name = "submenuCerrarSesion";
            submenuCerrarSesion.Size = new Size(180, 22);
            submenuCerrarSesion.Text = "Cerrar sesión";
            submenuCerrarSesion.Click += submenuCerrarSesion_Click;
            // 
            // menuMaestros
            // 
            menuMaestros.DropDownItems.AddRange(new ToolStripItem[] { submenuProductos, submenuUnidadCelular, submenuClientes, submenuProveedores });
            menuMaestros.Name = "menuMaestros";
            menuMaestros.Size = new Size(67, 20);
            menuMaestros.Text = "Maestros";
            // 
            // submenuProductos
            // 
            submenuProductos.Name = "submenuProductos";
            submenuProductos.Size = new Size(177, 22);
            submenuProductos.Text = "Productos";
            submenuProductos.Click += submenuProductos_Click;
            // 
            // submenuUnidadCelular
            // 
            submenuUnidadCelular.Name = "submenuUnidadCelular";
            submenuUnidadCelular.Size = new Size(177, 22);
            submenuUnidadCelular.Text = "Unidades de celular";
            submenuUnidadCelular.Click += submenuUnidadCelular_Click;
            // 
            // submenuClientes
            // 
            submenuClientes.Name = "submenuClientes";
            submenuClientes.Size = new Size(177, 22);
            submenuClientes.Text = "Clientes";
            submenuClientes.Click += submenuClientes_Click;
            // 
            // submenuProveedores
            // 
            submenuProveedores.Name = "submenuProveedores";
            submenuProveedores.Size = new Size(177, 22);
            submenuProveedores.Text = "Proveedores";
            submenuProveedores.Click += submenuProveedores_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(25, 37, 53);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 81);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(193, 390);
            pnlSidebar.TabIndex = 1;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Controls.Add(lblHashCommit);
            pnlContenido.Controls.Add(lblUltimoCommit);
            pnlContenido.Controls.Add(lblRama);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(193, 81);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(24);
            pnlContenido.Size = new Size(767, 390);
            pnlContenido.TabIndex = 2;
            // 
            // lblHashCommit
            // 
            lblHashCommit.AutoSize = true;
            lblHashCommit.Font = new Font("Segoe UI", 11F);
            lblHashCommit.ForeColor = Color.FromArgb(226, 232, 240);
            lblHashCommit.Location = new Point(6, 354);
            lblHashCommit.Name = "lblHashCommit";
            lblHashCommit.Size = new Size(172, 20);
            lblHashCommit.TabIndex = 7;
            lblHashCommit.Text = "Hash del último commit:";
            // 
            // lblUltimoCommit
            // 
            lblUltimoCommit.AutoSize = true;
            lblUltimoCommit.Font = new Font("Segoe UI", 11F);
            lblUltimoCommit.ForeColor = Color.FromArgb(226, 232, 240);
            lblUltimoCommit.Location = new Point(6, 322);
            lblUltimoCommit.Name = "lblUltimoCommit";
            lblUltimoCommit.Size = new Size(112, 20);
            lblUltimoCommit.TabIndex = 6;
            lblUltimoCommit.Text = "Último commit:";
            // 
            // lblRama
            // 
            lblRama.AutoSize = true;
            lblRama.Font = new Font("Segoe UI", 11F);
            lblRama.ForeColor = Color.FromArgb(226, 232, 240);
            lblRama.Location = new Point(6, 288);
            lblRama.Name = "lblRama";
            lblRama.Size = new Size(94, 20);
            lblRama.TabIndex = 5;
            lblRama.Text = "Rama actual:";
            // 
            // submenuEvaluarSolicitud
            // 
            submenuEvaluarSolicitud.Name = "submenuEvaluarSolicitud";
            submenuEvaluarSolicitud.Size = new Size(180, 22);
            submenuEvaluarSolicitud.Text = "Evaluar solicitud";
            submenuEvaluarSolicitud.Click += submenuEvaluarSolicitud_Click;
            // 
            // FrmMenu_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(960, 471);
            Controls.Add(pnlContenido);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(800, 500);
            Name = "FrmMenu_064RB";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión";
            FormClosing += FrmMenu_FormClosing;
            Load += FrmMenu_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopbar;
        private System.Windows.Forms.Label lblTituloTopbar;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlContenido;
        private Label label1;
        private ComboBox cboIdioma;
        private Label lblBienvenida;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem menuAdmin;
        private ToolStripMenuItem menuVendedor;
        private ToolStripMenuItem menuSesion;
        private ToolStripMenuItem submenuGestionUsuarios;
        private ToolStripMenuItem submenuBitacora;
        private ToolStripMenuItem submenuGestionRoles;
        private ToolStripMenuItem submenuGestionRespaldo;
        private ToolStripMenuItem submenuRelogin;
        private ToolStripMenuItem submenuCambiarClave;
        private ToolStripMenuItem submenuCerrarSesion;
        private ToolStripMenuItem menuReportes;
        private ToolStripMenuItem menuAyuda;
        private ToolStripMenuItem menuMaestros;
        private ToolStripMenuItem submenuProductos;
        private ToolStripMenuItem submenuUnidadCelular;
        private ToolStripMenuItem submenuClientes;
        private ToolStripMenuItem submenuVenta;
        private Label lblUltimoCommit;
        private Label lblRama;
        private Label lblHashCommit;
        private ToolStripMenuItem submenuProveedores;
        private ToolStripMenuItem menuStock;
        private ToolStripMenuItem submenuSolicitarReposicion;
        private ToolStripMenuItem submenuCotizarSolicitud;
        private ToolStripMenuItem submenuEvaluarSolicitud;
    }
}