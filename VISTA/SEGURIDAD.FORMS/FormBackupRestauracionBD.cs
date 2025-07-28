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
        private string backupDirectory;
        private string ultimoBackup = string.Empty;


        public FormBackupRestauracionBD()
        {

            InitializeComponent();
            // Obtener la cadena de conexión desde una instancia del contexto
            using (var contexto = new MODELO.Contexto())
            {
                connectionString = contexto.Database.Connection.ConnectionString;
            }

            // Crear el directorio de backups si no existe
            backupDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SupermercadoBackups");
            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            CargarUltimoBackUp();
            CargarListaBackups();
        }

        //
        private void CargarUltimoBackUp()
        {
            try
            {
                if (Directory.Exists(backupDirectory))
                {
                    var archivos = Directory.GetFiles(backupDirectory, "*.bak")
                                           .OrderByDescending(f => new FileInfo(f).CreationTime)
                                           .ToList();

                    if (archivos.Any())
                    {
                        ultimoBackup = archivos.First();
                        DateTime fechaUltimoBackup = File.GetCreationTime(ultimoBackup);
                        lblUltimoBackup.Text = $"Último Backup Realizado: {fechaUltimoBackup.ToString("dd/MM/yyyy HH:mm:ss")}";
                    }
                    else
                    {
                        lblUltimoBackup.Text = "Último Backup Realizado: No se encontraron backups";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el último backup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblUltimoBackup.Text = "Último Backup Realizado: Error al cargar";
            }
        }

        private void CargarListaBackups()
        {
            try
            {
                cboBackups.Items.Clear();
                var files = Directory.GetFiles(backupDirectory, "*.bak");

                // Ordenar por fecha de creación descendente
                var orderedFiles = files
                    .OrderByDescending(f => new FileInfo(f).CreationTime)
                    .ToList();

                foreach (var file in orderedFiles)
                {
                    var fileInfo = new FileInfo(file);
                    cboBackups.Items.Add($"{fileInfo.Name} - {fileInfo.CreationTime.ToString("dd/MM/yyyy HH:mm:ss")}");
                }

                if (cboBackups.Items.Count > 0)
                    cboBackups.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar lista de backups: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearBackup_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string resultado = GenerarBKP();
                Cursor = Cursors.Default;

                if (resultado.EndsWith(".bak"))
                {
                    MessageBox.Show("Backup creado correctamente en: " + resultado, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarUltimoBackUp();
                    CargarListaBackups();
                }
                else
                {
                    MessageBox.Show("Error al crear el backup: " + resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Error al crear el backup: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void FormBackupRestauracionBD_Load_1(object sender, EventArgs e)
        {
            CargarUltimoBackUp();
            CargarListaBackups();
        }



        private void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            if (cboBackups.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, seleccione un backup para restaurar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Obtener el nombre del archivo seleccionado
                string backupFileName = cboBackups.SelectedItem.ToString().Split(new[] { " - " }, StringSplitOptions.None)[0];
                string backupFilePath = Path.Combine(backupDirectory, backupFileName);

                if (!File.Exists(backupFilePath))
                {
                    MessageBox.Show("El archivo de backup seleccionado no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("¿Está seguro de restaurar la base de datos? Esta acción no se puede deshacer.\n\n" +
                                   "IMPORTANTE: Se cerrarán todas las conexiones a la base de datos.",
                                   "Confirmar restauración",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // Deshabilitamos los controles durante la restauración
                    btnCrearBackup.Enabled = false;
                    btnRestaurar.Enabled = false;
                    cboBackups.Enabled = false;

                    MessageBox.Show("La restauración comenzará ahora. Este proceso puede tardar varios minutos.\n" +
                                   "La aplicación podría parecer que no responde durante este tiempo.\n" +
                                   "Por favor, espere hasta que se complete.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Cursor = Cursors.WaitCursor;
                    btnRestaurar.Text = "Restaurando...";
                    Application.DoEvents(); // Permite que la UI se actualice

                    string resultado = RestaurarBKP(backupFilePath);

                    Cursor = Cursors.Default;
                    btnRestaurar.Text = "Restaurar BD";

                    // Rehabilitamos los controles
                    btnCrearBackup.Enabled = true;
                    btnRestaurar.Enabled = true;
                    cboBackups.Enabled = true;

                    if (resultado == "Restauración realizada con éxito")
                    {
                        MessageBox.Show(resultado + "\n\nEs recomendable reiniciar la aplicación para asegurar que los cambios se apliquen correctamente.",
                                       "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(resultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                btnRestaurar.Text = "Restaurar BD";
                btnCrearBackup.Enabled = true;
                btnRestaurar.Enabled = true;
                cboBackups.Enabled = true;
                MessageBox.Show("Error al restaurar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string GenerarBKP()
        {
            // Obtener el nombre de la base de datos desde la cadena de conexión
            string nombreBD = ObtenerNombreBaseDatos();

            string nombreBackup = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}.bak",
                DateTime.Today.Day.ToString(),
                DateTime.Today.Month.ToString(),
                DateTime.Today.Year.ToString(),
                DateTime.Now.Hour.ToString(),
                DateTime.Now.Minute.ToString(),
                DateTime.Now.Second.ToString(),
                nombreBD);

            string rutaCompleta = Path.Combine(backupDirectory, nombreBackup);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        connection.Open();
                        command.CommandText = $"BACKUP DATABASE [{nombreBD}] TO DISK = N'" +
                            rutaCompleta +
                            $"' WITH NOFORMAT, NOINIT, NAME = N'{nombreBackup}', SKIP, NOREWIND, NOUNLOAD, STATS = 10";
                        command.CommandType = System.Data.CommandType.Text;
                        command.ExecuteNonQuery();
                        connection.Close();
                        return rutaCompleta;
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        return ex.Message;
                    }
                }
            }
        }

        // Método para restaurar el backup
        public string RestaurarBKP(string path)
        {
            // Obtener el nombre de la base de datos desde la cadena de conexión
            string nombreBD = ObtenerNombreBaseDatos();

            // Conexión a la base de datos master para restaurar
            string masterConnectionString = connectionString.Replace($"Initial Catalog={nombreBD}", "Initial Catalog=master");

            using (SqlConnection connection = new SqlConnection(masterConnectionString))
            {
                try
                {
                    connection.Open();

                    // Cerrar todas las conexiones a la base de datos
                    string killQuery = $@"
                DECLARE @kill varchar(8000) = '';
                SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'
                FROM sys.dm_exec_sessions
                WHERE database_id = DB_ID('{nombreBD}')
                AND session_id <> @@SPID;
                EXEC(@kill);";

                    using (SqlCommand killCommand = new SqlCommand(killQuery, connection))
                    {
                        killCommand.ExecuteNonQuery();
                    }

                    // Cambiar la base de datos a modo SINGLE_USER
                    string singleUserQuery = $"ALTER DATABASE [{nombreBD}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    using (SqlCommand singleUserCommand = new SqlCommand(singleUserQuery, connection))
                    {
                        singleUserCommand.ExecuteNonQuery();
                    }

                    // Obtener la ruta de datos predeterminada de SQL Server
                    string defaultDataPath = string.Empty;
                    string defaultLogPath = string.Empty;

                    string pathQuery = "SELECT SERVERPROPERTY('InstanceDefaultDataPath') AS DataPath, SERVERPROPERTY('InstanceDefaultLogPath') AS LogPath";
                    using (SqlCommand pathCommand = new SqlCommand(pathQuery, connection))
                    {
                        using (SqlDataReader reader = pathCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                defaultDataPath = reader["DataPath"]?.ToString() ?? "";
                                defaultLogPath = reader["LogPath"]?.ToString() ?? "";
                            }
                        }
                    }

                    // Si no se pueden obtener las rutas predeterminadas, usar rutas alternativas
                    if (string.IsNullOrEmpty(defaultDataPath))
                    {
                        defaultDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\";
                        defaultLogPath = defaultDataPath;
                    }

                    // Restaurar la base de datos con MOVE para especificar nuevas ubicaciones
                    string restoreQuery = $@"RESTORE DATABASE [{nombreBD}] FROM DISK = N'{path}' 
                WITH MOVE '{nombreBD}' TO '{Path.Combine(defaultDataPath, nombreBD + ".mdf")}',
                MOVE '{nombreBD}_log' TO '{Path.Combine(defaultLogPath, nombreBD + "_log.ldf")}',
                REPLACE, STATS = 10";

                    using (SqlCommand restoreCommand = new SqlCommand(restoreQuery, connection))
                    {
                        restoreCommand.CommandTimeout = 300; // 5 minutos de timeout para operaciones grandes
                        restoreCommand.ExecuteNonQuery();
                    }

                    // Cambiar la base de datos a modo MULTI_USER
                    string multiUserQuery = $"ALTER DATABASE [{nombreBD}] SET MULTI_USER";
                    using (SqlCommand multiUserCommand = new SqlCommand(multiUserQuery, connection))
                    {
                        multiUserCommand.ExecuteNonQuery();
                    }

                    connection.Close();
                    return "Restauración realizada con éxito";
                }
                catch (Exception ex)
                {
                    // Si algo falla, intentamos volver a modo MULTI_USER para no dejar la BD bloqueada
                    try
                    {
                        string emergencyResetQuery = $"ALTER DATABASE [{nombreBD}] SET MULTI_USER";
                        using (SqlCommand emergencyCommand = new SqlCommand(emergencyResetQuery, connection))
                        {
                            emergencyCommand.ExecuteNonQuery();
                        }
                    }
                    catch { /* Ignoramos errores aquí, estamos en modo de recuperación */ }

                    connection.Close();
                    return "Error al restaurar: " + ex.Message;
                }
            }
        }

        private string ObtenerNombreBaseDatos()
        {
            // Extraer el nombre de la base de datos de la cadena de conexión
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }

     

    private void btnDescargar_Click(object sender, EventArgs e)
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
                string backupFilePath = Path.Combine(backupDirectory, backupFileName);

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