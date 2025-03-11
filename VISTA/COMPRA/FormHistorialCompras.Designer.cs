using System;
using System.Windows.Forms;

namespace VISTA
{
    partial class FormHistorialCompras : Form
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
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.lblFechaHasta = new System.Windows.Forms.Label();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.lblFechaDesde = new System.Windows.Forms.Label();
            this.panelEstadisticas = new System.Windows.Forms.Panel();
            this.lblPromedioCompra = new System.Windows.Forms.Label();
            this.lblCantidadCompras = new System.Windows.Forms.Label();
            this.lblTotalCompras = new System.Windows.Forms.Label();
            this.panelCompras = new System.Windows.Forms.Panel();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.panelFiltros.SuspendLayout();
            this.panelEstadisticas.SuspendLayout();
            this.panelCompras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.panelBotones.SuspendLayout();
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
            this.lblTitulo.Location = new System.Drawing.Point(302, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(306, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "HISTORIAL DE COMPRAS";
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelFiltros.Controls.Add(this.btnFiltrar);
            this.panelFiltros.Controls.Add(this.dtpFechaHasta);
            this.panelFiltros.Controls.Add(this.lblFechaHasta);
            this.panelFiltros.Controls.Add(this.dtpFechaDesde);
            this.panelFiltros.Controls.Add(this.lblFechaDesde);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(0, 70);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(900, 60);
            this.panelFiltros.TabIndex = 1;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.ForeColor = System.Drawing.Color.White;
            this.btnFiltrar.Location = new System.Drawing.Point(500, 15);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(100, 30);
            this.btnFiltrar.TabIndex = 4;
            this.btnFiltrar.Text = "FILTRAR";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaHasta.Location = new System.Drawing.Point(350, 20);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(120, 20);
            this.dtpFechaHasta.TabIndex = 3;
            // 
            // lblFechaHasta
            // 
            this.lblFechaHasta.AutoSize = true;
            this.lblFechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaHasta.Location = new System.Drawing.Point(290, 20);
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Size = new System.Drawing.Size(54, 16);
            this.lblFechaHasta.TabIndex = 2;
            this.lblFechaHasta.Text = "Hasta:";
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaDesde.Location = new System.Drawing.Point(100, 20);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(120, 20);
            this.dtpFechaDesde.TabIndex = 1;
            // 
            // lblFechaDesde
            // 
            this.lblFechaDesde.AutoSize = true;
            this.lblFechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaDesde.Location = new System.Drawing.Point(30, 20);
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Size = new System.Drawing.Size(58, 16);
            this.lblFechaDesde.TabIndex = 0;
            this.lblFechaDesde.Text = "Desde:";
            // 
            // panelEstadisticas
            // 
            this.panelEstadisticas.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelEstadisticas.Controls.Add(this.lblPromedioCompra);
            this.panelEstadisticas.Controls.Add(this.lblCantidadCompras);
            this.panelEstadisticas.Controls.Add(this.lblTotalCompras);
            this.panelEstadisticas.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEstadisticas.Location = new System.Drawing.Point(0, 130);
            this.panelEstadisticas.Name = "panelEstadisticas";
            this.panelEstadisticas.Size = new System.Drawing.Size(900, 40);
            this.panelEstadisticas.TabIndex = 2;
            // 
            // lblPromedioCompra
            // 
            this.lblPromedioCompra.AutoSize = true;
            this.lblPromedioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromedioCompra.ForeColor = System.Drawing.Color.White;
            this.lblPromedioCompra.Location = new System.Drawing.Point(600, 12);
            this.lblPromedioCompra.Name = "lblPromedioCompra";
            this.lblPromedioCompra.Size = new System.Drawing.Size(118, 16);
            this.lblPromedioCompra.TabIndex = 2;
            this.lblPromedioCompra.Text = "Promedio: $0.00";
            // 
            // lblCantidadCompras
            // 
            this.lblCantidadCompras.AutoSize = true;
            this.lblCantidadCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadCompras.ForeColor = System.Drawing.Color.White;
            this.lblCantidadCompras.Location = new System.Drawing.Point(350, 12);
            this.lblCantidadCompras.Name = "lblCantidadCompras";
            this.lblCantidadCompras.Size = new System.Drawing.Size(86, 16);
            this.lblCantidadCompras.TabIndex = 1;
            this.lblCantidadCompras.Text = "Cantidad: 0";
            // 
            // lblTotalCompras
            // 
            this.lblTotalCompras.AutoSize = true;
            this.lblTotalCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCompras.ForeColor = System.Drawing.Color.White;
            this.lblTotalCompras.Location = new System.Drawing.Point(30, 12);
            this.lblTotalCompras.Name = "lblTotalCompras";
            this.lblTotalCompras.Size = new System.Drawing.Size(96, 16);
            this.lblTotalCompras.TabIndex = 0;
            this.lblTotalCompras.Text = "Total: $0.00";
            // 
            // panelCompras
            // 
            this.panelCompras.Controls.Add(this.dgvCompras);
            this.panelCompras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCompras.Location = new System.Drawing.Point(0, 170);
            this.panelCompras.Name = "panelCompras";
            this.panelCompras.Padding = new System.Windows.Forms.Padding(20);
            this.panelCompras.Size = new System.Drawing.Size(900, 350);
            this.panelCompras.TabIndex = 3;
            // 
            // dgvCompras
            // 
            this.dgvCompras.AllowUserToAddRows = false;
            this.dgvCompras.AllowUserToDeleteRows = false;
            this.dgvCompras.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompras.Location = new System.Drawing.Point(20, 20);
            this.dgvCompras.Name = "dgvCompras";
            this.dgvCompras.ReadOnly = true;
            this.dgvCompras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompras.Size = new System.Drawing.Size(860, 310);
            this.dgvCompras.TabIndex = 0;
            this.dgvCompras.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompras_CellDoubleClick);
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelBotones.Controls.Add(this.btnCerrar);
            this.panelBotones.Controls.Add(this.btnExportar);
            this.panelBotones.Controls.Add(this.btnVerDetalle);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 520);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(900, 80);
            this.panelBotones.TabIndex = 4;
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
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.Location = new System.Drawing.Point(368, 20);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(180, 40);
            this.btnExportar.TabIndex = 1;
            this.btnExportar.Text = "EXPORTAR PDF";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnVerDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerDetalle.ForeColor = System.Drawing.Color.White;
            this.btnVerDetalle.Location = new System.Drawing.Point(27, 20);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(180, 40);
            this.btnVerDetalle.TabIndex = 0;
            this.btnVerDetalle.Text = "VER DETALLE";
            this.btnVerDetalle.UseVisualStyleBackColor = false;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            // 
            // FormHistorialCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelCompras);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelEstadisticas);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormHistorialCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial de Compras - Fresco Market";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.panelEstadisticas.ResumeLayout(false);
            this.panelEstadisticas.PerformLayout();
            this.panelCompras.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.Label lblFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Label lblFechaDesde;
        private System.Windows.Forms.Panel panelEstadisticas;
        private System.Windows.Forms.Label lblPromedioCompra;
        private System.Windows.Forms.Label lblCantidadCompras;
        private System.Windows.Forms.Label lblTotalCompras;
        private System.Windows.Forms.Panel panelCompras;
        private System.Windows.Forms.DataGridView dgvCompras;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnVerDetalle;
    }
}