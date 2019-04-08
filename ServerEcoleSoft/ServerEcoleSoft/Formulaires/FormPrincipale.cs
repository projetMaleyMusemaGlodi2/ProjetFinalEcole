using ServerEcoleSoft.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerEcoleSoft.Formulaires
{
    public partial class FormPrincipale : Form
    {
        public FormPrincipale()
        {
            InitializeComponent();
        }

        private void transmissionMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //foreach (Form child in this.MdiChildren)
            //{
            //    child.Close();
            //}
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void FormPrincipale_Load(object sender, EventArgs e)
        {
            //foreach (Form child in this.MdiChildren)
            //{
            //    child.Close();
            //}
            Form1 f1 = new Form1();
            f1.ShowDialog();

            restaurationToolStripMenuItem.Enabled = false;
        }

        private void sauvegardeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //foreach (Form child in this.MdiChildren)
            //{
            //    child.Close();
            //}
            FormSauvegarde f1 = new FormSauvegarde();
            f1.ShowDialog();
        }

        private void restaurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //foreach (Form frm in MdiChildren)
            //{
            //    frm.Close();
            //}
            FormRestauration f1 = new FormRestauration();
            f1.ShowDialog();
        }

        private void FormPrincipale_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClsGlossiaires.GetInstance().backupBD();
        }
    }
}
