namespace UI_064RB
{
    partial class FrmAyuda_064RB
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
            txtFaqNegocio = new TextBox();
            txtFaqAdmin = new TextBox();
            lblFaq = new Label();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // txtFaqNegocio
            // 
            txtFaqNegocio.BackColor = Color.FromArgb(25, 37, 53);
            txtFaqNegocio.ForeColor = SystemColors.Menu;
            txtFaqNegocio.Location = new Point(13, 68);
            txtFaqNegocio.Multiline = true;
            txtFaqNegocio.Name = "txtFaqNegocio";
            txtFaqNegocio.ReadOnly = true;
            txtFaqNegocio.ScrollBars = ScrollBars.Both;
            txtFaqNegocio.Size = new Size(776, 217);
            txtFaqNegocio.TabIndex = 0;
            // 
            // txtFaqAdmin
            // 
            txtFaqAdmin.BackColor = Color.FromArgb(25, 37, 53);
            txtFaqAdmin.ForeColor = SystemColors.Menu;
            txtFaqAdmin.Location = new Point(13, 306);
            txtFaqAdmin.Multiline = true;
            txtFaqAdmin.Name = "txtFaqAdmin";
            txtFaqAdmin.ReadOnly = true;
            txtFaqAdmin.ScrollBars = ScrollBars.Both;
            txtFaqAdmin.Size = new Size(776, 217);
            txtFaqAdmin.TabIndex = 1;
            // 
            // lblFaq
            // 
            lblFaq.AutoSize = true;
            lblFaq.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFaq.ForeColor = SystemColors.Menu;
            lblFaq.Location = new Point(13, 22);
            lblFaq.Name = "lblFaq";
            lblFaq.Size = new Size(220, 30);
            lblFaq.TabIndex = 25;
            lblFaq.Text = "Preguntas frecuentes";
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
            btnSalir.Location = new Point(693, 26);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(96, 28);
            btnSalir.TabIndex = 26;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // FrmAyuda_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(801, 549);
            Controls.Add(btnSalir);
            Controls.Add(lblFaq);
            Controls.Add(txtFaqAdmin);
            Controls.Add(txtFaqNegocio);
            Name = "FrmAyuda_064RB";
            Load += FrmAyuda_064RB_Load;
            Shown += FrmAyuda_064RB_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFaqNegocio;
        private TextBox txtFaqAdmin;
        private Label lblFaq;
        private Button btnSalir;
    }
}