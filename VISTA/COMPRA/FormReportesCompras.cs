using CONTROLADORA;
using ENTIDADES;
using iTextSharp.text.pdf;
using iTextSharp.text;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace VISTA
{
    public partial class FormReportesCompras : Form
    {
        private ControladoraReporteCompra controladoraReporte;
        private ControladoraCompra controladoraCompra;
        private LiveCharts.WinForms.CartesianChart chartCompras;

        public FormReportesCompras()
        {
            InitializeComponent();
            controladoraReporte = ControladoraReporteCompra.ObtenerInstancia();
            controladoraCompra = ControladoraCompra.ObtenerInstancia();

            dtpFechaInicio.Value = DateTime.Today.AddDays(-30);
            dtpFechaFin.Value = DateTime.Today;

            InicializarGrafico();
        }

        private void FormReportesCompras_Load(object sender, EventArgs e)
        {
            ActualizarGrafico();
        }

        private void ActualizarGrafico()
        {
            try
            {
                var compras = controladoraCompra.ObtenerComprasPorFecha(dtpFechaInicio.Value.Date,
                                                                       dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1));

                if (compras == null || !compras.Any())
                {
                    MessageBox.Show("No hay datos de compras para mostrar en el período seleccionado.",
                        "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Agrupar compras por proveedor
                var comprasPorProveedor = compras.GroupBy(c => c.Proveedor.RazonSocial)
                                                .Select(g => new {
                                                    Proveedor = g.Key,
                                                    Total = g.Sum(c => c.Total),
                                                    Cantidad = g.Count()
                                                })
                                                .OrderByDescending(x => x.Total)
                                                .Take(10)  // Tomamos los top 10 proveedores
                                                .ToList();

                // Limpiar gráfico anterior
                chartCompras.Series.Clear();
                chartCompras.AxisX.Clear();
                chartCompras.AxisY.Clear();

                // Gráfico de Barras para montos
                var columnSeries = new ColumnSeries
                {
                    Title = "Total Compras",
                    Values = new ChartValues<decimal>(comprasPorProveedor.Select(c => c.Total)),
                    Fill = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(184, 134, 11)), // DarkGoldenrod
                    MaxColumnWidth = 50
                };

                // Línea para cantidad de compras
                var lineSeries = new LineSeries
                {
                    Title = "Cantidad de Compras",
                    Values = new ChartValues<int>(comprasPorProveedor.Select(c => c.Cantidad)),
                    PointGeometry = DefaultGeometries.Diamond,
                    PointGeometrySize = 10,
                    Stroke = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(178, 34, 34)), // Firebrick
                    Fill = System.Windows.Media.Brushes.Transparent,
                    ScalesYAt = 1  // Usar el segundo eje Y
                };

                chartCompras.Series = new SeriesCollection { columnSeries, lineSeries };

                // Configurar eje X
                chartCompras.AxisX.Add(new Axis
                {
                    Title = "Proveedores",
                    Labels = comprasPorProveedor.Select(c => c.Proveedor).ToList(),
                    LabelsRotation = 45,
                    Separator = new Separator
                    {
                        Step = 1,
                        StrokeThickness = 1,
                        StrokeDashArray = new System.Windows.Media.DoubleCollection { 2 }
                    }
                });

                // Eje Y para montos
                chartCompras.AxisY.Add(new Axis
                {
                    Title = "Total Compras ($)",
                    LabelFormatter = value => value.ToString("C0"),
                    MinValue = 0,
                });

                // Segundo eje Y para cantidad
                chartCompras.AxisY.Add(new Axis
                {
                    Title = "Cantidad de Compras",
                    LabelFormatter = value => value.ToString("N0"),
                    MinValue = 0,
                    Foreground = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(178, 34, 34))  // Firebrick
                });

                chartCompras.LegendLocation = LegendLocation.Top;

                // Tooltip personalizado
                chartCompras.DataTooltip = new DefaultTooltip
                {
                    SelectionMode = TooltipSelectionMode.SharedXValues
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el gráfico: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {

        }

        private void InicializarGrafico()
        {
            chartCompras = new LiveCharts.WinForms.CartesianChart
            {
                Location = new Point(330, 76),
                Name = "chartCompras",
                Size = new Size(800, 400),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            this.Controls.Add(chartCompras);

            ActualizarGrafico();
        }
    }
}