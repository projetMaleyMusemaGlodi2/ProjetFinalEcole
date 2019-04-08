namespace Ecole.usercontroles
{
    partial class userBuckup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userBuckup));
            this.btnSauvegarde = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnParcourir = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.personalizePath = new DevExpress.XtraEditors.TextEdit();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.defaultPath = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personalizePath.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSauvegarde
            // 
            this.btnSauvegarde.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSauvegarde.Appearance.Options.UseFont = true;
            this.btnSauvegarde.Image = ((System.Drawing.Image)(resources.GetObject("btnSauvegarde.Image")));
            this.btnSauvegarde.Location = new System.Drawing.Point(482, 389);
            this.btnSauvegarde.Name = "btnSauvegarde";
            this.btnSauvegarde.Size = new System.Drawing.Size(176, 43);
            this.btnSauvegarde.TabIndex = 33;
            this.btnSauvegarde.Text = "Sauvegarde";
            this.btnSauvegarde.Click += new System.EventHandler(this.btnSauvegarde_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(367, 138);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 239);
            this.panel1.TabIndex = 32;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButton4.Location = new System.Drawing.Point(233, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(200, 21);
            this.radioButton4.TabIndex = 38;
            this.radioButton4.Text = "Emplacement personnalisé";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnParcourir);
            this.groupBox2.Controls.Add(this.labelControl12);
            this.groupBox2.Controls.Add(this.personalizePath);
            this.groupBox2.Location = new System.Drawing.Point(49, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 75);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // btnParcourir
            // 
            this.btnParcourir.Appearance.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParcourir.Appearance.Options.UseFont = true;
            this.btnParcourir.Enabled = false;
            this.btnParcourir.Location = new System.Drawing.Point(80, 11);
            this.btnParcourir.Name = "btnParcourir";
            this.btnParcourir.Size = new System.Drawing.Size(75, 23);
            this.btnParcourir.TabIndex = 34;
            this.btnParcourir.Text = "Parcourir";
            this.btnParcourir.Click += new System.EventHandler(this.btnParcourir_Click);
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl12.Location = new System.Drawing.Point(161, 15);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(174, 17);
            this.labelControl12.TabIndex = 35;
            this.labelControl12.Text = "Emplacement personnalisé";
            // 
            // personalizePath
            // 
            this.personalizePath.Enabled = false;
            this.personalizePath.Location = new System.Drawing.Point(81, 40);
            this.personalizePath.Name = "personalizePath";
            this.personalizePath.Size = new System.Drawing.Size(254, 20);
            this.personalizePath.TabIndex = 0;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButton3.Location = new System.Drawing.Point(15, 19);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(190, 21);
            this.radioButton3.TabIndex = 37;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Emplcaement par defaut";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl13);
            this.groupBox1.Controls.Add(this.defaultPath);
            this.groupBox1.Location = new System.Drawing.Point(49, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 84);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl13.Location = new System.Drawing.Point(125, 19);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(164, 17);
            this.labelControl13.TabIndex = 32;
            this.labelControl13.Text = "Emplacement par defaut";
            // 
            // defaultPath
            // 
            this.defaultPath.Enabled = false;
            this.defaultPath.Location = new System.Drawing.Point(81, 42);
            this.defaultPath.Name = "defaultPath";
            this.defaultPath.Size = new System.Drawing.Size(270, 20);
            this.defaultPath.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(23, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(338, 317);
            this.panel2.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(145, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(489, 29);
            this.label1.TabIndex = 35;
            this.label1.Text = "SAUVEGARDE LA BASE DES DONNEES";
            // 
            // userBuckup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSauvegarde);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "userBuckup";
            this.Size = new System.Drawing.Size(867, 479);
            this.Load += new System.EventHandler(this.userBuckup_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personalizePath.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSauvegarde;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton btnParcourir;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit personalizePath;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private System.Windows.Forms.TextBox defaultPath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
    }
}
