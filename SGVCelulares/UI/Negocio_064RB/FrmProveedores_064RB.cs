using BE_064RB;
using BLL_064RB.Negocio_064RB;
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
    public partial class FrmProveedores_064RB : Form
    {
        BLL_Proveedor_064RB bll_proveedores;
        public FrmProveedores_064RB()
        {
            InitializeComponent();
        }

        private void FrmProveedores_064RB_Load(object sender, EventArgs e)
        {
            grillaProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaProveedores.MultiSelect = false;
            grillaProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            bll_proveedores = new();
            CargarListado();
        }
        private void CargarListado()
        {
            grillaProveedores.DataSource = null;
            grillaProveedores.DataSource = bll_proveedores.ConsultarGrilla_064RB();
        }
        private void Limpiar()
        {
            txtCuit.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCuit.Text.Length != 11) throw new Exception("El formato del CUIT debe ser de 11 números sin guiones!");
                if (txtRazonSocial.Text.Length == 0) throw new Exception("La razón social no puede estar vacía!");
                BE_Proveedor_064RB proveedor = new(txtCuit.Text, txtRazonSocial.Text);

                bll_proveedores.Agregar_064RB(proveedor);
                CargarListado();
                Limpiar();
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
                if (grillaProveedores.Rows.Count == 0) throw new Exception("No hay proveedores para borrar");
                string cuit = grillaProveedores.CurrentRow.Cells[0].Value.ToString();
                BE_Proveedor_064RB proveedor = new(cuit);
                bll_proveedores.Borrar_064RB(proveedor);
                CargarListado();
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
                if (grillaProveedores.Rows.Count == 0) throw new Exception("No hay proveedores para borrar");
                if (txtRazonSocial.Text.Length == 0) throw new Exception("La razón social no puede estar vacía!");
                BE_Proveedor_064RB proveedor = new(grillaProveedores.CurrentRow.Cells[0].Value.ToString(), txtRazonSocial.Text);
                bll_proveedores.Modificar_064RB(proveedor);
                CargarListado();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
