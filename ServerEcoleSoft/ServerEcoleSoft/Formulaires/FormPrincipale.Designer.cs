namespace ServerEcoleSoft.Formulaires
{
    partial class FormPrincipale
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.accueilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fermerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messagerieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transmissionMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvegardeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvegardeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restaurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accueilToolStripMenuItem,
            this.messagerieToolStripMenuItem,
            this.sauvegardeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(850, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // accueilToolStripMenuItem
            // 
            this.accueilToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fermerToolStripMenuItem});
            this.accueilToolStripMenuItem.Name = "accueilToolStripMenuItem";
            this.accueilToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.accueilToolStripMenuItem.Text = "Accueil";
            // 
            // fermerToolStripMenuItem
            // 
            this.fermerToolStripMenuItem.Name = "fermerToolStripMenuItem";
            this.fermerToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.fermerToolStripMenuItem.Text = "Fermer";
            // 
            // messagerieToolStripMenuItem
            // 
            this.messagerieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transmissionMessagesToolStripMenuItem});
            this.messagerieToolStripMenuItem.Name = "messagerieToolStripMenuItem";
            this.messagerieToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.messagerieToolStripMenuItem.Text = "Messagerie";
            // 
            // transmissionMessagesToolStripMenuItem
            // 
            this.transmissionMessagesToolStripMenuItem.Name = "transmissionMessagesToolStripMenuItem";
            this.transmissionMessagesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.transmissionMessagesToolStripMenuItem.Text = "Transmission Messages";
            this.transmissionMessagesToolStripMenuItem.Click += new System.EventHandler(this.transmissionMessagesToolStripMenuItem_Click);
            // 
            // sauvegardeToolStripMenuItem
            // 
            this.sauvegardeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sauvegardeToolStripMenuItem1,
            this.restaurationToolStripMenuItem});
            this.sauvegardeToolStripMenuItem.Name = "sauvegardeToolStripMenuItem";
            this.sauvegardeToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.sauvegardeToolStripMenuItem.Text = "Sauvegarde";
            // 
            // sauvegardeToolStripMenuItem1
            // 
            this.sauvegardeToolStripMenuItem1.Name = "sauvegardeToolStripMenuItem1";
            this.sauvegardeToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.sauvegardeToolStripMenuItem1.Text = "Sauvegarde";
            this.sauvegardeToolStripMenuItem1.Click += new System.EventHandler(this.sauvegardeToolStripMenuItem1_Click);
            // 
            // restaurationToolStripMenuItem
            // 
            this.restaurationToolStripMenuItem.Name = "restaurationToolStripMenuItem";
            this.restaurationToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.restaurationToolStripMenuItem.Text = "Restauration";
            this.restaurationToolStripMenuItem.Click += new System.EventHandler(this.restaurationToolStripMenuItem_Click);
            // 
            // FormPrincipale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 346);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormPrincipale";
            this.Text = "FormPrincipale";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipale_FormClosing);
            this.Load += new System.EventHandler(this.FormPrincipale_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem accueilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fermerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem messagerieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transmissionMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvegardeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvegardeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restaurationToolStripMenuItem;
    }
}