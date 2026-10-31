namespace UI_064RB
{
    partial class FrmRecibirMercaderia_064RB
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
            txtNumRemito = new TextBox();
            btnSalir = new Button();
            lblOrdenes = new Label();
            btnCancelar = new Button();
            btnRegistrarRecepcion = new Button();
            grillaOrdenes = new DataGridView();
            btnRegistrarDiscrepancia = new Button();
            lblProducto = new Label();
            lblCantidadPedida = new Label();
            lblDiscrepancia = new Label();
            lblCantidadConforme = new Label();
            lblNumRemito = new Label();
            lblDatosRecepcion = new Label();
            lblImei = new Label();
            lblImeis = new Label();
            txtImei = new TextBox();
            btnAgregarImei = new Button();
            lstImeis = new ListBox();
            btnQuitarImei = new Button();
            ((System.ComponentModel.ISupportInitialize)grillaOrdenes).BeginInit();
            SuspendLayout();
            // 
            // txtNumRemito
            // 
            txtNumRemito.Location = new Point(27, 400);
            txtNumRemito.Name = "txtNumRemito";
            txtNumRemito.Size = new Size(196, 23);
            txtNumRemito.TabIndex = 26;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(351, 317);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(156, 35);
            btnSalir.TabIndex = 25;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblOrdenes
            // 
            lblOrdenes.AutoSize = true;
            lblOrdenes.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOrdenes.ForeColor = SystemColors.Menu;
            lblOrdenes.Location = new Point(27, 11);
            lblOrdenes.Name = "lblOrdenes";
            lblOrdenes.Size = new Size(94, 30);
            lblOrdenes.TabIndex = 24;
            lblOrdenes.Text = "Órdenes";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(45, 60, 82);
            btnCancelar.FlatStyle = FlatStyle.Popup;
            btnCancelar.ForeColor = SystemColors.Menu;
            btnCancelar.Location = new Point(189, 317);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(156, 35);
            btnCancelar.TabIndex = 23;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnRegistrarRecepcion
            // 
            btnRegistrarRecepcion.BackColor = Color.FromArgb(45, 60, 82);
            btnRegistrarRecepcion.FlatStyle = FlatStyle.Popup;
            btnRegistrarRecepcion.ForeColor = SystemColors.Menu;
            btnRegistrarRecepcion.Location = new Point(27, 317);
            btnRegistrarRecepcion.Name = "btnRegistrarRecepcion";
            btnRegistrarRecepcion.Size = new Size(156, 35);
            btnRegistrarRecepcion.TabIndex = 22;
            btnRegistrarRecepcion.Text = "Registrar recepción";
            btnRegistrarRecepcion.UseVisualStyleBackColor = false;
            btnRegistrarRecepcion.Click += btnRegistrarRecepcion_Click;
            // 
            // grillaOrdenes
            // 
            grillaOrdenes.AllowUserToAddRows = false;
            grillaOrdenes.AllowUserToDeleteRows = false;
            grillaOrdenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaOrdenes.Location = new Point(24, 44);
            grillaOrdenes.Name = "grillaOrdenes";
            grillaOrdenes.ReadOnly = true;
            grillaOrdenes.Size = new Size(869, 267);
            grillaOrdenes.TabIndex = 21;
            grillaOrdenes.SelectionChanged += grillaOrdenes_SelectionChanged;
            // 
            // btnRegistrarDiscrepancia
            // 
            btnRegistrarDiscrepancia.BackColor = Color.FromArgb(45, 60, 82);
            btnRegistrarDiscrepancia.FlatStyle = FlatStyle.Popup;
            btnRegistrarDiscrepancia.ForeColor = SystemColors.Menu;
            btnRegistrarDiscrepancia.Location = new Point(899, 202);
            btnRegistrarDiscrepancia.Name = "btnRegistrarDiscrepancia";
            btnRegistrarDiscrepancia.Size = new Size(200, 35);
            btnRegistrarDiscrepancia.TabIndex = 20;
            btnRegistrarDiscrepancia.Text = "Registrar discrepancia";
            btnRegistrarDiscrepancia.UseVisualStyleBackColor = false;
            btnRegistrarDiscrepancia.Click += btnRegistrarDiscrepancia_Click;
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI", 9F);
            lblProducto.ForeColor = SystemColors.Menu;
            lblProducto.Location = new Point(899, 68);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(56, 15);
            lblProducto.TabIndex = 29;
            lblProducto.Text = "Producto";
            // 
            // lblCantidadPedida
            // 
            lblCantidadPedida.AutoSize = true;
            lblCantidadPedida.Font = new Font("Segoe UI", 9F);
            lblCantidadPedida.ForeColor = SystemColors.Menu;
            lblCantidadPedida.Location = new Point(899, 101);
            lblCantidadPedida.Name = "lblCantidadPedida";
            lblCantidadPedida.Size = new Size(94, 15);
            lblCantidadPedida.TabIndex = 30;
            lblCantidadPedida.Text = "Cantidad pedida";
            // 
            // lblDiscrepancia
            // 
            lblDiscrepancia.AutoSize = true;
            lblDiscrepancia.Font = new Font("Segoe UI", 9F);
            lblDiscrepancia.ForeColor = SystemColors.Menu;
            lblDiscrepancia.Location = new Point(899, 172);
            lblDiscrepancia.Name = "lblDiscrepancia";
            lblDiscrepancia.Size = new Size(74, 15);
            lblDiscrepancia.TabIndex = 32;
            lblDiscrepancia.Text = "Discrepancia";
            // 
            // lblCantidadConforme
            // 
            lblCantidadConforme.AutoSize = true;
            lblCantidadConforme.Font = new Font("Segoe UI", 9F);
            lblCantidadConforme.ForeColor = SystemColors.Menu;
            lblCantidadConforme.Location = new Point(899, 139);
            lblCantidadConforme.Name = "lblCantidadConforme";
            lblCantidadConforme.Size = new Size(110, 15);
            lblCantidadConforme.TabIndex = 31;
            lblCantidadConforme.Text = "Cantidad conforme";
            // 
            // lblNumRemito
            // 
            lblNumRemito.AutoSize = true;
            lblNumRemito.Font = new Font("Segoe UI", 9F);
            lblNumRemito.ForeColor = SystemColors.Menu;
            lblNumRemito.Location = new Point(27, 382);
            lblNumRemito.Name = "lblNumRemito";
            lblNumRemito.Size = new Size(105, 15);
            lblNumRemito.TabIndex = 33;
            lblNumRemito.Text = "Número de remito";
            // 
            // lblDatosRecepcion
            // 
            lblDatosRecepcion.AutoSize = true;
            lblDatosRecepcion.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDatosRecepcion.ForeColor = SystemColors.Menu;
            lblDatosRecepcion.Location = new Point(899, 10);
            lblDatosRecepcion.Name = "lblDatosRecepcion";
            lblDatosRecepcion.Size = new Size(200, 30);
            lblDatosRecepcion.TabIndex = 34;
            lblDatosRecepcion.Text = "Datos de recepción";
            // 
            // lblImei
            // 
            lblImei.AutoSize = true;
            lblImei.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblImei.ForeColor = SystemColors.Menu;
            lblImei.Location = new Point(1118, 10);
            lblImei.Name = "lblImei";
            lblImei.Size = new Size(58, 30);
            lblImei.TabIndex = 35;
            lblImei.Text = "IMEI";
            // 
            // lblImeis
            // 
            lblImeis.AutoSize = true;
            lblImeis.Font = new Font("Segoe UI", 9F);
            lblImeis.ForeColor = SystemColors.Menu;
            lblImeis.Location = new Point(1118, 60);
            lblImeis.Name = "lblImeis";
            lblImeis.Size = new Size(105, 15);
            lblImeis.TabIndex = 37;
            lblImeis.Text = "Número de remito";
            // 
            // txtImei
            // 
            txtImei.Location = new Point(1118, 78);
            txtImei.Name = "txtImei";
            txtImei.Size = new Size(216, 23);
            txtImei.TabIndex = 36;
            txtImei.KeyDown += txtImei_KeyDown;
            // 
            // btnAgregarImei
            // 
            btnAgregarImei.BackColor = Color.FromArgb(45, 60, 82);
            btnAgregarImei.FlatStyle = FlatStyle.Popup;
            btnAgregarImei.ForeColor = SystemColors.Menu;
            btnAgregarImei.Location = new Point(1118, 119);
            btnAgregarImei.Name = "btnAgregarImei";
            btnAgregarImei.Size = new Size(105, 35);
            btnAgregarImei.TabIndex = 38;
            btnAgregarImei.Text = "Agregar IMEI";
            btnAgregarImei.UseVisualStyleBackColor = false;
            btnAgregarImei.Click += btnAgregarImei_Click;
            // 
            // lstImeis
            // 
            lstImeis.FormattingEnabled = true;
            lstImeis.ItemHeight = 15;
            lstImeis.Location = new Point(1118, 172);
            lstImeis.Name = "lstImeis";
            lstImeis.Size = new Size(216, 139);
            lstImeis.TabIndex = 39;
            // 
            // btnQuitarImei
            // 
            btnQuitarImei.BackColor = Color.FromArgb(45, 60, 82);
            btnQuitarImei.FlatStyle = FlatStyle.Popup;
            btnQuitarImei.ForeColor = SystemColors.Menu;
            btnQuitarImei.Location = new Point(1229, 119);
            btnQuitarImei.Name = "btnQuitarImei";
            btnQuitarImei.Size = new Size(105, 35);
            btnQuitarImei.TabIndex = 40;
            btnQuitarImei.Text = "Quitar IMEI";
            btnQuitarImei.UseVisualStyleBackColor = false;
            btnQuitarImei.Click += btnQuitarImei_Click;
            // 
            // FrmRecibirMercaderia_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1354, 435);
            Controls.Add(btnQuitarImei);
            Controls.Add(lstImeis);
            Controls.Add(btnAgregarImei);
            Controls.Add(lblImeis);
            Controls.Add(txtImei);
            Controls.Add(lblImei);
            Controls.Add(lblDatosRecepcion);
            Controls.Add(lblNumRemito);
            Controls.Add(lblDiscrepancia);
            Controls.Add(lblCantidadConforme);
            Controls.Add(lblCantidadPedida);
            Controls.Add(lblProducto);
            Controls.Add(txtNumRemito);
            Controls.Add(btnSalir);
            Controls.Add(lblOrdenes);
            Controls.Add(btnCancelar);
            Controls.Add(btnRegistrarRecepcion);
            Controls.Add(grillaOrdenes);
            Controls.Add(btnRegistrarDiscrepancia);
            Name = "FrmRecibirMercaderia_064RB";
            Text = "Recepción de mercaderia";
            Load += FrmRecibirMercaderia_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaOrdenes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtNumRemito;
        private Button btnSalir;
        private Label lblOrdenes;
        private Button btnCancelar;
        private Button btnRegistrarRecepcion;
        private DataGridView grillaOrdenes;
        private Button btnRegistrarDiscrepancia;
        private Label lblProducto;
        private Label lblCantidadPedida;
        private Label lblDiscrepancia;
        private Label lblCantidadConforme;
        private Label lblNumRemito;
        private Label lblDatosRecepcion;
        private Label lblImei;
        private Label lblImeis;
        private TextBox txtImei;
        private Button btnAgregarImei;
        private ListBox lstImeis;
        private Button btnQuitarImei;
    }
}