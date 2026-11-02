namespace UI_064RB.Negocio_064RB
{
    partial class FrmReporteVentas_064RB
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
            dtpDesde = new DateTimePicker();
            dtpHasta = new DateTimePicker();
            lblHasta = new Label();
            lblDesde = new Label();
            btnSalir = new Button();
            btnGenerar = new Button();
            SuspendLayout();
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(109, 63);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(108, 23);
            dtpDesde.TabIndex = 0;
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(109, 100);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(108, 23);
            dtpHasta.TabIndex = 1;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Segoe UI", 9F);
            lblHasta.ForeColor = SystemColors.Menu;
            lblHasta.Location = new Point(64, 106);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(37, 15);
            lblHasta.TabIndex = 27;
            lblHasta.Text = "Hasta";
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Segoe UI", 9F);
            lblDesde.ForeColor = SystemColors.Menu;
            lblDesde.Location = new Point(64, 69);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(39, 15);
            lblDesde.TabIndex = 26;
            lblDesde.Text = "Desde";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(64, 217);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(153, 43);
            btnSalir.TabIndex = 29;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnGenerar
            // 
            btnGenerar.BackColor = Color.FromArgb(45, 60, 82);
            btnGenerar.FlatStyle = FlatStyle.Popup;
            btnGenerar.ForeColor = SystemColors.Menu;
            btnGenerar.Location = new Point(64, 168);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(153, 43);
            btnGenerar.TabIndex = 28;
            btnGenerar.Text = "Generar PDF";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // FrmReporteVentas_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(282, 288);
            Controls.Add(btnSalir);
            Controls.Add(btnGenerar);
            Controls.Add(lblHasta);
            Controls.Add(lblDesde);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Name = "FrmReporteVentas_064RB";
            Text = "Reporte de ventas";
            Load += FrmReporteVentas_064RB_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpDesde;
        private DateTimePicker dtpHasta;
        private Label lblHasta;
        private Label lblDesde;
        private Button btnSalir;
        private Button btnGenerar;
    }
}