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
   public class clreport
    {


        OpenFileDialog ofdImage = new OpenFileDialog();

        public SqlConnection myconn = new SqlConnection();
        public SqlCommand mycomm = new SqlCommand();
        public SqlDataAdapter adpt1 = new SqlDataAdapter();
        connexion ap = new connexion();
        
        

        private static clreport glos;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter dt = null;
        //SqlDataReader dr = null;
        

        public void innitialiseConnect()
        {
            try
            {
                ap.connect();
                con = new SqlConnection(ap.chemin);
            }
            catch (Exception)
            {
                throw new Exception("l'un de vos fichiers de configuration est incorrect");
            }

        }
        public static clreport GetInstance()
        {
            if (glos == null)
                glos = new clreport();
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






        public DataSet liste_inscription(string nomTable,String code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from liste_inscription WHERE codeinscription ='" + code + "'", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "liste_inscription");
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




        public DataSet liste_presence_co(string nomTable, int code,string affect,string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from liste_inscription WHERE Classe =" + code + " and division='" + affect + "' and annee= "+annee+"", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "liste_inscription");
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


        public DataSet liste_presence_moyen(string nomTable, int code, string affect,string section,string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from liste_inscription WHERE Classe =" + code + " and division='" + affect + "' and section ='" + section + "' and annee= "+annee+"", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "liste_inscription");
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

        public DataSet liste_presence_final(string nomTable, int code, string affect, string section,string option,string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from liste_inscription WHERE Classe =" + code + " and division='" + affect + "' and section ='" + section + "' and optioneleve='" + option + "' and annee= "+annee+"", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "liste_inscription");
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

        public DataSet RapportTrie(string nomTable, string Cclasse, string Csection, string CoptionEleve, string Cannee, string Vclasse, string Vsection, string VoptionEleve, string Vannee,string champ )
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from "+nomTable+"  where "+Cclasse+" = '"+Vclasse+"' and "+Csection+" = '"+Vsection+"' and "+CoptionEleve+" = '"+VoptionEleve+"' and "+Cannee+" = "+Vannee+ "   order by "+champ+" ASC  ", con);
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

        public DataSet RapportTrie(string nomTable,string Cannee,string Vannee, string champ)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from " + nomTable + "  where " + Cannee + " = '" + Vannee + "' order by " + champ + " ASC  ", con);
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


        public DataSet RapportTrie(string nomTable, string CEnseignant,string Cannee, string Venseignat, string Vannee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from " + nomTable + "  where " + CEnseignant + " = '" + Venseignat + "' and " + Cannee + " = " + Vannee + " ", con);
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


        public DataSet get_Report_Trier(string nomTable, string nomchamp, DateTime val1, DateTime val2)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM " + nomTable + " WHERE " + nomchamp + " between @date1 and @date2 ", con);
                setParameter(cmd, "@date1", DbType.DateTime, 30, val1);
                setParameter(cmd, "@date2", DbType.DateTime, 30, val2);
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
        public DataSet liste_identite_tout(string nomTable,string champ)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from "+nomTable+" order by "+champ+" ASC", con);
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

        public DataSet liste_eleve_generale(string nomTable)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from liste_inscription order by nom,Classe", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "liste_inscription");
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


        public DataSet liste_inscription_tout(string nomTable)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from billet_inscription", con);
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


        public DataSet liste_inscription_classe(string nomTable, String code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from billet_inscription WHERE Classe ='" + code + "'", con);
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





        public DataSet liste_paiement(string nomTable, String code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from view_paiement_vrai WHERE Num_Recus ='" + code + "'", con);
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


        public DataSet liste_total_paiement(string nomTable, int code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from total_paiement WHERE Classe ='" + code + "' ", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "total_paiement");
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


        public DataSet liste_total_globale(string nomTable)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM " + nomTable + "", con);
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




        public DataSet Identite_eleve(string nomTable, String code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM Eleve WHERE codeeleve ='" + code + "'", con);
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

        public DataSet Identite_enseignant(string nomTable, String code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM Enseignant WHERE matriculeEns ='" + code + "'", con);
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


        public DataSet liste_bibliotheque(string nomTable, int code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from emprunt_bibliotheque WHERE num ='" + code + "' ", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "emprunt_bibliotheque");
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

        public DataSet liste_cours(string nomTable, string code,string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from liste_affectation WHERE classe ='" + code + "' and codeanne="+annee+ " and code_periode=1 ", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "liste_affectation");
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

        public DataSet liste_cours_moyen(string nomTable, string code,string section,string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from liste_affectation WHERE classe ='" + code + "' and section ='" + section + "' and codeanne="+annee+ " and code_periode=1", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "liste_affectation");
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

        public DataSet liste_cours_final(string nomTable, string ValClasse, string Valsection, string Valoption,string Valannee)
        {
            DataSet dst;
            try
            {   
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from "+nomTable+" WHERE classe = '" + ValClasse + "' and section='" + Valsection + "' and optioneleve ='" + Valoption + "' and codeanne="+Valannee+ " and code_periode=1", con);
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
        //===================================================================================

        public DataSet liste_pourcent_co(string nomTable, string code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from Pourcentage_General WHERE Classe ='" + code + "'", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "Pourcentage_General");
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

        public DataSet liste_pourcent_moyen(string nomTable, string code, string section)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from Pourcentage_General WHERE Classe ='" + code + "' and Section ='" + section + "'", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "Pourcentage_General");
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

        public DataSet liste_pourcent_final(string nomTable, string code, string section, string option)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from Pourcentage_General WHERE Classe = '" + code + "' and Section='" + section + "' and Option_Eleve ='" + option + "'", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "Pourcentage_General");
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
        //=======================================================================================
        // LA CAISSE

        public DataSet borderau_sortie(string nomTable, String code)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from view_depense WHERE code_dep ='" + code + "'", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "view_depense");
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



        public DataSet billetin(string nomTable, String code,string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from Proclamation_Final WHERE Codeeleve =@Codeeleve and CodeAnnee=@CodeAnnee", con);
                setParameter(cmd, "@Codeeleve", DbType.String, 20, code);
                setParameter(cmd, "@CodeAnnee", DbType.String, 20, annee);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "Proclamation_Final");
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
        public DataSet billetinClasse(string nomTable,  string classe,string section,string option,string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from Proclamation_Final WHERE CodeClasse="+classe+" and codesection='"+section+"' and Code_option='"+option+ "' and CodeAnnee='"+annee+"'", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "Proclamation_Final");
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



        public DataSet HoraireClasse(string nomTable, string classe, string section, string option, string annee,string Affect)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from "+nomTable+ " WHERE codeclasse='" + classe + "' and code_section='" + section + "' and codeop='" + option + "' and codeanne=" + annee + " and affectation='"+Affect+ "' order by codejours", con);
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

        public DataSet Recouvrement(string nomTable, string classe, string section, string option, string annee, string Frais,double montant)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from " + nomTable + " WHERE codecl='" + classe + "' and CodeSection='" + section + "' and codeop='" + option + "' and CodeAnnee=" + annee + " and CodeFrais='" + Frais + "' and Total>="+montant+" order by nom", con);
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

        public DataSet TesteListeEns(string nomTable)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from " + nomTable + " ", con);
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


        public DataSet billetinTous(string nomTable,string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from Proclamation_Final where CodeAnnee=@CodeAnnee ", con);
                setParameter(cmd, "@CodeAnnee", DbType.String, 20, annee);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "Proclamation_Final");
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


        public DataSet ListePrevision(string nomTable, string annee)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select * from liste_prevision where codeanne=@codeanne ", con);
                setParameter(cmd, "@codeanne", DbType.String, 20, annee);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "liste_prevision");
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

       


        public DataSet livre(DateTime date1, DateTime date2)
        {
            DataSet dst;
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("exec sp_print_livre_caisse @date1,@date2", con);
                setParameter(cmd, "@date1", DbType.Date, 20, date1);
                setParameter(cmd, "@date2", DbType.Date, 20, date2);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, "sp_print_livre_caisse");
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


        public DataSet liste_livre(DateTime date1,DateTime date2)
        {
            DataSet dta = new DataSet();
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("exec sp_print_livre_caisse 'Compt1','" + date1 + "','"+date2+"'", con);
                dt = new SqlDataAdapter(cmd);
                dt.Fill(dta, "sp_print_livre_caisse");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dta;

        }





    }
}
