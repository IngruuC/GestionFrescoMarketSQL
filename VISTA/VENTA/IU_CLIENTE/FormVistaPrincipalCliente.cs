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

            this.FormClosing += FormVistaPrincipalCliente_FormClosing;
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


        //
        private void ConfigurarCarrito()
        {
            // Configurar el botón del carrito si no existe
            if (btnCarrito == null)
            {
                // Este código es para si necesitas crear el botón programáticamente
                // Si ya tienes el botón en el diseñador, puedes omitir esta parte
                btnCarrito = new Button();
                btnCarrito.Text = "🛒";
                btnCarrito.Size = new Size(50, 40);
                btnCarrito.Location = new Point(this.Width - 100, 20);
                btnCarrito.Font = new Font("Arial", 16);
                this.Controls.Add(btnCarrito);
            }

            // Configurar el label del contador si no existe
            if (lblCantidadCarrito == null)
            {
                lblCantidadCarrito = new Label();
                lblCantidadCarrito.Size = new Size(20, 20);
                lblCantidadCarrito.Location = new Point(btnCarrito.Location.X + 30, btnCarrito.Location.Y - 5);
                lblCantidadCarrito.BackColor = Color.Red;
                lblCantidadCarrito.ForeColor = Color.White;
                lblCantidadCarrito.TextAlign = ContentAlignment.MiddleCenter;
                lblCantidadCarrito.Font = new Font("Arial", 8, FontStyle.Bold);
                lblCantidadCarrito.Visible = false;
                this.Controls.Add(lblCantidadCarrito);
                lblCantidadCarrito.BringToFront();
            }

            // Configurar timer para actualizar el carrito periodicamente
            timerActualizarCarrito = new Timer();
            timerActualizarCarrito.Interval = 1000; // Actualizar cada segundo
            timerActualizarCarrito.Tick += TimerActualizarCarrito_Tick;
            timerActualizarCarrito.Start();

            // Actualizar contador inicial
            ActualizarContadorCarrito();
        }
        private void TimerActualizarCarrito_Tick(object sender, EventArgs e)
        {
            ActualizarContadorCarrito();
        }
        private void MostrarCarrito()
        {
            try
            {
                var items = CarritoTemporal.ObtenerItems();

                if (items.Count == 0)
                {
                    MessageBox.Show("El carrito está vacío", "Carrito de Compras",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Mostrar el formulario de carrito
                using (var formCarrito = new FormCarritoCompra(clienteActual))
                {
                    var resultado = formCarrito.ShowDialog();

                    // Después de cerrar el formulario, actualizar el contador
                    ActualizarContadorCarrito();

                    // Si la compra fue exitosa, refrescar la vista actual
                    if (resultado == DialogResult.OK)
                    {
                        MessageBox.Show("¡Compra realizada con éxito!", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refrescar el formulario hijo actual si es necesario
                        RefrescarFormularioActual();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al mostrar el carrito: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefrescarFormularioActual()
        {
            // Si el formulario actual es FormInicioCliente, refrescarlo
            if (panelContenedorInicio.Tag is FormInicioCliente formInicio)
            {
                // Recargar las ofertas y recomendaciones
                formInicio.Refresh();
            }
        }

        // Método para manejar cuando se agrega un producto desde un formulario hijo
        private void FormHijo_ProductoAgregadoAlCarrito(object sender, EventArgs e)
        {
            ActualizarContadorCarrito();
        }

        //

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
            try
            {
                int cantidadItems = CarritoTemporal.ObtenerCantidadTotal();

                if (cantidadItems > 0)
                {
                    lblCantidadCarrito.Text = cantidadItems.ToString();
                    lblCantidadCarrito.Visible = true;
                    btnCarrito.BackColor = Color.Orange; // Cambiar color cuando hay items
                }
                else
                {
                    lblCantidadCarrito.Visible = false;
                    btnCarrito.BackColor = SystemColors.Control; // Color normal
                }
            }
            catch (Exception ex)
            {
                // Manejar errores silenciosamente para no interrumpir la UI
                System.Diagnostics.Debug.WriteLine($"Error actualizando carrito: {ex.Message}");
            }
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

            // Si es FormInicioCliente, suscribirse al evento
            if (formHijo is FormInicioCliente inicioCliente)
            {
                inicioCliente.ProductoAgregadoAlCarrito += FormHijo_ProductoAgregadoAlCarrito;
            }

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
                    // AGREGAR ESTO: Registrar cierre de sesión ANTES de limpiar
                    if (SesionActual.Usuario != null)
                    {
                        Console.WriteLine($"[CLIENTE] Cerrando sesión del usuario: {SesionActual.Usuario.NombreUsuario} (ID: {SesionActual.Usuario.Id})");

                        var controladoraAuditoria = new ControladoraAuditoria();
                        controladoraAuditoria.RegistrarCierreSesion(SesionActual.Usuario.Id);
                    }

                    // Tu código existente
                    // Limpiar carrito al cerrar sesión
                    CarritoTemporal.VaciarCarrito();

                    // Detener timer
                    if (timerActualizarCarrito != null)
                    {
                        timerActualizarCarrito.Stop();
                        timerActualizarCarrito.Dispose();
                    }

                    SesionActual.CerrarSesion();
                    Login loginForm = new Login();
                    this.Hide();
                    DialogResult loginResult = loginForm.ShowDialog();
                    if (loginResult != DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // PARA CUANDO SE CIERRE CON X
        private void FormVistaPrincipalCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (SesionActual.Usuario != null)
                {
                    Console.WriteLine($"[CLIENTE] Cerrando ventana - registrando cierre de sesión para: {SesionActual.Usuario.NombreUsuario}");

                    var controladoraAuditoria = new ControladoraAuditoria();
                    controladoraAuditoria.RegistrarCierreSesion(SesionActual.Usuario.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar cierre: {ex.Message}");
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



        private void btnCarrito_Click_1(object sender, EventArgs e)
        {
            MostrarCarrito();
        }
    }
}