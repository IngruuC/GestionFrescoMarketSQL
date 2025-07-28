namespace VISTA.IU_ADMIN
{
    partial class formGestionarGrupos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formGestionarGrupos));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cmbEstadosGrupo = new System.Windows.Forms.ComboBox();
            this.cmbNombreGrupos = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFrescoMarket = new System.Windows.Forms.Label();
            this.dgvGestionarGrupos = new System.Windows.Forms.DataGridView();
            this.btnEliminarGrupo = new System.Windows.Forms.Button();
            this.btnModificarGrupo = new System.Windows.Forms.Button();
            this.btnAgregarGrupo = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.AuditoriasSesion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRefrescarGrilla = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionarGrupos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.cmbEstadosGrupo);
            this.groupBox1.Controls.Add(this.cmbNombreGrupos);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 55);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILTROS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(261, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "ESTADO:";
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
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnBuscar.Location = new System.Drawing.Point(480, 22);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(80, 21);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cmbEstadosGrupo
            // 
            this.cmbEstadosGrupo.FormattingEnabled = true;
            this.cmbEstadosGrupo.Location = new System.Drawing.Point(328, 20);
            this.cmbEstadosGrupo.Name = "cmbEstadosGrupo";
            this.cmbEstadosGrupo.Size = new System.Drawing.Size(121, 21);
            this.cmbEstadosGrupo.TabIndex = 9;
            // 
            // cmbNombreGrupos
            // 
            this.cmbNombreGrupos.FormattingEnabled = true;
            this.cmbNombreGrupos.Location = new System.Drawing.Point(92, 19);
            this.cmbNombreGrupos.Name = "cmbNombreGrupos";
            this.cmbNombreGrupos.Size = new System.Drawing.Size(121, 21);
            this.cmbNombreGrupos.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblFrescoMarket);
            this.panel3.Controls.Add(this.AuditoriasSesion);
            this.panel3.Controls.Add(this.btnSalir);
            this.panel3.Controls.Add(this.btnRefrescarGrilla);
            this.panel3.Location = new System.Drawing.Point(3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 114);
            this.panel3.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(179, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(425, 42);
            this.label1.TabIndex = 10;
            this.label1.Text = "GESTIONAR GRUPOS";
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
            // dgvGestionarGrupos
            // 
            this.dgvGestionarGrupos.AllowUserToAddRows = false;
            this.dgvGestionarGrupos.AllowUserToDeleteRows = false;
            this.dgvGestionarGrupos.BackgroundColor = System.Drawing.Color.White;
            this.dgvGestionarGrupos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGestionarGrupos.Location = new System.Drawing.Point(12, 183);
            this.dgvGestionarGrupos.MultiSelect = false;
            this.dgvGestionarGrupos.Name = "dgvGestionarGrupos";
            this.dgvGestionarGrupos.ReadOnly = true;
            this.dgvGestionarGrupos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGestionarGrupos.Size = new System.Drawing.Size(776, 204);
            this.dgvGestionarGrupos.TabIndex = 24;
            // 
            // btnEliminarGrupo
            // 
            this.btnEliminarGrupo.BackColor = System.Drawing.Color.Goldenrod;
            this.btnEliminarGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarGrupo.Location = new System.Drawing.Point(135, 393);
            this.btnEliminarGrupo.Name = "btnEliminarGrupo";
            this.btnEliminarGrupo.Size = new System.Drawing.Size(117, 47);
            this.btnEliminarGrupo.TabIndex = 22;
            this.btnEliminarGrupo.Text = "ELIMINAR";
            this.btnEliminarGrupo.UseVisualStyleBackColor = false;
            this.btnEliminarGrupo.Click += new System.EventHandler(this.btnEliminarGrupo_Click);
            // 
            // btnModificarGrupo
            // 
            this.btnModificarGrupo.BackColor = System.Drawing.Color.Goldenrod;
            this.btnModificarGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarGrupo.Location = new System.Drawing.Point(264, 393);
            this.btnModificarGrupo.Name = "btnModificarGrupo";
            this.btnModificarGrupo.Size = new System.Drawing.Size(117, 47);
            this.btnModificarGrupo.TabIndex = 21;
            this.btnModificarGrupo.Text = "MODIFICAR";
            this.btnModificarGrupo.UseVisualStyleBackColor = false;
            this.btnModificarGrupo.Click += new System.EventHandler(this.btnModificarGrupo_Click);
            // 
            // btnAgregarGrupo
            // 
            this.btnAgregarGrupo.BackColor = System.Drawing.Color.Goldenrod;
            this.btnAgregarGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarGrupo.Location = new System.Drawing.Point(12, 393);
            this.btnAgregarGrupo.Name = "btnAgregarGrupo";
            this.btnAgregarGrupo.Size = new System.Drawing.Size(117, 47);
            this.btnAgregarGrupo.TabIndex = 20;
            this.btnAgregarGrupo.Text = "AGREGAR";
            this.btnAgregarGrupo.UseVisualStyleBackColor = false;
            this.btnAgregarGrupo.Click += new System.EventHandler(this.btnAgregarGrupo_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Image = global::VISTA.Properties.Resources.icons8_close_30;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCerrar.Location = new System.Drawing.Point(750, 402);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(38, 38);
            this.btnCerrar.TabIndex = 27;
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
            // formGestionarGrupos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgvGestionarGrupos);
            this.Controls.Add(this.btnEliminarGrupo);
            this.Controls.Add(this.btnModificarGrupo);
            this.Controls.Add(this.btnAgregarGrupo);
            this.Name = "formGestionarGrupos";
            this.Text = "formGestionarGrupos";
            this.Load += new System.EventHandler(this.formGestionarGrupos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionarGrupos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cmbEstadosGrupo;
        private System.Windows.Forms.ComboBox cmbNombreGrupos;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFrescoMarket;
        private System.Windows.Forms.Button AuditoriasSesion;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnRefrescarGrilla;
        private System.Windows.Forms.DataGridView dgvGestionarGrupos;
        private System.Windows.Forms.Button btnEliminarGrupo;
        private System.Windows.Forms.Button btnModificarGrupo;
        private System.Windows.Forms.Button btnAgregarGrupo;
    }
}