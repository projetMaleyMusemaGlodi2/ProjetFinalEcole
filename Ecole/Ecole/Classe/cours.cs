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
    public class cours
    {
        OpenFileDialog ofdImage = new OpenFileDialog();

        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        connexion ap = new connexion();


        int grame = DateTime.Now.Hour;
        int grame1 = DateTime.Now.Minute;
        int grame2 = DateTime.Now.Second;


       




        public void insertioncours( string cours)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO cours(cours)VALUES(@a)", myconn);
                mycomm.Parameters.AddWithValue("@a", cours);              

                mycomm.ExecuteNonQuery();               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }


        public void chargementcours(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select codecours,cours from cours", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void recherchecours(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select codecours,cours from cours where cours LIKE '%" + recherche + "%'OR codecours LIKE '%" + recherche + "%'", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
            


        }


        public void supprimercours(string codeeleve)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("DELETE  from cours WHERE codecours ='" + codeeleve + "'", myconn);
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



        public void modifiercours(string periode, string code)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("UpDate cours set cours='" + periode + "' Where codecours='" + code + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();

                MessageBox.Show("La modification a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("La modification a echouee !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        //======================================================================================================
        // POUR LA PERIODE 


        public void insertionperiode( int codeperiode, string periode)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("INSERT INTO periode(codeperiode,des_periode)VALUES(@a,@b)", myconn);
                mycomm.Parameters.AddWithValue("@a", codeperiode);
                mycomm.Parameters.AddWithValue("@b", periode);
                mycomm.ExecuteNonQuery();
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }


        public void chargementperiode(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select codeperiode,des_periode from periode", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();

        }


        public void rechercheperiode(string recherche, DataGridView data1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select codeperiode,des_periode from periode where des_periode LIKE '%" + recherche + "%'OR codeperiode LIKE '%" + recherche + "%'", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
            

        }


        public void supprimerperiode(string codeeleve)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("DELETE  from periode WHERE codeperiode ='" + codeeleve + "'", myconn);
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


        public void modifierperiode(string periode,string code)
        {


            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("UpDate periode  Set des_periode='" + periode + "' Where codeperiode='" + code + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                Object result = mycomm.ExecuteScalar();               

                MessageBox.Show("La modification a reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("La modification a echouee !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
                    



        }

        //=========================================================================================================
        // POUR LA COTATION        

        public void chargementcombo_periode_pourcent(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT des_periode FROM periode", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["des_periode"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();


        }

        public void chargementcombo_classe_cours(ComboBox comb1)
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


        public void chargementcombocours(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT cours FROM cours", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["cours"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
          

        }


        public void chargementcomboeleve(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeinscription FROM inscription", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["codeinscription"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
           


        }



        public void insertioncotation(double cote,int periode,string cours,int eleve)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec MergeCotationTeste @a,@b,@c,@d", myconn);
                mycomm.Parameters.AddWithValue("@a", cote);
                mycomm.Parameters.AddWithValue("@b", periode);
                mycomm.Parameters.AddWithValue("@c", cours);
                mycomm.Parameters.AddWithValue("@d", eleve);
                mycomm.ExecuteNonQuery();
                
                MessageBox.Show("Sauvegarde reussie!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Le cours n'est pas encore attribué a un maximum svp !!!" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }


        public void chargementcotation(DataGridView data1,string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select Top 100 * from liste_cote where codeanne = '"+Annee+"'", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void recherchecotation(string recherche, DataGridView data1,string Annee)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from liste_cote where codeanne='"+Annee+"' and (codecours LIKE '%" + recherche + "%'OR des_periode LIKE '%" + recherche + "%'OR nom LIKE '%" + recherche + "%'OR cote LIKE '%" + recherche + "%'OR codecote LIKE '%" + recherche + "%')", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();           

        }


        public void supprimercotation(string codeeleve)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from cotation WHERE codecote ='" + codeeleve + "'", myconn);
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



        public void modifiercotation(string code,double cote,int periode,string cours,int eleve)
        {


            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec UpdateCotationTeste @a,@b,@c,@d,@e ", myconn);
                mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", cote);
                mycomm.Parameters.AddWithValue("@c", periode);
                mycomm.Parameters.AddWithValue("@d", cours);
                mycomm.Parameters.AddWithValue("@e", eleve);
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

        public DataTable chargement_proclamation()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from Pourcentage_General", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        public void chargement_code_eleve_cote(ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT Codeeleve FROM Pourcentage_General", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["Codeeleve"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();



        }

        // ======================================================================================================================================

        //pour les affectations 

        public void recherche_combo_cours(ComboBox comb1,string recherche)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT cours FROM cours", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Insert(0,dr["cours"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();



        }






        public void insertionaffectation_co(double max, string cours, int annee, string ens,string classe,string periode)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("Exec merge_affecter_co @b,@c,@d,@e,@f,@g", myconn);
                //mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", max);
                mycomm.Parameters.AddWithValue("@c", cours);
                mycomm.Parameters.AddWithValue("@d", annee);
                mycomm.Parameters.AddWithValue("@e", ens);
                mycomm.Parameters.AddWithValue("@f", classe);
                mycomm.Parameters.AddWithValue("@g", periode);                
                mycomm.ExecuteNonQuery();
                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }


        public void insertionaffectation_moyen(double max, string cours, int annee, string ens, string classe,string section ,string periode)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("Exec merge_affecter_moyen @b,@c,@d,@e,@f,@g,@h", myconn);
                //mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", max);
                mycomm.Parameters.AddWithValue("@c", cours);
                mycomm.Parameters.AddWithValue("@d", annee);
                mycomm.Parameters.AddWithValue("@e", ens);
                mycomm.Parameters.AddWithValue("@f", classe);
                mycomm.Parameters.AddWithValue("@g", section);
                mycomm.Parameters.AddWithValue("@h", periode);

                mycomm.ExecuteNonQuery();
               
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde !!!" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }

        public void insertionaffectation_final(double max, string cours, int annee, string ens, string classe, string section,string option, string periode,int RefEcole)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("Exec merge_affecter_final @b,@c,@d,@e,@f,@g,@h,@i,@j", myconn);
                //mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", max);
                mycomm.Parameters.AddWithValue("@c", cours);
                mycomm.Parameters.AddWithValue("@d", annee);
                mycomm.Parameters.AddWithValue("@e", ens);
                mycomm.Parameters.AddWithValue("@f", classe);
                mycomm.Parameters.AddWithValue("@g", section);
                mycomm.Parameters.AddWithValue("@h", option);
                mycomm.Parameters.AddWithValue("@i", periode);
                mycomm.Parameters.AddWithValue("@j", RefEcole);
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde !!!" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        public void insertionaffectation_final1(int code,double max, string cours, int annee, string ens, string classe, string section, string option, string periode,int RefEcole)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("Exec merge_affecter_final1 @a,@b,@c,@d,@e,@f,@g,@h,@i,@j", myconn);
                mycomm.Parameters.AddWithValue("@a", code);
                mycomm.Parameters.AddWithValue("@b", max);
                mycomm.Parameters.AddWithValue("@c", cours);
                mycomm.Parameters.AddWithValue("@d", annee);
                mycomm.Parameters.AddWithValue("@e", ens);
                mycomm.Parameters.AddWithValue("@f", classe);
                mycomm.Parameters.AddWithValue("@g", section);
                mycomm.Parameters.AddWithValue("@h", option);
                mycomm.Parameters.AddWithValue("@i", periode);
                mycomm.Parameters.AddWithValue("@j", RefEcole);
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde !!!" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }
        public void chargementcombo_enseignant_designe(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT NomEns FROM Enseignant", myconn);
            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["NomEns"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            myconn.Close();
        }

        public void chargementcombo_enseignant_saisir(ComboBox comb1,TextBox texte,string code)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM Enseignant where NomEns='"+code+"' ", myconn);
            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    texte.Text=(dr["matriculeEns"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }

        public void chargementcombo_periode_designe(ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select distinct periode from view_Encours", myconn);
            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["periode"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            myconn.Close();
        }

        public void chargementcombo_periode_saisir(ComboBox comb1, TextBox texte, string code)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select distinct RefPeriode,periode from view_Encours where periode='" + code + "' ", myconn);
            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    texte.Text = (dr["RefPeriode"]).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            myconn.Close();
        }

       


        public void chargementaffectation(DataGridView data1,string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select Top 100 codeaffect,maxima,codecours,codeanne,codeens,NomEns,postnomEns,Code_classe,classe,code_section,section,codeop,optioneleve,code_periode,des_periode,Annee,NumCours,RefEcole,nomEcol from liste_affectation where codeanne = '" + Annee+"' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public void rechercheaffectation(string recherche, DataGridView data1,string Annee)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from liste_affectation where codeanne='"+Annee+"' and (codeaffect LIKE '%" + recherche + "%'OR maxima LIKE '%" + recherche + "%'OR codecours LIKE '%" + recherche + "%'OR codeanne LIKE '%" + recherche + "%'OR codeens LIKE '%" + recherche + "%'OR NomEns LIKE '%" + recherche + "%'OR Code_classe LIKE '%" + recherche + "%'OR optioneleve LIKE '%" + recherche + "%'OR des_periode LIKE '%" + recherche + "%'OR code_periode LIKE '%" + recherche + "%'OR postnomEns LIKE '%" + recherche + "%')", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
          
        }


        public void supprimeraffectation(string codeaff)
        {



            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from affecter WHERE codeaffect ='" + codeaff + "'", myconn);
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

        public void modifieraffectation(string code, double max, string cours, int annee, string ens,int classe,int option,int periode)
        {
            try
            {   ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("UpDate affecter  Set maxima='" + max + "', codecours='" + cours + "', codeanne='" + annee + "', codeop='" + option + "', code_periode='" + periode+ "', codeens='" + ens+ "', codecl='" + classe + "' Where codeaffect='" + code + "'", myconn);
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
