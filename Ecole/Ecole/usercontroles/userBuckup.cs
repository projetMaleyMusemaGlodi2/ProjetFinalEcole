using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Ecole.Classe;
using DevExpress.XtraEditors;

namespace Ecole.usercontroles
{
    public partial class userBuckup : UserControl
    {
        public userBuckup()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        clsDatebaseBackupRestor bd = new clsDatebaseBackupRestor();
        connexion ap = new connexion();


        private void btnParcourir_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    personalizePath.Text = dlg.SelectedPath;
                    btnSauvegarde.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            btnParcourir.Enabled = false;
            btnSauvegarde.Enabled = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            btnSauvegarde.Enabled = false;
            btnParcourir.Enabled = true;
        }

        private void userBuckup_Load(object sender, EventArgs e)
        {
            try
            {
                defaultPath.Text = bd.getBackupPath(radioButton3, personalizePath);
            }
            catch (Exception)
            { }
        }

        private void btnSauvegarde_Click(object sender, EventArgs e)
        {
            try
            {
                ap.connect();
                con = new SqlConnection(ap.chemin);
                string database = con.Database.ToString();

                if (bd.getBackupPath(radioButton3, personalizePath) == string.Empty)
                {
                    XtraMessageBox.Show("Veuillez selectionner d'abord un emplacement s.v.p.!");
                }
                else
                {

                    string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + bd.getBackupPath(radioButton3, personalizePath) + "\\" + database + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                    using (SqlCommand command = new SqlCommand(cmd, con))
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        command.ExecuteNonQuery();
                        con.Close();
                        XtraMessageBox.Show("Sauvegarde effectué avec succés", "Confirmation Sauvegarde");
                    }
                }

            }
            catch (Exception exc)
            {
                XtraMessageBox.Show(exc.Message);
            }
        }
    }
}
