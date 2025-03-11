using CONTROLADORA;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VISTA
{
    public partial class FormRegistroProveedor : Form
    {
        
        private ControladoraProveedor controladora;
        private ControladoraProducto controladoraProducto;

        private List<Producto> productosDisponibles = new List<Producto>();
        private List<Producto> productosAsignados = new List<Producto>();

        public FormRegistroProveedor()
        {
            InitializeComponent();
        controladora = ControladoraProveedor.ObtenerInstancia();
            InicializarDataGridView();
            ConfigurarControles();
            CargarDatosEnDataGridView();
        }

        private void InicializarDataGridView()
        {
            dgvProveedores.AutoGenerateColumns = false;
            dgvProveedores.ReadOnly = true;
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.AllowUserToDeleteRows = false;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Columnas para los datos básicos del proveedor
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Cuit",
                HeaderText = "CUIT",
                Width = 120
            });

            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RazonSocial",
                HeaderText = "Razón Social",
                Width = 180
            });

            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Telefono",
                HeaderText = "Teléfono",
                Width = 120
            });

            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 150
            });

            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Direccion",
                HeaderText = "Dirección",
                Width = 180
            });

            // Columnas para la información del usuario
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UsuarioId",
                HeaderText = "UsuarioId",
                Width = 80
            });

            // Columna especial para el nombre de usuario
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreUsuario",  // Nombre interno de la columna
                HeaderText = "Nombre de Usuario",
                Width = 150
            });

            // Columna para los datos completos (si la tienes)
            dgvProveedores.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DatosCompletos",
                HeaderText = "Datos Completos",
                Width = 200
            });

            // Agregar el evento CellFormatting
            dgvProveedores.CellFormatting += DgvProveedores_CellFormatting;
        }


        private void DgvProveedores_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Si es la columna "NombreUsuario" y el valor de la celda no está formateado aún
            if (dgvProveedores.Columns[e.ColumnIndex].Name == "NombreUsuario" && e.RowIndex >= 0)
            {
                // Obtener el proveedor de la fila actual
                var proveedor = (Proveedor)dgvProveedores.Rows[e.RowIndex].DataBoundItem;

                // Verificar si tiene un usuario asignado
                if (proveedor.Usuario != null)
                {
                    e.Value = proveedor.Usuario.NombreUsuario;
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
            txtCuit.MaxLength = 11;
            txtRazonSocial.MaxLength = 100;
            txtTelefono.MaxLength = 20;
            txtEmail.MaxLength = 100;
            txtDireccion.MaxLength = 100;
            
        }

        private void LimpiarCampos()
        {
            txtCuit.Clear();
            txtRazonSocial.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
            btnGuardar.Tag = null;
            txtCuit.Focus();
        }

        private void CargarDatosEnDataGridView()
        {
            try
            {
                dgvProveedores.DataSource = null;
               dgvProveedores.DataSource = controladora.ObtenerProveedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtCuit.Text) || txtCuit.Text.Length != 11)
            {
                MessageBox.Show("El CUIT debe contener 11 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuit.Focus();
                return false;
            }

            if (!txtCuit.Text.All(char.IsDigit))
            {
                MessageBox.Show("El CUIT debe contener solo números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCuit.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
            {
                MessageBox.Show("La razón social es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRazonSocial.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                }
                catch
                {
                    MessageBox.Show("El formato del email no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
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
                var proveedor = new Proveedor
                {
                    Cuit = txtCuit.Text.Trim(),
                    RazonSocial = txtRazonSocial.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                controladora.AgregarProveedor(proveedor);
                MessageBox.Show("Proveedor guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el proveedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un proveedor para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var proveedor = (Proveedor)dgvProveedores.SelectedRows[0].DataBoundItem;
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar al proveedor {proveedor.RazonSocial}?",
                                             "Confirmar eliminación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    controladora.EliminarProveedor(proveedor.Id);
                    MessageBox.Show("Proveedor eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDatosEnDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar el proveedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un proveedor para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var proveedor = (Proveedor)dgvProveedores.SelectedRows[0].DataBoundItem;
            txtCuit.Text = proveedor.Cuit;
            txtRazonSocial.Text = proveedor.RazonSocial;
            txtTelefono.Text = proveedor.Telefono;
            txtEmail.Text = proveedor.Email;
            txtDireccion.Text = proveedor.Direccion;
            btnGuardar.Tag = proveedor.Id;
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscarProveedor.Clear();
            CargarDatosEnDataGridView();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscarProveedor_Click_1(object sender, EventArgs e)
        {
            string criterio = txtBuscarProveedor.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(criterio))
            {
                CargarDatosEnDataGridView();
                return;
            }

            try
            {
                var proveedores = controladora.ObtenerProveedores();
                var proveedoresFiltrados = proveedores.Where(p =>
                    p.Cuit.ToLower().Contains(criterio) ||
                    p.RazonSocial.ToLower().Contains(criterio) ||
                    p.Email.ToLower().Contains(criterio) ||
                    p.Telefono.ToLower().Contains(criterio) ||
                    p.Direccion.ToLower().Contains(criterio)
                ).ToList();

                dgvProveedores.DataSource = null;
                dgvProveedores.DataSource = proveedoresFiltrados;

                if (proveedoresFiltrados.Count == 0)
                {
                    MessageBox.Show("No se encontraron proveedores que coincidan con el criterio de búsqueda.",
                                  "Búsqueda",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar proveedores: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (btnGuardar.Tag == null)
            {
                MessageBox.Show("Por favor, primero seleccione un proveedor para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidarDatos()) return;

            try
            {
                var proveedor = new Proveedor
                {
                    Id = (int)btnGuardar.Tag,
                    Cuit = txtCuit.Text.Trim(),
                    RazonSocial = txtRazonSocial.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                controladora.ModificarProveedor(proveedor);
                MessageBox.Show("Proveedor actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDatosEnDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el proveedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarProducto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta funcionalidad se implementará en una futura versión.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAsignarProductos_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor para asignar productos.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var proveedor = (Proveedor)dgvProveedores.SelectedRows[0].DataBoundItem;

            try
            {
                using (var formAsignar = new FormAsignarProductos(proveedor.Id, proveedor.RazonSocial))
                {
                    formAsignar.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario de asignación: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignarUsuarios_Click(object sender, EventArgs e)
        {

            try
            {
                var controladora = ControladoraProveedor.ObtenerInstancia();
                var controladoraUsuario = ControladoraUsuario.ObtenerInstancia();

                // Obtener todos los proveedores
                var proveedores = controladora.ObtenerProveedores();

                // Filtrar proveedores sin usuario
                var proveedoresSinUsuario = proveedores.Where(p => p.UsuarioId == null).ToList();

                if (proveedoresSinUsuario.Count == 0)
                {
                    MessageBox.Show("Todos los proveedores ya tienen usuarios asignados.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Confirmar la operación
                var resultado = MessageBox.Show(
                    $"Se encontraron {proveedoresSinUsuario.Count} proveedores sin usuario asignado.\n" +
                    "¿Desea crear usuarios automáticamente para estos proveedores?\n\n" +
                    "- Se usará el formato proveedor_CUIT como nombre de usuario\n" +
                    "- Se usará el CUIT como contraseña inicial\n" +
                    "- El proveedor deberá cambiar su contraseña al iniciar sesión",
                    "Confirmar asignación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.No)
                    return;

                // Obtener grupo Proveedor
                var grupoProveedor = controladoraUsuario.ObtenerGrupoPorNombre("Proveedor");
                if (grupoProveedor == null)
                {
                    MessageBox.Show("No se encontró el grupo 'Proveedor'. Por favor, cree este grupo primero.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Guardamos solo el ID del grupo para evitar problemas de rastreo de entidades
                int grupoProveedorId = grupoProveedor.Id;

                int proveedoresActualizados = 0;
                StringBuilder logErrores = new StringBuilder();

                // Procesar cada proveedor sin usuario
                foreach (var proveedor in proveedoresSinUsuario)
                {
                    try
                    {
                        // Verificar si ya existe un usuario con ese nombre
                        string nombreUsuario = $"proveedor_{proveedor.Cuit}";
                        if (controladoraUsuario.ExisteUsuario(nombreUsuario))
                        {
                            // Intenta con una variante
                            nombreUsuario = $"proveedor_{proveedor.Cuit}_{proveedor.Id}";
                            if (controladoraUsuario.ExisteUsuario(nombreUsuario))
                            {
                                logErrores.AppendLine($"No se pudo crear usuario para {proveedor.RazonSocial}: " +
                                                   $"Ya existe un usuario con nombre {nombreUsuario}");
                                continue;
                            }
                        }

                        // Crear usuario con su grupo
                        var usuario = new Usuario
                        {
                            NombreUsuario = nombreUsuario,
                            Contraseña = proveedor.Cuit, // Se hasheará automáticamente
                            Email = $"{nombreUsuario}@frescomarket.com",
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            IntentosIngreso = 0,
                            Rol = "Proveedor"
                        };

                        // Utilizar el método de agregar usuario con grupo
                        controladoraUsuario.AgregarUsuarioConGrupo(usuario, grupoProveedorId);

                        // Actualizar el proveedor con el ID del usuario
                        proveedor.UsuarioId = usuario.Id;
                        controladora.ActualizarUsuarioEnProveedor(proveedor.Id, usuario.Id);

                        proveedoresActualizados++;
                    }
                    catch (Exception ex)
                    {
                        logErrores.AppendLine($"Error con proveedor {proveedor.RazonSocial}: {ex.Message}");
                    }
                }

                // Mostrar resultados
                string mensaje = $"Proceso completado.\nProveedores actualizados: {proveedoresActualizados} de {proveedoresSinUsuario.Count}";
                if (logErrores.Length > 0)
                {
                    mensaje += $"\n\nErrores encontrados:\n{logErrores}";
                }

                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK,
                    proveedoresActualizados == proveedoresSinUsuario.Count ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

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
