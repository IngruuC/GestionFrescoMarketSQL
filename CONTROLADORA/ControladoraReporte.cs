using ENTIDADES;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CONTROLADORA
{
    public class ControladoraReporte
    {
        private static ControladoraReporte instancia;
        private ControladoraVenta controladoraVenta;

        private ControladoraReporte()
        {
            controladoraVenta = ControladoraVenta.ObtenerInstancia();
        }

        public static ControladoraReporte ObtenerInstancia()
        {
            if (instancia == null)
                instancia = new ControladoraReporte();
            return instancia;
        }

        public void GenerarReporteVentas(DateTime fechaInicio, DateTime fechaFin, string rutaGuardado)
        {
            var ventas = controladoraVenta.ObtenerVentasPorFecha(fechaInicio, fechaFin);

            using (FileStream fs = new FileStream(rutaGuardado, FileMode.Create))
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.Open();

                // Agregar título
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                document.Add(new Paragraph("FRESCO MARKET - Reporte de Ventas", titleFont));
                document.Add(new Paragraph($"Período: {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()}\n\n", normalFont));

                // Resumen general
                decimal totalVentas = ventas.Sum(v => v.Total);
                int totalTransacciones = ventas.Count;

                document.Add(new Paragraph("Resumen General", subtitleFont));
                document.Add(new Paragraph($"Total de Ventas: ${totalVentas:N2}", normalFont));
                document.Add(new Paragraph($"Número de Transacciones: {totalTransacciones}", normalFont));
                if (totalTransacciones > 0)
                    document.Add(new Paragraph($"Ticket Promedio: ${(totalVentas / totalTransacciones):N2}\n\n", normalFont));

                // Ventas por forma de pago
                document.Add(new Paragraph("Ventas por Forma de Pago", subtitleFont));
                var ventasPorFormaPago = ventas.GroupBy(v => v.FormaPago)
                    .Select(g => new { FormaPago = g.Key, Total = g.Sum(v => v.Total) });
                foreach (var grupo in ventasPorFormaPago)
                {
                    document.Add(new Paragraph($"{grupo.FormaPago}: ${grupo.Total:N2}", normalFont));
                }
                document.Add(new Paragraph("\n"));

                // Productos más vendidos
                document.Add(new Paragraph("Top 5 Productos Más Vendidos", subtitleFont));
                var productosMasVendidos = ventas.SelectMany(v => v.Detalles)
                    .GroupBy(d => d.ProductoNombre)
                    .Select(g => new
                    {
                        Producto = g.Key,
                        Cantidad = g.Sum(d => d.Cantidad),
                        Total = g.Sum(d => d.Subtotal)
                    })
                    .OrderByDescending(x => x.Cantidad)
                    .Take(5);

                foreach (var producto in productosMasVendidos)
                {
                    document.Add(new Paragraph(
                        $"{producto.Producto}: {producto.Cantidad} unidades - ${producto.Total:N2}",
                        normalFont));
                }
                document.Add(new Paragraph("\n"));

                // Tabla de ventas
                document.Add(new Paragraph("Detalle de Ventas", subtitleFont));
                PdfPTable table = new PdfPTable(5);
                float[] widths = new float[] { 20f, 25f, 20f, 15f, 20f };
                table.SetWidths(widths);
                table.WidthPercentage = 100;

                // Estilo para encabezados
                var cellStyle = new PdfPCell
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    Padding = 5
                };

                // Encabezados
                table.AddCell(new PdfPCell(new Phrase("Fecha", subtitleFont)) { BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Cliente", subtitleFont)) { BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Forma de Pago", subtitleFont)) { BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Items", subtitleFont)) { BackgroundColor = BaseColor.LIGHT_GRAY });
                table.AddCell(new PdfPCell(new Phrase("Total", subtitleFont)) { BackgroundColor = BaseColor.LIGHT_GRAY });

                // Datos
                foreach (var venta in ventas.OrderByDescending(v => v.FechaVenta))
                {
                    table.AddCell(venta.FechaVenta.ToString("dd/MM/yyyy HH:mm"));
                    table.AddCell($"{venta.Cliente.Nombre} {venta.Cliente.Apellido}");
                    table.AddCell(venta.FormaPago);
                    table.AddCell(venta.Detalles.Sum(d => d.Cantidad).ToString());
                    table.AddCell($"${venta.Total:N2}");
                }

                document.Add(table);

                document.Close();
            }
        }

        public List<Venta> ObtenerVentasPorFecha(DateTime inicio, DateTime fin)
        {
            return controladoraVenta.ObtenerVentasPorFecha(inicio, fin);
        }

        // Puedes agregar más métodos para diferentes tipos de reportes
        public List<object> ObtenerProductosMasVendidos(DateTime inicio, DateTime fin, int top = 5)
        {
            var ventas = controladoraVenta.ObtenerVentasPorFecha(inicio, fin);
            return ventas.SelectMany(v => v.Detalles)
                .GroupBy(d => new { d.ProductoId, d.ProductoNombre })
                .Select(g => new
                {
                    Producto = g.Key.ProductoNombre,
                    Cantidad = g.Sum(d => d.Cantidad),
                    Total = g.Sum(d => d.Subtotal)
                })
                .OrderByDescending(x => x.Cantidad)
                .Take(top)
                .Cast<object>()
                .ToList();
        }

        public List<object> ObtenerVentasPorFormaPago(DateTime inicio, DateTime fin)
        {
            var ventas = controladoraVenta.ObtenerVentasPorFecha(inicio, fin);
            return ventas.GroupBy(v => v.FormaPago)
                .Select(g => new
                {
                    FormaPago = g.Key,
                    Cantidad = g.Count(),
                    Total = g.Sum(v => v.Total)
                })
                .OrderByDescending(x => x.Total)
                .Cast<object>()
                .ToList();
        }
    }
}
