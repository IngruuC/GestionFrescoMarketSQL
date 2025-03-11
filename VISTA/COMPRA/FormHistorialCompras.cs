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
    public partial class FormHistorialCompras : Form
    {
        private Proveedor proveedor;
        private ControladoraCompra controladoraCompra;
        private ControladoraProducto controladoraProducto;
        private List<Compra> compras = new List<Compra>();

        public FormHistorialCompras(Proveedor proveedor)
        {
            InitializeComponent();
            this.proveedor = proveedor;
            controladoraCompra = ControladoraCompra.ObtenerInstancia();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();



            ConfigurarFormulario();
            ConfigurarDataGridView();
            CargarCompras();
        }


        private void ConfigurarFormulario()
        {
            this.Text = $"Historial de Compras - {proveedor.RazonSocial}";
            lblTitulo.Text = $"Historial de Compras - {proveedor.RazonSocial}";

            // Configurar controles de fecha
            dtpFechaDesde.Value = DateTime.Today.AddDays(-30);
            dtpFechaHasta.Value = DateTime.Today;
        }

        private void ConfigurarDataGridView()
        {
            dgvCompras.AutoGenerateColumns = false;
            dgvCompras.RowHeadersVisible = false;
            dgvCompras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCompras.MultiSelect = false;
            dgvCompras.ReadOnly = true;
            dgvCompras.AllowUserToAddRows = false;
            dgvCompras.AllowUserToDeleteRows = false;
            dgvCompras.AllowUserToResizeRows = false;

            // Configurar columnas
            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 60,
                ReadOnly = true
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaCompra",
                HeaderText = "Fecha",
                DataPropertyName = "FechaCompra",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumeroFactura",
                HeaderText = "N° Factura",
                DataPropertyName = "NumeroFactura",
                Width = 120,
                ReadOnly = true
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FormaPago",
                HeaderText = "Forma de Pago",
                DataPropertyName = "FormaPago",
                Width = 130,
                ReadOnly = true
            });

            dgvCompras.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private void CargarCompras()
        {
            try
            {
                // Obtener todas las compras y filtrar por proveedor
                var todasLasCompras = controladoraCompra.ObtenerCompras();
                compras = todasLasCompras.Where(c => c.ProveedorId == proveedor.Id).ToList();

                // Aplicar filtro de fechas
                FiltrarComprasPorFecha();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial de compras: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarComprasPorFecha()
        {
            try
            {
                var comprasFiltradas = compras.Where(c =>
                    c.FechaCompra.Date >= dtpFechaDesde.Value.Date &&
                    c.FechaCompra.Date <= dtpFechaHasta.Value.Date
                ).OrderByDescending(c => c.FechaCompra).ToList();

                dgvCompras.DataSource = null;
                dgvCompras.DataSource = comprasFiltradas;

                decimal total = comprasFiltradas.Sum(c => c.Total);
                lblTotalCompras.Text = $"Total: ${total:N2}";

                if (comprasFiltradas.Count == 0)
                {
                    MessageBox.Show("No se encontraron compras en el período seleccionado.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar compras: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalleCompra()
        {
            if (dgvCompras.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una compra para ver su detalle.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var compra = (Compra)dgvCompras.SelectedRows[0].DataBoundItem;

                var mensaje = new StringBuilder();
                mensaje.AppendLine($"DETALLE DE COMPRA #{compra.Id}");
                mensaje.AppendLine($"Fecha: {compra.FechaCompra:dd/MM/yyyy HH:mm}");
                mensaje.AppendLine($"Número de Factura: {compra.NumeroFactura}");
                mensaje.AppendLine($"Forma de Pago: {compra.FormaPago}");
                mensaje.AppendLine("\nPRODUCTOS:");
                mensaje.AppendLine("--------------------------------------------------------------");

                foreach (var detalle in compra.Detalles)
                {
                    mensaje.AppendLine($"- {detalle.ProductoNombre}");
                    mensaje.AppendLine($"  Cantidad: {detalle.Cantidad}");
                    mensaje.AppendLine($"  Precio Unitario: ${detalle.PrecioUnitario:N2}");
                    mensaje.AppendLine($"  Subtotal: ${detalle.Subtotal:N2}");

                    var producto = controladoraProducto.ObtenerProductoPorId(detalle.ProductoId);
                    if (producto != null && producto.EsPerecedero && producto.FechaVencimiento.HasValue)
                    {
                        mensaje.AppendLine($"  Vencimiento: {producto.FechaVencimiento.Value:dd/MM/yyyy}");
                    }

                    mensaje.AppendLine();
                }

                mensaje.AppendLine("--------------------------------------------------------------");
                mensaje.AppendLine($"TOTAL: ${compra.Total:N2}");

                MessageBox.Show(mensaje.ToString(), "Detalle de Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar detalle: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Eventos de botones
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            FiltrarComprasPorFecha();
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            MostrarDetalleCompra();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("La función de exportación estará disponible próximamente.",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCompras_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MostrarDetalleCompra();
            }
        }
        #endregion
    }
}