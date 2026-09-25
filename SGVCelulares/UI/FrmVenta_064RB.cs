using BE_064RB;
using BLL_064RB.Negocio_064RB;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_064RB
{
    public partial class FrmVenta_064RB : Form
    {
        BLL_Cliente_064RB bll_cliente;
        BLL_Producto_064RB bll_productos;
        public FrmVenta_064RB()
        {
            InitializeComponent();
        }

        private void FrmVenta_064RB_Load(object sender, EventArgs e)
        {
            bll_productos = new();
            bll_cliente = new();
            grillaProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaProductos.MultiSelect = false;
            grillaProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cbxFormaPago.Items.Add("Efectivo");
            cbxFormaPago.Items.Add("Tarjeta");
            cbxFormaPago.Items.Add("Transferencia");
            Mostrar(grillaProductos, bll_productos.ConsultarDisponibles());
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDni.Text;
                if (dni.Length != 8) throw new Exception("El DNI no cumple con el formato de 8 caracteres!");
                BE_Cliente_064RB cliente = bll_cliente.ConsultarPorDni_064RB(dni);
                if (cliente == null) lblCliente.Text = "Cliente no encontrado!";
                lblCliente.Text = $"{cliente.Nombre_064RB} {cliente.Apellido_064RB}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                string sku = grillaProductos.SelectedRows[0].Cells[0].Value.ToString();
                string tipo = grillaProductos.SelectedRows[0].Cells[5].Value.ToString();
                BE_Producto_064RB producto;
                if (tipo == "Celular")
                {
                    producto = bll_productos.ConsultarCelularPorSku(sku);
                }
                else if(tipo == "Accesorio")
                {
                    producto = bll_productos.ConsultarAccesorioPorSku_064RB(sku);
                }
                string cantidadInput = Interaction.InputBox("Ingresa la cantidad", "Agregar producto al detalle");
                if (!int.TryParse(cantidadInput, out int cantidad)) throw new Exception("La cantidad debe ser numérica!");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
