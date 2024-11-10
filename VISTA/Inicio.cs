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

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            FormRegistroCliente formCliente = new FormRegistroCliente();
            formCliente.ShowDialog();
        }

        private void btnRegistroProducto_Click(object sender, EventArgs e)
        {
            FormRegistroProducto formProducto = new FormRegistroProducto();
            formProducto.ShowDialog();
        }

        private void btnRealizarVenta_Click(object sender, EventArgs e)
        {
            FormVenta formVenta = new FormVenta();
            formVenta.ShowDialog();
        }

        private void btnVentasTotales_Click(object sender, EventArgs e)
        {
            FormVentasTotales formVentasTotales = new FormVentasTotales();
            formVentasTotales.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
