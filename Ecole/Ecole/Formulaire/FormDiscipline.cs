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

namespace Ecole.Formulaire
{
    public partial class FormDiscipline : Form
    {
        public FormDiscipline()
        {
            InitializeComponent();
        }

        cours cr = new cours();
        ClsDicipline dis = new ClsDicipline();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = ClIntelligence.GetInstance().rechercheDiscipline(textBox1.Text,UserSession.GetInstance().Annee);
        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void FormDiscipline_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = ClIntelligence.GetInstance().chargementDiscipline(UserSession.GetInstance().Annee);
            cr.chargementcombo_periode_designe(cmbPeriode);
            ClIntelligence.GetInstance().chargementcombo_Mension_designe(CmbConduite);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            gridControl2.DataSource = ClIntelligence.GetInstance().rechercheInscription(textBox2.Text,UserSession.GetInstance().Annee);
        }

        private void textEdit5_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            cr.chargementcombo_periode_saisir(cmbPeriode, txtcomboPeriode, cmbPeriode.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtcode.Text = "";
            txtcodeEleve.Text = "";
            txtcomboPeriode.Text = "";
            txtcomboconduite.Text = "";
            txtNomEleve.Text = "";
            cmbPeriode.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcodeEleve.Text == "" || txtcomboPeriode.Text == "" || txtcomboconduite.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
                else {

                    if (ClIntelligence.GetInstance().teste_Discipline(txtcomboPeriode.Text, txtcodeEleve.Text) == true)
                    {
                        dis.RefEleve1 = int.Parse(txtcodeEleve.Text);
                        dis.RefMession1 = txtcomboconduite.Text;
                        dis.Periode = txtcomboPeriode.Text;
                        dis.UserCession = UserSession.GetInstance().AccessLevel;
                        ClIntelligence.GetInstance().insertDiscipline(dis);
                        gridControl1.DataSource = ClIntelligence.GetInstance().chargementDiscipline(UserSession.GetInstance().Annee);
                    }
                    else {
                        MessageBox.Show("Cette affection est déjà faite svp!!!!", "Doublons", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    }
                    
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_Mension_text(txtcomboconduite, CmbConduite.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcode.Text==""|| txtcodeEleve.Text == "" || txtcomboPeriode.Text == "" || txtcomboconduite.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
                else
                {
                    dis.Code =int.Parse(txtcode.Text);
                    dis.RefEleve1 = int.Parse(txtcodeEleve.Text);
                    dis.RefMession1 = txtcomboconduite.Text;
                    dis.Periode = txtcomboPeriode.Text;
                    dis.UserCession = UserSession.GetInstance().AccessLevel;
                    ClIntelligence.GetInstance().updateDiscipline(dis);
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementDiscipline(UserSession.GetInstance().Annee);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcode.Text == "")
                {
                    MessageBox.Show("Entrez le code de la discipline a supprimer svp !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
                else
                {
                    dis.Code = int.Parse(txtcode.Text);                    
                    ClIntelligence.GetInstance().supprimerDiscipline(dis);
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementDiscipline(UserSession.GetInstance().Annee);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            try
            {
                txtcodeEleve.Text = gridView2.GetFocusedRowCellValue("codeinscription").ToString();
                txtNomEleve.Text = gridView2.GetFocusedRowCellValue("nom").ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                txtcodeEleve.Text = gridView1.GetFocusedRowCellValue("RefEleve").ToString();
                txtNomEleve.Text = gridView1.GetFocusedRowCellValue("nom").ToString();
                txtcomboPeriode.Text= gridView1.GetFocusedRowCellValue("RefPeriode").ToString();
                txtcomboconduite.Text= gridView1.GetFocusedRowCellValue("RefMession").ToString();
                txtcode.Text= gridView1.GetFocusedRowCellValue("CodeD").ToString();
                CmbConduite.Text= gridView1.GetFocusedRowCellValue("Menssion").ToString();
                cmbPeriode.Text= gridView1.GetFocusedRowCellValue("Periode").ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
