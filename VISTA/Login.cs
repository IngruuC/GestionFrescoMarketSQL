using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CONTROLADORA;
using ENTIDADES;

namespace VISTA
{
    public partial class Login : Form
    {
        private readonly ControladoraUsuario _controladoraUsuario;

        public Login()
        {
            InitializeComponent();
            _controladoraUsuario = new ControladoraUsuario();
            txtContraseña.PasswordChar = '*';
            ConfigurarForm();
        }

        private void ConfigurarForm()
        {
            // Crear usuario admin por defecto
            _controladoraUsuario.CrearUsuarioAdmin();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contraseña = txtContraseña.Text.Trim();

                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
                {
                    MessageBox.Show("Debe completar todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_controladoraUsuario.ValidarUsuario(usuario, contraseña))
                {
                    MessageBox.Show("Inicio de sesión exitoso", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var mainForm = new Inicio();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
                Application.Exit();
        }
    }
}
