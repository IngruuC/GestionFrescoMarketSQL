using System;
using System.Drawing;
using System.Windows.Forms;

namespace VISTA
{
    public partial class Inicio : Form
    {
        private Button currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        public Inicio()
        {
            InitializeComponent();
            CustomizeDesign();
        }

        private void CustomizeDesign()
        {
            panelMenu.BackColor = Color.FromArgb(31, 30, 68);
            panelLogo.BackColor = Color.FromArgb(31, 30, 68);
            panelTitleBar.BackColor = Color.FromArgb(26, 25, 62);
            panelDesktop.BackColor = Color.FromArgb(34, 33, 74);

            // Inicializar el borde izquierdo del botón
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            // Maximizar form
            this.WindowState = FormWindowState.Maximized;

            // Deshabilitar el botón maximizar
            this.MaximizeBox = false;
        }

        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                // Botón
                currentBtn = (Button)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.Padding = new Padding(10, 0, 0, 0);

                // Borde izquierdo
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // Título del formulario
                lblTitleChildForm.Text = currentBtn.Text;
                lblTitleChildForm.ForeColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.Padding = new Padding(10, 0, 0, 0);
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.FromArgb(172, 126, 241));
            OpenChildForm(new FormRegistroCliente());
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.FromArgb(249, 118, 176));
            OpenChildForm(new FormRegistroProducto());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.FromArgb(253, 138, 114));
            OpenChildForm(new FormVenta());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Color.FromArgb(95, 77, 221));
            OpenChildForm(new FormVentasTotales());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            lblTitleChildForm.Text = "Home";
            lblTitleChildForm.ForeColor = Color.Gainsboro;
        }

        // Drag Form
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}