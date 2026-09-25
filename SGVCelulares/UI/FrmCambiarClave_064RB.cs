using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;
using System.Data;
using System.Text.RegularExpressions;


namespace UI
{
    public partial class FrmCambiarClave_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Usuario_064RB bll_usuario;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        public FrmCambiarClave_064RB()
        {
            InitializeComponent();
            this.AcceptButton = btnConfirmar;
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }
        private void frmCambiarClave_Load(object sender, EventArgs e)
        {
            bll_usuario = new BLL_Usuario_064RB();
        }
        private bool ValidarDatos(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                string claveActual = txtClaveActual.Text;
                if (claveActual.Length == 0) throw new Exception("La clave actual está vacía!");

                string claveNueva = txtClaveNueva.Text;
                if (!ValidarDatos(claveNueva, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,50}$")) throw new Exception("El password debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial!");

                bll_usuario.ModificarPassword_064RB(claveActual, claveNueva);
                MessageBox.Show("Se modifico la contraseña con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var formularios = Application.OpenForms
                                  .Cast<Form>()
                                  .Where(f => f.Name != "FrmLogin")
                                  .ToList();
                
                bll_usuario.Logout_064RB();
                foreach (var form in formularios)
                {
                    form.Close();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("frmCambiarClave.Form");
            lblTitulo.Text = bll_idioma.Traducir_064RB("frmCambiarClave.lblTitulo");
            lblClaveActual.Text = bll_idioma.Traducir_064RB("frmCambiarClave.lblClaveActual");
            lblClaveNueva.Text = bll_idioma.Traducir_064RB("frmCambiarClave.lblClaveNueva");
            btnConfirmar.Text = bll_idioma.Traducir_064RB("frmCambiarClave.btnConfirmar");
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_064RB(this);
            base.OnFormClosed(e);
        }
    }
}
