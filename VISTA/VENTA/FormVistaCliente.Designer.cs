namespace VISTA
{
    partial class FormVistaCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVistaCliente));

            // Panel Superior
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();

            // Información del Cliente
            this.groupBoxInfoCliente = new System.Windows.Forms.GroupBox();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();

            // Historial de Compras
            this.groupBoxHistorialCompras = new System.Windows.Forms.GroupBox();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.lblTotalCompras = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();

            // Botones
            this.btnVerDetalleCompra = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();

            // Panel Superior
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxInfoCliente.SuspendLayout();
            this.groupBoxHistorialCompras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.SuspendLayout();

            // panelSuperior
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.pictureBox1);
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1084, 70);

            // pictureBox1
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(63, 21);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "MI PERFIL DE CLIENTE";

            // groupBoxInfoCliente
            this.groupBoxInfoCliente.Controls.Add(this.lblNombreCliente);
            this.groupBoxInfoCliente.Controls.Add(this.lblDocumento);
            this.groupBoxInfoCliente.Controls.Add(this.lblDireccion);
            this.groupBoxInfoCliente.Location = new System.Drawing.Point(12, 88);
            this.groupBoxInfoCliente.Name = "groupBoxInfoCliente";
            this.groupBoxInfoCliente.Size = new System.Drawing.Size(1060, 100);
            this.groupBoxInfoCliente.TabIndex = 1;
            this.groupBoxInfoCliente.TabStop = false;
            this.groupBoxInfoCliente.Text = "INFORMACIÓN PERSONAL";

            // lblNombreCliente
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Location = new System.Drawing.Point(20, 30);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblNombreCliente.Text = "Nombre del Cliente";

            // lblDocumento
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Location = new System.Drawing.Point(20, 60);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Text = "DNI:";

            // lblDireccion
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(400, 60);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Text = "Dirección:";

            // groupBoxHistorialCompras
            this.groupBoxHistorialCompras.Controls.Add(this.dgvCompras);
            this.groupBoxHistorialCompras.Controls.Add(this.lblTotalCompras);
            this.groupBoxHistorialCompras.Controls.Add(this.lblMontoTotal);
            this.groupBoxHistorialCompras.Controls.Add(this.btnVerDetalleCompra);
            this.groupBoxHistorialCompras.Location = new System.Drawing.Point(12, 200);
            this.groupBoxHistorialCompras.Name = "groupBoxHistorialCompras";
            this.groupBoxHistorialCompras.Size = new System.Drawing.Size(1060, 350);
            this.groupBoxHistorialCompras.TabIndex = 2;
            this.groupBoxHistorialCompras.TabStop = false;
            this.groupBoxHistorialCompras.Text = "HISTORIAL DE COMPRAS";

            // dgvCompras
            this.dgvCompras.Location = new System.Drawing.Point(20, 50);
            this.dgvCompras.Name = "dgvCompras";
            this.dgvCompras.Size = new System.Drawing.Size(1020, 200);
            this.dgvCompras.AllowUserToAddRows = false;
            this.dgvCompras.ReadOnly = true;
            this.dgvCompras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // lblTotalCompras
            this.lblTotalCompras.AutoSize = true;
            this.lblTotalCompras.Location = new System.Drawing.Point(20, 260);
            this.lblTotalCompras.Name = "lblTotalCompras";
            this.lblTotalCompras.Text = "Total Compras: 0";

            // lblMontoTotal
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.Location = new System.Drawing.Point(200, 260);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Text = "Monto Total: $0.00";

            // btnVerDetalleCompra
            this.btnVerDetalleCompra.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnVerDetalleCompra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerDetalleCompra.Location = new System.Drawing.Point(20, 290);
            this.btnVerDetalleCompra.Name = "btnVerDetalleCompra";
            this.btnVerDetalleCompra.Size = new System.Drawing.Size(150, 35);
            this.btnVerDetalleCompra.Text = "VER DETALLE";
            this.btnVerDetalleCompra.UseVisualStyleBackColor = false;
            //this.btnVerDetalleCompra.Click += new System.EventHandler(this.btnVerDetalleCompra_Click);

            // btnCerrar
            this.btnCerrar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.Red;
            this.btnCerrar.Location = new System.Drawing.Point(920, 560);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(150, 35);
            this.btnCerrar.Text = "CERRAR";
            this.btnCerrar.UseVisualStyleBackColor = false;
           // this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);

            // FormVistaCliente
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1084, 602);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.groupBoxHistorialCompras);
            this.Controls.Add(this.groupBoxInfoCliente);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormVistaCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vista Cliente - Fresco Market";

            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxInfoCliente.ResumeLayout(false);
            this.groupBoxInfoCliente.PerformLayout();
            this.groupBoxHistorialCompras.ResumeLayout(false);
            this.groupBoxHistorialCompras.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxInfoCliente;
        private System.Windows.Forms.Label lblNombreCliente;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.GroupBox groupBoxHistorialCompras;
        private System.Windows.Forms.DataGridView dgvCompras;
        private System.Windows.Forms.Label lblTotalCompras;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Button btnVerDetalleCompra;
        private System.Windows.Forms.Button btnCerrar;
    }
} 
#endregion