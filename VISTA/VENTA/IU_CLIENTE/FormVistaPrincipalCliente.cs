using System;
using System.Windows.Forms;
using CONTROLADORA;
using ENTIDADES;
using System.Drawing;
using System.Linq;
using System.Text;
using VISTA.VENTA;
using ENTIDADES.SEGURIDAD;
using DevExpress.XtraSpellChecker;
using VISTA.VENTA.IU_CLIENTE;

namespace VISTA
{
    public partial class FormVistaPrincipalCliente : Form
    {
        private ControladoraProducto controladoraProducto;
        private Cliente clienteActual;
        private ControladoraCliente controladoraCliente;
        public FormVistaPrincipalCliente()
        {
            InitializeComponent();
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            // Buscar el cliente asociado al usuario actual
            controladoraProducto = ControladoraProducto.ObtenerInstancia();
            controladoraCliente = ControladoraCliente.ObtenerInstancia();
            clienteActual = ObtenerClienteDelUsuarioActual();
            ConfigurarControles();

            // Cargar FormInicioCliente por defecto para que sea mas detallado
            FormInicioCliente frm = new FormInicioCliente();
            AbrirFormEnPanel(frm);
        }

        private void ConfigurarControles()
        {
            // Configurar título de bienvenida
            lblBienvenido.Text = $"Bienvenido, {clienteActual.Nombre}";

        }

        private Cliente ObtenerClienteDelUsuarioActual()
        {
            // Buscar el cliente con el mismo ID de usuario
            var clientes = controladoraCliente.ObtenerClientes();
            var clienteAsociado = clientes.FirstOrDefault(c => c.UsuarioId == SesionActual.Usuario.Id);

            if (clienteAsociado == null)
            {
                throw new InvalidOperationException("No se encontró un cliente asociado al usuario actual.");
            }

            return clienteAsociado;
        }




        //Revisar

        private void AgregarProductoACarrito(Producto producto)
        {
            try
            {
                // Agregar al carrito
                CarritoTemporal.AgregarProducto(producto);

                // Actualizar contador
                ActualizarContadorCarrito();

                string mensaje = $"Producto {producto.Nombre} agregado al carrito\n" +
                                $"Precio: ${producto.Precio:N2}";

                MessageBox.Show(mensaje, "Carrito de Compras",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar al carrito: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //REVISAR
        private void ActualizarContadorCarrito()
        {
            int cantidadItems = CarritoTemporal.ObtenerCantidadTotal();
            lblCantidadCarrito.Text = cantidadItems.ToString();

            // Mostrar/ocultar el contador dependiendo si hay items
            lblCantidadCarrito.Visible = cantidadItems > 0;
        }



        // Métodos de navegación

        private void AbrirFormEnPanel(Form formHijo)
        {
            // Limpiar el panel
            panelContenedorInicio.Controls.Clear();

            // Configurar el form hijo
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;

            // Agregar el form al panel
            panelContenedorInicio.Controls.Add(formHijo);
            panelContenedorInicio.Tag = formHijo;

            // Mostrar el form
            formHijo.Show();
        }



        private void btnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                    "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    SesionActual.CerrarSesion();
                    Login loginForm = new Login();
                    this.Hide(); // Oculta el formulario actual
                    DialogResult loginResult = loginForm.ShowDialog();

                    if (loginResult != DialogResult.OK)
                    {
                        this.Close(); // Cierra el formulario actual
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



       

  

        private void panelContenedorInicio_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }


        private void btnInicio_Click_1(object sender, EventArgs e)
        {
            ConfigurarControles();
            MessageBox.Show("Bienvenido a la página de inicio", "Inicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FormInicioCliente formInicio = new FormInicioCliente();
            AbrirFormEnPanel(formInicio);
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                FormPerfilCliente formPerfil = new FormPerfilCliente();
                AbrirFormEnPanel(formPerfil);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMisCompras_Click(object sender, EventArgs e)
        {
            try
            {
                FormComprasCliente formCompras = new FormComprasCliente();
                AbrirFormEnPanel(formCompras);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir mis compras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFavoritos_Click(object sender, EventArgs e)
        {
            try
            {
                FormFavoritosCliente formFavoritos = new FormFavoritosCliente();
                AbrirFormEnPanel(formFavoritos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir favoritos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}