namespace UI_064RB
{
    partial class FrmSolicitarReposicion_064RB
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
            lblReposicion = new Label();
            btnSalir = new Button();
            grillaProductosAReponer = new DataGridView();
            btnSolicitar = new Button();
            lblCantidad = new Label();
            nudCantidad = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)grillaProductosAReponer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            SuspendLayout();
            // 
            // lblReposicion
            // 
            lblReposicion.AutoSize = true;
            lblReposicion.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblReposicion.ForeColor = SystemColors.Menu;
            lblReposicion.Location = new Point(28, 27);
            lblReposicion.Name = "lblReposicion";
            lblReposicion.Size = new Size(208, 30);
            lblReposicion.TabIndex = 14;
            lblReposicion.Text = "Reposición de stock";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(129, 333);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 35);
            btnSalir.TabIndex = 13;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // grillaProductosAReponer
            // 
            grillaProductosAReponer.AllowUserToAddRows = false;
            grillaProductosAReponer.AllowUserToDeleteRows = false;
            grillaProductosAReponer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaProductosAReponer.Location = new Point(25, 60);
            grillaProductosAReponer.Name = "grillaProductosAReponer";
            grillaProductosAReponer.ReadOnly = true;
            grillaProductosAReponer.Size = new Size(639, 267);
            grillaProductosAReponer.TabIndex = 12;
            // 
            // btnSolicitar
            // 
            btnSolicitar.BackColor = Color.FromArgb(45, 60, 82);
            btnSolicitar.FlatStyle = FlatStyle.Popup;
            btnSolicitar.ForeColor = SystemColors.Menu;
            btnSolicitar.Location = new Point(28, 333);
            btnSolicitar.Name = "btnSolicitar";
            btnSolicitar.Size = new Size(95, 35);
            btnSolicitar.TabIndex = 11;
            btnSolicitar.Text = "Solicitar";
            btnSolicitar.UseVisualStyleBackColor = false;
            btnSolicitar.Click += btnSolicitar_Click;
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 9F);
            lblCantidad.ForeColor = SystemColors.Menu;
            lblCantidad.Location = new Point(284, 343);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(111, 15);
            lblCantidad.TabIndex = 18;
            lblCantidad.Text = "Cantidad a reponer:";
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(401, 341);
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(130, 23);
            nudCantidad.TabIndex = 17;
            // 
            // FrmSolicitarReposicion_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(800, 450);
            Controls.Add(lblCantidad);
            Controls.Add(nudCantidad);
            Controls.Add(lblReposicion);
            Controls.Add(btnSalir);
            Controls.Add(grillaProductosAReponer);
            Controls.Add(btnSolicitar);
            Name = "FrmSolicitarReposicion_064RB";
            Text = "FrmSolicitarReposicion_064RB";
            Load += FrmSolicitarReposicion_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaProductosAReponer).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblReposicion;
        private Button btnSalir;
        private DataGridView grillaProductosAReponer;
        private Button btnSolicitar;
        private Label lblCantidad;
        private NumericUpDown nudCantidad;
    }
}