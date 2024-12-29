using CONTROLADORA;
using ENTIDADES;
using System;
using System.Linq;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormRegistroCliente : Form
    {
        private ControladoraCliente controladora;

        public FormRegistroCliente()
        {
            InitializeComponent();
            controladora = ControladoraCliente.ObtenerInstancia();
            ConfigurarControles();
            CargarDatosEnDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvCliente.AutoGenerateColumns = true;
            dgvCliente.ReadOnly = true;
            dgvCliente.AllowUserToAddRows = false;
            dgvCliente.AllowUserToDeleteRows = false;
            dgvCliente.MultiSelect = false;
            dgvCliente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigurarControles()
        {
            txtDocumento.MaxLength = 8;
            txtNombre.MaxLength = 50;
            txtApellido.MaxLength = 50;
            txtDireccion.MaxLength = 100;
            ConfigurarDataGridView();
        }

        private void LimpiarCampos()
        {
            txtDocumento.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            btnGuardar.Tag = null;
            txtDocumento.Focus();
        }

        private void CargarDatosEnDataGridView()
        {
            try
            {
                dgvCliente.DataSource = null;
                dgvCliente.DataSource = controladora.ObtenerClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("El documento es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            if (!txtDocumento.Text.All(char.IsDigit) || txtDocumento.Text.Length != 8)
            {
                MessageBox.Show("El documento debe contener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

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

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                var cliente = new Cliente
                {
                    Documento = txtDocumento.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                controladora.AgregarCliente(cliente);
                MessageBox.Show("Cliente guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un cliente para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cliente = (Cliente)dgvCliente.SelectedRows[0].DataBoundItem;
            txtDocumento.Text = cliente.Documento;
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtDireccion.Text = cliente.Direccion;
            btnGuardar.Tag = cliente.Id;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Tag == null)
            {
                MessageBox.Show("Por favor, primero seleccione un cliente para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidarDatos()) return;

            try
            {
                var cliente = new Cliente
                {
                    Id = (int)btnGuardar.Tag,
                    Documento = txtDocumento.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                controladora.ModificarCliente(cliente);
                MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un cliente para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cliente = (Cliente)dgvCliente.SelectedRows[0].DataBoundItem;
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar al cliente {cliente.Nombre} {cliente.Apellido}?",
                                             "Confirmar eliminación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    controladora.EliminarCliente(cliente.Id);
                    MessageBox.Show("Cliente eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDatosEnDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscarCliente.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(criterio))
            {
                CargarDatosEnDataGridView();
                return;
            }

            try
            {
                var clientes = controladora.ObtenerClientes();
                var clientesFiltrados = clientes.Where(c =>
                    c.Documento.ToLower().Contains(criterio) ||
                    c.Nombre.ToLower().Contains(criterio) ||
                    c.Apellido.ToLower().Contains(criterio) ||
                    c.Direccion.ToLower().Contains(criterio)
                ).ToList();

                dgvCliente.DataSource = null;
                dgvCliente.DataSource = clientesFiltrados;

                if (clientesFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron clientes que coincidan con el criterio de búsqueda.",
                                  "Búsqueda",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarCliente.Clear();
            CargarDatosEnDataGridView();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRegistroCliente_Load(object sender, EventArgs e)
        {
            CargarDatosEnDataGridView();
        }
    }
}