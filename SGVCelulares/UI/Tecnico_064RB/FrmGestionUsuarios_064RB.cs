using BLL_064RB.Tecnico_064RB;
using Microsoft.VisualBasic;
using Servicio_064RB;
using System.Text.RegularExpressions;

namespace UI
{
    public partial class FrmGestionUsuarios_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Usuario_064RB bll_usuario;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        BLL_Rol_064RB bll_rol;
        public FrmGestionUsuarios_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            grillaUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cbxRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            grillaUsuarios.MultiSelect = false;
            bll_usuario = new BLL_Usuario_064RB();
            Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos_064RB());


            bll_rol = new BLL_Rol_064RB();
            foreach (var c in bll_rol.Consultar_064RB())
            {
                cbxRoles.Items.Add(c);
            }
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;

            btnCrearUsuario.Enabled = usuario.Rol_064RB.TienePermiso("Crear Usuario");
            btnModificarUsuario.Enabled = usuario.Rol_064RB.TienePermiso("Modificar Usuario");
            btnDesbloquearUsuario.Enabled = usuario.Rol_064RB.TienePermiso("Desbloquear Usuario");
            btnCambiarEstadoUsuario.Enabled = usuario.Rol_064RB.TienePermiso("Activar Usuario");
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas(grilla);
            ContarUsuarios(lblNumero);
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;  

            grilla.Columns["DNI"].HeaderText = bll_idioma.Traducir_064RB("FrmGestionUsuarios.colDni");
            grilla.Columns["Nombre"].HeaderText = bll_idioma.Traducir_064RB("FrmGestionUsuarios.colNombre");
            grilla.Columns["Apellido"].HeaderText = bll_idioma.Traducir_064RB("FrmGestionUsuarios.colApellido");
            grilla.Columns["Usuario"].HeaderText = bll_idioma.Traducir_064RB("FrmGestionUsuarios.colUsuario");
            grilla.Columns["Activo"].HeaderText = bll_idioma.Traducir_064RB("FrmGestionUsuarios.colActivo");
            grilla.Columns["Bloqueado"].HeaderText = bll_idioma.Traducir_064RB("FrmGestionUsuarios.colBloqueado");
            grilla.Columns["Rol"].HeaderText = bll_idioma.Traducir_064RB("FrmGestionUsuarios.colRol");
        }
        private void ContarUsuarios(Label lbl) 
        {
            int cant = bll_usuario.Consultar_064RB().Count();
            lbl.Text = $"{cant}";
        }
        private bool ValidarDatos(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDni.Text;
                if (!ValidarDatos(dni, @"^\d{8}$")) throw new Exception("El formato del DNI es incorrecto!");

                string nombre = txtNombre.Text;
                if (!ValidarDatos(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del nombre es incorrecto!");

                string apellido = txtApellido.Text;
                if (!ValidarDatos(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del apellido es incorrecto!");

                string correo = txtCorreo.Text;
                if (!ValidarDatos(correo, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) throw new Exception("El formato del correo es incorrecto!");

                if (cbxRoles.SelectedItem == null) throw new Exception("Debe seleccionar un rol!");

                SER_Rol_064RB rol = (SER_Rol_064RB)cbxRoles.SelectedItem;
                SER_Usuario_064RB usuario = new SER_Usuario_064RB(dni, nombre, apellido, correo, false, true, rol, "es-AR");
                

                bll_usuario.Agregar_064RB(usuario);
                MessageBox.Show("Usuario creado con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarTextBox();
                if (radioButton1.Checked)
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos_064RB());
                }
                else
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarParaGrilla_064RB());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesbloquearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaUsuarios.SelectedRows.Count == 0) throw new Exception("NO EXISTEN USUARIOS REGISTRADOS!");
                SER_Usuario_064RB usAux = new SER_Usuario_064RB(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                bll_usuario.Desbloquear_064RB(usAux);
                MessageBox.Show("El usuario con DNI " + usAux.Dni_064RB + " ha sido desbloqueado correctamente ", "Desbloqueo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos_064RB());
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCambiarEstadoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaUsuarios.SelectedRows.Count == 0) throw new Exception("NO EXISTEN USUARIOS REGISTRADOS!");
                SER_Usuario_064RB usAux = new SER_Usuario_064RB(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                SER_Usuario_064RB us = bll_usuario.ConsultarPorId_064RB(usAux);

                DialogResult res = MessageBox.Show($"¿Desea cambiar el estado de {us.NombreUsuario_064RB} de {(us.Activo_064RB ? "Activo" : "Inactivo")} a {(us.Activo_064RB ? "Inactivo" : "Activo")}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    bll_usuario.CambiarEstadoActivo_064RB(usAux);
                    if (radioButton1.Checked)
                    {
                        Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos_064RB());
                    }
                    else
                    {
                        Mostrar(grillaUsuarios, bll_usuario.ConsultarParaGrilla_064RB());
                    }
                }
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaUsuarios.SelectedRows.Count == 0) throw new Exception("NO EXISTEN USUARIOS REGISTRADOS");
                SER_Usuario_064RB usAux = new SER_Usuario_064RB(grillaUsuarios.SelectedRows[0].Cells["DNI"].Value.ToString());
                SER_Usuario_064RB us = bll_usuario.ConsultarPorId_064RB(usAux);
                string nombre = Interaction.InputBox("Ingrese nombre del usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Nombre"].Value.ToString());
                if (!ValidarDatos(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del nombre es incorrecto!");

                string apellido = Interaction.InputBox("Ingrese apellido del usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Apellido"].Value.ToString());
                if (!ValidarDatos(apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]{1,50}$")) throw new Exception("El formato del apellido es incorrecto!");

                string nombreUsuario = Interaction.InputBox("Ingrese nombre de usuario", "Modificar usuario", grillaUsuarios.SelectedRows[0].Cells["Usuario"].Value.ToString());
                if (!ValidarDatos(nombreUsuario, @"^[a-zA-Z0-9_]{1,50}$")) throw new Exception("El formato del nombre de usuario es incorrecto!");

                us.Nombre_064RB = nombre;
                us.Apellido_064RB = apellido;
                us.NombreUsuario_064RB = nombreUsuario;
                bll_usuario.Modificar_064RB(us);
                MessageBox.Show("El usuario con DNI " + us.Dni_064RB + " ha sido modificado correctamente ", "Modificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (radioButton1.Checked)
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarActivos_064RB());
                }
                else
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarParaGrilla_064RB());
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private void btnAplicarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                
                string dni = txtDni.Text;
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string correo = txtCorreo.Text;
                string nombreUsuario = txtNombreUsuario.Text;
                SER_Rol_064RB rol = (SER_Rol_064RB)cbxRoles.SelectedItem;
                SER_Usuario_064RB usuario = new SER_Usuario_064RB(dni, nombre, apellido, correo, nombreUsuario);
                usuario.Rol_064RB = rol;
                Mostrar(grillaUsuarios, bll_usuario.ConsultarFiltrado_064RB(usuario));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked) 
                {
                    Mostrar(grillaUsuarios,bll_usuario.ConsultarActivos_064RB());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(radioButton2.Checked)
                {
                    Mostrar(grillaUsuarios, bll_usuario.ConsultarParaGrilla_064RB());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.Form");
            lblTitulo.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.lblTitulo");
            btnCrearUsuario.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.btnCrearUsuario");
            btnDesbloquearUsuario.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.btnDesbloquearUsuario");
            btnModificarUsuario.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.btnModificarUsuario");
            btnCambiarEstadoUsuario.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.btnCambiarEstadoUsuario");
            btnAplicarCambios.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.btnAplicarCambios");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.btnSalir");
            label1.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.label1");
            lblDni.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.lblDni");
            lblNombre.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.lblNombre");
            lblApellido.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.lblApellido");
            lblCorreo.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.lblCorreo");
            lblFiltro.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.lblFiltro");
            lblNumeroUsuarios.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.lblNumeroUsuarios");
            radioButton1.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.radioButton1");
            radioButton2.Text = bll_idioma.Traducir_064RB("FrmGestionUsuarios.radioButton2");
            TraducirColumnas(grillaUsuarios);   

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_064RB(this);
            base.OnFormClosed(e);
        }
        private void LimpiarTextBox()
        {
            txtDni.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            txtNombreUsuario.Text = "";
            cbxRoles.SelectedIndex = 0;
        }
    }
}
