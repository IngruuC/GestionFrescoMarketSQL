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
    public partial class FormFavoritosCliente : Form
    {
        private ControladoraProducto controladoraProducto;
        private Cliente clienteActual;
        private ControladoraCliente controladoraCliente;
        private List<Producto> productosFavoritos;

        public FormFavoritosCliente()
        {
            InitializeComponent();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            controladoraCliente = ControladoraCliente.ObtenerInstancia();
            // Buscar el cliente asociado al usuario actual
            clienteActual = ObtenerClienteDelUsuarioActual();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar DataGridView de Favoritos
            dgvFavoritos.AutoGenerateColumns = false;

            // Columnas personalizadas
            var columnaId = new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50
            };
            dgvFavoritos.Columns.Add(columnaId);

            var columnaNombre = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200
            };
            dgvFavoritos.Columns.Add(columnaNombre);

            var columnaPrecio = new DataGridViewTextBoxColumn
            {
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            };
            dgvFavoritos.Columns.Add(columnaPrecio);

            var columnaStock = new DataGridViewTextBoxColumn
            {
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 100
            };
            dgvFavoritos.Columns.Add(columnaStock);

            // Columna de acciones
            var columnaAcciones = new DataGridViewButtonColumn
            {
                HeaderText = "Acciones",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            dgvFavoritos.Columns.Add(columnaAcciones);

            // Cargar productos favoritos
            CargarProductosFavoritos();

            // Configurar eventos
            dgvFavoritos.CellClick += DgvFavoritos_CellClick;
        }

        private void CargarProductosFavoritos()
        {
            try
            {
                // En un escenario real, implementarías una lógica de favoritos 
                // Aquí simularemos con algunos productos
                productosFavoritos = controladoraProducto.ObtenerProductos()
                    .Take(5) // Tomamos los primeros 5 como ejemplo
                    .ToList();

                dgvFavoritos.DataSource = productosFavoritos;

                // Actualizar etiqueta de total
                lblTotalFavoritos.Text = $"Total de Favoritos: {productosFavoritos.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar favoritos: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private Cliente ObtenerClienteDelUsuarioActual()
        {
            // Buscar el cliente con el mismo ID de usuario
            var clientes = controladoraCliente.ObtenerClientes();
            var clienteAsociado = clientes.FirstOrDefault(c => c.UsuarioId == SesionActual.Usuario.Id);

            if (clienteAsociado == null)
            {
                throw new InvalidOperationException("No se encontró un cliente asociado al usuario actual.");
            }

            return clienteAsociado;
        }
        private void DgvFavoritos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en la columna de acciones
            if (e.ColumnIndex == dgvFavoritos.Columns.Count - 1 && e.RowIndex >= 0)
            {
                // Obtener el producto seleccionado
                var producto = (Producto)dgvFavoritos.Rows[e.RowIndex].DataBoundItem;

                // Confirmar eliminación
                var resultado = MessageBox.Show(
                    $"¿Está seguro que desea eliminar {producto.Nombre} de favoritos?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    EliminarFavorito(producto);
                }
            }
        }

        private void EliminarFavorito(Producto producto)
        {
            try
            {
                // En un escenario real, implementarías la lógica de eliminar de favoritos
                productosFavoritos.Remove(producto);

                // Recargar el DataGridView
                dgvFavoritos.DataSource = null;
                dgvFavoritos.DataSource = productosFavoritos;

                // Actualizar etiqueta de total
                lblTotalFavoritos.Text = $"Total de Favoritos: {productosFavoritos.Count}";

                MessageBox.Show("Producto eliminado de favoritos.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar favorito: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AgregarFavorito(Producto producto)
        {
            try
            {
                // Verificar si ya está en favoritos
                if (productosFavoritos.Any(p => p.Id == producto.Id))
                {
                    MessageBox.Show("Este producto ya está en favoritos.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // Agregar a favoritos
                productosFavoritos.Add(producto);

                // Recargar el DataGridView
                dgvFavoritos.DataSource = null;
                dgvFavoritos.DataSource = productosFavoritos;

                // Actualizar etiqueta de total
                lblTotalFavoritos.Text = $"Total de Favoritos: {productosFavoritos.Count}";

                MessageBox.Show("Producto agregado a favoritos.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar favorito: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnAgregarFavorito_Click_1(object sender, EventArgs e)
        {
            // Abrir formulario para agregar productos a favoritos
            using (var formBuscarProducto = new FormBuscarProducto())
            {
                if (formBuscarProducto.ShowDialog() == DialogResult.OK)
                {
                    var productoSeleccionado = formBuscarProducto.ProductoSeleccionado;
                    if (productoSeleccionado != null)
                    {
                        AgregarFavorito(productoSeleccionado);
                    }
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Formulario para buscar y seleccionar productos
    public partial class FormBuscarProducto : Form
    {
        private ControladoraProducto controladoraProducto;
        public Producto ProductoSeleccionado { get; private set; }

        public FormBuscarProducto()
        {
            InitializeComponent();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                var productos = controladoraProducto.ObtenerProductos();
                dgvProductos.DataSource = productos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            ProductoSeleccionado = (Producto)dgvProductos.SelectedRows[0].DataBoundItem;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {
            // Configuración del formulario
            this.Text = "Buscar Producto";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            // DataGridView de Productos
            this.dgvProductos = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 300,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Configurar columnas
            this.dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50
            });
            this.dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200
            });
            this.dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            // Botones
            this.btnSeleccionar = new Button
            {
                Text = "Seleccionar",
                Location = new System.Drawing.Point(350, 320),
                Size = new System.Drawing.Size(100, 30)
            };
            this.btnSeleccionar.Click += btnSeleccionar_Click;

            this.btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new System.Drawing.Point(460, 320),
                Size = new System.Drawing.Size(100, 30)
            };
            this.btnCancelar.Click += btnCancelar_Click;

            // Agregar controles
            this.Controls.Add(dgvProductos);
            this.Controls.Add(btnSeleccionar);
            this.Controls.Add(btnCancelar);
        }

        // Controles
        private DataGridView dgvProductos;
        private Button btnSeleccionar;
        private Button btnCancelar;
    }
}
