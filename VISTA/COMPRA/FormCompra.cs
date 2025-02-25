using CONTROLADORA;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormCompra : Form
    {
        private ControladoraProveedor controladoraProveedor;
        private ControladoraProducto controladoraProducto;
        private ControladoraCompra controladoraCompra;
        private List<DetalleCompra> detallesCompra = new List<DetalleCompra>();
        private int compraIdActual;
        private GestorRelacionProveedorProducto gestorRelacion;


        public FormCompra()
        {
            InitializeComponent();
            controladoraProveedor = ControladoraProveedor.ObtenerInstancia();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            controladoraCompra = ControladoraCompra.ObtenerInstancia();
            ConfigurarControles();
            compraIdActual = GenerarNuevaCompraId();
            gestorRelacion = GestorRelacionProveedorProducto.ObtenerInstancia();

        }

        private void ConfigurarControles()
        {
            // Configuracion DataGridView
            dgvCompra.AutoGenerateColumns = false;
            dgvCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductoNombre",
                HeaderText = "Producto",
                ReadOnly = true,
                Width = 200
            });
            dgvCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Cantidad",
                HeaderText = "Cantidad",
                ReadOnly = true,
                Width = 100
            });
            dgvCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PrecioUnitario",
                HeaderText = "Precio Unit.",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });
            dgvCompra.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            // Cargar combos
            CargarProveedores();
            CargarProductos();

            // Configurar NumericUpDown
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 9999;
            nudCantidad.Value = 1;

            // Estado inicial de botones
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            dgvCompra.SelectionChanged += dgvCompra_SelectionChanged;

            cboProveedores.SelectedIndexChanged += cboProveedores_SelectedIndexChanged;
            cboProductos.SelectedIndexChanged += cboProductos_SelectedIndexChanged;

        }

        private void cboProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProveedores.SelectedIndex != -1)
            {
                cboProveedores.Enabled = false;  // Bloquea el ComboBox
                int proveedorId = (int)cboProveedores.SelectedValue;
                CargarProductosDelProveedor(proveedorId);
            }
        }

        private void SimularFiltroProductosPorProveedor(Proveedor proveedor)
        {
            try
            {
                // Esta es una lógica simple de filtrado. Puedes adaptarla según tu necesidad
                // Por ejemplo, podríamos filtrar productos que empiecen con la misma letra que el proveedor
                // O usar otras reglas de negocio que tengan sentido en tu contexto

                var productos = controladoraProducto.ObtenerProductos().ToList();

                // Simulamos un filtro basado en alguna regla arbitraria 
                // (puedes cambiar esta lógica por algo más relacionado a tu negocio)
                var productosDelProveedor = productos;

                // Si quieres aplicar algún filtro específico, puedes descomentar esto y ajustarlo:
                /*
                // Por ejemplo, si los productos comienzan con la primera letra de la razón social del proveedor
                char letraInicial = proveedor.RazonSocial.ToUpper()[0];
                var productosDelProveedor = productos.Where(p => p.Nombre.ToUpper().StartsWith(letraInicial.ToString())).ToList();
                */

                cboProductos.DataSource = null;
                cboProductos.DataSource = productosDelProveedor;
                cboProductos.DisplayMember = "Nombre";
                cboProductos.ValueMember = "Id";
                cboProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cboProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProductos.SelectedIndex != -1)
            {
                Producto productoSeleccionado = (Producto)cboProductos.SelectedItem;
                // Usamos el precio actual del producto como precio de compra sugerido
                // Quizás podrías aplicar un descuento
                decimal precioSugerido = productoSeleccionado.Precio * 0.8m; // 20% menos que el precio de venta
                txtPrecioUnitario.Text = precioSugerido.ToString("N2");
            }
            else
            {
                txtPrecioUnitario.Clear();
            }
        }

        private void CargarProveedores()
        {
            try
            {
                cboProveedores.DataSource = null;
                cboProveedores.DataSource = controladoraProveedor.ObtenerProveedores();
                cboProveedores.DisplayMember = "RazonSocial";
                cboProveedores.ValueMember = "Id";
                cboProveedores.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los proveedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                var productos = controladoraProducto.ObtenerProductos().ToList();
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

        private void CargarProductosDelProveedor(int proveedorId)
        {
            try
            {
                var todosLosProductos = controladoraProducto.ObtenerProductos();
                var productosProveedor = gestorRelacion.ObtenerProductosDeProveedor(proveedorId, todosLosProductos);

                if (productosProveedor.Count == 0)
                {
                    MessageBox.Show("Este proveedor no tiene productos asignados. Se mostrarán todos los productos disponibles.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cboProductos.DataSource = null;
                    cboProductos.DataSource = todosLosProductos;
                }
                else
                {
                    cboProductos.DataSource = null;
                    cboProductos.DataSource = productosProveedor;
                }

                cboProductos.DisplayMember = "Nombre";
                cboProductos.ValueMember = "Id";
                cboProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GenerarNuevaCompraId()
        {
            Random random = new Random();
            return random.Next(1000000, 9999999);
        }

        private void ActualizarTotal()
        {
            decimal total = detallesCompra.Sum(d => d.Subtotal);
            lblTotal.Text = $"TOTAL: $ {total:N2}";
        }

        private void LimpiarControles()
        {
            cboProductos.SelectedIndex = -1;
            nudCantidad.Value = 1;
            txtPrecioUnitario.Clear();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void ActualizarDataGridView()
        {
            dgvCompra.DataSource = null;
            dgvCompra.DataSource = new BindingList<DetalleCompra>(detallesCompra);
            ActualizarTotal();
        }

        private bool ValidarDatos()
        {
            if (cboProveedores.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecioUnitario.Text) || !decimal.TryParse(txtPrecioUnitario.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Debe ingresar un precio válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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

                cmbFormaPago.Items.AddRange(new string[] { "Efectivo", "Transferencia", "Cheque" });
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




        private void dgvCompra_SelectionChanged(object sender, EventArgs e)
        {
            btnModificar.Enabled = dgvCompra.SelectedRows.Count > 0;
            btnEliminar.Enabled = dgvCompra.SelectedRows.Count > 0;
        }

        private void FormCompra_Load(object sender, EventArgs e)
        {
            ActualizarDataGridView();
        }

        private void btnAgregarProducto_Click_1(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                var producto = (Producto)cboProductos.SelectedItem;
                int cantidad = (int)nudCantidad.Value;
                decimal precioUnitario = decimal.Parse(txtPrecioUnitario.Text);

                var detalle = new DetalleCompra
                {
                    ProductoId = producto.Id,
                    ProductoNombre = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnitario,
                    Subtotal = precioUnitario * cantidad,
                    Producto = producto
                };

                detallesCompra.Add(detalle);
                ActualizarDataGridView();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (dgvCompra.SelectedRows.Count == 0) return;

            var detalle = (DetalleCompra)dgvCompra.SelectedRows[0].DataBoundItem;
            var producto = controladoraProducto.ObtenerProductoPorId(detalle.ProductoId);

            if (producto == null)
            {
                MessageBox.Show("No se pudo encontrar el producto seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cboProductos.SelectedValue = producto.Id;
            nudCantidad.Value = detalle.Cantidad;
            txtPrecioUnitario.Text = detalle.PrecioUnitario.ToString();
            detallesCompra.Remove(detalle);
            ActualizarDataGridView();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvCompra.SelectedRows.Count == 0) return;

            var detalle = (DetalleCompra)dgvCompra.SelectedRows[0].DataBoundItem;
            detallesCompra.Remove(detalle);
            ActualizarDataGridView();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            if (detallesCompra.Count > 0)
            {
                var resultado = MessageBox.Show("¿Está seguro que desea cancelar la compra?",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No) return;
            }

            this.Close();
        }

        private void btnFinalizarCompra_Click_1(object sender, EventArgs e)
        {
            if (cboProveedores.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (detallesCompra.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Solicitar número de factura
            string numeroFactura = "";
            using (var inputForm = new Form())
            {
                inputForm.Text = "Número de Factura";
                var txtFactura = new TextBox() { Left = 50, Top = 20, Width = 200 };
                var label = new Label() { Left = 50, Top = 3, Text = "Ingrese el número de factura:" };
                var btnOk = new Button() { Text = "OK", Left = 50, Width = 100, Top = 50, DialogResult = DialogResult.OK };
                var btnCancel = new Button() { Text = "Cancelar", Left = 150, Width = 100, Top = 50, DialogResult = DialogResult.Cancel };

                inputForm.Controls.AddRange(new Control[] { label, txtFactura, btnOk, btnCancel });
                inputForm.AcceptButton = btnOk;
                inputForm.CancelButton = btnCancel;
                inputForm.Size = new System.Drawing.Size(300, 150);
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    numeroFactura = txtFactura.Text.Trim();
                    if (string.IsNullOrEmpty(numeroFactura))
                    {
                        MessageBox.Show("Debe ingresar un número de factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            string formaPago = MostrarDialogoFormaPago();
            if (string.IsNullOrEmpty(formaPago)) return;

            try
            {
                var compra = new Compra
                {
                    ProveedorId = (int)cboProveedores.SelectedValue,
                    FechaCompra = DateTime.Now,
                    FormaPago = formaPago,
                    NumeroFactura = numeroFactura
                };

                foreach (var detalle in detallesCompra)
                {
                    compra.Detalles.Add(new DetalleCompra
                    {
                        ProductoId = detalle.ProductoId,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        ProductoNombre = detalle.ProductoNombre,
                        Subtotal = detalle.Subtotal
                    });
                }

                controladoraCompra.RealizarCompra(compra);
                MessageBox.Show("Compra realizada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cboProveedores.Enabled = true;
                cboProveedores.SelectedIndex = -1;
                detallesCompra.Clear();
                ActualizarDataGridView();
                compraIdActual = GenerarNuevaCompraId();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al realizar la compra: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
