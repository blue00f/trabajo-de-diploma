namespace UI_064RB
{
    partial class FrmRegistrarDiscrepancia_064RB
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
            lblCantidadPedida = new Label();
            txtCantidadFaltante = new TextBox();
            btnCancelar = new Button();
            btnAceptar = new Button();
            txtCantidadDanada = new TextBox();
            txtObservaciones = new TextBox();
            lblCantidadFaltante = new Label();
            lblCantidadDanada = new Label();
            lblObservaciones = new Label();
            SuspendLayout();
            // 
            // lblCantidadPedida
            // 
            lblCantidadPedida.AutoSize = true;
            lblCantidadPedida.Font = new Font("Segoe UI", 9F);
            lblCantidadPedida.ForeColor = SystemColors.Menu;
            lblCantidadPedida.Location = new Point(12, 21);
            lblCantidadPedida.Name = "lblCantidadPedida";
            lblCantidadPedida.Size = new Size(94, 15);
            lblCantidadPedida.TabIndex = 21;
            lblCantidadPedida.Text = "Cantidad pedida";
            // 
            // txtCantidadFaltante
            // 
            txtCantidadFaltante.Location = new Point(262, 71);
            txtCantidadFaltante.Name = "txtCantidadFaltante";
            txtCantidadFaltante.Size = new Size(115, 23);
            txtCantidadFaltante.TabIndex = 20;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(45, 60, 82);
            btnCancelar.FlatStyle = FlatStyle.Popup;
            btnCancelar.ForeColor = SystemColors.Menu;
            btnCancelar.Location = new Point(208, 286);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(169, 35);
            btnCancelar.TabIndex = 19;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.BackColor = Color.FromArgb(45, 60, 82);
            btnAceptar.FlatStyle = FlatStyle.Popup;
            btnAceptar.ForeColor = SystemColors.Menu;
            btnAceptar.Location = new Point(12, 286);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(169, 35);
            btnAceptar.TabIndex = 18;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = false;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // txtCantidadDanada
            // 
            txtCantidadDanada.Location = new Point(262, 100);
            txtCantidadDanada.Name = "txtCantidadDanada";
            txtCantidadDanada.Size = new Size(115, 23);
            txtCantidadDanada.TabIndex = 22;
            // 
            // txtObservaciones
            // 
            txtObservaciones.Location = new Point(12, 149);
            txtObservaciones.MaxLength = 300;
            txtObservaciones.Multiline = true;
            txtObservaciones.Name = "txtObservaciones";
            txtObservaciones.Size = new Size(365, 117);
            txtObservaciones.TabIndex = 23;
            // 
            // lblCantidadFaltante
            // 
            lblCantidadFaltante.AutoSize = true;
            lblCantidadFaltante.Font = new Font("Segoe UI", 9F);
            lblCantidadFaltante.ForeColor = SystemColors.Menu;
            lblCantidadFaltante.Location = new Point(158, 74);
            lblCantidadFaltante.Name = "lblCantidadFaltante";
            lblCantidadFaltante.Size = new Size(98, 15);
            lblCantidadFaltante.TabIndex = 24;
            lblCantidadFaltante.Text = "Cantidad faltante";
            // 
            // lblCantidadDanada
            // 
            lblCantidadDanada.AutoSize = true;
            lblCantidadDanada.Font = new Font("Segoe UI", 9F);
            lblCantidadDanada.ForeColor = SystemColors.Menu;
            lblCantidadDanada.Location = new Point(158, 103);
            lblCantidadDanada.Name = "lblCantidadDanada";
            lblCantidadDanada.Size = new Size(97, 15);
            lblCantidadDanada.TabIndex = 25;
            lblCantidadDanada.Text = "Cantidad dañada";
            // 
            // lblObservaciones
            // 
            lblObservaciones.AutoSize = true;
            lblObservaciones.Font = new Font("Segoe UI", 9F);
            lblObservaciones.ForeColor = SystemColors.Menu;
            lblObservaciones.Location = new Point(12, 131);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(84, 15);
            lblObservaciones.TabIndex = 26;
            lblObservaciones.Text = "Observaciones";
            // 
            // FrmRegistrarDiscrepancia_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(389, 340);
            Controls.Add(lblObservaciones);
            Controls.Add(lblCantidadDanada);
            Controls.Add(lblCantidadFaltante);
            Controls.Add(txtObservaciones);
            Controls.Add(txtCantidadDanada);
            Controls.Add(lblCantidadPedida);
            Controls.Add(txtCantidadFaltante);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRegistrarDiscrepancia_064RB";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Registrar discrepancia";
            Load += FrmRegistrarDiscrepancia_064RB_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCantidadPedida;
        private TextBox txtCantidadFaltante;
        private Button btnCancelar;
        private Button btnAceptar;
        private TextBox txtCantidadDanada;
        private TextBox txtObservaciones;
        private Label lblCantidadFaltante;
        private Label lblCantidadDanada;
        private Label lblObservaciones;
    }
}