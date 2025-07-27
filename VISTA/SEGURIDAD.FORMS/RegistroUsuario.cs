using CONTROLADORA;
using ENTIDADES;
using ENTIDADES.SEGURIDAD;
using Org.BouncyCastle.Crypto.Generators;
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
    public partial class RegistroUsuario : Form
    {
        private readonly ControladoraUsuario controladora;

 
        /// //////////////////

        private readonly ControladoraCliente controladoraCliente;
        private bool esClienteExistente = false;
        private Cliente clienteEncontrado = null;

        // Propiedad para comunicar credenciales al formulario de login
        public string UsuarioCreado { get; private set; }

        /// //////////////////  
        public RegistroUsuario()
        {
            InitializeComponent();
            controladora = ControladoraUsuario.ObtenerInstancia();
            controladoraCliente = ControladoraCliente.ObtenerInstancia(); //
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            txtContraseña.PasswordChar = '*';
            txtConfirmarContraseña.PasswordChar = '*';

            cmbTipoUsuario.Items.Add("Cliente");
            cmbTipoUsuario.Items.Add("Proveedor");
            cmbTipoUsuario.SelectedIndex = 0;

            // Agregar evento para detectar cuando se ingresa el documento
            txtDocumento.TextChanged += TxtDocumento_TextChanged;
            txtDocumento.Leave += TxtDocumento_Leave;
            ///////////////////// Lo de arriba

            cmbTipoUsuario.SelectedIndexChanged += (s, e) => ActualizarCamposVisibles();
            ActualizarCamposVisibles();
        }


        /// ///////////////
        private void TxtDocumento_TextChanged(object sender, EventArgs e)
        {
            // Reset del estado si el usuario está modificando el documento
            if (esClienteExistente)
            {
                esClienteExistente = false;
                clienteEncontrado = null;
                RestaurarFormularioNormal();
            }
        }

        private void TxtDocumento_Leave(object sender, EventArgs e)
        {
            // Solo verificar si es Cliente y el documento tiene 8 dígitos
            // Y si no estamos ya en modo cliente existente (para evitar bucles)
            if (cmbTipoUsuario.SelectedItem.ToString() == "Cliente" &&
                txtDocumento.Text.Length == 8 &&
                txtDocumento.Text.All(char.IsDigit) &&
                !esClienteExistente)
            {
                VerificarClienteExistente();
            }
        }
        private void VerificarClienteExistente()
        {
            try
            {
                string documento = txtDocumento.Text.Trim();

                // Buscar si existe un cliente con ese documento
                var clientes = controladoraCliente.ObtenerClientes();
                clienteEncontrado = clientes.FirstOrDefault(c => c.Documento == documento);

                if (clienteEncontrado != null)
                {
                    // Siempre mostrar el mensaje de confirmación
                    MostrarMensajeClienteEncontrado();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar cliente: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarMensajeClienteEncontrado()
        {
            // Mostrar datos encontrados y preguntar si es la misma persona
            var resultado = MessageBox.Show(
                $"Encontramos un cliente registrado con este documento:\n\n" +
                $"Nombre: {clienteEncontrado.Nombre} {clienteEncontrado.Apellido}\n" +
                $"Documento: {clienteEncontrado.Documento}\n" +
                $"Dirección: {clienteEncontrado.Direccion}\n\n" +
                $"¿Eres tú esta persona?",
                "Cliente encontrado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Es la misma persona - preguntar si es primer acceso
                PreguntarSiEsPrimerAcceso();
            }
            else
            {
                // No es la misma persona - no permitir el registro
                MessageBox.Show(
                    "Este documento ya está registrado en el sistema con otra persona.\n\n" +
                    "Si crees que esto es un error, por favor contacta con un administrador.\n" +
                    "No es posible registrar el mismo documento para diferentes personas.",
                    "Documento ya utilizado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                LimpiarYCerrar();
            }
        }

        private void MostrarMensajePrimerAcceso()
        {
            esClienteExistente = true;

            // Mostrar mensaje de bienvenida para primer acceso
            var resultado = MessageBox.Show(
                $"¡Perfecto! Tu cuenta ya está creada.\n\n" +
                $"Para completar tu registro y acceder por primera vez,\n" +
                $"solo necesitas configurar tu contraseña de acceso.\n\n" +
                $"¿Deseas continuar?",
                "¡Bienvenido por primera vez!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (resultado == DialogResult.Yes)
            {
                ConfigurarFormularioParaClienteExistente();
            }
            else
            {
                esClienteExistente = false;
                clienteEncontrado = null;
                LimpiarYCerrar();
            }
        }

        private void LimpiarYCerrar()
        {
            // Resetear estado
            esClienteExistente = false;
            clienteEncontrado = null;

            // Limpiar solo el documento para evitar perder otros datos
            txtDocumento.Clear();
            txtDocumento.Focus();

            // Si estaba en modo cliente existente, restaurar formulario
            if (btnRegistrar.Text == "Configurar Contraseña")
            {
                RestaurarFormularioNormal();
            }
        }

        private void PreguntarSiEsPrimerAcceso()
        {
            var resultado = MessageBox.Show(
                "¿Es tu primer acceso al sistema?\n\n" +
                "• Si es tu PRIMER ACCESO: Podrás configurar tu contraseña ahora\n" +
                "• Si YA TIENES USUARIO: Serás redirigido al formulario de login",
                "Confirmar tipo de acceso",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Dice que es primer acceso - verificar si ya tiene usuario asignado
                if (clienteEncontrado.UsuarioId.HasValue)
                {
                    // YA TIENE USUARIO ASIGNADO - mostrar credenciales
                    MostrarCredencialesUsuarioExistente();
                }
                else
                {
                    // NO TIENE USUARIO - primer acceso legítimo
                    MostrarMensajePrimerAcceso();
                }
            }
            else
            {
                // Dice que NO es primer acceso - debería ir al login
                MessageBox.Show(
                    "Parece que ya tienes una cuenta registrada.\n" +
                    "Por favor, utiliza el formulario de inicio de sesión para acceder al sistema.\n\n" +
                    "Si olvidaste tu contraseña, puedes usar la opción 'Olvidé mi contraseña'.",
                    "Cuenta existente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarYCerrar();
            }
        }

        private void MostrarCredencialesUsuarioExistente()
        {
            // Obtener el usuario para mostrar las credenciales
            var controladoraUsuario = ControladoraUsuario.ObtenerInstancia();
            var usuarios = controladoraUsuario.ObtenerUsuarios();
            var usuarioAsignado = usuarios.FirstOrDefault(u => u.Id == clienteEncontrado.UsuarioId.Value);

            string nombreUsuario = usuarioAsignado?.NombreUsuario ?? $"cliente_{clienteEncontrado.Documento}";

            // Verificar si es un usuario creado automáticamente por el sistema de "Asignar Usuarios"
            // (que tendría DNI como contraseña) o si es un usuario que ya configuró su propia contraseña
            bool esUsuarioAutomatico = nombreUsuario.StartsWith("cliente_") &&
                                     usuarioAsignado != null &&
                                     usuarioAsignado.Email.Contains("@frescomarket.com");

            if (esUsuarioAutomatico)
            {
                // Usuario creado automáticamente - mostrar credenciales con DNI
                MessageBox.Show(
                    $"Ya tienes una cuenta de usuario creada por un administrador.\n\n" +
                    $"📋 Tus credenciales de acceso son:\n" +
                    $"👤 Usuario: {nombreUsuario}\n" +
                    $"🔑 Contraseña: {clienteEncontrado.Documento} (tu documento)\n\n" +
                    $"💡 Recomendación: Una vez que inicies sesión, ve a 'Mi Perfil' para cambiar tu contraseña por una personalizada.\n\n" +
                    $"Serás redirigido al formulario de inicio de sesión.",
                    "Credenciales de Acceso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Comunicar las credenciales al formulario padre
                UsuarioCreado = nombreUsuario;
            }
            else
            {
                // Usuario que ya configuró su contraseña personalizada
                MessageBox.Show(
                    $"Ya tienes una cuenta de usuario registrada.\n\n" +
                    $"👤 Tu usuario es: {nombreUsuario}\n" +
                    $"🔑 Contraseña: La que configuraste previamente\n\n" +
                    $"Por favor, utiliza el formulario de inicio de sesión para acceder al sistema.\n" +
                    $"Si olvidaste tu contraseña, puedes usar la opción 'Olvidé mi contraseña'.",
                    "Usuario Existente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // NO comunicar credenciales porque no sabemos la contraseña
                UsuarioCreado = null;
            }

            // Cerrar este formulario para ir al login
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfigurarFormularioParaClienteExistente()
        {
            // Llenar automáticamente los datos del cliente
            txtNombre.Text = clienteEncontrado.Nombre;
            txtApellido.Text = clienteEncontrado.Apellido;
            txtDireccionCliente.Text = clienteEncontrado.Direccion;

            // Generar usuario automáticamente
            txtUsuario.Text = $"cliente_{clienteEncontrado.Documento}";

            // Deshabilitar campos que ya están completos
            txtDocumento.Enabled = false;
            txtNombre.Enabled = false;
            txtApellido.Enabled = false;
            txtDireccionCliente.Enabled = false;
            txtUsuario.Enabled = false;
            cmbTipoUsuario.Enabled = false;

            // Cambiar el texto del botón
            btnRegistrar.Text = "Configurar Contraseña";

            // Enfocar en el campo de contraseña
            txtContraseña.Focus();

            // Cambiar el título o agregar una etiqueta indicativa
            this.Text = "Configuración de Contraseña - Primer Acceso";

            // Opcional: cambiar color de fondo para indicar modo especial
            this.BackColor = Color.LightGreen;
        }

        private void RestaurarFormularioNormal()
        {
            // Restaurar estado normal del formulario
            txtDocumento.Enabled = true;
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            txtDireccionCliente.Enabled = true;
            txtUsuario.Enabled = true;
            cmbTipoUsuario.Enabled = true;

            btnRegistrar.Text = "Registrar";
            this.Text = "Registro de Usuario";
            this.BackColor = SystemColors.Control;

            // Limpiar campos
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtContraseña.Clear();
            txtConfirmarContraseña.Clear();
            txtEmail.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccionCliente.Clear();
            txtCuit.Clear();
            txtRazonSocial.Clear();
            txtTelefono.Clear();
            txtDireccionProveedor.Clear();
        }
        /// 

        private void ActualizarCamposVisibles()
        {
            bool esProveedor = cmbTipoUsuario.SelectedItem.ToString() == "Proveedor";

            // Campos comunes que siempre deben estar visibles
            lblUsuario.Visible = true;
            txtUsuario.Visible = true;
            lblContraseña.Visible = true;
            txtContraseña.Visible = true;
            lblConfirmarContraseña.Visible = true;
            txtConfirmarContraseña.Visible = true;
            lblEmail.Visible = true;
            txtEmail.Visible = true;

            // IMPORTANTE: Documento y Nombre SIEMPRE visibles para ambos tipos
            lblDocumento.Visible = true;
            txtDocumento.Visible = true;
            lblNombre.Visible = true;
            txtNombre.Visible = true;
            lblApellido.Visible = true;
            txtApellido.Visible = true;


            // Campos específicos de Proveedor
            lblCuit.Visible = esProveedor;
            txtCuit.Visible = esProveedor;
            lblRazonSocial.Visible = esProveedor;
            txtRazonSocial.Visible = esProveedor;
            lblTelefono.Visible = esProveedor;
            txtTelefono.Visible = esProveedor;
            lblDireccionProveedor.Visible = esProveedor;
            txtDireccionProveedor.Visible = esProveedor;

            // Campos específicos de Cliente
            lblDireccionCliente.Visible = !esProveedor;
            txtDireccionCliente.Visible = !esProveedor;


        }

       

        private bool ValidarCampos()
        {
            bool esProveedor = cmbTipoUsuario.SelectedItem.ToString() == "Proveedor";

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MostrarError("Debe ingresar un nombre de usuario");
                txtUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MostrarError("Debe ingresar una contraseña");
                txtContraseña.Focus();
                return false;
            }

            if (txtContraseña.Text != txtConfirmarContraseña.Text)
            {
                MostrarError("Las contraseñas no coinciden");
                txtConfirmarContraseña.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MostrarError("Debe ingresar un email válido");
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MostrarError("Debe ingresar un número de documento");
                txtDocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarError("Debe ingresar un nombre");
                txtNombre.Focus();
                return false;
            }

            // Validaciones específicas para Cliente (solo si no es cliente existente)
            if (!esProveedor && !esClienteExistente)
            {
                if (string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    MostrarError("Debe ingresar un apellido");
                    txtApellido.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtDireccionCliente.Text))
                {
                    MostrarError("Debe ingresar una dirección");
                    txtDireccionCliente.Focus();
                    return false;
                }
            }
            // Validaciones específicas para Proveedor
            else if (esProveedor)
            {
                if (string.IsNullOrWhiteSpace(txtCuit.Text))
                {
                    MostrarError("Debe ingresar un CUIT");
                    txtCuit.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MostrarError("Debe ingresar una Razón Social");
                    txtRazonSocial.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtTelefono.Text))
                {
                    MostrarError("Debe ingresar un Teléfono");
                    txtTelefono.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtDireccionProveedor.Text))
                {
                    MostrarError("Debe ingresar una dirección");
                    txtDireccionProveedor.Focus();
                    return false;
                }
            }

            return true;
        }



        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

   


        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                if (esClienteExistente && clienteEncontrado != null)
                {
                    // Proceso para cliente existente (primer acceso)
                    ConfigurarUsuarioParaClienteExistente();
                }
                else
                {
                    // Proceso normal para nuevos usuarios
                    RegistrarNuevoUsuario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general al registrar usuario: {ex.Message}\nDetalles: {ex.InnerException?.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarUsuarioParaClienteExistente()
        {
            try
            {
                // Crear el usuario para el cliente existente
                var usuario = new Usuario
                {
                    NombreUsuario = txtUsuario.Text.Trim(),
                    Contraseña = txtContraseña.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    IntentosIngreso = 0,
                    Rol = "Cliente"
                };

                // Obtener el grupo Cliente
                var grupoCliente = controladora.ObtenerGrupoPorNombre("Cliente");
                if (grupoCliente == null)
                    throw new Exception("No se encontró el grupo Cliente");

                // Crear usuario con grupo
                controladora.AgregarUsuarioConGrupo(usuario, grupoCliente.Id);

                // Actualizar el cliente con el usuario
                controladoraCliente.ActualizarUsuarioEnCliente(clienteEncontrado.Id, usuario.Id);

                // Mostrar mensaje de éxito y configurar sesión
                MessageBox.Show(
                    "¡Contraseña configurada exitosamente!\n\n" +
                    "Tu cuenta está lista. Serás redirigido al sistema.",
                    "¡Bienvenido!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Configurar la sesión automáticamente
                SesionActual.Usuario = usuario;

                // Indicar que el login fue exitoso
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar la contraseña: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegistrarNuevoUsuario()
        {
            string tipoUsuario = cmbTipoUsuario.SelectedItem.ToString();

            var usuario = new Usuario
            {
                NombreUsuario = txtUsuario.Text.Trim(),
                Contraseña = txtContraseña.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Estado = true,
                FechaCreacion = DateTime.Now,
                IntentosIngreso = 0,
                Rol = tipoUsuario
            };

            if (tipoUsuario == "Cliente")
            {
                try
                {
                    var cliente = new Cliente
                    {
                        Documento = txtDocumento.Text.Trim(),
                        Nombre = txtNombre.Text.Trim(),
                        Apellido = txtApellido.Text.Trim(),
                        Direccion = txtDireccionCliente.Text.Trim(),
                        Ventas = new List<Venta>()
                    };

                    controladora.RegistrarCliente(usuario, cliente);

                    MessageBox.Show("Cliente registrado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error específico al registrar cliente: {ex.Message}\nDetalles: {ex.InnerException?.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tipoUsuario == "Proveedor")
            {
                try
                {
                    var proveedor = new Proveedor
                    {
                        Cuit = txtCuit.Text.Trim(),
                        RazonSocial = txtRazonSocial.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Direccion = txtDireccionProveedor.Text.Trim(),
                        Compras = new List<Compra>()
                    };

                    controladora.RegistrarProveedor(usuario, proveedor);

                    MessageBox.Show("Proveedor registrado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error específico al registrar proveedor: {ex.Message}\nDetalles: {ex.InnerException?.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        


    }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
