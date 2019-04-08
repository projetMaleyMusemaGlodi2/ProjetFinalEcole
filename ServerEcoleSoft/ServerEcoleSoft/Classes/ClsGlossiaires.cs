using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerEcoleSoft.Classes
{
    class ClsGlossiaires
    {
        connexion cnx = null;
        private static ClsGlossiaires glos;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter dt = null;
        SqlDataReader dr = null;
        private string str, code_isnt;
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
        public static ClsGlossiaires GetInstance()
        {
            if (glos == null)
                glos = new ClsGlossiaires();
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
       
        public DataTable chargementMessagerie()
        {
            innitialiseConnect();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from tMessagerie where EtatSms= 0 ", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public void backupBD()
        {

            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                string database = con.Database.ToString();

                string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + ClassConstantes.Table.CheminBackup + "\\" + database + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                using (SqlCommand command = new SqlCommand(cmd, con))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    command.ExecuteNonQuery();
                    con.Close();
                    //XtraMessageBox.Show("Sauvegarde effectué avec succés", "Confirmation Sauvegarde");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        public void update_Valmsg(string code)
        {
            try
            {
                innitialiseConnect();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("delete from tMessagerie where code=@code ", con);
                cmd.Parameters.AddWithValue("@code", code);                
                cmd.ExecuteNonQuery();              

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        

        ClsMessages ms = new ClsMessages();
         public void EnvoiMessagePlusieur(DataGridView Grid) {
            string touNum = "";
            string touMessage = "";
            string toutCode = "";

            if (Grid.Rows.Count > 0)
            {
                for (int i = 0; i < (Grid.Rows.Count) ; i++)
                {
                    touNum = Grid[1, i].Value.ToString();
                    touMessage = Grid[2, i].Value.ToString();
                    toutCode= Grid[0, i].Value.ToString();
                    if (touMessage.Length <= 140)
                    {
                        if (ms.sendlongMsg(touNum, touMessage + "                                                    "))
                        {
                            update_Valmsg(toutCode);
                            Grid.Rows.RemoveAt(i);
                        }
                        else {
                            MessageBox.Show("Un Message non envoye !!!!!");
                        }
                       
                        
                    }
                    else
                    {
                        if (ms.sendlongMsg(touNum, touMessage))
                        {
                            update_Valmsg(toutCode);
                            Grid.Rows.RemoveAt(i);
                        }
                        else
                        {
                            MessageBox.Show("Un Message non envoye !!!!!");
                        }

                    }


                }

            }
            else {
                MessageBox.Show("Pas de Message svp !!!");
            }
            
            
        }

        public void SendMessagePaiement()
        {
            bool envoie = true;
            string numero = "";
            string message = "";            
            string codeMs = "";
            string utilisateur = "";
            string dateEnvoie = "" ;
            string Etat = "";
            int count = 0;
            try
            {
                innitialiseConnect();
                con.Open();
                cmd = new SqlCommand("SELECT * FROM tMessagerie WHERE EtatSms = 0 ", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    numero = dr["NumeroTutaire"].ToString();
                    message = dr["CorpsMessage"].ToString();                    
                    codeMs = dr["code"].ToString();
                    dateEnvoie = dr["DateEnvoie"].ToString();
                    utilisateur = dr["Utilisateur"].ToString();
                    Etat = dr["EtatSms"].ToString();

                    if (numero != "" && message != "" && count!=1)
                    {
                        ClsMessages ms = new ClsMessages();
                        if (message.Length <= 140)
                        {
                            update_Valmsg(codeMs);
                            if (ms.sendshortMsg(numero, message) == false)
                            {
                                if (ms.sendlongMsg(numero, message + "                                                   ") == false) {
                                    envoie = false;
                                    ClsMessagerieInsert msInsert = new ClsMessagerieInsert();
                                    msInsert.Numero1 = numero;
                                    msInsert.MessateTexte1 = message;
                                    msInsert.DateEvoie1 = DateTime.Parse(dateEnvoie);
                                    msInsert.EtatSms1 = 0;
                                    msInsert.Utilisateur1 = utilisateur;
                                    ClsGlossiaires.GetInstance().insertMessagerie(msInsert);

                                    numero = "";
                                    message = "";
                                    codeMs = "";
                                    dateEnvoie = "";
                                    utilisateur = "";
                                    Etat = "";
                                }
                                
                            }
                            else {
                                envoie = true;
                                numero = "";
                                message = "";
                                codeMs = "";
                                dateEnvoie = "";
                                utilisateur = "";
                                Etat = "";
                            }
                            
                            
                        }
                        else {
                            update_Valmsg(codeMs);
                            if (ms.sendlongMsg(numero, message) == true)
                            {
                                numero = "";
                                message = "";
                                envoie = true;
                            }
                            else {
                                envoie = false;

                                ClsMessagerieInsert msInsert = new ClsMessagerieInsert();
                                msInsert.MessateTexte1 = message;
                                msInsert.DateEvoie1 = DateTime.Parse(dateEnvoie);
                                msInsert.EtatSms1 = 0;
                                msInsert.Utilisateur1 = utilisateur;
                                ClsGlossiaires.GetInstance().insertMessagerie(msInsert);
                            }
                            
                        }                        
                        //update set statutMessage='non'
                        

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                envoie = false;

                ClsMessagerieInsert msInsert = new ClsMessagerieInsert();
                msInsert.MessateTexte1 = message;
                msInsert.DateEvoie1 = DateTime.Parse(dateEnvoie);
                msInsert.EtatSms1 = 0;
                msInsert.Utilisateur1 = utilisateur;
                ClsGlossiaires.GetInstance().insertMessagerie(msInsert);
            }
            con.Close();

            //return envoie;
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


    }
}
