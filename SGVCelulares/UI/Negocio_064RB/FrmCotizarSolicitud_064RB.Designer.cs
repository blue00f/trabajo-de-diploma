namespace UI_064RB
{
    partial class FrmCotizarSolicitud_064RB
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
            lblSolicitudesReposicion = new Label();
            btnSalir = new Button();
            grillaSolicitudes = new DataGridView();
            btnCotizar = new Button();
            lblPrecioUnitario = new Label();
            txtPrecioUnitario = new TextBox();
            grillaProveedores = new DataGridView();
            lblProveedoresRegistrados = new Label();
            ((System.ComponentModel.ISupportInitialize)grillaSolicitudes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grillaProveedores).BeginInit();
            SuspendLayout();
            // 
            // lblSolicitudesReposicion
            // 
            lblSolicitudesReposicion.AutoSize = true;
            lblSolicitudesReposicion.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSolicitudesReposicion.ForeColor = SystemColors.Menu;
            lblSolicitudesReposicion.Location = new Point(29, 31);
            lblSolicitudesReposicion.Name = "lblSolicitudesReposicion";
            lblSolicitudesReposicion.Size = new Size(258, 30);
            lblSolicitudesReposicion.TabIndex = 22;
            lblSolicitudesReposicion.Text = "Solicitudes de reposición";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(130, 337);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 35);
            btnSalir.TabIndex = 21;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // grillaSolicitudes
            // 
            grillaSolicitudes.AllowUserToAddRows = false;
            grillaSolicitudes.AllowUserToDeleteRows = false;
            grillaSolicitudes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaSolicitudes.Location = new Point(26, 64);
            grillaSolicitudes.Name = "grillaSolicitudes";
            grillaSolicitudes.ReadOnly = true;
            grillaSolicitudes.Size = new Size(639, 267);
            grillaSolicitudes.TabIndex = 20;
            // 
            // btnCotizar
            // 
            btnCotizar.BackColor = Color.FromArgb(45, 60, 82);
            btnCotizar.FlatStyle = FlatStyle.Popup;
            btnCotizar.ForeColor = SystemColors.Menu;
            btnCotizar.Location = new Point(29, 337);
            btnCotizar.Name = "btnCotizar";
            btnCotizar.Size = new Size(95, 35);
            btnCotizar.TabIndex = 19;
            btnCotizar.Text = "Cotizar";
            btnCotizar.UseVisualStyleBackColor = false;
            btnCotizar.Click += btnCotizar_Click;
            // 
            // lblPrecioUnitario
            // 
            lblPrecioUnitario.AutoSize = true;
            lblPrecioUnitario.Font = new Font("Segoe UI", 9F);
            lblPrecioUnitario.ForeColor = SystemColors.Menu;
            lblPrecioUnitario.Location = new Point(29, 404);
            lblPrecioUnitario.Name = "lblPrecioUnitario";
            lblPrecioUnitario.Size = new Size(87, 15);
            lblPrecioUnitario.TabIndex = 24;
            lblPrecioUnitario.Text = "Precio unitario:";
            // 
            // txtPrecioUnitario
            // 
            txtPrecioUnitario.Location = new Point(130, 401);
            txtPrecioUnitario.Name = "txtPrecioUnitario";
            txtPrecioUnitario.Size = new Size(196, 23);
            txtPrecioUnitario.TabIndex = 23;
            // 
            // grillaProveedores
            // 
            grillaProveedores.AllowUserToAddRows = false;
            grillaProveedores.AllowUserToDeleteRows = false;
            grillaProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaProveedores.Location = new Point(671, 64);
            grillaProveedores.Name = "grillaProveedores";
            grillaProveedores.ReadOnly = true;
            grillaProveedores.Size = new Size(463, 267);
            grillaProveedores.TabIndex = 27;
            // 
            // lblProveedoresRegistrados
            // 
            lblProveedoresRegistrados.AutoSize = true;
            lblProveedoresRegistrados.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProveedoresRegistrados.ForeColor = SystemColors.Menu;
            lblProveedoresRegistrados.Location = new Point(671, 31);
            lblProveedoresRegistrados.Name = "lblProveedoresRegistrados";
            lblProveedoresRegistrados.Size = new Size(249, 30);
            lblProveedoresRegistrados.TabIndex = 28;
            lblProveedoresRegistrados.Text = "Proveedores registrados";
            // 
            // FrmCotizarSolicitud_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1150, 469);
            Controls.Add(lblProveedoresRegistrados);
            Controls.Add(grillaProveedores);
            Controls.Add(lblPrecioUnitario);
            Controls.Add(txtPrecioUnitario);
            Controls.Add(lblSolicitudesReposicion);
            Controls.Add(btnSalir);
            Controls.Add(grillaSolicitudes);
            Controls.Add(btnCotizar);
            Name = "FrmCotizarSolicitud_064RB";
            Text = "Cotizar solicitud";
            Load += FrmCotizarSolicitud_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaSolicitudes).EndInit();
            ((System.ComponentModel.ISupportInitialize)grillaProveedores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSolicitudesReposicion;
        private Button btnSalir;
        private DataGridView grillaSolicitudes;
        private Button btnCotizar;
        private Label lblPrecioUnitario;
        private TextBox txtPrecioUnitario;
        private DataGridView grillaProveedores;
        private Label lblProveedoresRegistrados;
    }
}