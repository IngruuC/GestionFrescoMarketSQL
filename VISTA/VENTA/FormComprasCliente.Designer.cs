namespace VISTA
{
    partial class FormComprasCliente
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
            this.groupBoxFiltro = new System.Windows.Forms.GroupBox();
            this.lblFechaDesde = new System.Windows.Forms.Label();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.lblFechaHasta = new System.Windows.Forms.Label();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrarCompras = new System.Windows.Forms.Button();
            this.btnLimpiarFiltro = new System.Windows.Forms.Button();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.lblTotalCompras = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.btnVerDetalleCompra = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.groupBoxFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(800, 70);
            this.panelSuperior.TabIndex = 6;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(100, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(172, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "MIS COMPRAS";
            // 
            // groupBoxFiltro
            // 
            this.groupBoxFiltro.Controls.Add(this.lblFechaDesde);
            this.groupBoxFiltro.Controls.Add(this.dtpFechaDesde);
            this.groupBoxFiltro.Controls.Add(this.lblFechaHasta);
            this.groupBoxFiltro.Controls.Add(this.dtpFechaHasta);
            this.groupBoxFiltro.Controls.Add(this.btnFiltrarCompras);
            this.groupBoxFiltro.Controls.Add(this.btnLimpiarFiltro);
            this.groupBoxFiltro.Location = new System.Drawing.Point(50, 100);
            this.groupBoxFiltro.Name = "groupBoxFiltro";
            this.groupBoxFiltro.Size = new System.Drawing.Size(700, 100);
            this.groupBoxFiltro.TabIndex = 5;
            this.groupBoxFiltro.TabStop = false;
            this.groupBoxFiltro.Text = "Filtrar Compras";
            // 
            // lblFechaDesde
            // 
            this.lblFechaDesde.Location = new System.Drawing.Point(20, 30);
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Size = new System.Drawing.Size(100, 23);
            this.lblFechaDesde.TabIndex = 0;
            this.lblFechaDesde.Text = "Desde:";
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Location = new System.Drawing.Point(20, 50);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaDesde.TabIndex = 1;
            this.dtpFechaDesde.Value = new System.DateTime(2025, 1, 24, 0, 0, 0, 0);
            // 
            // lblFechaHasta
            // 
            this.lblFechaHasta.Location = new System.Drawing.Point(250, 30);
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Size = new System.Drawing.Size(100, 23);
            this.lblFechaHasta.TabIndex = 2;
            this.lblFechaHasta.Text = "Hasta:";
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Location = new System.Drawing.Point(250, 50);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaHasta.TabIndex = 3;
            this.dtpFechaHasta.Value = new System.DateTime(2025, 2, 24, 0, 0, 0, 0);
            // 
            // btnFiltrarCompras
            // 
            this.btnFiltrarCompras.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnFiltrarCompras.ForeColor = System.Drawing.Color.White;
            this.btnFiltrarCompras.Location = new System.Drawing.Point(480, 40);
            this.btnFiltrarCompras.Name = "btnFiltrarCompras";
            this.btnFiltrarCompras.Size = new System.Drawing.Size(100, 35);
            this.btnFiltrarCompras.TabIndex = 4;
            this.btnFiltrarCompras.Text = "Filtrar";
            this.btnFiltrarCompras.UseVisualStyleBackColor = false;
            this.btnFiltrarCompras.Click += new System.EventHandler(this.btnFiltrarCompras_Click_1);
            // 
            // btnLimpiarFiltro
            // 
            this.btnLimpiarFiltro.BackColor = System.Drawing.Color.Gray;
            this.btnLimpiarFiltro.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarFiltro.Location = new System.Drawing.Point(590, 40);
            this.btnLimpiarFiltro.Name = "btnLimpiarFiltro";
            this.btnLimpiarFiltro.Size = new System.Drawing.Size(100, 35);
            this.btnLimpiarFiltro.TabIndex = 5;
            this.btnLimpiarFiltro.Text = "Limpiar";
            this.btnLimpiarFiltro.UseVisualStyleBackColor = false;
            this.btnLimpiarFiltro.Click += new System.EventHandler(this.btnLimpiarFiltro_Click_1);
            // 
            // dgvCompras
            // 
            this.dgvCompras.AllowUserToAddRows = false;
            this.dgvCompras.Location = new System.Drawing.Point(50, 220);
            this.dgvCompras.Name = "dgvCompras";
            this.dgvCompras.ReadOnly = true;
            this.dgvCompras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompras.Size = new System.Drawing.Size(700, 300);
            this.dgvCompras.TabIndex = 4;
            // 
            // lblTotalCompras
            // 
            this.lblTotalCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalCompras.Location = new System.Drawing.Point(50, 530);
            this.lblTotalCompras.Name = "lblTotalCompras";
            this.lblTotalCompras.Size = new System.Drawing.Size(100, 23);
            this.lblTotalCompras.TabIndex = 3;
            this.lblTotalCompras.Text = "Total de Compras: 0";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblMontoTotal.Location = new System.Drawing.Point(250, 530);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(100, 23);
            this.lblMontoTotal.TabIndex = 2;
            this.lblMontoTotal.Text = "Monto Total: $0.00";
            // 
            // btnVerDetalleCompra
            // 
            this.btnVerDetalleCompra.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnVerDetalleCompra.ForeColor = System.Drawing.Color.White;
            this.btnVerDetalleCompra.Location = new System.Drawing.Point(50, 560);
            this.btnVerDetalleCompra.Name = "btnVerDetalleCompra";
            this.btnVerDetalleCompra.Size = new System.Drawing.Size(150, 35);
            this.btnVerDetalleCompra.TabIndex = 1;
            this.btnVerDetalleCompra.Text = "Ver Detalle";
            this.btnVerDetalleCompra.UseVisualStyleBackColor = false;
            this.btnVerDetalleCompra.Click += new System.EventHandler(this.btnVerDetalleCompra_Click_1);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(600, 560);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(150, 35);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click_1);
            // 
            // FormComprasCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(800, 620);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnVerDetalleCompra);
            this.Controls.Add(this.lblMontoTotal);
            this.Controls.Add(this.lblTotalCompras);
            this.Controls.Add(this.dgvCompras);
            this.Controls.Add(this.groupBoxFiltro);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormComprasCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mis Compras - Fresco Market";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.groupBoxFiltro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            this.ResumeLayout(false);

        }

        // Declaración de controles
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;

        private System.Windows.Forms.GroupBox groupBoxFiltro;
        private System.Windows.Forms.Label lblFechaDesde;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.Label lblFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.Button btnFiltrarCompras;
        private System.Windows.Forms.Button btnLimpiarFiltro;

        private System.Windows.Forms.DataGridView dgvCompras;

        private System.Windows.Forms.Label lblTotalCompras;
        private System.Windows.Forms.Label lblMontoTotal;

        private System.Windows.Forms.Button btnVerDetalleCompra;
        private System.Windows.Forms.Button btnCerrar;
    }
}
#endregion