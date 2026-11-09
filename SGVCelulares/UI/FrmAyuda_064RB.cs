using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI_064RB
{
    public partial class FrmAyuda_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Idioma_064RB bll_idioma = new();
        public FrmAyuda_064RB()
        {
            InitializeComponent();
            this.Shown += FrmAyuda_064RB_Shown;
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }
        private void FrmAyuda_064RB_Load(object sender, EventArgs e)
        {
            ActualizarIdioma_064RB();
            var usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            bool tienePermisoNegocio = usuario.Rol_064RB.TienePermiso("Ver FAQ de negocio");
            bool tienePermisoAdmin = usuario.Rol_064RB.TienePermiso("Ver FAQ de administradores");
            txtFaqNegocio.Visible = tienePermisoNegocio;
            txtFaqAdmin.Visible = tienePermisoAdmin;
            if (!tienePermisoNegocio && tienePermisoAdmin)
            {
                txtFaqAdmin.Location = new Point(13, 68);
            }
            else
            {
                txtFaqNegocio.Location = new Point(13, 68);
                txtFaqAdmin.Location = new Point(13, 306);
            }
        }
        public void ActualizarIdioma_064RB()
        {
            lblFaq.Text = bll_idioma.Traducir_064RB("FrmAyuda.lblFaq");
            txtFaqNegocio.Text = bll_idioma.Traducir_064RB("FrmAyuda.textoFaqNegocio").Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
            txtFaqAdmin.Text = bll_idioma.Traducir_064RB("FrmAyuda.textoFaqAdmin").Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmAyuda.btnSalir");
            btnSalir.Focus();
        }

        private void FrmAyuda_064RB_Shown(object sender, EventArgs e)
        {
            txtFaqNegocio.SelectionLength = 0;
            txtFaqAdmin.SelectionLength = 0;
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
