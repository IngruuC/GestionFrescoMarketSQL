using CONTROLADORA;
using ENTIDADES;
using System;
using System.Linq;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormRegistroProducto : Form
    {
        private ControladoraProducto controladora;

        public FormRegistroProducto()
        {
            InitializeComponent();
            controladora = ControladoraProducto.ObtenerInstancia();
            ConfigurarControles();
            CargarDatosEnDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvProductos.AutoGenerateColumns = true;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.MultiSelect = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void ConfigurarControles()
        {
            txtNombre.MaxLength = 100;
            txtCodigoBarra.MaxLength = 8;
            ConfigurarDataGridView();

            // Configurar controles de producto perecedero
            chkEsPerecedero.Checked = false;
            dtpFechaVencimiento.Enabled = false;
            dtpFechaVencimiento.MinDate = DateTime.Now.AddDays(1);

            // Asignar eventos
            txtPrecio.KeyPress += TxtPrecio_KeyPress;
            txtStock.KeyPress += TxtStock_KeyPress;
            chkEsPerecedero.CheckedChanged += ChkEsPerecedero_CheckedChanged;
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCodigoBarra.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            chkEsPerecedero.Checked = false;
            dtpFechaVencimiento.Value = DateTime.Now.AddDays(1);
            btnGuardar.Tag = null;
            txtNombre.Focus();
        }

        private void CargarDatosEnDataGridView()
        {
            try
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = controladora.ObtenerProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

                if (string.IsNullOrWhiteSpace(txtCodigoBarra.Text) || txtCodigoBarra.Text.Length != 8 || !txtCodigoBarra.Text.All(char.IsDigit))
                {
                    MessageBox.Show("El código de barras debe contener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigoBarra.Focus();
                    return false;
                }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("El precio debe ser un número mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("El stock debe ser un número entero no negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return false;
            }

            if (chkEsPerecedero.Checked && dtpFechaVencimiento.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("La fecha de vencimiento debe ser posterior a la fecha actual.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaVencimiento.Focus();
                return false;
            }

            return true;
        }

        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, punto decimal y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void TxtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ChkEsPerecedero_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaVencimiento.Enabled = chkEsPerecedero.Checked;
            if (chkEsPerecedero.Checked)
            {
                dtpFechaVencimiento.Value = DateTime.Now.AddDays(1);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                var producto = new Producto
                {
                    Nombre = txtNombre.Text.Trim(),
                    CodigoBarra = txtCodigoBarra.Text.Trim(),
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text),
                    EsPerecedero = chkEsPerecedero.Checked,
                    FechaVencimiento = chkEsPerecedero.Checked ? dtpFechaVencimiento.Value : (DateTime?)null
                };

                controladora.AgregarProducto(producto);
                MessageBox.Show("Producto guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un producto para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var producto = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            txtNombre.Text = producto.Nombre;
            txtCodigoBarra.Text = producto.CodigoBarra;
            txtPrecio.Text = producto.Precio.ToString();
            txtStock.Text = producto.Stock.ToString();
            chkEsPerecedero.Checked = producto.EsPerecedero;
            if (producto.FechaVencimiento.HasValue)
            {
                dtpFechaVencimiento.Value = producto.FechaVencimiento.Value;
            }
            btnGuardar.Tag = producto.Id;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Tag == null)
            {
                MessageBox.Show("Por favor, primero seleccione un producto para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidarDatos()) return;

            try
            {
                var producto = new Producto
                {
                    Id = (int)btnGuardar.Tag,
                    Nombre = txtNombre.Text.Trim(),
                    CodigoBarra = txtCodigoBarra.Text.Trim(),
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text),
                    EsPerecedero = chkEsPerecedero.Checked,
                    FechaVencimiento = chkEsPerecedero.Checked ? dtpFechaVencimiento.Value : (DateTime?)null
                };

                controladora.ModificarProducto(producto);
                MessageBox.Show("Producto actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var producto = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar el producto {producto.Nombre}?",
                                             "Confirmar eliminación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    controladora.EliminarProducto(producto.Id);
                    MessageBox.Show("Producto eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDatosEnDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscarProducto.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(criterio))
            {
                CargarDatosEnDataGridView();
                return;
            }

            try
            {
                var productos = controladora.ObtenerProductos();
                var productosFiltrados = productos.Where(p =>
                    p.Nombre.ToLower().Contains(criterio) ||
                    p.CodigoBarra.ToLower().Contains(criterio)
                ).ToList();

                dgvProductos.DataSource = null;
                dgvProductos.DataSource = productosFiltrados;

                if (productosFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron productos que coincidan con el criterio de búsqueda.",
                                  "Búsqueda",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarProducto.Clear();
            CargarDatosEnDataGridView();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRegistroProducto_Load(object sender, EventArgs e)
        {
            CargarDatosEnDataGridView();
        }
    }
}