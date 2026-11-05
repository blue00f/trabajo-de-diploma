using BE_064RB;
using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Microsoft.VisualBasic;
using Servicio_064RB;
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
    public partial class FrmUnidadCelular_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_UnidadCelular_064RB bll_unidadCelular;
        BLL_Producto_064RB bll_producto;
        BLL_Idioma_064RB bll_idioma = new();
        public FrmUnidadCelular_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmUnidadCelular_064RB_Load(object sender, EventArgs e)
        {
            grillaUnidadCelular.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaUnidadCelular.MultiSelect = false;
            grillaUnidadCelular.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bll_unidadCelular = new();
            bll_producto = new();


            btnAgregar.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Crear Unidad Celular");
            btnBorrar.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Borrar Unidad Celular");
            btnModificar.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Modificar Unidad Celular");

            cbxCelulares.DataSource = bll_producto.ConsultarCelulares_064RB();
            cbxCelulares.DisplayMember = "TextoVisual";
            cbxCelulares.ValueMember = "SKU";
            Mostrar(grillaUnidadCelular, bll_unidadCelular.ConsultarGrilla_064RB());
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas(grillaUnidadCelular);
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
                 
                if (!ValidarDatos(txtImei.Text, @"^\d{15}$")) throw new Exception("El IMEI debe ser un número de 15 dígitos. Por ejemplo: 111111223333334!");

                BE_Celular_064RB celular = bll_producto.ConsultarCelularPorSku_064RB(sku);
                BE_UnidadCelular_064RB unidadCelular = new BE_UnidadCelular_064RB(txtImei.Text, "Disponible", celular);
                bll_unidadCelular.Agregar_064RB(unidadCelular);
                Mostrar(grillaUnidadCelular, bll_unidadCelular.ConsultarGrilla_064RB());
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
                Mostrar(grillaUnidadCelular, bll_unidadCelular.ConsultarGrilla_064RB());
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
                BE_Celular_064RB celularAAsignar = bll_producto.ConsultarCelularPorSku_064RB(cbxCelulares.SelectedValue.ToString());

                BE_UnidadCelular_064RB unidadNueva = new BE_UnidadCelular_064RB(unidadVieja.Imei_064RB, unidadVieja.Estado_064RB, celularAAsignar);

                bll_unidadCelular.Modificar_064RB(unidadNueva);
                Mostrar(grillaUnidadCelular, bll_unidadCelular.ConsultarGrilla_064RB());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmUnidadCelular.Form");
            lblUnidadCelular.Text = bll_idioma.Traducir_064RB("FrmUnidadCelular.lblUnidadCelular");
            btnAgregar.Text = bll_idioma.Traducir_064RB("FrmUnidadCelular.btnAgregar");
            btnBorrar.Text = bll_idioma.Traducir_064RB("FrmUnidadCelular.btnBorrar");
            btnModificar.Text = bll_idioma.Traducir_064RB("FrmUnidadCelular.btnModificar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmUnidadCelular.btnSalir");
            lblImei.Text = bll_idioma.Traducir_064RB("FrmUnidadCelular.lblImei");
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns[0].HeaderText = bll_idioma.Traducir_064RB("FrmUnidadCelular.colImei");
            grilla.Columns[1].HeaderText = bll_idioma.Traducir_064RB("FrmUnidadCelular.colEstado");
            grilla.Columns[2].HeaderText = bll_idioma.Traducir_064RB("FrmUnidadCelular.colSku");
            grilla.Columns[3].HeaderText = bll_idioma.Traducir_064RB("FrmUnidadCelular.colMarca");
            grilla.Columns[4].HeaderText = bll_idioma.Traducir_064RB("FrmUnidadCelular.colModelo");
            grilla.Columns[5].HeaderText = bll_idioma.Traducir_064RB("FrmUnidadCelular.colPrecio");
        }
    }
}
