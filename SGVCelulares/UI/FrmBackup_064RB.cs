using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI
{
    public partial class FrmBackup_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Backup_064RB bll_backup;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        public bool RestoreRealizado { get; private set; } = false;
        public FrmBackup_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }
        private void FrmBackup_Load(object sender, EventArgs e)
        {
            bll_backup = new();
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            btnBackup.Enabled = usuario.Rol_064RB.TienePermiso("Realizar Backup");
            btnRestore.Enabled = usuario.Rol_064RB.TienePermiso("Realizar Restore");
        }
        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Backup SQL Server (*.bak)|*.bak";
                saveDialog.DefaultExt = "bak";
                saveDialog.AddExtension = true;
                saveDialog.FileName = $"bkp_sgvcelulares_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = saveDialog.FileName;
                    bll_backup.Backup_064RB(ruta);
                    MessageBox.Show(bll_idioma.Traducir_064RB("FrmBackup.msgOperacionExitosaBackup"), bll_idioma.Traducir_064RB("FrmBackup.msgInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Backup SQL Server (*.bak)|*.bak";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = openDialog.FileName;
                    string nombreArchivo = Path.GetFileName(ruta);
                    DialogResult rdo = MessageBox.Show(bll_idioma.Traducir_064RB("FrmBackup.msgConfirmarRestore"), bll_idioma.Traducir_064RB("FrmBackup.msgConfirmarOperacion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (rdo == DialogResult.Yes)
                    {
                        Cursor = Cursors.WaitCursor;
                        bll_backup.Restore_064RB(ruta);
                        Cursor = Cursors.Default;
                        MessageBox.Show(bll_idioma.Traducir_064RB("FrmBackup.msgOperacionExitosaRestore"), bll_idioma.Traducir_064RB("FrmBackup.msgInformacion"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RestoreRealizado = true;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(bll_idioma.Traducir_064RB("FrmBackup.msgError"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmBackup.Form");
            btnBackup.Text = bll_idioma.Traducir_064RB("FrmBackup.btnBackup");
            btnRestore.Text = bll_idioma.Traducir_064RB("FrmBackup.btnRestore");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmBackup.btnSalir");

        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}
