using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CONTROLADORA;
using ENTIDADES;

namespace VISTA
{
    public partial class FormComprasCliente : Form
    {
        private ControladoraVenta controladoraVenta;
        private ControladoraProducto controladoraProducto;
        private ControladoraCliente controladoraCliente;
        private Cliente clienteActual;

        public FormComprasCliente()
        {
            InitializeComponent();
            controladoraVenta = ControladoraVenta.ObtenerInstancia();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            controladoraCliente = ControladoraCliente.ObtenerInstancia();
            // Buscar el cliente asociado al usuario actual
            clienteActual = ObtenerClienteDelUsuarioActual();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar DataGridView
            dgvCompras.AutoGenerateColumns = false;

            // Columnas personalizadas
            var columnaId = new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50
            };
            dgvCompras.Columns.Add(columnaId);

            var columnaFecha = new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha",
                DataPropertyName = "FechaVenta",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            };
            dgvCompras.Columns.Add(columnaFecha);

            var columnaFormaPago = new DataGridViewTextBoxColumn
            {
                HeaderText = "Forma de Pago",
                DataPropertyName = "FormaPago",
                Width = 100
            };
            dgvCompras.Columns.Add(columnaFormaPago);

            var columnaTotal = new DataGridViewTextBoxColumn
            {
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            };
            dgvCompras.Columns.Add(columnaTotal);

            // Configurar valores iniciales de fechas
            dtpFechaDesde.Value = DateTime.Today.AddMonths(-1);
            dtpFechaHasta.Value = DateTime.Today;

            // Cargar compras del cliente
            CargarComprasCliente();
        }

        private void CargarComprasCliente()
        {
            try
            {
                // Obtener todas las ventas del cliente
                var compras = controladoraVenta.ObtenerVentas()
                    .Where(v => v.ClienteId == clienteActual.Id)
                    .OrderByDescending(v => v.FechaVenta)
                    .ToList();

                // Configurar DataSource
                dgvCompras.DataSource = compras;

                // Actualizar totales
                decimal totalCompras = compras.Sum(v => v.Total);
                lblTotalCompras.Text = $"Total de Compras: {compras.Count}";
                lblMontoTotal.Text = $"Monto Total: ${totalCompras:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las compras: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private void MostrarDetalleCompra(Venta venta)
        {
            var mensaje = new StringBuilder();
            mensaje.AppendLine($"DETALLE DE VENTA - ID: {venta.Id}");
            mensaje.AppendLine($"Fecha: {venta.FechaVenta:dd/MM/yyyy HH:mm}");
            mensaje.AppendLine($"Forma de Pago: {venta.FormaPago}");
            mensaje.AppendLine("\nPRODUCTOS:");
            mensaje.AppendLine("------------------------------------------------");

            foreach (var detalle in venta.Detalles)
            {
                mensaje.AppendLine($"- {detalle.ProductoNombre}");
                mensaje.AppendLine($"  Cantidad: {detalle.Cantidad}");
                mensaje.AppendLine($"  Precio Unitario: ${detalle.PrecioUnitario:N2}");
                mensaje.AppendLine($"  Subtotal: ${detalle.Subtotal:N2}");

                var producto = controladoraProducto.ObtenerProductoPorId(detalle.ProductoId);
                if (producto.EsPerecedero && producto.FechaVencimiento.HasValue)
                {
                    mensaje.AppendLine($"  Vencimiento: {producto.FechaVencimiento.Value:dd/MM/yyyy}");
                }
                mensaje.AppendLine();
            }

            mensaje.AppendLine("------------------------------------------------");
            mensaje.AppendLine($"TOTAL: ${venta.Total:N2}");

            MessageBox.Show(mensaje.ToString(), "Detalle de Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnFiltrarCompras_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Obtener compras del cliente filtradas por fechas
                var compras = controladoraVenta.ObtenerVentas()
                    .Where(v => v.ClienteId == clienteActual.Id &&
                                v.FechaVenta.Date >= dtpFechaDesde.Value.Date &&
                                v.FechaVenta.Date <= dtpFechaHasta.Value.Date)
                    .OrderByDescending(v => v.FechaVenta)
                    .ToList();

                // Configurar DataSource
                dgvCompras.DataSource = compras;

                // Actualizar totales
                decimal totalCompras = compras.Sum(v => v.Total);
                lblTotalCompras.Text = $"Total de Compras: {compras.Count}";
                lblMontoTotal.Text = $"Monto Total: ${totalCompras:N2}";

                // Mostrar mensaje si no hay compras
                if (compras.Count == 0)
                {
                    MessageBox.Show("No se encontraron compras en el rango de fechas seleccionado.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar las compras: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarFiltro_Click_1(object sender, EventArgs e)
        {
            // Restablecer fechas
            dtpFechaDesde.Value = DateTime.Today.AddMonths(-1);
            dtpFechaHasta.Value = DateTime.Today;

            // Recargar todas las compras
            CargarComprasCliente();
        }

        private void btnVerDetalleCompra_Click_1(object sender, EventArgs e)
        {
            // Verificar si hay una compra seleccionada
            if (dgvCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una compra para ver los detalles.",
                    "Información",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Obtener la venta seleccionada
            var ventaSeleccionada = (Venta)dgvCompras.SelectedRows[0].DataBoundItem;
            MostrarDetalleCompra(ventaSeleccionada);
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}