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
using System.Windows.Media;
using SysColor = System.Drawing.Color;
using MediaColor = System.Windows.Media.Color;
using SysRectangle = System.Drawing.Rectangle;

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

            try
            {
                controladoraReporte = ControladoraReporteCompra.ObtenerInstancia();
                controladoraCompra = ControladoraCompra.ObtenerInstancia();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar controladores: {ex.Message}");
            }

            this.Load += FormReportesCompras_Load;


            // Fechas iniciales conf
            dtpFechaInicio.Value = DateTime.Today.AddDays(-30);
            dtpFechaFin.Value = DateTime.Today;

            // Eventos para actualizar el gráfico
            dtpFechaInicio.ValueChanged += (s, e) => ActualizarGrafico();
            dtpFechaFin.ValueChanged += (s, e) => ActualizarGrafico();

            this.Shown += (s, e) => ActualizarGrafico();

        }

        private void FormReportesCompras_Load(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Entrando en FormReportesCompras_Load");

                // Asegúrate de que el gráfico esté visible
                chartCompras.Visible = true;

                // Actualiza el gráfico
                ActualizarGrafico();

                System.Diagnostics.Debug.WriteLine("Saliendo de FormReportesCompras_Load");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en FormReportesCompras_Load: {ex}");
                MessageBox.Show($"Error al cargar el formulario: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarGrafico()
        {
            try
            {
                var compras = controladoraCompra.ObtenerComprasPorFecha(
                    dtpFechaInicio.Value.Date,
                    dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1)
                );

                if (compras == null || !compras.Any())
                {
                    MessageBox.Show("No hay datos de compras para mostrar en el período seleccionado.",
                        "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Agrupar compras por proveedor
                var comprasPorProveedor = compras
                    .GroupBy(c => c.Proveedor.RazonSocial)
                    .Select(g => new {
                        Proveedor = g.Key,
                        Total = g.Sum(c => c.Total)
                    })
                    .OrderByDescending(x => x.Total)
                    .Take(10)  // Tomar los 10 proveedores principales
                    .ToList();

                // Limpiar series existentes
                chartCompras.Series.Clear();
                chartCompras.AxisX.Clear();
                chartCompras.AxisY.Clear();

                // Configurar serie de columnas para simular un gráfico de pastel
                var columnSeries = new ColumnSeries
                {
                    Title = "Compras por Proveedor",
                    Values = new ChartValues<decimal>(comprasPorProveedor.Select(c => c.Total)),
                    DataLabels = true,
                    LabelPoint = point =>
                    {
                        var total = comprasPorProveedor.Sum(c => c.Total);
                        var currentValue = comprasPorProveedor[(int)point.X].Total;
                        var percentage = total > 0 ? (currentValue / total) * 100 : 0;

                        return $"{comprasPorProveedor[(int)point.X].Proveedor}\n{currentValue:C0}\n({percentage:F1}%)";
                    },
                    Fill = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(70, 130, 180))
                };

                // Añadir series
                chartCompras.Series = new SeriesCollection { columnSeries };

                // Configurar eje X
                chartCompras.AxisX.Add(new Axis
                {
                    Title = "Proveedores",
                    Labels = comprasPorProveedor.Select(c => c.Proveedor).ToList(),
                    LabelsRotation = 45
                });

                // Configurar eje Y
                chartCompras.AxisY.Add(new Axis
                {
                    Title = "Total de Compras ($)",
                    LabelFormatter = value => value.ToString("C0")
                });

                // Configurar leyenda
                chartCompras.LegendLocation = LegendLocation.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el gráfico: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFechaFin.Value < dtpFechaInicio.Value)
                {
                    MessageBox.Show("La fecha final no puede ser menor a la fecha inicial",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Archivos PDF|*.pdf",
                    Title = "Guardar Reporte de Compras",
                    FileName = $"ReporteCompras_{DateTime.Now:yyyyMMdd}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Convertir el gráfico actual a imagen
                    byte[] imagenGrafico = null;
                    using (var ms = new MemoryStream())
                    {
                        var bitmap = new Bitmap(chartCompras.Width, chartCompras.Height);
                        chartCompras.DrawToBitmap(bitmap, new SysRectangle(0, 0, chartCompras.Width, chartCompras.Height));
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        imagenGrafico = ms.ToArray();
                    }

                    // Generar el reporte con la imagen del gráfico
                    controladoraReporte.GenerarReporteCompras(
                        dtpFechaInicio.Value.Date,
                        dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1),
                        saveDialog.FileName,
                        imagenGrafico
                    );

                    Cursor = Cursors.Default;

                    if (MessageBox.Show("Reporte generado exitosamente. ¿Desea abrirlo ahora?",
                        "Éxito", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al generar el reporte: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

   
    }
}