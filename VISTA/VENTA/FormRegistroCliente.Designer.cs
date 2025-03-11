namespace VISTA
{
    partial class FormRegistroCliente
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistroCliente));
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.groupBoxDatos = new System.Windows.Forms.GroupBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxBusqueda = new System.Windows.Forms.GroupBox();
            this.txtBuscarCliente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLimpiarBusqueda = new System.Windows.Forms.Button();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.dgvCliente = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnAsignarUsuarios = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxDatos.SuspendLayout();
            this.groupBoxBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCliente)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panelSuperior.Controls.Add(this.pictureBox1);
            this.panelSuperior.Controls.Add(this.lblTitulo);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1084, 70);
            this.panelSuperior.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(63, 21);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(245, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "REGISTRO CLIENTES";
            // 
            // groupBoxDatos
            // 
            this.groupBoxDatos.Controls.Add(this.btnActualizar);
            this.groupBoxDatos.Controls.Add(this.btnModificar);
            this.groupBoxDatos.Controls.Add(this.btnEliminar);
            this.groupBoxDatos.Controls.Add(this.btnGuardar);
            this.groupBoxDatos.Controls.Add(this.txtDireccion);
            this.groupBoxDatos.Controls.Add(this.txtApellido);
            this.groupBoxDatos.Controls.Add(this.txtNombre);
            this.groupBoxDatos.Controls.Add(this.txtDocumento);
            this.groupBoxDatos.Controls.Add(this.label4);
            this.groupBoxDatos.Controls.Add(this.label3);
            this.groupBoxDatos.Controls.Add(this.label2);
            this.groupBoxDatos.Controls.Add(this.label1);
            this.groupBoxDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxDatos.Location = new System.Drawing.Point(12, 88);
            this.groupBoxDatos.Name = "groupBoxDatos";
            this.groupBoxDatos.Size = new System.Drawing.Size(304, 461);
            this.groupBoxDatos.TabIndex = 1;
            this.groupBoxDatos.TabStop = false;
            this.groupBoxDatos.Text = "DATOS DEL CLIENTE";
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(103, 410);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(120, 35);
            this.btnActualizar.TabIndex = 11;
            this.btnActualizar.Text = "ACTUALIZAR";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.ForeColor = System.Drawing.Color.White;
            this.btnModificar.Location = new System.Drawing.Point(159, 352);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(120, 35);
            this.btnModificar.TabIndex = 10;
            this.btnModificar.Text = "MODIFICAR";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(20, 352);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 35);
            this.btnEliminar.TabIndex = 9;
            this.btnEliminar.Text = "ELIMINAR";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(20, 293);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(259, 35);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(20, 245);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(259, 22);
            this.txtDireccion.TabIndex = 7;
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(20, 184);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(259, 22);
            this.txtApellido.TabIndex = 6;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(20, 123);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(259, 22);
            this.txtNombre.TabIndex = 5;
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(20, 62);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(259, 22);
            this.txtDocumento.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "DIRECCIÓN:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "APELLIDO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "NOMBRE:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "DOCUMENTO:";
            // 
            // groupBoxBusqueda
            // 
            this.groupBoxBusqueda.Controls.Add(this.txtBuscarCliente);
            this.groupBoxBusqueda.Controls.Add(this.label5);
            this.groupBoxBusqueda.Controls.Add(this.btnLimpiarBusqueda);
            this.groupBoxBusqueda.Controls.Add(this.btnBuscarCliente);
            this.groupBoxBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxBusqueda.Location = new System.Drawing.Point(322, 88);
            this.groupBoxBusqueda.Name = "groupBoxBusqueda";
            this.groupBoxBusqueda.Size = new System.Drawing.Size(750, 70);
            this.groupBoxBusqueda.TabIndex = 2;
            this.groupBoxBusqueda.TabStop = false;
            this.groupBoxBusqueda.Text = "BÚSQUEDA";
            // 
            // txtBuscarCliente
            // 
            this.txtBuscarCliente.Location = new System.Drawing.Point(186, 28);
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.Size = new System.Drawing.Size(380, 22);
            this.txtBuscarCliente.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "BUSCAR CLIENTES:";
            // 
            // btnLimpiarBusqueda
            // 
            this.btnLimpiarBusqueda.Location = new System.Drawing.Point(654, 24);
            this.btnLimpiarBusqueda.Name = "btnLimpiarBusqueda";
            this.btnLimpiarBusqueda.Size = new System.Drawing.Size(90, 30);
            this.btnLimpiarBusqueda.TabIndex = 14;
            this.btnLimpiarBusqueda.Text = "LIMPIAR";
            this.btnLimpiarBusqueda.UseVisualStyleBackColor = false;
            this.btnLimpiarBusqueda.Click += new System.EventHandler(this.btnLimpiarBusqueda_Click);
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnBuscarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCliente.ForeColor = System.Drawing.Color.White;
            this.btnBuscarCliente.Location = new System.Drawing.Point(572, 24);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(76, 30);
            this.btnBuscarCliente.TabIndex = 13;
            this.btnBuscarCliente.Text = "BUSCAR";
            this.btnBuscarCliente.UseVisualStyleBackColor = false;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // dgvCliente
            // 
            this.dgvCliente.AllowUserToAddRows = false;
            this.dgvCliente.AllowUserToDeleteRows = false;
            this.dgvCliente.BackgroundColor = System.Drawing.Color.White;
            this.dgvCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCliente.Location = new System.Drawing.Point(322, 164);
            this.dgvCliente.MultiSelect = false;
            this.dgvCliente.Name = "dgvCliente";
            this.dgvCliente.ReadOnly = true;
            this.dgvCliente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCliente.Size = new System.Drawing.Size(750, 385);
            this.dgvCliente.TabIndex = 3;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.Red;
            this.btnCerrar.Location = new System.Drawing.Point(982, 555);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(90, 35);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "CERRAR";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // btnAsignarUsuarios
            // 
            this.btnAsignarUsuarios.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnAsignarUsuarios.Location = new System.Drawing.Point(801, 555);
            this.btnAsignarUsuarios.Name = "btnAsignarUsuarios";
            this.btnAsignarUsuarios.Size = new System.Drawing.Size(168, 35);
            this.btnAsignarUsuarios.TabIndex = 5;
            this.btnAsignarUsuarios.Text = "ASIGNAR USUARIOS";
            this.btnAsignarUsuarios.UseVisualStyleBackColor = false;
            this.btnAsignarUsuarios.Click += new System.EventHandler(this.btnAsignarUsuarios_Click);
            // 
            // FormRegistroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(1084, 602);
            this.Controls.Add(this.btnAsignarUsuarios);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvCliente);
            this.Controls.Add(this.groupBoxBusqueda);
            this.Controls.Add(this.groupBoxDatos);
            this.Controls.Add(this.panelSuperior);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormRegistroCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Clientes - Fresco Market";
            this.Load += new System.EventHandler(this.FormRegistroCliente_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxDatos.ResumeLayout(false);
            this.groupBoxDatos.PerformLayout();
            this.groupBoxBusqueda.ResumeLayout(false);
            this.groupBoxBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCliente)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox groupBoxDatos;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxBusqueda;
        private System.Windows.Forms.TextBox txtBuscarCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLimpiarBusqueda;
        private System.Windows.Forms.DataGridView dgvCliente;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Button btnAsignarUsuarios;
    }
}
