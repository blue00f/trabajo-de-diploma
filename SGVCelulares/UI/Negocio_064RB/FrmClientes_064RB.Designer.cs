namespace UI_064RB
{
    partial class FrmClientes_064RB
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
            btnSalir = new Button();
            lblClientes = new Label();
            btnModificar = new Button();
            btnBorrar = new Button();
            grillaClientes = new DataGridView();
            btnAgregar = new Button();
            lblCorreo = new Label();
            txtCorreo = new TextBox();
            lblDni = new Label();
            txtDni = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            btnAplicar = new Button();
            gbFiltros = new GroupBox();
            btnSerializar = new Button();
            btnDeserializar = new Button();
            btnActualizar = new Button();
            ((System.ComponentModel.ISupportInitialize)grillaClientes).BeginInit();
            gbFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(356, 324);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 35);
            btnSalir.TabIndex = 11;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblClientes
            // 
            lblClientes.AutoSize = true;
            lblClientes.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClientes.ForeColor = SystemColors.Menu;
            lblClientes.Location = new Point(15, 18);
            lblClientes.Name = "lblClientes";
            lblClientes.Size = new Size(90, 30);
            lblClientes.TabIndex = 10;
            lblClientes.Text = "Clientes";
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(45, 60, 82);
            btnModificar.FlatStyle = FlatStyle.Popup;
            btnModificar.ForeColor = SystemColors.Menu;
            btnModificar.Location = new Point(217, 324);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(95, 35);
            btnModificar.TabIndex = 9;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.BackColor = Color.FromArgb(45, 60, 82);
            btnBorrar.FlatStyle = FlatStyle.Popup;
            btnBorrar.ForeColor = SystemColors.Menu;
            btnBorrar.Location = new Point(116, 324);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(95, 35);
            btnBorrar.TabIndex = 8;
            btnBorrar.Text = "Borrar";
            btnBorrar.UseVisualStyleBackColor = false;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // grillaClientes
            // 
            grillaClientes.AllowUserToAddRows = false;
            grillaClientes.AllowUserToDeleteRows = false;
            grillaClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaClientes.Location = new Point(12, 51);
            grillaClientes.Name = "grillaClientes";
            grillaClientes.ReadOnly = true;
            grillaClientes.Size = new Size(768, 267);
            grillaClientes.TabIndex = 7;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(45, 60, 82);
            btnAgregar.FlatStyle = FlatStyle.Popup;
            btnAgregar.ForeColor = SystemColors.Menu;
            btnAgregar.Location = new Point(15, 324);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(95, 35);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Segoe UI", 9F);
            lblCorreo.ForeColor = SystemColors.Menu;
            lblCorreo.Location = new Point(217, 37);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(43, 15);
            lblCorreo.TabIndex = 45;
            lblCorreo.Text = "Correo";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(217, 55);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(151, 23);
            txtCorreo.TabIndex = 44;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Font = new Font("Segoe UI", 9F);
            lblDni.ForeColor = SystemColors.Menu;
            lblDni.Location = new Point(30, 37);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(27, 15);
            lblDni.TabIndex = 43;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(30, 55);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(151, 23);
            txtDni.TabIndex = 42;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 9F);
            lblApellido.ForeColor = SystemColors.Menu;
            lblApellido.Location = new Point(217, 96);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(51, 15);
            lblApellido.TabIndex = 49;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(217, 114);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(151, 23);
            txtApellido.TabIndex = 48;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F);
            lblNombre.ForeColor = SystemColors.Menu;
            lblNombre.Location = new Point(30, 96);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 47;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(30, 114);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(151, 23);
            txtNombre.TabIndex = 46;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 9F);
            lblTelefono.ForeColor = SystemColors.Menu;
            lblTelefono.Location = new Point(396, 37);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(53, 15);
            lblTelefono.TabIndex = 51;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(396, 55);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(151, 23);
            txtTelefono.TabIndex = 50;
            // 
            // btnAplicar
            // 
            btnAplicar.BackColor = Color.FromArgb(45, 60, 82);
            btnAplicar.FlatStyle = FlatStyle.Popup;
            btnAplicar.ForeColor = SystemColors.Menu;
            btnAplicar.Location = new Point(396, 96);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(151, 41);
            btnAplicar.TabIndex = 52;
            btnAplicar.Text = "Aplicar filtros";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // gbFiltros
            // 
            gbFiltros.Controls.Add(txtDni);
            gbFiltros.Controls.Add(btnAplicar);
            gbFiltros.Controls.Add(lblDni);
            gbFiltros.Controls.Add(lblTelefono);
            gbFiltros.Controls.Add(txtCorreo);
            gbFiltros.Controls.Add(txtTelefono);
            gbFiltros.Controls.Add(lblCorreo);
            gbFiltros.Controls.Add(lblApellido);
            gbFiltros.Controls.Add(txtNombre);
            gbFiltros.Controls.Add(txtApellido);
            gbFiltros.Controls.Add(lblNombre);
            gbFiltros.ForeColor = SystemColors.Menu;
            gbFiltros.Location = new Point(15, 390);
            gbFiltros.Name = "gbFiltros";
            gbFiltros.Size = new Size(582, 176);
            gbFiltros.TabIndex = 53;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "Filtros";
            // 
            // btnSerializar
            // 
            btnSerializar.BackColor = Color.FromArgb(45, 60, 82);
            btnSerializar.FlatStyle = FlatStyle.Popup;
            btnSerializar.ForeColor = SystemColors.Menu;
            btnSerializar.Location = new Point(625, 460);
            btnSerializar.Name = "btnSerializar";
            btnSerializar.Size = new Size(155, 41);
            btnSerializar.TabIndex = 53;
            btnSerializar.Text = "Serializar";
            btnSerializar.UseVisualStyleBackColor = false;
            btnSerializar.Click += btnSerializar_Click;
            // 
            // btnDeserializar
            // 
            btnDeserializar.BackColor = Color.FromArgb(45, 60, 82);
            btnDeserializar.FlatStyle = FlatStyle.Popup;
            btnDeserializar.ForeColor = SystemColors.Menu;
            btnDeserializar.Location = new Point(625, 525);
            btnDeserializar.Name = "btnDeserializar";
            btnDeserializar.Size = new Size(155, 41);
            btnDeserializar.TabIndex = 54;
            btnDeserializar.Text = "Deserializar";
            btnDeserializar.UseVisualStyleBackColor = false;
            btnDeserializar.Click += btnDeserializar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.BackColor = Color.FromArgb(45, 60, 82);
            btnActualizar.FlatStyle = FlatStyle.Popup;
            btnActualizar.ForeColor = SystemColors.Menu;
            btnActualizar.Location = new Point(625, 390);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(155, 41);
            btnActualizar.TabIndex = 55;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // FrmClientes_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(814, 599);
            Controls.Add(btnActualizar);
            Controls.Add(btnDeserializar);
            Controls.Add(btnSerializar);
            Controls.Add(gbFiltros);
            Controls.Add(btnSalir);
            Controls.Add(lblClientes);
            Controls.Add(btnModificar);
            Controls.Add(btnBorrar);
            Controls.Add(grillaClientes);
            Controls.Add(btnAgregar);
            Name = "FrmClientes_064RB";
            Text = "FrmClientes_064RB";
            Load += FrmClientes_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaClientes).EndInit();
            gbFiltros.ResumeLayout(false);
            gbFiltros.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private Label lblClientes;
        private Button btnModificar;
        private Button btnBorrar;
        private DataGridView grillaClientes;
        private Button btnAgregar;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblDni;
        private TextBox txtDni;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Button btnAplicar;
        private GroupBox gbFiltros;
        private Button btnSerializar;
        private Button btnDeserializar;
        private Button btnActualizar;
    }
}