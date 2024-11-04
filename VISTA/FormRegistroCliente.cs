using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using CONTROLADORA;
using ENTIDADES;

namespace VISTA
{
    public partial class FormRegistroCliente : Form
    {
        private readonly ControladoraCliente _controladoraCliente;
        private Color primaryColor = Color.FromArgb(31, 30, 68);
        private Color secondaryColor = Color.FromArgb(34, 33, 74);
        private Color accentColor = Color.FromArgb(172, 126, 241);
        private bool isEditing = false;

        public FormRegistroCliente()
        {
            InitializeComponent();
            _controladoraCliente = new ControladoraCliente();
            ConfigurarFormulario();
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

            // Suscribir eventos
            txtBuscar.TextChanged += TxtBuscar_TextChanged;
            dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;

            // Tooltips
            ConfigurarTooltips();
        }

        private void ConfigurarPaneles()
        {
            panelDatos.BackColor = primaryColor;
            panelDatos.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panelDatos.ClientRectangle,
                    Color.FromArgb(60, 60, 60), ButtonBorderStyle.Solid);
            };
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
            foreach (TextBox txt in new[] { txtDocumento, txtNombre, txtApellido, txtDireccion, txtBuscar })
            {
                txt.BackColor = Color.FromArgb(45, 44, 85);
                txt.ForeColor = Color.Gainsboro;
                txt.Font = new Font("Segoe UI", 9F);
                txt.BorderStyle = BorderStyle.FixedSingle;

                // Efectos focus
                txt.Enter += (s, e) =>
                {
                    var textBox = (TextBox)s;
                    textBox.BackColor = Color.FromArgb(55, 54, 95);
                };
                txt.Leave += (s, e) =>
                {
                    var textBox = (TextBox)s;
                    textBox.BackColor = Color.FromArgb(45, 44, 85);
                };
            }
        }

        private void ConfigurarDataGridView()
        {
            dgvClientes.BackgroundColor = secondaryColor;
            dgvClientes.BorderStyle = BorderStyle.None;
            dgvClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvClientes.EnableHeadersVisualStyles = false;

            // Estilo de encabezados
            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvClientes.ColumnHeadersDefaultCellStyle.SelectionBackColor = primaryColor;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvClientes.ColumnHeadersHeight = 40;

            // Estilo de celdas
            dgvClientes.DefaultCellStyle.BackColor = secondaryColor;
            dgvClientes.DefaultCellStyle.ForeColor = Color.Gainsboro;
            dgvClientes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(75, 74, 115);
            dgvClientes.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvClientes.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            // Configurar columnas
            dgvClientes.AutoGenerateColumns = false;
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            dgvClientes.Columns.Clear();
            dgvClientes.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = "Id",
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Width = 70
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Documento",
                    DataPropertyName = "Documento",
                    HeaderText = "Documento",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    DataPropertyName = "Nombre",
                    HeaderText = "Nombre",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Apellido",
                    DataPropertyName = "Apellido",
                    HeaderText = "Apellido",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Direccion",
                    DataPropertyName = "Direccion",
                    HeaderText = "Dirección",
                    Width = 200
                }
            );
        }

        private void ConfigurarLabels()
        {
            foreach (Label lbl in new[] { lblDocumento, lblNombre, lblApellido, lblDireccion, lblBuscar, lblTitulo })
            {
                lbl.ForeColor = Color.Gainsboro;
                lbl.Font = new Font("Segoe UI", 9F);
            }
            lblTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        }

        private void ConfigurarTooltips()
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(btnGuardar, "Guardar cliente");
            toolTip.SetToolTip(btnModificar, "Modificar cliente seleccionado");
            toolTip.SetToolTip(btnEliminar, "Eliminar cliente seleccionado");
            toolTip.SetToolTip(btnLimpiar, "Limpiar formulario");
            toolTip.SetToolTip(txtBuscar, "Buscar por documento, nombre o apellido");
        }

        private void CargarDatos()
        {
            try
            {
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = _controladoraCliente.ObtenerClientes();
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al cargar los datos: {ex.Message}");
            }
        }

        private void LimpiarCampos()
        {
            txtDocumento.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            txtDocumento.Focus();
            isEditing = false;
            btnGuardar.Text = "Guardar";
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MostrarMensajeError("El documento es requerido");
                txtDocumento.Focus();
                return false;
            }

            if (txtDocumento.Text.Length != 8 || !txtDocumento.Text.All(char.IsDigit))
            {
                MostrarMensajeError("El documento debe tener 8 dígitos");
                txtDocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarMensajeError("El nombre es requerido");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MostrarMensajeError("El apellido es requerido");
                txtApellido.Focus();
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
                var cliente = new Cliente
                {
                    Documento = txtDocumento.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                if (isEditing)
                {
                    cliente.Id = (int)dgvClientes.SelectedRows[0].Cells["Id"].Value;
                    _controladoraCliente.ActualizarCliente(cliente);
                    MostrarMensajeExito("Cliente actualizado exitosamente");
                }
                else
                {
                    _controladoraCliente.AgregarCliente(cliente);
                    MostrarMensajeExito("Cliente registrado exitosamente");
                }

                CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MostrarMensajeError($"Error al procesar el cliente: {ex.Message}");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MostrarMensajeError("Seleccione un cliente para modificar");
                return;
            }

            var cliente = (Cliente)dgvClientes.SelectedRows[0].DataBoundItem;
            txtDocumento.Text = cliente.Documento;
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtDireccion.Text = cliente.Direccion;

            isEditing = true;
            btnGuardar.Text = "Actualizar";
            txtDocumento.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MostrarMensajeError("Seleccione un cliente para eliminar");
                return;
            }

            var cliente = (Cliente)dgvClientes.SelectedRows[0].DataBoundItem;
            var confirmacion = MessageBox.Show(
                $"¿Está seguro que desea eliminar al cliente {cliente.Nombre} {cliente.Apellido}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _controladoraCliente.EliminarCliente(cliente.Id);
                    CargarDatos();
                    LimpiarCampos();
                    MostrarMensajeExito("Cliente eliminado exitosamente");
                }
                catch (Exception ex)
                {
                    MostrarMensajeError($"Error al eliminar el cliente: {ex.Message}");
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
                var clientesFiltrados = _controladoraCliente.ObtenerClientes()
                    .Where(c => c.Documento.ToLower().Contains(filtro) ||
                               c.Nombre.ToLower().Contains(filtro) ||
                               c.Apellido.ToLower().Contains(filtro))
                    .ToList();

                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clientesFiltrados;

                if (clientesFiltrados.Count == 0 && !string.IsNullOrEmpty(filtro))
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

        private void DgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            btnModificar.Enabled = dgvClientes.SelectedRows.Count > 0;
            btnEliminar.Enabled = dgvClientes.SelectedRows.Count > 0;
        }

        private void FormRegistroCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}