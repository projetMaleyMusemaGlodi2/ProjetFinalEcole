using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecole
{
    public partial class Login1 : Form
    {
        public Login1()
        {
            InitializeComponent();
        }

        connexion ap = new connexion();
        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();


       
        public void login_user(string nom, string password,string fonction_l)
        {

            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();

            //cn.Open();
            SqlCommand cmd = new SqlCommand("select * from utilisateur1 where nom='" + nom + "'and pass='" + password + "'and fonction='" + fonction_l + "'", myconn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0; string fonction ="";
            while (dr.Read())
            {
                fonction = dr["fonction"].ToString();
                count += 1;
            }

            if (count == 1)
            {
                MessageBox.Show("La connection a reussie !!!!!!");
                
                UserSession.GetInstance().UserName = nom;
                UserSession.GetInstance().AccessLevel = fonction;


                Ecole1 f1 = new Ecole1();
                f1.Show();
                Visible = false;
              
            }
            else if (count > 1)
            {
                MessageBox.Show("duplicate");
            }
            else
            {

                MessageBox.Show("Echec de connection !!!!!!!!!");
            }

            //username



        }



        private String fonction;

        public string Fonction
        {
            get
            {
                return fonction;
            }

            set
            {
                fonction = value;
            }
        }

        public string retour_user()
        {
            Fonction = name.Text;
            string pass1 = pass.Text;
            string fonction = fonction1.Text;
            login_user(Fonction, pass1,fonction);

            return Fonction;
        }


        public void getcon()
        {

            //String nom = name.Text;
            Fonction = name.Text; ;
            String pass1 = pass.Text;
            string fonction = fonction1.Text;
            login_user(Fonction, pass1,fonction);

        }

            


        public static string retour;


        private void button1_Click(object sender, EventArgs e)
        {
            retour = retour_user();
            //text_user.Text = retour;
            name.Text = "";
            pass.Text = "";
            fonction1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
