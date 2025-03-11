using System;
using System.Windows.Forms;
using CONTROLADORA;
using ENTIDADES;
using System.Drawing;
using System.Linq;
using System.Text;
using VISTA.VENTA;
using ENTIDADES.SEGURIDAD;

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
        }

        private void ConfigurarControles()
        {
            // Configurar título de bienvenida
            lblBienvenido.Text = $"Bienvenido, {clienteActual.Nombre}";

            // Configurar ofertas especiales y recomendados
            CargarOfertasEspeciales();
            CargarRecomendados();
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


        private void ConfigurarPanelOferta(Panel panel, Producto producto)
        {
            // Limpiar controles anteriores
            panel.Controls.Clear();

            // Etiqueta de nombre
            Label lblNombre = new Label
            {
                Text = producto.Nombre,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            panel.Controls.Add(lblNombre);

            // Etiqueta de precio
            Label lblPrecio = new Label
            {
                Text = $"${producto.Precio:N2}",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 9)
            };
            panel.Controls.Add(lblPrecio);

            // Botón de agregar
            Button btnAgregar = new Button
            {
                Text = "Agregar",
                Dock = DockStyle.Bottom,
                BackColor = System.Drawing.Color.DarkGoldenrod,
                ForeColor = System.Drawing.Color.White
            };
            btnAgregar.Click += (sender, e) => AgregarProductoACarrito(producto);
            panel.Controls.Add(btnAgregar);
        }

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


        private void ActualizarContadorCarrito()
        {
            int cantidadItems = CarritoTemporal.ObtenerCantidadTotal();
            lblCantidadCarrito.Text = cantidadItems.ToString();

            // Mostrar/ocultar el contador dependiendo si hay items
            lblCantidadCarrito.Visible = cantidadItems > 0;
        }
        // Métodos de navegación

        private void btnPerfil_Click_1(object sender, EventArgs e)
        {
            try
            {
                FormPerfilCliente formPerfil = new FormPerfilCliente();
                formPerfil.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el perfil: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            // Cuando hacen clic en Inicio, simplemente actualizamos la vista principal
            ConfigurarControles();
            MessageBox.Show("Bienvenido a la página de inicio", "Inicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMisCompras_Click_1(object sender, EventArgs e)
        {
            try
            {
                FormComprasCliente formCompras = new FormComprasCliente();
                formCompras.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir mis compras: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFavoritos_Click_1(object sender, EventArgs e)
        {
            try
            {
                FormFavoritosCliente formFavoritos = new FormFavoritosCliente();
                formFavoritos.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir favoritos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void CargarOfertasEspeciales()
        {
            try
            {
                var productos = controladoraProducto.ObtenerProductos();

                // Filtramos solo productos con stock y ordenamos por precio (ofertas)
                var productosOferta = productos
                    .Where(p => p.Stock > 0)
                    .OrderBy(p => p.Precio)  // Ordenamos por precio ascendente para mostrar las mejores ofertas
                    .Take(3)                 // Tomamos los 3 primeros (los más baratos)
                    .ToList();

                if (productosOferta.Count >= 3)
                {
                    // Configurar los paneles de ofertas
                    ConfigurarPanelOferta(panelOferta1, productosOferta[0]);
                    ConfigurarPanelOferta(panelOferta2, productosOferta[1]);
                    ConfigurarPanelOferta(panelOferta3, productosOferta[2]);
                }
                else
                {
                    // Si no hay suficientes productos, mostrar mensaje en el primer panel
                    panelOferta1.Controls.Clear();
                    Label lblNoOfertas = new Label
                    {
                        Text = "No hay ofertas disponibles en este momento",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic)
                    };
                    panelOferta1.Controls.Add(lblNoOfertas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ofertas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRecomendados()
        {
            try
            {
                var productos = controladoraProducto.ObtenerProductos();

                // Para las recomendaciones, podemos mostrar productos con más stock
                var productosRecomendados = productos
                    .Where(p => p.Stock > 5)  // Productos con buen stock
                    .OrderByDescending(p => p.Id)  // Ordenamos por ID descendente (asumiendo que los más nuevos tienen ID mayor)
                    .Take(4)  // Tomamos 4 productos
                    .ToList();

                // Limpiar panel de recomendados
                panelRecomendados.Controls.Clear();

                if (productosRecomendados.Count > 0)
                {
                    // Crear una distribución de 2x2 para los productos recomendados
                    TableLayoutPanel table = new TableLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        RowCount = 2,
                        ColumnCount = 2,
                        CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
                    };

                    int index = 0;
                    for (int row = 0; row < 2; row++)
                    {
                        for (int col = 0; col < 2; col++)
                        {
                            if (index < productosRecomendados.Count)
                            {
                                // Crear panel para cada producto
                                Panel panelProducto = new Panel { Dock = DockStyle.Fill };

                                // Configurar el panel con el producto
                                Label lblNombre = new Label
                                {
                                    Text = productosRecomendados[index].Nombre,
                                    Dock = DockStyle.Top,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Font = new Font("Arial", 9, FontStyle.Bold),
                                    Height = 40
                                };

                                Label lblPrecio = new Label
                                {
                                    Text = $"${productosRecomendados[index].Precio:N2}",
                                    Dock = DockStyle.Top,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Font = new Font("Arial", 9),
                                    Height = 30
                                };

                                Button btnVer = new Button
                                {
                                    Text = "Ver Detalles",
                                    Dock = DockStyle.Bottom,
                                    BackColor = Color.FromArgb(50, 205, 50),
                                    ForeColor = Color.White,
                                    Height = 30
                                };

                                int productoId = productosRecomendados[index].Id;
                                btnVer.Click += (sender, e) => VerDetallesProducto(productoId);

                                panelProducto.Controls.Add(btnVer);
                                panelProducto.Controls.Add(lblPrecio);
                                panelProducto.Controls.Add(lblNombre);

                                // Añadir el panel a la tabla
                                table.Controls.Add(panelProducto, col, row);

                                index++;
                            }
                        }
                    }

                    panelRecomendados.Controls.Add(table);
                }
                else
                {
                    Label lblNoRecomendados = new Label
                    {
                        Text = "No hay recomendaciones disponibles",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", 10, FontStyle.Italic)
                    };
                    panelRecomendados.Controls.Add(lblNoRecomendados);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar recomendaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerDetallesProducto(int productoId)
        {
            try
            {
                var producto = controladoraProducto.ObtenerProductoPorId(productoId);
                if (producto != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Nombre: {producto.Nombre}");
                    sb.AppendLine($"Precio: ${producto.Precio:N2}");
                    sb.AppendLine($"Stock: {producto.Stock} unidades");

                    if (producto.EsPerecedero && producto.FechaVencimiento.HasValue)
                        sb.AppendLine($"Vencimiento: {producto.FechaVencimiento.Value:dd/MM/yyyy}");

                    MessageBox.Show(sb.ToString(), "Detalles del Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener detalles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarCarrito()
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
                formCarrito.ShowDialog();

                // Después de cerrar el formulario, actualizar el contador
                ActualizarContadorCarrito();
            }
        }
    }
}