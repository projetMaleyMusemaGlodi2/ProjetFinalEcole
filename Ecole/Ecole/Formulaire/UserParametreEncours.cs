using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ecole.Classe;

namespace Ecole.Formulaire
{
    public partial class UserParametreEncours : UserControl
    {
        public UserParametreEncours()
        {
            InitializeComponent();
        }

        eleve el1 = new eleve();
        parametre par1 = new parametre();
        cours cr1 = new cours();
        ClEncours encours = new ClEncours();

        private void UserParametreEncours_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = ClIntelligence.GetInstance().chargement_Encours();
            
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void cmbAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            el1.chargementcombo_annee_saisir(cmbAnnee, txtcomboAnnee, cmbAnnee.Text);           

        }

        private void cmbPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            cr1.chargementcombo_periode_saisir(cmbPeriode, txtcomboPeriode, cmbPeriode.Text);
        }

        private void cmbClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_classe_text(cmbClasse, txtcomboClasse, cmbClasse.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtcodeEncours.Text = "";
            txtcomboAnnee.Text = "";
            txtcomboClasse.Text = "";
            txtcomboPeriode.Text = "";
            cmbClasse.Text = "";
            cmbPeriode.Text = "";
            cmbAnnee.Text = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcodeEncours.Text == "" | txtcomboAnnee.Text == "" | txtcomboClasse.Text == "" | txtcomboPeriode.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!!!");
                }
                else {
                    encours.CodeEncours = int.Parse(txtcodeEncours.Text);
                    encours.RefAnnee1 = int.Parse(txtcomboAnnee.Text);
                    encours.Refclasse1 = txtcomboClasse.Text;
                    encours.Refperiode1 = txtcomboPeriode.Text;
                    ClIntelligence.GetInstance().insertEcours(encours);
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargement_Encours();
                }
                
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcodeEncours.Text == "" )
                {
                    MessageBox.Show("Entrez le code de l'encours a supprimer svp !!!!!");
                }
                else
                {
                    encours.CodeEncours = int.Parse(txtcodeEncours.Text);                    
                    ClIntelligence.GetInstance().insertEcours(encours);
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargement_Encours();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
