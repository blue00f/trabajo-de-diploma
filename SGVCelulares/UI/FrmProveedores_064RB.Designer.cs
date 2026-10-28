namespace UI_064RB
{
    partial class FrmProveedores_064RB
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
            lblProveedores = new Label();
            btnModificar = new Button();
            btnBorrar = new Button();
            grillaProveedores = new DataGridView();
            btnAgregar = new Button();
            txtCuit = new TextBox();
            lblCuit = new Label();
            lblRazonSocial = new Label();
            txtRazonSocial = new TextBox();
            ((System.ComponentModel.ISupportInitialize)grillaProveedores).BeginInit();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(332, 336);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 35);
            btnSalir.TabIndex = 11;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblProveedores
            // 
            lblProveedores.AutoSize = true;
            lblProveedores.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProveedores.ForeColor = SystemColors.Menu;
            lblProveedores.Location = new Point(29, 30);
            lblProveedores.Name = "lblProveedores";
            lblProveedores.Size = new Size(134, 30);
            lblProveedores.TabIndex = 10;
            lblProveedores.Text = "Proveedores";
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(45, 60, 82);
            btnModificar.FlatStyle = FlatStyle.Popup;
            btnModificar.ForeColor = SystemColors.Menu;
            btnModificar.Location = new Point(231, 336);
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
            btnBorrar.Location = new Point(130, 336);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(95, 35);
            btnBorrar.TabIndex = 8;
            btnBorrar.Text = "Borrar";
            btnBorrar.UseVisualStyleBackColor = false;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // grillaProveedores
            // 
            grillaProveedores.AllowUserToAddRows = false;
            grillaProveedores.AllowUserToDeleteRows = false;
            grillaProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaProveedores.Location = new Point(26, 63);
            grillaProveedores.Name = "grillaProveedores";
            grillaProveedores.ReadOnly = true;
            grillaProveedores.Size = new Size(639, 267);
            grillaProveedores.TabIndex = 7;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(45, 60, 82);
            btnAgregar.FlatStyle = FlatStyle.Popup;
            btnAgregar.ForeColor = SystemColors.Menu;
            btnAgregar.Location = new Point(29, 336);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(95, 35);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // txtCuit
            // 
            txtCuit.Location = new Point(130, 398);
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(196, 23);
            txtCuit.TabIndex = 12;
            // 
            // lblCuit
            // 
            lblCuit.AutoSize = true;
            lblCuit.Font = new Font("Segoe UI", 9F);
            lblCuit.ForeColor = SystemColors.Menu;
            lblCuit.Location = new Point(40, 401);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(33, 15);
            lblCuit.TabIndex = 17;
            lblCuit.Text = "CUIT";
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI", 9F);
            lblRazonSocial.ForeColor = SystemColors.Menu;
            lblRazonSocial.Location = new Point(40, 442);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(72, 15);
            lblRazonSocial.TabIndex = 19;
            lblRazonSocial.Text = "Razón social";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.Location = new Point(130, 439);
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(196, 23);
            txtRazonSocial.TabIndex = 18;
            // 
            // FrmProveedores_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(693, 520);
            Controls.Add(lblRazonSocial);
            Controls.Add(txtRazonSocial);
            Controls.Add(lblCuit);
            Controls.Add(txtCuit);
            Controls.Add(btnSalir);
            Controls.Add(lblProveedores);
            Controls.Add(btnModificar);
            Controls.Add(btnBorrar);
            Controls.Add(grillaProveedores);
            Controls.Add(btnAgregar);
            Name = "FrmProveedores_064RB";
            Text = "Proveedores";
            Load += FrmProveedores_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaProveedores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private Label lblProveedores;
        private Button btnModificar;
        private Button btnBorrar;
        private DataGridView grillaProveedores;
        private Button btnAgregar;
        private TextBox txtCuit;
        private Label lblCuit;
        private Label lblRazonSocial;
        private TextBox txtRazonSocial;
    }
}