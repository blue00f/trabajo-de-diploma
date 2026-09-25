namespace UI_064RB
{
    partial class FrmVenta_064RB
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
            lblSeccionCliente = new Label();
            btnAgregarProducto = new Button();
            grillaProductos = new DataGridView();
            btnBuscarCliente = new Button();
            txtDni = new TextBox();
            lblDni = new Label();
            lblClienteTitulo = new Label();
            lblCliente = new Label();
            lblFormaPago = new Label();
            label1 = new Label();
            cbxFormaPago = new ComboBox();
            lblProductos = new Label();
            lblSubtotalTitulo = new Label();
            lblSubtotal = new Label();
            btnProcesarPago = new Button();
            btnEmitirComprobante = new Button();
            lblDetalleVenta = new Label();
            grillaDetalleVenta = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)grillaProductos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grillaDetalleVenta).BeginInit();
            SuspendLayout();
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(1019, 440);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(118, 48);
            btnSalir.TabIndex = 12;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblSeccionCliente
            // 
            lblSeccionCliente.AutoSize = true;
            lblSeccionCliente.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSeccionCliente.ForeColor = SystemColors.Menu;
            lblSeccionCliente.Location = new Point(20, 31);
            lblSeccionCliente.Name = "lblSeccionCliente";
            lblSeccionCliente.Size = new Size(81, 30);
            lblSeccionCliente.TabIndex = 11;
            lblSeccionCliente.Text = "Cliente";
            // 
            // btnAgregarProducto
            // 
            btnAgregarProducto.BackColor = Color.FromArgb(45, 60, 82);
            btnAgregarProducto.FlatStyle = FlatStyle.Popup;
            btnAgregarProducto.ForeColor = SystemColors.Menu;
            btnAgregarProducto.Location = new Point(286, 299);
            btnAgregarProducto.Name = "btnAgregarProducto";
            btnAgregarProducto.Size = new Size(212, 48);
            btnAgregarProducto.TabIndex = 9;
            btnAgregarProducto.Text = "Agregar producto";
            btnAgregarProducto.UseVisualStyleBackColor = false;
            btnAgregarProducto.Click += btnAgregarProducto_Click;
            // 
            // grillaProductos
            // 
            grillaProductos.AllowUserToAddRows = false;
            grillaProductos.AllowUserToDeleteRows = false;
            grillaProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaProductos.Location = new Point(504, 80);
            grillaProductos.Name = "grillaProductos";
            grillaProductos.ReadOnly = true;
            grillaProductos.Size = new Size(775, 267);
            grillaProductos.TabIndex = 8;
            // 
            // btnBuscarCliente
            // 
            btnBuscarCliente.BackColor = Color.FromArgb(45, 60, 82);
            btnBuscarCliente.FlatStyle = FlatStyle.Popup;
            btnBuscarCliente.ForeColor = SystemColors.Menu;
            btnBuscarCliente.Location = new Point(286, 101);
            btnBuscarCliente.Name = "btnBuscarCliente";
            btnBuscarCliente.Size = new Size(212, 36);
            btnBuscarCliente.TabIndex = 7;
            btnBuscarCliente.Text = "Buscar cliente";
            btnBuscarCliente.UseVisualStyleBackColor = false;
            btnBuscarCliente.Click += btnBuscarCliente_Click;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(40, 109);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(206, 23);
            txtDni.TabIndex = 14;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDni.ForeColor = SystemColors.Menu;
            lblDni.Location = new Point(40, 85);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(37, 21);
            lblDni.TabIndex = 15;
            lblDni.Text = "DNI";
            // 
            // lblClienteTitulo
            // 
            lblClienteTitulo.AutoSize = true;
            lblClienteTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblClienteTitulo.ForeColor = SystemColors.Menu;
            lblClienteTitulo.Location = new Point(40, 147);
            lblClienteTitulo.Name = "lblClienteTitulo";
            lblClienteTitulo.Size = new Size(61, 21);
            lblClienteTitulo.TabIndex = 16;
            lblClienteTitulo.Text = "Cliente:";
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCliente.ForeColor = SystemColors.Menu;
            lblCliente.Location = new Point(113, 147);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(68, 21);
            lblCliente.TabIndex = 17;
            lblCliente.Text = "Nombre";
            // 
            // lblFormaPago
            // 
            lblFormaPago.AutoSize = true;
            lblFormaPago.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFormaPago.ForeColor = SystemColors.Menu;
            lblFormaPago.Location = new Point(20, 194);
            lblFormaPago.Name = "lblFormaPago";
            lblFormaPago.Size = new Size(161, 30);
            lblFormaPago.TabIndex = 18;
            lblFormaPago.Text = "Forma de pago";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Menu;
            label1.Location = new Point(40, 246);
            label1.Name = "label1";
            label1.Size = new Size(118, 21);
            label1.TabIndex = 19;
            label1.Text = "Forma de pago:";
            // 
            // cbxFormaPago
            // 
            cbxFormaPago.FormattingEnabled = true;
            cbxFormaPago.Location = new Point(40, 270);
            cbxFormaPago.Name = "cbxFormaPago";
            cbxFormaPago.Size = new Size(199, 23);
            cbxFormaPago.TabIndex = 20;
            // 
            // lblProductos
            // 
            lblProductos.AutoSize = true;
            lblProductos.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProductos.ForeColor = SystemColors.Menu;
            lblProductos.Location = new Point(504, 31);
            lblProductos.Name = "lblProductos";
            lblProductos.Size = new Size(113, 30);
            lblProductos.TabIndex = 21;
            lblProductos.Text = "Productos";
            // 
            // lblSubtotalTitulo
            // 
            lblSubtotalTitulo.AutoSize = true;
            lblSubtotalTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtotalTitulo.ForeColor = SystemColors.Menu;
            lblSubtotalTitulo.Location = new Point(811, 653);
            lblSubtotalTitulo.Name = "lblSubtotalTitulo";
            lblSubtotalTitulo.Size = new Size(68, 21);
            lblSubtotalTitulo.TabIndex = 23;
            lblSubtotalTitulo.Text = "Subtotal";
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblSubtotal.ForeColor = SystemColors.Menu;
            lblSubtotal.Location = new Point(885, 653);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(58, 21);
            lblSubtotal.TabIndex = 22;
            lblSubtotal.Text = "VALOR";
            // 
            // btnProcesarPago
            // 
            btnProcesarPago.BackColor = Color.FromArgb(45, 60, 82);
            btnProcesarPago.FlatStyle = FlatStyle.Popup;
            btnProcesarPago.ForeColor = SystemColors.Menu;
            btnProcesarPago.Location = new Point(801, 440);
            btnProcesarPago.Name = "btnProcesarPago";
            btnProcesarPago.Size = new Size(212, 48);
            btnProcesarPago.TabIndex = 24;
            btnProcesarPago.Text = "Procesar pago";
            btnProcesarPago.UseVisualStyleBackColor = false;
            // 
            // btnEmitirComprobante
            // 
            btnEmitirComprobante.BackColor = Color.FromArgb(45, 60, 82);
            btnEmitirComprobante.FlatStyle = FlatStyle.Popup;
            btnEmitirComprobante.ForeColor = SystemColors.Menu;
            btnEmitirComprobante.Location = new Point(801, 494);
            btnEmitirComprobante.Name = "btnEmitirComprobante";
            btnEmitirComprobante.Size = new Size(212, 48);
            btnEmitirComprobante.TabIndex = 25;
            btnEmitirComprobante.Text = "Emitir comprobante de venta";
            btnEmitirComprobante.UseVisualStyleBackColor = false;
            // 
            // lblDetalleVenta
            // 
            lblDetalleVenta.AutoSize = true;
            lblDetalleVenta.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDetalleVenta.ForeColor = SystemColors.Menu;
            lblDetalleVenta.Location = new Point(20, 387);
            lblDetalleVenta.Name = "lblDetalleVenta";
            lblDetalleVenta.Size = new Size(143, 30);
            lblDetalleVenta.TabIndex = 27;
            lblDetalleVenta.Text = "Detalle Venta";
            // 
            // grillaDetalleVenta
            // 
            grillaDetalleVenta.AllowUserToAddRows = false;
            grillaDetalleVenta.AllowUserToDeleteRows = false;
            grillaDetalleVenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaDetalleVenta.Location = new Point(20, 440);
            grillaDetalleVenta.Name = "grillaDetalleVenta";
            grillaDetalleVenta.ReadOnly = true;
            grillaDetalleVenta.Size = new Size(775, 267);
            grillaDetalleVenta.TabIndex = 26;
            // 
            // FrmVenta_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1303, 719);
            Controls.Add(lblDetalleVenta);
            Controls.Add(grillaDetalleVenta);
            Controls.Add(btnEmitirComprobante);
            Controls.Add(btnProcesarPago);
            Controls.Add(lblSubtotalTitulo);
            Controls.Add(lblSubtotal);
            Controls.Add(lblProductos);
            Controls.Add(cbxFormaPago);
            Controls.Add(label1);
            Controls.Add(lblFormaPago);
            Controls.Add(lblCliente);
            Controls.Add(lblClienteTitulo);
            Controls.Add(lblDni);
            Controls.Add(txtDni);
            Controls.Add(btnSalir);
            Controls.Add(lblSeccionCliente);
            Controls.Add(btnAgregarProducto);
            Controls.Add(grillaProductos);
            Controls.Add(btnBuscarCliente);
            Name = "FrmVenta_064RB";
            Text = "FrmRealizarVenta_064RB";
            Load += FrmVenta_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaProductos).EndInit();
            ((System.ComponentModel.ISupportInitialize)grillaDetalleVenta).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSalir;
        private Label lblSeccionCliente;
        private Button btnAgregarProducto;
        private DataGridView grillaProductos;
        private Button btnBuscarCliente;
        private TextBox txtDni;
        private Label lblDni;
        private Label lblClienteTitulo;
        private Label lblCliente;
        private Label lblFormaPago;
        private Label label1;
        private ComboBox cbxFormaPago;
        private Label lblProductos;
        private Label lblSubtotalTitulo;
        private Label lblSubtotal;
        private Button btnProcesarPago;
        private Button btnEmitirComprobante;
        private Label lblDetalleVenta;
        private DataGridView grillaDetalleVenta;
    }
}