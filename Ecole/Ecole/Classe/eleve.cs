using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
    public class eleve
    {
        OpenFileDialog ofdImage = new OpenFileDialog();
        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        connexion ap = new connexion();
        int grame = DateTime.Now.Hour;
        int grame1 = DateTime.Now.Minute;
        int grame2 = DateTime.Now.Second;        

        //==========================================================================================================

        //  IDENTIFICATION DE L'ELEVE

        public void mergeeleve(string codeel, string nom, string postnom, string prenom, string sexe, string datenaiss, string avenue, string quartier, string commune, string ville, string nation, string tutaire, string profession, string numtutaire, Image im1,string lieunaiss) {

            try
            {
                DateTime datenaiss1 = DateTime.Parse(datenaiss);
                //DateTime dtsortie = DateTime.Parse(datesortie.Text);


                    MemoryStream ms = new MemoryStream();
                    Bitmap bmpImage = new Bitmap(im1);
                    byte[] bytImage;
                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bytImage = ms.ToArray();
                    ms.Close();

                    ap.connect();
                    myconn = new SqlConnection(ap.chemin);
                    myconn.Open();

                    mycomm = new SqlCommand("exec mergeeleve1 @a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k,@l,@m,@n,@o,@p", myconn);
                    mycomm.Parameters.AddWithValue("@a", codeel);
                    mycomm.Parameters.AddWithValue("@b", nom);
                    mycomm.Parameters.AddWithValue("@c", postnom);
                    mycomm.Parameters.AddWithValue("@d", prenom);
                    mycomm.Parameters.AddWithValue("@e", sexe);
                    mycomm.Parameters.AddWithValue("@f", datenaiss1);
                    mycomm.Parameters.AddWithValue("@g", avenue);
                    mycomm.Parameters.AddWithValue("@h", quartier);
                    mycomm.Parameters.AddWithValue("@i", commune);
                    mycomm.Parameters.AddWithValue("@j", ville);
                    mycomm.Parameters.AddWithValue("@k", nation);
                    mycomm.Parameters.AddWithValue("@l", tutaire);
                    mycomm.Parameters.AddWithValue("@m", profession);
                    mycomm.Parameters.AddWithValue("@n", numtutaire);
                    mycomm.Parameters.AddWithValue("@o", bytImage);
                    mycomm.Parameters.AddWithValue("@p", lieunaiss);

                    mycomm.ExecuteNonQuery();
                 
               MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
          
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Erreur de sauvegarde !!!" + ex.Message, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }    



        public void supprimereleve(string codeeleve)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from Eleve WHERE codeeleve ='" + codeeleve + "'", myconn);
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



        public void chargementeleve(DataGridView data1)
        {
            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeeleve,nom,postnom,prenom,sexe,dateNaiss,avenue,quartier,commune,ville,nationalite,nom_tutaire,profession_tutaire,num_tutaire,LieuNaiss FROM Eleve", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
        }
               

        public DataTable chargementeleve_message(string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM viewEleveMessagerie where annee= '"+Annee+"' ", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        public DataTable chargementEns_message()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT matriculeEns,NomEns,postnomEns,prenomEns,numtel FROM Enseignant ", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }

        public void chargementcomboquartier(System.Windows.Forms.ComboBox comb1)
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
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();          

        }

        public void chargementcomboAvenue(System.Windows.Forms.ComboBox comb1)
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


        public void chargementcombocommune(System.Windows.Forms.ComboBox comb1)
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



        public void chargementcomboville(System.Windows.Forms.ComboBox comb1)
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



        public void chargementcombonation(System.Windows.Forms.ComboBox comb1)
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


        public void affichephotoelve(string code,PictureEdit photo)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("SELECT photo from Eleve WHERE codeeleve ='" + code + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                object result = mycomm.ExecuteScalar();

                if (DBNull.Value == (result))
                {
                }
                else
                {
                    byte[] buffer = (byte[])result;
                    MemoryStream ms = new MemoryStream(buffer);
                    Image image = Image.FromStream(ms);
                    photo.Image = image;
                    //return image;
                }
                myconn.Close();
            }
            catch 
            {

                //MessageBox.Show("Erreur de reperer la photo" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        public void affichephotoelve2(string code,PictureEdit photo)
        {
            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("SELECT photo from Eleve WHERE codeeleve ='" + code + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                object result = mycomm.ExecuteScalar();

                if (DBNull.Value == (result))
                {
                }
                else
                {
                    byte[] buffer = (byte[])result;
                    MemoryStream ms = new MemoryStream(buffer);
                    Image image = Image.FromStream(ms);
                    photo.Image = image;
                    //return image;
                }
                myconn.Close();
            }
            catch 
            {
                //MessageBox.Show("Erreur de reperer la photo" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            
        }


        public void affichephotoEns(string code,PictureEdit photoEns)
        {
            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                //mateleve3.Text = "6-61035362";
                //string matricule = mateleve3.Text + mateleve.Text;

                mycomm = new SqlCommand("SELECT photo from Enseignant WHERE matriculeEns ='" + code + "'", myconn);
                adpt1 = new SqlDataAdapter(mycomm);
                object result = mycomm.ExecuteScalar();

                if (DBNull.Value == (result))
                {
                }
                else
                {
                    byte[] buffer = (byte[])result;
                    MemoryStream ms = new MemoryStream(buffer);
                    Image image = Image.FromStream(ms);
                    photoEns.Image = image;
                    //return image;
                }
                myconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de reperer la photo" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }




        }






        public void rechercheeleve1(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeeleve,nom,postnom,prenom,sexe,dateNaiss,avenue,quartier,commune,ville,nationalite,nom_tutaire,profession_tutaire,num_tutaire FROM Eleve WHERE nom LIKE '%" + recherche + "%' OR prenom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR codeeleve LIKE '%" + recherche + "%'OR dateNaiss LIKE '%" + recherche + "%'OR avenue LIKE '%" + recherche + "%'OR quartier LIKE '%" + recherche + "%'OR commune LIKE '%" + recherche + "%'OR ville LIKE '%" + recherche + "%'OR nationalite LIKE '%" + recherche + "%'OR nom_tutaire LIKE '%" + recherche + "%'OR profession_tutaire LIKE '%" + recherche + "%'OR num_tutaire LIKE '%" + recherche + "%'OR sexe LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
           
        }


        public void rechercheeleve_message(string recherche, DataGridView data1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeeleve,nom,postnom,nom_tutaire,num_tutaire FROM Eleve WHERE nom LIKE '%" + recherche + "%' OR prenom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR codeeleve LIKE '%" + recherche + "%'OR dateNaiss LIKE '%" + recherche + "%'OR avenue LIKE '%" + recherche + "%'OR quartier LIKE '%" + recherche + "%'OR commune LIKE '%" + recherche + "%'OR ville LIKE '%" + recherche + "%'OR nationalite LIKE '%" + recherche + "%'OR nom_tutaire LIKE '%" + recherche + "%'OR profession_tutaire LIKE '%" + recherche + "%'OR num_tutaire LIKE '%" + recherche + "%'OR sexe LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();              


        }



        //==============================================================================================================
        // POUR L'INSCRIPTION

        public void rechercheeleve2(string recherche, DataGridView data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeeleve,nom,postnom,prenom,sexe FROM Eleve WHERE nom LIKE '%" + recherche + "%' OR prenom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR codeeleve LIKE '%" + recherche + "%'OR dateNaiss LIKE '%" + recherche + "%'OR sexe LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
           
        }




        public void chargementcombocodeclasse2(System.Windows.Forms.ComboBox comb1)
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

        public void chargementcombocodeannee(System.Windows.Forms.ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeanne FROM annee", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["codeanne"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }

        public void chargementcombo_annee_designe(System.Windows.Forms.ComboBox comb1)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select distinct annee from view_Encours", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["annee"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }

        public void chargementcombo_annee_saisir(System.Windows.Forms.ComboBox comb1,TextBox text,string code)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select distinct RefAnnee,annee from view_Encours where annee= '" + code+"'", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    text.Text=(dr["RefAnnee"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }

        public void chargementcombo_classe_inscrite(System.Windows.Forms.ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT Classe FROM liste_inscription", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["Classe"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }

        public void chargementcombo_affect_inscrit(System.Windows.Forms.ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT division FROM liste_inscription", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["division"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            myconn.Close();
        }


        public void chargementcombo_section_inscrit(System.Windows.Forms.ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT section FROM liste_inscription", myconn);

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

        public void chargementcombo_option_inscrit(System.Windows.Forms.ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT optioneleve FROM liste_inscription", myconn);

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

        public void insertioninscription_final( string codeeleve,string classe,string option, int codeannee,string section,string division,int Ecole)
        {
            
            try
            {                
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();              

                mycomm = new SqlCommand(" exec merge_inscription_final @a,@b,@c,@d,@e,@f,@g ", myconn);                
                mycomm.Parameters.AddWithValue("@a", codeeleve);
                mycomm.Parameters.AddWithValue("@b", classe);
                mycomm.Parameters.AddWithValue("@c", option);
                mycomm.Parameters.AddWithValue("@d", codeannee);
                mycomm.Parameters.AddWithValue("@e", section);
                mycomm.Parameters.AddWithValue("@f", division);
                mycomm.Parameters.AddWithValue("@g", Ecole);
                mycomm.ExecuteNonQuery();
                                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        public void insertioninscription_co(string codeeleve, string classe, int codeannee,string division,int Ecole)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand(" exec merge_inscription_co @a,@b,@c,@d,@e", myconn);
                mycomm.Parameters.AddWithValue("@a", codeeleve);
                mycomm.Parameters.AddWithValue("@b", classe);
                mycomm.Parameters.AddWithValue("@c", codeannee);
                mycomm.Parameters.AddWithValue("@d", division);
                mycomm.Parameters.AddWithValue("@e", Ecole);
                mycomm.ExecuteNonQuery();
                
                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public void insertioninscription_moyen(string codeeleve, string classe, int codeannee,string section ,string division,int Ecole)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand(" exec merge_inscription_moyen @a,@b,@c,@d,@e,@f", myconn);
                mycomm.Parameters.AddWithValue("@a", codeeleve);
                mycomm.Parameters.AddWithValue("@b", classe);
                mycomm.Parameters.AddWithValue("@c", codeannee);
                mycomm.Parameters.AddWithValue("@d", section);
                mycomm.Parameters.AddWithValue("@e", division);
                mycomm.Parameters.AddWithValue("@f", Ecole);
                mycomm.ExecuteNonQuery();                

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }


        public void insertioninscription_final1(int code,string codeeleve, string classe, string option, int codeannee, string section, string division, int Ecole)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand(" exec merge_inscription_final1 @h,@a,@b,@c,@d,@e,@f,@g ", myconn);
                mycomm.Parameters.AddWithValue("@h", code);
                mycomm.Parameters.AddWithValue("@a", codeeleve);
                mycomm.Parameters.AddWithValue("@b", classe);
                mycomm.Parameters.AddWithValue("@c", option);
                mycomm.Parameters.AddWithValue("@d", codeannee);
                mycomm.Parameters.AddWithValue("@e", section);
                mycomm.Parameters.AddWithValue("@f", division);
                mycomm.Parameters.AddWithValue("@g", Ecole);
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        public void insertioninscription_co1(int code,string codeeleve, string classe, int codeannee, string division, int Ecole)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand(" exec merge_inscription_co1 @f,@a,@b,@c,@d,@e", myconn);
                mycomm.Parameters.AddWithValue("@f", code);
                mycomm.Parameters.AddWithValue("@a", codeeleve);
                mycomm.Parameters.AddWithValue("@b", classe);
                mycomm.Parameters.AddWithValue("@c", codeannee);
                mycomm.Parameters.AddWithValue("@d", division);
                mycomm.Parameters.AddWithValue("@e", Ecole);
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public void insertioninscription_moyen1(int code,string codeeleve, string classe, int codeannee, string section, string division, int Ecole)
        {

            try
            {

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand(" exec merge_inscription_moyen1 @g,@a,@b,@c,@d,@e,@f", myconn);
                mycomm.Parameters.AddWithValue("@g", code);
                mycomm.Parameters.AddWithValue("@a", codeeleve);
                mycomm.Parameters.AddWithValue("@b", classe);
                mycomm.Parameters.AddWithValue("@c", codeannee);
                mycomm.Parameters.AddWithValue("@d", section);
                mycomm.Parameters.AddWithValue("@e", division);
                mycomm.Parameters.AddWithValue("@f", Ecole);
                mycomm.ExecuteNonQuery();

                MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de sauvegarde" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }



        }




        public void supprimer_inscription(string codeeleve)
        {

            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from inscription WHERE codeinscription ='" + codeeleve + "'", myconn);
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



        public void chargementinscription(DataGridView data1,string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select top 100 codeinscription,codeeleve,nom,postnom,prenom,Classe,codeoption,optioneleve,annee,codesection,section,division,Designation_classe,Designation_annee,num_tutaire,RefEcole,nomEcol from liste_inscription where annee = '" + Annee+"' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }


        public DataTable chargement_recherche_inscription(string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select codeinscription,nom,postnom,prenom,Classe,codesection,section,codeoption,optioneleve,annee from liste_inscription where annee = '"+Annee+"' ", myconn);
            adpt1.Fill(table);
            myconn.Close();

            return table;


        }

        public DataTable chargement_historique_inscription()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from historique_inscription", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }




        public void rechercheinscription(GridControl data1,string Annee)
        {
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from view_inscription WHERE CodeAnnee = '"+Annee+"' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
            
        }


        public void supprimereinscription(string codeeleve)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("DELETE  from inscription WHERE codeinscription ='" + codeeleve + "'", myconn);
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

        //==============================================================================================

            // pour le paiement

        public void insertion_paiement(double montant, int codeinscription, string codefrais, string codeutil, string libelle)
        {

            try
            {                      

                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();

                mycomm = new SqlCommand("exec MergePaiementTeste @montantpay,@datepay,@codeincription,@codefrais,@codeutil,@libelle ", myconn);
                //mycomm.Parameters.AddWithValue("@a", codepay);
                mycomm.Parameters.AddWithValue("@montantpay", montant);                
                mycomm.Parameters.AddWithValue("@datepay", DateTime.Now);
                mycomm.Parameters.AddWithValue("@codeincription", codeinscription);
                mycomm.Parameters.AddWithValue("@codefrais", codefrais);
                mycomm.Parameters.AddWithValue("@codeutil", codeutil);
                mycomm.Parameters.AddWithValue("@libelle", libelle);
                mycomm.ExecuteNonQuery();
               MessageBox.Show("Sauvegarde reussie !!!", "Successfully", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Il n'y a pas de montant prevu pour cette classe !! veuillez voir l'administrateur pour le parametrage du Systeme svp !!!" + ex.Message, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        public void supprimer_paiement(string codeeleve)
        {
            try
            {
                ap.connect();
                myconn = new SqlConnection(ap.chemin);
                myconn.Open();
                mycomm = new SqlCommand("DELETE  from paiement WHERE codepay ='" + codeeleve + "'", myconn);
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




        public void chargementcombocodeprevision(System.Windows.Forms.ComboBox comb1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT codeprev FROM prevision", myconn);

            try
            {

                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    comb1.Items.Add(dr["codeprev"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
            
        }


        public void chargementcombocodeutilisateur(System.Windows.Forms.ComboBox comb1)
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
                MessageBox.Show("Erreur"+ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();
           
        }


        public void chargementcombotypefrais(System.Windows.Forms.ComboBox comb1)
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


        public void sairie_code_frais(TextBox txt,string cmb)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("SELECT * FROM frais where frais='"+cmb+"'", myconn);

            try
            {
                DataTable dt = new DataTable();
                adpt1.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    txt.Text=(dr["codefrais"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex, "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            myconn.Close();

        }

        public void chargementcombo_inscription(System.Windows.Forms.ComboBox comb1)
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


        public void chargementpaiement(GridControl data1,string Annee)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select Top 100 * from view_paiement_vrai where codennee= '"+Annee+"' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }

        public void chargementreste(DataGridView data1)
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from total_paiement", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();


        }

        public DataTable chargement_reste()
        {

            connexion ap = new connexion();
            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from total_paiement", myconn);
            adpt1.Fill(table);

            myconn.Close();

            return table;


        }


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



        public void recherche_paiement(string recherche, GridControl data1)
        {


            ap.connect();
            myconn = new SqlConnection(ap.chemin);
            myconn.Open();
            DataTable table = new DataTable();
            adpt1 = new SqlDataAdapter("select * from tous_les_details_paiement  WHERE Nom LIKE '%" + recherche + "%'  OR Postnom LIKE '%" + recherche + "%' OR Matricule LIKE '%" + recherche + "%'OR datepay LIKE '%" + recherche + "%'OR montantpay LIKE '%" + recherche + "%'OR Code_Inscription LIKE '%" + recherche + "%'OR section LIKE '%" + recherche + "%'OR optioneleve LIKE '%" + recherche + "%'OR TypeFrais LIKE '%" + recherche + "%'OR utilisateur LIKE '%" + recherche + "%' ", myconn);
            adpt1.Fill(table);
            data1.DataSource = table;
            myconn.Close();
            

        }


    }
}
