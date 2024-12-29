using CONTROLADORA;
using ENTIDADES;
using iTextSharp.text.pdf;
using iTextSharp.text;
using LiveCharts.Wpf;
using LiveCharts;
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
using Rectangle = System.Drawing.Rectangle;
using System.Windows.Media;

namespace VISTA
{
    public partial class FormReportes : Form
    {
        private ControladoraReporte controladoraReporte;
        private ControladoraVenta controladoraVenta;
        private LiveCharts.WinForms.CartesianChart chartVentas;

        public FormReportes()
        {
            InitializeComponent();
            controladoraReporte = ControladoraReporte.ObtenerInstancia();
            controladoraVenta = ControladoraVenta.ObtenerInstancia();
            

            // Fechas iniciales conf
            dtpFechaInicio.Value = DateTime.Today.AddDays(-30);
            dtpFechaFin.Value = DateTime.Today;

            InicializarGrafico();  
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {
            ActualizarGrafico();
        }

        private void ActualizarGrafico()
        {
            try
            {
                var ventas = controladoraVenta.ObtenerVentasPorFecha(dtpFechaInicio.Value.Date, dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1));

                if (ventas == null || !ventas.Any())
                {
                    MessageBox.Show("No hay datos de ventas para mostrar en el período seleccionado.",
                        "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var ventasPorDia = ventas.GroupBy(v => v.FechaVenta.Date)
                                       .Select(g => new {
                                           Fecha = g.Key,
                                           Total = g.Sum(v => v.Total)
                                       })
                                       .OrderBy(x => x.Fecha)
                                       .ToList();

                // Configurar el gráfico
                chartVentas.Series.Clear();
                chartVentas.AxisX.Clear();
                chartVentas.AxisY.Clear();

                var series = new LineSeries
                {
                    Title = "Ventas Diarias",
                    Values = new ChartValues<decimal>(ventasPorDia.Select(v => v.Total)),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15,
                    LineSmoothness = 0.5,
                    StrokeThickness = 4,
                    Stroke = new System.Windows.Media.SolidColorBrush(
                        System.Windows.Media.Color.FromRgb(184, 134, 11)),
                    Fill = new System.Windows.Media.LinearGradientBrush
                    {
                        GradientStops = new System.Windows.Media.GradientStopCollection
                    {
                        new System.Windows.Media.GradientStop(
                            System.Windows.Media.Color.FromArgb(100, 184, 134, 11), 0),
                        new System.Windows.Media.GradientStop(
                            System.Windows.Media.Color.FromArgb(0, 184, 134, 11), 1)
                    }
                    }
                };

                chartVentas.Series = new SeriesCollection { series };

                chartVentas.AxisX.Add(new Axis
                {
                    Title = "Fecha",
                    Labels = ventasPorDia.Select(v => v.Fecha.ToString("dd/MM")).ToList(),
                    LabelsRotation = 20,
                    Separator = new Separator
                    {
                        Step = Math.Max(1, ventasPorDia.Count / 10),
                        StrokeThickness = 1,
                        StrokeDashArray = new DoubleCollection { 2 }
                    }
                });

                chartVentas.AxisY.Add(new Axis
                {
                    Title = "Ventas Totales ($)",
                    LabelFormatter = value => value.ToString("C0"),
                    MinValue = 0
                });

                chartVentas.LegendLocation = LegendLocation.Top;

 
                chartVentas.DataTooltip = new DefaultTooltip
                {
                    SelectionMode = TooltipSelectionMode.SharedYValues
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
                    Title = "Guardar Reporte de Ventas",
                    FileName = $"ReporteVentas_{DateTime.Now:yyyyMMdd}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    // Convertir el gráfico actual a imagen
                    byte[] imagenGrafico = null;
                    using (var ms = new MemoryStream())
                    {
                        var bitmap = new Bitmap(chartVentas.Width, chartVentas.Height);
                        chartVentas.DrawToBitmap(bitmap, new Rectangle(0, 0, chartVentas.Width, chartVentas.Height));
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        imagenGrafico = ms.ToArray();
                    }

                    // Generar el reporte con la imagen del gráfico
                    controladoraReporte.GenerarReporteVentas(
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





        private void InicializarGrafico()
        {
  
            chartVentas = new LiveCharts.WinForms.CartesianChart
            {
                Location = new System.Drawing.Point(330, 76),
                Name = "chartVentas",
                Size = new System.Drawing.Size(800, 400),
                BackColor = System.Drawing.Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            this.Controls.Add(chartVentas);

           
            ActualizarGrafico();
        }




    }
}
