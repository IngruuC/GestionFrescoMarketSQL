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
using ENTIDADES;
using ENTIDADES.SEGURIDAD;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VISTA;
using VISTA.SEGURIDAD.FORMS;


namespace VISTA.IU_ADMIN
{
    public partial class FormModalSeguridad : Form
    {

        #region Mover la ventana
        [DllImport("User32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void formModalSeguridad_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
        public FormModalSeguridad()
        {
            InitializeComponent();
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            formGestionarGrupos formGestionarGrupos = new formGestionarGrupos();
            formGestionarGrupos.ShowDialog();
        }

        private void btnGestionarUsuarios_Click(object sender, EventArgs e)
        {
              FormGestionarUsuarios formGestionarUsuarios = new FormGestionarUsuarios();
            formGestionarUsuarios.ShowDialog();

                
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAuditoriaSesion_Click(object sender, EventArgs e)
        {
            // Usar tu FormAuditoriaSesion existente
            FormAuditoriaSesion frmAuditoriaSesion = new FormAuditoriaSesion();
            frmAuditoriaSesion.ShowDialog();
        }


        private void btnAdministrarBD_Click_1(object sender, EventArgs e)
        {
            // Usar tu FormBackupRestauracionBD existente
            FormBackupRestauracionBD frmBackUpoRestBD = new FormBackupRestauracionBD();
            frmBackUpoRestBD.ShowDialog();
        }
    }
}
