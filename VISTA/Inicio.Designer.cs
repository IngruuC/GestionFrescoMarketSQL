namespace VISTA
{
    partial class Inicio
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRegistrarCliente = new System.Windows.Forms.Button();
            this.btnRegistroProducto = new System.Windows.Forms.Button();
            this.btnRealizarVenta = new System.Windows.Forms.Button();
            this.btnVentasTotales = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Controls.Add(this.pictureBox1);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(884, 100);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(323, 31);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(279, 37);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "FRESCO MARKET";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnRegistrarCliente
            // 
            this.btnRegistrarCliente.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRegistrarCliente.FlatAppearance.BorderSize = 0;
            this.btnRegistrarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarCliente.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarCliente.Image = null;
            this.btnRegistrarCliente.Location = new System.Drawing.Point(100, 150);
            this.btnRegistrarCliente.Name = "btnRegistrarCliente";
            this.btnRegistrarCliente.Size = new System.Drawing.Size(200, 150);
            this.btnRegistrarCliente.TabIndex = 1;
            this.btnRegistrarCliente.Text = "REGISTRAR\r\nCLIENTE";
            this.btnRegistrarCliente.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRegistrarCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRegistrarCliente.UseVisualStyleBackColor = false;
            this.btnRegistrarCliente.Click += new System.EventHandler(this.btnRegistrarCliente_Click);
            // 
            // btnRegistroProducto
            // 
            this.btnRegistroProducto.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRegistroProducto.FlatAppearance.BorderSize = 0;
            this.btnRegistroProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistroProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistroProducto.ForeColor = System.Drawing.Color.White;
            this.btnRegistroProducto.Image = null;
            this.btnRegistroProducto.Location = new System.Drawing.Point(342, 150);
            this.btnRegistroProducto.Name = "btnRegistroProducto";
            this.btnRegistroProducto.Size = new System.Drawing.Size(200, 150);
            this.btnRegistroProducto.TabIndex = 2;
            this.btnRegistroProducto.Text = "REGISTRAR\r\nPRODUCTO";
            this.btnRegistroProducto.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRegistroProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRegistroProducto.UseVisualStyleBackColor = false;
            this.btnRegistroProducto.Click += new System.EventHandler(this.btnRegistroProducto_Click);
            // 
            // btnRealizarVenta
            // 
            this.btnRealizarVenta.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnRealizarVenta.FlatAppearance.BorderSize = 0;
            this.btnRealizarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarVenta.ForeColor = System.Drawing.Color.White;
            this.btnRealizarVenta.Image = null; // ACA
            this.btnRealizarVenta.Location = new System.Drawing.Point(584, 150);
            this.btnRealizarVenta.Name = "btnRealizarVenta";
            this.btnRealizarVenta.Size = new System.Drawing.Size(200, 150);
            this.btnRealizarVenta.TabIndex = 3;
            this.btnRealizarVenta.Text = "REALIZAR\r\nVENTA";
            this.btnRealizarVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRealizarVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRealizarVenta.UseVisualStyleBackColor = false;
            this.btnRealizarVenta.Click += new System.EventHandler(this.btnRealizarVenta_Click);
            // 
            // btnVentasTotales
            // 
            this.btnVentasTotales.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnVentasTotales.FlatAppearance.BorderSize = 0;
            this.btnVentasTotales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentasTotales.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentasTotales.ForeColor = System.Drawing.Color.White;
            this.btnVentasTotales.Image = null; //ACA VER
            this.btnVentasTotales.Location = new System.Drawing.Point(342, 330);
            this.btnVentasTotales.Name = "btnVentasTotales";
            this.btnVentasTotales.Size = new System.Drawing.Size(200, 150);
            this.btnVentasTotales.TabIndex = 4;
            this.btnVentasTotales.Text = "VENTAS\r\nTOTALES";
            this.btnVentasTotales.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVentasTotales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVentasTotales.UseVisualStyleBackColor = false;
            this.btnVentasTotales.Click += new System.EventHandler(this.btnVentasTotales_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Crimson;
            this.btnSalir.Location = new System.Drawing.Point(771, 498);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(101, 41);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(884, 551);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnVentasTotales);
            this.Controls.Add(this.btnRealizarVenta);
            this.Controls.Add(this.btnRegistroProducto);
            this.Controls.Add(this.btnRegistrarCliente);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio - Fresco Market";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnRegistrarCliente;
        private System.Windows.Forms.Button btnRegistroProducto;
        private System.Windows.Forms.Button btnRealizarVenta;
        private System.Windows.Forms.Button btnVentasTotales;
        private System.Windows.Forms.Button btnSalir;
    }
}