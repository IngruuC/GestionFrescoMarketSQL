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
    public partial class FormAsignarProductos : Form
    {
        private int proveedorId;
        private string proveedorNombre;
        private ControladoraProducto controladoraProducto;
        private GestorRelacionProveedorProducto gestorRelacion;

        // Listas para manejar los productos
        private List<Producto> productosDisponibles;
        private List<Producto> productosAsignados;

        public FormAsignarProductos(int proveedorId, string proveedorNombre)
        {
            InitializeComponent();
            this.proveedorId = proveedorId;
            this.proveedorNombre = proveedorNombre;
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            gestorRelacion = GestorRelacionProveedorProducto.ObtenerInstancia();

            ConfigurarFormulario();
            CargarProductos();
        }

        private void ConfigurarFormulario()
        {
            // Configurar el título del formulario
            this.Text = $"Asignar Productos a {proveedorNombre}";
            lblTitulo.Text = $"Asignar Productos a: {proveedorNombre}";

            // Configurar ListBox productos disponibles
            lstProductosDisponibles.DisplayMember = "Nombre";
            lstProductosDisponibles.ValueMember = "Id";

            // Configurar ListBox productos asignados
            lstProductosAsignados.DisplayMember = "Nombre";
            lstProductosAsignados.ValueMember = "Id";
        }

        private void CargarProductos()
        {
            try
            {
                // Obtener todos los productos
                var todosLosProductos = controladoraProducto.ObtenerProductos();

                // Obtener IDs de productos ya asignados al proveedor
                var idsProductosAsignados = gestorRelacion.ObtenerIdsProductosDeProveedor(proveedorId);

                // Obtener los productos ya asignados
                productosAsignados = todosLosProductos
                    .Where(p => idsProductosAsignados.Contains(p.Id))
                    .ToList();

                // Obtener los productos disponibles (no asignados)
                productosDisponibles = todosLosProductos
                    .Where(p => !idsProductosAsignados.Contains(p.Id))
                    .ToList();

                // Actualizar los ListBox
                ActualizarListBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarListBoxes()
        {
            // Actualizar ListBox de productos disponibles
            lstProductosDisponibles.DataSource = null;
            lstProductosDisponibles.DataSource = productosDisponibles;

            // Actualizar ListBox de productos asignados
            lstProductosAsignados.DataSource = null;
            lstProductosAsignados.DataSource = productosAsignados;
        }


        private void btnAsignarProducto_Click(object sender, EventArgs e)
        {
            if (lstProductosDisponibles.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto para asignar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Obtener el producto seleccionado
                var producto = (Producto)lstProductosDisponibles.SelectedItem;

                // Asignar el producto al proveedor
                gestorRelacion.AsignarProductoAProveedor(proveedorId, producto.Id, producto.Nombre);

                // Actualizar nuestras listas locales
                productosDisponibles.Remove(producto);
                productosAsignados.Add(producto);

                // Actualizar los ListBox
                ActualizarListBoxes();

                MessageBox.Show($"Producto '{producto.Nombre}' asignado correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al asignar producto: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            if (lstProductosAsignados.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto para quitar",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Obtener el producto seleccionado
                var producto = (Producto)lstProductosAsignados.SelectedItem;

                // Quitar el producto del proveedor
                gestorRelacion.QuitarProductoDeProveedor(proveedorId, producto.Id);

                // Actualizar nuestras listas locales
                productosAsignados.Remove(producto);
                productosDisponibles.Add(producto);

                // Actualizar los ListBox
                ActualizarListBoxes();

                MessageBox.Show($"Producto '{producto.Nombre}' removido correctamente",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al quitar producto: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
