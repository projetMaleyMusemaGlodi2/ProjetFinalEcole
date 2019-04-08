namespace Ecole.usercontroles
{
    partial class userrestauration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userrestauration));
            this.btnreset = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.btnBrowseBack = new DevExpress.XtraEditors.SimpleButton();
            this.dbPath = new DevExpress.XtraEditors.TextEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnreset
            // 
            this.btnreset.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreset.Appearance.Options.UseFont = true;
            this.btnreset.Image = ((System.Drawing.Image)(resources.GetObject("btnreset.Image")));
            this.btnreset.Location = new System.Drawing.Point(421, 318);
            this.btnreset.Name = "btnreset";
            this.btnreset.Size = new System.Drawing.Size(200, 46);
            this.btnreset.TabIndex = 33;
            this.btnreset.Text = "Restaurer";
            this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(333, 145);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 127);
            this.panel1.TabIndex = 32;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl14);
            this.groupBox2.Controls.Add(this.btnBrowseBack);
            this.groupBox2.Controls.Add(this.dbPath);
            this.groupBox2.Location = new System.Drawing.Point(20, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 75);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Location = new System.Drawing.Point(138, 20);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(193, 17);
            this.labelControl14.TabIndex = 30;
            this.labelControl14.Text = "Parcourir la base des données";
            // 
            // btnBrowseBack
            // 
            this.btnBrowseBack.Appearance.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseBack.Appearance.Options.UseFont = true;
            this.btnBrowseBack.Location = new System.Drawing.Point(57, 18);
            this.btnBrowseBack.Name = "btnBrowseBack";
            this.btnBrowseBack.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseBack.TabIndex = 19;
            this.btnBrowseBack.Text = "Parcourir";
            this.btnBrowseBack.Click += new System.EventHandler(this.btnBrowseBack_Click);
            // 
            // dbPath
            // 
            this.dbPath.Location = new System.Drawing.Point(57, 47);
            this.dbPath.Name = "dbPath";
            this.dbPath.Properties.ReadOnly = true;
            this.dbPath.Size = new System.Drawing.Size(291, 20);
            this.dbPath.TabIndex = 31;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(19, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(308, 317);
            this.panel2.TabIndex = 31;
            // 
            // userrestauration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.btnreset);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "userrestauration";
            this.Size = new System.Drawing.Size(782, 523);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbPath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnreset;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.SimpleButton btnBrowseBack;
        private DevExpress.XtraEditors.TextEdit dbPath;
        private System.Windows.Forms.Panel panel2;
    }
}
