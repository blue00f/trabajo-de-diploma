using BE_064RB;
using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Microsoft.VisualBasic;
using Servicio_064RB;

namespace UI_064RB
{
    public partial class FrmProductos_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        BLL_Producto_064RB bll_producto;
        public FrmProductos_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmProductos_064RB_Load(object sender, EventArgs e)
        {
            grillaProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaProductos.MultiSelect = false;
            grillaProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bll_producto = new();
            Mostrar(grillaProductos, bll_producto.ConsultarGrilla_064RB());
            radCelular.Checked = true;

            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;

            btnAgregar.Enabled = usuario.Rol_064RB.TienePermiso("Crear Producto");
            btnBorrar.Enabled = usuario.Rol_064RB.TienePermiso("Borrar Producto");
            btnModificar.Enabled = usuario.Rol_064RB.TienePermiso("Modificar Producto");
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas(grillaProductos);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (radCelular.Checked)
                {
                    string sku = Interaction.InputBox("Ingresa el SKU:", "Alta de celular");
                    if (sku.Length == 0) throw new Exception("El SKU está vacío!");

                    string marca = Interaction.InputBox("Ingresa la marca:", "Alta de celular");
                    if (marca.Length == 0) throw new Exception("La marca está vacía!");

                    string modelo = Interaction.InputBox("Ingresa el modelo:", "Alta de celular");
                    if (modelo.Length == 0) throw new Exception("El modelo está vacío!");

                    string precioInput = Interaction.InputBox("Ingresa el precio:", "Alta de celular");
                    if (!decimal.TryParse(precioInput, out decimal precio)) throw new Exception("El precio ingresado es incorrecto!");

                    string descripcion = Interaction.InputBox("Ingresa la descripcion:", "Alta de celular");
                    if (descripcion.Length == 0) throw new Exception("La descripción está vacía!");

                    string almacenamiento = Interaction.InputBox("Ingresa el almacenamiento:", "Alta de celular");
                    if (almacenamiento.Length == 0) throw new Exception("La descripción está vacía!");

                    string color = Interaction.InputBox("Ingresa el color:", "Alta de celular");
                    if (color.Length == 0) throw new Exception("El color está vacío!");

                    BE_Celular_064RB celular = new(sku, marca, modelo, precio, descripcion, almacenamiento, color);
                    bll_producto.AgregarCelular_064RB(celular);
                    Mostrar(grillaProductos, bll_producto.ConsultarGrilla_064RB());
                }
                else
                {
                    string sku = Interaction.InputBox("Ingresa el SKU:", "Alta de accesorio");
                    if (sku.Length == 0) throw new Exception("El SKU está vacío!");

                    string marca = Interaction.InputBox("Ingresa la marca:", "Alta de accesorio");
                    if (marca.Length == 0) throw new Exception("La marca está vacía!");

                    string modelo = Interaction.InputBox("Ingresa el modelo:", "Alta de accesorio");
                    if (modelo.Length == 0) throw new Exception("El modelo está vacío!");

                    string precioInput = Interaction.InputBox("Ingresa el precio:", "Alta de accesorio");
                    if (!decimal.TryParse(precioInput, out decimal precio)) throw new Exception("El precio ingresado es incorrecto!");

                    string descripcion = Interaction.InputBox("Ingresa la descripcion:", "Alta de accesorio");
                    if (descripcion.Length == 0) throw new Exception("La descripción está vacía!");

                    string categoria = Interaction.InputBox("Ingresa la categoría:", "Alta de accesorio");
                    if (categoria.Length == 0) throw new Exception("La categoría está vacía!");

                    string stockInput = Interaction.InputBox("Ingresa el stock:", "Alta de accesorio");
                    if (!int.TryParse(stockInput, out int stock)) throw new Exception("El stock debe ser numérico!");

                    BE_Accesorio_064RB accesorio = new BE_Accesorio_064RB(sku, marca, modelo, precio, descripcion, categoria, stock);
                    bll_producto.AgregarAccesorio_064RB(accesorio);
                    Mostrar(grillaProductos, bll_producto.ConsultarGrilla_064RB());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaProductos.Rows.Count == 0) throw new Exception("No hay productos para borrar!");
                string sku = grillaProductos.CurrentRow.Cells[0].Value.ToString();
                bll_producto.Borrar_064RB(sku);
                Mostrar(grillaProductos, bll_producto.ConsultarGrilla_064RB());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaProductos.Rows.Count == 0) throw new Exception("No hay productos para modificar!");
                string sku = grillaProductos.CurrentRow.Cells[0].Value.ToString();
                string tipo = grillaProductos.CurrentRow.Cells[5].Value.ToString();

                string marca = Interaction.InputBox("Ingrese la marca", "Modificar producto", grillaProductos.CurrentRow.Cells[1].Value.ToString());
                if (marca.Length == 0) throw new Exception("La marca está vacía!");

                string modelo = Interaction.InputBox("Ingrese el modelo", "Modificar producto", grillaProductos.CurrentRow.Cells[2].Value.ToString());
                if (modelo.Length == 0) throw new Exception("El modelo está vacío!");

                string precioInput = Interaction.InputBox("Ingresa el precio:", "Modificar producto", grillaProductos.CurrentRow.Cells[3].Value.ToString());
                if (!decimal.TryParse(precioInput, out decimal precio)) throw new Exception("El precio ingresado es incorrecto!");

                string descripcion = Interaction.InputBox("Ingresa la descripción:", "Modificar producto", grillaProductos.CurrentRow.Cells[4].Value.ToString());
                if (descripcion.Length == 0) throw new Exception("La descripción está vacía!");

                BE_Producto_064RB producto;

                if(tipo == "Celular")
                {
                    string almacenamiento = Interaction.InputBox("Ingresa el almacenamiento:", "Modificar celular", grillaProductos.CurrentRow.Cells[6].Value.ToString());
                    if (almacenamiento.Length == 0) throw new Exception("El almacenamiento está vacío!");
                    string color = Interaction.InputBox("Ingresa el color:", "Modificar celular", grillaProductos.CurrentRow.Cells[7].Value.ToString());
                    if (color.Length == 0) throw new Exception("El color está vacío!");
                    producto = new BE_Celular_064RB(sku, marca, modelo, precio, descripcion, almacenamiento, color);
                }
                else
                {
                    string categoria = Interaction.InputBox("Ingresa la categoría:", "Modificar accesorio", grillaProductos.CurrentRow.Cells[8].Value.ToString());
                    if (categoria.Length == 0) throw new Exception("La categoría está vacía!");
                    string stockInput = Interaction.InputBox("Ingresa el stock:", "Modificar accesorio");
                    if (!int.TryParse(stockInput, out int stock)) throw new Exception("El stock debe ser numérico!");
                    producto = new BE_Accesorio_064RB(sku, marca, modelo, precio, descripcion, categoria, stock);
                }

                bll_producto.Modificar_064RB(producto);
                Mostrar(grillaProductos, bll_producto.ConsultarGrilla_064RB());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmProductos.Form");
            lblProductos.Text = bll_idioma.Traducir_064RB("FrmProductos.lblProductos");
            btnAgregar.Text = bll_idioma.Traducir_064RB("FrmProductos.btnAgregar");
            btnBorrar.Text = bll_idioma.Traducir_064RB("FrmProductos.btnBorrar");
            btnModificar.Text = bll_idioma.Traducir_064RB("FrmProductos.btnModificar");
            groupBox1.Text = bll_idioma.Traducir_064RB("FrmProductos.groupBox1");
            radAccesorio.Text = bll_idioma.Traducir_064RB("FrmProductos.radAccesorio");
            radCelular.Text = bll_idioma.Traducir_064RB("FrmProductos.radCelular");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmProductos.btnSalir");
            TraducirColumnas(grillaProductos);
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns["SKU"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colSku");
            grilla.Columns["Marca"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colMarca");
            grilla.Columns["Modelo"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colModelo");
            grilla.Columns["Precio"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colPrecio");
            grilla.Columns["Descripcion"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colDescripcion");
            grilla.Columns["Tipo"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colTipo");
            grilla.Columns["Almacenamiento"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colAlmacenamiento");
            grilla.Columns["Color"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colColor");
            grilla.Columns["Categoria"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colCategoria");
            grilla.Columns["Stock"].HeaderText = bll_idioma.Traducir_064RB("FrmProductos.colStock");
        }
    }
}
