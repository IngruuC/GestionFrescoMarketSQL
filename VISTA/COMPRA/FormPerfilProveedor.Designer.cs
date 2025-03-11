using System;
using System.Windows.Forms;

namespace VISTA.COMPRA
{
    partial class FormPerfilProveedor : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBoxDatosProveedor = new System.Windows.Forms.GroupBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtCuit = new System.Windows.Forms.TextBox();
            this.lblCuit = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.groupBoxDatosUsuario = new System.Windows.Forms.GroupBox();
            this.btnCambiarPassword = new System.Windows.Forms.Button();
            this.txtCorreoElectronico = new System.Windows.Forms.TextBox();
            this.lblCorreoElectronico = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.groupBoxDatosProveedor.SuspendLayout();
            this.groupBoxDatosUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(800, 80);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(279, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(243, 31);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Mi Perfil Empresa";
            // 
            // groupBoxDatosProveedor
            // 
            this.groupBoxDatosProveedor.Controls.Add(this.txtDireccion);
            this.groupBoxDatosProveedor.Controls.Add(this.lblDireccion);
            this.groupBoxDatosProveedor.Controls.Add(this.txtEmail);
            this.groupBoxDatosProveedor.Controls.Add(this.lblEmail);
            this.groupBoxDatosProveedor.Controls.Add(this.txtTelefono);
            this.groupBoxDatosProveedor.Controls.Add(this.lblTelefono);
            this.groupBoxDatosProveedor.Controls.Add(this.txtCuit);
            this.groupBoxDatosProveedor.Controls.Add(this.lblCuit);
            this.groupBoxDatosProveedor.Controls.Add(this.txtRazonSocial);
            this.groupBoxDatosProveedor.Controls.Add(this.lblRazonSocial);
            this.groupBoxDatosProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDatosProveedor.Location = new System.Drawing.Point(30, 100);
            this.groupBoxDatosProveedor.Name = "groupBoxDatosProveedor";
            this.groupBoxDatosProveedor.Size = new System.Drawing.Size(350, 350);
            this.groupBoxDatosProveedor.TabIndex = 1;
            this.groupBoxDatosProveedor.TabStop = false;
            this.groupBoxDatosProveedor.Text = "Datos de la Empresa";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(20, 260);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(310, 70);
            this.txtDireccion.TabIndex = 9;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.Location = new System.Drawing.Point(20, 240);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(67, 16);
            this.lblDireccion.TabIndex = 8;
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(20, 210);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(310, 22);
            this.txtEmail.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(20, 190);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 16);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(20, 160);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(310, 22);
            this.txtTelefono.TabIndex = 5;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.Location = new System.Drawing.Point(20, 140);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(64, 16);
            this.lblTelefono.TabIndex = 4;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtCuit
            // 
            this.txtCuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuit.Location = new System.Drawing.Point(20, 110);
            this.txtCuit.Name = "txtCuit";
            this.txtCuit.Size = new System.Drawing.Size(310, 22);
            this.txtCuit.TabIndex = 3;
            // 
            // lblCuit
            // 
            this.lblCuit.AutoSize = true;
            this.lblCuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuit.Location = new System.Drawing.Point(20, 90);
            this.lblCuit.Name = "lblCuit";
            this.lblCuit.Size = new System.Drawing.Size(41, 16);
            this.lblCuit.TabIndex = 2;
            this.lblCuit.Text = "CUIT:";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazonSocial.Location = new System.Drawing.Point(20, 60);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(310, 22);
            this.txtRazonSocial.TabIndex = 1;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRazonSocial.Location = new System.Drawing.Point(20, 40);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(90, 16);
            this.lblRazonSocial.TabIndex = 0;
            this.lblRazonSocial.Text = "Razón Social:";
            // 
            // groupBoxDatosUsuario
            // 
            this.groupBoxDatosUsuario.Controls.Add(this.btnCambiarPassword);
            this.groupBoxDatosUsuario.Controls.Add(this.txtCorreoElectronico);
            this.groupBoxDatosUsuario.Controls.Add(this.lblCorreoElectronico);
            this.groupBoxDatosUsuario.Controls.Add(this.txtUsuario);
            this.groupBoxDatosUsuario.Controls.Add(this.lblUsuario);
            this.groupBoxDatosUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxDatosUsuario.Location = new System.Drawing.Point(420, 100);
            this.groupBoxDatosUsuario.Name = "groupBoxDatosUsuario";
            this.groupBoxDatosUsuario.Size = new System.Drawing.Size(350, 220);
            this.groupBoxDatosUsuario.TabIndex = 2;
            this.groupBoxDatosUsuario.TabStop = false;
            this.groupBoxDatosUsuario.Text = "Datos de Usuario";
            // 
            // btnCambiarPassword
            // 
            this.btnCambiarPassword.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCambiarPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarPassword.ForeColor = System.Drawing.Color.White;
            this.btnCambiarPassword.Location = new System.Drawing.Point(20, 150);
            this.btnCambiarPassword.Name = "btnCambiarPassword";
            this.btnCambiarPassword.Size = new System.Drawing.Size(310, 40);
            this.btnCambiarPassword.TabIndex = 4;
            this.btnCambiarPassword.Text = "Cambiar Contraseña";
            this.btnCambiarPassword.UseVisualStyleBackColor = false;
            this.btnCambiarPassword.Click += new System.EventHandler(this.btnCambiarPassword_Click);
            // 
            // txtCorreoElectronico
            // 
            this.txtCorreoElectronico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoElectronico.Location = new System.Drawing.Point(20, 110);
            this.txtCorreoElectronico.Name = "txtCorreoElectronico";
            this.txtCorreoElectronico.Size = new System.Drawing.Size(310, 22);
            this.txtCorreoElectronico.TabIndex = 3;
            // 
            // lblCorreoElectronico
            // 
            this.lblCorreoElectronico.AutoSize = true;
            this.lblCorreoElectronico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorreoElectronico.Location = new System.Drawing.Point(20, 90);
            this.lblCorreoElectronico.Name = "lblCorreoElectronico";
            this.lblCorreoElectronico.Size = new System.Drawing.Size(121, 16);
            this.lblCorreoElectronico.TabIndex = 2;
            this.lblCorreoElectronico.Text = "Correo Electrónico:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(20, 60);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(310, 22);
            this.txtUsuario.TabIndex = 1;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(20, 40);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(57, 16);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(420, 390);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(350, 40);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar Datos";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click_1);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Green;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(420, 350);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(170, 40);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click_1);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Firebrick;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(600, 350);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(170, 40);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCerrar.Location = new System.Drawing.Point(600, 436);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(170, 40);
            this.btnCerrar.TabIndex = 6;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FormPerfilProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.groupBoxDatosUsuario);
            this.Controls.Add(this.groupBoxDatosProveedor);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPerfilProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mi Perfil - Fresco Market";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.groupBoxDatosProveedor.ResumeLayout(false);
            this.groupBoxDatosProveedor.PerformLayout();
            this.groupBoxDatosUsuario.ResumeLayout(false);
            this.groupBoxDatosUsuario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxDatosProveedor;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label lblCuit;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.GroupBox groupBoxDatosUsuario;
        private System.Windows.Forms.Button btnCambiarPassword;
        private System.Windows.Forms.TextBox txtCorreoElectronico;
        private System.Windows.Forms.Label lblCorreoElectronico;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCerrar;
    }
}