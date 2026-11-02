namespace UI_064RB.Negocio_064RB
{
    partial class FrmReporteReposicion_064RB
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
            btnGenerar = new Button();
            lblHasta = new Label();
            lblDesde = new Label();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(46, 210);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(153, 43);
            btnSalir.TabIndex = 35;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnGenerar
            // 
            btnGenerar.BackColor = Color.FromArgb(45, 60, 82);
            btnGenerar.FlatStyle = FlatStyle.Popup;
            btnGenerar.ForeColor = SystemColors.Menu;
            btnGenerar.Location = new Point(46, 161);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(153, 43);
            btnGenerar.TabIndex = 34;
            btnGenerar.Text = "Generar PDF";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.Font = new Font("Segoe UI", 9F);
            lblHasta.ForeColor = SystemColors.Menu;
            lblHasta.Location = new Point(46, 99);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(37, 15);
            lblHasta.TabIndex = 33;
            lblHasta.Text = "Hasta";
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.Font = new Font("Segoe UI", 9F);
            lblDesde.ForeColor = SystemColors.Menu;
            lblDesde.Location = new Point(46, 62);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(39, 15);
            lblDesde.TabIndex = 32;
            lblDesde.Text = "Desde";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(91, 93);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(108, 23);
            dtpHasta.TabIndex = 31;
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(91, 56);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(108, 23);
            dtpDesde.TabIndex = 30;
            // 
            // FrmReporteReposicion_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(250, 308);
            Controls.Add(btnSalir);
            Controls.Add(btnGenerar);
            Controls.Add(lblHasta);
            Controls.Add(lblDesde);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Name = "FrmReporteReposicion_064RB";
            Text = "Reporte reposición";
            Load += FrmReporteReposicion_064RB_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSalir;
        private Button btnGenerar;
        private Label lblHasta;
        private Label lblDesde;
        private DateTimePicker dtpHasta;
        private DateTimePicker dtpDesde;
    }
}