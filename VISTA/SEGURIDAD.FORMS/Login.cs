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
 controladoraAuditoria.RegistrarInicioSesion(loginResultado.Usuario, direccionIP);

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
                var formRegistro = new RegistroUsuario();
                var resultado = formRegistro.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    // Verificar si el usuario ya fue logueado automáticamente (primer acceso sin usuario previo)
                    if (SesionActual.Usuario != null)
                    {
                        // El usuario se registró como cliente existente y ya está logueado
                        MessageBox.Show(
                            $"¡Bienvenido {SesionActual.Usuario.NombreUsuario}!\n" +
                            "Tu sesión ha sido iniciada automáticamente.",
                            "Sesión iniciada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }

                    // Registro normal exitoso
                    MessageBox.Show(
                        "Usuario registrado exitosamente.\n" +
                        "Ahora puedes iniciar sesión con tus credenciales.",
                        "Registro exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else if (resultado == DialogResult.Cancel)
                {
                    // Verificar si viene con credenciales específicas (usuario ya tenía cuenta)
                    if (!string.IsNullOrEmpty(formRegistro.UsuarioCreado))
                    {
                        // Pre-llenar el usuario
                        txtUsuario.Text = formRegistro.UsuarioCreado;
                        txtContraseña.Focus();

                        // Solo mostrar recordatorio si es un usuario automático (que usa DNI como contraseña)
                        if (formRegistro.UsuarioCreado.StartsWith("cliente_"))
                        {
                            // Usuario automático - mostrar recordatorio sobre contraseña = DNI
                            var timer = new System.Windows.Forms.Timer();
                            timer.Interval = 500; // Medio segundo
                            timer.Tick += (s, args) =>
                            {
                                timer.Stop();
                                MessageBox.Show(
                                    "💡 Recordatorio:\n\n" +
                                    "Tu contraseña actual es tu número de documento.\n" +
                                    "Te recomendamos cambiarla por una personalizada después de iniciar sesión.\n\n" +
                                    "Puedes hacerlo desde: Menú → Mi Perfil → Cambiar Contraseña",
                                    "Contraseña Temporal",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            };
                            timer.Start();
                        }
                        // Si no es usuario automático, no mostramos recordatorio porque no sabemos su contraseña
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario de registro: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
