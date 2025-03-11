using System;
using System.Linq;
using System.Windows.Forms;
using CONTROLADORA;
using ENTIDADES;
using ENTIDADES.SEGURIDAD;

namespace VISTA
{
    public partial class FormPerfilCliente : Form
    {
        private ControladoraCliente controladoraCliente;
        private ControladoraUsuario controladoraUsuario;
        private Cliente clienteActual;
        private Usuario usuarioActual;

        public FormPerfilCliente()
        {
            InitializeComponent();
            controladoraCliente = ControladoraCliente.ObtenerInstancia();
            controladoraUsuario = ControladoraUsuario.ObtenerInstancia();

            usuarioActual = SesionActual.Usuario;
            // Buscar el cliente asociado al usuario actual
            clienteActual = ObtenerClienteDelUsuarioActual();

            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar campos con la información actual del cliente
            txtNombre.Text = clienteActual.Nombre;
            txtApellido.Text = clienteActual.Apellido;
            txtDocumento.Text = clienteActual.Documento;
            txtDireccion.Text = clienteActual.Direccion;
            txtEmail.Text = usuarioActual.Email;

            // Deshabilitar edición de algunos campos
            txtDocumento.ReadOnly = true;
        }
        private Cliente ObtenerClienteDelUsuarioActual()
        {
            // Buscar el cliente con el mismo ID de usuario
            var clientes = controladoraCliente.ObtenerClientes();
            var clienteAsociado = clientes.FirstOrDefault(c => c.UsuarioId == usuarioActual.Id);

            if (clienteAsociado == null)
            {
                throw new InvalidOperationException("No se encontró un cliente asociado al usuario actual.");
            }

            return clienteAsociado;
        }

        private bool ValidarDatos()
        {
            // Validaciones de campos
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("La dirección es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Ingrese un email válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }


        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                // Actualizar datos del cliente
                clienteActual.Nombre = txtNombre.Text.Trim();
                clienteActual.Apellido = txtApellido.Text.Trim();
                clienteActual.Direccion = txtDireccion.Text.Trim();

                // Actualizar email del usuario
                usuarioActual.Email = txtEmail.Text.Trim();

                // Guardar cambios
                controladoraCliente.ModificarCliente(clienteActual);

                // Actualizar usuario
                controladoraUsuario.ModificarUsuario(usuarioActual);

                MessageBox.Show("Datos actualizados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCambiarContraseña_Click_1(object sender, EventArgs e)
        {
            // Mostrar formulario de cambio de contraseña
            using (var formCambioContraseña = new FormCambioContraseña())
            {
                formCambioContraseña.ShowDialog();
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Formulario de Cambio de Contraseña
    public partial class FormCambioContraseña : Form
    {
        private ControladoraUsuario controladoraUsuario;
        private Usuario usuarioActual;

        public FormCambioContraseña()
        {
            InitializeComponent();
            controladoraUsuario = ControladoraUsuario.ObtenerInstancia();
            usuarioActual = SesionActual.Usuario;
        }

        private bool ValidarCambioContraseña()
        {
            if (string.IsNullOrWhiteSpace(txtContraseñaActual.Text))
            {
                MessageBox.Show("Ingrese la contraseña actual.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseñaActual.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNuevaContraseña.Text))
            {
                MessageBox.Show("Ingrese la nueva contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNuevaContraseña.Focus();
                return false;
            }

            if (txtNuevaContraseña.Text != txtConfirmarContraseña.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmarContraseña.Focus();
                return false;
            }

            // Validar complejidad de la nueva contraseña
            string mensajeError;
            if (!ENTIDADES.SEGURIDAD.ValidadorContraseña.ValidarComplejidad(txtNuevaContraseña.Text, out mensajeError))
            {
                MessageBox.Show(mensajeError, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNuevaContraseña.Focus();
                return false;
            }

            return true;
        }

        private void btnGuardarContraseña_Click(object sender, EventArgs e)
        {
            if (!ValidarCambioContraseña()) return;

            try
            {
                // Validar contraseña actual
                var resultado = controladoraUsuario.ValidarCredenciales(usuarioActual.NombreUsuario, txtContraseñaActual.Text);

                if (!resultado.Exitoso)
                {
                    MessageBox.Show("La contraseña actual es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Actualizar contraseña
                controladoraUsuario.CambiarContrasena(usuarioActual.Id, txtNuevaContraseña.Text);

                MessageBox.Show("Contraseña actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar la contraseña: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            // Configuraciones del formulario
            this.Text = "Cambiar Contraseña";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Crear controles
            this.lblContraseñaActual = new Label();
            this.txtContraseñaActual = new TextBox();
            this.lblNuevaContraseña = new Label();
            this.txtNuevaContraseña = new TextBox();
            this.lblConfirmarContraseña = new Label();
            this.txtConfirmarContraseña = new TextBox();
            this.btnGuardarContraseña = new Button();
            this.btnCancelar = new Button();

            // Configurar controles
            // Contraseña Actual
            this.lblContraseñaActual.Text = "Contraseña Actual:";
            this.lblContraseñaActual.Location = new System.Drawing.Point(30, 30);
            this.txtContraseñaActual.Location = new System.Drawing.Point(30, 50);
            this.txtContraseñaActual.Width = 340;
            this.txtContraseñaActual.PasswordChar = '*';

            // Nueva Contraseña
            this.lblNuevaContraseña.Text = "Nueva Contraseña:";
            this.lblNuevaContraseña.Location = new System.Drawing.Point(30, 90);
            this.txtNuevaContraseña.Location = new System.Drawing.Point(30, 110);
            this.txtNuevaContraseña.Width = 340;
            this.txtNuevaContraseña.PasswordChar = '*';

            // Confirmar Contraseña
            this.lblConfirmarContraseña.Text = "Confirmar Nueva Contraseña:";
            this.lblConfirmarContraseña.Location = new System.Drawing.Point(30, 150);
            this.txtConfirmarContraseña.Location = new System.Drawing.Point(30, 170);
            this.txtConfirmarContraseña.Width = 340;
            this.txtConfirmarContraseña.PasswordChar = '*';

            // Botón Guardar
            this.btnGuardarContraseña.Text = "Guardar";
            this.btnGuardarContraseña.Location = new System.Drawing.Point(120, 220);
            this.btnGuardarContraseña.Width = 100;
            this.btnGuardarContraseña.Click += btnGuardarContraseña_Click;

            // Botón Cancelar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new System.Drawing.Point(230, 220);
            this.btnCancelar.Width = 100;
            this.btnCancelar.Click += btnCancelar_Click;

            // Agregar controles
            this.Controls.Add(lblContraseñaActual);
            this.Controls.Add(txtContraseñaActual);
            this.Controls.Add(lblNuevaContraseña);
            this.Controls.Add(txtNuevaContraseña);
            this.Controls.Add(lblConfirmarContraseña);
            this.Controls.Add(txtConfirmarContraseña);
            this.Controls.Add(btnGuardarContraseña);
            this.Controls.Add(btnCancelar);
        }

        // Declaración de controles
        private Label lblContraseñaActual;
        private TextBox txtContraseñaActual;
        private Label lblNuevaContraseña;
        private TextBox txtNuevaContraseña;
        private Label lblConfirmarContraseña;
        private TextBox txtConfirmarContraseña;
        private Button btnGuardarContraseña;
        private Button btnCancelar;
    }
}