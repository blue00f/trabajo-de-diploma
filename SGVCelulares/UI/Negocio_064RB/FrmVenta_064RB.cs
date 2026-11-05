using BE_064RB;
using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Microsoft.VisualBasic;
using Servicio_064RB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_064RB
{
    public partial class FrmVenta_064RB : Form, IObservadorIdioma_064RB
    {
        int ultimaVentaRegistrada = 0;
        BLL_Idioma_064RB bll_idioma = new();
        BLL_Comprobante_064RB bll_comprobante;

        BLL_Producto_064RB bll_productos;
        BLL_Venta_064RB bll_venta;
        BLL_DetalleVenta_064RB bll_detalle;
        BE_Venta_064RB venta;
        public FrmVenta_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmVenta_064RB_Load(object sender, EventArgs e)
        {
            bll_comprobante = new();

            bll_productos = new();
            bll_venta = new();
            bll_detalle = new();
            venta = bll_venta.Iniciar_064RB();
            btnEmitirComprobante.Enabled = false;

            foreach (var control in Controls)
            {
                if (control is DataGridView grilla)
                {
                    grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grilla.MultiSelect = false;
                    grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; ;
                }
            }

            btnProcesarPago.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Procesar Pago");
            btnEmitirComprobante.Enabled = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.TienePermiso("Emitir Comprobante de Venta");
            cbxFormaPago.Items.AddRange(BLL_Venta_064RB.FormasPago);
            cbxTipo.Items.Add("Celular");
            cbxTipo.Items.Add("Accesorio");
            NuevaVenta();
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirGrillaProducto(grillaProductos);
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDni.Text.Length != 8) throw new Exception("El DNI no cumple con el formato de 8 caracteres!");
                var cliente = bll_venta.AsociarCliente_064RB(venta, txtDni.Text);
                lblCliente.Text = $"{cliente.Nombre_064RB} {cliente.Apellido_064RB}";
                lblSubtotal.Text = venta.MontoTotal_064RB.ToString("C2");
            }
            catch (Exception ex)
            {
                lblCliente.Text = "-";
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaProductos.Rows.Count == 0) throw new Exception("No hay productos en la grilla!");
                string sku = grillaProductos.SelectedRows[0].Cells[0].Value.ToString();
                var producto = bll_productos.ConsultarPorSku_064RB(sku);
                bll_venta.AgregarProducto_064RB(venta, producto, (int)nudCantidad.Value);
                RefrescarDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaDetalleVenta.Rows.Count == 0) throw new Exception("No hay detalle venta en la grilla!");
                string sku = grillaDetalleVenta.SelectedRows[0].Cells[0].Value.ToString();
                bll_venta.QuitarProducto_064RB(venta, sku);
                RefrescarDetalle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefrescarDetalle()
        {
            grillaDetalleVenta.DataSource = null;
            grillaDetalleVenta.DataSource = bll_detalle.ConsultarGrilla_064RB(venta.Detalle_064RB);
            lblSubtotal.Text = venta.MontoTotal_064RB.ToString("C2");
            TraducirGrillaDetalle(grillaDetalleVenta);
        }
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal? precioMax = null;
                if (!string.IsNullOrWhiteSpace(txtPrecioMax.Text))
                {
                    if (!decimal.TryParse(txtPrecioMax.Text, out decimal precioConvertido)) throw new Exception("El precio debe ser numérico!");
                    if (precioConvertido <= 0) throw new Exception("El precio no puede ser cero o negativo!");
                    precioMax = precioConvertido;
                }
                string tipoSeleccionado = cbxTipo.SelectedIndex != -1 ? cbxTipo.SelectedItem.ToString() : null;

                Mostrar(grillaProductos, bll_productos.Buscar_064RB(txtSku.Text, txtMarca.Text, txtModelo.Text, precioMax, tipoSeleccionado, txtAlmacenamiento.Text, txtColor.Text, txtCategoria.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnProcesarPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(venta.FormaPago_064RB))
                {
                    string? forma = cbxFormaPago.SelectedItem?.ToString();
                    bool aprobado = bll_venta.ProcesarPago_064RB(venta, forma, monto => MessageBox.Show($"Cobre {monto:C2} en el POS con {forma}.\n¿La entidad bancaria aprobó la operación?", "POS - Entidad bancaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                    if (!aprobado)
                    {
                        MessageBox.Show("La entidad bancaria rechazó el pago con la forma de pago seleccionada.", "Pago rechazado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                MessageBox.Show("Pago aprobado.", "Procesar pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int numVenta = bll_venta.RegistrarVenta_064RB(venta);
                ultimaVentaRegistrada = numVenta;
                MessageBox.Show($"Venta N° {numVenta} registrada.", "Registro de venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NuevaVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ActualizarControles();
            }
        }

        private void ActualizarControles()
        {
            bool pagado = !string.IsNullOrWhiteSpace(venta.FormaPago_064RB);
            btnAgregarProducto.Enabled = !pagado;
            btnQuitarProducto.Enabled = !pagado;
            nudCantidad.Enabled = !pagado;
            btnBuscarCliente.Enabled = !pagado;
            txtDni.Enabled = !pagado;
            cbxFormaPago.Enabled = !pagado;
            btnProcesarPago.Enabled = true;
            btnCancelar.Enabled = !pagado;
            btnEmitirComprobante.Enabled = ultimaVentaRegistrada != 0;
            lblUltimaVenta.Text = ultimaVentaRegistrada != 0 ? $"{ultimaVentaRegistrada}" : "";
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            venta = bll_venta.Iniciar_064RB();
            grillaDetalleVenta.DataSource = null;
            lblSubtotal.Text = 0m.ToString("C2");
            lblCliente.Text = "-";
            txtDni.Clear();
            cbxFormaPago.SelectedIndex = -1;
            nudCantidad.Value = nudCantidad.Minimum;
            Mostrar(grillaProductos, bll_productos.Buscar_064RB());
            ActualizarControles();
        }
        private void NuevaVenta()
        {
            venta = bll_venta.Iniciar_064RB();
            grillaDetalleVenta.DataSource = null;
            lblSubtotal.Text = 0m.ToString("C2");
            txtDni.Clear();
            lblCliente.Text = "-";
            cbxFormaPago.SelectedIndex = -1;
            nudCantidad.Value = nudCantidad.Minimum;
            txtSku.Clear(); txtMarca.Clear(); txtModelo.Clear();
            txtColor.Clear(); txtAlmacenamiento.Clear(); txtCategoria.Clear();
            txtPrecioMax.Clear();
            cbxTipo.SelectedIndex = -1;
            Mostrar(grillaProductos, bll_productos.ConsultarDisponibles_064RB());
            ActualizarControles();
        }
        private void btnEmitirComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] pdf = bll_comprobante.Generar_064RB(ultimaVentaRegistrada);
                string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"comprobante_{ultimaVentaRegistrada}_{DateTime.Now:HHmmss}.pdf");
                File.WriteAllBytes(ruta, pdf);
                Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmVenta.Form");
            lblProductos.Text = bll_idioma.Traducir_064RB("FrmVenta.lblProductos");
            lblSku.Text = bll_idioma.Traducir_064RB("FrmVenta.lblSku");
            lblMarca.Text = bll_idioma.Traducir_064RB("FrmVenta.lblMarca");
            lblModelo.Text = bll_idioma.Traducir_064RB("FrmVenta.lblModelo");
            lblColor.Text = bll_idioma.Traducir_064RB("FrmVenta.lblColor");
            lblAlmacenamiento.Text = bll_idioma.Traducir_064RB("FrmVenta.lblAlmacenamiento");
            lblCategoria.Text = bll_idioma.Traducir_064RB("FrmVenta.lblCategoria");
            lblPrecioMax.Text = bll_idioma.Traducir_064RB("FrmVenta.lblPrecioMax");
            lblTipo.Text = bll_idioma.Traducir_064RB("FrmVenta.lblTipo");
            btnFiltrar.Text = bll_idioma.Traducir_064RB("FrmVenta.btnFiltrar");
            btnAgregarProducto.Text = bll_idioma.Traducir_064RB("FrmVenta.btnAgregarProducto");
            btnQuitarProducto.Text = bll_idioma.Traducir_064RB("FrmVenta.btnQuitarProducto");
            lblDetalleVenta.Text = bll_idioma.Traducir_064RB("FrmVenta.lblDetalleVenta");
            btnProcesarPago.Text = bll_idioma.Traducir_064RB("FrmVenta.btnProcesarPago");
            btnEmitirComprobante.Text = bll_idioma.Traducir_064RB("FrmVenta.btnEmitirComprobante");
            btnCancelar.Text = bll_idioma.Traducir_064RB("FrmVenta.btnCancelar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmVenta.btnSalir");
            lblSubtotalTitulo.Text = bll_idioma.Traducir_064RB("FrmVenta.lblSubtotalTitulo");
            lblSeccionCliente.Text = bll_idioma.Traducir_064RB("FrmVenta.lblSeccionCliente");
            lblDni.Text = bll_idioma.Traducir_064RB("FrmVenta.lblDni");
            lblClienteTitulo.Text = bll_idioma.Traducir_064RB("FrmVenta.lblClienteTitulo");
            lblFormaPago.Text = bll_idioma.Traducir_064RB("FrmVenta.lblFormaPago");
            lblUltimaVentaTexto.Text = bll_idioma.Traducir_064RB("FrmVenta.lblUltimaVentaTexto");
            btnBuscarCliente.Text = bll_idioma.Traducir_064RB("FrmVenta.btnBuscar");
        }
        private void TraducirGrillaProducto(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns["SKU"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colSku");
            grilla.Columns["Marca"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colMarca");
            grilla.Columns["Modelo"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colModelo");
            grilla.Columns["Precio"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colPrecio");
            grilla.Columns["Descripcion"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colDescripcion");
            grilla.Columns["Tipo"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colTipo");
            grilla.Columns["Almacenamiento"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colAlmacenamiento");
            grilla.Columns["Color"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colColor");
            grilla.Columns["Categoria"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colCategoria");
            grilla.Columns["Stock"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colStock");
            grilla.Columns["StockMinimo"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colStockMinimo");
        }
        private void TraducirGrillaDetalle(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns["SKU"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colSkuDetalle");
            grilla.Columns["Marca"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colMarcaDetalle");
            grilla.Columns["Modelo"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colModeloDetalle");
            grilla.Columns["Cantidad"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colCantidadDetalle");
            grilla.Columns["PrecioUnitario"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colPrecioUnitarioDetalle");
            grilla.Columns["Subtotal"].HeaderText = bll_idioma.Traducir_064RB("FrmVenta.colSubtotalDetalle");
        }
    }
}
