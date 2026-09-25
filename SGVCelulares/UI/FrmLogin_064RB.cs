using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Text.RegularExpressions;

namespace UI
{
    public partial class FrmLogin_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Usuario_064RB bll_usuario;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        bool cargandoIdiomas;
        string nombreUsuarioRestringido = null;
        public FrmLogin_064RB()
        {
            InitializeComponent();
            this.AcceptButton = btnEntrar;
            bll_idioma.Suscribir_064RB(this);
            CargarIdiomas();
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            bll_usuario = new BLL_Usuario_064RB();
        }
        public void RestringirProximoLoginA(string nombreUsuario)
        {
            this.nombreUsuarioRestringido = nombreUsuario;
        }
        private bool ValidarDatos(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtNombreUsuario.Text;
                if (!ValidarDatos(nombreUsuario, @"^[a-zA-Z0-9_]{1,50}$")) throw new Exception("Datos erróneos!");

                string password = txtPassword.Text;
                if (password.Length == 0) throw new Exception("Datos erróneos!");

                if (nombreUsuarioRestringido != null && nombreUsuario != nombreUsuarioRestringido) throw new Exception("Solo puede reingresar con el mismo usuario de la sesión activa!");
                bool esRelogin = nombreUsuarioRestringido != null;

                string mensajeInconsistencia;
                if (bll_usuario.Login_064RB(new SER_Usuario_064RB(nombreUsuario, password), out mensajeInconsistencia, esRelogin))
                {
                    nombreUsuarioRestringido = null;
                    if (!string.IsNullOrEmpty(mensajeInconsistencia))
                    {
                        cboIdioma.SelectedValue = bll_idioma.IdiomaActual;
                        MessageBox.Show(mensajeInconsistencia, "Inconsistencia detectada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNombreUsuario.Text = "";
                        txtPassword.Text = "";
                        this.Hide();
                        FrmRepararInconsistencia_064RB frmRepararInconsistencia = new FrmRepararInconsistencia_064RB();
                        frmRepararInconsistencia.ShowDialog();
                        this.Show();
                        cboIdioma.SelectedValue = bll_idioma.IdiomaActual;
                    }
                    else 
                    {
                        txtNombreUsuario.Text = "";
                        txtPassword.Text = "";
                        this.Hide();
                        FrmMenu_064RB frmMenu = new FrmMenu_064RB();
                        frmMenu.ShowDialog(this);
                        this.Show();
                        cargandoIdiomas = true;
                        cboIdioma.SelectedValue = bll_idioma.IdiomaActual;
                        cargandoIdiomas = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarIdiomas()
        {
            cargandoIdiomas = true;
            cboIdioma.DisplayMember = "Nombre_064RB";   
            cboIdioma.ValueMember = "Codigo_064RB";   
            cboIdioma.DataSource = bll_idioma.ObtenerIdiomasDisponibles_064RB();
            cboIdioma.SelectedValue = bll_idioma.IdiomaActual;
            cargandoIdiomas = false;
        }

        private void cboIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoIdiomas) return;

            try
            {
                string codigo = cboIdioma.SelectedValue.ToString();
                bll_idioma.CambiarIdioma_064RB(codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmLogin.Form");
            lblTitulo.Text = bll_idioma.Traducir_064RB("FrmLogin.lblTitulo");
            lblUsuario.Text = bll_idioma.Traducir_064RB("FrmLogin.lblUsuario");
            lblPassword.Text = bll_idioma.Traducir_064RB("FrmLogin.lblPassword");
            btnEntrar.Text = bll_idioma.Traducir_064RB("FrmLogin.btnEntrar");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_064RB(this);
            base.OnFormClosed(e);
        }
    }
}
