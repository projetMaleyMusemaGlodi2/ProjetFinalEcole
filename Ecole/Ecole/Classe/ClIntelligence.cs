using DevExpress.XtraEditors;
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

namespace Ecole.Classe
{
    class ClIntelligence
    {

        connexion cnx = null;
        private static ClIntelligence glos;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter dt = null;
        SqlDataReader dr = null;
        
        public void innitialiseConnect()
        {
            try
            {
                cnx = new connexion(); cnx.connect();
                con = new SqlConnection(cnx.chemin);
            }
            catch (Exception)
            {
                throw new Exception("l'un de vos fichiers de configuration est incorrect");
            }

        }
        public static ClIntelligence GetInstance()
        {
            if (glos == null)
                glos = new ClIntelligence();
            return glos;
        }
        private static void setParameter(SqlCommand cmd, string name, DbType type, int length, object paramValue)
        {
            IDbDataParameter a = cmd.CreateParameter();
            a.ParameterName = name;
            a.Size = length;
            a.DbType = type;

            if (paramValue == null)
            {
                if (!a.IsNullable)
                {
                    a.DbType = DbType.String;
                }
                a.Value = DBNull.Value;
            }
            else
                a.Value = paramValue;
            cmd.Parameters.Add(a);
        }
        //Control adressage

        //Update Annee and Frais

        public void update_Parametre(ClsAdresse cb, string nomTable, string code, string designe)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("update " + nomTable + " set " + designe + "=@b where " + code + "=@a", con);
                cmd.Parameters.AddWithValue("@a", cb.Code1);
                cmd.Parameters.AddWithValue("@b", cb.Designation);

                DialogResult res = MessageBox.Show("voulez vous vraiment enregistrer cette operation?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Enregistrement réussi avec succès", "Message de confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Opération Annulée!");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }


        // POUR LES ECOLES

        public void insertEcole(ClsEcole cb)
        {

            try
            {
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(cb.Logo);
                byte[] bytImage;
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                bytImage = ms.ToArray();
                ms.Close();

                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("exec merge_Ecole @a,@b,@c,@d,@e,@f,@g,@h,@i", con);
                setParameter(cmd, "@a", DbType.Int32, 90, cb.CodeEcol);
                setParameter(cmd, "@b", DbType.String, 100, cb.NomEcol);
                setParameter(cmd, "@c", DbType.String, 100, cb.AdresseEcol);
                setParameter(cmd, "@d", DbType.String, 100, cb.TeleohoneEcol);
                setParameter(cmd, "@e", DbType.String, 100, cb.MailEcol);
                setParameter(cmd, "@f", DbType.Binary, int.MaxValue, bytImage);
                setParameter(cmd, "@g", DbType.String, 90, cb.VilleEcole);
                setParameter(cmd, "@h", DbType.String, 90, cb.CommuneEcole);
                setParameter(cmd, "@i", DbType.String, 90, cb.CodeEcole);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sauvegarde reussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //finally
            //{
            //    cmd.Dispose();
            //    con.Close();
            //}
        }

        public void supprimer_Ecole(ClsEcole cb)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("DELETE  from tEcole WHERE codeEcol ='" + cb.CodeEcol + "'", con);
                dt = new SqlDataAdapter(cmd);
                Object result = cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("La suppression a reussie !!! ");
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!! ");
            }
        }

        public DataTable chargement_Ecole()
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select codeEcol,nomEcol,adresseEcol,telephoneEcol,mailEcol,villeEcole,CommuneEcole,CodeEcole from tEcole", con);
            dt.Fill(table);
            con.Close();

            return table;
        }


        public DataTable recherche_Magasin(string recherche)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select codeEcol,nomEcol,adresseEcol,telephoneEcol,mailEcol,villeEcole,CommuneEcole,CodeEcole from tEcole WHERE nomEcol LIKE '%" + recherche + "%' OR adresseEcol LIKE '%" + recherche + "%' OR telephoneEcol LIKE '%" + recherche + "%' OR mailEcol LIKE '%" + recherche + "%' ", con);
            dt.Fill(table);
            con.Close();
            return table;
        }


        public void chargementcombo_Ecole_designe(System.Windows.Forms.ComboBox comb1)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT nomEcol FROM tEcole", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    comb1.Items.Add(dr["nomEcol"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void chargementcombo_Ecole_text(TextBox champs, string texte)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT * FROM tEcole where nomEcol= '" + texte + "'", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    champs.Text = (dr["codeEcol"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void affichepho_Ecole(string code, PictureEdit photo)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT photo from tEcole WHERE codeEcol ='" + code + "'", con);
                dt = new SqlDataAdapter(cmd);
                object result = cmd.ExecuteScalar();

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
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de reperer la photo" + ex);
            }

        }


        //=========================================================================================================
        public void loadCombo(clsBase cb, System.Windows.Forms.ComboBox cmb, string champ, string valeur)
        {
            try
            {
                cmb.Items.Clear();
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from " + cb.NomTable1 + " where " + champ + " = '" + valeur + "' ", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmb.Items.Add(dr[cb.NomChamp1].ToString());
                }
                cmd.Dispose();
                con.Close();
            }
            catch (Exception)
            { throw new Exception("Erreur de selection"); }
        }

        public void getcode_Combo(clsBase cb, TextBox txt, String champ, String val)
        {
            try
            {
                txt.Clear();
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select " + cb.NomChamp1 + " from " + cb.NomTable1 + " where " + champ + "=@a", con);
                cmd.Parameters.AddWithValue("@a", val);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt.Text = (dr[cb.NomChamp1].ToString());
                }
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable recherche_Eleve(string recherche,string Annee)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from liste_inscription where annee = '" + Annee + "' and (nom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR optioneleve LIKE '%" + recherche + "%' OR Designation_annee LIKE '%" + recherche + "%' OR Designation_classe LIKE '%" + recherche + "%' OR section LIKE '%" + recherche + "%' OR codeeleve  LIKE '%" + recherche + "%')", con);
            dt.Fill(table);
            con.Close();
            return table;
        }

        public void update_Adresse(ClsAdresse cb, string nomTable, string code, string designe, string reference)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("update " + nomTable + " set " + designe + "=@b," + reference + "=@c where " + code + "=@a", con);
                cmd.Parameters.AddWithValue("@a", cb.Code1);
                cmd.Parameters.AddWithValue("@b", cb.Designation);
                cmd.Parameters.AddWithValue("@c", cb.Reference);

                DialogResult res = MessageBox.Show("voulez vous vraiment enregistrer cette operation?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Enregistrement réussi avec succès", "Message de confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Opération Annulée!");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public void update_Pays(ClsAdresse cb, string nomTable, string code, string designe)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("update " + nomTable + " set " + designe + "=@b where " + code + "=@a", con);
                cmd.Parameters.AddWithValue("@a", cb.Code1);
                cmd.Parameters.AddWithValue("@b", cb.Designation);

                DialogResult res = MessageBox.Show("voulez vous vraiment enregistrer cette operation?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Enregistrement réussi avec succès", "Message de confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Opération Annulée!");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public void LoadDataGrid(string nomTable, DataGridView grd)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from " + nomTable, con);
                dt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dt.Fill(ds, "grid");
                grd.DataSource = ds.Tables["grid"];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose();
                con.Close();
            }
        }
        public void deleteFrom(clsBase cb)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("Delete from " + cb.NomTable1 + " where " + cb.NomChamp1 + "=@valCode", con);
                cmd.Parameters.AddWithValue("@valCode", cb.Code1);

                DialogResult res = MessageBox.Show("voulez vous vraiment enregistrer cette operation?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Suppréssion réussi avec succès", "Message de confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("vous n'avez pas Supprimer!");

                }

            }
            catch (Exception)
            {
                throw new Exception("Ce code n'existe pas!");
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }



        //Recherche cours
        //==================================================================================================================
        public DataTable chargementCours()
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select cours from cours", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public DataTable chargementEnseignant()
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select matriculeEns,NomEns,postnomEns,prenomEns from Enseignant", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public DataTable chargementLivre()
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select code_livre,titre_livre,auteur from livre", con);
            dt.Fill(table);
            con.Close();

            return table;
        }








        //==============================================================================================================
        //=================================================================================================================
        //INTELIIGENCE LOGICIEL(LES TESTS)

        public string[] RetourneEleve(string code)
        {
            string[] tab = new string[4];
                                    
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select * from liste_inscription where codeinscription= @codeinscription", con);
            setParameter(cmd, "codeinscription", DbType.String, 20, code);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {               
               tab[0] = dr["nom"].ToString();
               tab[1] = dr["postnom"].ToString();
               tab[2] = dr["prenom"].ToString();
               tab[3] = dr["num_tutaire"].ToString();
               count += 1;                  
              
            }
            if (count == 1)
            {                
            }
            else
            {
                MessageBox.Show("Eleve Introuvable !!!");
            }

            return tab;

        }


        public string RetourneEcole()
        {
            string Ecole="" ;

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select nomEcol from tEcole", con);            
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                Ecole = dr["nomEcol"].ToString();               
                count += 1;

            }
            if (count == 1)
            {
            }
            else
            {
                MessageBox.Show("Eleve Introuvable !!!");
            }

            return Ecole;

        }
        public bool teste_Option(string optionEntree,string sectionEntree)
        {
            bool teste = true;
            string sectionTest;            
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select codesect from option1 where optioneleve= @op", con);
            setParameter(cmd, "@op", DbType.String, 20, optionEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    sectionTest = dr["codesect"].ToString();                    

                    if (sectionEntree == sectionTest)
                    {
                        count += 1;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }



        public bool teste_Periode(string anneeEntree, string classeEntree,string periodeEntree)
        {
            bool teste = true;
            string periodeTest="";
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select RefPeriode from tEncours where RefAnnee= " + anneeEntree+" and RefClasse="+classeEntree+ "", con);
            //setParameter(cmd, "@RefAnnee", DbType.String, 20, anneeEntree);
            //setParameter(cmd, "@RefClasse", DbType.String, 20, classeEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    periodeTest = dr["RefPeriode"].ToString();

                    if (periodeEntree == periodeTest)
                    {
                        count += 1;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }

        public bool teste_Periode1(string anneeEntree, string classeEntree, string periodeEntree)
        {
            bool teste = true;
            string periodeTest;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select * from view_Encours where RefAnnee= @RefAnnee and RefClasse=@RefClasse", con);
            setParameter(cmd, "@RefAnnee", DbType.String, 20, anneeEntree);
            setParameter(cmd, "@RefClasse", DbType.String, 20, classeEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    periodeTest = dr["RefPeriode"].ToString();

                    if (periodeEntree == periodeTest)
                    {
                        count += 1;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }



        public bool teste_Cours(string anneeEntree, string classeEntree, string sectionEntree,string optionEntree,string coursEntree)
        {
            bool teste = true;
            string coursTest;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select distinct codecours from affecter where codeanne="+anneeEntree+" and codecl="+classeEntree+" and codeop='"+optionEntree+"' and code_section='"+sectionEntree+"' and codecours='"+coursEntree+"'", con);
            //setParameter(cmd, "@codeanne", DbType.String, 20, anneeEntree);
            //setParameter(cmd, "@codecl", DbType.String, 20, classeEntree);
            //setParameter(cmd, "@codeop", DbType.String, 20, optionEntree);
            //setParameter(cmd, "@code_section", DbType.String, 20, sectionEntree);
            //setParameter(cmd, "@codecours", DbType.String, 20, coursEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    coursTest = dr["codecours"].ToString();
                    if (coursEntree == coursTest)
                    {
                        count += 1;
                    }
                    else {

                    }
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }






        public double teste_Maximum(string classeEntree, string sectionEntree,string optionEntree,string anneeEntree,string coursEntree,string periodeEntree)
        {
            bool teste = true;
            double maxima= 0;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select maxima from affecter where codecours ='"+ coursEntree + "' and codeanne = "+anneeEntree+" and codecl = "+classeEntree+" and code_section ='"+sectionEntree+"' and codeop ='"+optionEntree+"' and code_periode ="+periodeEntree+"", con);
            //SqlCommand cmd = new SqlCommand("select maxima from affecter where codecours='Education à la vie(5Soc)' and codeanne=2017 and codecl=5 and code_section='Soc' and codeop='Soc' and code_periode=1", con);
            //SqlCommand cmd = new SqlCommand("select maxima from affecter where codecours=@codecours and codeanne=@codeanne and codecl=@codecl and code_section=@code_section and codeop=@codeop and code_periode=@code_periode", con);
            //setParameter(cmd, "@codecours", DbType.String, 20, coursEntree);
            //setParameter(cmd, "@codeanne", DbType.String, 20, anneeEntree);
            //setParameter(cmd, "@codecl", DbType.String, 20,classeEntree );
            //setParameter(cmd, "@code_section", DbType.String, 20, sectionEntree);
            //setParameter(cmd, "@codeop", DbType.String, 20, optionEntree);
            //setParameter(cmd, "@code_periode", DbType.String, 20, periodeEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {

                maxima = double.Parse(dr["maxima"].ToString());
                count += 1;
                //try
                //{
                //   //maxima = double.Parse(dr["maxima"].ToString());                  
                //   //count += 1;

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
                MessageBox.Show("Le Maximum de ce cours dans cette classe n'est pas encore inseré svp !!! Veillez d'abord inserer le Maximum de ce cours dans attributions avant de coter svp !!!", "Maximum Obligatoire", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }

            return maxima;

        }


        //public bool teste_Maximum(string classeEntree, string sectionEntree, string optionEntree, string anneeEntree, string coursEntree, string periodeEntree, string coteEntree)
        //{
        //    bool teste = true;
        //    string maxima;
        //    innitialiseConnect();
        //    if (!con.State.ToString().ToLower().Equals("open")) con.Open();
        //    SqlCommand cmd = new SqlCommand("select maxima from affecter where codecours=@codecours and codeanne=@codeanne and codecl=@Code_classe and code_section=@code_section and codeop=@codeop and code_periode=@code_periode", con);
        //    setParameter(cmd, "@codecours", DbType.String, 20, coursEntree);
        //    setParameter(cmd, "@codeanne", DbType.String, 20, anneeEntree);
        //    setParameter(cmd, "@Code_classe", DbType.String, 20, classeEntree);
        //    setParameter(cmd, "@code_section", DbType.String, 20, sectionEntree);
        //    setParameter(cmd, "@codeop", DbType.String, 20, optionEntree);
        //    setParameter(cmd, "@code_periode", DbType.String, 20, periodeEntree);
        //    SqlDataReader dr;
        //    dr = cmd.ExecuteReader();
        //    int count = 0;
        //    while (dr.Read())
        //    {
        //        try
        //        {
        //            maxima = dr["maxima"].ToString();

        //            if (double.Parse(coteEntree) <= double.Parse(maxima))
        //            {
        //                count += 1;
        //            }
        //            else
        //            {
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }

        //    if (count == 1)
        //    {
        //        teste = true;
        //    }

        //    else
        //    {
        //        teste = false;
        //    }

        //    return teste;

        //}

        public bool teste_Cotation(string coursEntree, string periodeEntree,string inscriptionEntree)
        {
            bool teste = true;
            string cote;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select cote from cotation where codecours='"+coursEntree+"' and codeperiode='"+periodeEntree+"' and codeinscription="+inscriptionEntree+"", con);
            //setParameter(cmd, "@CodeCours", DbType.String, 20, coursEntree);
            //setParameter(cmd, "@codeannee", DbType.String, 20, anneeEntree);
            //setParameter(cmd, "@codeclasse", DbType.String, 20, classeEntree);
            //setParameter(cmd, "@codesection", DbType.String, 20, sectionEntree);
            //setParameter(cmd, "@codeoption", DbType.String, 20, optionEntree);
            //setParameter(cmd, "@codeperiode", DbType.String, 20, periodeEntree);
            //setParameter(cmd, "@codeInscription", DbType.String, 20, inscriptionEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    cote = dr["cote"].ToString();                    
                        count += 1;                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = false;
            }

            else
            {
                teste = true;
            }

            return teste;

        }

        public bool teste_Discipline(string periodeEntree, string inscriptionEntree)
        {
            bool teste = true;
            string cote;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select RefMession from tDicipline where RefEleve='" + inscriptionEntree + "' and RefPeriode='" + periodeEntree + "'", con);
            //setParameter(cmd, "@CodeCours", DbType.String, 20, coursEntree);
            //setParameter(cmd, "@codeannee", DbType.String, 20, anneeEntree);
            //setParameter(cmd, "@codeclasse", DbType.String, 20, classeEntree);
            //setParameter(cmd, "@codesection", DbType.String, 20, sectionEntree);
            //setParameter(cmd, "@codeoption", DbType.String, 20, optionEntree);
            //setParameter(cmd, "@codeperiode", DbType.String, 20, periodeEntree);
            //setParameter(cmd, "@codeInscription", DbType.String, 20, inscriptionEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    cote = dr["RefMession"].ToString();
                    count += 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = false;
            }

            else
            {
                teste = true;
            }

            return teste;

        }

        public bool teste_Affectation(string classeEntree, string sectionEntree, string optionEntree, string anneeEntree, string coursEntree, string periodeEntree)
        {
            bool teste = true;
            string maxima;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select maxima from affecter where codecours ='"+coursEntree+"' and codeanne ="+anneeEntree+" and codecl ="+classeEntree+" and code_section ='"+sectionEntree+ "' and codeop ='" + optionEntree+ "' and code_periode='"+periodeEntree+"'", con);
            //setParameter(cmd, "@codecours", DbType.String, 20, coursEntree);
            //setParameter(cmd, "@codeanne", DbType.String, 20, anneeEntree);
            //setParameter(cmd, "@Code_classe", DbType.String, 20, classeEntree);
            //setParameter(cmd, "@code_section", DbType.String, 20, sectionEntree);
            //setParameter(cmd, "@codeop", DbType.String, 20, optionEntree);
            //setParameter(cmd, "@code_periode", DbType.String, 20, periodeEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    maxima = dr["maxima"].ToString();                   
                        count += 1;
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = false;
            }

            else
            {
                teste = true;
            }

            return teste;

        }


        public bool teste_Inscription(string EleveEntree, string anneeEntree)
        {
            bool teste = true;
            string maxima;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select codeinscription,codeeleve,codeclasse,codeoption,codeanne,codesection from inscription where codeanne=@codeanne and codeeleve=@codeeleve", con);
            setParameter(cmd, "@codeeleve", DbType.String, 20, EleveEntree);
            setParameter(cmd, "@codeanne", DbType.String, 20, anneeEntree);                     
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    maxima = dr["codeinscription"].ToString();
                    count += 1;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = false;
            }

            else
            {
                teste = true;
            }

            return teste;

        }



        public bool teste_AffectationHoraire(string classeEntree, string sectionEntree, string optionEntree, string anneeEntree,string AffectEntree, string coursEntree, string joursEntree,string heureDebut,string heureFin)
        {
            bool teste = true;
            string maxima;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select * from affect_horaire where ccours=@ccours and codeanne=@codeanne and codeclasse=@codeclasse and code_section=@code_section and codeop=@codeop and codejours=@codejours and affectation=@affectation and heure_debut=@heure_debut and heure_fin=@heure_fin", con);
            setParameter(cmd, "@ccours", DbType.String, 20, coursEntree);
            setParameter(cmd, "@codeanne", DbType.String, 20, anneeEntree);
            setParameter(cmd, "@codeclasse", DbType.String, 20, classeEntree);
            setParameter(cmd, "@code_section", DbType.String, 20, sectionEntree);
            setParameter(cmd, "@codeop", DbType.String, 20, optionEntree);
            setParameter(cmd, "@codejours", DbType.String, 20, joursEntree);
            setParameter(cmd, "@affectation", DbType.String, 20, AffectEntree);
            setParameter(cmd, "@heure_debut", DbType.String, 20, heureDebut);
            setParameter(cmd, "@heure_fin", DbType.String, 20, heureFin);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    maxima = dr["ccours"].ToString();
                    count += 1;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = false;
            }

            else
            {
                teste = true;
            }

            return teste;

        }



        public bool teste_PaiementExist(string classeEntree, string sectionEntree, string optionEntree, string anneeEntree, string FraisEntree,string inscriptionEntree,string paieEntree)
        {
            bool teste = true;
            string montantTotal;
            string montantPrevu;
            string montantReste;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select CodeInscription,codeclasse,codesection,Code_option,Code_frais,codennee,Total,montantprev,Reste from view_paiement_vrai where Code_frais=@Code_frais and codennee=@codennee and codeclasse=@codeclasse and codesection=@codesection and Code_option=@Code_option and CodeInscription=@CodeInscription ", con);
            setParameter(cmd, "@Code_frais", DbType.String, 20, FraisEntree);
            setParameter(cmd, "@codennee", DbType.String, 20, anneeEntree);
            setParameter(cmd, "@codeclasse", DbType.String, 20, classeEntree);
            setParameter(cmd, "@codesection", DbType.String, 20, sectionEntree);
            setParameter(cmd, "@Code_option", DbType.String, 20, optionEntree);
            setParameter(cmd, "@CodeInscription", DbType.String, 20, inscriptionEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    montantPrevu = dr["montantprev"].ToString();
                    montantTotal = dr["Total"].ToString();
                    montantReste = dr["Reste"].ToString();

                    if ((double.Parse(montantTotal) + double.Parse(paieEntree)) <= double.Parse(montantPrevu))
                    {
                        count += 1;
                    }
                    else {
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }

        public bool teste_PaiementNotExist(string classeEntree, string sectionEntree, string optionEntree, string anneeEntree, string FraisEntree, string inscriptionEntree, string paieEntree)
        {
            bool teste = true;            
            string montantPrevu;            
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select codeanne,codecl,codefrais,codeoption,codesection,montantprev from liste_prevision where codefrais=@codefrais and codeanne=@codeanne and codecl=@codecl and codesection=@codesection and codeoption=@codeoption", con);
            setParameter(cmd, "@codefrais", DbType.String, 20, FraisEntree);
            setParameter(cmd, "@codeanne", DbType.String, 20, anneeEntree);
            setParameter(cmd, "@codecl", DbType.String, 20, classeEntree);
            setParameter(cmd, "@codesection", DbType.String, 20, sectionEntree);
            setParameter(cmd, "@codeoption", DbType.String, 20, optionEntree);            
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    montantPrevu = dr["montantprev"].ToString();                    

                    if (( double.Parse(paieEntree)) <= double.Parse(montantPrevu))
                    {
                        count += 1;
                    }
                    else
                    {
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }



        public bool teste_Paiement1(string InscriptionEntree)
        {
            bool teste = true;
            string montant;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select * from paiement where codeincription=@codeincription", con);
            setParameter(cmd, "@codeincription", DbType.String, 20, InscriptionEntree);            
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    montant = dr["codeincription"].ToString();
                    count += 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }


        public bool teste_EmpruntLivre(string LivreEntree, string nombreEntree)
        {
            bool teste = true;
            string nombreTest;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select * from livre where code_livre= @livre", con);
            setParameter(cmd, "@livre", DbType.String, 20, LivreEntree);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    nombreTest = dr["nombre_livre"].ToString();

                    if (int.Parse(nombreEntree) < int.Parse(nombreTest))
                    {
                        count += 1;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }

        public bool teste_RemiseLivre(string EmpruntEntree, string nombreEntree,string LivreEntree)
        {
            bool teste = true;
            string nombreTest;
            string livreTest;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select * from emprunt_livre where num= @code", con);
            setParameter(cmd, "@code", DbType.String, 20, EmpruntEntree);            
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    nombreTest = dr["Nombre"].ToString();
                    livreTest = dr["ref_livre"].ToString();

                    if ((int.Parse(nombreEntree) <= int.Parse(nombreTest)) && LivreEntree==livreTest)
                    {
                        count += 1;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (count == 1)
            {
                teste = true;
            }

            else
            {
                teste = false;
            }

            return teste;

        }


        public bool teste_periode(string entree)
        {
            bool teste;
            string code;
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            SqlCommand cmd = new SqlCommand("select codeperiode from periode", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                try
                {
                    code = dr["codeperiode"].ToString();
                    if (entree != code)
                    {
                        count += 1;
                    }
                    else
                    {

                    }

                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }


            }

            if (count == 1)
            {
               teste=true;    
            
            }  
            
            
                      
            else
            {
                teste = false;
            }

            return teste;



        }

        //=========================================================================================================
        // RETOUR CODE ECOLE

        public string RetourCodeEcole()
        {
            string codeEcole = "";
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT CodeEcole from tEcole", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    codeEcole = (dr["CodeEcole"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();

            return codeEcole;
        }




        //===========================================================================================================
        //===========================================================================================================
        //ENREGISTREMENT DES PARAMETRAGES EN COURS

        public void insertEcours(ClEncours cb)
        {

            try
            {

                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("exec mergeEncours @a,@b,@c,@d", con);
                setParameter(cmd, "@a", DbType.Int32, 90, cb.CodeEncours);
                setParameter(cmd, "@b", DbType.Int32, 90, cb.RefAnnee1);
                setParameter(cmd, "@c", DbType.String, 90, cb.Refperiode1);
                setParameter(cmd, "@d", DbType.String, 90, cb.Refclasse1);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sauvegarde reussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       

        public void supprimer_Encours(ClEncours cb)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("DELETE  from tEncours WHERE code ='" + cb.CodeEncours + "'", con);
                dt = new SqlDataAdapter(cmd);
                Object result = cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("La suppression a reussie !!! ");
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!! ");
            }
        }

        public DataTable chargement_Encours()
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from view_Encours", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public DataTable recherche_Encours(string recherche)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from view_Encours WHERE annee LIKE '%" + recherche + "%' OR Periode LIKE '%" + recherche + "%' OR Classe LIKE '%" + recherche + "%'", con);
            dt.Fill(table);
            con.Close();
            return table;
        }


        public void chargementcombo_classe_designe(System.Windows.Forms.ComboBox comb1)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT classe FROM classe", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    comb1.Items.Add(dr["classe"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void chargementcombo_classe_text(System.Windows.Forms.ComboBox comb1, TextBox champs, string texte)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT * FROM classe where classe = '" + texte + "'", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    champs.Text = (dr["codecl"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }


        public void chargementcombo_periode_designe(System.Windows.Forms.ComboBox comb1)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT des_periode FROM periode", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    comb1.Items.Add(dr["des_periode"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void chargementcombo_periode_text(System.Windows.Forms.ComboBox comb1, TextBox champs, string texte)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT * FROM periode where des_periode = '" + texte + "'", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    champs.Text = (dr["codeperiode"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }



        public void chargementcombo_annee_designe(System.Windows.Forms.ComboBox comb1)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT annee FROM annee", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    comb1.Items.Add(dr["annee"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void chargementcombo_annee_text(System.Windows.Forms.ComboBox comb1, TextBox champs, string texte)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT * FROM annee where annee = '" + texte + "'", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    champs.Text = (dr["codeanne"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void chargementcombo_annee_text(System.Windows.Forms.ComboBox comb1, Label champs, string texte)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT * FROM annee where annee = '" + texte + "'", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    champs.Text = (dr["codeanne"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }



        public void chargementcombo_coure_designe_recherche(System.Windows.Forms.ComboBox comb1,string recherche)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT cours FROM cours where cours like '%"+recherche+"%'", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    comb1.Items.Insert(0,(dr["cours"]));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void chargementcombocours(System.Windows.Forms.ComboBox comb1,string anneeEntree,string classeEntree,string optionEntree,string sectionEntree)
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from affecter where codeanne="+anneeEntree+" and codecl='"+classeEntree+"' and codeop='"+optionEntree+"' and code_section='"+sectionEntree+"'", con);
            try
            {
                DataTable dtable = new DataTable();
                dt.Fill(dtable);

                foreach (DataRow dr in dtable.Rows)
                {

                    comb1.Items.Add(dr["codecours"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("La liste des Cours est initialisée !!!");
            }

            con.Close();


        }
        //===================================================================================================
        //=================================================================================================
        // POUR LA DISCIPLINE

        public void insertDiscipline(ClsDicipline cb)
        {

            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("insert into tDicipline values (@a,@b,@c,@d)", con);
                setParameter(cmd, "@a", DbType.Int32, 20, cb.RefEleve1);
                setParameter(cmd, "@b", DbType.String, 20, cb.Periode);
                setParameter(cmd, "@c", DbType.String, 90, cb.UserCession);
                setParameter(cmd, "@d", DbType.String, 20, cb.RefMession1);               
                
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sauvegarde reussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void updateDiscipline(ClsDicipline cb)
        {

            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("update tDicipline set RefEleve=@b,RefPeriode=@c,usersession=@d,RefMession=@e where code=@a", con);
                setParameter(cmd, "@a", DbType.Int32, 20, cb.Code);
                setParameter(cmd, "@b", DbType.Int32, 20, cb.RefEleve1);
                setParameter(cmd, "@c", DbType.String, 20, cb.Periode);
                setParameter(cmd, "@d", DbType.String, 90, cb.UserCession);
                setParameter(cmd, "@e", DbType.String, 20, cb.RefMession1);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sauvegarde reussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        public void supprimerDiscipline(ClsDicipline cb)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("DELETE  from tDicipline WHERE code ='" + cb.Code + "'", con);
                dt = new SqlDataAdapter(cmd);
                Object result = cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("La suppression a reussie !!! ");
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!! ");
            }
        }

        public DataTable chargementDiscipline(string Annee)
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select TOP 100 * from viewDiscipline where CodeAnnee = '"+Annee+"' ", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public DataTable rechercheDiscipline(string recherche,string Annee)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from viewDiscipline WHERE (nom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR prenom LIKE '%" + recherche + "%' OR Periode LIKE '%" + recherche + "%' OR Menssion LIKE '%" + recherche + "%') and CodeAnnee= "+Annee+"", con);
            dt.Fill(table);
            con.Close();
            return table;
        }

        //===================================================================================================
        //=================================================================================================
        // POUR LA MENSSION

        public void insertMenssion(ClsMenssion cb)
        {

            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("exec MergeMenssion @a,@b", con);
                setParameter(cmd, "@a", DbType.String, 20, cb.Code);
                setParameter(cmd, "@b", DbType.String, 90, cb.Designation1);             
                
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sauvegarde reussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }      

        public void supprimerMenssion(ClsMenssion cb)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("DELETE  from tMenssion WHERE code ='" + cb.Code + "'", con);
                dt = new SqlDataAdapter(cmd);
                Object result = cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("La suppression a reussie !!! ");
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!! ");
            }
        }

        public DataTable chargementMenssion()
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from tMenssion", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public DataTable chargementStatistique( string Annee)
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from viewStatistiqueInscription where Annee='"+Annee+"'", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public DataTable chargementStatistique()
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from viewStatistiqueInscription ", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public DataTable chargementStatReussite(string Nomprocedure,string Annee)
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("exec "+Nomprocedure+" '"+Annee+"'", con);
            //setParameter(cmd, "@a", DbType.String, 20, Annee);
            dt.Fill(table);
            con.Close();

            return table;
        }

        

        public DataTable rechercheMenssion(string recherche)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from tMenssion WHERE Designation LIKE '%" + recherche + "%'", con);
            dt.Fill(table);
            con.Close();
            return table;
        }

        public DataTable rechercheInscription(string recherche,string Annee)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select TOP 100 * from liste_inscription WHERE (codeinscription LIKE '%" + recherche + "%' or nom LIKE '%" + recherche + "%' or postnom LIKE '%" + recherche + "%' or prenom LIKE '%" + recherche + "%' or Classe LIKE '%" + recherche + "%' or codesection LIKE '%" + recherche + "%' or codeoption LIKE '%" + recherche + "%') and annee= "+Annee+" ", con);
            dt.Fill(table);
            con.Close();
            return table;
        }

        public DataTable SearchInscriptionPresence(string Annee)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select TOP 100 codeinscription,codeeleve,nom,postnom,prenom,Designation_classe as Classe,codesection,codeoption,Designation_annee as Annee from liste_inscription where annee = '"+Annee+"' ", con);
            dt.Fill(table);
            con.Close();
            return table;
        }
        public void chargementcombo_Mension_designe(System.Windows.Forms.ComboBox comb1)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT Designation FROM tMenssion", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    comb1.Items.Add(dr["Designation"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void chargementcombo_Mension_text(TextBox champs, string texte)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT * FROM tMenssion where Designation = '" + texte + "'", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    champs.Text = (dr["code"]).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public void insertMessagerie(ClsMessagerieInsert cb)
        {

            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("insert into tMessagerie values (@NumeroTutaire,@CorpsMessage,@DateEnvoie,@EtatSms,@Utilisateur)", con);
                setParameter(cmd, "@NumeroTutaire", DbType.String, 20, cb.Numero1);
                setParameter(cmd, "@CorpsMessage", DbType.String, 90, cb.MessateTexte1);
                setParameter(cmd, "@DateEnvoie", DbType.DateTime, 20, cb.DateEvoie1);
                setParameter(cmd, "@EtatSms", DbType.Int32, 90, cb.EtatSms1);
                setParameter(cmd, "@Utilisateur", DbType.String, 90, cb.Utilisateur1);
                cmd.ExecuteNonQuery();
                con.Close();
                //MessageBox.Show("Envoie du message pris en charge par le serveur !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //===================================================================================================
        //=================================================================================================
        // POUR LA DISCIPLINE

        public void insertPresence(ClsPresence cb)
        {

            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("insert into tPresence values (@RefEleve,@HeureArriver,@HeuereSortie,@DatePresnce,@userSession)", con);
                setParameter(cmd, "@RefEleve", DbType.Int32, 20, cb.RefEleve1);
                setParameter(cmd, "@HeureArriver", DbType.String, 20, cb.HeuereArriver1);
                setParameter(cmd, "@HeuereSortie", DbType.String, 20, cb.HeuerSortie1);
                setParameter(cmd, "@DatePresnce", DbType.DateTime, 20, cb.DatePresence1);
                setParameter(cmd, "@userSession", DbType.String, 90, cb.Utilisateur1);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sauvegarde reussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void updatePresence(ClsPresence cb)
        {

            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("update tPresence set RefEleve=@RefEleve,HeureArriver=@HeureArriver,HeuereSortie=@HeuereSortie,DatePresnce=@DatePresnce,userSession=@userSession where CodePres=@CodePres ", con);
                setParameter(cmd, "@CodePres", DbType.Int32, 20, cb.CodePres);
                setParameter(cmd, "@RefEleve", DbType.Int32, 20, cb.RefEleve1);
                setParameter(cmd, "@HeureArriver", DbType.String, 20, cb.HeuereArriver1);
                setParameter(cmd, "@HeuereSortie", DbType.String, 20, cb.HeuerSortie1);
                setParameter(cmd, "@DatePresnce", DbType.DateTime, 20, DateTime.Now);
                setParameter(cmd, "@userSession", DbType.String, 90, cb.Utilisateur1);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sauvegarde reussie!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        public void supprimerPresence(ClsPresence cb)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("DELETE  from tPresence WHERE CodePres ='" + cb.CodePres + "'", con);
                dt = new SqlDataAdapter(cmd);
                Object result = cmd.ExecuteScalar();
                con.Close();
                MessageBox.Show("La suppression a reussie !!! ");
            }

            catch
            {
                MessageBox.Show("La suppression a echouee !!! ");
            }
        }

        public DataTable chargementPresence(string Annee)
        {

            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select TOP 100 * from viewPresence where CodeAnnee = '" + Annee + "' ", con);
            dt.Fill(table);
            con.Close();

            return table;
        }


        //=====================================================
        public DataSet get_Report_X(string nomTable, string nomchamp, string valchamp)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM " + nomTable + " WHERE " + nomchamp + "='" + valchamp + "'", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, nomTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }
        public DataSet get_Report_S(string nomTable, string idTable)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM " + nomTable + " ORDER BY " + idTable + "", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, nomTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }
        public DataSet StatistiqueP1(string Annee,string NomProcedure)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("exec " + NomProcedure + " '" + Annee + "'", con);
                //setParameter(cmd, "@Annee", DbType.String, 30, Annee);                
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, NomProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }
        // MODIFICATION PARAMETRE

        public void insert_Paramatre(ClsParametre cb, string nomTable)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("insert into " + nomTable + " values (@a)", con);
                cmd.Parameters.AddWithValue("@a", cb.Designation);
                DialogResult res = MessageBox.Show("voulez vous vraiment enregistrer cette operation?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Enregistrement réussi avec succès", "Message de confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Opération Annulée!");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        public void update_Parametre(ClsParametre cb, string nomTable, string code, string designe)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("update " + nomTable + " set " + designe + "=@b where " + code + "=@a", con);
                cmd.Parameters.AddWithValue("@a", cb.Code);
                cmd.Parameters.AddWithValue("@b", cb.Designation);

                DialogResult res = MessageBox.Show("voulez vous vraiment enregistrer cette operation?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Enregistrement réussi avec succès", "Message de confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Opération Annulée!");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }


        public void update_Parametre2(ClsParametre cb, string nomTable, string code, string designe,string refcode)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("update " + nomTable + " set " + designe + "=@b,"+refcode+" = @c  where " + code + "=@a", con);
                cmd.Parameters.AddWithValue("@a", cb.Code);
                cmd.Parameters.AddWithValue("@b", cb.Designation);
                cmd.Parameters.AddWithValue("@c", cb.RefCode);
                DialogResult res = MessageBox.Show("voulez vous vraiment enregistrer cette operation?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Enregistrement réussi avec succès", "Message de confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Opération Annulée!");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        public DataTable recherche_Messagerie(string NomTable, string Nom, string Numero, string recherche)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select " + Nom + "," + Numero + " from " + NomTable + " WHERE " + Nom + " LIKE '%" + recherche + "%' ", con);
            dt.Fill(table);
            con.Close();
            return table;
        }

        public DataTable chargementGrid(string nomTable)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from " + nomTable + "", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public DataTable chargementGrid1(string nomTable)
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select cours from " + nomTable + "", con);
            dt.Fill(table);
            con.Close();

            return table;
        }




    }
}
