namespace UI_064RB
{
    partial class FrmEvaluarSolicitud_064RB
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
            lblSolicitudes = new Label();
            btnRechazar = new Button();
            grillaSolicitudes = new DataGridView();
            btnAprobar = new Button();
            btnSalir = new Button();
            lblMotivo = new Label();
            txtMotivo = new TextBox();
            ((System.ComponentModel.ISupportInitialize)grillaSolicitudes).BeginInit();
            SuspendLayout();
            // 
            // lblSolicitudes
            // 
            lblSolicitudes.AutoSize = true;
            lblSolicitudes.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSolicitudes.ForeColor = SystemColors.Menu;
            lblSolicitudes.Location = new Point(26, 26);
            lblSolicitudes.Name = "lblSolicitudes";
            lblSolicitudes.Size = new Size(120, 30);
            lblSolicitudes.TabIndex = 14;
            lblSolicitudes.Text = "Solicitudes";
            // 
            // btnRechazar
            // 
            btnRechazar.BackColor = Color.FromArgb(45, 60, 82);
            btnRechazar.FlatStyle = FlatStyle.Popup;
            btnRechazar.ForeColor = SystemColors.Menu;
            btnRechazar.Location = new Point(127, 332);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(95, 35);
            btnRechazar.TabIndex = 13;
            btnRechazar.Text = "Rechazar";
            btnRechazar.UseVisualStyleBackColor = false;
            btnRechazar.Click += btnRechazar_Click;
            // 
            // grillaSolicitudes
            // 
            grillaSolicitudes.AllowUserToAddRows = false;
            grillaSolicitudes.AllowUserToDeleteRows = false;
            grillaSolicitudes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaSolicitudes.Location = new Point(23, 59);
            grillaSolicitudes.Name = "grillaSolicitudes";
            grillaSolicitudes.ReadOnly = true;
            grillaSolicitudes.Size = new Size(921, 267);
            grillaSolicitudes.TabIndex = 12;
            // 
            // btnAprobar
            // 
            btnAprobar.BackColor = Color.FromArgb(45, 60, 82);
            btnAprobar.FlatStyle = FlatStyle.Popup;
            btnAprobar.ForeColor = SystemColors.Menu;
            btnAprobar.Location = new Point(26, 332);
            btnAprobar.Name = "btnAprobar";
            btnAprobar.Size = new Size(95, 35);
            btnAprobar.TabIndex = 11;
            btnAprobar.Text = "Aprobar";
            btnAprobar.UseVisualStyleBackColor = false;
            btnAprobar.Click += btnAprobar_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(228, 332);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 35);
            btnSalir.TabIndex = 15;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblMotivo
            // 
            lblMotivo.AutoSize = true;
            lblMotivo.Font = new Font("Segoe UI", 9F);
            lblMotivo.ForeColor = SystemColors.Menu;
            lblMotivo.Location = new Point(26, 396);
            lblMotivo.Name = "lblMotivo";
            lblMotivo.Size = new Size(48, 15);
            lblMotivo.TabIndex = 19;
            lblMotivo.Text = "Motivo:";
            // 
            // txtMotivo
            // 
            txtMotivo.Location = new Point(26, 414);
            txtMotivo.MaxLength = 200;
            txtMotivo.Multiline = true;
            txtMotivo.Name = "txtMotivo";
            txtMotivo.Size = new Size(297, 72);
            txtMotivo.TabIndex = 18;
            // 
            // FrmEvaluarSolicitud_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(969, 515);
            Controls.Add(lblMotivo);
            Controls.Add(txtMotivo);
            Controls.Add(btnSalir);
            Controls.Add(lblSolicitudes);
            Controls.Add(btnRechazar);
            Controls.Add(grillaSolicitudes);
            Controls.Add(btnAprobar);
            Name = "FrmEvaluarSolicitud_064RB";
            Text = "Evaluación de solicitud";
            Load += FrmEvaluarSolicitud_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaSolicitudes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSolicitudes;
        private Button btnRechazar;
        private DataGridView grillaSolicitudes;
        private Button btnAprobar;
        private Button btnSalir;
        private Label lblMotivo;
        private TextBox txtMotivo;
    }
}