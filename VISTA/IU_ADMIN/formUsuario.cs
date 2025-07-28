using ENTIDADES.SEGURIDAD;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CONTROLADORA;


namespace VISTA.IU_ADMIN
{
    public partial class formUsuario : Form
    {
        #region Mover la ventana
        [DllImport("User32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void formUsuario_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region Atributos privados
        private Usuario usuario;
        private bool modificar = false;
        private List<Grupo> gruposOriginales = new List<Grupo>();
        private List<Accion> accionesOriginales = new List<Accion>();
        private bool soloLectura = false;
        #endregion

        public formUsuario()
        {
                InitializeComponent();
            usuario = new Usuario();

            // IMPORTANTE: Inicializar las listas correctamente
            if (usuario.Perfil == null) usuario.Perfil = new List<object>();
            if (usuario.Grupos == null) usuario.Grupos = new List<Grupo>();

            ConfigurarFormulario();
            CargarDatosIniciales();
        }
        public formUsuario(Usuario usuarioModificar, bool soloLectura = false)
        {
            InitializeComponent();
            usuario = usuarioModificar;
            this.soloLectura = soloLectura;
            modificar = true;

            // IMPORTANTE: Inicializar las listas si son null
            if (usuario.Perfil == null) usuario.Perfil = new List<object>();
            if (usuario.Grupos == null) usuario.Grupos = new List<Grupo>();

            ConfigurarFormulario();
            CargarDatosIniciales();
        }

        private void formUsuario_Load(object sender, EventArgs e)
        {
            if (modificar)
            {
                lblAgregaroModificar.Text = "Modificar Usuario";
                txtNombreUsuario.Text = usuario.NombreUsuario;
                txtCorreoElectronico.Text = usuario.Email;
                txtNombreApellido.Text = usuario.NombreyApellido;
                if (usuario.EstadoUsuario != null)
                {
                    cmbCargarEstadosUsuario.SelectedItem = usuario.EstadoUsuario.Nombre;
                }
            }
            else
            {
                lblAgregaroModificar.Text = "Agregar Usuario";
            }
        }
        #region Configuración DGV para Prolijidad
        private void ConfigurarFormulario()
        {
            if (tabControl1.TabPages.Count >= 3)
            {
                tabControl1.TabPages[0].Text = "Datos";
                tabControl1.TabPages[1].Text = "Grupos";
                tabControl1.TabPages[2].Text = "Acciones";
            }

            // Configurar DataGridViews solo una vez
            ConfigurarDataGridViewGrupos();
            ConfigurarDataGridViewAcciones();

            // Si es modo solo lectura, desactivar controles de edición
            if (soloLectura)
            {
                txtNombreUsuario.ReadOnly = true;
                txtCorreoElectronico.ReadOnly = true;
                txtNombreApellido.ReadOnly = true;
                cmbCargarEstadosUsuario.Enabled = false;
                dgvGrupos.Enabled = false;
                dgvAcciones.Enabled = false;
                btnGuardar.Visible = false;
                btnCancelar.Text = "Cerrar";
                lblAgregaroModificar.Text = "Mis Datos";

                // Deshabilitar checkboxes en los DataGridViews
                if (dgvGrupos.Columns.Contains("Asignado"))
                    dgvGrupos.Columns["Asignado"].ReadOnly = true;
                if (dgvAcciones.Columns.Contains("Asignada"))
                    dgvAcciones.Columns["Asignada"].ReadOnly = true;
            }
            else
            {
                // Suscribir eventos solo si no es modo solo lectura
                dgvGrupos.SelectionChanged += DgvGrupos_SelectionChanged;
                dgvGrupos.CellClick += DgvGrupos_CellClick;
                dgvGrupos.CellValueChanged += DgvGrupos_CellValueChanged;
                dgvGrupos.CurrentCellDirtyStateChanged += DgvGrupos_CurrentCellDirtyStateChanged;
                dgvAcciones.CellClick += DgvAcciones_CellClick;
            }


        }
        // Método para manejar cambios inmediatos en checkboxes
        private void DgvGrupos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvGrupos.IsCurrentCellDirty)
            {
                dgvGrupos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Método para manejar cuando el valor del checkbox cambia
        private void DgvGrupos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que es la columna "Asignado"
            if (e.RowIndex >= 0 && dgvGrupos.Columns.Contains("Asignado") &&
                e.ColumnIndex == dgvGrupos.Columns["Asignado"].Index)
            {
                var grupo = dgvGrupos.Rows[e.RowIndex].DataBoundItem as Grupo;
                if (grupo != null)
                {
                    bool nuevoValor = Convert.ToBoolean(dgvGrupos.Rows[e.RowIndex].Cells["Asignado"].Value ?? false);

                    // Actualizar la lista de permisos del usuario
                    if (usuario.Perfil == null) usuario.Perfil = new List<object>();

                    if (nuevoValor)
                    {
                        // Agregar el grupo si no está ya asignado
                        if (!usuario.Perfil.Any(p => p is Grupo g && g.ComponenteId == grupo.ComponenteId))
                        {
                            usuario.AgregarPermiso(grupo);
                        }
                    }
                    else
                    {
                        // Remover el grupo
                        var grupoExistente = usuario.Perfil.FirstOrDefault(p => p is Grupo g && g.ComponenteId == grupo.ComponenteId);
                        if (grupoExistente != null)
                        {
                            usuario.EliminarPermiso(grupoExistente);
                        }
                    }

                    // Refrescar la vista de acciones basada en el grupo seleccionado
                    if (dgvGrupos.CurrentRow != null && dgvGrupos.CurrentRow.DataBoundItem is Grupo grupoSeleccionado)
                    {
                        ActualizarAccionesDGV(grupoSeleccionado);
                    }
                }
            }
        }

       

        private void ConfigurarDataGridViewGrupos()
        {
            // Limpiar el DataGridView
            dgvGrupos.DataSource = null;
            dgvGrupos.Columns.Clear();

            // Configuración básica
            dgvGrupos.AutoGenerateColumns = true; // Cambiar a true
            dgvGrupos.MultiSelect = false;
            dgvGrupos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrupos.AllowUserToAddRows = false;
            dgvGrupos.AllowUserToDeleteRows = false;
            dgvGrupos.ReadOnly = false;

            // Cargar los datos
            dgvGrupos.DataSource = ControladoraSeguridad.Instancia.RecuperarGrupos();

            // CREAR la columna checkbox "Asignado" después de que se generen las columnas automáticas
            if (!dgvGrupos.Columns.Contains("Asignado"))
            {
                var columnaAsignado = new DataGridViewCheckBoxColumn
                {
                    Name = "Asignado",
                    HeaderText = "Asignado",
                    Width = 80,
                    ReadOnly = false,
                    TrueValue = true,
                    FalseValue = false
                };
                dgvGrupos.Columns.Insert(0, columnaAsignado);
            }

            // Ocultar columnas innecesarias
            string[] columnasAOcultar = {
        "GrupoId", "ComponenteId", "Usuarios", "Permisos",
        "Acciones", "Hijos", "EstadoGrupoId", "Nombre", "Perfil", "Id"
    };

            foreach (string nombreCol in columnasAOcultar)
            {
                if (dgvGrupos.Columns.Contains(nombreCol))
                {
                    dgvGrupos.Columns[nombreCol].Visible = false;
                }
            }

            // Hacer que todas las columnas excepto "Asignado" sean de solo lectura
            foreach (DataGridViewColumn column in dgvGrupos.Columns)
            {
                if (column.Name != "Asignado")
                {
                    column.ReadOnly = true;
                }
            }
        }

        private void ConfigurarDataGridViewAcciones()
        {
            dgvAcciones.DataSource = null;
            dgvAcciones.DataSource = ControladoraSeguridad.Instancia.RecuperarAcciones();
            dgvAcciones.AutoGenerateColumns = false;
            dgvAcciones.MultiSelect = false;
            dgvAcciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // CREAR la columna checkbox "Asignada" si no existe
            if (!dgvAcciones.Columns.Contains("Asignada"))
            {
                var columnaAsignada = new DataGridViewCheckBoxColumn
                {
                    Name = "Asignada",
                    HeaderText = "Asignada",
                    Width = 80,
                    ReadOnly = false
                };
                dgvAcciones.Columns.Add(columnaAsignada);
                Console.WriteLine("Columna 'Asignada' creada en dgvAcciones");
            }

            // Ocultar columnas innecesarias
            if (dgvAcciones.Columns.Contains("Grupos"))
                dgvAcciones.Columns["Grupos"].Visible = false;
        }
        #endregion

        private void CargarDatosIniciales()
        {
            try
            {
                CargarCmb();
                CargarDatosUsuario();
                ActualizarGruposDGV();
                ActualizarAccionesDGV();

                // Llamar al método de debug temporalmente
                // VerificarDatos(); // Descomenta esta línea para verificar que los datos estén correctos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos iniciales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCmb()
        {
            cmbCargarEstadosUsuario.Items.Clear();
            foreach (EstadoUsuario estadoUsuario in ControladoraSeguridad.Instancia.RecuperarEstadosUsuario())
            {
                cmbCargarEstadosUsuario.Items.Add(estadoUsuario.Nombre);
            }
        }

        private void CargarDatosUsuario()
        {
            if (modificar)
            {
                txtNombreUsuario.Text = usuario.NombreUsuario;
                txtCorreoElectronico.Text = usuario.Email;
                txtNombreApellido.Text = usuario.NombreyApellido;
                if (usuario.EstadoUsuario != null)
                {
                    cmbCargarEstadosUsuario.SelectedItem = usuario.EstadoUsuario.Nombre;
                }
                lblAgregaroModificar.Text = "Modificar Usuario";

                // Guardar estados originales
                if (usuario.Perfil != null)
                {
                    gruposOriginales = usuario.Perfil.OfType<Grupo>().ToList();
                    accionesOriginales = usuario.Perfil.OfType<Accion>().ToList();
                }
            }
            else
            {
                cmbCargarEstadosUsuario.Items.Add("Seleccione un estado...");
                cmbCargarEstadosUsuario.SelectedItem = "Seleccione un estado...";
                lblAgregaroModificar.Text = "Agregar Usuario";
            }
        }

        #region Carga de datos
        private void ActualizarGruposDGV()
        {
            // Solo recargar los datos y marcar los checkboxes
            var grupos = ControladoraSeguridad.Instancia.RecuperarGrupos();
            dgvGrupos.DataSource = null;
            dgvGrupos.DataSource = grupos;

            // Marcar grupos asignados
            foreach (DataGridViewRow row in dgvGrupos.Rows)
            {
                var grupo = row.DataBoundItem as Grupo;
                if (grupo != null)
                {
                    bool estaAsignado = false;

                    if (usuario.Perfil != null)
                    {
                        estaAsignado = usuario.Perfil.Any(p => p is Grupo g && g.ComponenteId == grupo.ComponenteId);
                    }
                    else if (usuario.Grupos != null)
                    {
                        estaAsignado = usuario.Grupos.Any(g => g.Id == grupo.Id);
                    }

                    if (dgvGrupos.Columns.Contains("Asignado"))
                    {
                        row.Cells["Asignado"].Value = estaAsignado;
                    }
                }
            }
        }




        private void ActualizarAccionesDGV(Grupo grupoSeleccionado = null)
        {
            List<Accion> acciones;
            if (grupoSeleccionado != null && grupoSeleccionado.Hijos != null)
            {
                acciones = grupoSeleccionado.Hijos.OfType<Accion>().ToList();
            }
            else
            {
                acciones = ControladoraSeguridad.Instancia.RecuperarAcciones().ToList();
            }

            dgvAcciones.DataSource = null;
            dgvAcciones.DataSource = acciones;

            // Marcar acciones asignadas
            foreach (DataGridViewRow row in dgvAcciones.Rows)
            {
                var accion = row.DataBoundItem as Accion;
                if (accion != null)
                {
                    bool estaAsignada = false;
                    if (usuario.Perfil != null)
                    {
                        estaAsignada = usuario.Perfil.Any(p => p is Accion a && a.ComponenteId == accion.ComponenteId);
                    }

                    if (dgvAcciones.Columns.Contains("Asignada"))
                    {
                        row.Cells["Asignada"].Value = estaAsignada;
                    }
                }
            }

            // Ocultar columnas innecesarias después de cargar datos
            ConfigurarDataGridViewAcciones();
        }
        #endregion

        #region Manejo de checkboxes
        private void DgvGrupos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGrupos.CurrentRow != null && dgvGrupos.CurrentRow.DataBoundItem is Grupo grupoSeleccionado)
            {
                ActualizarAccionesDGV(grupoSeleccionado);
            }
        }


        private void DgvGrupos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se haya hecho clic en una fila válida y en la columna "Asignado"
            if (e.RowIndex >= 0 && dgvGrupos.Columns.Contains("Asignado") &&
                e.ColumnIndex == dgvGrupos.Columns["Asignado"].Index)
            {
                var grupo = dgvGrupos.Rows[e.RowIndex].DataBoundItem as Grupo;
                if (grupo != null)
                {
                    // Alternar el valor del checkbox
                    bool nuevoValor = !Convert.ToBoolean(dgvGrupos.Rows[e.RowIndex].Cells["Asignado"].Value ?? false);
                    dgvGrupos.Rows[e.RowIndex].Cells["Asignado"].Value = nuevoValor;

                    // Actualizar la lista de permisos del usuario
                    if (usuario.Perfil == null) usuario.Perfil = new List<object>();

                    if (nuevoValor)
                    {
                        // Agregar el grupo si no está ya asignado
                        if (!usuario.Perfil.Contains(grupo))
                        {
                            usuario.AgregarPermiso(grupo);
                        }
                    }
                    else
                    {
                        // Remover el grupo
                        usuario.EliminarPermiso(grupo);
                    }

                    // Refrescar la vista de acciones basada en el grupo seleccionado
                    if (dgvGrupos.CurrentRow != null && dgvGrupos.CurrentRow.DataBoundItem is Grupo grupoSeleccionado)
                    {
                        ActualizarAccionesDGV(grupoSeleccionado);
                    }
                }
            }
        }

        private void DgvAcciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvAcciones.Columns.Contains("Asignada") &&
                e.ColumnIndex == dgvAcciones.Columns["Asignada"].Index)
            {
                var accion = dgvAcciones.Rows[e.RowIndex].DataBoundItem as Accion;
                if (accion != null)
                {
                    bool nuevoValor = !Convert.ToBoolean(dgvAcciones.Rows[e.RowIndex].Cells["Asignada"].Value ?? false);
                    dgvAcciones.Rows[e.RowIndex].Cells["Asignada"].Value = nuevoValor;

                    if (usuario.Perfil == null) usuario.Perfil = new List<object>();

                    if (nuevoValor)
                    {
                        if (!usuario.Perfil.Contains(accion))
                        {
                            usuario.AgregarPermiso(accion);
                        }
                    }
                    else
                    {
                        usuario.EliminarPermiso(accion);
                    }
                }
            }
        }

        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    // Asignar datos básicos del usuario
                    usuario.NombreUsuario = txtNombreUsuario.Text.Trim();
                    usuario.Email = txtCorreoElectronico.Text.Trim();
                    usuario.NombreyApellido = txtNombreApellido.Text.Trim();

                    // IMPORTANTE: Asignar campos requeridos que faltan
                    if (string.IsNullOrEmpty(usuario.Rol))
                        usuario.Rol = "Usuario"; // Valor por defecto

                    usuario.Estado = true; // Asegurar que esté activo
                    usuario.IntentosIngreso = 0; // Resetear intentos

                    if (!modificar)
                    {
                        usuario.FechaCreacion = DateTime.Now;
                        usuario.Contraseña = "123456"; // Solo para usuarios nuevos
                    }

                    // Asignar estado de usuario
                    var estadoSeleccionado = ControladoraSeguridad.Instancia.RecuperarEstadosUsuario()
                        .FirstOrDefault(estado => estado.Nombre == cmbCargarEstadosUsuario.SelectedItem.ToString());

                    if (estadoSeleccionado != null)
                    {
                        usuario.EstadoUsuario = estadoSeleccionado;
                        usuario.EstadoUsuarioId = estadoSeleccionado.Id;
                    }

                    if (modificar)
                    {
                        DialogResult result = MessageBox.Show("¿Está seguro de que desea modificar el usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            // Obtener grupos desde la base de datos para evitar problemas de tracking
                            List<int> gruposSeleccionadosIds = new List<int>();
                            foreach (DataGridViewRow row in dgvGrupos.Rows)
                            {
                                if (row.Cells["Asignado"].Value != null && Convert.ToBoolean(row.Cells["Asignado"].Value))
                                {
                                    var grupo = row.DataBoundItem as Grupo;
                                    if (grupo != null)
                                    {
                                        gruposSeleccionadosIds.Add(grupo.Id);
                                    }
                                }
                            }

                            // Verificar que tenga al menos un grupo
                            if (gruposSeleccionadosIds.Count == 0)
                            {
                                MessageBox.Show("Debe seleccionar al menos un grupo para el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Obtener grupos frescos de la base de datos
                            var gruposFrescos = ControladoraSeguridad.Instancia.RecuperarGrupos()
                                .Where(g => gruposSeleccionadosIds.Contains(g.Id)).ToList();

                            // Limpiar y asignar grupos
                            if (usuario.Grupos == null) usuario.Grupos = new HashSet<Grupo>();
                            usuario.Grupos.Clear();

                            foreach (var grupo in gruposFrescos)
                            {
                                usuario.Grupos.Add(grupo);
                            }

                            // Limpiar perfil (es NotMapped, no se guarda en BD)
                            if (usuario.Perfil == null) usuario.Perfil = new List<object>();
                            usuario.Perfil.Clear();
                            usuario.Perfil.AddRange(gruposFrescos);

                            // Intentar guardar
                            var mensaje = ControladoraSeguridad.Instancia.ModificarUsuario(usuario);
                            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        // Para usuario nuevo
                        // Obtener grupos seleccionados por ID
                        List<int> gruposSeleccionadosIds = new List<int>();
                        foreach (DataGridViewRow row in dgvGrupos.Rows)
                        {
                            if (row.Cells["Asignado"].Value != null && Convert.ToBoolean(row.Cells["Asignado"].Value))
                            {
                                var grupo = row.DataBoundItem as Grupo;
                                if (grupo != null)
                                {
                                    gruposSeleccionadosIds.Add(grupo.Id);
                                }
                            }
                        }

                        // Verificar que tenga al menos un grupo
                        if (gruposSeleccionadosIds.Count == 0)
                        {
                            MessageBox.Show("Debe seleccionar al menos un grupo para el usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Obtener grupos frescos de la base de datos
                        var gruposFrescos = ControladoraSeguridad.Instancia.RecuperarGrupos()
                            .Where(g => gruposSeleccionadosIds.Contains(g.Id)).ToList();

                        // Inicializar y asignar grupos
                        if (usuario.Grupos == null) usuario.Grupos = new HashSet<Grupo>();
                        usuario.Grupos.Clear();

                        foreach (var grupo in gruposFrescos)
                        {
                            usuario.Grupos.Add(grupo);
                        }

                        // Asignar perfil (es NotMapped, no se guarda en BD)
                        if (usuario.Perfil == null) usuario.Perfil = new List<object>();
                        usuario.Perfil.Clear();
                        usuario.Perfil.AddRange(gruposFrescos);

                        // Intentar guardar
                        var mensaje = ControladoraSeguridad.Instancia.AgregarUsuario(usuario);
                        MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string errorDetallado = ex.Message;
                if (ex.InnerException != null)
                {
                    errorDetallado += "\nDetalle: " + ex.InnerException.Message;

                    // Si hay más niveles de InnerException
                    var innerEx = ex.InnerException;
                    while (innerEx.InnerException != null)
                    {
                        innerEx = innerEx.InnerException;
                        errorDetallado += "\nCausa raíz: " + innerEx.Message;
                    }
                }

                MessageBox.Show($"Error al guardar usuario:\n{errorDetallado}\n\nDatos del usuario:\nNombre: {usuario.NombreUsuario}\nEmail: {usuario.Email}\nRol: {usuario.Rol}\nEstado: {usuario.Estado}", "Error Detallado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (soloLectura)
            {
                this.Close();
            }
            else
            {
                if (MessageBox.Show("¿Está seguro que desea cancelar la operación?", "Confirmación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            }
        }

        // Método temporal para verificar que los datos estén en los DataGridView correctos
        private void VerificarDatos()
        {
            // Verificar dgvGrupos
            var gruposEnDGV = dgvGrupos.DataSource;
            MessageBox.Show($"dgvGrupos contiene: {gruposEnDGV?.GetType().Name}\nCantidad de filas: {dgvGrupos.Rows.Count}", "Debug - dgvGrupos");

            if (dgvGrupos.Rows.Count > 0 && dgvGrupos.Rows[0].DataBoundItem != null)
            {
                var tipoItem = dgvGrupos.Rows[0].DataBoundItem.GetType().Name;
                MessageBox.Show($"Primer item en dgvGrupos es de tipo: {tipoItem}", "Debug - Tipo dgvGrupos");
            }

            // Verificar dgvAcciones
            var accionesEnDGV = dgvAcciones.DataSource;
            MessageBox.Show($"dgvAcciones contiene: {accionesEnDGV?.GetType().Name}\nCantidad de filas: {dgvAcciones.Rows.Count}", "Debug - dgvAcciones");

            if (dgvAcciones.Rows.Count > 0 && dgvAcciones.Rows[0].DataBoundItem != null)
            {
                var tipoItem = dgvAcciones.Rows[0].DataBoundItem.GetType().Name;
                MessageBox.Show($"Primer item en dgvAcciones es de tipo: {tipoItem}", "Debug - Tipo dgvAcciones");
            }
        }

        #region Validaciones
        private bool ValidarCampos()
        {
            // Validar nombre de usuario
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
            {
                MessageBox.Show("Debe ingresar un nombre de usuario", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                txtNombreUsuario.Focus();
                return false;
            }

            // Validar longitud del nombre de usuario
            if (txtNombreUsuario.Text.Trim().Length > 50) // Ajusta según tu BD
            {
                MessageBox.Show("El nombre de usuario no puede exceder 50 caracteres", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                txtNombreUsuario.Focus();
                return false;
            }

            // Validar nombre y apellido
            if (string.IsNullOrWhiteSpace(txtNombreApellido.Text))
            {
                MessageBox.Show("Debe ingresar nombre y apellido", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                txtNombreApellido.Focus();
                return false;
            }

            // Validar email
            if (string.IsNullOrWhiteSpace(txtCorreoElectronico.Text))
            {
                MessageBox.Show("Debe ingresar un correo electrónico", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                txtCorreoElectronico.Focus();
                return false;
            }

            // Validar formato de email básico
            if (!txtCorreoElectronico.Text.Contains("@") || !txtCorreoElectronico.Text.Contains("."))
            {
                MessageBox.Show("El formato del correo electrónico no es válido", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                txtCorreoElectronico.Focus();
                return false;
            }

            // Validar estado
            if (cmbCargarEstadosUsuario.SelectedItem == null ||
                cmbCargarEstadosUsuario.SelectedItem.ToString() == "Seleccione un estado...")
            {
                MessageBox.Show("Debe seleccionar un estado", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                cmbCargarEstadosUsuario.Focus();
                return false;
            }

            // Validar que al menos un grupo esté seleccionado
            bool tieneGrupoSeleccionado = false;
            int gruposSeleccionados = 0;

            foreach (DataGridViewRow row in dgvGrupos.Rows)
            {
                if (row.Cells["Asignado"].Value != null && Convert.ToBoolean(row.Cells["Asignado"].Value))
                {
                    tieneGrupoSeleccionado = true;
                    gruposSeleccionados++;
                }
            }

            if (!tieneGrupoSeleccionado)
            {
                MessageBox.Show("Debe seleccionar al menos un grupo para el usuario", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 1)
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                return false;
            }

            // Mensaje informativo sobre grupos seleccionados
            Console.WriteLine($"Grupos seleccionados: {gruposSeleccionados}");

            return true;
        }
        #endregion
    }

}

