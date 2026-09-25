using Microsoft.VisualBasic;
using Servicio_064RB;
using BLL_064RB.Tecnico_064RB;

namespace UI
{
    public partial class FrmGestionRoles_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Permiso_064RB bll_permiso;
        BLL_Familia_064RB bll_familia;
        BLL_Rol_064RB bll_rol;
        List<SER_Componente_064RB> componentes;
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();

        public FrmGestionRoles_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmGestionRoles_Load(object sender, EventArgs e)
        {
            clbComponentes.CheckOnClick = true;
            bll_permiso = new BLL_Permiso_064RB();
            bll_familia = new BLL_Familia_064RB();
            bll_rol = new BLL_Rol_064RB();

            foreach (var control in Controls)
            {
                if (control is DataGridView grilla)
                {
                    grilla.MultiSelect = false;
                    grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            CargarDatos();

            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;
            btnBorrarRol.Enabled = usuario.Rol_064RB.TienePermiso("Eliminar Rol");
            btnBorrarFamilia.Enabled = usuario.Rol_064RB.TienePermiso("Eliminar Familia");
            AplicarPermisoCrear();

            radRol.CheckedChanged += (s, e) => AplicarPermisoCrear();
            radFamilia.CheckedChanged += (s, e) => AplicarPermisoCrear();
        }
        private void CargarDatos()
        {
            componentes = new List<SER_Componente_064RB>();
            componentes.AddRange(bll_permiso.Consultar_064RB());
            componentes.AddRange(bll_familia.Consultar_064RB());

            cbxRoles.Items.Clear();
            foreach (var rol in bll_rol.Consultar_064RB())
                cbxRoles.Items.Add(rol);

            cbxFamilias.Items.Clear();
            foreach (var c in componentes)
                if (c is SER_Familia_064RB f)
                    cbxFamilias.Items.Add(f);

            Mostrar(clbComponentes, componentes);
        }
        private void Mostrar(CheckedListBox listbox, object datos)
        {
            listbox.DataSource = null;
            listbox.DataSource = datos;
        }
        private void AplicarPermisoCrear()
        {
            SER_Usuario_064RB usuario = SER_SessionManager_064RB.ObtenerSesion_064RB().Usuario_064RB;

            if (radRol.Checked)
                btnCrear.Enabled = usuario.Rol_064RB.TienePermiso("Crear Rol");
            else
                btnCrear.Enabled = usuario.Rol_064RB.TienePermiso("Crear Familia");
        }

        private void clbComponentes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(ActualizarTreeView));
        }

        private void ActualizarTreeView()
        {
            tvNodosComposite.BeginUpdate();
            tvNodosComposite.Nodes.Clear();
            string nombreRaiz = string.IsNullOrWhiteSpace(txtNombreFamilia.Text) ? "Nuevo rol" : txtNombreFamilia.Text;
            TreeNode raiz = new TreeNode(nombreRaiz);

            foreach (SER_Componente_064RB componente in clbComponentes.CheckedItems)
            {
                AgregarNodo(raiz, componente);
            }
            tvNodosComposite.Nodes.Add(raiz);
            raiz.ExpandAll();
            tvNodosComposite.EndUpdate();
        }
        private void txtNombreFamilia_TextChanged(object sender, EventArgs e)
        {
            ActualizarTreeView();
        }
        private void AgregarNodo(TreeNode nodoPadre, SER_Componente_064RB componente)
        {
            TreeNode nodo = new TreeNode(componente.Nombre_064RB);
            nodoPadre.Nodes.Add(nodo);
            if (componente is SER_Familia_064RB familia)
            {
                foreach (SER_Componente_064RB hijo in familia.Componentes_064RB)
                {
                    AgregarNodo(nodo, hijo);
                }
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (radRol.Checked)
                {
                    if (txtNombreFamilia.Text.Length == 0) throw new Exception("El nombre del rol no puede estar vacío!");
                    SER_Rol_064RB rol = new SER_Rol_064RB(txtNombreFamilia.Text);
                    List<SER_Componente_064RB> permisos = new List<SER_Componente_064RB>();
                    foreach (SER_Componente_064RB c in clbComponentes.CheckedItems)
                    {
                        permisos.Add(c);
                    }

                    bll_rol.Agregar_064RB(rol, permisos);
                    MessageBox.Show($"El rol {rol.Nombre_064RB} se creó con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    ActualizarTreeView();
                    txtNombreFamilia.Text = "";
                }
                else
                {
                    if (txtNombreFamilia.Text.Length == 0) throw new Exception("El nombre de la familia no puede estar vacía!");
                    SER_Familia_064RB familia = new SER_Familia_064RB(txtNombreFamilia.Text);
                    List<SER_Componente_064RB> permisos = new List<SER_Componente_064RB>();
                    foreach (SER_Componente_064RB c in clbComponentes.CheckedItems)
                    {
                        permisos.Add(c);
                    }

                    bll_familia.Agregar_064RB(familia, permisos);
                    MessageBox.Show($"La familia {familia.Nombre_064RB} se creó con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    ActualizarTreeView();
                    txtNombreFamilia.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarSeleccionados_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbComponentes.Items.Count; i++)
            {
                clbComponentes.SetItemChecked(i, false);
            }
            clbComponentes.ClearSelected();
        }
        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.Form");
            label1.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.label1");
            label3.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.label3");
            label4.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.label4");
            label5.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.label5");
            label2.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.label2");
            btnCrear.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.btnCrear");
            radRol.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.radRol");
            radFamilia.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.radFamilia");
            btnEliminarSeleccionados.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.btnEliminarSeleccionados");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.btnSalir");
            label6.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.label6");
            btnBorrarRol.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.btnBorrarRol");
            btnBorrarFamilia.Text = bll_idioma.Traducir_064RB("FrmGestionRoles.btnBorrarFamilia");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            bll_idioma.Desuscribir_064RB(this);
            base.OnFormClosed(e);
        }
        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
        private void MostrarComponentesEnTreeView(string nombreRaiz, List<SER_Componente_064RB> componentes)
        {
            tvNodosComposite.BeginUpdate();
            tvNodosComposite.Nodes.Clear();

            TreeNode raiz = new TreeNode(nombreRaiz);
            foreach (SER_Componente_064RB componente in componentes)
                AgregarNodo(raiz, componente);

            tvNodosComposite.Nodes.Add(raiz);
            raiz.ExpandAll();
            tvNodosComposite.EndUpdate();
        }
        private void MostrarRolEnTreeView(SER_Rol_064RB rol) =>
            MostrarComponentesEnTreeView(rol.Nombre_064RB, rol.Componentes_064RB);
        private void cbxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxRoles.SelectedItem is SER_Rol_064RB rol)
                {
                    MostrarRolEnTreeView(rol);
                }
            }
            catch (Exception) { }
        }

        private void cbxFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFamilias.SelectedItem is SER_Familia_064RB familia)
                MostrarComponentesEnTreeView(familia.Nombre_064RB, familia.Componentes_064RB);
        }

        private void btnBorrarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxRoles.SelectedItem is not SER_Rol_064RB rol)
                    throw new Exception("Seleccioná un rol para borrar.");

                if (MessageBox.Show($"¿Seguro que querés borrar el rol '{rol.Nombre_064RB}'?",
                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                bll_rol.Borrar_064RB(rol);
                MessageBox.Show($"El rol '{rol.Nombre_064RB}' se borró con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombreFamilia.Text = "";
                tvNodosComposite.Nodes.Clear();
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxFamilias.SelectedItem is not SER_Familia_064RB familia)
                    throw new Exception("Seleccioná una familia para borrar.");

                if (MessageBox.Show($"¿Seguro que querés borrar la familia '{familia.Nombre_064RB}'?",
                        "CONFIRMAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                bll_familia.Borrar_064RB(familia);
                MessageBox.Show($"La familia '{familia.Nombre_064RB}' se borró con éxito!", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNombreFamilia.Text = "";
                tvNodosComposite.Nodes.Clear();
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
