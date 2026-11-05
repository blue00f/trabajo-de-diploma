namespace UI_064RB.Negocio_064RB
{
    partial class FrmAccesoriosCopia_064RB
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
            lblSku = new Label();
            txtSku = new TextBox();
            lblSolicitudesReposicion = new Label();
            btnSalir = new Button();
            grillaAccesorios = new DataGridView();
            btnAplicar = new Button();
            btnActivar = new Button();
            btnLimpiar = new Button();
            lblFechaFin = new Label();
            lblFechaInicio = new Label();
            dtpFin = new DateTimePicker();
            dtpInicio = new DateTimePicker();
            lblModelo = new Label();
            txtModelo = new TextBox();
            lblMarca = new Label();
            txtMarca = new TextBox();
            ((System.ComponentModel.ISupportInitialize)grillaAccesorios).BeginInit();
            SuspendLayout();
            // 
            // lblSku
            // 
            lblSku.AutoSize = true;
            lblSku.Font = new Font("Segoe UI", 9F);
            lblSku.ForeColor = SystemColors.Menu;
            lblSku.Location = new Point(12, 344);
            lblSku.Name = "lblSku";
            lblSku.Size = new Size(28, 15);
            lblSku.TabIndex = 30;
            lblSku.Text = "SKU";
            // 
            // txtSku
            // 
            txtSku.Location = new Point(12, 362);
            txtSku.Name = "txtSku";
            txtSku.Size = new Size(151, 23);
            txtSku.TabIndex = 29;
            // 
            // lblSolicitudesReposicion
            // 
            lblSolicitudesReposicion.AutoSize = true;
            lblSolicitudesReposicion.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSolicitudesReposicion.ForeColor = SystemColors.Menu;
            lblSolicitudesReposicion.Location = new Point(12, 24);
            lblSolicitudesReposicion.Name = "lblSolicitudesReposicion";
            lblSolicitudesReposicion.Size = new Size(393, 30);
            lblSolicitudesReposicion.TabIndex = 28;
            lblSolicitudesReposicion.Text = "Bitácora de cambios - Accesorios copia";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(545, 406);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(132, 35);
            btnSalir.TabIndex = 27;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // grillaAccesorios
            // 
            grillaAccesorios.AllowUserToAddRows = false;
            grillaAccesorios.AllowUserToDeleteRows = false;
            grillaAccesorios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaAccesorios.Location = new Point(12, 57);
            grillaAccesorios.Name = "grillaAccesorios";
            grillaAccesorios.ReadOnly = true;
            grillaAccesorios.Size = new Size(1394, 267);
            grillaAccesorios.TabIndex = 26;
            // 
            // btnAplicar
            // 
            btnAplicar.BackColor = Color.FromArgb(45, 60, 82);
            btnAplicar.FlatStyle = FlatStyle.Popup;
            btnAplicar.ForeColor = SystemColors.Menu;
            btnAplicar.Location = new Point(366, 355);
            btnAplicar.Name = "btnAplicar";
            btnAplicar.Size = new Size(151, 35);
            btnAplicar.TabIndex = 25;
            btnAplicar.Text = "Aplicar";
            btnAplicar.UseVisualStyleBackColor = false;
            btnAplicar.Click += btnAplicar_Click;
            // 
            // btnActivar
            // 
            btnActivar.BackColor = Color.FromArgb(45, 60, 82);
            btnActivar.FlatStyle = FlatStyle.Popup;
            btnActivar.ForeColor = SystemColors.Menu;
            btnActivar.Location = new Point(545, 355);
            btnActivar.Name = "btnActivar";
            btnActivar.Size = new Size(132, 35);
            btnActivar.TabIndex = 32;
            btnActivar.Text = "Activar";
            btnActivar.UseVisualStyleBackColor = false;
            btnActivar.Click += btnActivar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(45, 60, 82);
            btnLimpiar.FlatStyle = FlatStyle.Popup;
            btnLimpiar.ForeColor = SystemColors.Menu;
            btnLimpiar.Location = new Point(366, 406);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(151, 35);
            btnLimpiar.TabIndex = 31;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // lblFechaFin
            // 
            lblFechaFin.AutoSize = true;
            lblFechaFin.Font = new Font("Segoe UI", 9F);
            lblFechaFin.ForeColor = SystemColors.Menu;
            lblFechaFin.Location = new Point(12, 457);
            lblFechaFin.Name = "lblFechaFin";
            lblFechaFin.Size = new Size(55, 15);
            lblFechaFin.TabIndex = 37;
            lblFechaFin.Text = "Fecha fin";
            // 
            // lblFechaInicio
            // 
            lblFechaInicio.AutoSize = true;
            lblFechaInicio.Font = new Font("Segoe UI", 9F);
            lblFechaInicio.ForeColor = SystemColors.Menu;
            lblFechaInicio.Location = new Point(12, 400);
            lblFechaInicio.Name = "lblFechaInicio";
            lblFechaInicio.Size = new Size(70, 15);
            lblFechaInicio.TabIndex = 36;
            lblFechaInicio.Text = "Fecha inicio";
            // 
            // dtpFin
            // 
            dtpFin.Format = DateTimePickerFormat.Short;
            dtpFin.Location = new Point(12, 475);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(151, 23);
            dtpFin.TabIndex = 35;
            // 
            // dtpInicio
            // 
            dtpInicio.Format = DateTimePickerFormat.Short;
            dtpInicio.Location = new Point(12, 418);
            dtpInicio.MaxDate = new DateTime(2026, 11, 5, 0, 0, 0, 0);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(151, 23);
            dtpInicio.TabIndex = 34;
            dtpInicio.Value = new DateTime(2026, 11, 5, 0, 0, 0, 0);
            // 
            // lblModelo
            // 
            lblModelo.AutoSize = true;
            lblModelo.Font = new Font("Segoe UI", 9F);
            lblModelo.ForeColor = SystemColors.Menu;
            lblModelo.Location = new Point(199, 400);
            lblModelo.Name = "lblModelo";
            lblModelo.Size = new Size(48, 15);
            lblModelo.TabIndex = 39;
            lblModelo.Text = "Modelo";
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(199, 418);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(151, 23);
            txtModelo.TabIndex = 38;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 9F);
            lblMarca.ForeColor = SystemColors.Menu;
            lblMarca.Location = new Point(199, 344);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(40, 15);
            lblMarca.TabIndex = 41;
            lblMarca.Text = "Marca";
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(199, 362);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(151, 23);
            txtMarca.TabIndex = 40;
            // 
            // FrmAccesoriosCopia_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1418, 539);
            Controls.Add(lblMarca);
            Controls.Add(txtMarca);
            Controls.Add(lblModelo);
            Controls.Add(txtModelo);
            Controls.Add(lblFechaFin);
            Controls.Add(lblFechaInicio);
            Controls.Add(dtpFin);
            Controls.Add(dtpInicio);
            Controls.Add(btnActivar);
            Controls.Add(btnLimpiar);
            Controls.Add(lblSku);
            Controls.Add(txtSku);
            Controls.Add(lblSolicitudesReposicion);
            Controls.Add(btnSalir);
            Controls.Add(grillaAccesorios);
            Controls.Add(btnAplicar);
            Name = "FrmAccesoriosCopia_064RB";
            Text = "Accesorio Copias";
            Load += FrmAccesoriosCopia_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaAccesorios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSku;
        private TextBox txtSku;
        private Label lblSolicitudesReposicion;
        private Button btnSalir;
        private DataGridView grillaAccesorios;
        private Button btnAplicar;
        private Button btnActivar;
        private Button btnLimpiar;
        private Label lblFechaFin;
        private Label lblFechaInicio;
        private DateTimePicker dtpFin;
        private DateTimePicker dtpInicio;
        private Label lblModelo;
        private TextBox txtModelo;
        private Label lblMarca;
        private TextBox txtMarca;
    }
}