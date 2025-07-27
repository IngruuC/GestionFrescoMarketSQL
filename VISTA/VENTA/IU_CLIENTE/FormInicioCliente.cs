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

namespace VISTA.VENTA.IU_CLIENTE
{
    public partial class FormInicioCliente : Form
    {
        // Evento para notificar cuando se agrega un producto al carrito
        public event EventHandler ProductoAgregadoAlCarrito;

        private ControladoraProducto controladoraProducto;
        private Cliente clienteActual;
        private ControladoraCliente controladoraCliente;

        public FormInicioCliente()
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
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                Height = 40
            };
            panel.Controls.Add(lblNombre);

            // Etiqueta de precio
            Label lblPrecio = new Label
            {
                Text = $"${producto.Precio:N2}",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 9),
                Height = 30,
                ForeColor = Color.Green
            };
            panel.Controls.Add(lblPrecio);

            // Etiqueta de stock
            Label lblStock = new Label
            {
                Text = $"Stock: {producto.Stock}",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 8),
                Height = 25,
                ForeColor = producto.Stock > 5 ? Color.Green : Color.Orange
            };
            panel.Controls.Add(lblStock);

            // Panel para botones
            Panel panelBotones = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60
            };

            // Botón de agregar al carrito
            Button btnAgregar = new Button
            {
                Text = "🛒 Agregar",
                Location = new Point(5, 5),
                Size = new Size(panel.Width - 10, 25),
                BackColor = System.Drawing.Color.Orange,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAgregar.Click += (sender, e) => AgregarProductoACarrito(producto);

            // Botón de ver detalles
            Button btnDetalles = new Button
            {
                Text = "Ver Detalles",
                Location = new Point(5, 30),
                Size = new Size(panel.Width - 10, 25),
                BackColor = System.Drawing.Color.DarkSlateGray,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDetalles.Click += (sender, e) => VerDetallesProducto(producto.Id);

            panelBotones.Controls.Add(btnAgregar);
            panelBotones.Controls.Add(btnDetalles);
            panel.Controls.Add(panelBotones);
        }

        private void AgregarProductoACarrito(Producto producto)
        {
            try
            {
                // Verificar stock antes de agregar
                if (producto.Stock <= 0)
                {
                    MessageBox.Show("Este producto no tiene stock disponible.",
                        "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Agregar al carrito
                CarritoTemporal.AgregarProducto(producto);

                // Notificar al formulario principal
                ProductoAgregadoAlCarrito?.Invoke(this, EventArgs.Empty);

                // Mostrar mensaje de confirmación
                string mensaje = $"✅ Producto agregado al carrito\n\n" +
                                $"Producto: {producto.Nombre}\n" +
                                $"Precio: ${producto.Precio:N2}\n" +
                                $"Cantidad en carrito: {CarritoTemporal.ObtenerCantidadTotal()}";

                MessageBox.Show(mensaje, "Carrito de Compras",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refrescar la vista para actualizar el stock mostrado
                CargarOfertasEspeciales();
                CargarRecomendados();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar al carrito: {ex.Message}",
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
                    .OrderBy(p => p.Precio)
                    .Take(3)
                    .ToList();

                if (productosOferta.Count >= 1)
                {
                    if (productosOferta.Count >= 1) ConfigurarPanelOferta(panelOferta1, productosOferta[0]);
                    if (productosOferta.Count >= 2) ConfigurarPanelOferta(panelOferta2, productosOferta[1]);
                    if (productosOferta.Count >= 3) ConfigurarPanelOferta(panelOferta3, productosOferta[2]);

                    // Limpiar paneles vacíos
                    if (productosOferta.Count < 3) LimpiarPanel(panelOferta3, "");
                    if (productosOferta.Count < 2) LimpiarPanel(panelOferta2, "");
                }
                else
                {
                    LimpiarPanel(panelOferta1, "No hay ofertas disponibles");
                    LimpiarPanel(panelOferta2, "");
                    LimpiarPanel(panelOferta3, "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ofertas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarPanel(Panel panel, string mensaje)
        {
            panel.Controls.Clear();
            if (!string.IsNullOrEmpty(mensaje))
            {
                Label lblMensaje = new Label
                {
                    Text = mensaje,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                panel.Controls.Add(lblMensaje);
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
                    sb.AppendLine($"📦 DETALLES DEL PRODUCTO");
                    sb.AppendLine($"═══════════════════════");
                    sb.AppendLine($"Nombre: {producto.Nombre}");
                    sb.AppendLine($"Código: {producto.CodigoBarra}");
                    sb.AppendLine($"Precio: ${producto.Precio:N2}");
                    sb.AppendLine($"Stock disponible: {producto.Stock} unidades");

                    if (producto.EsPerecedero)
                    {
                        sb.AppendLine($"⚠️ Producto perecedero");
                        if (producto.FechaVencimiento.HasValue)
                            sb.AppendLine($"Vencimiento: {producto.FechaVencimiento.Value:dd/MM/yyyy}");
                    }

                    // Mostrar estado del stock
                    if (producto.Stock > 10)
                        sb.AppendLine($"✅ Stock disponible");
                    else if (producto.Stock > 0)
                        sb.AppendLine($"⚠️ Stock limitado");
                    else
                        sb.AppendLine($"❌ Sin stock");

                    MessageBox.Show(sb.ToString(), "Detalles del Producto",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener detalles: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Método público para refrescar la vista
        public new void Refresh()
        {
            base.Refresh();
            CargarOfertasEspeciales();
            CargarRecomendados();
        }


    }




}
