using BLL_064RB.Tecnico_064RB;
using Servicio_064RB;

namespace UI
{
    public partial class FrmRepararInconsistencia_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_DV_064RB bll_dv;
        BLL_Backup_064RB bll_backup;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        BLL_Usuario_064RB bll_usuario;
        private List<SER_Inconsistencia_064RB> inconsistencias;

        public FrmRepararInconsistencia_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmRepararInconsistencia_Load(object sender, EventArgs e)
        {
            bll_dv = new BLL_DV_064RB();
            bll_backup = new BLL_Backup_064RB();
            bll_usuario = new BLL_Usuario_064RB();
            try
            {
                inconsistencias = bll_dv.ObtenerInconsistencias_064RB();
                grillaInconsistencias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grillaInconsistencias.MultiSelect = false;
                grillaInconsistencias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                MostrarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarGrilla()
        {
            grillaInconsistencias.AutoGenerateColumns = false;
            grillaInconsistencias.Columns.Clear();

            grillaInconsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreTabla_064RB",
                HeaderText = "Tabla",
                DataPropertyName = "NombreTabla_064RB",
            });

            grillaInconsistencias.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IdRegistro_064RB",
                HeaderText = "Id Registro",
                DataPropertyName = "IdRegistro_064RB",
            });

            grillaInconsistencias.DataSource = null; ;
            grillaInconsistencias.DataSource = inconsistencias;
            TraducirColumnas(grillaInconsistencias);
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;

            grilla.Columns["NombreTabla_064RB"].HeaderText = bll_idioma.Traducir_064RB("FrmRepararInconsistencia.colNombreTabla");
            grilla.Columns["IdRegistro_064RB"].HeaderText = bll_idioma.Traducir_064RB("FrmRepararInconsistencia.colIdRegistro");
        }
        private void btnRestaurarBackup_Click(object sender, EventArgs e)
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

        private void btnRestaurarDV_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("¿Estás seguro de que desea realcular los valores del DV?", "Confirmar operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                bll_dv.RecalcularDV_064RB();
                MessageBox.Show("Los valores del DV fueron recalculados correctamente.", "Recalcular DV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void grillaInconsistencias_SelectionChanged(object sender, EventArgs e)
        {
            if (grillaInconsistencias.SelectedRows.Count == 0)
            {
                richTextBox1.Clear();
                return;
            }

            var seleccionada = (SER_Inconsistencia_064RB)grillaInconsistencias.SelectedRows[0].DataBoundItem;
            richTextBox1.Text = seleccionada.Mensaje_064RB;
        }
        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmRepararInconsistencia.Form");
            btnRestaurarBackup.Text = bll_idioma.Traducir_064RB("FrmRepararInconsistencia.btnRestaurarBackup");
            btnRestaurarDV.Text = bll_idioma.Traducir_064RB("FrmRepararInconsistencia.btnRestaurarDV");
            lblTitulo.Text = bll_idioma.Traducir_064RB("FrmRepararInconsistencia.lblTitulo");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmRepararInconsistencia.btnSalir");
            TraducirColumnas(grillaInconsistencias);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            bll_idioma.Desuscribir_064RB(this);
            this.Close();
        }
        private void FrmRepararInconsistencia_064RB_FormClosing(object sender, FormClosingEventArgs e)
        {
            bll_usuario.Logout_064RB();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_064RB(this);
            base.OnFormClosed(e);
        }

    }
}
