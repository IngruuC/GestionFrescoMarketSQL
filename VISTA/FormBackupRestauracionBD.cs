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
                // Buscar el archivo de backup más reciente
                var files = Directory.GetFiles(backupFolder, "SupermercadoDB_*.bak");
                if (files.Length > 0)
                {
                    var lastFile = new FileInfo(files[files.Length - 1]);
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
                var files = Directory.GetFiles(backupFolder, "SupermercadoDB_*.bak");
                Array.Sort(files);
                Array.Reverse(files); // Mostrar más recientes primero

                foreach (var file in files)
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
            if (cboBackups.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione un backup para restaurar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "¿Está seguro que desea restaurar la base de datos desde el backup seleccionado?\n\n" +
                "ADVERTENCIA: Todos los datos actuales serán reemplazados con los datos del backup.\n" +
                "Esta operación no se puede deshacer.",
                "Confirmar Restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Extraer nombre de base de datos y server de la cadena de conexión
                    string dbName = "SupermercadoDB"; // Por defecto
                    var builder = new SqlConnectionStringBuilder(connectionString);
                    if (!string.IsNullOrEmpty(builder.InitialCatalog))
                        dbName = builder.InitialCatalog;

                    // Obtener nombre del archivo de backup seleccionado
                    string backupFileName = cboBackups.SelectedItem.ToString().Split(new[] { " - " }, StringSplitOptions.None)[0];
                    string backupFilePath = Path.Combine(backupFolder, backupFileName);

                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        connection.Open();

                        // Cerrar todas las conexiones a la base de datos
                        string setOfflineCommand = $@"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                        using (SqlCommand command = new SqlCommand(setOfflineCommand, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Restaurar la base de datos
                        string restoreCommand = $@"RESTORE DATABASE [{dbName}] FROM DISK = '{backupFilePath}' WITH REPLACE";
                        using (SqlCommand command = new SqlCommand(restoreCommand, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Volver a poner la base de datos en modo multi-usuario
                        string setOnlineCommand = $@"ALTER DATABASE [{dbName}] SET MULTI_USER";
                        using (SqlCommand command = new SqlCommand(setOnlineCommand, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    MessageBox.Show("Base de datos restaurada exitosamente. La aplicación se reiniciará para aplicar los cambios.",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reiniciar la aplicación
                    Application.Restart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al restaurar la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void btnCrearBackup_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Extraer nombre de base de datos de la cadena de conexión
                string dbName = "SupermercadoDB"; // Por defecto
                var builder = new SqlConnectionStringBuilder(connectionString);
                if (!string.IsNullOrEmpty(builder.InitialCatalog))
                    dbName = builder.InitialCatalog;

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFileName = $"{dbName}_{timestamp}.bak";

                // Usar el procedimiento almacenado para determinar la ruta predeterminada de backup
                string backupPath = string.Empty;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("DECLARE @backupPath NVARCHAR(512); EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\\Microsoft\\MSSQLServer\\MSSQLServer', N'BackupDirectory', @backupPath OUTPUT, 'no_output'; SELECT @backupPath;", connection))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            backupPath = result.ToString();
                        }
                        else
                        {
                            // Si no se puede obtener la ruta, usar una predeterminada que funcione
                            backupPath = @"C:\Temp";
                            if (!Directory.Exists(backupPath))
                            {
                                Directory.CreateDirectory(backupPath);
                            }
                        }
                    }

                    string backupFilePath = Path.Combine(backupPath, backupFileName);

                    // Crear el backup
                    string backupCommand = $"BACKUP DATABASE [{dbName}] TO DISK = N'{backupFilePath}'";
                    using (SqlCommand command = new SqlCommand(backupCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Guardar una copia en la carpeta de la aplicación
                    backupFolder = Path.Combine(Application.StartupPath, "Backups");
                    if (!Directory.Exists(backupFolder))
                    {
                        Directory.CreateDirectory(backupFolder);
                    }

                    if (File.Exists(backupFilePath))
                    {
                        File.Copy(backupFilePath, Path.Combine(backupFolder, backupFileName), true);
                    }

                    CargarFechaUltimoBackup();
                    CargarListaBackups();

                    MessageBox.Show($"Backup creado correctamente en:\n{backupFilePath}\n\nY copiado a la carpeta de la aplicación",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error detallado al crear backup: {ex.Message}\n\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
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
                    Filter = "Archivos de backup (*.bak)|*.bak",
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
    }
}
