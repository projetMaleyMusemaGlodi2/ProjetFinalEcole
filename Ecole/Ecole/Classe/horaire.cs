using DevExpress.XtraGrid;
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
    public class horaire
    {
        OpenFileDialog ofdImage = new OpenFileDialog();

        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        connexion ap = new connexion();

        int grame = DateTime.Now.Hour;
        int grame1 = DateTime.Now.Minute;
        int grame2 = DateTime.Now.Second;

        public void insertion_detail_horaire(string lundi, string mardi, string mercredi, string jeudi, string vendredi,string samedi)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO Detail_horaire(lundi,mardi,mercredi,jeudi,vendredi,samedi)VALUES(@a,@b,@c,@d,@e,@f)", myconn);
                mycomm.Parameters.AddWithValue("@a", lundi);
                mycomm.Parameters.AddWithValue("@b", mardi);
                mycomm.Parameters.AddWithValue("@c", mercredi);
                mycomm.Parameters.AddWithValue("@d", jeudi);
                mycomm.Parameters.AddWithValue("@e", vendredi);
                mycomm.Parameters.AddWithValue("@f", samedi);
                mycomm.ExecuteNonQuery();
               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public void chargement_detail_horaire(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select codedetail,lundi,mardi,mercredi,jeudi,vendredi,samedi from Detail_horaire ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void recherche_detail_horaire(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select codedetail,lundi,mardi,mercredi,jeudi,vendredi,samedi from Detail_horaire  where codedetail LIKE '%" + recherche + "%'OR lundi LIKE '%" + recherche + "%'OR mardi LIKE '%" + recherche + "%'OR mercredi LIKE '%" + recherche + "%'OR jeudi LIKE '%" + recherche + "%'OR vendredi LIKE '%" + recherche + "%'OR samedi LIKE '%" + recherche + "%'", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
          

        }

        public void supprimer_detail_horaire(string codedet)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from Detail_horaire WHERE codedetail ='" + codedet + "'", myconn);
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



        public void modifier_detail_horaire(string code, string lundi, string mardi, string mercredi, string jeudi, string vendredi,string samedi)
        {


            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("UpDate Detail_horaire Set lundi='" +lundi+ "', mardi='" +mardi+ "', mercredi='" +mercredi+ "', jeudi='" +jeudi+ "', vendredi='" +vendredi+ "', samedi='" +samedi+ "' Where codedetail='" + code + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();

                myconn.Close();



                MessageBox.Show("La modification a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("La modification a echouee !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }
        //===================================================================================
        // POUR L'HORAIRE

        //public void insertion_horaire_co(int code, string classe, int jours, string cours, string heure_debut,string heure_fin,string ens,int anne,string affectation)
        //{
            
        //    try
        //    {

        //        ap.connect();
        //        myconn = new SqlConnection(ap.chemin);
        //        myconn.Open();

        //        mycomm = new SqlCommand("exec merge_horaire_co @a,@b,@c,@d,@e,@f,@g,@h,@i", myconn);
        //        mycomm.Parameters.AddWithValue("@a", code);
        //        mycomm.Parameters.AddWithValue("@b", classe);
        //        mycomm.Parameters.AddWithValue("@c", jours);
        //        mycomm.Parameters.AddWithValue("@d", cours);
        //        mycomm.Parameters.AddWithValue("@e", heure_debut);
        //        mycomm.Parameters.AddWithValue("@f", heure_fin);
        //        mycomm.Parameters.AddWithValue("@g", ens);
        //        mycomm.Parameters.AddWithValue("@h", anne);
        //        mycomm.Parameters.AddWithValue("@i", affectation);
        //        mycomm.ExecuteNonQuery();               
        //        MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        //    }

        //}



        //public void insertion_horaire_moyen(int code, string classe, int jours, string cours, string heure_debut, string heure_fin, string ens, int anne,string section, string affectation)
        //{

        //    try
        //    {

        //        ap.connect();
        //        myconn = new SqlConnection(ap.chemin);
        //        myconn.Open();

        //        mycomm = new SqlCommand("exec merge_horaire_moyen @a,@b,@c,@d,@e,@f,@g,@h,@i,@j", myconn);
        //        mycomm.Parameters.AddWithValue("@a", code);
        //        mycomm.Parameters.AddWithValue("@b", classe);
        //        mycomm.Parameters.AddWithValue("@c", jours);
        //        mycomm.Parameters.AddWithValue("@d", cours);
        //        mycomm.Parameters.AddWithValue("@e", heure_debut);
        //        mycomm.Parameters.AddWithValue("@f", heure_fin);
        //        mycomm.Parameters.AddWithValue("@g", ens);
        //        mycomm.Parameters.AddWithValue("@h", anne);
        //        mycomm.Parameters.AddWithValue("@i", section);
        //        mycomm.Parameters.AddWithValue("@j", affectation);
        //        mycomm.ExecuteNonQuery();
                
        //        MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        //    }

        //}

        public void insertion_horaire_final(int code, string classe,string option, int jours, string cours, string heure_debut, string heure_fin, string ens, int anne, string section, string affectation,int RefEcole)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec merge_horaire_final @a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l", myconn);
                mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", classe);
                mycomm.Parameters.AddWithValue("@c", option);
                mycomm.Parameters.AddWithValue("@d", jours);
                mycomm.Parameters.AddWithValue("@e", cours);
                mycomm.Parameters.AddWithValue("@f", heure_debut);
                mycomm.Parameters.AddWithValue("@g", heure_fin);
                mycomm.Parameters.AddWithValue("@h", ens);
                mycomm.Parameters.AddWithValue("@i", anne);
                mycomm.Parameters.AddWithValue("@j", section);
                mycomm.Parameters.AddWithValue("@k", affectation);
                mycomm.Parameters.AddWithValue("@l", RefEcole);
                mycomm.ExecuteNonQuery();
                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        //public void insertion_horaire_co1( string classe, int jours, string cours, string heure_debut, string heure_fin, string ens, int anne, string affectation)
        //{

        //    try
        //    {

        //        ap.connect();
        //        myconn = new SqlConnection(ap.chemin);
        //        myconn.Open();

        //        mycomm = new SqlCommand("exec merge_horaire_co1 @b,@c,@d,@e,@f,@g,@h,@i", myconn);               
        //        mycomm.Parameters.AddWithValue("@b", classe);
        //        mycomm.Parameters.AddWithValue("@c", jours);
        //        mycomm.Parameters.AddWithValue("@d", cours);
        //        mycomm.Parameters.AddWithValue("@e", heure_debut);
        //        mycomm.Parameters.AddWithValue("@f", heure_fin);
        //        mycomm.Parameters.AddWithValue("@g", ens);
        //        mycomm.Parameters.AddWithValue("@h", anne);
        //        mycomm.Parameters.AddWithValue("@i", affectation);
        //        mycomm.ExecuteNonQuery();               
        //        MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        //    }

        //}



        //public void insertion_horaire_moyen1(string classe, int jours, string cours, string heure_debut, string heure_fin, string ens, int anne, string section, string affectation)
        //{

        //    try
        //    {

        //        ap.connect();
        //        myconn = new SqlConnection(ap.chemin);
        //        myconn.Open();

        //        mycomm = new SqlCommand("exec merge_horaire_moyen1 @b,@c,@d,@e,@f,@g,@h,@i,@j", myconn);
        //        mycomm.Parameters.AddWithValue("@b", classe);
        //        mycomm.Parameters.AddWithValue("@c", jours);
        //        mycomm.Parameters.AddWithValue("@d", cours);
        //        mycomm.Parameters.AddWithValue("@e", heure_debut);
        //        mycomm.Parameters.AddWithValue("@f", heure_fin);
        //        mycomm.Parameters.AddWithValue("@g", ens);
        //        mycomm.Parameters.AddWithValue("@h", anne);
        //        mycomm.Parameters.AddWithValue("@i", section);
        //        mycomm.Parameters.AddWithValue("@j", affectation);
        //        mycomm.ExecuteNonQuery();
               
        //        MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        //    }

        //}

        public void insertion_horaire_final1(string classe, string option, int jours, string cours, string heure_debut, string heure_fin, string ens, int anne, string section, string affectation,int RefEcole)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("exec merge_horaire_final1 @b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l", myconn);
                mycomm.Parameters.AddWithValue("@b", classe);
                mycomm.Parameters.AddWithValue("@c", option);
                mycomm.Parameters.AddWithValue("@d", jours);
                mycomm.Parameters.AddWithValue("@e", cours);
                mycomm.Parameters.AddWithValue("@f", heure_debut);
                mycomm.Parameters.AddWithValue("@g", heure_fin);
                mycomm.Parameters.AddWithValue("@h", ens);
                mycomm.Parameters.AddWithValue("@i", anne);
                mycomm.Parameters.AddWithValue("@j", section);
                mycomm.Parameters.AddWithValue("@k", affectation);
                mycomm.Parameters.AddWithValue("@l", RefEcole);
                mycomm.ExecuteNonQuery();               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }










        public void chargement_horaire(GridControl data1,string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select Top 100 codeaffect,codeclasse,classe,codeop,optioneleve,codejours, design_jours, ccours,heure_debut, heure_fin, codeens, NomEns, postnomEns,codeanne, code_section, section,affectation, annee, RefEcole, nomEcol from horaire where codeanne = '"+Annee+"' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void recherche_horaire(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from horaire where codeaffect LIKE '%" + recherche + "%'OR codeclasse LIKE '%" + recherche + "%'OR optioneleve LIKE '%" + recherche + "%'OR design_jours LIKE '%" + recherche + "%'OR ccours LIKE '%" + recherche + "%'OR ccours LIKE '%" + recherche + "%'OR classe LIKE '%" + recherche + "%'OR heure_debut LIKE '%" + recherche + "%'OR heure_fin LIKE '%" + recherche + "%'OR NomEns LIKE '%" + recherche + "%'OR postnomEns LIKE '%" + recherche + "%'OR codeanne LIKE '%" + recherche + "%'", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
           

        }


        public void supprimer_horaire(string codedet)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from affect_horaire WHERE codeaffect ='" + codedet + "'", myconn);
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



        public void modifier_horaire(string code, int classe, int option, int jours, string cours, string heure_debut, string heure_fin,string ens,int annee )
        {
            
            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("UpDate affect_horaire Set codeclasse='" + classe+ "', codeop='" + option + "', codejours='" +jours+ "', ccours='" +cours+ "', heure_debut='" + heure_debut+ "', heure_fin='" + heure_fin + "', codeens='" + ens + "', codeanne='" + annee + "' Where codeaffect='" + code + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();

                myconn.Close();



                MessageBox.Show("La modification a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("La modification a echouee !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        //=================================================================================================
        //POUR L'AFFECTATION DE L'HORAIRE

        public void chargementcombo_jours_designe(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT design_jours FROM jours", myconn);
            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    comb1.Items.Add(dr["design_jours"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            myconn.Close();         
        }

        public void chargementcombo_jours_saisir(ComboBox comb1, TextBox texte, string code)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM jours where design_jours = '"+code+"'", myconn);
            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                  texte.Text=(dr["codejours"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            myconn.Close();
        }




        public void insertion_affectation_horaire(int horaire,string classe)
        {
           
            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO jours(codejours,design_jours)VALUES(@a,@b)", myconn);
                mycomm.Parameters.AddWithValue("@a", horaire);
                mycomm.Parameters.AddWithValue("@b", classe);                
                mycomm.ExecuteNonQuery();                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public void chargement_affectation_horaire(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from jours", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void recherche_affectation_horaire(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from jours where codejours LIKE '%" + recherche + "%'OR design_jours LIKE '%" + recherche + "%'", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
           
        }


        public void supprimer_affectation_horaire(string codedet)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from jours WHERE codejours ='" + codedet + "'", myconn);
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



        public void modifier_affectation_horaire(int horaire,int classe)
        {
            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("UpDate jours Set jours='" + classe + "' Where codejours='" + horaire + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();
                myconn.Close();

                MessageBox.Show("La modification a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("La modification a echouee !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


    }
}
