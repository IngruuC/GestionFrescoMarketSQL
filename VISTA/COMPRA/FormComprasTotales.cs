using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CONTROLADORA;
using ENTIDADES;

namespace VISTA.COMPRA
{
    public partial class FormComprasTotales : Form
    {
        private ControladoraCompra controladoraCompra;
        private ControladoraProducto controladoraProducto;
        private ControladoraProveedor controladoraProveedor;

        public FormComprasTotales()
        {
            InitializeComponent();
            controladoraCompra = ControladoraCompra.ObtenerInstancia();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            controladoraProveedor = ControladoraProveedor.ObtenerInstancia();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            dtpFechaDesde.Value = DateTime.Today.AddDays(-30);
            dtpFechaHasta.Value = DateTime.Today;
            ConfigurarDataGridView();
            CargarCompras();
        }

        private void ConfigurarDataGridView()
        {
            dgvCompras.AutoGenerateColumns = false;
            dgvCompras.Columns.Clear();

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 80,
                ReadOnly = true
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                DataPropertyName = "FechaCompra",
                HeaderText = "Fecha",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Proveedor",
                DataPropertyName = "Proveedor",
                HeaderText = "Proveedor",
                Width = 200,
                ReadOnly = true
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumeroFactura",
                DataPropertyName = "NumeroFactura",
                HeaderText = "N° Factura",
                Width = 120,
                ReadOnly = true
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FormaPago",
                DataPropertyName = "FormaPago",
                HeaderText = "Forma de Pago",
                Width = 150,
                ReadOnly = true
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                DataPropertyName = "Total",
                HeaderText = "Total",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private void CargarCompras()
        {
            try
            {
                var compras = controladoraCompra.ObtenerCompras()
                    .Where(c => c.FechaCompra.Date >= dtpFechaDesde.Value.Date &&
                               c.FechaCompra.Date <= dtpFechaHasta.Value.Date)
                    .OrderByDescending(c => c.FechaCompra)
                    .ToList();

                dgvCompras.DataSource = null;
                dgvCompras.DataSource = compras;

                decimal totalCompras = compras.Sum(c => c.Total);
                lblTotalCompras.Text = $"TOTAL: $ {totalCompras:N2}";

                btnDetalleCompra.Enabled = compras.Any();
                btnEliminarCompra.Enabled = compras.Any();
                btnEstadisticas.Enabled = compras.Any();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las compras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarCompras()
        {
            try
            {
                string criterio = txtBuscarCompra.Text.Trim().ToLower();
                var compras = controladoraCompra.ObtenerCompras()
                    .Where(c => c.FechaCompra.Date >= dtpFechaDesde.Value.Date &&
                               c.FechaCompra.Date <= dtpFechaHasta.Value.Date &&
                               (c.Id.ToString().Contains(criterio) ||
                                c.Proveedor.RazonSocial.ToLower().Contains(criterio) ||
                                c.NumeroFactura.ToLower().Contains(criterio) ||
                                c.FormaPago.ToLower().Contains(criterio)))
                    .OrderByDescending(c => c.FechaCompra)
                    .ToList();

                dgvCompras.DataSource = null;
                dgvCompras.DataSource = compras;

                decimal totalCompras = compras.Sum(c => c.Total);
                lblTotalCompras.Text = $"TOTAL: $ {totalCompras:N2}";

                if (compras.Count == 0)
                {
                    MessageBox.Show("No se encontraron compras que coincidan con los criterios de búsqueda.",
                                  "Búsqueda",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar las compras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalleCompra(Compra compra)
        {
            var mensaje = new StringBuilder();
            mensaje.AppendLine($"DETALLE DE COMPRA - ID: {compra.Id}");
            mensaje.AppendLine($"Fecha: {compra.FechaCompra:dd/MM/yyyy HH:mm}");
            mensaje.AppendLine($"Proveedor: {compra.Proveedor.RazonSocial}");
            mensaje.AppendLine($"Número de Factura: {compra.NumeroFactura}");
            mensaje.AppendLine($"Forma de Pago: {compra.FormaPago}");
            mensaje.AppendLine("\nPRODUCTOS:");
            mensaje.AppendLine("------------------------------------------------");

            foreach (var detalle in compra.Detalles)
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
            mensaje.AppendLine($"TOTAL: ${compra.Total:N2}");

            MessageBox.Show(mensaje.ToString(), "Detalle de Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarEstadisticas()
        {
            try
            {
                var compras = controladoraCompra.ObtenerCompras()
                    .Where(c => c.FechaCompra.Date >= dtpFechaDesde.Value.Date &&
                               c.FechaCompra.Date <= dtpFechaHasta.Value.Date)
                    .ToList();

                if (!compras.Any())
                {
                    MessageBox.Show("No hay compras en el período seleccionado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var mensaje = new StringBuilder();
                mensaje.AppendLine("ESTADÍSTICAS DE COMPRAS");
                mensaje.AppendLine($"Período: {dtpFechaDesde.Value:dd/MM/yyyy} - {dtpFechaHasta.Value:dd/MM/yyyy}");
                mensaje.AppendLine("------------------------------------------------");

                decimal totalCompras = compras.Sum(c => c.Total);
                int cantidadCompras = compras.Count;
                decimal promedioCompra = totalCompras / cantidadCompras;

                mensaje.AppendLine($"Total compras: ${totalCompras:N2}");
                mensaje.AppendLine($"Cantidad de compras: {cantidadCompras}");
                mensaje.AppendLine($"Promedio por compra: ${promedioCompra:N2}");
                mensaje.AppendLine();

                var comprasPorFormaPago = compras
                    .GroupBy(c => c.FormaPago)
                    .Select(g => new
                    {
                        FormaPago = g.Key,
                        Total = g.Sum(c => c.Total),
                        Cantidad = g.Count()
                    })
                    .OrderByDescending(x => x.Total);

                mensaje.AppendLine("COMPRAS POR FORMA DE PAGO:");
                foreach (var grupo in comprasPorFormaPago)
                {
                    mensaje.AppendLine($"{grupo.FormaPago}:");
                    mensaje.AppendLine($"  Total: ${grupo.Total:N2}");
                    mensaje.AppendLine($"  Cantidad: {grupo.Cantidad}");
                }
                mensaje.AppendLine();

                var comprasPorProveedor = compras
                    .GroupBy(c => c.Proveedor.RazonSocial)
                    .Select(g => new
                    {
                        Proveedor = g.Key,
                        Total = g.Sum(c => c.Total),
                        Cantidad = g.Count()
                    })
                    .OrderByDescending(x => x.Total)
                    .Take(5);

                mensaje.AppendLine("PRINCIPALES PROVEEDORES:");
                foreach (var proveedor in comprasPorProveedor)
                {
                    mensaje.AppendLine($"{proveedor.Proveedor}:");
                    mensaje.AppendLine($"  Total Comprado: ${proveedor.Total:N2}");
                    mensaje.AppendLine($"  Cantidad de Compras: {proveedor.Cantidad}");
                }

                MessageBox.Show(mensaje.ToString(), "Estadísticas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar estadísticas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eventos de los botones
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {

        }

        private void btnDetalleCompra_Click(object sender, EventArgs e)
        {
            if (dgvCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una compra para ver su detalle.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var compra = (Compra)dgvCompras.SelectedRows[0].DataBoundItem;
            MostrarDetalleCompra(compra);
        }

        private void btnEliminarCompra_Click(object sender, EventArgs e)
        {
            if (dgvCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una compra para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var compra = (Compra)dgvCompras.SelectedRows[0].DataBoundItem;
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar la compra #{compra.Id}?\n\n" +
                                          $"Esta acción restaurará el stock de los productos y no se puede deshacer.",
                                          "Confirmar eliminación",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    controladoraCompra.EliminarCompra(compra.Id);
                    MessageBox.Show("Compra eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarCompras();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la compra: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            MostrarEstadisticas();
        }

        private void btnGenerarInforme_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormComprasTotales_Load(object sender, EventArgs e)
        {
            CargarCompras();
        }
    }
}
