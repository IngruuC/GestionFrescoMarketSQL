using System;
using System.Windows.Forms;

namespace VISTA
{
    partial class FormMisProductos : Form
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
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnActualizarLista = new System.Windows.Forms.Button();
            this.panelProductos = new System.Windows.Forms.Panel();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblCantidadProductos = new System.Windows.Forms.Label();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.panelProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.panelBusqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(900, 70);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(346, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(228, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "MIS PRODUCTOS";
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelBotones.Controls.Add(this.btnCerrar);
            this.panelBotones.Controls.Add(this.btnActualizarLista);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 520);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(900, 80);
            this.panelBotones.TabIndex = 1;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(754, 20);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 40);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "CERRAR";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // btnActualizarLista
            // 
            this.btnActualizarLista.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnActualizarLista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarLista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarLista.ForeColor = System.Drawing.Color.White;
            this.btnActualizarLista.Location = new System.Drawing.Point(27, 20);
            this.btnActualizarLista.Name = "btnActualizarLista";
            this.btnActualizarLista.Size = new System.Drawing.Size(180, 40);
            this.btnActualizarLista.TabIndex = 0;
            this.btnActualizarLista.Text = "ACTUALIZAR LISTA";
            this.btnActualizarLista.UseVisualStyleBackColor = false;
            this.btnActualizarLista.Click += new System.EventHandler(this.btnActualizarLista_Click_1);
            // 
            // panelProductos
            // 
            this.panelProductos.Controls.Add(this.dgvProductos);
            this.panelProductos.Controls.Add(this.lblCantidadProductos);
            this.panelProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProductos.Location = new System.Drawing.Point(0, 130);
            this.panelProductos.Name = "panelProductos";
            this.panelProductos.Padding = new System.Windows.Forms.Padding(20);
            this.panelProductos.Size = new System.Drawing.Size(900, 390);
            this.panelProductos.TabIndex = 2;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.Location = new System.Drawing.Point(20, 36);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(860, 334);
            this.dgvProductos.TabIndex = 1;
            // 
            // lblCantidadProductos
            // 
            this.lblCantidadProductos.AutoSize = true;
            this.lblCantidadProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCantidadProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadProductos.Location = new System.Drawing.Point(20, 20);
            this.lblCantidadProductos.Name = "lblCantidadProductos";
            this.lblCantidadProductos.Size = new System.Drawing.Size(152, 16);
            this.lblCantidadProductos.TabIndex = 0;
            this.lblCantidadProductos.Text = "0 producto(s) en lista";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelBusqueda.Controls.Add(this.btnBuscar);
            this.panelBusqueda.Controls.Add(this.txtBuscar);
            this.panelBusqueda.Controls.Add(this.lblBuscar);
            this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusqueda.Location = new System.Drawing.Point(0, 70);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Size = new System.Drawing.Size(900, 60);
            this.panelBusqueda.TabIndex = 3;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(650, 17);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(83, 20);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(550, 20);
            this.txtBuscar.TabIndex = 1;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(20, 20);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(59, 16);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";
            // 
            // FormMisProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelProductos);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelBusqueda);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMisProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mis Productos - Fresco Market";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelProductos.ResumeLayout(false);
            this.panelProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnActualizarLista;
        private System.Windows.Forms.Panel panelProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lblCantidadProductos;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
    }
}