using CONTROLADORA;
using ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VISTA.VENTA
{
    public partial class FormCarritoCompra : Form
    {
        private ControladoraVenta controladoraVenta;
        private Cliente clienteActual;

        public FormCarritoCompra(Cliente cliente)
        {
            ConfigurarComponentes();
            controladoraVenta = ControladoraVenta.ObtenerInstancia();
            clienteActual = cliente;
            CargarDatosCarrito();
        }

        private void ConfigurarComponentes()
        {
            this.Text = "Carrito de Compras";
            this.Size = new System.Drawing.Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // DataGridView
            dgvCarrito = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 350,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                ReadOnly = false, // Permitimos editar la cantidad
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Configurar columnas
            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Producto",
                DataPropertyName = "Producto.Nombre",
                Width = 200,
                ReadOnly = true
            });

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Precio",
                DataPropertyName = "Producto.Precio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                ReadOnly = true
            });

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 80,
                ReadOnly = false // Permitimos editar cantidad
            });

            dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Subtotal",
                DataPropertyName = "Subtotal",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                ReadOnly = true
            });

            // Botón para eliminar item
            var btnEliminarColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Eliminar",
                Text = "X",
                UseColumnTextForButtonValue = true,
                Width = 70
            };
            dgvCarrito.Columns.Add(btnEliminarColumn);

            // Resumen de compra panel
            panelResumen = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100
            };

            // Total label
            lblTotal = new Label
            {
                Text = "Total: $0.00",
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };

            // Botón Vaciar
            btnVaciar = new Button
            {
                Text = "Vaciar Carrito",
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(120, 30)
            };
            btnVaciar.Click += btnVaciar_Click;

            // Botón Seguir Comprando
            btnSeguirComprando = new Button
            {
                Text = "Seguir Comprando",
                Location = new System.Drawing.Point(200, 60),
                Size = new System.Drawing.Size(120, 30)
            };
            btnSeguirComprando.Click += btnSeguirComprando_Click;

            // Botón Finalizar Compra
            btnFinalizarCompra = new Button
            {
                Text = "Finalizar Compra",
                Location = new System.Drawing.Point(500, 20),
                Size = new System.Drawing.Size(150, 50),
                BackColor = System.Drawing.Color.Green,
                ForeColor = System.Drawing.Color.White
            };
            btnFinalizarCompra.Click += btnFinalizarCompra_Click;

            // Eventos del DataGridView
            dgvCarrito.CellValueChanged += dgvCarrito_CellValueChanged;
            dgvCarrito.CellContentClick += dgvCarrito_CellContentClick;

            // Agregar controles al formulario
            panelResumen.Controls.Add(lblTotal);
            panelResumen.Controls.Add(btnVaciar);
            panelResumen.Controls.Add(btnSeguirComprando);
            panelResumen.Controls.Add(btnFinalizarCompra);

            this.Controls.Add(dgvCarrito);
            this.Controls.Add(panelResumen);
        }

        private void CargarDatosCarrito()
        {
            dgvCarrito.DataSource = null;
            dgvCarrito.DataSource = CarritoTemporal.ObtenerItems();
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            decimal total = CarritoTemporal.ObtenerTotal();
            lblTotal.Text = $"Total: ${total:N2}";

            // Habilitar o deshabilitar el botón de finalizar compra
            btnFinalizarCompra.Enabled = total > 0;
        }

        private void dgvCarrito_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2) // Columna de Cantidad
            {
                try
                {
                    var item = (ItemCarritoTemp)dgvCarrito.Rows[e.RowIndex].DataBoundItem;
                    int nuevaCantidad;

                    if (int.TryParse(dgvCarrito.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString(), out nuevaCantidad))
                    {
                        // Validar cantidad
                        if (nuevaCantidad <= 0)
                        {
                            MessageBox.Show("La cantidad debe ser mayor a cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvCarrito.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = item.Cantidad; // Restaurar valor anterior
                            return;
                        }

                        if (nuevaCantidad > item.Producto.Stock)
                        {
                            MessageBox.Show($"Stock insuficiente. Solo hay {item.Producto.Stock} unidades disponibles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvCarrito.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = item.Cantidad; // Restaurar valor anterior
                            return;
                        }

                        // Actualizar cantidad
                        CarritoTemporal.ModificarCantidad(item.Producto.Id, nuevaCantidad);
                        CargarDatosCarrito(); // Recargar para actualizar subtotales
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cambiar cantidad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CargarDatosCarrito(); // Recargar en caso de error
                }
            }
        }

        private void dgvCarrito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si se hizo clic en el botón Eliminar
            if (e.RowIndex >= 0 && e.ColumnIndex == 4) // Columna del botón Eliminar
            {
                var item = (ItemCarritoTemp)dgvCarrito.Rows[e.RowIndex].DataBoundItem;
                CarritoTemporal.EliminarProducto(item.Producto.Id);
                CargarDatosCarrito();
            }
        }

        private void btnVaciar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea vaciar el carrito?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CarritoTemporal.VaciarCarrito();
                CargarDatosCarrito();
            }
        }

        private void btnSeguirComprando_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostrar opciones de pago
                string formaPago = MostrarDialogoFormaPago();

                if (string.IsNullOrEmpty(formaPago))
                    return;

                // Crear venta
                var venta = new Venta
                {
                    ClienteId = clienteActual.Id,
                    FechaVenta = DateTime.Now,
                    FormaPago = formaPago
                };

                // Agregar detalles
                foreach (var item in CarritoTemporal.ObtenerItems())
                {
                    venta.Detalles.Add(new DetalleVenta
                    {
                        ProductoId = item.Producto.Id,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.Producto.Precio,
                        ProductoNombre = item.Producto.Nombre,
                        Subtotal = item.Subtotal
                    });
                }

                // Realizar la venta usando el controlador existente
                controladoraVenta.RealizarVenta(venta);

                MessageBox.Show($"Compra realizada con éxito. Total: ${venta.Total:N2}",
                    "Compra Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Vaciar carrito y cerrar
                CarritoTemporal.VaciarCarrito();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al finalizar la compra: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string MostrarDialogoFormaPago()
        {
            using (var form = new Form())
            {
                form.Text = "Seleccionar Forma de Pago";
                form.Size = new System.Drawing.Size(300, 180);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                Label lblTitulo = new Label
                {
                    Text = "Seleccione forma de pago:",
                    Location = new System.Drawing.Point(20, 15),
                    AutoSize = true,
                    Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
                };

                ComboBox cmbFormaPago = new ComboBox
                {
                    Left = 50,
                    Top = 50,
                    Width = 200,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                cmbFormaPago.Items.AddRange(new string[] { "Efectivo", "Tarjeta de Crédito", "Tarjeta de Débito" });
                cmbFormaPago.SelectedIndex = 0;

                Button btnAceptar = new Button
                {
                    Text = "Aceptar",
                    DialogResult = DialogResult.OK,
                    Left = 70,
                    Width = 100,
                    Top = 100
                };

                Button btnCancelar = new Button
                {
                    Text = "Cancelar",
                    DialogResult = DialogResult.Cancel,
                    Left = 180,
                    Width = 100,
                    Top = 100
                };

                form.Controls.AddRange(new Control[] { lblTitulo, cmbFormaPago, btnAceptar, btnCancelar });
                form.AcceptButton = btnAceptar;
                form.CancelButton = btnCancelar;

                return form.ShowDialog() == DialogResult.OK ? cmbFormaPago.SelectedItem.ToString() : string.Empty;
            }
        }

        // Campos
        private DataGridView dgvCarrito;
        private Panel panelResumen;
        private Label lblTotal;
        private Button btnVaciar;
        private Button btnSeguirComprando;
        private Button btnFinalizarCompra;
    }
}
