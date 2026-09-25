namespace UI_064RB
{
    partial class FrmUnidadCelular_064RB
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
            lblUnidadCelular = new Label();
            btnModificar = new Button();
            btnBorrar = new Button();
            grillaUnidadCelular = new DataGridView();
            btnAgregar = new Button();
            cbxCelulares = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)grillaUnidadCelular).BeginInit();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(679, 341);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 35);
            btnSalir.TabIndex = 11;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblUnidadCelular
            // 
            lblUnidadCelular.AutoSize = true;
            lblUnidadCelular.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUnidadCelular.ForeColor = SystemColors.Menu;
            lblUnidadCelular.Location = new Point(15, 35);
            lblUnidadCelular.Name = "lblUnidadCelular";
            lblUnidadCelular.Size = new Size(178, 30);
            lblUnidadCelular.TabIndex = 10;
            lblUnidadCelular.Text = "Unidad Celulares";
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(45, 60, 82);
            btnModificar.FlatStyle = FlatStyle.Popup;
            btnModificar.ForeColor = SystemColors.Menu;
            btnModificar.Location = new Point(217, 341);
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
            btnBorrar.Location = new Point(116, 341);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(95, 35);
            btnBorrar.TabIndex = 8;
            btnBorrar.Text = "Borrar";
            btnBorrar.UseVisualStyleBackColor = false;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // grillaUnidadCelular
            // 
            grillaUnidadCelular.AllowUserToAddRows = false;
            grillaUnidadCelular.AllowUserToDeleteRows = false;
            grillaUnidadCelular.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaUnidadCelular.Location = new Point(12, 68);
            grillaUnidadCelular.Name = "grillaUnidadCelular";
            grillaUnidadCelular.ReadOnly = true;
            grillaUnidadCelular.Size = new Size(762, 267);
            grillaUnidadCelular.TabIndex = 7;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(45, 60, 82);
            btnAgregar.FlatStyle = FlatStyle.Popup;
            btnAgregar.ForeColor = SystemColors.Menu;
            btnAgregar.Location = new Point(15, 341);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(95, 35);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // cbxCelulares
            // 
            cbxCelulares.FormattingEnabled = true;
            cbxCelulares.Location = new Point(15, 392);
            cbxCelulares.Name = "cbxCelulares";
            cbxCelulares.Size = new Size(495, 23);
            cbxCelulares.TabIndex = 12;
            // 
            // FrmUnidadCelular_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(795, 450);
            Controls.Add(cbxCelulares);
            Controls.Add(btnSalir);
            Controls.Add(lblUnidadCelular);
            Controls.Add(btnModificar);
            Controls.Add(btnBorrar);
            Controls.Add(grillaUnidadCelular);
            Controls.Add(btnAgregar);
            Name = "FrmUnidadCelular_064RB";
            Text = "FrmUnidadCelular_064RB";
            Load += FrmUnidadCelular_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaUnidadCelular).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private Label lblUnidadCelular;
        private Button btnModificar;
        private Button btnBorrar;
        private DataGridView grillaUnidadCelular;
        private Button btnAgregar;
        private ComboBox cbxCelulares;
    }
}