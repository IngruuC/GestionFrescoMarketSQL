namespace VISTA
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.lblFrescoMarket = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnComprasTotales = new System.Windows.Forms.Button();
            this.btnRealizarVenta = new System.Windows.Forms.Button();
            this.btnRegistrarCliente = new System.Windows.Forms.Button();
            this.btnRegistrarProducto = new System.Windows.Forms.Button();
            this.btnRealizarCompra = new System.Windows.Forms.Button();
            this.btnVentasTotales = new System.Windows.Forms.Button();
            this.btnRegistrarProveedor = new System.Windows.Forms.Button();
            this.AuditoriasSesion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.AutoScroll = true;
            this.panelMenu.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelMenu.Controls.Add(this.btnComprasTotales);
            this.panelMenu.Controls.Add(this.btnRealizarVenta);
            this.panelMenu.Controls.Add(this.btnRegistrarCliente);
            this.panelMenu.Controls.Add(this.btnRegistrarProducto);
            this.panelMenu.Controls.Add(this.btnRealizarCompra);
            this.panelMenu.Controls.Add(this.btnVentasTotales);
            this.panelMenu.Controls.Add(this.btnRegistrarProveedor);
            this.panelMenu.Location = new System.Drawing.Point(1, 109);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(225, 632);
            this.panelMenu.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panel3.Controls.Add(this.AuditoriasSesion);
            this.panel3.Controls.Add(this.btnSalir);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(207, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1114, 114);
            this.panel3.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MingLiU-ExtB", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(377, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 96);
            this.label2.TabIndex = 0;
            this.label2.Text = "Menú";
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.lblFrescoMarket);
            this.panelContenedor.Location = new System.Drawing.Point(232, 120);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1089, 610);
            this.panelContenedor.TabIndex = 13;
            this.panelContenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenedor_Paint);
            // 
            // lblFrescoMarket
            // 
            this.lblFrescoMarket.AutoSize = true;
            this.lblFrescoMarket.Font = new System.Drawing.Font("MingLiU-ExtB", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrescoMarket.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFrescoMarket.Location = new System.Drawing.Point(221, 202);
            this.lblFrescoMarket.Name = "lblFrescoMarket";
            this.lblFrescoMarket.Size = new System.Drawing.Size(628, 96);
            this.lblFrescoMarket.TabIndex = 0;
            this.lblFrescoMarket.Text = "FrescoMarket";
            this.lblFrescoMarket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 114);
            this.panel1.TabIndex = 14;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(45, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 131);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnComprasTotales
            // 
            this.btnComprasTotales.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnComprasTotales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComprasTotales.Image = global::VISTA.Properties.Resources.icons8_supplier_60;
            this.btnComprasTotales.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnComprasTotales.Location = new System.Drawing.Point(7, 571);
            this.btnComprasTotales.Name = "btnComprasTotales";
            this.btnComprasTotales.Size = new System.Drawing.Size(194, 87);
            this.btnComprasTotales.TabIndex = 10;
            this.btnComprasTotales.Text = "COMPRAS TOTALES";
            this.btnComprasTotales.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnComprasTotales.UseVisualStyleBackColor = false;
            this.btnComprasTotales.Click += new System.EventHandler(this.btnComprasTotales_Click);
            // 
            // btnRealizarVenta
            // 
            this.btnRealizarVenta.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRealizarVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarVenta.Image = global::VISTA.Properties.Resources.icons8_upselling_641;
            this.btnRealizarVenta.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRealizarVenta.Location = new System.Drawing.Point(6, 200);
            this.btnRealizarVenta.Name = "btnRealizarVenta";
            this.btnRealizarVenta.Size = new System.Drawing.Size(195, 86);
            this.btnRealizarVenta.TabIndex = 13;
            this.btnRealizarVenta.Text = "NUEVA VENTA";
            this.btnRealizarVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRealizarVenta.UseVisualStyleBackColor = false;
            this.btnRealizarVenta.Click += new System.EventHandler(this.btnRealizarVenta_Click);
            // 
            // btnRegistrarCliente
            // 
            this.btnRegistrarCliente.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRegistrarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCliente.Image = global::VISTA.Properties.Resources.icons8_clients_64;
            this.btnRegistrarCliente.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRegistrarCliente.Location = new System.Drawing.Point(5, 18);
            this.btnRegistrarCliente.Name = "btnRegistrarCliente";
            this.btnRegistrarCliente.Size = new System.Drawing.Size(195, 87);
            this.btnRegistrarCliente.TabIndex = 4;
            this.btnRegistrarCliente.Text = "CLIENTES";
            this.btnRegistrarCliente.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRegistrarCliente.UseVisualStyleBackColor = false;
            this.btnRegistrarCliente.Click += new System.EventHandler(this.btnRegistrarCliente_Click);
            // 
            // btnRegistrarProducto
            // 
            this.btnRegistrarProducto.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRegistrarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarProducto.Image = global::VISTA.Properties.Resources.icons8_product_64;
            this.btnRegistrarProducto.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRegistrarProducto.Location = new System.Drawing.Point(6, 111);
            this.btnRegistrarProducto.Name = "btnRegistrarProducto";
            this.btnRegistrarProducto.Size = new System.Drawing.Size(195, 83);
            this.btnRegistrarProducto.TabIndex = 5;
            this.btnRegistrarProducto.Text = "PRODUCTOS";
            this.btnRegistrarProducto.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRegistrarProducto.UseVisualStyleBackColor = false;
            this.btnRegistrarProducto.Click += new System.EventHandler(this.btnRegistrarProducto_Click);
            // 
            // btnRealizarCompra
            // 
            this.btnRealizarCompra.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRealizarCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarCompra.Image = global::VISTA.Properties.Resources.icons8_inventory_64__1_2;
            this.btnRealizarCompra.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRealizarCompra.Location = new System.Drawing.Point(7, 478);
            this.btnRealizarCompra.Name = "btnRealizarCompra";
            this.btnRealizarCompra.Size = new System.Drawing.Size(194, 87);
            this.btnRealizarCompra.TabIndex = 9;
            this.btnRealizarCompra.Text = "NUEVA COMPRA";
            this.btnRealizarCompra.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRealizarCompra.UseVisualStyleBackColor = false;
            this.btnRealizarCompra.Click += new System.EventHandler(this.btnRealizarCompra_Click);
            // 
            // btnVentasTotales
            // 
            this.btnVentasTotales.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnVentasTotales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentasTotales.Image = global::VISTA.Properties.Resources.icons8_low_sales_64;
            this.btnVentasTotales.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVentasTotales.Location = new System.Drawing.Point(7, 292);
            this.btnVentasTotales.Name = "btnVentasTotales";
            this.btnVentasTotales.Size = new System.Drawing.Size(194, 87);
            this.btnVentasTotales.TabIndex = 7;
            this.btnVentasTotales.Text = "VENTAS TOTALES";
            this.btnVentasTotales.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVentasTotales.UseVisualStyleBackColor = false;
            this.btnVentasTotales.Click += new System.EventHandler(this.btnVentasTotales_Click);
            // 
            // btnRegistrarProveedor
            // 
            this.btnRegistrarProveedor.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRegistrarProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarProveedor.Image = global::VISTA.Properties.Resources.icons8_supplier_64;
            this.btnRegistrarProveedor.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRegistrarProveedor.Location = new System.Drawing.Point(7, 385);
            this.btnRegistrarProveedor.Name = "btnRegistrarProveedor";
            this.btnRegistrarProveedor.Size = new System.Drawing.Size(194, 87);
            this.btnRegistrarProveedor.TabIndex = 8;
            this.btnRegistrarProveedor.Text = "PROVEEDORES";
            this.btnRegistrarProveedor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRegistrarProveedor.UseVisualStyleBackColor = false;
            this.btnRegistrarProveedor.Click += new System.EventHandler(this.btnRegistrarProveedor_Click);
            // 
            // AuditoriasSesion
            // 
            this.AuditoriasSesion.BackColor = System.Drawing.Color.Transparent;
            this.AuditoriasSesion.FlatAppearance.BorderSize = 0;
            this.AuditoriasSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AuditoriasSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuditoriasSesion.ForeColor = System.Drawing.Color.Transparent;
            this.AuditoriasSesion.Image = ((System.Drawing.Image)(resources.GetObject("AuditoriasSesion.Image")));
            this.AuditoriasSesion.Location = new System.Drawing.Point(994, 65);
            this.AuditoriasSesion.Name = "AuditoriasSesion";
            this.AuditoriasSesion.Size = new System.Drawing.Size(63, 49);
            this.AuditoriasSesion.TabIndex = 8;
            this.AuditoriasSesion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AuditoriasSesion.UseVisualStyleBackColor = false;
            this.AuditoriasSesion.Click += new System.EventHandler(this.AuditoriasSesion_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Transparent;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Transparent;
            this.btnSalir.Image = global::VISTA.Properties.Resources.icons8_salida;
            this.btnSalir.Location = new System.Drawing.Point(1051, 72);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(63, 42);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1320, 742);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panel3);
            this.Name = "FormPrincipal";
            this.Text = "Menu";
            this.panelMenu.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            this.panelContenedor.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnRegistrarCliente;
        private System.Windows.Forms.Button btnRegistrarProducto;
        private System.Windows.Forms.Button btnVentasTotales;
        private System.Windows.Forms.Button btnRegistrarProveedor;
        private System.Windows.Forms.Button btnRealizarCompra;
        private System.Windows.Forms.Button btnComprasTotales;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRealizarVenta;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFrescoMarket;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button AuditoriasSesion;
    }
}