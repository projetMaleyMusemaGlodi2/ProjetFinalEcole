using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecole
{
    public class bibliotheque
    {
        OpenFileDialog ofdImage = new OpenFileDialog();

        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        connexion ap = new connexion();
       
        int grame = DateTime.Now.Hour;
        int grame1 = DateTime.Now.Minute;
        int grame2 = DateTime.Now.Second;
       

        public void insertion_livre(string code, string titre, string auteur, string etat, double nombre)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec merge_livre @a,@b,@c,@d,@e", myconn);
                mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", titre);
                mycomm.Parameters.AddWithValue("@c", auteur);
                mycomm.Parameters.AddWithValue("@d", etat);
                mycomm.Parameters.AddWithValue("@e", nombre);               
                mycomm.ExecuteNonQuery();              

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        public DataTable chargement_livre()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select Top 100 code_livre,titre_livre,auteur,etat_livre,nombre_livre from livre", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }



      
        public void supprimer_livre(string codedet)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from livre WHERE code_livre ='" + codedet + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();

                myconn.Close();
                MessageBox.Show("La suppression a reussie !!! ", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!! ", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }   
       
  
        public void chargementcombocodelivre(ComboBox comb1,string recherche)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT titre_livre FROM livre where titre_livre LIKE '%" + recherche + "%'", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Insert(0,(dr["titre_livre"]));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
            
        }

        public void chargementcombocodelivre(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT titre_livre FROM livre ", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add (dr["titre_livre"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }

        public void chargementcombocodelivre_saisir(ComboBox comb1,TextBox texte,string code)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM livre where titre_livre='"+code+"'", myconn);
            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    texte.Text=(dr["code_livre"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }



        //========================================================================================
        //les emprunts


        public void insertion_emprunt(int inscription, string livre, DateTime date_emprunt, DateTime date_retour,int nombre)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec merge_empruntLivreFinal @a,@b,@c,@d,@e", myconn);
                mycomm.Parameters.AddWithValue("@a", inscription);
                mycomm.Parameters.AddWithValue("@b", livre);
                mycomm.Parameters.AddWithValue("@c", date_emprunt);
                mycomm.Parameters.AddWithValue("@d", date_retour);
                mycomm.Parameters.AddWithValue("@e", nombre);
                mycomm.ExecuteNonQuery();
                //chergementclient();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public DataTable chargement_emprunt(string Annee)
        {
           
                connexion ap = new connexion();
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                DataTable table = new DataTable();
                adpt1 = new SqlDataAdapter("select * from emprunt_bibliotheque where codeanne = '"+Annee+"' ", myconn);
                adpt1.Fill(table);

                myconn.Close();               
           
            return table;


        }               


        public void supprimer_emprunt(string codedet)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from emprunt_livre WHERE num ='" + codedet + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();

                myconn.Close();
                MessageBox.Show("La suppression a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }

        //=====================================================================================
        // ENREGISTREMENT RETOUR

        public void insertion_remise(int ref_emp, int nombre,string livre)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("exec merge_RemiseLivreFinal @a,@b,@c", myconn);
                mycomm.Parameters.AddWithValue("@a", ref_emp);
                mycomm.Parameters.AddWithValue("@b", nombre);
                mycomm.Parameters.AddWithValue("@c", livre);
                mycomm.ExecuteNonQuery();
                //chergementclient();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public DataTable chargement_remise_livre(string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from remise_bibliotheque where codeanne = '"+Annee+"' ", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        public DataTable chargement_recherche_emprunt_livre(string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from recharche_emprunt where codeanne = '"+Annee+"' ", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }


        public void supprimer_remise_livre(string codedet)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from remise_livre WHERE code_retour ='" + codedet + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();

                myconn.Close();
                MessageBox.Show("La suppression a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        //===========================================================================================
        //  

    }
}
