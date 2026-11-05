using BE_064RB;
using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;
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
    public partial class FrmRecibirMercaderia_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_OrdenCompra_064RB bll_orden;
        BLL_RemitoCompra_064RB bll_remito;
        BLL_Discrepancia_064RB bll_discrepancia;
        BE_RemitoCompra_064RB remito;
        BE_Discrepancia_064RB discrepancia;
        BLL_Idioma_064RB bll_idioma = new();
        List<string> imeis;
        bool esCelular;
        int cantidadConforme;
        public FrmRecibirMercaderia_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmRecibirMercaderia_064RB_Load(object sender, EventArgs e)
        {
            bll_orden = new();
            bll_remito = new();
            bll_discrepancia = new();
            imeis = new();

            grillaOrdenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaOrdenes.MultiSelect = false;
            grillaOrdenes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            btnRegistrarRecepcion.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Recibir Mercadería");
            btnRegistrarDiscrepancia.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Registrar Discrepancia");

            NuevaRecepcion();
        }
        private void NuevaRecepcion()
        {
            grillaOrdenes.DataSource = null;
            grillaOrdenes.DataSource = bll_orden.ConsultarPendientesDeRecepcionGrilla_064RB();
            TraducirColumnas(grillaOrdenes);
            SeleccionarOrden();
        }
        private void grillaOrdenes_SelectionChanged(object sender, EventArgs e) => SeleccionarOrden();
        private void SeleccionarOrden()
        {
            remito = null;
            discrepancia = null;
            imeis.Clear();
            esCelular = false;
            txtNumRemito.Clear();
            txtImei.Clear();

            if (grillaOrdenes.CurrentRow != null)
            {
                int numOrden = Convert.ToInt32(grillaOrdenes.CurrentRow.Cells[0].Value);
                BE_OrdenCompra_064RB? orden = bll_orden.ObtenerPendientePorNumero_064RB(numOrden);
                if (orden != null)
                {
                    remito = new BE_RemitoCompra_064RB();
                    remito.OrdenCompra_064RB = orden;
                    esCelular = orden.SolicitudCompra_064RB.Producto_064RB is BE_Celular_064RB;
                }
            }
            ActualizarPantalla();
        }
        private void ActualizarPantalla()
        {
            bool hayRecepcion = remito != null;
            cantidadConforme = 0;

            if (remito != null)
            {
                BE_SolicitudCompra_064RB solicitud = remito.OrdenCompra_064RB.SolicitudCompra_064RB;
                cantidadConforme = bll_discrepancia.CalcularCantidadConforme_064RB(remito, discrepancia);

                lblProducto.Text = solicitud.Producto_064RB.Marca_064RB + " " + solicitud.Producto_064RB.Modelo_064RB;
                lblCantidadPedida.Text = solicitud.Cantidad_064RB.ToString();
                lblCantidadConforme.Text = cantidadConforme.ToString();
                if (discrepancia == null) lblDiscrepancia.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.mensajeSinDiscrepancia");
                else lblDiscrepancia.Text = $"{discrepancia.CantidadFaltante_064RB.ToString()} {bll_idioma.Traducir_064RB("FrmRecibirMercaderia.mensajeFaltantes")}, {discrepancia.CantidadDanada_064RB.ToString()} {bll_idioma.Traducir_064RB("FrmRecibirMercaderia.mensajeDanadas")}";
            }

            else
            {
                lblProducto.Text = "";
                lblCantidadPedida.Text = "";
                lblCantidadConforme.Text = "";
                lblDiscrepancia.Text = "";
            }

            lstImeis.Items.Clear();
            foreach (string imei in imeis) lstImeis.Items.Add(imei);
            if (esCelular)
            {
                lblImeis.Text = $"{bll_idioma.Traducir_064RB("FrmRecibirMercaderia.mensaje1")} {imeis.Count} {bll_idioma.Traducir_064RB("FrmRecibirMercaderia.mensaje2")} {cantidadConforme}";
            }
            else
            {
                lblImeis.Text = hayRecepcion ? bll_idioma.Traducir_064RB("FrmRecibirMercaderia.mensaje3") : "";
            }

            txtNumRemito.Enabled = hayRecepcion;
            btnRegistrarRecepcion.Enabled = hayRecepcion;
            btnRegistrarRecepcion.Enabled = hayRecepcion;
            txtImei.Enabled = esCelular;
            btnAgregarImei.Enabled = esCelular;
            btnQuitarImei.Enabled = esCelular;
        }

        private void btnRegistrarDiscrepancia_Click(object sender, EventArgs e)
        {
            if (remito == null) return;
            FrmRegistrarDiscrepancia_064RB frm = new FrmRegistrarDiscrepancia_064RB(remito, discrepancia);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                discrepancia = frm.DiscrepanciaCargada_064RB;
                imeis.Clear();
                ActualizarPantalla();
            }
        }

        private void btnAgregarImei_Click(object sender, EventArgs e)
        {
            try
            {
                string imei = txtImei.Text.Trim();
                bll_remito.ValidarImeiIngresado_064RB(imei, imeis, cantidadConforme);
                imeis.Add(imei);
                txtImei.Clear();
                txtImei.Focus();
                ActualizarPantalla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtImei_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAgregarImei_Click(sender, e);
            }
        }

        private void btnQuitarImei_Click(object sender, EventArgs e)
        {
            if (lstImeis.SelectedItem == null)
            {
                MessageBox.Show("Seleccione el IMEI que desea quitar.", "Recibir mercadería");
                return;
            }
            imeis.Remove(lstImeis.SelectedItem.ToString());
            ActualizarPantalla();
        }

        private void btnRegistrarRecepcion_Click(object sender, EventArgs e)
        {
            try
            {
                if (remito == null) throw new Exception("Seleccione una solicitud del listado.");
                bll_remito.Registrar_064RB(remito, txtNumRemito.Text, discrepancia, imeis);
                MessageBox.Show("Recepción de mercadería registrada.", "Recibir mercadería", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NuevaRecepcion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e) => NuevaRecepcion();
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.Form");
            lblOrdenes.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblOrdenes");
            btnRegistrarDiscrepancia.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.btnRegistrarDiscrepancia");
            btnCancelar.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.btnCancelar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.btnSalir");
            lblNumRemito.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblNumRemito");
            lblDatosRecepcion.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblDatosRecepcion");
            lblProductoTexto.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblProductoTexto");
            lblCantidadPedidaTexto.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblCantidadPedidaTexto");
            lblCantidadConformeTexto.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblCantidadConformeTexto");
            lblDiscrepanciaTexto.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblDiscrepanciaTexto");
            btnRegistrarDiscrepancia.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.btnRegistrarDiscrepancia");
            lblImei.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblImei");
            lblImeis.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.lblImeis");
            btnAgregarImei.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.btnAgregarImei");
            btnQuitarImei.Text = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.btnQuitarImei");
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns[0].HeaderText = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.colOrden");
            grilla.Columns[1].HeaderText = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.colSku");
            grilla.Columns[2].HeaderText = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.colMarca");
            grilla.Columns[3].HeaderText = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.colModelo");
            grilla.Columns[4].HeaderText = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.colTipo");
            grilla.Columns[5].HeaderText = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.colCantidad");
            grilla.Columns[6].HeaderText = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.colProveedor");
            grilla.Columns[7].HeaderText = bll_idioma.Traducir_064RB("FrmRecibirMercaderia.colFecha");
        }
    }
}
