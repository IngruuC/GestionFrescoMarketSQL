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
            Menu formMenu = new Menu();
            this.Hide();
            formMenu.ShowDialog();
            this.Close();
        }
    }
}
