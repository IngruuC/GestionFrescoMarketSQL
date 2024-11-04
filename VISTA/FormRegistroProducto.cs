using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.ComponentModel;
using CONTROLADORA;
using ENTIDADES;

namespace VISTA
{
    public partial class FormRegistroProducto : Form
    {
        private readonly ControladoraProducto _controladoraProducto;
        private Color primaryColor = Color.FromArgb(87, 12, 102);   // Púrpura oscuro
        private Color secondaryColor = Color.FromArgb(70, 15, 75);  // Púrpura más oscuro
        private Color accentColor = Color.FromArgb(249, 118, 176);  // Rosa
        private bool isEditing = false;

        public FormRegistroProducto()
        {
            InitializeComponent();
            _controladoraProducto = new ControladoraProducto();
            ConfigurarFormulario();
            CargarDatos();
        }

        private void FormRegistroProducto_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void ConfigurarFormulario()
        {
            // Configuración general
            this.BackColor = secondaryColor;
            ConfigurarPaneles();
            ConfigurarBotones();
            ConfigurarTextBoxes();
            ConfigurarDataGridView();
            ConfigurarLabels();
            ConfigurarControlesEspeciales();

            // Suscribir eventos
            txtBuscar.TextChanged += TxtBuscar_TextChanged;
            dgvProductos.SelectionChanged += DgvProductos_SelectionChanged;
            chkEsPerecedero.CheckedChanged += ChkEsPerecedero_CheckedChanged;

            // Tooltips
            ConfigurarTooltips();
        }

        private void ConfigurarPaneles()
        {
            panelDatos.BackColor = primaryColor;
            panelDatos.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panelDatos.ClientRectangle,
                    Color.FromArgb(100, 70, 100), ButtonBorderStyle.Solid);
            };
        }

        private void ConfigurarControlesEspeciales()
        {
            // Configurar DateTimePicker
            dtpFechaVencimiento.Enabled = false;
            dtpFechaVencimiento.Format = DateTimePickerFormat.Short;
            dtpFechaVencimiento.Value = DateTime.Today.AddDays(1);

            // Configurar CheckBox
            chkEsPerecedero.ForeColor = Color.Gainsboro;
            chkEsPerecedero.BackColor = Color.Transparent;
        }

        private void ConfigurarBotones()
        {
            foreach (Button btn in new[] { btnGuardar, btnModificar, btnEliminar, btnLimpiar })
            {
                btn.BackColor = primaryColor;
                btn.ForeColor = Color.Gainsboro;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;

                // Efectos hover
                btn.MouseEnter += (s, e) =>
                {
                    var button = (Button)s;
                    button.BackColor = accentColor;
                };
                btn.MouseLeave += (s, e) =>
                {
                    var button = (Button)s;
                    button.BackColor = primaryColor;
                };
            }
        }

        private void ConfigurarTextBoxes()
        {
            foreach (TextBox txt in new[] { txtCodigoBarra, txtNombre, txtPrecio, txtStock, txtBuscar })
            {
                txt.BackColor = Color.FromArgb(95, 20, 100);
                txt.ForeColor = Color.Gainsboro;
                txt.Font = new Font("Segoe UI", 9F);
                txt.BorderStyle = BorderStyle.FixedSingle;

                // Efectos focus
                txt.Enter += (s, e) =>
                {
                    var textBox = (TextBox)s;
                    textBox.BackColor = Color.FromArgb(105, 25, 110);
                };
                txt.Leave += (s, e) =>
                {
                    var textBox = (TextBox)s;
                    textBox.BackColor = Color.FromArgb(95, 20, 100);
                };
            }

            // Validación específica para campos numéricos
            txtPrecio.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;
                if (e.KeyChar == '.' && ((TextBox)s).Text.Contains("."))
                    e.Handled = true;
            };

            txtStock.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };
        }

        /*private void ConfigurarDataGridView()
        {
            dgvProductos.BackgroundColor = secondaryColor;
            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProductos.EnableHeadersVisualStyles = false;

            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvProductos.ColumnHeadersDefaultCellStyle.SelectionBackColor = primaryColor;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvProductos.ColumnHeadersHeight = 40;

            dgvProductos.DefaultCellStyle.BackColor = secondaryColor;
            dgvProductos.DefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvProductos.DefaultCellStyle.SelectionBackColor = accentColor;
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvProductos.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            dgvProductos.AutoGenerateColumns = false; 
            ConfigurarColumnas();
        } */

        private void ConfigurarColumnas()
        {
            dgvProductos.Columns.Clear();
            dgvProductos.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = "Id",
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Width = 70
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "CodigoBarra",
                    DataPropertyName = "CodigoBarra",
                    HeaderText = "Código",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    DataPropertyName = "Nombre",
                    HeaderText = "Nombre",
                    Width = 200
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Precio",
                    DataPropertyName = "Precio",
                    HeaderText = "Precio",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "C2",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Stock",
                    DataPropertyName = "Stock",
                    HeaderText = "Stock",
                    Width = 80,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                },
                new DataGridViewCheckBoxColumn
                {
                    Name = "EsPerecedero",
                    DataPropertyName = "EsPerecedero",
                    HeaderText = "Perecedero",
                    Width = 80
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "FechaVencimiento",
                    DataPropertyName = "FechaVencimiento",
                    HeaderText = "Vencimiento",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "d"
                    }
                }
            );
        }

        private void ChkEsPerecedero_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaVencimiento.Enabled = chkEsPerecedero.Checked;
        }

        private void ConfigurarLabels()
        {
            foreach (Label lbl in new[] { lblCodigoBarra, lblNombre, lblPrecio, lblStock, lblBuscar, lblTitulo, lblFechaVencimiento })
            {
                lbl.ForeColor = Color.Gainsboro;
                lbl.Font = new Font("Segoe UI", 9F);
            }
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitulo.ForeColor = accentColor;
        }

        private void ConfigurarTooltips()
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtCodigoBarra, "Ingrese el código de barras del producto (8 dígitos)");
            toolTip.SetToolTip(txtPrecio, "Ingrese el precio sin símbolos");
            toolTip.SetToolTip(txtStock, "Ingrese la cantidad disponible");
            toolTip.SetToolTip(chkEsPerecedero, "Marque si el producto tiene fecha de vencimiento");
            toolTip.SetToolTip(btnGuardar, "Guardar producto");
            toolTip.SetToolTip(btnModificar, "Modificar producto seleccionado");
            toolTip.SetToolTip(btnEliminar, "Eliminar producto seleccionado");
            toolTip.SetToolTip(btnLimpiar, "Limpiar formulario");
        }

        private void CargarDatos()
        {
            try
            {
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = _controladoraProducto.ObtenerProductos();
                AplicarColorStockBajo();
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al cargar los datos: {ex.Message}");
            }
        }

        private void AplicarColorStockBajo()
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                int stock = Convert.ToInt32(row.Cells["Stock"].Value);
                if (stock < 10) // Stock bajo
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(150, 30, 30);
                }
            }
        }

        private void LimpiarCampos()
        {
            txtCodigoBarra.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            chkEsPerecedero.Checked = false;
            dtpFechaVencimiento.Value = DateTime.Today.AddDays(1);
            dtpFechaVencimiento.Enabled = false;
            txtCodigoBarra.Focus();
            isEditing = false;
            btnGuardar.Text = "Guardar";
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigoBarra.Text))
            {
                MostrarMensajeError("El código de barras es requerido");
                txtCodigoBarra.Focus();
                return false;
            }

            if (txtCodigoBarra.Text.Length != 8 || !txtCodigoBarra.Text.All(char.IsDigit))
            {
                MostrarMensajeError("El código de barras debe tener 8 dígitos");
                txtCodigoBarra.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarMensajeError("El nombre es requerido");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || !decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MostrarMensajeError("Ingrese un precio válido mayor a 0");
                txtPrecio.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStock.Text) || !int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MostrarMensajeError("Ingrese una cantidad válida de stock");
                txtStock.Focus();
                return false;
            }

            if (chkEsPerecedero.Checked && dtpFechaVencimiento.Value.Date <= DateTime.Today)
            {
                MostrarMensajeError("La fecha de vencimiento debe ser posterior a hoy");
                dtpFechaVencimiento.Focus();
                return false;
            }

            return true;
        }

        private void MostrarMensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarMensajeExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                var producto = new Producto
                {
                    CodigoBarra = txtCodigoBarra.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text),
                    EsPerecedero = chkEsPerecedero.Checked,
                    FechaVencimiento = chkEsPerecedero.Checked ? dtpFechaVencimiento.Value : (DateTime?)null
                };

                if (isEditing)
                {
                    producto.Id = (int)dgvProductos.SelectedRows[0].Cells["Id"].Value;
                    _controladoraProducto.ActualizarProducto(producto);
                    MostrarMensajeExito("Producto actualizado exitosamente");
                }
                else
                {
                    _controladoraProducto.AgregarProducto(producto);
                    MostrarMensajeExito("Producto registrado exitosamente");
                }

                CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al procesar el producto: {ex.Message}");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MostrarMensajeError("Seleccione un producto para modificar");
                return;
            }

            var producto = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            txtCodigoBarra.Text = producto.CodigoBarra;
            txtNombre.Text = producto.Nombre;
            txtPrecio.Text = producto.Precio.ToString("N2");
            txtStock.Text = producto.Stock.ToString();
            chkEsPerecedero.Checked = producto.EsPerecedero;
            if (producto.FechaVencimiento.HasValue)
                dtpFechaVencimiento.Value = producto.FechaVencimiento.Value;

            isEditing = true;
            btnGuardar.Text = "Actualizar";
            txtCodigoBarra.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MostrarMensajeError("Seleccione un producto para eliminar");
                return;
            }

            var producto = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            var confirmacion = MessageBox.Show(
                $"¿Está seguro que desea eliminar el producto '{producto.Nombre}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _controladoraProducto.EliminarProducto(producto.Id);
                    CargarDatos();
                    LimpiarCampos();
                    MostrarMensajeExito("Producto eliminado exitosamente");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al eliminar el producto: {ex.Message}");
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBuscar.Text.ToLower();
                var productosFiltrados = _controladoraProducto.ObtenerProductos()
                    .Where(p => p.CodigoBarra.ToLower().Contains(filtro) ||
                               p.Nombre.ToLower().Contains(filtro))
                    .ToList();

                dgvProductos.DataSource = null;
                dgvProductos.DataSource = productosFiltrados;
                AplicarColorStockBajo();

                if (productosFiltrados.Count == 0 && !string.IsNullOrEmpty(filtro))
                {
                    lblResultadoBusqueda.Text = "No se encontraron resultados";
                    lblResultadoBusqueda.Visible = true;
                }
                else
                {
                    lblResultadoBusqueda.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al realizar la búsqueda: {ex.Message}");
            }
        }

        private void DgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            btnModificar.Enabled = dgvProductos.SelectedRows.Count > 0;
            btnEliminar.Enabled = dgvProductos.SelectedRows.Count > 0;
        }
    }
}