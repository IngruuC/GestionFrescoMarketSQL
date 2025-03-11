using CONTROLADORA;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTIDADES.SEGURIDAD;

namespace VISTA.COMPRA
{
    public partial class FormPerfilProveedor : Form
    {
        private Proveedor proveedor;
        private ControladoraUsuario controladoraUsuario;
        private ControladoraProveedor controladora;
        private bool modoEdicion = false;

        // Constructor por defecto
        public FormPerfilProveedor()
        {
            InitializeComponent();
            controladora = ControladoraProveedor.ObtenerInstancia();
            controladoraUsuario = ControladoraUsuario.ObtenerInstancia();
        }

        // Método público para configurar el proveedor después de la inicialización
        public void ConfigurarProveedor(Proveedor proveedorActual)
        {
            this.proveedor = proveedorActual;

            ConfigurarFormulario();
            CargarDatosProveedor();
        }

        private void ConfigurarFormulario()
        {
            if (proveedor == null) return;

            // Configuración inicial del formulario
            this.Text = $"Perfil de {proveedor.RazonSocial}";

            // Inicialmente los campos estarán en modo solo lectura
            txtCuit.ReadOnly = true;
            txtRazonSocial.ReadOnly = true;
            txtTelefono.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtDireccion.ReadOnly = true;

            // Mostrar/ocultar botones según el modo inicial
            btnEditar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
        }

        private void CargarDatosProveedor()
        {
            if (proveedor == null) return;

            txtCuit.Text = proveedor.Cuit;
            txtRazonSocial.Text = proveedor.RazonSocial;
            txtTelefono.Text = proveedor.Telefono;
            txtEmail.Text = proveedor.Email;
            txtDireccion.Text = proveedor.Direccion;
        }

        private void CambiarModoEdicion(bool edicion)
        {
            modoEdicion = edicion;

            // Cambiar estado de solo lectura
            txtCuit.ReadOnly = !edicion;
            txtRazonSocial.ReadOnly = !edicion;
            txtTelefono.ReadOnly = !edicion;
            txtEmail.ReadOnly = !edicion;
            txtDireccion.ReadOnly = !edicion;

            // Mostrar/ocultar botones según el modo
            btnEditar.Visible = !edicion;
            btnGuardar.Visible = edicion;
            btnCancelar.Visible = edicion;
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtCuit.Text) || txtCuit.Text.Length != 11)
            {
                MessageBox.Show("El CUIT debe contener 11 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuit.Focus();
                return false;
            }

            if (!txtCuit.Text.All(char.IsDigit))
            {
                MessageBox.Show("El CUIT debe contener solo números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuit.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
            {
                MessageBox.Show("La razón social es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRazonSocial.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                }
                catch
                {
                    MessageBox.Show("El formato del email no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("La dirección es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            return true;
        }







        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                // Actualizar datos del proveedor
                proveedor.Cuit = txtCuit.Text.Trim();
                proveedor.RazonSocial = txtRazonSocial.Text.Trim();
                proveedor.Telefono = txtTelefono.Text.Trim();
                proveedor.Email = txtEmail.Text.Trim();
                proveedor.Direccion = txtDireccion.Text.Trim();

                // Guardar cambios
                controladora.ModificarProveedor(proveedor);

                MessageBox.Show("Datos actualizados correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CambiarModoEdicion(false);
                this.Text = $"Perfil de {proveedor.RazonSocial}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            CambiarModoEdicion(true);
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            CargarDatosProveedor();
            CambiarModoEdicion(false);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (proveedor == null || proveedor.UsuarioId == null)
                {
                    MessageBox.Show("No se pudo encontrar el usuario asociado a este proveedor.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Solicitar contraseña actual para verificar
                string passwordActual = MostrarDialogoPassword("Ingrese su contraseña actual:");
                if (string.IsNullOrEmpty(passwordActual))
                    return;

                // Verificar la contraseña actual
                var usuarioActual = SesionActual.Usuario;
                if (usuarioActual == null)
                {
                    MessageBox.Show("No hay una sesión activa.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var resultado = controladoraUsuario.ValidarCredenciales(usuarioActual.NombreUsuario, passwordActual);
                if (!resultado.Exitoso)
                {
                    MessageBox.Show("La contraseña actual es incorrecta.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Solicitar nueva contraseña
                string nuevaPassword = MostrarDialogoPassword("Ingrese la nueva contraseña:");
                if (string.IsNullOrEmpty(nuevaPassword))
                    return;

                // Confirmar nueva contraseña
                string confirmPassword = MostrarDialogoPassword("Confirme la nueva contraseña:");
                if (string.IsNullOrEmpty(confirmPassword))
                    return;

                // Verificar que las contraseñas coincidan
                if (nuevaPassword != confirmPassword)
                {
                    MessageBox.Show("Las contraseñas no coinciden.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validar complejidad de la contraseña
                if (nuevaPassword.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Cambiar la contraseña
                controladoraUsuario.CambiarContrasena(usuarioActual.Id, nuevaPassword);

                MessageBox.Show("Contraseña cambiada exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar la contraseña: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private string MostrarDialogoPassword(string mensaje)
        {
            Form promptForm = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Cambio de Contraseña",
                StartPosition = FormStartPosition.CenterParent
            };

            Label lblPrompt = new Label() { Left = 50, Top = 20, Width = 300, Text = mensaje };
            TextBox txtPassword = new TextBox() { Left = 50, Top = 50, Width = 300, PasswordChar = '*' };
            Button btnOk = new Button() { Text = "Aceptar", Left = 120, Width = 80, Top = 90, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancelar", Left = 210, Width = 80, Top = 90, DialogResult = DialogResult.Cancel };

            btnOk.Click += (sender, e) => { promptForm.Close(); };
            btnCancel.Click += (sender, e) => { promptForm.Close(); };

            promptForm.Controls.Add(lblPrompt);
            promptForm.Controls.Add(txtPassword);
            promptForm.Controls.Add(btnOk);
            promptForm.Controls.Add(btnCancel);
            promptForm.AcceptButton = btnOk;
            promptForm.CancelButton = btnCancel;

            return promptForm.ShowDialog() == DialogResult.OK ? txtPassword.Text : "";
        }
    }
}