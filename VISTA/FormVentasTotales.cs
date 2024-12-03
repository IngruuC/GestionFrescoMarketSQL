using CONTROLADORA;
using ENTIDADES;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormVentasTotales : Form
    {
        private ControladoraVenta controladoraVenta;
        private ControladoraProducto controladoraProducto;

        public FormVentasTotales()
        {
            InitializeComponent();
            controladoraVenta = ControladoraVenta.ObtenerInstancia();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar DateTimePickers
            dtpFechaDesde.Value = DateTime.Today.AddDays(-30);
            dtpFechaHasta.Value = DateTime.Today;
         
            ConfigurarDataGridView();
         
            CargarVentas();
        }

        private void ConfigurarDataGridView()
        {
            dgvVentas.AutoGenerateColumns = false;
            dgvVentas.Columns.Clear();

            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 80,
                ReadOnly = true
            });

            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                DataPropertyName = "FechaVenta",
                HeaderText = "Fecha",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cliente",
                DataPropertyName = "Cliente",
                HeaderText = "Cliente",
                Width = 200,
                ReadOnly = true
            });

            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FormaPago",
                DataPropertyName = "FormaPago",
                HeaderText = "Forma de Pago",
                Width = 150,
                ReadOnly = true
            });

            dgvVentas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                DataPropertyName = "Total",
                HeaderText = "Total",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private void CargarVentas()
        {
            try
            {
                var ventas = controladoraVenta.ObtenerVentas()
                    .Where(v => v.FechaVenta.Date >= dtpFechaDesde.Value.Date &&
                               v.FechaVenta.Date <= dtpFechaHasta.Value.Date)
                    .OrderByDescending(v => v.FechaVenta)
                    .ToList();

                dgvVentas.DataSource = null;
                dgvVentas.DataSource = ventas;

                decimal totalVentas = ventas.Sum(v => v.Total);
                lblTotalVentas.Text = $"TOTAL: $ {totalVentas:N2}";

                btnDetalleVenta.Enabled = ventas.Any();
                btnEliminarVenta.Enabled = ventas.Any();
                btnEstadisticas.Enabled = ventas.Any();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarVentas()
        {
            try
            {
                string criterio = txtBuscarVenta.Text.Trim().ToLower();
                var ventas = controladoraVenta.ObtenerVentas()
                    .Where(v => v.FechaVenta.Date >= dtpFechaDesde.Value.Date &&
                               v.FechaVenta.Date <= dtpFechaHasta.Value.Date &&
                               (v.Id.ToString().Contains(criterio) ||
                                v.Cliente.Nombre.ToLower().Contains(criterio) ||
                                v.Cliente.Apellido.ToLower().Contains(criterio) ||
                                v.Cliente.Documento.ToLower().Contains(criterio) ||
                                v.FormaPago.ToLower().Contains(criterio)))
                    .OrderByDescending(v => v.FechaVenta)
                    .ToList();

                dgvVentas.DataSource = null;
                dgvVentas.DataSource = ventas;

                decimal totalVentas = ventas.Sum(v => v.Total);
                lblTotalVentas.Text = $"TOTAL: $ {totalVentas:N2}";

                if (ventas.Count == 0)
                {
                    MessageBox.Show("No se encontraron ventas que coincidan con los criterios de búsqueda.",
                                  "Búsqueda",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar las ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalleVenta(Venta venta)
        {
            var mensaje = new StringBuilder();
            mensaje.AppendLine($"DETALLE DE VENTA - ID: {venta.Id}");
            mensaje.AppendLine($"Fecha: {venta.FechaVenta:dd/MM/yyyy HH:mm}");
            mensaje.AppendLine($"Cliente: {venta.Cliente}");
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

            MessageBox.Show(mensaje.ToString(), "Detalle de Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarEstadisticas()
        {
            try
            {
                var ventas = controladoraVenta.ObtenerVentas()
                    .Where(v => v.FechaVenta.Date >= dtpFechaDesde.Value.Date &&
                               v.FechaVenta.Date <= dtpFechaHasta.Value.Date)
                    .ToList();

                if (!ventas.Any())
                {
                    MessageBox.Show("No hay ventas en el período seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var mensaje = new StringBuilder();
                mensaje.AppendLine("ESTADÍSTICAS DE VENTAS");
                mensaje.AppendLine($"Período: {dtpFechaDesde.Value:dd/MM/yyyy} - {dtpFechaHasta.Value:dd/MM/yyyy}");
                mensaje.AppendLine("------------------------------------------------");

                // Totales generales
                decimal totalVentas = ventas.Sum(v => v.Total);
                int cantidadVentas = ventas.Count;
                decimal promedioVenta = totalVentas / cantidadVentas;

                mensaje.AppendLine($"Total ventas: ${totalVentas:N2}");
                mensaje.AppendLine($"Cantidad de ventas: {cantidadVentas}");
                mensaje.AppendLine($"Promedio por venta: ${promedioVenta:N2}");
                mensaje.AppendLine();

                // Ventas por forma de pago
                var ventasPorFormaPago = ventas
                    .GroupBy(v => v.FormaPago)
                    .Select(g => new
                    {
                        FormaPago = g.Key,
                        Total = g.Sum(v => v.Total),
                        Cantidad = g.Count()
                    })
                    .OrderByDescending(x => x.Total);

                mensaje.AppendLine("VENTAS POR FORMA DE PAGO:");
                foreach (var grupo in ventasPorFormaPago)
                {
                    mensaje.AppendLine($"{grupo.FormaPago}:");
                    mensaje.AppendLine($"  Total: ${grupo.Total:N2}");
                    mensaje.AppendLine($"  Cantidad: {grupo.Cantidad}");
                }
                mensaje.AppendLine();

                // Productos más vendidos
                var productosMasVendidos = ventas
                    .SelectMany(v => v.Detalles)
                    .GroupBy(d => d.ProductoNombre)
                    .Select(g => new
                    {
                        Producto = g.Key,
                        Cantidad = g.Sum(d => d.Cantidad),
                        Total = g.Sum(d => d.Subtotal)
                    })
                    .OrderByDescending(x => x.Cantidad)
                    .Take(5);

                mensaje.AppendLine("PRODUCTOS MÁS VENDIDOS:");
                foreach (var producto in productosMasVendidos)
                {
                    mensaje.AppendLine($"{producto.Producto}:");
                    mensaje.AppendLine($"  Cantidad: {producto.Cantidad}");
                    mensaje.AppendLine($"  Total: ${producto.Total:N2}");
                }

                MessageBox.Show(mensaje.ToString(), "Estadísticas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar estadísticas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        
            private void btnBuscar_Click(object sender, EventArgs e)
            {
                FiltrarVentas();
            }

            private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
            {
                dtpFechaDesde.Value = DateTime.Today.AddDays(-30);
                dtpFechaHasta.Value = DateTime.Today;
                txtBuscarVenta.Clear();
                CargarVentas();
            }

            private void btnDetalleVenta_Click(object sender, EventArgs e)
            {
                if (dgvVentas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Por favor, seleccione una venta para ver su detalle.",
                                  "Información",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    var venta = (Venta)dgvVentas.SelectedRows[0].DataBoundItem;
                    MostrarDetalleVenta(venta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al mostrar el detalle de la venta: {ex.Message}",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }

            private void btnEliminarVenta_Click(object sender, EventArgs e)
            {
                if (dgvVentas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Por favor, seleccione una venta para eliminar.",
                                  "Información",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;
                }

                var venta = (Venta)dgvVentas.SelectedRows[0].DataBoundItem;
                var resultado = MessageBox.Show($"¿Está seguro que desea eliminar la venta #{venta.Id}?\n\n" +
                                              $"Esta acción restaurará el stock de los productos y no se puede deshacer.",
                                              "Confirmar eliminación",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        controladoraVenta.EliminarVenta(venta.Id);
                        MessageBox.Show("Venta eliminada exitosamente.",
                                      "Éxito",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                        CargarVentas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar la venta: {ex.Message}",
                                      "Error",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                    }
                }
            }

            private void btnEstadisticas_Click(object sender, EventArgs e)
            {
                MostrarEstadisticas();
            }

            private void btnCerrar_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void FormVentasTotales_Load(object sender, EventArgs e)
            {
                CargarVentas();
            }

        private void btnGenerarInforme_Click(object sender, EventArgs e)
        {

            try
            {
                using (var formReportes = new FormReportes())
                {
                    formReportes.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario de reportes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }