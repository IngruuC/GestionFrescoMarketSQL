using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
namespace VISTA
{
    partial class FormRegistroProducto
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
            this.panelDatos = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtCodigoBarra = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.chkEsPerecedero = new System.Windows.Forms.CheckBox();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.lblCodigoBarra = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblFechaVencimiento = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.lblResultadoBusqueda = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.panelDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDatos
            // 
            this.panelDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(12)))), ((int)(((byte)(102)))));
            this.panelDatos.Controls.Add(this.dgvProductos);
            this.panelDatos.Controls.Add(this.lblTitulo);
            this.panelDatos.Controls.Add(this.txtCodigoBarra);
            this.panelDatos.Controls.Add(this.txtNombre);
            this.panelDatos.Controls.Add(this.txtPrecio);
            this.panelDatos.Controls.Add(this.txtStock);
            this.panelDatos.Controls.Add(this.chkEsPerecedero);
            this.panelDatos.Controls.Add(this.dtpFechaVencimiento);
            this.panelDatos.Controls.Add(this.lblCodigoBarra);
            this.panelDatos.Controls.Add(this.lblNombre);
            this.panelDatos.Controls.Add(this.lblPrecio);
            this.panelDatos.Controls.Add(this.lblStock);
            this.panelDatos.Controls.Add(this.lblFechaVencimiento);
            this.panelDatos.Controls.Add(this.panelBotones);
            this.panelDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDatos.Location = new System.Drawing.Point(0, 0);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Padding = new System.Windows.Forms.Padding(20);
            this.panelDatos.Size = new System.Drawing.Size(1000, 300);
            this.panelDatos.TabIndex = 2;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(118)))), ((int)(((byte)(176)))));
            this.lblTitulo.Location = new System.Drawing.Point(45, 45);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(210, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Registro de Productos";
            // 
            // txtCodigoBarra
            // 
            this.txtCodigoBarra.Location = new System.Drawing.Point(0, 0);
            this.txtCodigoBarra.Name = "txtCodigoBarra";
            this.txtCodigoBarra.Size = new System.Drawing.Size(100, 20);
            this.txtCodigoBarra.TabIndex = 1;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(0, 0);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 2;
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(0, 0);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(100, 20);
            this.txtPrecio.TabIndex = 3;
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(0, 0);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(100, 20);
            this.txtStock.TabIndex = 4;
            // 
            // chkEsPerecedero
            // 
            this.chkEsPerecedero.AutoSize = true;
            this.chkEsPerecedero.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkEsPerecedero.Location = new System.Drawing.Point(520, 100);
            this.chkEsPerecedero.Name = "chkEsPerecedero";
            this.chkEsPerecedero.Size = new System.Drawing.Size(127, 17);
            this.chkEsPerecedero.TabIndex = 5;
            this.chkEsPerecedero.Text = "Producto Perecedero";
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(500, 130);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaVencimiento.TabIndex = 6;
            // 
            // lblCodigoBarra
            // 
            this.lblCodigoBarra.Location = new System.Drawing.Point(0, 0);
            this.lblCodigoBarra.Name = "lblCodigoBarra";
            this.lblCodigoBarra.Size = new System.Drawing.Size(100, 23);
            this.lblCodigoBarra.TabIndex = 7;
            // 
            // lblNombre
            // 
            this.lblNombre.Location = new System.Drawing.Point(0, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(100, 23);
            this.lblNombre.TabIndex = 8;
            // 
            // lblPrecio
            // 
            this.lblPrecio.Location = new System.Drawing.Point(0, 0);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(100, 23);
            this.lblPrecio.TabIndex = 9;
            // 
            // lblStock
            // 
            this.lblStock.Location = new System.Drawing.Point(0, 0);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(100, 23);
            this.lblStock.TabIndex = 10;
            // 
            // lblFechaVencimiento
            // 
            this.lblFechaVencimiento.AutoSize = true;
            this.lblFechaVencimiento.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFechaVencimiento.Location = new System.Drawing.Point(520, 130);
            this.lblFechaVencimiento.Name = "lblFechaVencimiento";
            this.lblFechaVencimiento.Size = new System.Drawing.Size(116, 13);
            this.lblFechaVencimiento.TabIndex = 11;
            this.lblFechaVencimiento.Text = "Fecha de Vencimiento:";
            // 
            // panelBotones
            // 
            this.panelBotones.Location = new System.Drawing.Point(0, 0);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(200, 100);
            this.panelBotones.TabIndex = 12;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(0, 0);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 0;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(0, 0);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 0;
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(0, 0);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 0;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(0, 0);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 0;
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Location = new System.Drawing.Point(0, 0);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Size = new System.Drawing.Size(200, 100);
            this.panelBusqueda.TabIndex = 1;
            // 
            // lblResultadoBusqueda
            // 
            this.lblResultadoBusqueda.Location = new System.Drawing.Point(0, 0);
            this.lblResultadoBusqueda.Name = "lblResultadoBusqueda";
            this.lblResultadoBusqueda.Size = new System.Drawing.Size(100, 23);
            this.lblResultadoBusqueda.TabIndex = 0;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(0, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(100, 20);
            this.txtBuscar.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(0, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(100, 23);
            this.lblBuscar.TabIndex = 0;
            // 
            // dgvProductos
            // 
            this.dgvProductos.Location = new System.Drawing.Point(0, 127);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.Size = new System.Drawing.Size(240, 150);
            this.dgvProductos.TabIndex = 0;
            // 
            // FormRegistroProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(15)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelBusqueda);
            this.Controls.Add(this.panelDatos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRegistroProducto";
            this.Text = "Registro de Productos";
            this.Load += new System.EventHandler(this.FormRegistroProducto_Load);
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);

        }

        private void ConfigurarCampoEntrada(TextBox textBox, Label label, string texto, int y)
        {
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(25, y);
            label.Text = texto;
            label.ForeColor = System.Drawing.Color.Gainsboro;

            textBox.Location = new System.Drawing.Point(150, y);
            textBox.Size = new System.Drawing.Size(250, 20);
            textBox.BackColor = System.Drawing.Color.FromArgb(95, 20, 100);
            textBox.ForeColor = System.Drawing.Color.Gainsboro;
            textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void ConfigurarPanelBotones()
        {
            this.panelBotones.Height = 50;
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(87, 12, 102);

            ConfigurarBoton(btnGuardar, "Guardar", 0);
            ConfigurarBoton(btnModificar, "Modificar", 1);
            ConfigurarBoton(btnEliminar, "Eliminar", 2);
            ConfigurarBoton(btnLimpiar, "Limpiar", 3);
        }

        private void ConfigurarBoton(Button btn, string texto, int posicion)
        {
            btn.Text = texto;
            btn.Size = new System.Drawing.Size(100, 35);
            btn.Location = new System.Drawing.Point(25 + (posicion * 120), 8);
            btn.BackColor = System.Drawing.Color.FromArgb(87, 12, 102);
            btn.ForeColor = System.Drawing.Color.Gainsboro;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
        }

        private void ConfigurarPanelBusqueda()
        {
            this.panelBusqueda.Height = 60;
            this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusqueda.BackColor = System.Drawing.Color.FromArgb(70, 15, 75);

            lblBuscar.Text = "Buscar:";
            lblBuscar.ForeColor = System.Drawing.Color.Gainsboro;
            lblBuscar.Location = new System.Drawing.Point(25, 20);
            lblBuscar.AutoSize = true;

            txtBuscar.Location = new System.Drawing.Point(80, 17);
            txtBuscar.Size = new System.Drawing.Size(250, 20);
            txtBuscar.BackColor = System.Drawing.Color.FromArgb(95, 20, 100);
            txtBuscar.ForeColor = System.Drawing.Color.Gainsboro;
            txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void ConfigurarDataGridView()
        {
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.FromArgb(70, 15, 75);
            this.dgvProductos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvProductos.EnableHeadersVisualStyles = false;
            this.dgvProductos.GridColor = System.Drawing.Color.FromArgb(87, 12, 102);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        }

        private System.Windows.Forms.Panel panelDatos;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtCodigoBarra;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblCodigoBarra;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblFechaVencimiento;
        private System.Windows.Forms.CheckBox chkEsPerecedero;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Label lblResultadoBusqueda;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.DataGridView dgvProductos;
    }
}