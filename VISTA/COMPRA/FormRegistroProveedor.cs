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

namespace VISTA
{
    public partial class FormRegistroProveedor : Form
    {
       
         private ControladoraProveedor controladora;
        
        public FormRegistroProveedor()
        {
            InitializeComponent();
        controladora = ControladoraProveedor.ObtenerInstancia();
            InicializarDataGridView();
            ConfigurarControles();
            CargarDatosEnDataGridView();
        }

        private void InicializarDataGridView()
        {
            dgvProveedores.AutoGenerateColumns = true;
            dgvProveedores.ReadOnly = true;
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.AllowUserToDeleteRows = false;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigurarControles()
        {
            txtCuit.MaxLength = 11;
            txtRazonSocial.MaxLength = 100;
            txtTelefono.MaxLength = 20;
            txtEmail.MaxLength = 100;
            txtDireccion.MaxLength = 100;
            
        }

        private void LimpiarCampos()
        {
            txtCuit.Clear();
            txtRazonSocial.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            btnGuardar.Tag = null;
            txtCuit.Focus();
        }

        private void CargarDatosEnDataGridView()
        {
            try
            {
                dgvProveedores.DataSource = null;
               dgvProveedores.DataSource = controladora.ObtenerProveedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


        private void btnModificar_Click(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Tag == null)
            {
                MessageBox.Show("Por favor, primero seleccione un proveedor para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidarDatos()) return;

            try
            {
                var proveedor = new Proveedor
                {
                    Id = (int)btnGuardar.Tag,
                    Cuit = txtCuit.Text.Trim(),
                    RazonSocial = txtRazonSocial.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                controladora.ModificarProveedor(proveedor);
                MessageBox.Show("Proveedor actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el proveedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscarProveedor.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(criterio))
            {
                CargarDatosEnDataGridView();
                return;
            }

            try
            {
                var proveedores = controladora.ObtenerProveedores();
                var proveedoresFiltrados = proveedores.Where(p =>
                    p.Cuit.ToLower().Contains(criterio) ||
                    p.RazonSocial.ToLower().Contains(criterio) ||
                    p.Email.ToLower().Contains(criterio) ||
                    p.Telefono.ToLower().Contains(criterio)
                ).ToList();

                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = proveedoresFiltrados;

                if (proveedoresFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron proveedores que coincidan con el criterio de búsqueda.",
                                  "Búsqueda",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar proveedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
       private void FormRegistroProveedor_Load(object sender, EventArgs e)
        {
            CargarDatosEnDataGridView();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {

        }


    }
}
