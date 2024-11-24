using CONTROLADORA;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormVenta : Form
    {
        private ControladoraCliente controladoraCliente;
        private ControladoraProducto controladoraProducto;
        private ControladoraVenta controladoraVenta;
        private List<DetalleVenta> detallesVenta = new List<DetalleVenta>();
        private int ventaIdActual;

        public FormVenta()
        {
            InitializeComponent();
            controladoraCliente = ControladoraCliente.ObtenerInstancia();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            controladoraVenta = ControladoraVenta.ObtenerInstancia();
            ConfigurarControles();
            ventaIdActual = GenerarNuevaVentaId();
        }

        private void ConfigurarControles()
        {
            // Configurar DataGridView
            dgvVenta.AutoGenerateColumns = false;
            dgvVenta.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductoNombre",
                HeaderText = "Producto",
                ReadOnly = true,
                Width = 200
            });
            dgvVenta.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Cantidad",
                HeaderText = "Cantidad",
                ReadOnly = true,
                Width = 100
            });
            dgvVenta.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrecioUnitario",
                HeaderText = "Precio Unit.",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgvVenta.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            // Cargar combos
            CargarClientes();
            CargarProductos();

            // Configurar NumericUpDown
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 9999;
            nudCantidad.Value = 1;

            // Estado inicial de botones
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void CargarClientes()
        {
            try
            {
                cboClientes.DataSource = null;
                cboClientes.DataSource = controladoraCliente.ObtenerClientes();
                cboClientes.DisplayMember = "ToString";
                cboClientes.ValueMember = "Id";
                cboClientes.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                var productos = controladoraProducto.ObtenerProductos()
                    .Where(p => p.Stock > 0)
                    .ToList();
                cboProductos.DataSource = null;
                cboProductos.DataSource = productos;
                cboProductos.DisplayMember = "Nombre";
                cboProductos.ValueMember = "Id";
                cboProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GenerarNuevaVentaId()
        {
            Random random = new Random();
            return random.Next(1000000, 9999999);
        }

        private void ActualizarTotal()
        {
            decimal total = detallesVenta.Sum(d => d.Subtotal);
            lblTotal.Text = $"TOTAL: $ {total:N2}";
        }

        private void LimpiarControles()
        {
            cboProductos.SelectedIndex = -1;
            nudCantidad.Value = 1;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void ActualizarDataGridView()
        {
            dgvVenta.DataSource = null;
            dgvVenta.DataSource = new BindingList<DetalleVenta>(detallesVenta);
            ActualizarTotal();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cboClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var producto = (Producto)cboProductos.SelectedItem;
            int cantidad = (int)nudCantidad.Value;

            if (cantidad > producto.Stock)
            {
                MessageBox.Show($"Stock insuficiente. Stock disponible: {producto.Stock}", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var detalle = new DetalleVenta
                {
                    // No asignar VentaId aquí
                    ProductoId = producto.Id,
                    ProductoNombre = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio,
                    Subtotal = producto.Precio * cantidad,
                    Producto = producto  // Agregar esta línea
                };

                detallesVenta.Add(detalle);
                ActualizarDataGridView();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvVenta.SelectedRows.Count == 0) return;

            var detalle = (DetalleVenta)dgvVenta.SelectedRows[0].DataBoundItem;
            var producto = controladoraProducto.ObtenerProductoPorId(detalle.ProductoId);

            if (producto == null)
            {
                MessageBox.Show("No se pudo encontrar el producto seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cboProductos.SelectedValue = producto.Id;
            nudCantidad.Value = detalle.Cantidad;
            detallesVenta.Remove(detalle);
            ActualizarDataGridView();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvVenta.SelectedRows.Count == 0) return;

            var detalle = (DetalleVenta)dgvVenta.SelectedRows[0].DataBoundItem;
            detallesVenta.Remove(detalle);
            ActualizarDataGridView();
        }

        private string MostrarDialogoFormaPago()
        {
            using (var form = new Form())
            {
                form.Text = "Seleccionar Forma de Pago";
                form.Size = new System.Drawing.Size(300, 150);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var cmbFormaPago = new ComboBox()
                {
                    Left = 50,
                    Top = 20,
                    Width = 200,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                cmbFormaPago.Items.AddRange(new string[] { "Efectivo", "Tarjeta de Crédito", "Tarjeta de Débito" });
                cmbFormaPago.SelectedIndex = 0;

                var btnAceptar = new Button()
                {
                    Text = "Aceptar",
                    DialogResult = DialogResult.OK,
                    Left = 100,
                    Width = 100,
                    Top = 60
                };

                form.Controls.AddRange(new Control[] { cmbFormaPago, btnAceptar });
                form.AcceptButton = btnAceptar;

                return form.ShowDialog() == DialogResult.OK ? cmbFormaPago.SelectedItem.ToString() : string.Empty;
            }
        }

        private void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            if (cboClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (detallesVenta.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string formaPago = MostrarDialogoFormaPago();
            if (string.IsNullOrEmpty(formaPago)) return;

            try
            {
                var venta = new Venta
                {
                    ClienteId = (int)cboClientes.SelectedValue,
                    FechaVenta = DateTime.Now,
                    FormaPago = formaPago
                };

                foreach (var detalle in detallesVenta)
                {
                    venta.Detalles.Add(new DetalleVenta
                    {
                        ProductoId = detalle.ProductoId,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        ProductoNombre = detalle.ProductoNombre,
                        Subtotal = detalle.Subtotal
                    });
                }

                controladoraVenta.RealizarVenta(venta);
                MessageBox.Show("Venta realizada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar todo
                cboClientes.SelectedIndex = -1;
                detallesVenta.Clear();
                ActualizarDataGridView();
                ventaIdActual = GenerarNuevaVentaId();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (detallesVenta.Count > 0)
            {
                var resultado = MessageBox.Show("¿Está seguro que desea cancelar la venta?",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No) return;
            }

            this.Close();
        }

        private void dgvVenta_SelectionChanged(object sender, EventArgs e)
        {
            btnModificar.Enabled = dgvVenta.SelectedRows.Count > 0;
            btnEliminar.Enabled = dgvVenta.SelectedRows.Count > 0;
        }

        private void FormVenta_Load(object sender, EventArgs e)
        {
            ActualizarDataGridView();
        }
    }
}