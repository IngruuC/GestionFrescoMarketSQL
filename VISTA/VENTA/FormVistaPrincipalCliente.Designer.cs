namespace VISTA
{
    partial class FormVistaPrincipalCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVistaPrincipalCliente));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnInicio = new System.Windows.Forms.Button();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.btnMisCompras = new System.Windows.Forms.Button();
            this.btnFavoritos = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblBienvenido = new System.Windows.Forms.Label();
            this.panelOfertas = new System.Windows.Forms.Panel();
            this.lblOfertasEspeciales = new System.Windows.Forms.Label();
            this.panelRecomendados = new System.Windows.Forms.Panel();
            this.lblRecomendados = new System.Windows.Forms.Label();
            this.panelOferta1 = new System.Windows.Forms.Panel();
            this.panelOferta3 = new System.Windows.Forms.Panel();
            this.panelOferta2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblCantidadCarrito = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelMenu.Controls.Add(this.pictureBox1);
            this.panelMenu.Controls.Add(this.btnInicio);
            this.panelMenu.Controls.Add(this.btnPerfil);
            this.panelMenu.Controls.Add(this.btnMisCompras);
            this.panelMenu.Controls.Add(this.btnFavoritos);
            this.panelMenu.Controls.Add(this.btnCerrarSesion);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 93);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(250, 557);
            this.panelMenu.TabIndex = 0;
            // 
            // btnInicio
            // 
            this.btnInicio.BackColor = this.panelMenu.BackColor;
            this.btnInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.ForeColor = System.Drawing.Color.White;
            this.btnInicio.Location = new System.Drawing.Point(0, 171);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(250, 50);
            this.btnInicio.TabIndex = 0;
            this.btnInicio.Text = "Inicio";
            this.btnInicio.UseVisualStyleBackColor = false;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // btnPerfil
            // 
            this.btnPerfil.BackColor = this.panelMenu.BackColor;
            this.btnPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnPerfil.ForeColor = System.Drawing.Color.White;
            this.btnPerfil.Location = new System.Drawing.Point(0, 218);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Size = new System.Drawing.Size(250, 50);
            this.btnPerfil.TabIndex = 1;
            this.btnPerfil.Text = "Mi Perfil";
            this.btnPerfil.UseVisualStyleBackColor = false;
            this.btnPerfil.Click += new System.EventHandler(this.btnPerfil_Click_1);
            // 
            // btnMisCompras
            // 
            this.btnMisCompras.BackColor = this.panelMenu.BackColor;
            this.btnMisCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMisCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnMisCompras.ForeColor = System.Drawing.Color.White;
            this.btnMisCompras.Location = new System.Drawing.Point(0, 268);
            this.btnMisCompras.Name = "btnMisCompras";
            this.btnMisCompras.Size = new System.Drawing.Size(250, 50);
            this.btnMisCompras.TabIndex = 2;
            this.btnMisCompras.Text = "Mis Compras";
            this.btnMisCompras.UseVisualStyleBackColor = false;
            this.btnMisCompras.Click += new System.EventHandler(this.btnMisCompras_Click_1);
            // 
            // btnFavoritos
            // 
            this.btnFavoritos.BackColor = this.panelMenu.BackColor;
            this.btnFavoritos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFavoritos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnFavoritos.ForeColor = System.Drawing.Color.White;
            this.btnFavoritos.Location = new System.Drawing.Point(0, 318);
            this.btnFavoritos.Name = "btnFavoritos";
            this.btnFavoritos.Size = new System.Drawing.Size(250, 50);
            this.btnFavoritos.TabIndex = 3;
            this.btnFavoritos.Text = "Favoritos";
            this.btnFavoritos.UseVisualStyleBackColor = false;
            this.btnFavoritos.Click += new System.EventHandler(this.btnFavoritos_Click_1);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(0, 463);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(250, 50);
            this.btnCerrarSesion.TabIndex = 4;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click_1);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.lblCantidadCarrito);
            this.panelSuperior.Controls.Add(this.pictureBox2);
            this.panelSuperior.Controls.Add(this.lblBienvenido);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1150, 93);
            this.panelSuperior.TabIndex = 1;
            // 
            // lblBienvenido
            // 
            this.lblBienvenido.AutoSize = true;
            this.lblBienvenido.Font = new System.Drawing.Font("Modern No. 20", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenido.ForeColor = System.Drawing.Color.White;
            this.lblBienvenido.Location = new System.Drawing.Point(254, 32);
            this.lblBienvenido.Name = "lblBienvenido";
            this.lblBienvenido.Size = new System.Drawing.Size(346, 38);
            this.lblBienvenido.TabIndex = 0;
            this.lblBienvenido.Text = "Bienvenido, Usuario";
            // 
            // panelOfertas
            // 
            this.panelOfertas.BackColor = System.Drawing.Color.White;
            this.panelOfertas.Location = new System.Drawing.Point(300, 100);
            this.panelOfertas.Name = "panelOfertas";
            this.panelOfertas.Size = new System.Drawing.Size(800, 200);
            this.panelOfertas.TabIndex = 0;
            // 
            // lblOfertasEspeciales
            // 
            this.lblOfertasEspeciales.AutoSize = true;
            this.lblOfertasEspeciales.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblOfertasEspeciales.Location = new System.Drawing.Point(296, 125);
            this.lblOfertasEspeciales.Name = "lblOfertasEspeciales";
            this.lblOfertasEspeciales.Size = new System.Drawing.Size(208, 20);
            this.lblOfertasEspeciales.TabIndex = 2;
            this.lblOfertasEspeciales.Text = "OFERTAS ESPECIALES";
            // 
            // panelRecomendados
            // 
            this.panelRecomendados.BackColor = System.Drawing.Color.White;
            this.panelRecomendados.Location = new System.Drawing.Point(300, 300);
            this.panelRecomendados.Name = "panelRecomendados";
            this.panelRecomendados.Size = new System.Drawing.Size(800, 250);
            this.panelRecomendados.TabIndex = 7;
            // 
            // lblRecomendados
            // 
            this.lblRecomendados.AutoSize = true;
            this.lblRecomendados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecomendados.Location = new System.Drawing.Point(300, 280);
            this.lblRecomendados.Name = "lblRecomendados";
            this.lblRecomendados.Size = new System.Drawing.Size(234, 20);
            this.lblRecomendados.TabIndex = 6;
            this.lblRecomendados.Text = "RECOMENDADOS PARA TI";
            // 
            // panelOferta1
            // 
            this.panelOferta1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelOferta1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelOferta1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelOferta1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.panelOferta1.Location = new System.Drawing.Point(314, 158);
            this.panelOferta1.Name = "panelOferta1";
            this.panelOferta1.Size = new System.Drawing.Size(160, 108);
            this.panelOferta1.TabIndex = 8;
            // 
            // panelOferta3
            // 
            this.panelOferta3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelOferta3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelOferta3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelOferta3.Location = new System.Drawing.Point(940, 158);
            this.panelOferta3.Name = "panelOferta3";
            this.panelOferta3.Size = new System.Drawing.Size(160, 108);
            this.panelOferta3.TabIndex = 9;
            // 
            // panelOferta2
            // 
            this.panelOferta2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelOferta2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelOferta2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelOferta2.Location = new System.Drawing.Point(624, 158);
            this.panelOferta2.Name = "panelOferta2";
            this.panelOferta2.Size = new System.Drawing.Size(160, 108);
            this.panelOferta2.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(78, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 90);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1048, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(99, 90);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // lblCantidadCarrito
            // 
            this.lblCantidadCarrito.AutoSize = true;
            this.lblCantidadCarrito.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadCarrito.ForeColor = System.Drawing.Color.Brown;
            this.lblCantidadCarrito.Location = new System.Drawing.Point(1090, 46);
            this.lblCantidadCarrito.Name = "lblCantidadCarrito";
            this.lblCantidadCarrito.Size = new System.Drawing.Size(0, 20);
            this.lblCantidadCarrito.TabIndex = 11;
            // 
            // FormVistaPrincipalCliente
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(205)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1150, 650);
            this.Controls.Add(this.panelOferta2);
            this.Controls.Add(this.panelOferta3);
            this.Controls.Add(this.panelOferta1);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelSuperior);
            this.Controls.Add(this.lblOfertasEspeciales);
            this.Controls.Add(this.lblRecomendados);
            this.Controls.Add(this.panelRecomendados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormVistaPrincipalCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fresco Market - Inicio";
            this.panelMenu.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        // Declaración de controles
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.Button btnPerfil;
        private System.Windows.Forms.Button btnMisCompras;
        private System.Windows.Forms.Button btnFavoritos;
        private System.Windows.Forms.Button btnCerrarSesion;

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblBienvenido;

        private System.Windows.Forms.Panel panelOfertas;
        private System.Windows.Forms.Label lblOfertasEspeciales;

        private System.Windows.Forms.Panel panelRecomendados;
        private System.Windows.Forms.Label lblRecomendados;
        private System.Windows.Forms.Panel panelOferta1;
        private System.Windows.Forms.Panel panelOferta3;
        private System.Windows.Forms.Panel panelOferta2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblCantidadCarrito;
    }
}
#endregion