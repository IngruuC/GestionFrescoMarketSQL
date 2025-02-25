﻿using CONTROLADORA;
using ENTIDADES;
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

        public RegistroUsuario()
        {
            InitializeComponent();
            controladora = ControladoraUsuario.ObtenerInstancia();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            txtContraseña.PasswordChar = '*';
            txtConfirmarContraseña.PasswordChar = '*';

            cmbTipoUsuario.Items.Add("Cliente");
            cmbTipoUsuario.Items.Add("Proveedor");
            cmbTipoUsuario.SelectedIndex = 0;

            cmbTipoUsuario.SelectedIndexChanged += (s, e) => ActualizarCamposVisibles();
            ActualizarCamposVisibles();
        }

        private void ActualizarCamposVisibles()
        {
            bool esProveedor = cmbTipoUsuario.SelectedItem.ToString() == "Proveedor";

            // Campos comunes que siempre deben estar visibles
           // lblUsuario.Visible = true;
            txtUsuario.Visible = true;
            lblContraseña.Visible = true;
            txtContraseña.Visible = true;
            lblConfirmarContraseña.Visible = true;
            txtConfirmarContraseña.Visible = true;
            lblEmail.Visible = true;
            txtEmail.Visible = true;

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
            lblDocumento.Visible = !esProveedor;
            txtDocumento.Visible = !esProveedor;
            lblNombre.Visible = !esProveedor;
            txtNombre.Visible = !esProveedor;
            lblApellido.Visible = !esProveedor;
            txtApellido.Visible = !esProveedor;
            lblDireccionCliente.Visible = !esProveedor;
            txtDireccionCliente.Visible = !esProveedor;
        }

       

        private bool ValidarCampos()
        {
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

            return true;
        }



        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void btnRegistrar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                string tipoUsuario = cmbTipoUsuario.SelectedItem.ToString();

                var usuario = new Usuario
                {
                    NombreUsuario = txtUsuario.Text.Trim(),
                    Contraseña = txtContraseña.Text.Trim(), // La contraseña se hasheará en la controladora
                    Email = txtEmail.Text.Trim(),
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    IntentosIngreso = 0,
                    Rol = tipoUsuario // Agregamos el Rol aquí
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
                            Ventas = new List<Venta>() // Inicializamos la lista de ventas
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error general al registrar usuario: {ex.Message}\nDetalles: {ex.InnerException?.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
