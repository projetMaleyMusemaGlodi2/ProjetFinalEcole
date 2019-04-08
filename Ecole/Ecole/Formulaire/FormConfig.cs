using System;
using Ecole.Formulaire;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ecole.Classe;
using System.IO;

namespace Ecole.Formulaire
{
    public partial class FormConfig : Form 
    {

        bool test;

          
        public FormConfig()
        {
            InitializeComponent();            
        }


        String chem;
        void connecter()
        {
            pubCon.dataS = txtServerName.Text;
            pubCon.initcat = txtDatabase.Text;
            pubCon.id = txtCUsername.Text;
            pubCon.pass = txtCPassword.Text;
        }

        
        private void FormConfig_Load(object sender, EventArgs e)
        {

            txtServerName.Items.Add(".");
            txtServerName.Items.Add("(local)");
            txtServerName.Items.Add(@".\SQLEXPRESS");
            txtServerName.Items.Add(string.Format(@"{0}", Environment.MachineName));
            txtServerName.SelectedIndex = 3;
            txtCPassword.UseSystemPasswordChar = true;
            

        }

        private void btfermer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btConfCon_Click(object sender, EventArgs e)
        {
            if (txtCPassword.Text.Trim() == "" || txtCUsername.Text.Trim() == "" || txtDatabase.Text.Trim() == "" || txtServerName.Text.Trim() == "")
            {
                MessageBox.Show("Completer tous les champs necessaires SVP", "Champs Obligatoires", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                connecter();
                chem = "Data Source=" + pubCon.dataS + "; Initial Catalog=" + pubCon.initcat + "; User Id=" + pubCon.id + "; Password=" + pubCon.pass + ";";
                File.WriteAllText(ClassConstantes.Table.chemin, chem.ToString());
                this.Close();
                Visible = false;

            }

        }

        private void btTest_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtCPassword.UseSystemPasswordChar = false;
            }
            else {
                txtCPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
