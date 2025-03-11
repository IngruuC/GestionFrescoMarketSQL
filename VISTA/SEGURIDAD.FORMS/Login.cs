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
using VISTA.COMPRA;
using ENTIDADES.SEGURIDAD;

namespace VISTA
{
    public partial class Login : Form
    {
        private readonly ControladoraUsuario controladora;
        private ControladoraAuditoria controladoraAuditoria;

        public Login()
        {
            InitializeComponent();
            try
            {
                controladora = ControladoraUsuario.ObtenerInstancia();
                controladoraAuditoria = new ControladoraAuditoria(); 
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

            // Agregar enlace para recuperar contraseña
            lnkOlvideContraseña = new LinkLabel();
            lnkOlvideContraseña.AutoSize = true;
            lnkOlvideContraseña.Location = new System.Drawing.Point(
                txtContraseña.Left,
                txtContraseña.Bottom + 10);
            lnkOlvideContraseña.Text = "Olvidé mi contraseña";
            lnkOlvideContraseña.LinkClicked += new LinkLabelLinkClickedEventHandler(lnkOlvideContraseña_LinkClicked);
            this.Controls.Add(lnkOlvideContraseña);

        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            // Primero forzar contraseña de admin (si es necesario)
            controladora.ForzarContrasenaAdmin();

            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            if (string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese la contraseña", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContraseña.Focus();
                return;
            }

            try
            {
                // Validar credenciales
                var loginResultado = controladora.ValidarCredenciales(usuario, contraseña);

                if (loginResultado == null)
                {
                    MessageBox.Show("Error al validar credenciales", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!loginResultado.Exitoso)
                {
                    MessageBox.Show(loginResultado.Mensaje, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                    return;
                }

                // Verificar que el usuario no sea null
                if (loginResultado.Usuario == null)
                {
                    MessageBox.Show("Información de usuario no disponible", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener IP del cliente
                string direccionIP = ObtenerDireccionIP();

                // Inicializar la controladora de auditoría
                var controladoraAuditoria = new ControladoraAuditoria();

                // Registrar inicio de sesión
 //ERROR  // controladoraAuditoria.RegistrarInicioSesion(loginResultado.Usuario, direccionIP);

                // Actualizar último acceso
                controladora.ActualizarAcceso(loginResultado.Usuario.Id);

                // Establecer sesión actual
                SesionActual.Usuario = loginResultado.Usuario;

                // Determinar formulario de destino
                Form formularioDestino;
                var grupoUsuario = loginResultado.Usuario.Grupos?.FirstOrDefault()?.NombreGrupo.ToUpper() ?? "";

                switch (grupoUsuario)
                {
                    case "ADMINISTRADOR":
                        formularioDestino = new FormPrincipal();
                        break;
                    case "PROVEEDOR":
                        formularioDestino = new FormVistaPrincipalProveedor();
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
                MessageBox.Show($"Error en el inicio de sesión: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ObtenerDireccionIP()
        {
            try
            {
                return System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
                    .AddressList
                    .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    ?.ToString() ?? "Desconocida";
            }
            catch
            {
                return "No disponible";
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

        private void lnkOlvideContraseña_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (var formRecuperar = new FormRecuperarContraseña())
                {
                    formRecuperar.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario de recuperación: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
