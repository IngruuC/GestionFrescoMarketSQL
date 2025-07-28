using CONTROLADORA;
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
using MODELO;
using VISTA.IU_ADMIN;

namespace VISTA.SEGURIDAD.FORMS
{
    public partial class FormGestionarUsuarios : Form
    {
        #region Mover la ventana
        [DllImport("User32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void formGestionarUsuarios_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private ControladoraSeguridad controladoraSeguridad;
        public FormGestionarUsuarios()
        {
            InitializeComponent();
            controladoraSeguridad = ControladoraSeguridad.Instancia;
            dgvGestionarUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvGestionarUsuarios.CellFormatting += DgvGestionarUsuarios_CellFormatting;
            ActualizarGrilla();
            CargarComboBoxes();
        }
        private void ActualizarGrilla()
        {
            dgvGestionarUsuarios.DataSource = null;
            dgvGestionarUsuarios.DataSource = controladoraSeguridad.RecuperarUsuarios();

            // Ocultar columnas sensibles y técnicas
            if (dgvGestionarUsuarios.Columns.Contains("Contraseña"))
                dgvGestionarUsuarios.Columns["Contraseña"].Visible = false;
            if (dgvGestionarUsuarios.Columns.Contains("Id"))
                dgvGestionarUsuarios.Columns["Id"].Visible = false;
            if (dgvGestionarUsuarios.Columns.Contains("Clave"))
                dgvGestionarUsuarios.Columns["Clave"].Visible = false;
            if (dgvGestionarUsuarios.Columns.Contains("Perfil"))
                dgvGestionarUsuarios.Columns["Perfil"].Visible = false;
            if (dgvGestionarUsuarios.Columns.Contains("EstadoUsuarioId"))
                dgvGestionarUsuarios.Columns["EstadoUsuarioId"].Visible = false;
            if (dgvGestionarUsuarios.Columns.Contains("Clave_Alias"))
                dgvGestionarUsuarios.Columns["Clave_Alias"].Visible = false;
        }
        private void DgvGestionarUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvGestionarUsuarios.Rows[e.RowIndex].DataBoundItem is Usuario usuario)
            {
                string nombreColumna = dgvGestionarUsuarios.Columns[e.ColumnIndex].Name;

                switch (nombreColumna)
                {
                    case "NombreyApellido":
                    case "NombreApellido":
                        e.Value = usuario.NombreyApellido ?? "No especificado";
                        e.FormattingApplied = true;
                        break;

                    case "EstadoUsuario":
                        e.Value = usuario.EstadoUsuario?.Nombre ?? "Sin Estado";
                        e.FormattingApplied = true;
                        break;

                    case "Grupos":
                        if (usuario.Grupos != null && usuario.Grupos.Any())
                        {
                            e.Value = string.Join(", ", usuario.Grupos.Select(g => g.NombreGrupo));
                        }
                        else
                        {
                            e.Value = "Sin Grupos";
                        }
                        e.FormattingApplied = true;
                        break;

                    case "Clave":
                    case "Clave_Alias":
                        e.Value = "****"; // Ocultar contraseña
                        e.FormattingApplied = true;
                        break;
                }
            }
        }
        private void CargarComboBoxes()
        {
            // Cargar estados de usuario
            cmbCargarEstadoUsuario.Items.Clear();
            var estadosUsuario = controladoraSeguridad.RecuperarEstadosUsuario();
            cmbCargarEstadoUsuario.Items.AddRange(estadosUsuario.Select(e => e.Nombre).ToArray());

            // Cargar grupos
            cmbCargarGrupos.Items.Clear();
            var grupos = controladoraSeguridad.RecuperarGrupos();
            cmbCargarGrupos.Items.AddRange(grupos.Select(g => g.NombreGrupo).ToArray());

            // Cargar nombres de usuarios
            cmbNombreUsuarios.Items.Clear();
            var usuarios = controladoraSeguridad.RecuperarUsuarios();
            cmbNombreUsuarios.Items.AddRange(usuarios.Select(u => u.NombreUsuario).ToArray());
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
             formUsuario formUsuario = new formUsuario();
            if (formUsuario.ShowDialog() == DialogResult.OK)
            {
                ActualizarGrilla();
                 CargarComboBoxes();
            }
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvGestionarUsuarios.CurrentRow != null)
            {
                Usuario usuario = (Usuario)dgvGestionarUsuarios.CurrentRow.DataBoundItem;
                formUsuario frmUsuario = new formUsuario(usuario);
                if (frmUsuario.ShowDialog() == DialogResult.OK)
                {
                    ActualizarGrilla();
                    CargarComboBoxes();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario para modificar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvGestionarUsuarios.SelectedRows.Count > 0)
            {
                var usuarioSeleccionado = (Usuario)dgvGestionarUsuarios.CurrentRow.DataBoundItem;

                if (usuarioSeleccionado.NombreUsuario.ToLower() == "admin")
                {
                    MessageBox.Show("No se puede eliminar el usuario administrador.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar el usuario '{usuarioSeleccionado.NombreUsuario}'?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    var mensaje = controladoraSeguridad.EliminarUsuario(usuarioSeleccionado);
                    MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActualizarGrilla();
                    CargarComboBoxes();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnResetearUsuario_Click(object sender, EventArgs e)
        {
            if (dgvGestionarUsuarios.SelectedRows.Count > 0)
            {
                var usuarioSeleccionado = (Usuario)dgvGestionarUsuarios.CurrentRow.DataBoundItem;
                var confirmacion = MessageBox.Show($"¿Está seguro que desea resetear la clave del usuario '{usuarioSeleccionado.NombreUsuario}'?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    var mensaje = controladoraSeguridad.ResetearClave(usuarioSeleccionado);
                    MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para resetear su clave.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RealizarBusqueda();
        }

        private void RealizarBusqueda()
        {
            var usuarios = controladoraSeguridad.RecuperarUsuarios().ToList();

            // Filtro por nombre de usuario
            if (!string.IsNullOrEmpty(cmbNombreUsuarios.Text))
            {
                usuarios = usuarios.Where(u => u.NombreUsuario.ToLower().Contains(cmbNombreUsuarios.Text.ToLower())).ToList();
            }

            // Filtro por grupo
            if (!string.IsNullOrEmpty(cmbCargarGrupos.Text))
            {
                usuarios = usuarios.Where(u =>
                    u.Grupos != null &&
                    u.Grupos.Any(g => g.NombreGrupo.ToLower().Contains(cmbCargarGrupos.Text.ToLower()))
                ).ToList();
            }

            // Filtro por estado de usuario
            if (!string.IsNullOrEmpty(cmbCargarEstadoUsuario.Text))
            {
                usuarios = usuarios.Where(u =>
                    u.EstadoUsuario != null &&
                    u.EstadoUsuario.Nombre.ToLower() == cmbCargarEstadoUsuario.Text.ToLower()
                ).ToList();
            }

            // Verificar si se encontraron resultados
            if (usuarios.Count == 0)
            {
                string mensaje = "No se encontraron usuarios con los filtros seleccionados.";
                MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvGestionarUsuarios.DataSource = null;
                return;
            }

            dgvGestionarUsuarios.DataSource = null;
            dgvGestionarUsuarios.DataSource = usuarios;

            // Ocultar columnas sensibles
            if (dgvGestionarUsuarios.Columns.Contains("Contraseña"))
                dgvGestionarUsuarios.Columns["Contraseña"].Visible = false;
        }

        private void btnRefrescarGrilla_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
            ActualizarGrilla();
        }

        private void LimpiarFiltros()
        {
            cmbNombreUsuarios.Text = string.Empty;
            cmbCargarGrupos.SelectedIndex = -1;
            cmbCargarEstadoUsuario.SelectedIndex = -1;
        }

        private void FormGestionarUsuarios_Load(object sender, EventArgs e)
        {
            ActualizarGrilla();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
