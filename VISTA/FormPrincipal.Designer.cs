using System;
using System.Drawing;
using System.Windows.Forms;
using CONTROLADORA;
using ENTIDADES;

namespace VISTA
{
    partial class FormPrincipal
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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabVentas = new System.Windows.Forms.TabPage();
            this.btnRegistrarCliente = new System.Windows.Forms.Button();
            this.btnRegistrarProducto = new System.Windows.Forms.Button();
            this.btnNuevaVenta = new System.Windows.Forms.Button();
            this.btnVentasTotales = new System.Windows.Forms.Button();
            this.btnReportesVentas = new System.Windows.Forms.Button();
            this.tabCompras = new System.Windows.Forms.TabPage();
            this.btnRegistrarProveedor = new System.Windows.Forms.Button();
            this.btnNuevaCompra = new System.Windows.Forms.Button();
            this.btnComprasTotales = new System.Windows.Forms.Button();
            this.btnReportesCompras = new System.Windows.Forms.Button();
            this.lblVentasHoy = new System.Windows.Forms.Label();
            this.lblComprasHoy = new System.Windows.Forms.Label();
            this.panelInferior = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabVentas.SuspendLayout();
            this.tabCompras.SuspendLayout();
            this.panelInferior.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(857, 69);
            this.panelSuperior.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(77, 17);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(233, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "FRESCO MARKET";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabVentas);
            this.tabControl.Controls.Add(this.tabCompras);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tabControl.Location = new System.Drawing.Point(0, 69);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(857, 408);
            this.tabControl.TabIndex = 0;
            // 
            // tabVentas
            // 
            this.tabVentas.BackColor = System.Drawing.Color.White;
            this.tabVentas.Controls.Add(this.btnRegistrarCliente);
            this.tabVentas.Controls.Add(this.btnRegistrarProducto);
            this.tabVentas.Controls.Add(this.btnNuevaVenta);
            this.tabVentas.Controls.Add(this.btnVentasTotales);
            this.tabVentas.Controls.Add(this.btnReportesVentas);
            this.tabVentas.Location = new System.Drawing.Point(4, 29);
            this.tabVentas.Name = "tabVentas";
            this.tabVentas.Padding = new System.Windows.Forms.Padding(17);
            this.tabVentas.Size = new System.Drawing.Size(849, 375);
            this.tabVentas.TabIndex = 0;
            this.tabVentas.Text = "VENTAS";
            // 
            // btnRegistrarCliente
            // 
            this.btnRegistrarCliente.Location = new System.Drawing.Point(37, 26);
            this.btnRegistrarCliente.Name = "btnRegistrarCliente";
            this.btnRegistrarCliente.Size = new System.Drawing.Size(171, 87);
            this.btnRegistrarCliente.TabIndex = 0;
            this.btnRegistrarCliente.Text = "Registrar\nCliente";
            // 
            // btnRegistrarProducto
            // 
            this.btnRegistrarProducto.Location = new System.Drawing.Point(214, 26);
            this.btnRegistrarProducto.Name = "btnRegistrarProducto";
            this.btnRegistrarProducto.Size = new System.Drawing.Size(171, 87);
            this.btnRegistrarProducto.TabIndex = 1;
            this.btnRegistrarProducto.Text = "Registrar\nProducto";
            // 
            // btnNuevaVenta
            // 
            this.btnNuevaVenta.Location = new System.Drawing.Point(403, 26);
            this.btnNuevaVenta.Name = "btnNuevaVenta";
            this.btnNuevaVenta.Size = new System.Drawing.Size(171, 87);
            this.btnNuevaVenta.TabIndex = 2;
            this.btnNuevaVenta.Text = "Nueva\nVenta";
            // 
            // btnVentasTotales
            // 
            this.btnVentasTotales.Location = new System.Drawing.Point(120, 130);
            this.btnVentasTotales.Name = "btnVentasTotales";
            this.btnVentasTotales.Size = new System.Drawing.Size(171, 87);
            this.btnVentasTotales.TabIndex = 3;
            this.btnVentasTotales.Text = "Ventas\nTotales";
            // 
            // btnReportesVentas
            // 
            this.btnReportesVentas.Location = new System.Drawing.Point(309, 130);
            this.btnReportesVentas.Name = "btnReportesVentas";
            this.btnReportesVentas.Size = new System.Drawing.Size(171, 87);
            this.btnReportesVentas.TabIndex = 4;
            this.btnReportesVentas.Text = "Reportes de\nVentas";
            // 
            // tabCompras
            // 
            this.tabCompras.BackColor = System.Drawing.Color.White;
            this.tabCompras.Controls.Add(this.btnRegistrarProveedor);
            this.tabCompras.Controls.Add(this.btnNuevaCompra);
            this.tabCompras.Controls.Add(this.btnComprasTotales);
            this.tabCompras.Controls.Add(this.btnReportesCompras);
            this.tabCompras.Location = new System.Drawing.Point(4, 29);
            this.tabCompras.Name = "tabCompras";
            this.tabCompras.Padding = new System.Windows.Forms.Padding(17);
            this.tabCompras.Size = new System.Drawing.Size(849, 375);
            this.tabCompras.TabIndex = 1;
            this.tabCompras.Text = "COMPRAS";
            // 
            // btnRegistrarProveedor
            // 
            this.btnRegistrarProveedor.Location = new System.Drawing.Point(26, 26);
            this.btnRegistrarProveedor.Name = "btnRegistrarProveedor";
            this.btnRegistrarProveedor.Size = new System.Drawing.Size(171, 87);
            this.btnRegistrarProveedor.TabIndex = 0;
            this.btnRegistrarProveedor.Text = "Registrar\nProveedor";
            // 
            // btnNuevaCompra
            // 
            this.btnNuevaCompra.Location = new System.Drawing.Point(214, 26);
            this.btnNuevaCompra.Name = "btnNuevaCompra";
            this.btnNuevaCompra.Size = new System.Drawing.Size(171, 87);
            this.btnNuevaCompra.TabIndex = 1;
            this.btnNuevaCompra.Text = "Nueva\nCompra";
            // 
            // btnComprasTotales
            // 
            this.btnComprasTotales.Location = new System.Drawing.Point(403, 26);
            this.btnComprasTotales.Name = "btnComprasTotales";
            this.btnComprasTotales.Size = new System.Drawing.Size(171, 87);
            this.btnComprasTotales.TabIndex = 2;
            this.btnComprasTotales.Text = "Compras\nTotales";
            // 
            // btnReportesCompras
            // 
            this.btnReportesCompras.Location = new System.Drawing.Point(214, 130);
            this.btnReportesCompras.Name = "btnReportesCompras";
            this.btnReportesCompras.Size = new System.Drawing.Size(171, 87);
            this.btnReportesCompras.TabIndex = 3;
            this.btnReportesCompras.Text = "Reportes de\nCompras";
            // 
            // lblVentasHoy
            // 
            this.lblVentasHoy.Location = new System.Drawing.Point(0, 0);
            this.lblVentasHoy.Name = "lblVentasHoy";
            this.lblVentasHoy.Size = new System.Drawing.Size(86, 20);
            this.lblVentasHoy.TabIndex = 1;
            // 
            // lblComprasHoy
            // 
            this.lblComprasHoy.Location = new System.Drawing.Point(0, 0);
            this.lblComprasHoy.Name = "lblComprasHoy";
            this.lblComprasHoy.Size = new System.Drawing.Size(86, 20);
            this.lblComprasHoy.TabIndex = 2;
            // 
            // panelInferior
            // 
            this.panelInferior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelInferior.Controls.Add(this.btnSalir);
            this.panelInferior.Controls.Add(this.lblVentasHoy);
            this.panelInferior.Controls.Add(this.lblComprasHoy);
            this.panelInferior.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInferior.Location = new System.Drawing.Point(0, 477);
            this.panelInferior.Name = "panelInferior";
            this.panelInferior.Size = new System.Drawing.Size(857, 43);
            this.panelInferior.TabIndex = 2;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(737, 7);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(86, 30);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 520);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.panelInferior);
            this.MinimumSize = new System.Drawing.Size(859, 525);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fresco Market - Sistema de Gestión";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabVentas.ResumeLayout(false);
            this.tabCompras.ResumeLayout(false);
            this.panelInferior.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabVentas;
        private System.Windows.Forms.TabPage tabCompras;
        private System.Windows.Forms.Panel panelInferior;
        private System.Windows.Forms.Button btnRegistrarCliente;
        private System.Windows.Forms.Button btnRegistrarProducto;
        private System.Windows.Forms.Button btnNuevaVenta;
        private System.Windows.Forms.Button btnVentasTotales;
        private System.Windows.Forms.Button btnReportesVentas;
        private System.Windows.Forms.Button btnRegistrarProveedor;
        private System.Windows.Forms.Button btnNuevaCompra;
        private System.Windows.Forms.Button btnComprasTotales;
        private System.Windows.Forms.Button btnReportesCompras;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblVentasHoy;
        private System.Windows.Forms.Label lblComprasHoy;
    }
}