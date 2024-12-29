using CONTROLADORA;
using System;
using System.Drawing;
using System.Windows.Forms;
using VISTA.COMPRA;
using System.Linq; // Para poder usar Sum()

namespace VISTA
{
    public partial class FormPrincipal : Form
    {
        private ControladoraVenta controladoraVenta;
        private ControladoraCompra controladoraCompra;

        public FormPrincipal()
        {
            InitializeComponent();
            controladoraVenta = ControladoraVenta.ObtenerInstancia();
            controladoraCompra = ControladoraCompra.ObtenerInstancia();
            ConfigurarControles();
            PersonalizarDiseno();
        }

        private void ConfigurarControles()
        {
            // Configurar el formulario
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Configurar botones de Ventas
            ConfigurarBotonTab(btnRegistrarCliente, "Registrar\nCliente", 30, 30, tabVentas);
            ConfigurarBotonTab(btnRegistrarProducto, "Registrar\nProducto", 250, 30, tabVentas);
            ConfigurarBotonTab(btnNuevaVenta, "Nueva\nVenta", 470, 30, tabVentas);
            ConfigurarBotonTab(btnVentasTotales, "Ventas\nTotales", 140, 150, tabVentas);
            ConfigurarBotonTab(btnReportesVentas, "Reportes de\nVentas", 360, 150, tabVentas);

            // Configurar botones de Compras
            ConfigurarBotonTab(btnRegistrarProveedor, "Registrar\nProveedor", 30, 30, tabCompras);
            ConfigurarBotonTab(btnNuevaCompra, "Nueva\nCompra", 250, 30, tabCompras);
            ConfigurarBotonTab(btnComprasTotales, "Compras\nTotales", 470, 30, tabCompras);
            ConfigurarBotonTab(btnReportesCompras, "Reportes de\nCompras", 250, 150, tabCompras);

            // Eventos de los botones de Ventas
            btnRegistrarCliente.Click += (s, e) => AbrirFormulario(new FormRegistroCliente());
            btnRegistrarProducto.Click += (s, e) => AbrirFormulario(new FormRegistroProducto());
            btnNuevaVenta.Click += (s, e) => AbrirFormulario(new FormVenta());
            btnVentasTotales.Click += (s, e) => AbrirFormulario(new FormVentasTotales());
            btnReportesVentas.Click += (s, e) => AbrirFormulario(new FormReportes());

            // Eventos de los botones de Compras
            btnRegistrarProveedor.Click += (s, e) => AbrirFormulario(new FormRegistroProveedor());
            btnNuevaCompra.Click += (s, e) => AbrirFormulario(new FormCompra());
            btnComprasTotales.Click += (s, e) => AbrirFormulario(new FormComprasTotales());
            btnReportesCompras.Click += (s, e) => AbrirFormulario(new FormReportesCompras());

            // Configurar estado inicial
            ActualizarResumen();
        }
        private void ConfigurarBotonTab(Button btn, string texto, int x, int y, TabPage tab)
        {
            btn.Text = texto;
            btn.Location = new Point(x, y);
            btn.Size = new Size(200, 100);
            tab.Controls.Add(btn);
            EstilizarBoton(btn);
        }

        private void PersonalizarDiseno()
        {
            // Personalizar colores
            this.BackColor = Color.FromArgb(240, 240, 240);
            panelSuperior.BackColor = Color.DarkGoldenrod;
            tabControl.BackColor = Color.White;

            // Estilo de las pestañas
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += TabControl_DrawItem;

            // Estilo de los botones
            EstilizarBotones();
        }

        private void EstilizarBotones()
        {
            // Aplicar estilo a todos los botones
            foreach (Control control in this.Controls)
            {
                if (control is TabControl tabControl)
                {
                    foreach (TabPage page in tabControl.TabPages)
                    {
                        foreach (Control c in page.Controls)
                        {
                            if (c is Button btn)
                            {
                                EstilizarBoton(btn);
                            }
                        }
                    }
                }
            }
        }

        private void EstilizarBoton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.DarkGoldenrod;
            btn.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btn.Height = 100;
            btn.Width = 200;
            btn.TextAlign = ContentAlignment.BottomCenter;
            btn.ImageAlign = ContentAlignment.TopCenter;
            btn.Padding = new Padding(0, 10, 0, 10);
            btn.Margin = new Padding(10);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.DarkGoldenrod;
                btn.ForeColor = Color.White;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.DarkGoldenrod;
            };
        }

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = this.tabControl.TabPages[e.Index];
            var tabRect = this.tabControl.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);

            

            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                tabRect, tabPage.ForeColor, TextFormatFlags.Left);
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.ShowDialog();
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            try
            {
                // Actualizar resumen de ventas
                var ventasHoy = controladoraVenta.ObtenerVentasPorFecha(DateTime.Today, DateTime.Today.AddDays(1));
                lblVentasHoy.Text = $"Ventas de hoy: ${ventasHoy.Sum(v => v.Total):N2}";

                // Actualizar resumen de compras
                var comprasHoy = controladoraCompra.ObtenerComprasPorFecha(DateTime.Today, DateTime.Today.AddDays(1));
                lblComprasHoy.Text = $"Compras de hoy: ${comprasHoy.Sum(c => c.Total):N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el resumen: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}