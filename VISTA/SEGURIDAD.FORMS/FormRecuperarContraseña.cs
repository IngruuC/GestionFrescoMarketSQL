using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormRecuperarContraseña : Form
    {
     
        private void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar los campos
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MessageBox.Show("Por favor, ingrese el nombre de usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Por favor, ingrese un email válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                Cursor = Cursors.WaitCursor;

                // Solicitar recuperación de contraseña
                bool resultado = controladora.SolicitarRecuperacionClave(txtUsuario.Text.Trim(), txtEmail.Text.Trim());

                Cursor = Cursors.Default;

                if (resultado)
                {
                    MessageBox.Show(
                        "La nueva contraseña le será enviada a su email registrado.\n\n" +
                        "Por favor, verifique su bandeja de entrada y carpeta de spam.",
                        "Recuperación Exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "No se encontró un usuario activo con el nombre de usuario y email proporcionados.",
                        "Error de Recuperación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(
                    $"Error al recuperar la contraseña: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }



}
