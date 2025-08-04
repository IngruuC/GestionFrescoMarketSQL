using CONTROLADORA;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

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

        // Nuevas variables para el selector (filtro) de productos
        private TextBox txtBuscarProducto;
        private DataGridView dgvSelectorProductos;
        private Panel panelSelector;
        private List<Producto> productosDisponibles;
        private Producto productoSeleccionado;


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
            // Configurar selector de productos 
            ConfigurarSelectorProductosNativo();

            // Configurar NumericUpDown
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 9999;
            nudCantidad.Value = 1;

            // Estado inicial de botones
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            dgvCompra.SelectionChanged += dgvCompra_SelectionChanged;

            cboProveedores.SelectedIndexChanged += cboProveedores_SelectedIndexChanged;
            

        }

        private void ConfigurarSelectorProductosNativo()
        {
            // Cargar productos disponibles
            productosDisponibles = controladoraProducto.ObtenerProductos().ToList();

            // Panel contenedor del selector
            panelSelector = new Panel
            {
                Location = new Point(200, 150),
                Size = new Size(450, 280),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Visible = false
            };

            // Barra de título
            Panel panelTitulo = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(448, 30),
                BackColor = Color.FromArgb(0, 120, 215),
                Cursor = Cursors.SizeAll
            };

            // Label del título
            Label lblTitulo = new Label
            {
                Text = "Seleccionar Producto",
                Location = new Point(8, 6),
                Size = new Size(200, 18),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.Transparent
            };

            // Botón cerrar (X)
            Button btnCerrar = new Button
            {
                Text = "✕",
                Location = new Point(420, 2),
                Size = new Size(26, 26),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Arial", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 17, 35);
            btnCerrar.Click += (s, e) => panelSelector.Visible = false;

            panelTitulo.Controls.AddRange(new Control[] { lblTitulo, btnCerrar });

            // Funcionalidad para arrastrar el panel
            bool arrastrando = false;
            Point ultimoPunto = Point.Empty;

            panelTitulo.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    arrastrando = true;
                    ultimoPunto = e.Location;
                }
            };

            panelTitulo.MouseMove += (s, e) =>
            {
                if (arrastrando)
                {
                    Point ubicacionActual = panelSelector.Location;
                    panelSelector.Location = new Point(
                        ubicacionActual.X + e.X - ultimoPunto.X,
                        ubicacionActual.Y + e.Y - ultimoPunto.Y
                    );
                }
            };

            panelTitulo.MouseUp += (s, e) => { arrastrando = false; };

            // Panel para filtros
            Panel panelFiltros = new Panel
            {
                Location = new Point(5, 35),
                Size = new Size(438, 30),
                BackColor = Color.FromArgb(248, 248, 248),
                BorderStyle = BorderStyle.FixedSingle
            };

            // ComboBox para filtro Perecedero
            ComboBox cboFiltroPerecedero = new ComboBox
            {
                Location = new Point(5, 4),
                Size = new Size(120, 22),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 8F)
            };
            cboFiltroPerecedero.Items.AddRange(new string[] { "Todos", "Perecederos", "No Perecederos" });
            cboFiltroPerecedero.SelectedIndex = 0;
            cboFiltroPerecedero.SelectedIndexChanged += (s, e) => FiltrarProductosEnSelector();

            // Label para el filtro
            Label lblFiltroPerecedero = new Label
            {
                Text = "Tipo:",
                Location = new Point(130, 6),
                Size = new Size(35, 18),
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            panelFiltros.Controls.AddRange(new Control[] { cboFiltroPerecedero, lblFiltroPerecedero });

            // TextBox para búsqueda
            txtBuscarProducto = new TextBox
            {
                Location = new Point(5, 70),
                Size = new Size(438, 25),
                Font = new Font("Segoe UI", 10F),
                Text = "Buscar producto por nombre o código...",
                ForeColor = Color.Gray
            };

            txtBuscarProducto.Enter += (s, e) => {
                if (txtBuscarProducto.Text == "Buscar producto por nombre o código...")
                {
                    txtBuscarProducto.Text = "";
                    txtBuscarProducto.ForeColor = Color.Black;
                }
            };

            txtBuscarProducto.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtBuscarProducto.Text))
                {
                    txtBuscarProducto.Text = "Buscar producto por nombre o código...";
                    txtBuscarProducto.ForeColor = Color.Gray;
                }
            };

            txtBuscarProducto.TextChanged += TxtBuscarProducto_TextChanged;
            txtBuscarProducto.KeyDown += TxtBuscarProducto_KeyDown;

            // DataGridView para productos
            dgvSelectorProductos = new DataGridView
            {
                Location = new Point(5, 100),
                Size = new Size(438, 175),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9F),
                GridColor = Color.FromArgb(230, 230, 230),
                ColumnHeadersHeight = 35,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    SelectionBackColor = Color.FromArgb(0, 120, 215),
                    SelectionForeColor = Color.White,
                    Padding = new Padding(5, 3, 5, 3)
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(248, 248, 248)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(240, 240, 240),
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Padding = new Padding(8, 8, 8, 8),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    WrapMode = DataGridViewTriState.True
                }
            };

            // Configurar columnas
            dgvSelectorProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Producto",
                Width = 180
            });
            dgvSelectorProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodigoBarra",
                HeaderText = "Código",
                Width = 90
            });
            dgvSelectorProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Precio",
                HeaderText = "Precio",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            dgvSelectorProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Stock",
                HeaderText = "Stock",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Eventos del DataGridView
            dgvSelectorProductos.DoubleClick += DgvSelectorProductos_DoubleClick;
            dgvSelectorProductos.KeyDown += DgvSelectorProductos_KeyDown;

            // Agregar controles al panel principal
            panelSelector.Controls.AddRange(new Control[] {
                panelTitulo,
                panelFiltros,
                txtBuscarProducto,
                dgvSelectorProductos
            });

            // Guardar referencia al ComboBox para usarlo en filtros
            panelSelector.Tag = cboFiltroPerecedero;

            this.Controls.Add(panelSelector);
            panelSelector.BringToFront();
        }

        private void cboProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProveedores.SelectedIndex != -1)
            {
                cboProveedores.Enabled = false;
                int proveedorId = (int)cboProveedores.SelectedValue;
                CargarProductosDelProveedorEnSelector(proveedorId);
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


        private void CargarProductosDelProveedorEnSelector(int proveedorId)
        {
            try
            {
                var todosLosProductos = controladoraProducto.ObtenerProductos();
                var productosProveedor = gestorRelacion.ObtenerProductosDeProveedor(proveedorId, todosLosProductos);

                if (productosProveedor.Count == 0)
                {
                    productosDisponibles = todosLosProductos.ToList();
                    MessageBox.Show("Este proveedor no tiene productos asignados. Se mostrarán todos los productos disponibles.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    productosDisponibles = productosProveedor;
                }

                // Filtrar productos en el selector si está visible
                if (panelSelector.Visible)
                {
                    FiltrarProductosEnSelector();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Métodos del selector de productos
        private void TxtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            FiltrarProductosEnSelector();
        }

        private void TxtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && dgvSelectorProductos.Rows.Count > 0)
            {
                dgvSelectorProductos.Focus();
                dgvSelectorProductos.Rows[0].Selected = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                panelSelector.Visible = false;
            }
            else if (e.KeyCode == Keys.Enter && dgvSelectorProductos.SelectedRows.Count > 0)
            {
                SeleccionarProductoDelGrid();
            }
        }
        private void DgvSelectorProductos_DoubleClick(object sender, EventArgs e)
        {
            SeleccionarProductoDelGrid();
        }

        private void DgvSelectorProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SeleccionarProductoDelGrid();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                panelSelector.Visible = false;
            }
        }

        private void SeleccionarProductoDelGrid()
        {
            if (dgvSelectorProductos.SelectedRows.Count > 0)
            {
                productoSeleccionado = (Producto)dgvSelectorProductos.SelectedRows[0].DataBoundItem;

                // Aquí necesitarás un Label para mostrar el producto seleccionado
                // Por ejemplo: lblProductoSeleccionado.Text = $"{productoSeleccionado.Nombre} - ${productoSeleccionado.Precio:N2}";
                // lblProductoSeleccionado.ForeColor = Color.Black;

                // Calcular precio sugerido (20% menos que el precio de venta)
                decimal precioSugerido = productoSeleccionado.Precio * 0.8m;
                txtPrecioUnitario.Text = precioSugerido.ToString("N2");

                // Ocultar selector y enfocar cantidad
                panelSelector.Visible = false;
                nudCantidad.Focus();
            }
        }

        private void CargarProductosEnSelector()
        {
            if (cboProveedores.SelectedIndex != -1)
            {
                int proveedorId = (int)cboProveedores.SelectedValue;
                CargarProductosDelProveedorEnSelector(proveedorId);
            }
            else
            {
                productosDisponibles = controladoraProducto.ObtenerProductos().ToList();
            }

            dgvSelectorProductos.DataSource = null;
            dgvSelectorProductos.DataSource = productosDisponibles;
        }

        private void FiltrarProductosEnSelector()
        {
            string filtroTexto = txtBuscarProducto.Text.ToLower();

            // Ignorar el texto de placeholder
            if (filtroTexto == "buscar producto por nombre o código..." || txtBuscarProducto.ForeColor == Color.Gray)
                filtroTexto = "";

            // Obtener filtro de perecedero del ComboBox
            var cboFiltroPerecedero = panelSelector?.Tag as ComboBox;
            string filtroPerecedero = cboFiltroPerecedero?.SelectedItem?.ToString() ?? "Todos";

            var productosFiltrados = productosDisponibles.Where(p =>
            {
                // Filtro por texto (nombre o código)
                bool coincideTexto = string.IsNullOrEmpty(filtroTexto) ||
                    p.Nombre.ToLower().Contains(filtroTexto) ||
                    (p.CodigoBarra != null && p.CodigoBarra.ToLower().Contains(filtroTexto));

                // Filtro por perecedero
                bool coincidePerecedero = filtroPerecedero == "Todos" ||
                    (filtroPerecedero == "Perecederos" && p.EsPerecedero) ||
                    (filtroPerecedero == "No Perecederos" && !p.EsPerecedero);

                return coincideTexto && coincidePerecedero;
            }).ToList();

            dgvSelectorProductos.DataSource = null;
            dgvSelectorProductos.DataSource = productosFiltrados;

            if (productosFiltrados.Count > 0)
            {
                dgvSelectorProductos.Rows[0].Selected = true;
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
            LimpiarControlesSelector();
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

            if (productoSeleccionado == null)
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
                int cantidad = (int)nudCantidad.Value;
                decimal precioUnitario = decimal.Parse(txtPrecioUnitario.Text);

                var detalle = new DetalleCompra
                {
                    ProductoId = productoSeleccionado.Id,
                    ProductoNombre = productoSeleccionado.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnitario,
                    Subtotal = precioUnitario * cantidad,
                    Producto = productoSeleccionado
                };

                detallesCompra.Add(detalle);
                ActualizarDataGridView();
                LimpiarControlesSelector();
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

            // Establecer el producto seleccionado
            productoSeleccionado = producto;

            // Actualizar el label de producto seleccionado si lo tienes
            // lblProductoSeleccionado.Text = $"{producto.Nombre} - ${producto.Precio:N2}";
            // lblProductoSeleccionado.ForeColor = Color.Black;

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

        private void LimpiarControlesSelector()
        {
            productoSeleccionado = null;
            nudCantidad.Value = 1;
            txtPrecioUnitario.Clear();

            // Limpiar label de producto seleccionado si lo tienes
            // lblProductoSeleccionado.Text = "Ningún producto seleccionado";
            // lblProductoSeleccionado.ForeColor = Color.Gray;

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnSelectorProducto_Click(object sender, EventArgs e)
        {
            if (cboProveedores.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un proveedor primero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (panelSelector.Visible)
            {
                panelSelector.Visible = false;
            }
            else
            {
                // Posicionar el panel cerca del botón
                Button btn = sender as Button;
                panelSelector.Location = new Point(btn.Left, btn.Bottom + 5);

                CargarProductosEnSelector();
                panelSelector.Visible = true;
                panelSelector.BringToFront();

                // Enfocar y limpiar el textbox
                txtBuscarProducto.Focus();
                if (txtBuscarProducto.ForeColor == Color.Gray)
                {
                    txtBuscarProducto.Text = "";
                    txtBuscarProducto.ForeColor = Color.Black;
                }
            }
        }
    }
}
