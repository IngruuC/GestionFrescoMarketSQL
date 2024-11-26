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
            txtUsuario.Text = "admin";
            txtUsuario.Enabled = false;
            txtContraseña.Select();

            this.StartPosition = FormStartPosition.CenterScreen;

            txtContraseña.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    btnIniciarSesion_Click(sender, e);
                }
            };
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                string contraseña = txtContraseña.Text;

                if (string.IsNullOrEmpty(contraseña))
                {
                    MessageBox.Show("Por favor, ingrese la contraseña", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContraseña.Focus();
                    return;
                }

                if (controladora.ValidarCredenciales("admin", contraseña))
                {
                    Inicio formInicio = new Inicio();
                    this.Hide();
                    formInicio.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el inicio de sesión: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
