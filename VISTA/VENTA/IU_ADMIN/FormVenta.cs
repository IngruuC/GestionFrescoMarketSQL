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
    public partial class FormVenta : Form
    {
        private ControladoraCliente controladoraCliente;
        private ControladoraProducto controladoraProducto;
        private ControladoraVenta controladoraVenta;
        private List<DetalleVenta> detallesVenta = new List<DetalleVenta>();
        private int ventaIdActual;

        private TextBox txtBuscarProducto;
        private DataGridView dgvSelectorProductos;
        private Panel panelSelector;
        private List<Producto> productosDisponibles;
        private Producto productoSeleccionado;

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
            // Configuracion DataGridView
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
            ConfigurarSelectorProductosNativo();



            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 9999;
            nudCantidad.Value = 1;

            // Estado inicial de botones
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            dgvVenta.SelectionChanged += dgvVenta_SelectionChanged;

            cboClientes.SelectedIndexChanged += cboClientes_SelectedIndexChanged;

            // Buscar y ocultar el botón "SELECCIONAR" original
            foreach (Control control in this.Controls)
            {
                if (control is Button && control.Text == "SELECCIONAR")
                {
                    control.Visible = false;
                    break;
                }
            }


        }

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClientes.SelectedIndex != -1)
            {
                cboClientes.Enabled = false;  // Bloquea el ComboBox para realizar venta a un unico cliente
            }
        }
        private void CargarClientes()
        {
            try
            {
                cboClientes.DataSource = null;
                cboClientes.DataSource = controladoraCliente.ObtenerClientes();
                cboClientes.DisplayMember = "DatosCompletos";
                cboClientes.ValueMember = "Id";
                cboClientes.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ConfigurarSelectorProductosNativo()
        {
            // Cargar productos disponibles
            productosDisponibles = controladoraProducto.ObtenerProductos()
                .Where(p => p.Stock > 0)
                .ToList();

            // Panel contenedor del selector (AUMENTADO para filtros)
            panelSelector = new Panel
            {
                Location = new Point(200, 150),
                Size = new Size(450, 280), // Aumentado para nuevos controles
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

            // NUEVO: Panel para filtros
            Panel panelFiltros = new Panel
            {
                Location = new Point(5, 35),
                Size = new Size(438, 30),
                BackColor = Color.FromArgb(248, 248, 248),
                BorderStyle = BorderStyle.FixedSingle
            };

            // NUEVO: ComboBox para filtro Perecedero
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

            // TextBox para búsqueda (ajustado por filtros)
            txtBuscarProducto = new TextBox
            {
                Location = new Point(5, 70), // Movido hacia abajo para los filtros
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

            // DataGridView MEJORADO (ajustado por filtros y header más grande)
            dgvSelectorProductos = new DataGridView
            {
                Location = new Point(5, 100), // Movido hacia abajo
                Size = new Size(438, 175), // Ajustado el tamaño
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

                // MEJORA 1: Header más grande y mejor estilizado
                ColumnHeadersHeight = 35, // ← ALTURA AUMENTADA
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
                    Padding = new Padding(8, 8, 8, 8), // ← PADDING AUMENTADO
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    WrapMode = DataGridViewTriState.True
                }
            };

            // Configurar columnas con mejor distribución
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
        panelFiltros,  // ← NUEVO panel de filtros
        txtBuscarProducto,
        dgvSelectorProductos
    });

            // Guardar referencia al ComboBox para usarlo en filtros
            panelSelector.Tag = cboFiltroPerecedero;

            this.Controls.Add(panelSelector);
            panelSelector.BringToFront();

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
            LimpiarControlesSelector();
        }

        private void ActualizarDataGridView()
        {
            dgvVenta.DataSource = null;
            dgvVenta.DataSource = new BindingList<DetalleVenta>(detallesVenta);

            // Ocultar columnas de navegación de Entity Framework
            if (dgvVenta.Columns["Venta"] != null)
                dgvVenta.Columns["Venta"].Visible = false;

            if (dgvVenta.Columns["Producto"] != null)
                dgvVenta.Columns["Producto"].Visible = false;
            ActualizarTotal();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cboClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (productoSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad = (int)nudCantidad.Value;

            if (cantidad > productoSeleccionado.Stock)
            {
                MessageBox.Show($"Stock insuficiente. Stock disponible: {productoSeleccionado.Stock}",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var detalle = new DetalleVenta
                {
                    ProductoId = productoSeleccionado.Id,
                    ProductoNombre = productoSeleccionado.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = productoSeleccionado.Precio,
                    Subtotal = productoSeleccionado.Precio * cantidad,
                    Producto = productoSeleccionado
                };

                detallesVenta.Add(detalle);
                ActualizarDataGridView();
                LimpiarControlesSelector(); // Cambiar por el nuevo método
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

            // Establecer el producto seleccionado
            productoSeleccionado = producto;

            // Actualizar el label manual de producto seleccionado
            lblProductoSeleccionado.Text = $"{producto.Nombre} - ${producto.Precio:N2}";
            lblProductoSeleccionado.ForeColor = Color.Black;

            // Establecer la cantidad
            nudCantidad.Value = detalle.Cantidad;

            // Remover el detalle de la lista y actualizar
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


                cboClientes.Enabled = true;  // Desbloquea el ComboBox
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

        private void btnAbrirSelector_Click(object sender, EventArgs e)
        {
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

                // Actualizar el label manual de producto seleccionado
                lblProductoSeleccionado.Text = $"{productoSeleccionado.Nombre} - ${productoSeleccionado.Precio:N2}";
                lblProductoSeleccionado.ForeColor = Color.Black;

                // Ocultar selector y enfocar cantidad
                panelSelector.Visible = false;
                nudCantidad.Focus();
            }
        }

        private void CargarProductosEnSelector()
        {
            productosDisponibles = controladoraProducto.ObtenerProductos()
                .Where(p => p.Stock > 0)
                .ToList();

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

        private void LimpiarControlesSelector()
        {
            productoSeleccionado = null;
            nudCantidad.Value = 1;

            // Limpiar label manual de producto seleccionado
            lblProductoSeleccionado.Text = "Ningún producto seleccionado";
            lblProductoSeleccionado.ForeColor = Color.Gray;

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnSelectorProducto_Click(object sender, EventArgs e)
        {
            if (panelSelector.Visible)
            {
                panelSelector.Visible = false;
            }
            else
            {
                // Posicionar el panel cerca de tu botón
                panelSelector.Location = new Point(btnSelectorProducto.Left, btnSelectorProducto.Bottom + 5);

                CargarProductosEnSelector();
                panelSelector.Visible = true;
                panelSelector.BringToFront();

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