using Ecole.Classe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecole.Formulaire
{
    public partial class FormParametreEncours : Form
    {
        public FormParametreEncours()
        {
            InitializeComponent();
        }

        eleve el1 = new eleve();
        parametre par1 = new parametre();
        cours cr1 = new cours();
        ClEncours encours = new ClEncours();
        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtcodeEncours.Text == "")
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcodeEncours.Text == "" | txtcomboAnnee.Text == "" | txtcomboClasse.Text == "" | txtcomboPeriode.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!!!");
                }
                else
                {
                    encours.CodeEncours = int.Parse(txtcodeEncours.Text);
                    encours.RefAnnee1 = int.Parse(txtcomboAnnee.Text);
                    encours.Refclasse1 = txtcomboClasse.Text;
                    encours.Refperiode1 = txtcomboPeriode.Text;
                    ClIntelligence.GetInstance().insertEcours(encours);
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargement_Encours();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void FormParametreEncours_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = ClIntelligence.GetInstance().chargement_Encours();
            ClIntelligence.GetInstance().chargementcombo_periode_designe(cmbPeriode);          
            
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbAnnee);
            ClIntelligence.GetInstance().chargementcombo_classe_designe(cmbClasse);
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = ClIntelligence.GetInstance().recherche_Encours(textEdit1.Text);
            
        }

        private void cmbAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbAnnee, txtcomboAnnee, cmbAnnee.Text);          
        }

        private void cmbPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_periode_text(cmbPeriode, txtcomboPeriode, cmbPeriode.Text);            
        }

        private void cmbClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_classe_text(cmbClasse, txtcomboClasse, cmbClasse.Text);
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                txtcodeEncours.Text = gridView1.GetFocusedRowCellValue("code").ToString();
                txtcomboAnnee.Text = gridView1.GetFocusedRowCellValue("RefAnnee").ToString();
                txtcomboPeriode.Text = gridView1.GetFocusedRowCellValue("RefPeriode").ToString();
                txtcomboClasse.Text = gridView1.GetFocusedRowCellValue("RefClasse").ToString();
                cmbAnnee.Text= gridView1.GetFocusedRowCellValue("annee").ToString();
                cmbClasse.Text= gridView1.GetFocusedRowCellValue("Classe").ToString();
                cmbPeriode.Text= gridView1.GetFocusedRowCellValue("Periode").ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
