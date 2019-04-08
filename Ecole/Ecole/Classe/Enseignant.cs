using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecole
{
    public class Enseignant
    {
        OpenFileDialog ofdImage = new OpenFileDialog();

        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        connexion ap = new connexion();

       
        int grame = DateTime.Now.Hour;
        int grame1 = DateTime.Now.Minute;
        int grame2 = DateTime.Now.Second;

        // ===============================================================================================

        public void mergeenseignant(string codeel, string nom, string postnom, string prenom, string sexe, string mail, string num, string domaine, string qualif, string etat, Image im1)
        {

            try
            {
                


                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(im1);
                byte[] bytImage;
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                bytImage = ms.ToArray();
                ms.Close();

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec mergeenseignant1 @a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@o", myconn);
                mycomm.Parameters.AddWithValue("@a", codeel);
                mycomm.Parameters.AddWithValue("@b", nom);
                mycomm.Parameters.AddWithValue("@c", postnom);
                mycomm.Parameters.AddWithValue("@d", prenom);
                mycomm.Parameters.AddWithValue("@e", sexe);
                mycomm.Parameters.AddWithValue("@f", mail);
                mycomm.Parameters.AddWithValue("@g", num);
                mycomm.Parameters.AddWithValue("@h", domaine);
                mycomm.Parameters.AddWithValue("@i", qualif);
                mycomm.Parameters.AddWithValue("@j", etat);
                mycomm.Parameters.AddWithValue("@o", bytImage);

                mycomm.ExecuteNonQuery();
               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }

        public void chargementEns(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select matriculeEns,NomEns,postnomEns,prenomEns,sexeEns,Mail,numtel,Domaine,qualification,etacivil from Enseignant", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }

        


        public void supprimerEns(string codeeleve)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from Enseignant WHERE matriculeEns ='" + codeeleve + "'", myconn);
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


        public void rechercheEns(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select matriculeEns,NomEns,postnomEns,prenomEns,sexeEns,Mail,numtel,Domaine,qualification,etacivil from Enseignant WHERE NomEns LIKE '%" + recherche + "%' OR postnomEns LIKE '%" + recherche + "%' OR prenomEns LIKE '%" + recherche + "%' OR matriculeEns LIKE '%" + recherche + "%'OR sexeEns LIKE '%" + recherche + "%'OR Mail LIKE '%" + recherche + "%'OR numtel LIKE '%" + recherche + "%'OR Domaine LIKE '%" + recherche + "%'OR qualification LIKE '%" + recherche + "%'OR etacivil LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
          

        }

        public void rechercheEns_message(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select matriculeEns,NomEns,postnomEns,numtel from Enseignant WHERE NomEns LIKE '%" + recherche + "%' OR postnomEns LIKE '%" + recherche + "%' OR prenomEns LIKE '%" + recherche + "%' OR matriculeEns LIKE '%" + recherche + "%'OR sexeEns LIKE '%" + recherche + "%'OR Mail LIKE '%" + recherche + "%'OR numtel LIKE '%" + recherche + "%'OR Domaine LIKE '%" + recherche + "%'OR qualification LIKE '%" + recherche + "%'OR etacivil LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();          


        }

        // GESTION DE LA CAISSE
   //=========================================================================================

        public DataTable chargement_solde_caisse()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter(" SELECT * FROM return_solde()", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        // ENREGISTREMENT DES RESSOURCES 

        public void insertion_ressource(double montant, string motif,string compte)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec merge_entree @a,@b,@c", myconn);
                mycomm.Parameters.AddWithValue("@a", montant);
                mycomm.Parameters.AddWithValue("@b", motif);
                mycomm.Parameters.AddWithValue("@c", compte);
                mycomm.ExecuteNonQuery();
              
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        public DataTable chargement_ressource()
        {
            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter(" select * from entree", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }


       

        public void supprimer_ressource(string codedet)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from entree WHERE code_ent ='" + codedet + "'", myconn);
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

        //=========================================================================================
        //ENREGISTREMENT DES DEPENSES

        public void insertion_depense(double montant, string motif,string lettre,string faveur,string autoriser,string comptable,string acquit)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec merge_sortie @a,@b,@c,@d,@e,@f,@g", myconn);
                mycomm.Parameters.AddWithValue("@a", montant);
                mycomm.Parameters.AddWithValue("@b", motif);
                mycomm.Parameters.AddWithValue("@c", lettre);
                mycomm.Parameters.AddWithValue("@d", faveur);
                mycomm.Parameters.AddWithValue("@e", autoriser);
                mycomm.Parameters.AddWithValue("@f", comptable);
                mycomm.Parameters.AddWithValue("@g", acquit);
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public DataTable chargement_depense()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from depense", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }




        public void supprimer_depense(string codedet)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from depense WHERE code_dep ='" + codedet + "'", myconn);
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

        public void chargementcombocodecomptable(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeutil FROM utilisateur", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["codeutil"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }

        public void chargementcombocodecompte(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT utilisateur FROM utilisateur", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["utilisateur"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }
        public void saisir_code_comptable(TextBox txt,string comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM utilisateur where utilisateur= '" + comb1+"'", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    txt.Text=(dr["codeutil"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex,"Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }






        //===========================================================================================
        //LES UTILISATEURS

        public void insertion_utilisateur(int code,string nom, string fonction,string pass)
        {

            try
            {
                
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec inserer_user @a,@b,@c,@d", myconn);
                mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", nom);
                mycomm.Parameters.AddWithValue("@c", fonction);
                mycomm.Parameters.AddWithValue("@d", pass);
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!","Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public DataTable chargement_utilisateur()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select code,nom,fonction,pass from utilisateur1", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }




        public void supprimer_utilisateur(string codedet)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from utilisateur1 WHERE code ='" + codedet + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();

                myconn.Close();
                MessageBox.Show("La suppression a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!!","Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public void chargementcombo_utilisateur_login(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT nom FROM utilisateur1", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["nom"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }



        //========================================================================================
        //PROCEDURE DE LA MISE A JOURS

        public DataTable chargement_mise_a_jours()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("exec mise_a_jours", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        //============================================================================================
        //LES HISTORIQUES DE L'ECOLE


        public DataTable chargement_historique_paiement()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from historique_paiement", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        public DataTable chargement_historique_cotation()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from historique_cotation", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }




        public DataTable chargement_historique_bibliothque()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from historique_biblitheque", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;
        }


        public void recherche_historique_inscription(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM historique_inscription WHERE matricule_eleve LIKE '%" + recherche + "%' OR nom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR annee LIKE '%" + recherche + "%'OR classe LIKE '%" + recherche + "%'OR division LIKE '%" + recherche + "%'OR section LIKE '%" + recherche + "%'OR optioneleve LIKE '%" + recherche + "%'OR code_annee LIKE '%" + recherche + "%'OR code_classe LIKE '%" + recherche + "%'OR code_eleve LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();

        }


        public void recherche_historique_paiement(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM historique_paiement WHERE datepay LIKE '%" + recherche + "%' OR nom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR prenom LIKE '%" + recherche + "%'OR montantpay LIKE '%" + recherche + "%'OR division LIKE '%" + recherche + "%'OR codeclasse LIKE '%" + recherche + "%'OR section LIKE '%" + recherche + "%'OR optioneleve LIKE '%" + recherche + "%'OR typefrais LIKE '%" + recherche + "%'OR utilisateur LIKE '%" + recherche + "%' OR num LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();

        }

        public void recherche_historique_cotation(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM historique_cotation WHERE codeeleve LIKE '%" + recherche + "%' OR nom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR prenom LIKE '%" + recherche + "%'OR sexe LIKE '%" + recherche + "%'OR classe LIKE '%" + recherche + "%'OR section LIKE '%" + recherche + "%'OR option_eleve LIKE '%" + recherche + "%'OR codeannee LIKE '%" + recherche + "%'OR annee LIKE '%" + recherche + "%'OR lieu_naiss LIKE '%" + recherche + "%' OR mat_eleve LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();

        }


        public void recherche_historique_bibliotheque(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM historique_biblitheque WHERE Reference_inscription LIKE '%" + recherche + "%' OR Nom LIKE '%" + recherche + "%' OR PostNom LIKE '%" + recherche + "%' OR Prenom LIKE '%" + recherche + "%'OR Classe LIKE '%" + recherche + "%'OR Section LIKE '%" + recherche + "%'OR Option_eleve LIKE '%" + recherche + "%'OR Titre_livre LIKE '%" + recherche + "%'OR Date_retrait LIKE '%" + recherche + "%'OR date_retour LIKE '%" + recherche + "%'OR Signature_eleve LIKE '%" + recherche + "%' OR Signature_Biblithecaire LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();

        }
        //==================================================================================================================================================
        // POUR LES COMPTABLE

        public void insertion_comptable(string  code, string nom)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec merge_comptable @a,@b", myconn);
                mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", nom);                
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public DataTable chargement_comptable()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter(" select * from utilisateur", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }




        public void supprimer_comptable(string codedet)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from utilisateur WHERE codeutil='" + codedet + "'", myconn);
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

    }
}
