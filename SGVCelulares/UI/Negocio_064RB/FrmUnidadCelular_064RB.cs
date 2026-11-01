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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_064RB
{
    public partial class FrmUnidadCelular_064RB : Form
    {
        BLL_UnidadCelular_064RB bll_unidadCelular;
        BLL_Producto_064RB bll_producto;
        public FrmUnidadCelular_064RB()
        {
            InitializeComponent();
        }

        private void FrmUnidadCelular_064RB_Load(object sender, EventArgs e)
        {
            grillaUnidadCelular.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaUnidadCelular.MultiSelect = false;
            grillaUnidadCelular.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bll_unidadCelular = new();
            bll_producto = new();

            cbxCelulares.DataSource = bll_producto.ConsultarCelulares_064RB();
            cbxCelulares.DisplayMember = "TextoVisual";
            cbxCelulares.ValueMember = "SKU";
            Mostrar(grillaUnidadCelular, bll_unidadCelular.ConsultarGrilla());
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
        }
        private bool ValidarDatos(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxCelulares.Items.Count == 0) throw new Exception("No hay celulares");
                string sku = cbxCelulares.SelectedValue.ToString();

                string imei = Interaction.InputBox("Ingrese el IMEI numérico AA-BBBBBB-CCCCCC-D (ingresar sin guiones)", "Alta unidad celular");
                if (!ValidarDatos(imei, @"^\d{15}$")) throw new Exception("El formato del IMEI es inválido!");

                BE_Celular_064RB celular = bll_producto.ConsultarCelularPorSku_064RB(sku);
                BE_UnidadCelular_064RB unidadCelular = new BE_UnidadCelular_064RB(imei, "Disponible", celular);
                bll_unidadCelular.Agregar_064RB(unidadCelular);
                Mostrar(grillaUnidadCelular, bll_unidadCelular.ConsultarGrilla());
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
                if (grillaUnidadCelular.Rows.Count == 0) throw new Exception("No hay unidades celulares para borrar!");
                string imei = grillaUnidadCelular.SelectedRows[0].Cells[0].Value.ToString();
                BE_UnidadCelular_064RB unidadCelular = new BE_UnidadCelular_064RB(imei);

                bll_unidadCelular.Borrar_064RB(unidadCelular);
                Mostrar(grillaUnidadCelular, bll_unidadCelular.ConsultarGrilla());
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
                if (grillaUnidadCelular.Rows.Count == 0) throw new Exception("No hay unidades celulares para modificar!");
                BE_UnidadCelular_064RB unidadVieja = new BE_UnidadCelular_064RB(
                    grillaUnidadCelular.SelectedRows[0].Cells[0].Value.ToString(),
                    grillaUnidadCelular.SelectedRows[0].Cells[1].Value.ToString(),
                    bll_producto.ConsultarCelularPorSku_064RB(grillaUnidadCelular.SelectedRows[0].Cells[2].Value.ToString())
                    );
                string estado = Interaction.InputBox("Ingrese el estado:", "Modificar unidad celular", unidadVieja.Estado_064RB);
                BE_Celular_064RB celularAAsignar = bll_producto.ConsultarCelularPorSku_064RB(cbxCelulares.SelectedValue.ToString());

                BE_UnidadCelular_064RB unidadNueva = new BE_UnidadCelular_064RB(unidadVieja.Imei_064RB, estado, celularAAsignar);

                bll_unidadCelular.Modificar_064RB(unidadNueva);
                Mostrar(grillaUnidadCelular, bll_unidadCelular.ConsultarGrilla());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
