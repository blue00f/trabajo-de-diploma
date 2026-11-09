using BE_064RB;
using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI_064RB
{
    public partial class FrmRegistrarDiscrepancia_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Discrepancia_064RB bll_discrepancia;
        BE_RemitoCompra_064RB remito;
        BE_Discrepancia_064RB? discrepanciaActual;
        BLL_Idioma_064RB bll_idioma = new();
        public BE_Discrepancia_064RB? DiscrepanciaCargada_064RB { get; private set; }
        public FrmRegistrarDiscrepancia_064RB(BE_RemitoCompra_064RB remito, BE_Discrepancia_064RB? discrepanciaActual)
        {
            InitializeComponent();
            this.remito = remito;
            this.discrepanciaActual = discrepanciaActual;
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmRegistrarDiscrepancia_064RB_Load(object sender, EventArgs e)
        {
            btnAceptar.Enabled = TienePermiso("Registrar Discrepancia");
            bll_discrepancia = new();
            lblCantidadPedida.Text = $"{bll_idioma.Traducir_064RB("FrmRegistrarDiscrepancia.lblCantidadPedida")}: {remito.OrdenCompra_064RB.SolicitudCompra_064RB.Cantidad_064RB}";
            if (discrepanciaActual != null)
            {
                txtCantidadFaltante.Text = discrepanciaActual.CantidadFaltante_064RB.ToString();
                txtCantidadDanada.Text = discrepanciaActual.CantidadDanada_064RB.ToString();
                txtObservaciones.Text = discrepanciaActual.Observaciones_064RB;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                BE_Discrepancia_064RB discrepancia = bll_discrepancia.Registrar_064RB(remito, txtCantidadFaltante.Text, txtCantidadDanada.Text, txtObservaciones.Text);
                int conforme = bll_discrepancia.CalcularCantidadConforme_064RB(remito, discrepancia);

                DiscrepanciaCargada_064RB = discrepancia;
                MessageBox.Show("Discrepancia cargada. Cantidad recibida en condiciones conformes: " + conforme + ".", "Registrar discrepancia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
        public bool TienePermiso(string nombrePermiso)
        {
            foreach (SER_Componente_064RB hijo in SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB.Rol_064RB.Componentes_064RB)
            {
                if (hijo.TienePermiso(nombrePermiso))
                    return true;
            }
            return false;
        }

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmRegistrarDiscrepancia.Form");
            lblCantidadFaltante.Text = bll_idioma.Traducir_064RB("FrmRegistrarDiscrepancia.lblCantidadFaltante");
            lblCantidadDanada.Text = bll_idioma.Traducir_064RB("FrmRegistrarDiscrepancia.lblCantidadDanada");
            lblObservaciones.Text = bll_idioma.Traducir_064RB("FrmRegistrarDiscrepancia.lblObservaciones");
            btnAceptar.Text = bll_idioma.Traducir_064RB("FrmRegistrarDiscrepancia.btnAceptar");
            btnCancelar.Text = bll_idioma.Traducir_064RB("FrmRegistrarDiscrepancia.btnCancelar");

        }
    }
}
