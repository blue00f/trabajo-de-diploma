using BE_064RB;
using BLL_064RB.Negocio_064RB;
using BLL_064RB.Tecnico_064RB;
using Microsoft.VisualBasic;
using Servicio_064RB;
using System.Text.RegularExpressions;

namespace UI_064RB
{
    public partial class FrmClientes_064RB : Form, IObservadorIdioma_064RB
    {
        BLL_Idioma_064RB bll_idioma = new BLL_Idioma_064RB();
        BLL_Cliente_064RB bll_cliente;
        public FrmClientes_064RB()
        {
            InitializeComponent();
            bll_idioma.Suscribir_064RB(this);
            ActualizarIdioma_064RB();
        }

        private void FrmClientes_064RB_Load(object sender, EventArgs e)
        {
            bll_cliente = new();
            grillaClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grillaClientes.MultiSelect = false;
            grillaClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Mostrar(grillaClientes, bll_cliente.Consultar());
        }
        private void Mostrar(DataGridView grilla, object datos)
        {
            grilla.DataSource = null;
            grilla.DataSource = datos;
            TraducirColumnas(grilla);
        }
        private bool ValidarDatos(string texto, string expresionRegular)
        {
            Regex re = new Regex(expresionRegular);
            return re.IsMatch(texto);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = Interaction.InputBox("Ingrese DNI", "Alta cliente");
                if (dni.Length != 8) throw new Exception("El DNI tiene que tener 8 caracteres!");

                string nombre = Interaction.InputBox("Ingrese nombre", "Alta cliente");
                if (nombre.Length == 0) throw new Exception("El nombre está vacío!");

                string apellido = Interaction.InputBox("Ingrese apellido", "Alta cliente");
                if (nombre.Length == 0) throw new Exception("El apellido está vacío!");

                string telefono = Interaction.InputBox("Ingrese teléfono", "Alta cliente");
                if (nombre.Length == 0) throw new Exception("El teléfono está vacío!");

                string email = Interaction.InputBox("Ingrese email", "Alta cliente");
                if (!ValidarDatos(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) throw new Exception("El formato del correo es incorrecto!");

                string fechaNacimientoInput = Interaction.InputBox("Ingrese fecha de nacimiento DD/MM/AAAA", "Alta cliente");
                if (!ValidarDatos(fechaNacimientoInput, @"^([0-2]?[0-9]|3[01])/([0]?[1-9]|1[0-2])/\d{4}$") || !DateTime.TryParse(fechaNacimientoInput, out DateTime fechaNacimiento) || fechaNacimiento > DateTime.Now) throw new Exception("La fecha de nacimiento es incorrecta o tiene un formato inválido!");

                BE_Cliente_064RB cliente = new BE_Cliente_064RB(dni, nombre, apellido, telefono, email, fechaNacimiento);
                bll_cliente.Agregar_064RB(cliente);
                Mostrar(grillaClientes, bll_cliente.Consultar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaClientes.Rows.Count == 0) throw new Exception("No hay clientes para borrar!");
                BE_Cliente_064RB cliente = grillaClientes.SelectedRows[0].DataBoundItem as BE_Cliente_064RB;
                bll_cliente.Borrar_064RB(cliente);
                Mostrar(grillaClientes, bll_cliente.Consultar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grillaClientes.Rows.Count == 0) throw new Exception("No hay clientes para modificar!");
                BE_Cliente_064RB clienteViejo = grillaClientes.SelectedRows[0].DataBoundItem as BE_Cliente_064RB;

                string nombre = Interaction.InputBox("Ingrese nombre", "Modificar cliente", clienteViejo.Nombre_064RB.ToString());
                if (nombre.Length == 0) throw new Exception("El nombre está vacío!");

                string apellido = Interaction.InputBox("Ingrese apellido", "Modificar cliente", clienteViejo.Apellido_064RB.ToString());
                if (nombre.Length == 0) throw new Exception("El apellido está vacío!");

                string telefono = Interaction.InputBox("Ingrese teléfono", "Modificar cliente", clienteViejo.Telefono_064RB.ToString());
                if (nombre.Length == 0) throw new Exception("El teléfono está vacío!");

                string email = Interaction.InputBox("Ingrese email", "Modificar cliente", clienteViejo.Email_064RB.ToString());
                if (!ValidarDatos(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) throw new Exception("El formato del correo es incorrecto!");

                string fechaNacimientoInput = Interaction.InputBox("Ingrese fecha de nacimiento DD/MM/AAAA", "Alta cliente", clienteViejo.FechaNacimiento_064RB.ToShortDateString());
                if (!ValidarDatos(fechaNacimientoInput, @"^([0-2]?[0-9]|3[01])/([0]?[1-9]|1[0-2])/\d{4}$") || !DateTime.TryParse(fechaNacimientoInput, out DateTime fechaNacimiento) || fechaNacimiento > DateTime.Now) throw new Exception("La fecha de nacimiento es incorrecta o tiene un formato inválido!");

                BE_Cliente_064RB clienteNuevo = new BE_Cliente_064RB(clienteViejo.Dni_064RB, nombre, apellido, telefono, email, fechaNacimiento);
                bll_cliente.Modificar_064RB(clienteNuevo);
                Mostrar(grillaClientes, bll_cliente.Consultar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        public void ActualizarIdioma_064RB()
        {
            this.Text = bll_idioma.Traducir_064RB("FrmClientes.Form");
            lblClientes.Text = bll_idioma.Traducir_064RB("FrmClientes.lblClientes");
            btnAgregar.Text = bll_idioma.Traducir_064RB("FrmClientes.btnAgregar");
            btnBorrar.Text = bll_idioma.Traducir_064RB("FrmClientes.btnBorrar");
            btnModificar.Text = bll_idioma.Traducir_064RB("FrmClientes.btnModificar");
            btnSalir.Text = bll_idioma.Traducir_064RB("FrmClientes.btnSalir");
            TraducirColumnas(grillaClientes);
        }
        private void TraducirColumnas(DataGridView grilla)
        {
            if (grilla.Columns.Count == 0) return;
            grilla.Columns["Dni_064RB"].HeaderText = bll_idioma.Traducir_064RB("FrmClientes.colDni");
            grilla.Columns["Nombre_064RB"].HeaderText = bll_idioma.Traducir_064RB("FrmClientes.colNombre");
            grilla.Columns["Apellido_064RB"].HeaderText = bll_idioma.Traducir_064RB("FrmClientes.colApellido");
            grilla.Columns["Telefono_064RB"].HeaderText = bll_idioma.Traducir_064RB("FrmClientes.colTelefono");
            grilla.Columns["Email_064RB"].HeaderText = bll_idioma.Traducir_064RB("FrmClientes.colEmail");
            grilla.Columns["FechaNacimiento_064RB"].HeaderText = bll_idioma.Traducir_064RB("FrmClientes.colFechaNacimiento");
        }
    }
}
