using CONTROLADORA;
using ENTIDADES;
using ENTIDADES.SEGURIDAD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;


namespace VISTA.IU_ADMIN
{
    public partial class formGrupo : Form
    {
        #region Mover la ventana
        [DllImport("User32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void formGrupo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private Grupo grupo;
        private bool modificar = false;


        public formGrupo()
        {
            InitializeComponent();
            grupo = new Grupo();
            CargarCmb();
            ActualizarAccionesDGV();
            ConfigurarTabPages();
        }

        public formGrupo(Grupo grupoModificar)
        {
            InitializeComponent();
            grupo = grupoModificar;
            modificar = true;
            CargarCmb();
            ActualizarAccionesDGV();
            ConfigurarTabPages();
        }

        private void formGrupo_Load(object sender, EventArgs e)
        {
            if (modificar)
            {
                lblAgregaroModificar.Text = "Modificar grupo"; 

                txtCodigoGrupo.Text = grupo.Codigo?.ToString() ?? "";
                txtNombreGrupo.Text = grupo.NombreGrupo?.ToString() ?? "";
                txtDescripcionGrupo.Text = grupo.Descripcion?.ToString() ?? "";

                if (grupo.EstadoGrupo != null)
                {
                    cmbCargarEstadoGrupo.SelectedItem = grupo.EstadoGrupo.Nombre.ToString();
                }
            }
            else
            {
                cmbCargarEstadoGrupo.Items.Add("Seleccione un estado...");
                cmbCargarEstadoGrupo.SelectedItem = "Seleccione un estado...";
                lblAgregaroModificar.Text = "Agregar Grupo";
            }
        }
        private void ConfigurarTabPages()
        {
            if (tabControl1.TabPages.Count >= 2)
            {
                tabControl1.TabPages[0].Text = "Datos";
                tabControl1.TabPages[1].Text = "Acciones";
            }
        }

        public void CargarCmb()
        {
            cmbCargarEstadoGrupo.Items.Clear();
            foreach (EstadoGrupo estadoGrupo in ControladoraSeguridad.Instancia.RecuperarEstadosGrupo())
            {
                cmbCargarEstadoGrupo.Items.Add(estadoGrupo.Nombre.ToString());
            }
        }

        #region ActualizarAccionesDGV
        private void ActualizarAccionesDGV()
        {
            dgvAcciones.DataSource = null;
            dgvAcciones.DataSource = ControladoraSeguridad.Instancia.RecuperarAcciones();

            // Configurar el DataGridView
            dgvAcciones.ReadOnly = false;
            dgvAcciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Ocultar columnas innecesarias
            if (dgvAcciones.Columns.Contains("Grupos"))
                dgvAcciones.Columns["Grupos"].Visible = false;

            // Solo marcar las acciones si estamos modificando un grupo existente
            if (modificar && grupo.Hijos != null)
            {
                foreach (DataGridViewRow row in dgvAcciones.Rows)
                {
                    var accion = (Accion)row.DataBoundItem;
                    if (grupo.Hijos.Any(h => h is Accion a && a.ComponenteId == accion.ComponenteId))
                    {
                        row.Cells["Asignada"].Value = true;
                    }
                    else
                    {
                        row.Cells["Asignada"].Value = false;
                    }
                }
            }
            else
            {
                // Si es un nuevo grupo, asegurarse de que ninguna acción esté marcada
                foreach (DataGridViewRow row in dgvAcciones.Rows)
                {
                    row.Cells["Asignada"].Value = false;
                }
            }

            // Asegurarse de que el evento esté suscrito solo una vez
            dgvAcciones.CellClick -= DgvAcciones_CellClick;
            dgvAcciones.CellClick += DgvAcciones_CellClick;
        }

        private void DgvAcciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se haya hecho clic en la columna "Asignada"
            if (e.RowIndex >= 0 && dgvAcciones.Columns.Contains("Asignada") &&
                dgvAcciones.Columns["Asignada"].Index == e.ColumnIndex)
            {
                DataGridViewRow row = dgvAcciones.Rows[e.RowIndex];
                bool currentValue = row.Cells["Asignada"].Value != null && (bool)row.Cells["Asignada"].Value;
                row.Cells["Asignada"].Value = !currentValue;
            }
        }
        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    if (modificar)
                    {
                        DialogResult result = MessageBox.Show("¿Está seguro de que desea modificar el grupo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            grupo.Codigo = txtCodigoGrupo.Text;
                            grupo.NombreGrupo = txtNombreGrupo.Text;
                            grupo.Nombre = txtNombreGrupo.Text; // Para compatibilidad
                            grupo.Descripcion = txtDescripcionGrupo.Text;

                            var estadoSeleccionado = ControladoraSeguridad.Instancia.RecuperarEstadosGrupo()
                                .FirstOrDefault(estado => estado.Nombre == cmbCargarEstadoGrupo.SelectedItem.ToString());
                            grupo.EstadoGrupo = estadoSeleccionado;
                            grupo.EstadoGrupoId = estadoSeleccionado?.Id;

                            grupo.Hijos.Clear(); // Limpiar las acciones que tenía asignadas antes 
                            grupo.Acciones.Clear();

                            foreach (DataGridViewRow row in dgvAcciones.Rows)
                            {
                                if (row.Cells["Asignada"].Value != null && (bool)row.Cells["Asignada"].Value)
                                {
                                    if (row.DataBoundItem is Accion accion)
                                    {
                                        grupo.AgregarHijo(accion);
                                    }
                                }
                            }

                            var mensaje = ControladoraSeguridad.Instancia.ModificarGrupo(grupo);
                            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    else
                    {
                        grupo.Codigo = txtCodigoGrupo.Text;
                        grupo.NombreGrupo = txtNombreGrupo.Text;
                        grupo.Nombre = txtNombreGrupo.Text; // Para compatibilidad
                        grupo.Descripcion = txtDescripcionGrupo.Text;

                        var estadoSeleccionado = ControladoraSeguridad.Instancia.RecuperarEstadosGrupo()
                            .FirstOrDefault(estado => estado.Nombre == cmbCargarEstadoGrupo.SelectedItem.ToString());
                        grupo.EstadoGrupo = estadoSeleccionado;
                        grupo.EstadoGrupoId = estadoSeleccionado?.Id;

                        foreach (DataGridViewRow row in dgvAcciones.Rows)
                        {
                            if (row.Cells["Asignada"].Value != null && (bool)row.Cells["Asignada"].Value)
                            {
                                if (row.DataBoundItem is Accion accion)
                                {
                                    grupo.AgregarHijo(accion);
                                }
                            }
                        }

                        var mensaje = ControladoraSeguridad.Instancia.AgregarGrupo(grupo);
                        MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #region Validaciones
        private bool ValidarCampos()
        {
            // Validaciones existentes
            if (string.IsNullOrEmpty(txtCodigoGrupo.Text) || txtCodigoGrupo.Text == "Código del grupo...")
            {
                MessageBox.Show("Debe ingresar un código", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                txtCodigoGrupo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNombreGrupo.Text) || txtNombreGrupo.Text == "Nombre del grupo...")
            {
                MessageBox.Show("Debe ingresar un nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                txtNombreGrupo.Focus();
                return false;
            }
            if (cmbCargarEstadoGrupo.SelectedItem == null || cmbCargarEstadoGrupo.SelectedItem.ToString() == "Seleccione un estado...")
            {
                MessageBox.Show("El estado del grupo no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 0)
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                cmbCargarEstadoGrupo.Focus();
                return false;
            }

            // Validar que al menos una acción esté seleccionada
            bool tieneAccionSeleccionada = false;
            foreach (DataGridViewRow row in dgvAcciones.Rows)
            {
                if (row.Cells["Asignada"].Value != null && (bool)row.Cells["Asignada"].Value)
                {
                    tieneAccionSeleccionada = true;
                    break;
                }
            }

            if (!tieneAccionSeleccionada)
            {
                MessageBox.Show("Debe seleccionar al menos una acción para el grupo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl1.TabPages.Count > 1)
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                return false;
            }

            return true;
        }
        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cancelar la carga de datos?", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }


    }
}
