namespace VISTA
{
    partial class FormFavoritosCliente
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
            this.dgvFavoritos = new System.Windows.Forms.DataGridView();
            this.lblTotalFavoritos = new System.Windows.Forms.Label();
            this.btnAgregarFavorito = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFavoritos)).BeginInit();
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
            this.panelSuperior.TabIndex = 4;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(100, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(188, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "MIS FAVORITOS";
            // 
            // dgvFavoritos
            // 
            this.dgvFavoritos.AllowUserToAddRows = false;
            this.dgvFavoritos.Location = new System.Drawing.Point(50, 100);
            this.dgvFavoritos.Name = "dgvFavoritos";
            this.dgvFavoritos.ReadOnly = true;
            this.dgvFavoritos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFavoritos.Size = new System.Drawing.Size(700, 350);
            this.dgvFavoritos.TabIndex = 3;
            // 
            // lblTotalFavoritos
            // 
            this.lblTotalFavoritos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalFavoritos.Location = new System.Drawing.Point(50, 460);
            this.lblTotalFavoritos.Name = "lblTotalFavoritos";
            this.lblTotalFavoritos.Size = new System.Drawing.Size(100, 23);
            this.lblTotalFavoritos.TabIndex = 2;
            this.lblTotalFavoritos.Text = "Total de Favoritos: 0";
            // 
            // btnAgregarFavorito
            // 
            this.btnAgregarFavorito.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnAgregarFavorito.ForeColor = System.Drawing.Color.White;
            this.btnAgregarFavorito.Location = new System.Drawing.Point(50, 500);
            this.btnAgregarFavorito.Name = "btnAgregarFavorito";
            this.btnAgregarFavorito.Size = new System.Drawing.Size(150, 35);
            this.btnAgregarFavorito.TabIndex = 1;
            this.btnAgregarFavorito.Text = "Agregar Favorito";
            this.btnAgregarFavorito.UseVisualStyleBackColor = false;
            this.btnAgregarFavorito.Click += new System.EventHandler(this.btnAgregarFavorito_Click_1);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(600, 500);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(150, 35);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FormFavoritosCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(800, 570);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnAgregarFavorito);
            this.Controls.Add(this.lblTotalFavoritos);
            this.Controls.Add(this.dgvFavoritos);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormFavoritosCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mis Favoritos - Fresco Market";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFavoritos)).EndInit();
            this.ResumeLayout(false);

        }

        // Declaración de controles
        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;

        private System.Windows.Forms.DataGridView dgvFavoritos;
        private System.Windows.Forms.Label lblTotalFavoritos;

        private System.Windows.Forms.Button btnAgregarFavorito;
        private System.Windows.Forms.Button btnCerrar;
    }
}
#endregion