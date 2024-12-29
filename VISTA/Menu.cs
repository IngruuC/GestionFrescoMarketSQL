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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnRegistrarCliente_Click(object sender, EventArgs e)
        {
            FormRegistroCliente formCliente = new FormRegistroCliente();
            formCliente.ShowDialog();
        }

        private void btnRegistrarProducto_Click(object sender, EventArgs e)
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

        private void btnRegistrarProveedor_Click(object sender, EventArgs e)
        {
            FormRegistroProveedor formRegistroProveedor = new FormRegistroProveedor();
            formRegistroProveedor.ShowDialog();
        }
    }
}
