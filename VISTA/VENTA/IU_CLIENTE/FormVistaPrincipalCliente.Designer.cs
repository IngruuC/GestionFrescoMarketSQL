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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVistaPrincipalCliente));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblCantidadCarrito = new System.Windows.Forms.Label();
            this.lblBienvenido = new System.Windows.Forms.Label();
            this.panelOfertas = new System.Windows.Forms.Panel();
            this.panelContenedorInicio = new System.Windows.Forms.Panel();
            this.timerActualizarCarrito = new System.Windows.Forms.Timer(this.components);
            this.btnFavoritos = new System.Windows.Forms.Button();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.btnMisCompras = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnCarrito = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelMenu.Controls.Add(this.btnCerrarSesion);
            this.panelMenu.Controls.Add(this.btnFavoritos);
            this.panelMenu.Controls.Add(this.btnPerfil);
            this.panelMenu.Controls.Add(this.btnMisCompras);
            this.panelMenu.Controls.Add(this.btnInicio);
            this.panelMenu.Controls.Add(this.pictureBox1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 114);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(225, 536);
            this.panelMenu.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(7, 474);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(195, 50);
            this.btnCerrarSesion.TabIndex = 4;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click_1);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.lblCantidadCarrito);
            this.panelSuperior.Controls.Add(this.btnCarrito);
            this.panelSuperior.Controls.Add(this.pictureBox3);
            this.panelSuperior.Controls.Add(this.lblBienvenido);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1150, 114);
            this.panelSuperior.TabIndex = 1;
            // 
            // lblCantidadCarrito
            // 
            this.lblCantidadCarrito.AutoSize = true;
            this.lblCantidadCarrito.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadCarrito.ForeColor = System.Drawing.Color.Brown;
            this.lblCantidadCarrito.Location = new System.Drawing.Point(1093, 73);
            this.lblCantidadCarrito.Name = "lblCantidadCarrito";
            this.lblCantidadCarrito.Size = new System.Drawing.Size(0, 20);
            this.lblCantidadCarrito.TabIndex = 11;
            // 
            // lblBienvenido
            // 
            this.lblBienvenido.AutoSize = true;
            this.lblBienvenido.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenido.ForeColor = System.Drawing.Color.Black;
            this.lblBienvenido.Location = new System.Drawing.Point(279, 43);
            this.lblBienvenido.Name = "lblBienvenido";
            this.lblBienvenido.Size = new System.Drawing.Size(440, 50);
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
            // panelContenedorInicio
            // 
            this.panelContenedorInicio.Location = new System.Drawing.Point(222, 114);
            this.panelContenedorInicio.Name = "panelContenedorInicio";
            this.panelContenedorInicio.Size = new System.Drawing.Size(928, 536);
            this.panelContenedorInicio.TabIndex = 14;
            this.panelContenedorInicio.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenedorInicio_Paint);
            // 
            // btnFavoritos
            // 
            this.btnFavoritos.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnFavoritos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFavoritos.Image = global::VISTA.Properties.Resources.icons8_star_filled_75;
            this.btnFavoritos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFavoritos.Location = new System.Drawing.Point(7, 375);
            this.btnFavoritos.Name = "btnFavoritos";
            this.btnFavoritos.Size = new System.Drawing.Size(195, 87);
            this.btnFavoritos.TabIndex = 9;
            this.btnFavoritos.Text = "FAVORITOS";
            this.btnFavoritos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFavoritos.UseVisualStyleBackColor = false;
            this.btnFavoritos.Click += new System.EventHandler(this.btnFavoritos_Click);
            // 
            // btnPerfil
            // 
            this.btnPerfil.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnPerfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPerfil.Image = global::VISTA.Properties.Resources.icons8_user_75;
            this.btnPerfil.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPerfil.Location = new System.Drawing.Point(7, 189);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Size = new System.Drawing.Size(195, 87);
            this.btnPerfil.TabIndex = 7;
            this.btnPerfil.Text = "MI PERFIL";
            this.btnPerfil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPerfil.UseVisualStyleBackColor = false;
            this.btnPerfil.Click += new System.EventHandler(this.btnPerfil_Click);
            // 
            // btnMisCompras
            // 
            this.btnMisCompras.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnMisCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMisCompras.Image = global::VISTA.Properties.Resources.icons8_full_shopping_basket_75;
            this.btnMisCompras.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMisCompras.Location = new System.Drawing.Point(7, 282);
            this.btnMisCompras.Name = "btnMisCompras";
            this.btnMisCompras.Size = new System.Drawing.Size(195, 87);
            this.btnMisCompras.TabIndex = 8;
            this.btnMisCompras.Text = "COMPRAS";
            this.btnMisCompras.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMisCompras.UseVisualStyleBackColor = false;
            this.btnMisCompras.Click += new System.EventHandler(this.btnMisCompras_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.Image = global::VISTA.Properties.Resources.icons8_house_75;
            this.btnInicio.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInicio.Location = new System.Drawing.Point(7, 96);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(195, 87);
            this.btnInicio.TabIndex = 6;
            this.btnInicio.Text = "INICIO";
            this.btnInicio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInicio.UseVisualStyleBackColor = false;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(55, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(95, 90);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Location = new System.Drawing.Point(55, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(126, 131);
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // btnCarrito
            // 
            this.btnCarrito.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCarrito.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarrito.Image = global::VISTA.Properties.Resources.icons8_add_shopping_cart_100;
            this.btnCarrito.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCarrito.Location = new System.Drawing.Point(1054, 0);
            this.btnCarrito.Name = "btnCarrito";
            this.btnCarrito.Size = new System.Drawing.Size(96, 114);
            this.btnCarrito.TabIndex = 7;
            this.btnCarrito.Text = "CARRITO";
            this.btnCarrito.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCarrito.UseVisualStyleBackColor = false;
            this.btnCarrito.Click += new System.EventHandler(this.btnCarrito_Click_1);
            // 
            // FormVistaPrincipalCliente
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(250)))), ((int)(((byte)(205)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1150, 650);
            this.Controls.Add(this.panelContenedorInicio);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormVistaPrincipalCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fresco Market - Inicio";
            this.panelMenu.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }
        // Declaración de controles
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnCerrarSesion;

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblBienvenido;

        private System.Windows.Forms.Panel panelOfertas;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCantidadCarrito;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.Panel panelContenedorInicio;
        private System.Windows.Forms.Button btnPerfil;
        private System.Windows.Forms.Button btnFavoritos;
        private System.Windows.Forms.Button btnMisCompras;
        private System.Windows.Forms.Timer timerActualizarCarrito;
        private System.Windows.Forms.Button btnCarrito;
    }
}
#endregion