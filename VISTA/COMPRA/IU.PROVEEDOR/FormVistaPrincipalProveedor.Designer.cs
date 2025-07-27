using System;
using System.Windows.Forms;

namespace VISTA.COMPRA
{
    partial class FormVistaPrincipalProveedor : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVistaPrincipalProveedor));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.btnMiPerfil = new System.Windows.Forms.Button();
            this.btnMisProductos = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblBienvenido = new System.Windows.Forms.Label();
            this.panelDashboard = new System.Windows.Forms.Panel();
            this.panelMasVendidos = new System.Windows.Forms.Panel();
            this.dgvProductosMasVendidos = new System.Windows.Forms.DataGridView();
            this.lblProductosMasVendidos = new System.Windows.Forms.Label();
            this.panelComprasRecientes = new System.Windows.Forms.Panel();
            this.dgvComprasRecientes = new System.Windows.Forms.DataGridView();
            this.lblComprasRecientes = new System.Windows.Forms.Label();
            this.panelEstadisticas = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelTotalVentas = new System.Windows.Forms.Panel();
            this.lblTotalVentas = new System.Windows.Forms.Label();
            this.lblTotalVentasTitulo = new System.Windows.Forms.Label();
            this.panelTotalProductos = new System.Windows.Forms.Panel();
            this.lblTotalProductos = new System.Windows.Forms.Label();
            this.lblTotalProductosTitulo = new System.Windows.Forms.Label();
            this.panelPedidosRecientes = new System.Windows.Forms.Panel();
            this.lblPedidosRecientes = new System.Windows.Forms.Label();
            this.lblPedidosRecientesTitulo = new System.Windows.Forms.Label();
            this.lblTituloDashboard = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelMenu.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelDashboard.SuspendLayout();
            this.panelMasVendidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosMasVendidos)).BeginInit();
            this.panelComprasRecientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprasRecientes)).BeginInit();
            this.panelEstadisticas.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelTotalVentas.SuspendLayout();
            this.panelTotalProductos.SuspendLayout();
            this.panelPedidosRecientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelMenu.Controls.Add(this.btnHistorial);
            this.panelMenu.Controls.Add(this.pictureBoxLogo);
            this.panelMenu.Controls.Add(this.btnMiPerfil);
            this.panelMenu.Controls.Add(this.btnMisProductos);
            this.panelMenu.Controls.Add(this.btnReportes);
            this.panelMenu.Controls.Add(this.btnCerrarSesion);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 114);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(225, 536);
            this.panelMenu.TabIndex = 0;
            // 
            // btnHistorial
            // 
            this.btnHistorial.FlatAppearance.BorderSize = 0;
            this.btnHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorial.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorial.ForeColor = System.Drawing.Color.White;
            this.btnHistorial.Location = new System.Drawing.Point(-8, 244);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(250, 50);
            this.btnHistorial.TabIndex = 6;
            this.btnHistorial.Text = "HISTORIAL";
            this.btnHistorial.UseVisualStyleBackColor = true;
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorial_Click);
            // 
            // btnMiPerfil
            // 
            this.btnMiPerfil.FlatAppearance.BorderSize = 0;
            this.btnMiPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMiPerfil.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMiPerfil.ForeColor = System.Drawing.Color.White;
            this.btnMiPerfil.Location = new System.Drawing.Point(-11, 150);
            this.btnMiPerfil.Name = "btnMiPerfil";
            this.btnMiPerfil.Size = new System.Drawing.Size(250, 50);
            this.btnMiPerfil.TabIndex = 0;
            this.btnMiPerfil.Text = "MI PERFIL";
            this.btnMiPerfil.UseVisualStyleBackColor = true;
            this.btnMiPerfil.Click += new System.EventHandler(this.btnMiPerfil_Click);
            // 
            // btnMisProductos
            // 
            this.btnMisProductos.FlatAppearance.BorderSize = 0;
            this.btnMisProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMisProductos.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMisProductos.ForeColor = System.Drawing.Color.White;
            this.btnMisProductos.Location = new System.Drawing.Point(-11, 200);
            this.btnMisProductos.Name = "btnMisProductos";
            this.btnMisProductos.Size = new System.Drawing.Size(250, 50);
            this.btnMisProductos.TabIndex = 1;
            this.btnMisProductos.Text = "MIS PRODUCTOS";
            this.btnMisProductos.UseVisualStyleBackColor = true;
            this.btnMisProductos.Click += new System.EventHandler(this.btnMisProductos_Click_1);
            // 
            // btnReportes
            // 
            this.btnReportes.FlatAppearance.BorderSize = 0;
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Location = new System.Drawing.Point(-11, 300);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(250, 50);
            this.btnReportes.TabIndex = 3;
            this.btnReportes.Text = "REPORTES";
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(-11, 486);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(250, 50);
            this.btnCerrarSesion.TabIndex = 4;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.pictureBox1);
            this.panelSuperior.Controls.Add(this.lblBienvenido);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1150, 114);
            this.panelSuperior.TabIndex = 1;
            // 
            // lblBienvenido
            // 
            this.lblBienvenido.AutoSize = true;
            this.lblBienvenido.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenido.ForeColor = System.Drawing.Color.White;
            this.lblBienvenido.Location = new System.Drawing.Point(256, 29);
            this.lblBienvenido.Name = "lblBienvenido";
            this.lblBienvenido.Size = new System.Drawing.Size(623, 55);
            this.lblBienvenido.TabIndex = 0;
            this.lblBienvenido.Text = "Bienvenido, [Razón Social]";
            // 
            // panelDashboard
            // 
            this.panelDashboard.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelDashboard.Controls.Add(this.panelMasVendidos);
            this.panelDashboard.Controls.Add(this.panelComprasRecientes);
            this.panelDashboard.Controls.Add(this.panelEstadisticas);
            this.panelDashboard.Controls.Add(this.lblTituloDashboard);
            this.panelDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDashboard.Location = new System.Drawing.Point(225, 114);
            this.panelDashboard.Name = "panelDashboard";
            this.panelDashboard.Padding = new System.Windows.Forms.Padding(20);
            this.panelDashboard.Size = new System.Drawing.Size(925, 536);
            this.panelDashboard.TabIndex = 2;
            // 
            // panelMasVendidos
            // 
            this.panelMasVendidos.BackColor = System.Drawing.Color.White;
            this.panelMasVendidos.Controls.Add(this.dgvProductosMasVendidos);
            this.panelMasVendidos.Controls.Add(this.lblProductosMasVendidos);
            this.panelMasVendidos.Location = new System.Drawing.Point(456, 180);
            this.panelMasVendidos.Name = "panelMasVendidos";
            this.panelMasVendidos.Size = new System.Drawing.Size(380, 350);
            this.panelMasVendidos.TabIndex = 3;
            // 
            // dgvProductosMasVendidos
            // 
            this.dgvProductosMasVendidos.AllowUserToAddRows = false;
            this.dgvProductosMasVendidos.AllowUserToDeleteRows = false;
            this.dgvProductosMasVendidos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductosMasVendidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductosMasVendidos.Location = new System.Drawing.Point(10, 40);
            this.dgvProductosMasVendidos.Name = "dgvProductosMasVendidos";
            this.dgvProductosMasVendidos.ReadOnly = true;
            this.dgvProductosMasVendidos.Size = new System.Drawing.Size(360, 300);
            this.dgvProductosMasVendidos.TabIndex = 1;
            // 
            // lblProductosMasVendidos
            // 
            this.lblProductosMasVendidos.AutoSize = true;
            this.lblProductosMasVendidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductosMasVendidos.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblProductosMasVendidos.Location = new System.Drawing.Point(10, 10);
            this.lblProductosMasVendidos.Name = "lblProductosMasVendidos";
            this.lblProductosMasVendidos.Size = new System.Drawing.Size(208, 20);
            this.lblProductosMasVendidos.TabIndex = 0;
            this.lblProductosMasVendidos.Text = "Productos Más Vendidos";
            // 
            // panelComprasRecientes
            // 
            this.panelComprasRecientes.BackColor = System.Drawing.Color.White;
            this.panelComprasRecientes.Controls.Add(this.dgvComprasRecientes);
            this.panelComprasRecientes.Controls.Add(this.lblComprasRecientes);
            this.panelComprasRecientes.Location = new System.Drawing.Point(36, 180);
            this.panelComprasRecientes.Name = "panelComprasRecientes";
            this.panelComprasRecientes.Size = new System.Drawing.Size(380, 350);
            this.panelComprasRecientes.TabIndex = 2;
            // 
            // dgvComprasRecientes
            // 
            this.dgvComprasRecientes.AllowUserToAddRows = false;
            this.dgvComprasRecientes.AllowUserToDeleteRows = false;
            this.dgvComprasRecientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvComprasRecientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComprasRecientes.Location = new System.Drawing.Point(10, 40);
            this.dgvComprasRecientes.Name = "dgvComprasRecientes";
            this.dgvComprasRecientes.ReadOnly = true;
            this.dgvComprasRecientes.Size = new System.Drawing.Size(360, 300);
            this.dgvComprasRecientes.TabIndex = 1;
            // 
            // lblComprasRecientes
            // 
            this.lblComprasRecientes.AutoSize = true;
            this.lblComprasRecientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComprasRecientes.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblComprasRecientes.Location = new System.Drawing.Point(10, 10);
            this.lblComprasRecientes.Name = "lblComprasRecientes";
            this.lblComprasRecientes.Size = new System.Drawing.Size(166, 20);
            this.lblComprasRecientes.TabIndex = 0;
            this.lblComprasRecientes.Text = "Compras Recientes";
            // 
            // panelEstadisticas
            // 
            this.panelEstadisticas.Controls.Add(this.tableLayoutPanel1);
            this.panelEstadisticas.Location = new System.Drawing.Point(36, 60);
            this.panelEstadisticas.Name = "panelEstadisticas";
            this.panelEstadisticas.Size = new System.Drawing.Size(800, 100);
            this.panelEstadisticas.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panelTotalVentas, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelTotalProductos, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelPedidosRecientes, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelTotalVentas
            // 
            this.panelTotalVentas.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelTotalVentas.Controls.Add(this.lblTotalVentas);
            this.panelTotalVentas.Controls.Add(this.lblTotalVentasTitulo);
            this.panelTotalVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTotalVentas.Location = new System.Drawing.Point(10, 10);
            this.panelTotalVentas.Margin = new System.Windows.Forms.Padding(10);
            this.panelTotalVentas.Name = "panelTotalVentas";
            this.panelTotalVentas.Size = new System.Drawing.Size(246, 80);
            this.panelTotalVentas.TabIndex = 0;
            // 
            // lblTotalVentas
            // 
            this.lblTotalVentas.AutoSize = true;
            this.lblTotalVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVentas.ForeColor = System.Drawing.Color.White;
            this.lblTotalVentas.Location = new System.Drawing.Point(10, 35);
            this.lblTotalVentas.Name = "lblTotalVentas";
            this.lblTotalVentas.Size = new System.Drawing.Size(71, 25);
            this.lblTotalVentas.TabIndex = 1;
            this.lblTotalVentas.Text = "$0.00";
            // 
            // lblTotalVentasTitulo
            // 
            this.lblTotalVentasTitulo.AutoSize = true;
            this.lblTotalVentasTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVentasTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTotalVentasTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblTotalVentasTitulo.Name = "lblTotalVentasTitulo";
            this.lblTotalVentasTitulo.Size = new System.Drawing.Size(99, 16);
            this.lblTotalVentasTitulo.TabIndex = 0;
            this.lblTotalVentasTitulo.Text = "Total Ventas:";
            // 
            // panelTotalProductos
            // 
            this.panelTotalProductos.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelTotalProductos.Controls.Add(this.lblTotalProductos);
            this.panelTotalProductos.Controls.Add(this.lblTotalProductosTitulo);
            this.panelTotalProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTotalProductos.Location = new System.Drawing.Point(276, 10);
            this.panelTotalProductos.Margin = new System.Windows.Forms.Padding(10);
            this.panelTotalProductos.Name = "panelTotalProductos";
            this.panelTotalProductos.Size = new System.Drawing.Size(246, 80);
            this.panelTotalProductos.TabIndex = 1;
            // 
            // lblTotalProductos
            // 
            this.lblTotalProductos.AutoSize = true;
            this.lblTotalProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProductos.ForeColor = System.Drawing.Color.White;
            this.lblTotalProductos.Location = new System.Drawing.Point(10, 35);
            this.lblTotalProductos.Name = "lblTotalProductos";
            this.lblTotalProductos.Size = new System.Drawing.Size(25, 25);
            this.lblTotalProductos.TabIndex = 1;
            this.lblTotalProductos.Text = "0";
            // 
            // lblTotalProductosTitulo
            // 
            this.lblTotalProductosTitulo.AutoSize = true;
            this.lblTotalProductosTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProductosTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTotalProductosTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblTotalProductosTitulo.Name = "lblTotalProductosTitulo";
            this.lblTotalProductosTitulo.Size = new System.Drawing.Size(121, 16);
            this.lblTotalProductosTitulo.TabIndex = 0;
            this.lblTotalProductosTitulo.Text = "Total Productos:";
            // 
            // panelPedidosRecientes
            // 
            this.panelPedidosRecientes.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelPedidosRecientes.Controls.Add(this.lblPedidosRecientes);
            this.panelPedidosRecientes.Controls.Add(this.lblPedidosRecientesTitulo);
            this.panelPedidosRecientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPedidosRecientes.Location = new System.Drawing.Point(542, 10);
            this.panelPedidosRecientes.Margin = new System.Windows.Forms.Padding(10);
            this.panelPedidosRecientes.Name = "panelPedidosRecientes";
            this.panelPedidosRecientes.Size = new System.Drawing.Size(248, 80);
            this.panelPedidosRecientes.TabIndex = 2;
            // 
            // lblPedidosRecientes
            // 
            this.lblPedidosRecientes.AutoSize = true;
            this.lblPedidosRecientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidosRecientes.ForeColor = System.Drawing.Color.White;
            this.lblPedidosRecientes.Location = new System.Drawing.Point(10, 35);
            this.lblPedidosRecientes.Name = "lblPedidosRecientes";
            this.lblPedidosRecientes.Size = new System.Drawing.Size(25, 25);
            this.lblPedidosRecientes.TabIndex = 1;
            this.lblPedidosRecientes.Text = "0";
            // 
            // lblPedidosRecientesTitulo
            // 
            this.lblPedidosRecientesTitulo.AutoSize = true;
            this.lblPedidosRecientesTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidosRecientesTitulo.ForeColor = System.Drawing.Color.White;
            this.lblPedidosRecientesTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblPedidosRecientesTitulo.Name = "lblPedidosRecientesTitulo";
            this.lblPedidosRecientesTitulo.Size = new System.Drawing.Size(143, 16);
            this.lblPedidosRecientesTitulo.TabIndex = 0;
            this.lblPedidosRecientesTitulo.Text = "Pedidos Recientes:";
            // 
            // lblTituloDashboard
            // 
            this.lblTituloDashboard.AutoSize = true;
            this.lblTituloDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloDashboard.Location = new System.Drawing.Point(36, 20);
            this.lblTituloDashboard.Name = "lblTituloDashboard";
            this.lblTituloDashboard.Size = new System.Drawing.Size(240, 25);
            this.lblTituloDashboard.TabIndex = 0;
            this.lblTituloDashboard.Text = "Panel de Estadísticas";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::VISTA.Properties.Resources.icons8_product_50;
            this.pictureBoxLogo.Location = new System.Drawing.Point(64, 30);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 5;
            this.pictureBoxLogo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(64, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 131);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FormVistaPrincipalProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 650);
            this.Controls.Add(this.panelDashboard);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormVistaPrincipalProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fresco Market - Portal de Proveedor";
            this.panelMenu.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelDashboard.ResumeLayout(false);
            this.panelDashboard.PerformLayout();
            this.panelMasVendidos.ResumeLayout(false);
            this.panelMasVendidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosMasVendidos)).EndInit();
            this.panelComprasRecientes.ResumeLayout(false);
            this.panelComprasRecientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComprasRecientes)).EndInit();
            this.panelEstadisticas.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelTotalVentas.ResumeLayout(false);
            this.panelTotalVentas.PerformLayout();
            this.panelTotalProductos.ResumeLayout(false);
            this.panelTotalProductos.PerformLayout();
            this.panelPedidosRecientes.ResumeLayout(false);
            this.panelPedidosRecientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button btnMiPerfil;
        private System.Windows.Forms.Button btnMisProductos;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblBienvenido;
        private System.Windows.Forms.Panel panelDashboard;
        private System.Windows.Forms.Label lblTituloDashboard;
        private System.Windows.Forms.Panel panelEstadisticas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelTotalVentas;
        private System.Windows.Forms.Label lblTotalVentas;
        private System.Windows.Forms.Label lblTotalVentasTitulo;
        private System.Windows.Forms.Panel panelTotalProductos;
        private System.Windows.Forms.Label lblTotalProductos;
        private System.Windows.Forms.Label lblTotalProductosTitulo;
        private System.Windows.Forms.Panel panelPedidosRecientes;
        private System.Windows.Forms.Label lblPedidosRecientes;
        private System.Windows.Forms.Label lblPedidosRecientesTitulo;
        private System.Windows.Forms.Panel panelComprasRecientes;
        private System.Windows.Forms.DataGridView dgvComprasRecientes;
        private System.Windows.Forms.Label lblComprasRecientes;
        private System.Windows.Forms.Panel panelMasVendidos;
        private System.Windows.Forms.DataGridView dgvProductosMasVendidos;
        private System.Windows.Forms.Label lblProductosMasVendidos;
        private Button btnHistorial;
        private PictureBox pictureBox1;
    }
}