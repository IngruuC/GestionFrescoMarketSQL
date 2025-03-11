using System;
using System.Drawing;
using System.Windows.Forms;

namespace VISTA
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            this.Hide();
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                // Si el login fue exitoso, el formulario Inicio se cierra
                this.Close();
            }
            else
            {
                // Si canceló o cerró el login, volvemos a mostrar el Inicio
                this.Show();
            }
        }
    }
    
}
