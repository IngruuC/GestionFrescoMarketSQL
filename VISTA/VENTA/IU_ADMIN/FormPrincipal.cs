using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VISTA.COMPRA;
using ENTIDADES;
using ENTIDADES.SEGURIDAD;
using VISTA.IU_ADMIN;
using CONTROLADORA;

namespace VISTA
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            panelContenedor.Controls.Add(lblFrescoMarket); // Marca de agua
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            FormRegistroCliente frm = new FormRegistroCliente();
            AbrirFormEnPanel(frm);
        }

        private void btnRegistrarProducto_Click(object sender, EventArgs e)
        {
            FormRegistroProducto frm = new FormRegistroProducto();
            AbrirFormEnPanel(frm);
        }


        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
            FormVenta frm = new FormVenta();
            AbrirFormEnPanel(frm);
        }

        private void btnVentasTotales_Click(object sender, EventArgs e)
        {
            FormVentasTotales frm = new FormVentasTotales();
            AbrirFormEnPanel(frm);
        }

        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            FormRegistroProveedor frm = new FormRegistroProveedor();
            AbrirFormEnPanel(frm);
        }




        private void AbrirFormEnPanel(Form formHijo)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);

            // Guardar referencia a la etiqueta
            var etiqueta = lblFrescoMarket;
            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(etiqueta);


            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(formHijo);

            formHijo.Show();
            formHijo.BringToFront();
        }

        private void btnComprasTotales_Click(object sender, EventArgs e)
        {
            FormComprasTotales frm = new FormComprasTotales();
            AbrirFormEnPanel(frm);
        }

        private void btnRealizarCompra_Click(object sender, EventArgs e)
        {
               FormCompra frm = new FormCompra();
            AbrirFormEnPanel(frm);
        }

        private void btnGestionBackup_Click(object sender, EventArgs e)
        {
            if (SesionActual.EsAdministrador())
            {
                FormBackupRestauracionBD frm = new FormBackupRestauracionBD();
                AbrirFormEnPanel(frm);
            }
            else
            {
                MessageBox.Show("Solo los administradores pueden acceder a esta función.",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                    "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // NUEVO: Registrar cierre de sesión ANTES de cerrar
                    if (SesionActual.Usuario != null)
                    {
                        Console.WriteLine($"Cerrando sesión del usuario: {SesionActual.Usuario.NombreUsuario} (ID: {SesionActual.Usuario.Id})");

                        var controladoraAuditoria = new ControladoraAuditoria();
                        controladoraAuditoria.RegistrarCierreSesion(SesionActual.Usuario.Id);
                    }

                    // Cerrar sesión actual
                    SesionActual.CerrarSesion();

                    // Mostrar login
                    Login loginForm = new Login();
                    this.Hide();
                    DialogResult loginResult = loginForm.ShowDialog();

                    if (loginResult != DialogResult.OK)
                    {
                        this.Close();
                    }
                    else
                    {
                        this.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Registrar cierre cuando se cierra la ventana con la X
                if (SesionActual.Usuario != null)
                {
                    Console.WriteLine($"Cerrando ventana - registrando cierre de sesión para: {SesionActual.Usuario.NombreUsuario}");

                    var controladoraAuditoria = new ControladoraAuditoria();
                    controladoraAuditoria.RegistrarCierreSesion(SesionActual.Usuario.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar cierre: {ex.Message}");
            }
        }


        private void AuditoriasSesion_Click(object sender, EventArgs e)
        {
            if (SesionActual.EsAdministrador())
            {
                FormModalSeguridad formSeguridad = new FormModalSeguridad();
                formSeguridad.ShowDialog();
            }
            else
            {
                MessageBox.Show("Solo los administradores pueden acceder a la gestión de seguridad.",
                   "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
