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
   public class parametre
    {

        OpenFileDialog ofdImage = new OpenFileDialog();

        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        connexion ap = new connexion();      
        int grame = DateTime.Now.Hour;
        int grame1 = DateTime.Now.Minute;
        int grame2 = DateTime.Now.Second;        


        //                                 PARAMETRE ANNEE
  //--------------------------------------------------------------------------------------------------------

        public void insertionannee(int codeanne,string anne) {

            try
            {          
              
                    ap.connect();
                    myconn = new SqlConnection(ap.chemin);
                    myconn.Open();

                    mycomm = new SqlCommand("INSERT INTO annee(codeanne,annee)VALUES(@a,@b)", myconn);
                    mycomm.Parameters.AddWithValue("@a", codeanne);
                    mycomm.Parameters.AddWithValue("@b", anne);
                    mycomm.ExecuteNonQuery();                   
                    MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }


        public void chargementanne(DataGridView data1) {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeanne,annee FROM annee", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }



        public void supprimerannee(string codeanne) {
         try { 
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();

            mycomm = new SqlCommand("DELETE  from annee WHERE codeanne ='" + codeanne + "'", myconn);
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

        //===================================================================================
        //PARAMETRE TYPE FRAIS

        public void insertion_frais(string codefrais, string frais)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO frais VALUES (@a,@b)", myconn);
                mycomm.Parameters.AddWithValue("@a", codefrais);
                mycomm.Parameters.AddWithValue("@b", frais);
                mycomm.ExecuteNonQuery();
               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


        public void chargement_frais(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codefrais,frais FROM frais", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }



        public void supprimer_frais(string codefrais)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from frais WHERE codefrais ='" + codefrais + "'", myconn);
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



        //------------------------------------------------------------------------------------------------------------

        //    PARAMETRE CLASSE


        public void insertionclasse(string codecl,string classe)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO classe(codecl,classe)VALUES(@a,@b)", myconn);
                mycomm.Parameters.AddWithValue("@a", codecl);
                mycomm.Parameters.AddWithValue("@b", classe);                
                mycomm.ExecuteNonQuery();
               
                MessageBox.Show("Sauvegarde reussie !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


        public void chargementclasse(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM classe", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void supprimerclasse(string codeclasse)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from classe WHERE codecl ='" + codeclasse + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();

                myconn.Close();
                MessageBox.Show("La suppression a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Supprimer d'abord la reference dans l'option svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------------------------

        //    PARAMETRE OPTION


        public void insertionoption(string codeop, string option,string codecl)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO option1 VALUES (@a,@b,@c)", myconn);
                mycomm.Parameters.AddWithValue("@a", codeop);
                mycomm.Parameters.AddWithValue("@b", option);
                mycomm.Parameters.AddWithValue("@c", codecl);
                mycomm.ExecuteNonQuery();
                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public void chargementoption(DataGridView data1)
        {
            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeop,optioneleve,codesect FROM option1", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
        }


        public void supprimeroption(string codeop)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("DELETE  from option1 WHERE codeop ='" + codeop + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();
                myconn.Close();
                MessageBox.Show("La suppression a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("Supprmer la reference dans la table section svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
             
            }
        }

       public void chargementcombocodeclasse(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codecl FROM classe", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["codecl"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
           

        }

        public void chargementcombo_classe_pourcent(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT classe FROM classe", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["classe"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
           

        }


        public void chargementcombocodesection(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codesect FROM section", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["codesect"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();



        }

        public void chargementcombo_section_designe(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT section FROM section", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["section"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();



        }

        public void chargementcombo_section_saisir(ComboBox comb1,TextBox text,string code)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM section where section = '"+code+"' ", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    text.Text=(dr["codesect"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
        }

        public void chargementcombo_option_designe(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT optioneleve FROM option1", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    comb1.Items.Add(dr["optioneleve"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            myconn.Close();
        }

        public void chargementcombo_option_saisir(ComboBox comb1, TextBox text, string code)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM option1 where optioneleve = '" + code + "' ", myconn);
            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    text.Text = (dr["codeop"]).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            myconn.Close();
        }


        //---------------------------------------------------------------------------------------------------------

        //  PARAMETRE SECTION


        public void insertionsection(string codesect, string section)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO section VALUES (@a,@b)", myconn);
                mycomm.Parameters.AddWithValue("@a", codesect);
                mycomm.Parameters.AddWithValue("@b", section);           

                mycomm.ExecuteNonQuery();
               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


        public void chargementsection(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM section", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void supprimersection(string codesect)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from section WHERE codesect ='" + codesect + "'", myconn);
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

        public void chargementcombocodeoption(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeop FROM option1", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["codeop"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
        }

        public void chargementcombo_ption_cours(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT optioneleve FROM option1", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["optioneleve"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
           
        }



        //============================================================================================================

        //PARAMETRE PAYS

        public void insertionpays(string codepay, string pays)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO pays VALUES (@a,@b)", myconn);
                mycomm.Parameters.AddWithValue("@a", codepay);
                mycomm.Parameters.AddWithValue("@b", pays);     

                mycomm.ExecuteNonQuery();
               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        

        public void supprimerpays(string codepay)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from pays WHERE codepays ='" + codepay + "'", myconn);
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

        //============================================================================================================

        //  PARAMETRE VILLE

        public void insertionville(string codeville, string ville, string codepay)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("INSERT INTO ville VALUES (@a,@b,@c)", myconn);
                mycomm.Parameters.AddWithValue("@a", codeville);
                mycomm.Parameters.AddWithValue("@b", ville);
                mycomm.Parameters.AddWithValue("@c", codepay);
                mycomm.ExecuteNonQuery();                
                MessageBox.Show("Sauvegarde reussie !!!", "Succefully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


       

        public void supprimerville(string codeville)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("DELETE  from ville WHERE codeville ='" + codeville + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();
                myconn.Close();
                MessageBox.Show("La suppression a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public void chargementcombocodepays(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT pays FROM pays", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["pays"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
            
        }

        // =====================================================================================================

        // PARAMETRE COMMUNE


        public void insertioncommine(string codecom, string commune, string codeville)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO commune VALUES (@a,@b,@c)", myconn);
                mycomm.Parameters.AddWithValue("@a", codecom);
                mycomm.Parameters.AddWithValue("@b", commune);
                mycomm.Parameters.AddWithValue("@c", codeville);
                mycomm.ExecuteNonQuery();
               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }




        public void supprimercommune(string codecom)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from commune WHERE codecommune ='" + codecom + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();
                myconn.Close();
                MessageBox.Show("La suppression a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!! ", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        public void chargementcombocodeville(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT ville FROM ville", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["ville"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
            
        }


        //============================================================================================================

        //  PARAMETRE QUARTIER


        public void insertionquartier(string codecom, string commune, string codeville)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("INSERT INTO quartier VALUES (@a,@b,@c)", myconn);
                mycomm.Parameters.AddWithValue("@a", codecom);
                mycomm.Parameters.AddWithValue("@b", commune);
                mycomm.Parameters.AddWithValue("@c", codeville);
                mycomm.ExecuteNonQuery();                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }




        public void supprimerquartier(string codequartier)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("DELETE  from quartier WHERE codequartier ='" + codequartier + "'", myconn);
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

        public void chargementcombocodecommune(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT commune FROM commune", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["commune"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
           
        }

        public void chargementcombocodeQuartier(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT quartier FROM quartier", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["quartier"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }



        public void chargementcombocodeAvenue(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT DesigneAvenue FROM tAvenue", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["DesigneAvenue"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }



        //==================================================================================================================
        // PARAMETRE AVENUE

        public void insertionAvenue( string Avenue, string RefQuartier)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("INSERT INTO tAvenue VALUES (@a,@b)", myconn);
                mycomm.Parameters.AddWithValue("@a", Avenue);
                mycomm.Parameters.AddWithValue("@b", RefQuartier);                
                mycomm.ExecuteNonQuery();
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }




        public void supprimerAvenue(string codeAvenue)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("DELETE  from tAvenue WHERE codeAvenue ='" + codeAvenue + "'", myconn);
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

        
        //===========================================================================================================================
        // LES CHARGEMENTS DES VILLES,PAYS,PROVINCES,ET DES QUARTIERS

        public DataTable chargement_pays()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from pays", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        public DataTable chargement_ville()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from ville", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        public DataTable chargement_commune()
        {
            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from commune", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;
        }

        public DataTable chargement_quartier()
        {
            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from quartier", myconn);
            adpt1.Fill(table);
            myconn.Close();

            return table;

        }

        public DataTable chargement_Avenue()
        {
            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from tAvenue", myconn);
            adpt1.Fill(table);
            myconn.Close();

            return table;

        }




        //==========================================================================================

        //pour la prevision

        public void insertionprevision_co(int codeprev, double montant,int anne,string classe,string frais)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("exec merge_prevision_co @a,@b,@c,@d,@e", myconn);
                mycomm.Parameters.AddWithValue("@a", codeprev);
                mycomm.Parameters.AddWithValue("@b", montant);
                mycomm.Parameters.AddWithValue("@c", anne);
                mycomm.Parameters.AddWithValue("@d", classe);
                mycomm.Parameters.AddWithValue("@e", frais);         
                
                mycomm.ExecuteNonQuery();
                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex.Message, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }


        public void insertionprevision_moyen(int codeprev, float montant, int anne, string classe, string frais,string section)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("exec merge_prevision_moyen @a,@b,@c,@d,@e,@f", myconn);
                mycomm.Parameters.AddWithValue("@a", codeprev);
                mycomm.Parameters.AddWithValue("@b", montant);
                mycomm.Parameters.AddWithValue("@c", anne);
                mycomm.Parameters.AddWithValue("@d", classe);
                mycomm.Parameters.AddWithValue("@e", frais);
                mycomm.Parameters.AddWithValue("@f", section);
                mycomm.ExecuteNonQuery();                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex.Message, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }

        public void insertionprevision_final(int codeprev, float montant, int anne, string classe, string frais,string option, string section,int RefEcole)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("exec merge_prevision_final @a,@b,@c,@d,@e,@f,@g,@h", myconn);
                mycomm.Parameters.AddWithValue("@a", codeprev);
                mycomm.Parameters.AddWithValue("@b", montant);
                mycomm.Parameters.AddWithValue("@c", anne);
                mycomm.Parameters.AddWithValue("@d", classe);
                mycomm.Parameters.AddWithValue("@e", frais);
                mycomm.Parameters.AddWithValue("@f", option);
                mycomm.Parameters.AddWithValue("@g", section);
                mycomm.Parameters.AddWithValue("@h", RefEcole);
                mycomm.ExecuteNonQuery();                

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex.Message, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        public void insertionprevision_final1(float montant, int anne, string classe, string frais, string option, string section, int RefEcole)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("exec merge_prevision_final1 @b,@c,@d,@e,@f,@g,@h", myconn);
                //mycomm.Parameters.AddWithValue("@a", codeprev);
                mycomm.Parameters.AddWithValue("@b", montant);
                mycomm.Parameters.AddWithValue("@c", anne);
                mycomm.Parameters.AddWithValue("@d", classe);
                mycomm.Parameters.AddWithValue("@e", frais);
                mycomm.Parameters.AddWithValue("@f", option);
                mycomm.Parameters.AddWithValue("@g", section);
                mycomm.Parameters.AddWithValue("@h", RefEcole);
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex.Message, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }



        public void supprimerprevision(string codeprev)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("DELETE  from prevision WHERE codeprev ='" + codeprev + "'", myconn);
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


        public void chargementprevision(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select Top 100 codeprev,montantprev,codeanne,annee,codecl,codefrais,frais,codeoption,optioneleve,codesection,section,RefEcole,nomEcol from liste_prevision", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void chargementcombo_fais(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT frais FROM frais", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    comb1.Items.Add(dr["frais"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
           
        }

    }
}
