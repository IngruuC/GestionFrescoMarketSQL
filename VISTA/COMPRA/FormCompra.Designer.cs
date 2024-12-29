using System;
using System.Windows.Forms;
using System.Drawing;

namespace VISTA
{
    partial class FormCompra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCompra));

            this.panelSuperior = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBoxCompra = new System.Windows.Forms.GroupBox();
            this.cboProveedores = new System.Windows.Forms.ComboBox();
            this.cboProductos = new System.Windows.Forms.ComboBox();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.txtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgvCompra = new System.Windows.Forms.DataGridView();
            this.btnFinalizarCompra = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompra)).BeginInit();
            this.panelSuperior.SuspendLayout();
            this.groupBoxCompra.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.pictureBox1);
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1084, 70);

            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;

            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(63, 21);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(171, 25);
            this.lblTitulo.Text = "NUEVA COMPRA";

            // 
            // groupBoxCompra
            // 
            this.groupBoxCompra.Controls.Add(this.cboProveedores);
            this.groupBoxCompra.Controls.Add(this.cboProductos);
            this.groupBoxCompra.Controls.Add(this.nudCantidad);
            this.groupBoxCompra.Controls.Add(this.txtPrecioUnitario);
            this.groupBoxCompra.Controls.Add(this.label1);
            this.groupBoxCompra.Controls.Add(this.label2);
            this.groupBoxCompra.Controls.Add(this.label3);
            this.groupBoxCompra.Controls.Add(this.label4);
            this.groupBoxCompra.Controls.Add(this.btnAgregarProducto);
            this.groupBoxCompra.Controls.Add(this.btnModificar);
            this.groupBoxCompra.Controls.Add(this.btnEliminar);
            this.groupBoxCompra.Controls.Add(this.lblTotal);
            this.groupBoxCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxCompra.Location = new System.Drawing.Point(12, 88);
            this.groupBoxCompra.Name = "groupBoxCompra";
            this.groupBoxCompra.Size = new System.Drawing.Size(304, 461);
            this.groupBoxCompra.Text = "DATOS DE LA COMPRA";

            // 
            // Labels
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(17, 43);
            label1.Size = new System.Drawing.Size(96, 16);
            label1.Text = "PROVEEDOR:";

            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(17, 104);
            label2.Size = new System.Drawing.Size(96, 16);
            label2.Text = "PRODUCTO:";

            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(17, 165);
            label3.Size = new System.Drawing.Size(88, 16);
            label3.Text = "CANTIDAD:";

            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(17, 226);
            label4.Size = new System.Drawing.Size(135, 16);
            label4.Text = "PRECIO UNITARIO:";

            // 
            // ComboBoxes
            // 
            cboProveedores.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProveedores.Location = new System.Drawing.Point(20, 62);
            cboProveedores.Size = new System.Drawing.Size(259, 24);

            cboProductos.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProductos.Location = new System.Drawing.Point(20, 123);
            cboProductos.Size = new System.Drawing.Size(259, 24);

            // 
            // NumericUpDown y TextBox
            // 
            nudCantidad.Location = new System.Drawing.Point(20, 184);
            nudCantidad.Size = new System.Drawing.Size(120, 22);
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 9999;
            nudCantidad.Value = 1;

            txtPrecioUnitario.Location = new System.Drawing.Point(20, 245);
            txtPrecioUnitario.Size = new System.Drawing.Size(259, 22);

            // 
            // Botones
            // 
            btnAgregarProducto.BackColor = System.Drawing.Color.DarkGoldenrod;
            btnAgregarProducto.FlatStyle = FlatStyle.Flat;
            btnAgregarProducto.ForeColor = System.Drawing.Color.White;
            btnAgregarProducto.Location = new System.Drawing.Point(20, 293);
            btnAgregarProducto.Size = new System.Drawing.Size(259, 35);
            btnAgregarProducto.Text = "AGREGAR PRODUCTO";

            btnModificar.BackColor = System.Drawing.Color.DarkGoldenrod;
            btnModificar.FlatStyle = FlatStyle.Flat;
            btnModificar.ForeColor = System.Drawing.Color.White;
            btnModificar.Location = new System.Drawing.Point(20, 340);
            btnModificar.Size = new System.Drawing.Size(120, 35);
            btnModificar.Text = "MODIFICAR";

            btnEliminar.BackColor = System.Drawing.Color.DarkGoldenrod;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = System.Drawing.Color.White;
            btnEliminar.Location = new System.Drawing.Point(159, 340);
            btnEliminar.Size = new System.Drawing.Size(120, 35);
            btnEliminar.Text = "ELIMINAR";

            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            lblTotal.Location = new System.Drawing.Point(16, 421);
            lblTotal.Size = new System.Drawing.Size(124, 20);
            lblTotal.Text = "TOTAL: $ 0.00";

            // 
            // dgvCompra
            // 
            this.dgvCompra.AllowUserToAddRows = false;
            this.dgvCompra.AllowUserToDeleteRows = false;
            this.dgvCompra.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompra.Location = new System.Drawing.Point(322, 88);
            this.dgvCompra.MultiSelect = false;
            this.dgvCompra.ReadOnly = true;
            this.dgvCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompra.Size = new System.Drawing.Size(750, 461);

            // 
            // Botones Finales
            // 
            btnFinalizarCompra.BackColor = System.Drawing.Color.DarkGoldenrod;
            btnFinalizarCompra.FlatStyle = FlatStyle.Flat;
            btnFinalizarCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            btnFinalizarCompra.ForeColor = System.Drawing.Color.White;
            btnFinalizarCompra.Location = new System.Drawing.Point(845, 555);
            btnFinalizarCompra.Size = new System.Drawing.Size(113, 35);
            btnFinalizarCompra.Text = "FINALIZAR";

            btnCancelar.BackColor = System.Drawing.Color.DarkGoldenrod;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            btnCancelar.ForeColor = System.Drawing.Color.Red;
            btnCancelar.Location = new System.Drawing.Point(964, 555);
            btnCancelar.Size = new System.Drawing.Size(108, 35);
            btnCancelar.Text = "CANCELAR";

            // 
            // FormCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1084, 602);
            this.Controls.AddRange(new Control[] {
                this.btnCancelar,
                this.btnFinalizarCompra,
                this.dgvCompra,
                this.groupBoxCompra,
                this.panelSuperior
            });
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Nueva Compra - Fresco Market";


            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxCompra;
        private System.Windows.Forms.ComboBox cboProveedores;
        private System.Windows.Forms.ComboBox cboProductos;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.TextBox txtPrecioUnitario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dgvCompra;
        private System.Windows.Forms.Button btnFinalizarCompra;
        private System.Windows.Forms.Button btnCancelar;
    }
}