using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CONTROLADORA;
using DevExpress.CodeParser;
using ENTIDADES;

namespace VISTA
{
    public partial class Login : Form
    {
        private readonly ControladoraUsuario controladora;

        public Login()
        {
            InitializeComponent();
            try
            {
                controladora = ControladoraUsuario.ObtenerInstancia();
                ConfigurarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar el formulario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ConfigurarFormulario()
        {
            txtContraseña.PasswordChar = '*';
            // Ya no fijamos el usuario como "admin"
            txtUsuario.Enabled = true;
            txtContraseña.Select();

            // Agregar botón de registro
            btnRegistrar.Visible = true;
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text;
                string contraseña = txtContraseña.Text;

                if (string.IsNullOrEmpty(contraseña))
                {
                    MessageBox.Show("Por favor, ingrese la contraseña", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraseña.Focus();
                    return;
                }

                var resultado = controladora.ValidarCredenciales(usuario, contraseña);

                if (!resultado.Exitoso)
                {
                    MessageBox.Show(resultado.Mensaje, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                    return;
                }

                controladora.ActualizarAcceso(resultado.Usuario.Id);
                SesionActual.Usuario = resultado.Usuario;

                Form formularioDestino;
                var grupoUsuario = resultado.Usuario.Grupos.FirstOrDefault()?.NombreGrupo.ToUpper() ?? "";

                switch (grupoUsuario)
                {
                    case "ADMINISTRADOR":
                        formularioDestino = new FormPrincipal();
                        break;
                    case "PROVEEDOR":
                        formularioDestino = new FormVistaProveedor();
                        break;
                    case "CLIENTE":
                        formularioDestino = new FormVistaPrincipalCliente();
                        break;
                    default:
                        MessageBox.Show($"Tipo de usuario no válido: {grupoUsuario}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                this.DialogResult = DialogResult.OK;
                this.Hide();
                formularioDestino.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el inicio de sesión: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var formRegistro = new RegistroUsuario())
                {
                    this.Hide(); // Ocultamos el login

                    if (formRegistro.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Usuario registrado exitosamente. Por favor inicie sesión.",
                            "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.Show(); // Volvemos a mostrar el login
                    txtUsuario.Clear();
                    txtContraseña.Clear();
                    txtUsuario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario de registro: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show();
            }


        }
    }
}
