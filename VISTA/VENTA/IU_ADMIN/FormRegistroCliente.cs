using CONTROLADORA;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormRegistroCliente : Form
    {
        private ControladoraCliente controladora;

        public FormRegistroCliente()
        {
            InitializeComponent();
            controladora = ControladoraCliente.ObtenerInstancia();
            ConfigurarControles();
            CargarDatosEnDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvCliente.AutoGenerateColumns = false;

            // Columnas existentes
            dgvCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Documento",
                HeaderText = "Documento",
                Width = 100
            });

            dgvCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                Width = 150
            });

            dgvCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Apellido",
                HeaderText = "Apellido",
                Width = 150
            });

            dgvCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Direccion",
                HeaderText = "Direccion",
                Width = 200
            });

            dgvCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UsuarioId",
                HeaderText = "UsuarioId",
                Width = 80
            });

            // Nueva columna que muestra solo el nombre de usuario
            dgvCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreUsuario", // Nombre interno de la columna
                HeaderText = "Nombre de Usuario",
                Width = 150,
                // No podemos usar DataPropertyName directamente con propiedades anidadas
            });

            dgvCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DatosCompletos",
                HeaderText = "Datos Completos",
                Width = 250
            });

            // Configurar el evento CellFormatting para manejar la columna de nombre de usuario
            dgvCliente.CellFormatting += DgvCliente_CellFormatting;
        }

        private void DgvCliente_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Si es la columna "NombreUsuario" y el valor de la celda no está formateado aún
            if (dgvCliente.Columns[e.ColumnIndex].Name == "NombreUsuario" && e.RowIndex >= 0)
            {
                // Obtener el cliente de la fila actual
                var cliente = (Cliente)dgvCliente.Rows[e.RowIndex].DataBoundItem;

                // Verificar si tiene un usuario asignado
                if (cliente.Usuario != null)
                {
                    e.Value = cliente.Usuario.NombreUsuario;
                }
                else
                {
                    e.Value = "No asignado";
                }

                e.FormattingApplied = true;
            }
        }

        private void ConfigurarControles()
        {
            txtDocumento.MaxLength = 8;
            txtNombre.MaxLength = 50;
            txtApellido.MaxLength = 50;
            txtDireccion.MaxLength = 100;
            ConfigurarDataGridView();
        }

        private void LimpiarCampos()
        {
            txtDocumento.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            btnGuardar.Tag = null;
            txtDocumento.Focus();
        }

        private void CargarDatosEnDataGridView()
        {
            try
            {
                dgvCliente.DataSource = null;
                dgvCliente.DataSource = controladora.ObtenerClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("El documento es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            if (!txtDocumento.Text.All(char.IsDigit) || txtDocumento.Text.Length != 8)
            {
                MessageBox.Show("El documento debe contener exactamente 8 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("La dirección es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos()) return;

            try
            {
                // Crear el cliente
                var cliente = new Cliente
                {
                    Documento = txtDocumento.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                    // UsuarioId = null por defecto
                };

                // Guardar cliente SIN usuario
                controladora.AgregarCliente(cliente);

                MessageBox.Show("Cliente guardado exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un cliente para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cliente = (Cliente)dgvCliente.SelectedRows[0].DataBoundItem;
            txtDocumento.Text = cliente.Documento;
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtDireccion.Text = cliente.Direccion;
            btnGuardar.Tag = cliente.Id;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Tag == null)
            {
                MessageBox.Show("Por favor, primero seleccione un cliente para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidarDatos()) return;

            try
            {
                var cliente = new Cliente
                {
                    Id = (int)btnGuardar.Tag,
                    Documento = txtDocumento.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                controladora.ModificarCliente(cliente);
                MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un cliente para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cliente = (Cliente)dgvCliente.SelectedRows[0].DataBoundItem;
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar al cliente {cliente.Nombre} {cliente.Apellido}?",
                                             "Confirmar eliminación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    controladora.EliminarCliente(cliente.Id);
                    MessageBox.Show("Cliente eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDatosEnDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscarCliente.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(criterio))
            {
                CargarDatosEnDataGridView();
                return;
            }

            try
            {
                var clientes = controladora.ObtenerClientes();
                var clientesFiltrados = clientes.Where(c =>
                    c.Documento.ToLower().Contains(criterio) ||
                    c.Nombre.ToLower().Contains(criterio) ||
                    c.Apellido.ToLower().Contains(criterio) ||
                    c.Direccion.ToLower().Contains(criterio)
                ).ToList();

                dgvCliente.DataSource = null;
                dgvCliente.DataSource = clientesFiltrados;

                if (clientesFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron clientes que coincidan con el criterio de búsqueda.",
                                  "Búsqueda",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarCliente.Clear();
            CargarDatosEnDataGridView();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRegistroCliente_Load(object sender, EventArgs e)
        {
            CargarDatosEnDataGridView();
        }

        private void btnAsignarUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                var controladoraCliente = ControladoraCliente.ObtenerInstancia();
                var controladoraUsuario = ControladoraUsuario.ObtenerInstancia();

                // Obtener todos los clientes
                var clientes = controladoraCliente.ObtenerClientes();

                // Filtrar clientes sin usuario
                var clientesSinUsuario = clientes.Where(c => c.UsuarioId == null).ToList();

                if (clientesSinUsuario.Count == 0)
                {
                    MessageBox.Show("Todos los clientes ya tienen usuarios asignados.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirmar la operación
                var resultado = MessageBox.Show(
                    $"Se encontraron {clientesSinUsuario.Count} clientes sin usuario asignado.\n" +
                    "¿Desea crear usuarios automáticamente para estos clientes?\n\n" +
                    "- Se usará el formato cliente_DNI como nombre de usuario\n" +
                    "- Se usará el DNI como contraseña inicial\n" +
                    "- El cliente deberá cambiar su contraseña al iniciar sesión",
                    "Confirmar asignación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                    return;

                // Obtener ID del grupo Cliente (utilizando ControladoraUsuario)
                var grupoCliente = controladoraUsuario.ObtenerGrupoPorNombre("Cliente");
                if (grupoCliente == null)
                {
                    MessageBox.Show("No se encontró el grupo 'Cliente'. Por favor, cree este grupo primero.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Guardamos solo el ID del grupo para evitar problemas de rastreo de entidades
                int grupoClienteId = grupoCliente.Id;

                int clientesActualizados = 0;
                StringBuilder logErrores = new StringBuilder();

                // Procesar cada cliente sin usuario
                foreach (var cliente in clientesSinUsuario)
                {
                    try
                    {
                        // Verificar si ya existe un usuario con ese nombre
                        string nombreUsuario = $"cliente_{cliente.Documento}";
                        if (controladoraUsuario.ExisteUsuario(nombreUsuario))
                        {
                            // Intenta con una variante
                            nombreUsuario = $"cliente_{cliente.Documento}_{cliente.Id}";
                            if (controladoraUsuario.ExisteUsuario(nombreUsuario))
                            {
                                logErrores.AppendLine($"No se pudo crear usuario para {cliente.Nombre} {cliente.Apellido}: " +
                                                   $"Ya existe un usuario con nombre {nombreUsuario}");
                                continue;
                            }
                        }

                        // En lugar de usar RegistrarCliente, crearemos y asignaremos manualmente

                        // 1. Crear usuario con su grupo
                        var usuario = new Usuario
                        {
                            NombreUsuario = nombreUsuario,
                            Contraseña = cliente.Documento, // Se hasheará automáticamente
                            Email = $"{nombreUsuario}@frescomarket.com",
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            IntentosIngreso = 0,
                            Rol = "Cliente"
                        };

                        // Utilizar el método de agregar usuario con grupo
                        controladoraUsuario.AgregarUsuarioConGrupo(usuario, grupoClienteId);

                        // 2. Actualizar el cliente con el ID del usuario
                        cliente.UsuarioId = usuario.Id;
                        controladoraCliente.ActualizarUsuarioEnCliente(cliente.Id, usuario.Id);

                        clientesActualizados++;
                    }
                    catch (Exception ex)
                    {
                        logErrores.AppendLine($"Error con cliente {cliente.Nombre} {cliente.Apellido}: {ex.Message}");
                    }
                }

                // Mostrar resultados
                string mensaje = $"Proceso completado.\nClientes actualizados: {clientesActualizados} de {clientesSinUsuario.Count}";
                if (logErrores.Length > 0)
                {
                    mensaje += $"\n\nErrores encontrados:\n{logErrores}";
                }

                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK,
                    clientesActualizados == clientesSinUsuario.Count ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                // Recargar datos
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el proceso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}