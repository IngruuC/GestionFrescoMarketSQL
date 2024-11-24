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

namespace VISTA
{
    public partial class FormReportes : Form
    {
        private ControladoraReporte controladoraReporte;
        private ControladoraVenta controladoraVenta;

        public FormReportes()
        {
            InitializeComponent();
            controladoraReporte = ControladoraReporte.ObtenerInstancia();
            controladoraVenta = ControladoraVenta.ObtenerInstancia();

            // Configurar fechas iniciales
            dtpFechaInicio.Value = DateTime.Today.AddDays(-30);
            dtpFechaFin.Value = DateTime.Today;
        }

        private void FormReportes_Load(object sender, EventArgs e)
        {
            ActualizarGrafico();
        }

        private void ActualizarGrafico()
        {
            try
            {
                var ventas = controladoraVenta.ObtenerVentasPorFecha(dtpFechaInicio.Value, dtpFechaFin.Value);

                var ventasPorDia = ventas.GroupBy(v => v.FechaVenta.Date)
                                        .Select(g => new { Fecha = g.Key, Total = g.Sum(v => v.Total) })
                                        .OrderBy(x => x.Fecha)
                                        .ToList();

                chartVentas.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Ventas Diarias",
                    Values = new ChartValues<decimal>(ventasPorDia.Select(v => v.Total)),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 15
                }
            };

                chartVentas.AxisX.Clear();
                chartVentas.AxisY.Clear();

                chartVentas.AxisX.Add(new Axis
                {
                    Title = "Fecha",
                    Labels = ventasPorDia.Select(v => v.Fecha.ToShortDateString()).ToList(),
                    Separator = new Separator
                    {
                        Step = Math.Max(1, ventasPorDia.Count / 10) // Mostrar máximo 10 etiquetas
                    }
                });

                chartVentas.AxisY.Add(new Axis
                {
                    Title = "Total Ventas ($)",
                    LabelFormatter = value => value.ToString("C2")
                });

                chartVentas.LegendLocation = LegendLocation.Right;
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
                    controladoraReporte.GenerarReporteVentas(
                        dtpFechaInicio.Value.Date,
                        dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1),
                        saveDialog.FileName
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

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            ActualizarGrafico();
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            ActualizarGrafico();
        }
    }
}
