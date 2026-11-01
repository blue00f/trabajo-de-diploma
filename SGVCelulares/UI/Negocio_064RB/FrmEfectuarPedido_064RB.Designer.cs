namespace UI_064RB
{
    partial class FrmEfectuarPedido_064RB
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
            btnSalir = new Button();
            grillaSolicitudes = new DataGridView();
            btnEfectuarPedido = new Button();
            ((System.ComponentModel.ISupportInitialize)grillaSolicitudes).BeginInit();
            SuspendLayout();
            // 
            // lblSolicitudes
            // 
            lblSolicitudes.AutoSize = true;
            lblSolicitudes.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSolicitudes.ForeColor = SystemColors.Menu;
            lblSolicitudes.Location = new Point(24, 27);
            lblSolicitudes.Name = "lblSolicitudes";
            lblSolicitudes.Size = new Size(228, 30);
            lblSolicitudes.TabIndex = 26;
            lblSolicitudes.Text = "Solicitudes aprobadas";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(197, 333);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 35);
            btnSalir.TabIndex = 25;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // grillaSolicitudes
            // 
            grillaSolicitudes.AllowUserToAddRows = false;
            grillaSolicitudes.AllowUserToDeleteRows = false;
            grillaSolicitudes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaSolicitudes.Location = new Point(24, 60);
            grillaSolicitudes.Name = "grillaSolicitudes";
            grillaSolicitudes.ReadOnly = true;
            grillaSolicitudes.Size = new Size(1136, 267);
            grillaSolicitudes.TabIndex = 24;
            // 
            // btnEfectuarPedido
            // 
            btnEfectuarPedido.BackColor = Color.FromArgb(45, 60, 82);
            btnEfectuarPedido.FlatStyle = FlatStyle.Popup;
            btnEfectuarPedido.ForeColor = SystemColors.Menu;
            btnEfectuarPedido.Location = new Point(24, 333);
            btnEfectuarPedido.Name = "btnEfectuarPedido";
            btnEfectuarPedido.Size = new Size(167, 35);
            btnEfectuarPedido.TabIndex = 23;
            btnEfectuarPedido.Text = "Efectuar pedido";
            btnEfectuarPedido.UseVisualStyleBackColor = false;
            btnEfectuarPedido.Click += btnEfectuarPedido_Click;
            // 
            // FrmEfectuarPedido_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1172, 390);
            Controls.Add(lblSolicitudes);
            Controls.Add(btnSalir);
            Controls.Add(grillaSolicitudes);
            Controls.Add(btnEfectuarPedido);
            Name = "FrmEfectuarPedido_064RB";
            Text = "Efectuar pedido al proveedor";
            Load += FrmEfectuarPedido_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaSolicitudes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSolicitudes;
        private Button btnSalir;
        private DataGridView grillaSolicitudes;
        private Button btnEfectuarPedido;
    }
}