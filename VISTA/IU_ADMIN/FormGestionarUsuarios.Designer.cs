namespace VISTA.SEGURIDAD.FORMS
{
    partial class FormGestionarUsuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGestionarUsuarios));
            this.btnAgregarUsuario = new System.Windows.Forms.Button();
            this.btnModificarUsuario = new System.Windows.Forms.Button();
            this.btnEliminarUsuario = new System.Windows.Forms.Button();
            this.btnResetearUsuario = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cmbNombreUsuarios = new System.Windows.Forms.ComboBox();
            this.cmbCargarGrupos = new System.Windows.Forms.ComboBox();
            this.cmbCargarEstadoUsuario = new System.Windows.Forms.ComboBox();
            this.dgvGestionarUsuarios = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblFrescoMarket = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.AuditoriasSesion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRefrescarGrilla = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionarUsuarios)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAgregarUsuario
            // 
            this.btnAgregarUsuario.BackColor = System.Drawing.Color.Goldenrod;
            this.btnAgregarUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarUsuario.Location = new System.Drawing.Point(12, 391);
            this.btnAgregarUsuario.Name = "btnAgregarUsuario";
            this.btnAgregarUsuario.Size = new System.Drawing.Size(117, 47);
            this.btnAgregarUsuario.TabIndex = 0;
            this.btnAgregarUsuario.Text = "AGREGAR";
            this.btnAgregarUsuario.UseVisualStyleBackColor = false;
            this.btnAgregarUsuario.Click += new System.EventHandler(this.btnAgregarUsuario_Click);
            // 
            // btnModificarUsuario
            // 
            this.btnModificarUsuario.BackColor = System.Drawing.Color.Goldenrod;
            this.btnModificarUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarUsuario.Location = new System.Drawing.Point(264, 391);
            this.btnModificarUsuario.Name = "btnModificarUsuario";
            this.btnModificarUsuario.Size = new System.Drawing.Size(117, 47);
            this.btnModificarUsuario.TabIndex = 1;
            this.btnModificarUsuario.Text = "MODIFICAR";
            this.btnModificarUsuario.UseVisualStyleBackColor = false;
            this.btnModificarUsuario.Click += new System.EventHandler(this.btnModificarUsuario_Click);
            // 
            // btnEliminarUsuario
            // 
            this.btnEliminarUsuario.BackColor = System.Drawing.Color.Goldenrod;
            this.btnEliminarUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarUsuario.Location = new System.Drawing.Point(135, 391);
            this.btnEliminarUsuario.Name = "btnEliminarUsuario";
            this.btnEliminarUsuario.Size = new System.Drawing.Size(117, 47);
            this.btnEliminarUsuario.TabIndex = 2;
            this.btnEliminarUsuario.Text = "ELIMINAR";
            this.btnEliminarUsuario.UseVisualStyleBackColor = false;
            this.btnEliminarUsuario.Click += new System.EventHandler(this.btnEliminarUsuario_Click);
            // 
            // btnResetearUsuario
            // 
            this.btnResetearUsuario.BackColor = System.Drawing.Color.Goldenrod;
            this.btnResetearUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetearUsuario.Location = new System.Drawing.Point(387, 391);
            this.btnResetearUsuario.Name = "btnResetearUsuario";
            this.btnResetearUsuario.Size = new System.Drawing.Size(117, 47);
            this.btnResetearUsuario.TabIndex = 3;
            this.btnResetearUsuario.Text = "RESETEAR";
            this.btnResetearUsuario.UseVisualStyleBackColor = false;
            this.btnResetearUsuario.Click += new System.EventHandler(this.btnResetearUsuario_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnBuscar.Location = new System.Drawing.Point(678, 19);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 21);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cmbNombreUsuarios
            // 
            this.cmbNombreUsuarios.FormattingEnabled = true;
            this.cmbNombreUsuarios.Location = new System.Drawing.Point(92, 19);
            this.cmbNombreUsuarios.Name = "cmbNombreUsuarios";
            this.cmbNombreUsuarios.Size = new System.Drawing.Size(121, 21);
            this.cmbNombreUsuarios.TabIndex = 7;
            // 
            // cmbCargarGrupos
            // 
            this.cmbCargarGrupos.FormattingEnabled = true;
            this.cmbCargarGrupos.Location = new System.Drawing.Point(304, 19);
            this.cmbCargarGrupos.Name = "cmbCargarGrupos";
            this.cmbCargarGrupos.Size = new System.Drawing.Size(121, 21);
            this.cmbCargarGrupos.TabIndex = 8;
            // 
            // cmbCargarEstadoUsuario
            // 
            this.cmbCargarEstadoUsuario.FormattingEnabled = true;
            this.cmbCargarEstadoUsuario.Location = new System.Drawing.Point(527, 19);
            this.cmbCargarEstadoUsuario.Name = "cmbCargarEstadoUsuario";
            this.cmbCargarEstadoUsuario.Size = new System.Drawing.Size(121, 21);
            this.cmbCargarEstadoUsuario.TabIndex = 9;
            // 
            // dgvGestionarUsuarios
            // 
            this.dgvGestionarUsuarios.AllowUserToAddRows = false;
            this.dgvGestionarUsuarios.AllowUserToDeleteRows = false;
            this.dgvGestionarUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvGestionarUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGestionarUsuarios.Location = new System.Drawing.Point(12, 181);
            this.dgvGestionarUsuarios.MultiSelect = false;
            this.dgvGestionarUsuarios.Name = "dgvGestionarUsuarios";
            this.dgvGestionarUsuarios.ReadOnly = true;
            this.dgvGestionarUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGestionarUsuarios.Size = new System.Drawing.Size(776, 204);
            this.dgvGestionarUsuarios.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblFrescoMarket);
            this.panel3.Controls.Add(this.AuditoriasSesion);
            this.panel3.Controls.Add(this.btnSalir);
            this.panel3.Controls.Add(this.btnRefrescarGrilla);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 114);
            this.panel3.TabIndex = 14;
            // 
            // lblFrescoMarket
            // 
            this.lblFrescoMarket.AutoSize = true;
            this.lblFrescoMarket.Font = new System.Drawing.Font("MingLiU-ExtB", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrescoMarket.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFrescoMarket.Location = new System.Drawing.Point(194, 0);
            this.lblFrescoMarket.Name = "lblFrescoMarket";
            this.lblFrescoMarket.Size = new System.Drawing.Size(423, 64);
            this.lblFrescoMarket.TabIndex = 9;
            this.lblFrescoMarket.Text = "FrescoMarket";
            this.lblFrescoMarket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(179, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 42);
            this.label1.TabIndex = 10;
            this.label1.Text = "GESTIONAR USUARIOS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "NOMBRE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "ESTADO:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "GRUPO:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.cmbCargarEstadoUsuario);
            this.groupBox1.Controls.Add(this.cmbCargarGrupos);
            this.groupBox1.Controls.Add(this.cmbNombreUsuarios);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 55);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTROS";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Image = global::VISTA.Properties.Resources.icons8_close_30;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCerrar.Location = new System.Drawing.Point(750, 400);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(38, 38);
            this.btnCerrar.TabIndex = 19;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
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
            // 
            // btnRefrescarGrilla
            // 
            this.btnRefrescarGrilla.BackColor = System.Drawing.Color.LemonChiffon;
            this.btnRefrescarGrilla.Image = global::VISTA.Properties.Resources.icons8_refresh_30;
            this.btnRefrescarGrilla.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefrescarGrilla.Location = new System.Drawing.Point(690, 74);
            this.btnRefrescarGrilla.Name = "btnRefrescarGrilla";
            this.btnRefrescarGrilla.Size = new System.Drawing.Size(107, 37);
            this.btnRefrescarGrilla.TabIndex = 6;
            this.btnRefrescarGrilla.Text = "REFRESCAR";
            this.btnRefrescarGrilla.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefrescarGrilla.UseVisualStyleBackColor = false;
            this.btnRefrescarGrilla.Click += new System.EventHandler(this.btnRefrescarGrilla_Click);
            // 
            // FormGestionarUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgvGestionarUsuarios);
            this.Controls.Add(this.btnResetearUsuario);
            this.Controls.Add(this.btnEliminarUsuario);
            this.Controls.Add(this.btnModificarUsuario);
            this.Controls.Add(this.btnAgregarUsuario);
            this.Name = "FormGestionarUsuarios";
            this.Text = "FormGestionarUsuarios";
            this.Load += new System.EventHandler(this.FormGestionarUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionarUsuarios)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAgregarUsuario;
        private System.Windows.Forms.Button btnModificarUsuario;
        private System.Windows.Forms.Button btnEliminarUsuario;
        private System.Windows.Forms.Button btnResetearUsuario;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnRefrescarGrilla;
        private System.Windows.Forms.ComboBox cmbNombreUsuarios;
        private System.Windows.Forms.ComboBox cmbCargarGrupos;
        private System.Windows.Forms.ComboBox cmbCargarEstadoUsuario;
        private System.Windows.Forms.DataGridView dgvGestionarUsuarios;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button AuditoriasSesion;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFrescoMarket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCerrar;
    }
}