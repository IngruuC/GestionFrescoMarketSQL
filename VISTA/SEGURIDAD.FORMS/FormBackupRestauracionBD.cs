using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace VISTA
{
    public partial class FormBackupRestauracionBD : Form
    {
        private string connectionString;
        private string backupFolder;

        public FormBackupRestauracionBD()
        {
            InitializeComponent();

            // Obtener la cadena de conexión desde una instancia del contexto
            using (var contexto = new MODELO.Contexto())
            {
                connectionString = contexto.Database.Connection.ConnectionString;
            }

            // Crear carpeta para backups si no existe
            backupFolder = Path.Combine(Application.StartupPath, "Backups");
            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }

            CargarFechaUltimoBackup();
            CargarListaBackups();
        }

        private void CargarFechaUltimoBackup()
        {
            try
            {
                // Usar un patrón más genérico para encontrar todos los archivos .xml
                var files = Directory.GetFiles(backupFolder, "*.xml");
                if (files.Length > 0)
                {
                    // Ordenar por fecha de creación descendente
                    var lastFile = new FileInfo(files
                        .OrderByDescending(f => new FileInfo(f).CreationTime)
                        .First());

                    lblUltimoBackup.Text = $"Último Backup Realizado: {lastFile.CreationTime}";
                }
                else
                {
                    lblUltimoBackup.Text = "Último Backup Realizado: Ninguno";
                }
            }
            catch (Exception ex)
            {
                lblUltimoBackup.Text = "Error al obtener último backup";
                MessageBox.Show($"Error al obtener último backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarListaBackups()
        {
            try
            {
                cboBackups.Items.Clear();
                // Usar un patrón más genérico para encontrar todos los archivos .xml
                var files = Directory.GetFiles(backupFolder, "*.xml");

                // Ordenar por fecha de creación
                var orderedFiles = files
                    .OrderByDescending(f => new FileInfo(f).CreationTime)
                    .ToList();

                foreach (var file in orderedFiles)
                {
                    var fileInfo = new FileInfo(file);
                    cboBackups.Items.Add($"{fileInfo.Name} - {fileInfo.CreationTime}");
                }

                if (cboBackups.Items.Count > 0)
                    cboBackups.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar lista de backups: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestaurar_Click_1(object sender, EventArgs e)
        {
                                     
        }

        

        private void btnCrearBackup_Click(object sender, EventArgs e)
        {
            try
            {
                // Solicitar al usuario que elija dónde guardar el backup
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Title = "Guardar Exportación de Base de Datos",
                    Filter = "Archivos XML (*.xml)|*.xml",
                    FileName = $"SupermercadoDB_Export_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xml",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                Cursor = Cursors.WaitCursor;

                string exportFilePath = saveDialog.FileName;
                ExportarBaseDatosAXml(exportFilePath);

                // Actualizar historial de backups
                try
                {
                    backupFolder = Path.Combine(Application.StartupPath, "Backups");
                    if (!Directory.Exists(backupFolder))
                    {
                        Directory.CreateDirectory(backupFolder);
                    }

                    string fileName = Path.GetFileName(exportFilePath);
                    string appBackupPath = Path.Combine(backupFolder, fileName);

                    // Solo copiar si no es la misma ubicación
                    if (!string.Equals(exportFilePath, appBackupPath, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Copy(exportFilePath, appBackupPath, true);
                    }
                }
                catch
                {
                    // Si falla la copia, al menos tenemos el archivo original
                }

                // Refrescar la interfaz
                this.Refresh();
                CargarFechaUltimoBackup();
                CargarListaBackups();

                MessageBox.Show($"Exportación de datos creada correctamente en:\n{exportFilePath}",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar datos: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ExportarBaseDatosAXml(string filePath)
        {
            using (var contexto = new MODELO.Contexto())
            {
                DataSet ds = new DataSet("SupermercadoDB");

                // Exportar tabla Clientes
                var clientes = contexto.Clientes.AsNoTracking().ToList();
                DataTable dtClientes = ConvertirListaADataTable(clientes, "Clientes");
                ds.Tables.Add(dtClientes);

                // Exportar tabla Proveedores
                var proveedores = contexto.Proveedores.AsNoTracking().ToList();
                DataTable dtProveedores = ConvertirListaADataTable(proveedores, "Proveedores");
                ds.Tables.Add(dtProveedores);

                // Exportar tabla Productos
                var productos = contexto.Productos.AsNoTracking().ToList();
                DataTable dtProductos = ConvertirListaADataTable(productos, "Productos");
                ds.Tables.Add(dtProductos);

                // Exportar tabla Ventas
                var ventas = contexto.Ventas.AsNoTracking().ToList();
                DataTable dtVentas = ConvertirListaADataTable(ventas, "Ventas");
                ds.Tables.Add(dtVentas);

                // Exportar tabla Usuarios
                var usuarios = contexto.Usuarios.AsNoTracking().ToList();
                DataTable dtUsuarios = ConvertirListaADataTable(usuarios, "Usuarios");
                ds.Tables.Add(dtUsuarios);

                // Guardar el DataSet a un archivo XML
                ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
            }
        }

        

        // Método genérico para convertir cualquier lista a DataTable
        private DataTable ConvertirListaADataTable<T>(List<T> items, string tableName)
        {
            DataTable dt = new DataTable(tableName);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));

            foreach (PropertyDescriptor prop in props)
            {
                // No incluir propiedades de navegación para evitar ciclos
                if (!prop.PropertyType.Namespace.StartsWith("System.Collections") &&
                    !typeof(ENTIDADES.Usuario).IsAssignableFrom(prop.PropertyType) &&
                    !typeof(ENTIDADES.Cliente).IsAssignableFrom(prop.PropertyType) &&
                    !typeof(ENTIDADES.Proveedor).IsAssignableFrom(prop.PropertyType) &&
                    !typeof(ENTIDADES.Producto).IsAssignableFrom(prop.PropertyType) &&
                    !typeof(ENTIDADES.Venta).IsAssignableFrom(prop.PropertyType))
                {
                    dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }

            foreach (T item in items)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyDescriptor prop in props)
                {
                    if (dt.Columns.Contains(prop.Name))
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        private void FormBackupRestauracionBD_Load_1(object sender, EventArgs e)
        {
            CargarFechaUltimoBackup();
            CargarListaBackups();
        }

        private void btnDescargarBackup_Click(object sender, EventArgs e)
        {
            if (cboBackups.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione un backup para descargar.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Obtener el nombre del archivo de backup seleccionado
                string backupFileName = cboBackups.SelectedItem.ToString().Split(new[] { " - " }, StringSplitOptions.None)[0];
                string backupFilePath = Path.Combine(backupFolder, backupFileName);

                if (!File.Exists(backupFilePath))
                {
                    MessageBox.Show($"El archivo de backup no existe: {backupFilePath}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mostrar diálogo para guardar archivo
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    FileName = backupFileName,
                    Filter = "Archivos XML (*.xml)|*.xml",
                    Title = "Guardar archivo de backup"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Copiar el archivo a la ubicación seleccionada
                    File.Copy(backupFilePath, saveDialog.FileName, true);

                    MessageBox.Show($"Backup descargado correctamente a:\n{saveDialog.FileName}",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al descargar el backup: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        
        

       

        
        private void ExecuteNonQuery(string sql, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error ejecutando SQL [{sql}]: {ex.Message}");
                throw;
            }
        }

        

    }
}