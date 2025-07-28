using CONTROLADORA;
using ENTIDADES;
using ENTIDADES.SEGURIDAD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;


namespace VISTA
{
    public partial class FormAuditoriaSesion : Form
    {
        private ControladoraAuditoria controladoraAuditoria;
        private ControladoraUsuario controladoraUsuario;
        private ControladoraAuditoria _controladoraAuditoria;

        public FormAuditoriaSesion()
        {
            InitializeComponent();
            // Inicializar las controladoras
            controladoraAuditoria = new ControladoraAuditoria();
            controladoraUsuario = ControladoraUsuario.ObtenerInstancia();
            _controladoraAuditoria = new ControladoraAuditoria();
            ConfigurarControles();
        }

        private void ConfigurarControles()
        {
            // Configurar ComboBox de Usuarios
            cboUsuarios.DataSource = null;
            cboUsuarios.DataSource = controladoraUsuario.ObtenerUsuarios();
            cboUsuarios.DisplayMember = "NombreUsuario";
            cboUsuarios.ValueMember = "Id";
            cboUsuarios.SelectedIndex = -1;

            // Configurar rango de fechas
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            // Configurar DataGridView
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvAuditorias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Usuario",
                DataPropertyName = "NombreUsuario"
            });
            dgvAuditorias.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Ingreso",
                DataPropertyName = "FechaIngreso",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
            // Más columnas...
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Debug: Imprimir valores seleccionados
                Console.WriteLine($"Usuario seleccionado: {cboUsuarios.SelectedValue}");
                Console.WriteLine($"Fecha desde: {dtpDesde.Value}");
                Console.WriteLine($"Fecha hasta: {dtpHasta.Value}");

                int? usuarioId = cboUsuarios.SelectedIndex != -1
                    ? (int?)cboUsuarios.SelectedValue
                    : null;

                var sesiones = controladoraAuditoria.ObtenerHistorialSesiones(
                    usuarioId,
                    dtpDesde.Value,
                    dtpHasta.Value
                );

                // Imprimir los resultados en la consola
                foreach (var sesion in sesiones)
                {
                    Console.WriteLine($"Sesión - ID: {sesion.Id}, Usuario: {sesion.NombreUsuario}, Fecha Ingreso: {sesion.FechaIngreso}");
                }

                if (sesiones == null || sesiones.Count == 0)
                {
                    MessageBox.Show("No se encontraron registros de sesión para los criterios seleccionados.",
                        "Sin Resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    dgvAuditorias.DataSource = null;
                    return;
                }

                dgvAuditorias.DataSource = sesiones;
            }
            catch (Exception ex)
            {
                // Logging de errores más detallado
                Console.WriteLine($"Error en búsqueda: {ex.Message}");
                Console.WriteLine($"Detalles internos: {ex.InnerException?.Message}");

                MessageBox.Show($"Error al buscar registros de sesión: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


        }

        private void btnLimpiar_Click(object sender, EventArgs e)
                {
                    // Restablecer controles
                    cboUsuarios.SelectedIndex = -1;
                    dtpDesde.Value = DateTime.Today.AddMonths(-1);
                    dtpHasta.Value = DateTime.Today;

                    // Limpiar DataGridView
                    dgvAuditorias.DataSource = null;

                }

        private void FormAuditoriaSesion_Load(object sender, EventArgs e)
        {
            Console.WriteLine("=== FormAuditoriaSesion_Load INICIADO ===");
            Console.WriteLine($"Usuario actual: {SesionActual.Usuario?.NombreUsuario}");
            Console.WriteLine($"Es administrador: {SesionActual.EsAdministrador()}");

            // Verificar que solo administradores puedan ver todas las auditorías
            if (!SesionActual.EsAdministrador())
            {
                MessageBox.Show("Solo los administradores pueden acceder a la auditoría completa del sistema.",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            CargarHistorialSesiones();
            Console.WriteLine("=== FormAuditoriaSesion_Load TERMINADO ===");
        }


        private void CargarHistorialSesiones()
        {

            Console.WriteLine("=== CargarHistorialSesiones INICIADO ===");

            try
            {
                // CAMBIO: Usar ObtenerTodasLasSesiones() en lugar de ObtenerAuditoriasPorUsuario()
                List<AuditoriaSesion> auditorias = _controladoraAuditoria.ObtenerTodasLasSesiones();

                Console.WriteLine($"Total auditorías cargadas: {auditorias.Count}");

                // Debug: Mostrar usuarios únicos
                if (auditorias.Count > 0)
                {
                    var usuariosUnicos = auditorias.Select(a => a.NombreUsuario).Distinct().ToList();
                    Console.WriteLine($"Usuarios encontrados: {string.Join(", ", usuariosUnicos)}");

                    // Mostrar primeros 3 registros
                    Console.WriteLine("Primeros registros:");
                    for (int i = 0; i < Math.Min(3, auditorias.Count); i++)
                    {
                        var a = auditorias[i];
                        Console.WriteLine($"  {i + 1}. {a.NombreUsuario} - {a.FechaIngreso}");
                    }
                }

                dgvAuditorias.DataSource = auditorias;
                Console.WriteLine("DataSource asignado correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en CargarHistorialSesiones: {ex.Message}");
                MessageBox.Show($"Error al cargar historial: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Console.WriteLine("=== CargarHistorialSesiones TERMINADO ===");
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay datos para exportar
                if (dgvAuditorias.DataSource == null || dgvAuditorias.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.",
                        "Exportar a PDF",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // Configurar diálogo para guardar archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos PDF (*.pdf)|*.pdf",
                    Title = "Guardar reporte de auditoría de sesiones",
                    FileName = $"Auditoria_Sesiones_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                // Crear documento PDF
                using (var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10f, 10f, 10f, 10f))
                {
                    // Crear el escritor de PDF
                    var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(
                        saveFileDialog.FileName, System.IO.FileMode.Create));

                    document.Open();

                    // Añadir información del documento
                    document.AddTitle("Reporte de Auditoría de Sesiones");
                    document.AddAuthor("Sistema de Gestión");
                    document.AddCreationDate();

                    // Fuentes y colores
                    var titleFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 16);
                    var headerFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLD, 12);
                    var contentFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 10);
                    var footerFont = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8, iTextSharp.text.Font.ITALIC);

                    // Añadir título
                    var title = new iTextSharp.text.Paragraph("Reporte de Auditoría de Sesiones", titleFont);
                    title.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                    title.SpacingAfter = 20f;
                    document.Add(title);

                    // Añadir información del filtro
                    var filtroInfo = new iTextSharp.text.Paragraph();
                    filtroInfo.Add(new iTextSharp.text.Chunk("Filtros aplicados:", headerFont));
                    filtroInfo.Add(new iTextSharp.text.Chunk("\nUsuario: ", headerFont));
                    filtroInfo.Add(new iTextSharp.text.Chunk(cboUsuarios.SelectedIndex != -1 ? cboUsuarios.Text : "Todos", contentFont));
                    filtroInfo.Add(new iTextSharp.text.Chunk("\nFecha desde: ", headerFont));
                    filtroInfo.Add(new iTextSharp.text.Chunk(dtpDesde.Value.ToString("dd/MM/yyyy"), contentFont));
                    filtroInfo.Add(new iTextSharp.text.Chunk("\nFecha hasta: ", headerFont));
                    filtroInfo.Add(new iTextSharp.text.Chunk(dtpHasta.Value.ToString("dd/MM/yyyy"), contentFont));
                    filtroInfo.SpacingAfter = 15f;
                    document.Add(filtroInfo);

                    // Crear tabla para los datos
                    var table = new iTextSharp.text.pdf.PdfPTable(dgvAuditorias.Columns.Count);
                    table.WidthPercentage = 100;
                    table.DefaultCell.Padding = 3;

                    // Añadir encabezados
                    foreach (DataGridViewColumn column in dgvAuditorias.Columns)
                    {
                        var cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(column.HeaderText, headerFont));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                        cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }

                    // Añadir filas
                    foreach (DataGridViewRow row in dgvAuditorias.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            var value = cell.Value?.ToString() ?? string.Empty;
                            table.AddCell(new iTextSharp.text.Phrase(value, contentFont));
                        }
                    }

                    document.Add(table);

                    // Añadir pie de página
                    var footer = new iTextSharp.text.Paragraph($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", footerFont);
                    footer.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    footer.SpacingBefore = 10f;
                    document.Add(footer);

                    document.Close();

                    MessageBox.Show($"El reporte se ha exportado correctamente a:\n{saveFileDialog.FileName}",
                        "Exportación Exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Abrir el archivo PDF
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar a PDF: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Console.WriteLine($"Error de exportación PDF: {ex}");
            }
        }
    }
        }
    
