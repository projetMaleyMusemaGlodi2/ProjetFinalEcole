using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ecole.Classe;

namespace Ecole
{
    public partial class FormEcole : Form
    {
        public FormEcole()
        {
            InitializeComponent();
        }

        ClsEcole ecole = new ClsEcole();


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcode.Text == "" | txtadresseEntreprise.Text == "" | txtmailEntreprise.Text == "" | txtnomentreprise.Text == "" | txttelephone.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!!");
                }
                else
                {
                    ecole.CodeEcol = int.Parse(txtcode.Text);
                    ecole.NomEcol = txtnomentreprise.Text;
                    ecole.AdresseEcol = txtadresseEntreprise.Text;
                    ecole.TeleohoneEcol = txttelephone.Text;
                    ecole.MailEcol = txtmailEntreprise.Text;
                    ecole.Logo = photo.Image;
                    ecole.VilleEcole = txtVille.Text;
                    ecole.CommuneEcole = txtcommune.Text;
                    ecole.CodeEcole = txtCodeEcole.Text;
                    ClIntelligence.GetInstance().insertEcole(ecole);
                    dataGridView2.DataSource = ClIntelligence.GetInstance().chargement_Ecole();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtadresseEntreprise.Text = "";
            txtcode.Text = "";
            txtmailEntreprise.Text = "";
            txtnomentreprise.Text = "";
            txttelephone.Text = "";
            txtVille.Text = "";
            txtcommune.Text = "";
            txtCodeEcole.Text = "";
            photo.Image = Image.FromFile(Environment.CurrentDirectory + @"\library\Image2.jpg");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcode.Text == "")
                {
                    MessageBox.Show("Selectionnez l'ecole supprimer dans la liste des ecoles svp !!!!");
                }
                else
                {
                    ecole.CodeEcol = int.Parse(txtcode.Text);                    
                    ClIntelligence.GetInstance().supprimer_Ecole(ecole);
                    dataGridView2.DataSource = ClIntelligence.GetInstance().chargement_Ecole();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormEcole_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = ClIntelligence.GetInstance().chargement_Ecole();
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = ClIntelligence.GetInstance().recherche_Magasin(textEdit1.Text);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i;
                i = dataGridView2.CurrentRow.Index;
                txtcode.Text = dataGridView2["codeEcol", i].Value.ToString();
                txtnomentreprise.Text = dataGridView2["nomEcol", i].Value.ToString();
                txtadresseEntreprise.Text = dataGridView2["adresseEcol", i].Value.ToString();
                txttelephone.Text = dataGridView2["telephoneEcol", i].Value.ToString();
                txtmailEntreprise.Text = dataGridView2["mailEcol", i].Value.ToString();
                txtVille.Text = dataGridView2["villeEcole", i].Value.ToString();
                txtcommune.Text = dataGridView2["CommuneEcole", i].Value.ToString();
                txtCodeEcole.Text = dataGridView2["CodeEcole", i].Value.ToString();
                ClIntelligence.GetInstance().affichepho_Ecole(txtcode.Text, photo);


                //

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
