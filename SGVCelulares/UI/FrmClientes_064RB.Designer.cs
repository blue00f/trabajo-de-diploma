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
            ((System.ComponentModel.ISupportInitialize)grillaClientes).BeginInit();
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
            grillaClientes.Size = new Size(715, 267);
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
            // FrmClientes_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(740, 395);
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
    }
}