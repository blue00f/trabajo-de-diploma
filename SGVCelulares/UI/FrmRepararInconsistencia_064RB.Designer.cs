namespace UI
{
    partial class FrmRepararInconsistencia_064RB
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            pnlTopbar = new Panel();
            lblTitulo = new Label();
            pnlContenido = new Panel();
            grillaInconsistencias = new DataGridView();
            richTextBox1 = new RichTextBox();
            pnlBotones = new Panel();
            btnSalir = new Button();
            btnRestaurarBackup = new Button();
            btnRestaurarDV = new Button();
            pnlTopbar.SuspendLayout();
            pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grillaInconsistencias).BeginInit();
            pnlBotones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTopbar
            // 
            pnlTopbar.BackColor = Color.FromArgb(21, 32, 48);
            pnlTopbar.Controls.Add(lblTitulo);
            pnlTopbar.Dock = DockStyle.Top;
            pnlTopbar.Location = new Point(0, 0);
            pnlTopbar.Name = "pnlTopbar";
            pnlTopbar.Size = new Size(900, 48);
            pnlTopbar.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(226, 232, 240);
            lblTitulo.Location = new Point(16, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(213, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "REPARAR INCONSISTENCIAS";
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(30, 42, 58);
            pnlContenido.Controls.Add(grillaInconsistencias);
            pnlContenido.Controls.Add(richTextBox1);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 48);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Padding = new Padding(24);
            pnlContenido.Size = new Size(900, 422);
            pnlContenido.TabIndex = 1;
            // 
            // grillaInconsistencias
            // 
            grillaInconsistencias.AllowUserToAddRows = false;
            grillaInconsistencias.AllowUserToDeleteRows = false;
            grillaInconsistencias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grillaInconsistencias.BackgroundColor = Color.FromArgb(25, 37, 53);
            grillaInconsistencias.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(21, 32, 48);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(148, 163, 184);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            grillaInconsistencias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            grillaInconsistencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(25, 37, 53);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(45, 60, 82);
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(226, 232, 240);
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            grillaInconsistencias.DefaultCellStyle = dataGridViewCellStyle4;
            grillaInconsistencias.EnableHeadersVisualStyles = false;
            grillaInconsistencias.GridColor = Color.FromArgb(45, 60, 82);
            grillaInconsistencias.Location = new Point(24, 24);
            grillaInconsistencias.Name = "grillaInconsistencias";
            grillaInconsistencias.ReadOnly = true;
            grillaInconsistencias.RowHeadersVisible = false;
            grillaInconsistencias.Size = new Size(380, 374);
            grillaInconsistencias.TabIndex = 0;
            grillaInconsistencias.SelectionChanged += grillaInconsistencias_SelectionChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = Color.FromArgb(21, 32, 48);
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Font = new Font("Segoe UI", 9.5F);
            richTextBox1.ForeColor = Color.FromArgb(226, 232, 240);
            richTextBox1.Location = new Point(420, 24);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(456, 374);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // pnlBotones
            // 
            pnlBotones.BackColor = Color.FromArgb(25, 37, 53);
            pnlBotones.Controls.Add(btnSalir);
            pnlBotones.Controls.Add(btnRestaurarBackup);
            pnlBotones.Controls.Add(btnRestaurarDV);
            pnlBotones.Dock = DockStyle.Bottom;
            pnlBotones.Location = new Point(0, 470);
            pnlBotones.Name = "pnlBotones";
            pnlBotones.Padding = new Padding(24, 8, 24, 8);
            pnlBotones.Size = new Size(900, 90);
            pnlBotones.TabIndex = 2;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 9.5F);
            btnSalir.ForeColor = Color.FromArgb(226, 232, 240);
            btnSalir.Location = new Point(708, 11);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(168, 53);
            btnSalir.TabIndex = 2;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnRestaurarBackup
            // 
            btnRestaurarBackup.BackColor = Color.FromArgb(45, 60, 82);
            btnRestaurarBackup.Cursor = Cursors.Hand;
            btnRestaurarBackup.FlatAppearance.BorderSize = 0;
            btnRestaurarBackup.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRestaurarBackup.FlatStyle = FlatStyle.Flat;
            btnRestaurarBackup.Font = new Font("Segoe UI", 9.5F);
            btnRestaurarBackup.ForeColor = Color.FromArgb(226, 232, 240);
            btnRestaurarBackup.Location = new Point(16, 11);
            btnRestaurarBackup.Name = "btnRestaurarBackup";
            btnRestaurarBackup.Size = new Size(198, 53);
            btnRestaurarBackup.TabIndex = 0;
            btnRestaurarBackup.Text = "Realizar respaldo";
            btnRestaurarBackup.UseVisualStyleBackColor = false;
            btnRestaurarBackup.Click += btnRestaurarBackup_Click;
            // 
            // btnRestaurarDV
            // 
            btnRestaurarDV.BackColor = Color.FromArgb(45, 60, 82);
            btnRestaurarDV.Cursor = Cursors.Hand;
            btnRestaurarDV.FlatAppearance.BorderSize = 0;
            btnRestaurarDV.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnRestaurarDV.FlatStyle = FlatStyle.Flat;
            btnRestaurarDV.Font = new Font("Segoe UI", 9.5F);
            btnRestaurarDV.ForeColor = Color.FromArgb(226, 232, 240);
            btnRestaurarDV.Location = new Point(233, 11);
            btnRestaurarDV.Name = "btnRestaurarDV";
            btnRestaurarDV.Size = new Size(198, 53);
            btnRestaurarDV.TabIndex = 1;
            btnRestaurarDV.Text = "Recalcular digitos verificadores";
            btnRestaurarDV.UseVisualStyleBackColor = false;
            btnRestaurarDV.Click += btnRestaurarDV_Click;
            // 
            // FrmRepararInconsistencia_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(900, 560);
            Controls.Add(pnlContenido);
            Controls.Add(pnlBotones);
            Controls.Add(pnlTopbar);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmRepararInconsistencia_064RB";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reparar Inconsistencias";
            FormClosing += FrmRepararInconsistencia_064RB_FormClosing;
            Load += FrmRepararInconsistencia_Load;
            pnlTopbar.ResumeLayout(false);
            pnlTopbar.PerformLayout();
            pnlContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grillaInconsistencias).EndInit();
            pnlBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlTopbar;
        private Label lblTitulo;
        private Panel pnlContenido;
        private Panel pnlBotones;
        private Button btnRestaurarBackup;
        private Button btnRestaurarDV;
        private DataGridView grillaInconsistencias;
        private RichTextBox richTextBox1;
        private Button btnSalir;
    }
}