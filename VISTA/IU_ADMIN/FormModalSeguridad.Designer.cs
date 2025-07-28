namespace VISTA.IU_ADMIN
{
    partial class FormModalSeguridad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModalSeguridad));
            this.btnCerrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFrescoMarket = new System.Windows.Forms.Label();
            this.btnAdministrarBD = new System.Windows.Forms.Button();
            this.btnAuditoriaSesion = new System.Windows.Forms.Button();
            this.btnGestionarUsuarios = new System.Windows.Forms.Button();
            this.btnSeguridad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Goldenrod;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(192, 325);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "CERRAR";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Panel de Administración de Seguridad";
            // 
            // lblFrescoMarket
            // 
            this.lblFrescoMarket.AutoSize = true;
            this.lblFrescoMarket.Font = new System.Drawing.Font("MingLiU-ExtB", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrescoMarket.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFrescoMarket.Location = new System.Drawing.Point(25, 9);
            this.lblFrescoMarket.Name = "lblFrescoMarket";
            this.lblFrescoMarket.Size = new System.Drawing.Size(423, 64);
            this.lblFrescoMarket.TabIndex = 6;
            this.lblFrescoMarket.Text = "FrescoMarket";
            this.lblFrescoMarket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAdministrarBD
            // 
            this.btnAdministrarBD.BackColor = System.Drawing.Color.Goldenrod;
            this.btnAdministrarBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdministrarBD.Image = ((System.Drawing.Image)(resources.GetObject("btnAdministrarBD.Image")));
            this.btnAdministrarBD.Location = new System.Drawing.Point(254, 221);
            this.btnAdministrarBD.Name = "btnAdministrarBD";
            this.btnAdministrarBD.Size = new System.Drawing.Size(194, 87);
            this.btnAdministrarBD.TabIndex = 7;
            this.btnAdministrarBD.Text = "BACKUP";
            this.btnAdministrarBD.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAdministrarBD.UseVisualStyleBackColor = false;
            this.btnAdministrarBD.Click += new System.EventHandler(this.btnAdministrarBD_Click_1);
            // 
            // btnAuditoriaSesion
            // 
            this.btnAuditoriaSesion.BackColor = System.Drawing.Color.Goldenrod;
            this.btnAuditoriaSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAuditoriaSesion.Image = global::VISTA.Properties.Resources.icons8_resume_65;
            this.btnAuditoriaSesion.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAuditoriaSesion.Location = new System.Drawing.Point(12, 221);
            this.btnAuditoriaSesion.Name = "btnAuditoriaSesion";
            this.btnAuditoriaSesion.Size = new System.Drawing.Size(195, 87);
            this.btnAuditoriaSesion.TabIndex = 3;
            this.btnAuditoriaSesion.Text = "AUDITORÍA DE SESIONES";
            this.btnAuditoriaSesion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAuditoriaSesion.UseVisualStyleBackColor = false;
            this.btnAuditoriaSesion.Click += new System.EventHandler(this.btnAuditoriaSesion_Click);
            // 
            // btnGestionarUsuarios
            // 
            this.btnGestionarUsuarios.BackColor = System.Drawing.Color.Goldenrod;
            this.btnGestionarUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionarUsuarios.Image = global::VISTA.Properties.Resources.icons8_select_users_65;
            this.btnGestionarUsuarios.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGestionarUsuarios.Location = new System.Drawing.Point(253, 113);
            this.btnGestionarUsuarios.Name = "btnGestionarUsuarios";
            this.btnGestionarUsuarios.Size = new System.Drawing.Size(195, 87);
            this.btnGestionarUsuarios.TabIndex = 1;
            this.btnGestionarUsuarios.Text = "GESTIONAR USUARIOS";
            this.btnGestionarUsuarios.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGestionarUsuarios.UseVisualStyleBackColor = false;
            this.btnGestionarUsuarios.Click += new System.EventHandler(this.btnGestionarUsuarios_Click);
            // 
            // btnSeguridad
            // 
            this.btnSeguridad.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSeguridad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeguridad.Image = global::VISTA.Properties.Resources.icons8_users_65;
            this.btnSeguridad.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSeguridad.Location = new System.Drawing.Point(12, 113);
            this.btnSeguridad.Name = "btnSeguridad";
            this.btnSeguridad.Size = new System.Drawing.Size(195, 87);
            this.btnSeguridad.TabIndex = 0;
            this.btnSeguridad.Text = "GESTIONAR GRUPOS";
            this.btnSeguridad.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSeguridad.UseVisualStyleBackColor = false;
            this.btnSeguridad.Click += new System.EventHandler(this.btnSeguridad_Click);
            // 
            // FormModalSeguridad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(465, 360);
            this.Controls.Add(this.btnAdministrarBD);
            this.Controls.Add(this.lblFrescoMarket);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAuditoriaSesion);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnGestionarUsuarios);
            this.Controls.Add(this.btnSeguridad);
            this.Name = "FormModalSeguridad";
            this.Text = "FormModalSeguridad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSeguridad;
        private System.Windows.Forms.Button btnGestionarUsuarios;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnAuditoriaSesion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFrescoMarket;
        private System.Windows.Forms.Button btnAdministrarBD;
    }
}