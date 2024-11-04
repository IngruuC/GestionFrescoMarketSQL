using System;
using System.Drawing;
using System.Windows.Forms;
namespace VISTA
    {
        partial class FormRegistroCliente
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

            private void InitializeComponent()
            {
                System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

                this.panelDatos = new System.Windows.Forms.Panel();
                this.lblTitulo = new System.Windows.Forms.Label();
                this.txtDireccion = new System.Windows.Forms.TextBox();
                this.txtApellido = new System.Windows.Forms.TextBox();
                this.txtNombre = new System.Windows.Forms.TextBox();
                this.txtDocumento = new System.Windows.Forms.TextBox();
                this.lblDireccion = new System.Windows.Forms.Label();
                this.lblApellido = new System.Windows.Forms.Label();
                this.lblNombre = new System.Windows.Forms.Label();
                this.lblDocumento = new System.Windows.Forms.Label();
                this.panelBotones = new System.Windows.Forms.Panel();
                this.btnLimpiar = new System.Windows.Forms.Button();
                this.btnEliminar = new System.Windows.Forms.Button();
                this.btnModificar = new System.Windows.Forms.Button();
                this.btnGuardar = new System.Windows.Forms.Button();
                this.panelBusqueda = new System.Windows.Forms.Panel();
                this.lblResultadoBusqueda = new System.Windows.Forms.Label();
                this.txtBuscar = new System.Windows.Forms.TextBox();
                this.lblBuscar = new System.Windows.Forms.Label();
                this.dgvClientes = new System.Windows.Forms.DataGridView();
                this.panelContenedor = new System.Windows.Forms.Panel();

                // 
                // panelDatos
                // 
                this.panelDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
                this.panelDatos.Controls.Add(this.lblTitulo);
                this.panelDatos.Controls.Add(this.txtDireccion);
                this.panelDatos.Controls.Add(this.txtApellido);
                this.panelDatos.Controls.Add(this.txtNombre);
                this.panelDatos.Controls.Add(this.txtDocumento);
                this.panelDatos.Controls.Add(this.lblDireccion);
                this.panelDatos.Controls.Add(this.lblApellido);
                this.panelDatos.Controls.Add(this.lblNombre);
                this.panelDatos.Controls.Add(this.lblDocumento);
                this.panelDatos.Controls.Add(this.panelBotones);
                this.panelDatos.Dock = System.Windows.Forms.DockStyle.Top;
                this.panelDatos.Location = new System.Drawing.Point(0, 0);
                this.panelDatos.Name = "panelDatos";
                this.panelDatos.Padding = new System.Windows.Forms.Padding(10);
                this.panelDatos.Size = new System.Drawing.Size(1000, 280);

                // 
                // lblTitulo
                // 
                this.lblTitulo.AutoSize = true;
                this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
                this.lblTitulo.ForeColor = System.Drawing.Color.Gainsboro;
                this.lblTitulo.Location = new System.Drawing.Point(15, 15);
                this.lblTitulo.Name = "lblTitulo";
                this.lblTitulo.Size = new System.Drawing.Size(186, 25);
                this.lblTitulo.Text = "Registro de Clientes";

                // 
                // Labels
                // 
                ConfigurarLabel(lblDocumento, "Documento", 50);
                ConfigurarLabel(lblNombre, "Nombre", 100);
                ConfigurarLabel(lblApellido, "Apellido", 150);
                ConfigurarLabel(lblDireccion, "Dirección", 200);

                // 
                // TextBoxes
                // 
                ConfigurarTextBox(txtDocumento, 50);
                ConfigurarTextBox(txtNombre, 100);
                ConfigurarTextBox(txtApellido, 150);
                ConfigurarTextBox(txtDireccion, 200);

                // 
                // panelBotones
                // 
                this.panelBotones.Controls.Add(this.btnLimpiar);
                this.panelBotones.Controls.Add(this.btnEliminar);
                this.panelBotones.Controls.Add(this.btnModificar);
                this.panelBotones.Controls.Add(this.btnGuardar);
                this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.panelBotones.Location = new System.Drawing.Point(10, 220);
                this.panelBotones.Name = "panelBotones";
                this.panelBotones.Size = new System.Drawing.Size(980, 50);

                // 
                // Botones
                // 
                ConfigurarBoton(btnGuardar, "Guardar", 0);
                ConfigurarBoton(btnModificar, "Modificar", 1);
                ConfigurarBoton(btnEliminar, "Eliminar", 2);
                ConfigurarBoton(btnLimpiar, "Limpiar", 3);

                // 
                // panelBusqueda
                // 
                this.panelBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
                this.panelBusqueda.Controls.Add(this.lblResultadoBusqueda);
                this.panelBusqueda.Controls.Add(this.txtBuscar);
                this.panelBusqueda.Controls.Add(this.lblBuscar);
                this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
                this.panelBusqueda.Location = new System.Drawing.Point(0, 280);
                this.panelBusqueda.Name = "panelBusqueda";
                this.panelBusqueda.Size = new System.Drawing.Size(1000, 60);
                this.panelBusqueda.Padding = new System.Windows.Forms.Padding(10);

                // 
                // lblBuscar y txtBuscar
                // 
                this.lblBuscar.AutoSize = true;
                this.lblBuscar.ForeColor = System.Drawing.Color.Gainsboro;
                this.lblBuscar.Location = new System.Drawing.Point(15, 25);
                this.lblBuscar.Name = "lblBuscar";
                this.lblBuscar.Size = new System.Drawing.Size(45, 13);
                this.lblBuscar.Text = "Buscar:";

                this.txtBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(44)))), ((int)(((byte)(85)))));
                this.txtBuscar.ForeColor = System.Drawing.Color.Gainsboro;
                this.txtBuscar.Location = new System.Drawing.Point(70, 22);
                this.txtBuscar.Name = "txtBuscar";
                this.txtBuscar.Size = new System.Drawing.Size(300, 20);

                // 
                // lblResultadoBusqueda
                // 
                this.lblResultadoBusqueda.AutoSize = true;
                this.lblResultadoBusqueda.ForeColor = System.Drawing.Color.Gainsboro;
                this.lblResultadoBusqueda.Location = new System.Drawing.Point(380, 25);
                this.lblResultadoBusqueda.Name = "lblResultadoBusqueda";
                this.lblResultadoBusqueda.Size = new System.Drawing.Size(0, 13);
                this.lblResultadoBusqueda.Visible = false;

                // 
                // dgvClientes
                // 
                this.dgvClientes.AllowUserToAddRows = false;
                this.dgvClientes.AllowUserToDeleteRows = false;
                this.dgvClientes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
                this.dgvClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.dgvClientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
                this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Fill;
                this.dgvClientes.Location = new System.Drawing.Point(0, 340);
                this.dgvClientes.Name = "dgvClientes";
                this.dgvClientes.ReadOnly = true;
                this.dgvClientes.RowHeadersVisible = false;
                this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                this.dgvClientes.Size = new System.Drawing.Size(1000, 360);

                // 
                // FormRegistroCliente
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
                this.ClientSize = new System.Drawing.Size(1000, 700);
                this.Controls.Add(this.dgvClientes);
                this.Controls.Add(this.panelBusqueda);
                this.Controls.Add(this.panelDatos);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Name = "FormRegistroCliente";
                this.Text = "Registro de Clientes";
                this.Load += new System.EventHandler(this.FormRegistroCliente_Load);
                this.panelDatos.ResumeLayout(false);
                this.panelBotones.ResumeLayout(false);
                this.panelBusqueda.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
                this.ResumeLayout(false);
            }

            private void ConfigurarLabel(Label lbl, string texto, int y)
            {
                lbl.AutoSize = true;
                lbl.ForeColor = System.Drawing.Color.Gainsboro;
                lbl.Location = new System.Drawing.Point(15, y);
                lbl.Name = "lbl" + texto;
                lbl.Size = new System.Drawing.Size(70, 13);
                lbl.Text = texto + ":";
            }

            private void ConfigurarTextBox(TextBox txt, int y)
            {
                txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(44)))), ((int)(((byte)(85)))));
                txt.ForeColor = System.Drawing.Color.Gainsboro;
                txt.Location = new System.Drawing.Point(100, y);
                txt.Name = "txt" + txt.Name;
                txt.Size = new System.Drawing.Size(250, 20);
            }

            private void ConfigurarBoton(Button btn, string texto, int posicion)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ForeColor = System.Drawing.Color.Gainsboro;
                btn.Location = new System.Drawing.Point(15 + (posicion * 120), 10);
                btn.Name = "btn" + texto;
                btn.Size = new System.Drawing.Size(100, 30);
                btn.Text = texto;
                btn.UseVisualStyleBackColor = false;
            }

            private System.Windows.Forms.Panel panelDatos;
            private System.Windows.Forms.Label lblTitulo;
            private System.Windows.Forms.TextBox txtDireccion;
            private System.Windows.Forms.TextBox txtApellido;
            private System.Windows.Forms.TextBox txtNombre;
            private System.Windows.Forms.TextBox txtDocumento;
            private System.Windows.Forms.Label lblDireccion;
            private System.Windows.Forms.Label lblApellido;
            private System.Windows.Forms.Label lblNombre;
            private System.Windows.Forms.Label lblDocumento;
            private System.Windows.Forms.Panel panelBotones;
            private System.Windows.Forms.Button btnLimpiar;
            private System.Windows.Forms.Button btnEliminar;
            private System.Windows.Forms.Button btnModificar;
            private System.Windows.Forms.Button btnGuardar;
            private System.Windows.Forms.Panel panelBusqueda;
            private System.Windows.Forms.Label lblResultadoBusqueda;
            private System.Windows.Forms.TextBox txtBuscar;
            private System.Windows.Forms.Label lblBuscar;
            private System.Windows.Forms.DataGridView dgvClientes;
            private System.Windows.Forms.Panel panelContenedor;
        }
    }
