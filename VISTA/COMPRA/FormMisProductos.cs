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
    public partial class FormMisProductos : Form
    {
        private Proveedor proveedor;
        private ControladoraProducto controladoraProducto;
        private GestorRelacionProveedorProducto gestorRelacion;
        private List<Producto> productos = new List<Producto>();

        // Constructor que recibe un proveedor
        public FormMisProductos(Proveedor proveedor)
        {
            InitializeComponent();
            this.proveedor = proveedor;
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            gestorRelacion = GestorRelacionProveedorProducto.ObtenerInstancia();
            ConfigurarDataGridView();
            ConfigurarFormulario();
            CargarProductos();
        }

        // Constructor sin parámetros (para compatibilidad)
        public FormMisProductos()
        {
            InitializeComponent();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            gestorRelacion = GestorRelacionProveedorProducto.ObtenerInstancia();
            ConfigurarDataGridView();
        }

        // Método para configurar el proveedor después de la inicialización
        public void ConfigurarProveedor(Proveedor proveedorActual)
        {
            this.proveedor = proveedorActual;
            ConfigurarFormulario();
            CargarProductos();
        }

        private void ConfigurarFormulario()
        {
            if (proveedor != null)
            {
                this.Text = $"Mis Productos - {proveedor.RazonSocial}";

            }
        }

        private void ConfigurarDataGridView()
        {
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;

            // Añadir columnas
            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50,
                ReadOnly = true
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Codigo",
                HeaderText = "Código",
                DataPropertyName = "CodigoBarra",
                Width = 100,
                ReadOnly = true
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200,
                ReadOnly = true
            });

            dgvProductos.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "EsPerecedero",
                HeaderText = "Perecedero",
                DataPropertyName = "EsPerecedero",
                Width = 80,
                ReadOnly = true
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaVencimiento",
                HeaderText = "Vencimiento",
                DataPropertyName = "FechaVencimiento",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Precio",
                HeaderText = "Precio",
                DataPropertyName = "Precio",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Stock",
                DataPropertyName = "Stock",
                Width = 80,
                ReadOnly = true
            });
        }

        private void CargarProductos()
        {
            try
            {
                if (proveedor == null)
                {
                    MessageBox.Show("No se ha configurado un proveedor para este formulario.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var todosLosProductos = controladoraProducto.ObtenerProductos();
                productos = gestorRelacion.ObtenerProductosDeProveedor(proveedor.Id, todosLosProductos);

                dgvProductos.DataSource = null;
                dgvProductos.DataSource = productos;

                // Mostrar cantidad de productos
                lblCantidadProductos.Text = $"{productos.Count} producto(s) en lista";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarProductos()
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(filtro))
            {
                dgvProductos.DataSource = productos;
                return;
            }

            var productosFiltrados = productos.Where(p =>
                p.Nombre.ToLower().Contains(filtro) ||
                p.CodigoBarra.ToLower().Contains(filtro) ||
                p.Id.ToString().Contains(filtro)
            ).ToList();

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productosFiltrados;
            lblCantidadProductos.Text = $"{productosFiltrados.Count} producto(s) en lista";
        }







        private void btnActualizarLista_Click_1(object sender, EventArgs e)
        {
            CargarProductos();
            txtBuscar.Clear();
        }

        private void btnAsignarProductos_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FiltrarProductos();
        }
    }
}