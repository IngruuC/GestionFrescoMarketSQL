namespace VISTA
{
    partial class FormAsignarProductos
    {
        //// <summary>
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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlProductosDisponibles = new System.Windows.Forms.Panel();
            this.lblProductosDisponibles = new System.Windows.Forms.Label();
            this.lstProductosDisponibles = new System.Windows.Forms.ListBox();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnAsignarProducto = new System.Windows.Forms.Button();
            this.btnQuitarProducto = new System.Windows.Forms.Button();
            this.pnlProductosAsignados = new System.Windows.Forms.Panel();
            this.lblProductosAsignados = new System.Windows.Forms.Label();
            this.lstProductosAsignados = new System.Windows.Forms.ListBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSuperior.SuspendLayout();
            this.pnlProductosDisponibles.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.pnlProductosAsignados.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(680, 60);
            this.panelSuperior.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(12, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(181, 24);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Asignar Productos";
            // 
            // pnlProductosDisponibles
            // 
            this.pnlProductosDisponibles.Controls.Add(this.lblProductosDisponibles);
            this.pnlProductosDisponibles.Controls.Add(this.lstProductosDisponibles);
            this.pnlProductosDisponibles.Location = new System.Drawing.Point(12, 83);
            this.pnlProductosDisponibles.Name = "pnlProductosDisponibles";
            this.pnlProductosDisponibles.Size = new System.Drawing.Size(250, 300);
            this.pnlProductosDisponibles.TabIndex = 1;
            // 
            // lblProductosDisponibles
            // 
            this.lblProductosDisponibles.AutoSize = true;
            this.lblProductosDisponibles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductosDisponibles.Location = new System.Drawing.Point(3, 8);
            this.lblProductosDisponibles.Name = "lblProductosDisponibles";
            this.lblProductosDisponibles.Size = new System.Drawing.Size(168, 16);
            this.lblProductosDisponibles.TabIndex = 1;
            this.lblProductosDisponibles.Text = "Productos Disponibles:";
            // 
            // lstProductosDisponibles
            // 
            this.lstProductosDisponibles.FormattingEnabled = true;
            this.lstProductosDisponibles.Location = new System.Drawing.Point(6, 36);
            this.lstProductosDisponibles.Name = "lstProductosDisponibles";
            this.lstProductosDisponibles.Size = new System.Drawing.Size(241, 251);
            this.lstProductosDisponibles.TabIndex = 0;
            // 
            // pnlBotones
            // 
            this.pnlBotones.Controls.Add(this.label2);
            this.pnlBotones.Controls.Add(this.label1);
            this.pnlBotones.Controls.Add(this.btnAsignarProducto);
            this.pnlBotones.Controls.Add(this.btnQuitarProducto);
            this.pnlBotones.Location = new System.Drawing.Point(268, 146);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(125, 170);
            this.pnlBotones.TabIndex = 2;
            // 
            // btnAsignarProducto
            // 
            this.btnAsignarProducto.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnAsignarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarProducto.ForeColor = System.Drawing.Color.White;
            this.btnAsignarProducto.Location = new System.Drawing.Point(15, 33);
            this.btnAsignarProducto.Name = "btnAsignarProducto";
            this.btnAsignarProducto.Size = new System.Drawing.Size(95, 40);
            this.btnAsignarProducto.TabIndex = 0;
            this.btnAsignarProducto.Text = ">>";
            this.btnAsignarProducto.UseVisualStyleBackColor = false;
            this.btnAsignarProducto.Click += new System.EventHandler(this.btnAsignarProducto_Click);
            // 
            // btnQuitarProducto
            // 
            this.btnQuitarProducto.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnQuitarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarProducto.ForeColor = System.Drawing.Color.White;
            this.btnQuitarProducto.Location = new System.Drawing.Point(15, 105);
            this.btnQuitarProducto.Name = "btnQuitarProducto";
            this.btnQuitarProducto.Size = new System.Drawing.Size(95, 40);
            this.btnQuitarProducto.TabIndex = 0;
            this.btnQuitarProducto.Text = "<<";
            this.btnQuitarProducto.UseVisualStyleBackColor = false;
            this.btnQuitarProducto.Click += new System.EventHandler(this.btnQuitarProducto_Click);
            // 
            // pnlProductosAsignados
            // 
            this.pnlProductosAsignados.Controls.Add(this.lblProductosAsignados);
            this.pnlProductosAsignados.Controls.Add(this.lstProductosAsignados);
            this.pnlProductosAsignados.Location = new System.Drawing.Point(399, 83);
            this.pnlProductosAsignados.Name = "pnlProductosAsignados";
            this.pnlProductosAsignados.Size = new System.Drawing.Size(250, 300);
            this.pnlProductosAsignados.TabIndex = 3;
            // 
            // lblProductosAsignados
            // 
            this.lblProductosAsignados.AutoSize = true;
            this.lblProductosAsignados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductosAsignados.Location = new System.Drawing.Point(3, 8);
            this.lblProductosAsignados.Name = "lblProductosAsignados";
            this.lblProductosAsignados.Size = new System.Drawing.Size(159, 16);
            this.lblProductosAsignados.TabIndex = 1;
            this.lblProductosAsignados.Text = "Productos Asignados:";
            // 
            // lstProductosAsignados
            // 
            this.lstProductosAsignados.FormattingEnabled = true;
            this.lstProductosAsignados.Location = new System.Drawing.Point(6, 36);
            this.lstProductosAsignados.Name = "lstProductosAsignados";
            this.lstProductosAsignados.Size = new System.Drawing.Size(241, 251);
            this.lstProductosAsignados.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(554, 398);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(95, 40);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "CERRAR";
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "QUITAR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ASIGNAR";
            // 
            // FormAsignarProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(680, 450);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.pnlProductosAsignados);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.pnlProductosDisponibles);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAsignarProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asignar Productos";
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.pnlProductosDisponibles.ResumeLayout(false);
            this.pnlProductosDisponibles.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.pnlBotones.PerformLayout();
            this.pnlProductosAsignados.ResumeLayout(false);
            this.pnlProductosAsignados.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlProductosDisponibles;
        private System.Windows.Forms.Label lblProductosDisponibles;
        private System.Windows.Forms.ListBox lstProductosDisponibles;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnAsignarProducto;
        private System.Windows.Forms.Button btnQuitarProducto;
        private System.Windows.Forms.Panel pnlProductosAsignados;
        private System.Windows.Forms.Label lblProductosAsignados;
        private System.Windows.Forms.ListBox lstProductosAsignados;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}