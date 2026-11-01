namespace UI_064RB
{
    partial class FrmProductos_064RB
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
            btnAgregar = new Button();
            grillaProductos = new DataGridView();
            btnBorrar = new Button();
            btnModificar = new Button();
            lblProductos = new Label();
            btnSalir = new Button();
            groupBox1 = new GroupBox();
            radAccesorio = new RadioButton();
            radCelular = new RadioButton();
            nudStockMinimo = new NumericUpDown();
            lblStockMinimo = new Label();
            ((System.ComponentModel.ISupportInitialize)grillaProductos).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).BeginInit();
            SuspendLayout();
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.FromArgb(45, 60, 82);
            btnAgregar.FlatStyle = FlatStyle.Popup;
            btnAgregar.ForeColor = SystemColors.Menu;
            btnAgregar.Location = new Point(15, 334);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(95, 35);
            btnAgregar.TabIndex = 0;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // grillaProductos
            // 
            grillaProductos.AllowUserToAddRows = false;
            grillaProductos.AllowUserToDeleteRows = false;
            grillaProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grillaProductos.Location = new Point(12, 61);
            grillaProductos.Name = "grillaProductos";
            grillaProductos.ReadOnly = true;
            grillaProductos.Size = new Size(1098, 267);
            grillaProductos.TabIndex = 1;
            // 
            // btnBorrar
            // 
            btnBorrar.BackColor = Color.FromArgb(45, 60, 82);
            btnBorrar.FlatStyle = FlatStyle.Popup;
            btnBorrar.ForeColor = SystemColors.Menu;
            btnBorrar.Location = new Point(116, 334);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(95, 35);
            btnBorrar.TabIndex = 2;
            btnBorrar.Text = "Borrar";
            btnBorrar.UseVisualStyleBackColor = false;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnModificar
            // 
            btnModificar.BackColor = Color.FromArgb(45, 60, 82);
            btnModificar.FlatStyle = FlatStyle.Popup;
            btnModificar.ForeColor = SystemColors.Menu;
            btnModificar.Location = new Point(217, 334);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(95, 35);
            btnModificar.TabIndex = 3;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = false;
            btnModificar.Click += btnModificar_Click;
            // 
            // lblProductos
            // 
            lblProductos.AutoSize = true;
            lblProductos.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblProductos.ForeColor = SystemColors.Menu;
            lblProductos.Location = new Point(15, 28);
            lblProductos.Name = "lblProductos";
            lblProductos.Size = new Size(113, 30);
            lblProductos.TabIndex = 4;
            lblProductos.Text = "Productos";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(45, 60, 82);
            btnSalir.FlatStyle = FlatStyle.Popup;
            btnSalir.ForeColor = SystemColors.Menu;
            btnSalir.Location = new Point(318, 334);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(95, 35);
            btnSalir.TabIndex = 5;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radAccesorio);
            groupBox1.Controls.Add(radCelular);
            groupBox1.ForeColor = SystemColors.Menu;
            groupBox1.Location = new Point(33, 386);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(196, 70);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tipo de producto";
            // 
            // radAccesorio
            // 
            radAccesorio.AutoSize = true;
            radAccesorio.Location = new Point(101, 34);
            radAccesorio.Name = "radAccesorio";
            radAccesorio.Size = new Size(77, 19);
            radAccesorio.TabIndex = 1;
            radAccesorio.TabStop = true;
            radAccesorio.Text = "Accesorio";
            radAccesorio.UseVisualStyleBackColor = true;
            // 
            // radCelular
            // 
            radCelular.AutoSize = true;
            radCelular.Location = new Point(22, 34);
            radCelular.Name = "radCelular";
            radCelular.Size = new Size(62, 19);
            radCelular.TabIndex = 0;
            radCelular.TabStop = true;
            radCelular.Text = "Celular";
            radCelular.UseVisualStyleBackColor = true;
            // 
            // nudStockMinimo
            // 
            nudStockMinimo.Location = new Point(557, 342);
            nudStockMinimo.Name = "nudStockMinimo";
            nudStockMinimo.Size = new Size(130, 23);
            nudStockMinimo.TabIndex = 7;
            // 
            // lblStockMinimo
            // 
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Font = new Font("Segoe UI", 9F);
            lblStockMinimo.ForeColor = SystemColors.Menu;
            lblStockMinimo.Location = new Point(467, 344);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(84, 15);
            lblStockMinimo.TabIndex = 16;
            lblStockMinimo.Text = "Stock mínimo:";
            // 
            // FrmProductos_064RB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 42, 58);
            ClientSize = new Size(1122, 500);
            Controls.Add(lblStockMinimo);
            Controls.Add(nudStockMinimo);
            Controls.Add(groupBox1);
            Controls.Add(btnSalir);
            Controls.Add(lblProductos);
            Controls.Add(btnModificar);
            Controls.Add(btnBorrar);
            Controls.Add(grillaProductos);
            Controls.Add(btnAgregar);
            Name = "FrmProductos_064RB";
            Text = "FrmProductos_064RB";
            Load += FrmProductos_064RB_Load;
            ((System.ComponentModel.ISupportInitialize)grillaProductos).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAgregar;
        private DataGridView grillaProductos;
        private Button btnBorrar;
        private Button btnModificar;
        private Label lblProductos;
        private Button btnSalir;
        private GroupBox groupBox1;
        private RadioButton radAccesorio;
        private RadioButton radCelular;
        private NumericUpDown nudStockMinimo;
        private Label lblStockMinimo;
    }
}