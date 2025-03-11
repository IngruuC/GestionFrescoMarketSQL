using CONTROLADORA;
using ENTIDADES;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace VISTA.COMPRA
{
    public partial class FormReportesProveedor : Form
    {
        private Proveedor proveedor;
        private ControladoraCompra controladoraCompra;
        private ControladoraProducto controladoraProducto;
        private GestorRelacionProveedorProducto gestorRelacion;
        private ControladoraReporteCompra controladoraReporte;
        private LiveCharts.WinForms.CartesianChart chartProductos;
        private LiveCharts.WinForms.PieChart chartFormasPago;

        // Clase auxiliar para estadísticas de productos
        private class ProductoEstadisticas
        {
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
            public decimal Total { get; set; }
        }

        public FormReportesProveedor()
        {
            InitializeComponent();
        }

        public FormReportesProveedor(Proveedor proveedor)
        {
            InitializeComponent();
            this.proveedor = proveedor;
            controladoraCompra = ControladoraCompra.ObtenerInstancia();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            gestorRelacion = GestorRelacionProveedorProducto.ObtenerInstancia();
            controladoraReporte = ControladoraReporteCompra.ObtenerInstancia();

            ConfigurarFormulario();
            InicializarGraficos();
            ActualizarGraficos();
        }

        private void ConfigurarFormulario()
        {
            this.Text = $"Reportes - {proveedor.RazonSocial}";
            lblTitulo.Text = $"Reportes de {proveedor.RazonSocial}";

            // Configurar controles de fecha
            dtpFechaDesde.Value = DateTime.Today.AddMonths(-3);
            dtpFechaHasta.Value = DateTime.Today;
        }

        private void InicializarGraficos()
        {
            // Gráfico de barras para productos
            chartProductos = new LiveCharts.WinForms.CartesianChart
            {
                Location = new Point(20, 100),
                Size = new Size(750, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(chartProductos);

            // Gráfico circular para formas de pago
            chartFormasPago = new LiveCharts.WinForms.PieChart
            {
                Location = new Point(20, 450),
                Size = new Size(350, 250),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            this.Controls.Add(chartFormasPago);

            // Etiquetas para los gráficos
            Label lblProductosChart = new Label
            {
                Text = "Productos Más Comprados",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 75),
                Size = new Size(300, 20)
            };
            this.Controls.Add(lblProductosChart);

            Label lblFormasPagoChart = new Label
            {
                Text = "Compras por Forma de Pago",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 425),
                Size = new Size(300, 20)
            };
            this.Controls.Add(lblFormasPagoChart);
        }

        private void ActualizarGraficos()
        {
            try
            {
                // Compras del proveedor en el período seleccionado
                var compras = controladoraCompra.ObtenerCompras()
                    .Where(c => c.ProveedorId == proveedor.Id &&
                               c.FechaCompra.Date >= dtpFechaDesde.Value.Date &&
                               c.FechaCompra.Date <= dtpFechaHasta.Value.Date)
                    .ToList();

                if (compras.Count == 0)
                {
                    MessageBox.Show("No hay datos de compras para mostrar en el período seleccionado.",
                        "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar gráficos
                    chartProductos.Series = new SeriesCollection();
                    chartFormasPago.Series = new SeriesCollection();
                    return;
                }

                // Gráfico de productos
                var productosComprados = new Dictionary<int, ProductoEstadisticas>();
                foreach (var compra in compras)
                {
                    foreach (var detalle in compra.Detalles)
                    {
                        if (!productosComprados.ContainsKey(detalle.ProductoId))
                        {
                            productosComprados[detalle.ProductoId] = new ProductoEstadisticas
                            {
                                Nombre = detalle.ProductoNombre,
                                Cantidad = 0,
                                Total = 0
                            };
                        }

                        var producto = productosComprados[detalle.ProductoId];
                        producto.Cantidad += detalle.Cantidad;
                        producto.Total += detalle.Subtotal;
                    }
                }

                // Ordenar productos por cantidad y tomar los top 10
                var topProductos = productosComprados.Values
                    .OrderByDescending(p => p.Cantidad)
                    .Take(10)
                    .ToList();

                // Configurar gráfico de barras para productos
                var columnSeries = new ColumnSeries
                {
                    Title = "Cantidad Comprada",
                    Values = new ChartValues<int>(topProductos.Select(p => p.Cantidad)),
                    MaxColumnWidth = 50,
                    Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(70, 130, 180)) // SteelBlue
                };

                var lineSeries = new LineSeries
                {
                    Title = "Total Comprado ($)",
                    Values = new ChartValues<decimal>(topProductos.Select(p => p.Total)),
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10,
                    LineSmoothness = 0,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34)), // Firebrick
                    ScalesYAt = 1
                };

                chartProductos.Series = new SeriesCollection { columnSeries, lineSeries };

                // Configurar ejes X e Y
                chartProductos.AxisX.Clear();
                chartProductos.AxisY.Clear();

                chartProductos.AxisX.Add(new Axis
                {
                    Title = "Productos",
                    Labels = topProductos.Select(p => p.Nombre).ToList(),
                    LabelsRotation = 45,
                    Separator = new Separator
                    {
                        Step = 1,
                        StrokeThickness = 1,
                        StrokeDashArray = new DoubleCollection { 2 }
                    }
                });

                // Eje Y para cantidad
                chartProductos.AxisY.Add(new Axis
                {
                    Title = "Cantidad",
                    LabelFormatter = value => value.ToString("N0"),
                    MinValue = 0
                });

                // Segundo eje Y para monto
                chartProductos.AxisY.Add(new Axis
                {
                    Title = "Total ($)",
                    LabelFormatter = value => value.ToString("C0"),
                    MinValue = 0,
                    Position = AxisPosition.RightTop,
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34)), // Firebrick
                });

                // Gráfico circular para formas de pago
                var comprasPorFormaPago = compras
                    .GroupBy(c => c.FormaPago)
                    .Select(g => new
                    {
                        FormaPago = g.Key,
                        Total = g.Sum(c => c.Total),
                        Cantidad = g.Count()
                    })
                    .OrderByDescending(x => x.Total)
                    .ToList();

                // Colores predefinidos para el gráfico circular
                var colorBrushes = new List<SolidColorBrush>
                {
                   new SolidColorBrush(System.Windows.Media.Color.FromRgb(65, 105, 225)),  // RoyalBlue
                  new SolidColorBrush(System.Windows.Media.Color.FromRgb(46, 139, 87)),   // SeaGreen
                   new SolidColorBrush(System.Windows.Media.Color.FromRgb(178, 34, 34)),   // Firebrick
                  new SolidColorBrush(System.Windows.Media.Color.FromRgb(218, 165, 32)),  // Goldenrod
                 new SolidColorBrush(System.Windows.Media.Color.FromRgb(72, 61, 139)),   // DarkSlateBlue
                 new SolidColorBrush(System.Windows.Media.Color.FromRgb(188, 143, 143)), // RosyBrown
                 new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 128, 128))    // Teal
                };

                // Configurar gráfico circular
                var pieSeriesCollection = new SeriesCollection();

                for (int i = 0; i < comprasPorFormaPago.Count; i++)
                {
                    var forma = comprasPorFormaPago[i];
                    var colorIndex = i % colorBrushes.Count;

                    pieSeriesCollection.Add(new PieSeries
                    {
                        Title = forma.FormaPago,
                        Values = new ChartValues<decimal> { forma.Total },
                        DataLabels = true,
                        LabelPoint = point => $"{forma.FormaPago} ({forma.Cantidad}): ${point.Y:N0}",
                        Fill = colorBrushes[colorIndex]
                    });
                }

                chartFormasPago.Series = pieSeriesCollection;
                chartFormasPago.LegendLocation = LegendLocation.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar gráficos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGraficos();
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    saveDialog.FileName = $"Reporte_{proveedor.RazonSocial}_{DateTime.Now:yyyyMMdd}.pdf";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Capturar el gráfico como imagen
                        using (var ms = new MemoryStream())
                        {
                            // Captura del gráfico de productos
                            var chartBitmap = new Bitmap(chartProductos.Width, chartProductos.Height);
                            chartProductos.DrawToBitmap(chartBitmap, new Rectangle(0, 0, chartProductos.Width, chartProductos.Height));
                            chartBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] chartBytes = ms.ToArray();

                            // Generar el PDF
                            controladoraReporte.GenerarReporteCompras(
                                dtpFechaDesde.Value,
                                dtpFechaHasta.Value,
                                saveDialog.FileName,
                                chartBytes
                            );

                            MessageBox.Show($"Reporte generado exitosamente en:\n{saveDialog.FileName}",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar PDF: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormReportesProveedor_Load(object sender, EventArgs e)
        {
            // Ya se llama a ActualizarGraficos en el constructor
        }
    }
}