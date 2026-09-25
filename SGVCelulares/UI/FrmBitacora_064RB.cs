using QuestPDF.Fluent;
using System.Data;
using System.Diagnostics;
using Servicio_064RB;
using BLL_064RB.Tecnico_064RB;

namespace UI
{
    public partial class FrmBitacora_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Bitacora_064RB bll_bitacora;
        BLL_Usuario_064RB bll_usuario;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();

        private readonly Dictionary<string, string[]> eventosPorModulo =
        new Dictionary<string, string[]>
        {
            {
                "Usuarios",
                new[]
                {
                    "Login",
                    "Cambiar clave",
                    "Logout",
                    "Crear usuario",
                    "Desbloquear usuario",
                    "Modificar usuario",
                    "Activar usuario",
                    "Desactivar usuario",
                    "Cambiar idioma"
                }
            },
            {
                "Roles",
                new[]
                {
                    "Crear rol",
                    "Modificar rol",
                    "Borrar rol",
                    "Crear familia",
                    "Borrar familia"
                }
            },
            {
                "Bitácora",
                new[]
                {
                    "Auditar bitácora"
                }
            }

        };

        public FrmBitacora_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            bll_bitacora = new BLL_Bitacora_064RB();
            bll_usuario = new BLL_Usuario_064RB();
            grillaBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaBitacora.MultiSelect = false;
            grillaBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtpFechaInicio.MaxDate = DateTime.Today;
            dtpFechaFin.MaxDate = DateTime.Today;

            cbxCriticidad.Items.AddRange(new string[] { "Alta", "Media", "Baja" });
            MostrarTodosLosEventos();
            txtNombre.ReadOnly = true;
            txtApellido.ReadOnly = true;
            Mostrar(grillaBitacora, bll_bitacora.ConsultarParaGrilla3Dias_064RB());
            ActualizarTxt();
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            btnImprimirBitacora.Enabled = usuario.Rol_064RB.TienePermiso("Exportar Bitácora");
        }

        private void MostrarTodosLosEventos()
        {
            cbxModulo.Items.AddRange(new string[] { "Usuarios", "Roles", "Bitácora" });
            cbxEvento.Items.AddRange(new string[] {
                "Login", "Cambiar clave", "Logout", "Crear usuario", "Desbloquear usuario", "Modificar usuario", "Activar Usuario", "Desactivar Usuario", "Cambiar idioma",
                "Crear rol", "Modificar rol", "Borrar rol", "Crear familia", "Borrar familia",
                "Auditar bitácora"
            });
        }

        private void ActualizarTxt()
        {
            try
            {
                if (grillaBitacora.SelectedRows.Count == 0)
                    return;

                if (grillaBitacora.SelectedRows[0].Cells["Usuario"].Value == null)
                    return;

                string login = grillaBitacora.SelectedRows[0].Cells["Usuario"].Value.ToString();

                SER_Usuario_064RB us = bll_bitacora.ConsultarPorNombreUsuario_064RB(login);

                if (us == null)
                    return;

                txtNombre.Text = us.Nombre_064RB;
                txtApellido.Text = us.Apellido_064RB;
            }
            catch
            {

            }
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas(grilla);
        }

        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;

            grilla.Columns["Usuario"].HeaderText = bll_idioma.Traducir_064RB("FrmBitacora.colUsuario");
            grilla.Columns["Fecha"].HeaderText = bll_idioma.Traducir_064RB("FrmBitacora.colFecha");
            grilla.Columns["Hora"].HeaderText = bll_idioma.Traducir_064RB("FrmBitacora.colHora");
            grilla.Columns["Modulo"].HeaderText = bll_idioma.Traducir_064RB("FrmBitacora.colModulo");
            grilla.Columns["Evento"].HeaderText = bll_idioma.Traducir_064RB("FrmBitacora.colEvento");
            grilla.Columns["Criticidad"].HeaderText = bll_idioma.Traducir_064RB("FrmBitacora.colCriticidad");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtNombreUsuario.Text;
                string modulo = cbxModulo.SelectedItem?.ToString();
                string evento = cbxEvento.SelectedItem?.ToString();
                int? criticidad = cbxCriticidad.SelectedIndex >= 0 ? (int?)cbxCriticidad.SelectedIndex + 1 : null;
                DateTime? fechaInicio = dtpFechaInicio.Value.Date;
                DateTime? fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddTicks(-1);
                if (fechaInicio > fechaFin) throw new Exception("La fecha de inicio no puede ser mayor a la fecha de fin!!");
                Mostrar(grillaBitacora, bll_bitacora.ConsultarFiltradoBitacora_064RB(nombreUsuario, modulo, evento, criticidad, fechaInicio, fechaFin));
                if (grillaBitacora.Rows.Count == 0)
                {
                    txtApellido.Text = "";
                    txtNombre.Text = "";
                }
                else
                {
                    ActualizarTxt();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtNombreUsuario.Clear();
                cbxCriticidad.SelectedIndex = -1;
                cbxEvento.SelectedIndex = -1;
                cbxModulo.SelectedIndex = -1;
                dtpFechaFin.Value = DateTime.Today;
                dtpFechaInicio.Value = DateTime.Today;
                Mostrar(grillaBitacora, bll_bitacora.ConsultarParaGrilla3Dias_064RB());
                cbxModulo.Items.Clear(); cbxEvento.Items.Clear();
                MostrarTodosLosEventos();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void grillaBitacora_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grillaBitacora.Rows.Count > 0) ActualizarTxt();
            }
            catch (Exception) { }
        }

        private void cbxModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxEvento.Items.Clear();
            if (cbxModulo.SelectedItem != null && eventosPorModulo.ContainsKey(cbxModulo.SelectedItem.ToString()))
            {
                cbxEvento.Items.AddRange(eventosPorModulo[cbxModulo.SelectedItem.ToString()]);
            }

            cbxEvento.SelectedIndex = -1;
        }

        private void btnImprimirBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaBitacora.Rows.Count == 0) throw new Exception("No hay registros para imprimir!");

                var columnasVisibles = grillaBitacora.Columns
                                       .Cast<DataGridViewColumn>()
                                       .Where(c => c.Visible)
                                       .ToList();
                var filas = grillaBitacora.Rows
                            .Cast<DataGridViewRow>()
                            .Where(f => !f.IsNewRow)
                            .ToList();

                using SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF (*.pdf)|*.pdf";
                saveDialog.FileName = $"Bitacora_{DateTime.Now:ddMMyyyy_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    GuardarPdf(columnasVisibles, filas, saveDialog);
                    AbrirPdf(saveDialog.FileName);
                    MessageBox.Show("PDF generado correctamente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GuardarPdf(List<DataGridViewColumn> columnasVisibles, List<DataGridViewRow> filas, SaveFileDialog saveDialog)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Header()
                        .Text("Reporte de Bitácora")
                        .FontSize(18)
                        .Bold();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            foreach (var columna in columnasVisibles)
                                columns.RelativeColumn();
                        });
                        table.Header(header =>
                        {
                            foreach (var columna in columnasVisibles)
                            {
                                header.Cell()
                                    .Border(1)
                                    .Padding(5)
                                    .Text(columna.HeaderText)
                                    .Bold();
                            }
                        });
                        foreach (var fila in filas)
                        {
                            foreach (var columna in columnasVisibles)
                            {
                                string valor =
                                    fila.Cells[columna.Index].Value?.ToString() ?? "";

                                table.Cell()
                                    .Border(1)
                                    .Padding(5)
                                    .Text(valor);
                            }
                        }
                    });
                });
            }).GeneratePdf(saveDialog.FileName);
        }
        private void AbrirPdf(string ruta)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = ruta,
                UseShellExecute = true
            });
        }
        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmBitacora.Form");
            lblTitulo.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblTitulo");
            lblNombre.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblNombre");
            lblApellido.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblApellido");
            lblLogin.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblLogin");
            lblModulo.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblModulo");
            lblFechaInicio.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblFechaInicio");
            lblEvento.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblEvento");
            lblFechaFin.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblFechaFin");
            lblCriticidad.Text = bll_idioma.Traducir_064RB("FrmBitacora.lblCriticidad");
            btnImprimirBitacora.Text = bll_idioma.Traducir_064RB("FrmBitacora.btnImprimirBitacora");
            btnAplicar.Text = bll_idioma.Traducir_064RB("FrmBitacora.btnAplicar");
            btnLimpiar.Text = bll_idioma.Traducir_064RB("FrmBitacora.btnLimpiar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmBitacora.btnSalir");
            TraducirColumnas(grillaBitacora);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_064RB(this);
            base.OnFormClosed(e);
        }
    }
}
