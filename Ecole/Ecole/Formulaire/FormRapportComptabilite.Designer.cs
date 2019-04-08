namespace Ecole.Formulaire
{
    partial class FormRapportComptabilite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRapportComptabilite));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtcomboprevision = new System.Windows.Forms.TextBox();
            this.cmbprevision = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.txtcomboprevision);
            this.groupControl1.Controls.Add(this.cmbprevision);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(545, 292);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Liste des Previsions";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton1.Location = new System.Drawing.Point(167, 138);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(204, 69);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Les Previsions";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(88, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Année :";
            // 
            // txtcomboprevision
            // 
            this.txtcomboprevision.Enabled = false;
            this.txtcomboprevision.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcomboprevision.Location = new System.Drawing.Point(348, 76);
            this.txtcomboprevision.Name = "txtcomboprevision";
            this.txtcomboprevision.Size = new System.Drawing.Size(80, 26);
            this.txtcomboprevision.TabIndex = 1;
            // 
            // cmbprevision
            // 
            this.cmbprevision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbprevision.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbprevision.FormattingEnabled = true;
            this.cmbprevision.Location = new System.Drawing.Point(153, 76);
            this.cmbprevision.Name = "cmbprevision";
            this.cmbprevision.Size = new System.Drawing.Size(189, 26);
            this.cmbprevision.TabIndex = 0;
            this.cmbprevision.SelectedIndexChanged += new System.EventHandler(this.cmbprevision_SelectedIndexChanged);
            // 
            // FormRapportComptabilite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 315);
            this.Controls.Add(this.groupControl1);
            this.Name = "FormRapportComptabilite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRapportComptabilite";
            this.Load += new System.EventHandler(this.FormRapportComptabilite_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcomboprevision;
        private System.Windows.Forms.ComboBox cmbprevision;
    }
}