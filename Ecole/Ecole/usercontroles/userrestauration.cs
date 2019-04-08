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
    public partial class userrestauration : UserControl
    {
        public userrestauration()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        connexion ap = new connexion();
        clsDatebaseBackupRestor bd = new clsDatebaseBackupRestor();

        private void btnreset_Click(object sender, EventArgs e)
        {
            ap.connect();
            con = new SqlConnection(ap.chemin);
            string database = con.Database.ToString();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, con);
                bu2.ExecuteNonQuery();

                string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + dbPath.Text + "'WITH REPLACE;";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, con);
                bu3.ExecuteNonQuery();

                string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, con);
                bu4.ExecuteNonQuery();

                XtraMessageBox.Show("Database restoration done successefully", "Confirmation de restauration");
                con.Close();
            }
            catch (Exception exc)
            {
                XtraMessageBox.Show("Erreur de " + exc);
            }

        }

        private void btnBrowseBack_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "SQL SERVER database backup files|*.bak";
            dlg.Title = "Database restore";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dbPath.Text = dlg.FileName;
                btnreset.Enabled = true;
            }
            btnreset.Enabled = true;
        }
    }
}
