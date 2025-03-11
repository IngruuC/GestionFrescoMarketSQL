using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTIDADES;
using CONTROLADORA;
using VISTA;
using VISTA.COMPRA;

namespace VISTA.COMPRA
{
    public partial class FormVistaPrincipalProveedor : Form
    {
        private Proveedor proveedorActual;
        private ControladoraProveedor controladoraProveedor;
        private ControladoraCompra controladoraCompra;
        private ControladoraProducto controladoraProducto;
        private GestorRelacionProveedorProducto gestorRelacion;

        public FormVistaPrincipalProveedor()
        {
            InitializeComponent();
            controladoraProveedor = ControladoraProveedor.ObtenerInstancia();
            controladoraCompra = ControladoraCompra.ObtenerInstancia();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            gestorRelacion = GestorRelacionProveedorProducto.ObtenerInstancia();

            CargarProveedorActual();
            ConfigurarInterfaz();
            CargarDatosEstadisticas();
        }

        private void CargarProveedorActual()
        {
            try
            {
                // Obtenemos el usuario actual de la sesión
                var usuarioActual = SesionActual.Usuario;
                if (usuarioActual == null)
                {
                    MessageBox.Show("No hay un usuario logueado", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Obtenemos todos los proveedores y buscamos el que esté asociado al usuario actual
                var proveedores = controladoraProveedor.ObtenerProveedores();
                proveedorActual = proveedores.FirstOrDefault(p => p.UsuarioId == usuarioActual.Id);

                if (proveedorActual == null)
                {
                    MessageBox.Show("No se encontró un proveedor asociado al usuario actual",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del proveedor: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ConfigurarInterfaz()
        {
            if (proveedorActual != null)
            {
                this.Text = $"Panel de Proveedor - {proveedorActual.RazonSocial}";
                lblBienvenido.Text = $"Bienvenido, {proveedorActual.RazonSocial}";
            }
        }


        private void CargarDatosEstadisticas()
        {
            if (proveedorActual == null) return;

            try
            {
                // Obtener todas las compras realizadas a este proveedor
                var compras = controladoraCompra.ObtenerCompras()
                    .Where(c => c.ProveedorId == proveedorActual.Id)
                    .ToList();

                // Calcular total de ventas
                decimal totalVentas = compras.Sum(c => c.Total);
                lblTotalVentas.Text = $"${totalVentas:N2}";

                // Obtener productos del proveedor
                var todosLosProductos = controladoraProducto.ObtenerProductos();
                var productosProveedor = gestorRelacion.ObtenerProductosDeProveedor(proveedorActual.Id, todosLosProductos);
                lblTotalProductos.Text = productosProveedor.Count.ToString();

                // Calcular pedidos recientes (últimos 30 días)
                var comprasRecientes = compras.Where(c => c.FechaCompra >= DateTime.Now.AddDays(-30)).ToList();
                lblPedidosRecientes.Text = comprasRecientes.Count.ToString();

                // Cargar compras recientes en el DataGridView
                DataTable dtCompras = new DataTable();
                dtCompras.Columns.Add("Fecha", typeof(string));
                dtCompras.Columns.Add("Total", typeof(string));
                dtCompras.Columns.Add("FormaPago", typeof(string));

                foreach (var compra in comprasRecientes.OrderByDescending(c => c.FechaCompra).Take(5))
                {
                    dtCompras.Rows.Add(
                        compra.FechaCompra.ToString("dd/MM/yyyy"),
                        $"${compra.Total:N2}",
                        compra.FormaPago
                    );
                }

                dgvComprasRecientes.DataSource = dtCompras;

                // Cargar productos más vendidos en el DataGridView
                DataTable dtProductos = new DataTable();
                dtProductos.Columns.Add("Producto", typeof(string));
                dtProductos.Columns.Add("Cantidad", typeof(string));

                var productosVendidos = new Dictionary<int, int>(); // ProductoId, Cantidad
                foreach (var compra in compras)
                {
                    foreach (var detalle in compra.Detalles)
                    {
                        if (!productosVendidos.ContainsKey(detalle.ProductoId))
                            productosVendidos[detalle.ProductoId] = 0;

                        productosVendidos[detalle.ProductoId] += detalle.Cantidad;
                    }
                }

                foreach (var kvp in productosVendidos.OrderByDescending(x => x.Value).Take(5))
                {
                    var producto = controladoraProducto.ObtenerProductoPorId(kvp.Key);
                    if (producto != null)
                    {
                        dtProductos.Rows.Add(
                            producto.Nombre,
                            $"{kvp.Value} unidades"
                        );
                    }
                }

                dgvProductosMasVendidos.DataSource = dtProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar estadísticas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMiPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                if (proveedorActual == null) return;

                var formPerfil = new FormPerfilProveedor();
                formPerfil.ConfigurarProveedor(proveedorActual);
                formPerfil.ShowDialog();

                // Después de cerrar el formulario, recargamos el proveedor por si ha habido cambios
                CargarProveedorActual();
                ConfigurarInterfaz();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir perfil: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                if (proveedorActual == null) return;

                using (var formHistorial = new FormHistorialCompras(proveedorActual))
                {
                    formHistorial.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir historial: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                    "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    SesionActual.CerrarSesion();
                    Login loginForm = new Login();
                    this.Hide(); // Oculta el formulario actual
                    DialogResult loginResult = loginForm.ShowDialog();

                    if (loginResult != DialogResult.OK)
                    {
                        this.Close(); // Cierra el formulario actual si no se inició sesión
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMisProductos_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (proveedorActual == null) return;

                using (var formProductos = new FormMisProductos(proveedorActual))
                {
                    formProductos.ShowDialog();
                    // Recargar datos después de cerrar
                    CargarDatosEstadisticas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir productos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            try
            {
                if (proveedorActual == null) return;

                using (var formReportes = new FormReportesProveedor(proveedorActual))
                {
                    formReportes.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir reportes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}