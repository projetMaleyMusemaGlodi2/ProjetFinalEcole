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
    public partial class FormPresence : Form
    {
        public FormPresence()
        {
            InitializeComponent();
        }


        ClsPresence Pres = new ClsPresence();

        private void grid_view_clic_Presence()
        {
            try
            {

                if (gridView1.RowCount < 1)
                {

                }
                else
                {//
                    txtcode.Text = gridView1.GetFocusedRowCellValue(gridView1.Columns["CodePres"]).ToString();
                    txtNomEleve.Text = gridView1.GetFocusedRowCellValue(gridView1.Columns["nom"]).ToString();
                    txtcodeEleve.Text = gridView1.GetFocusedRowCellValue(gridView1.Columns["RefEleve"]).ToString();
                    txtHeureArriver.Text = gridView1.GetFocusedRowCellValue(gridView1.Columns["HeureArriver"]).ToString();
                    txtHeureSortie.Text = gridView1.GetFocusedRowCellValue(gridView1.Columns["HeuereSortie"]).ToString();
                    txtDatePres.Text = gridView1.GetFocusedRowCellValue(gridView1.Columns["DatePresnce"]).ToString();

                }

            }
            catch (Exception)
            { }
        }
        void SaveUpdateDelete(int i)
        {
            if (i == 1 | i == 2)
            {
                if (txtcodeEleve.Text == "" || txtHeureArriver.Text == "" || txtHeureSortie.Text == "" || txtDatePres.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!", "Erreur de Sauvegarde", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (DateTime.Parse(txtHeureArriver.Text) < DateTime.Parse(txtHeureSortie.Text))
                    {
                        Pres.RefEleve1 = int.Parse(txtcodeEleve.Text);
                        Pres.HeuereArriver1 = txtHeureArriver.Text;
                        Pres.HeuerSortie1 = txtHeureSortie.Text;
                        Pres.DatePresence1 = DateTime.Parse(txtDatePres.Text);
                        Pres.Utilisateur1 = UserSession.GetInstance().AccessLevel;
                        if (i == 1)
                        {
                            ClIntelligence.GetInstance().insertPresence(Pres);
                            gridControl1.DataSource = ClIntelligence.GetInstance().chargementPresence(UserSession.GetInstance().Annee);
                        }
                        else if (i == 2)
                        {
                            if (txtcode.Text == "")
                            {
                                MessageBox.Show("Selection l'Enregistrement dans la liste ci-dessous !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                Pres.CodePres = int.Parse(txtcode.Text);
                                ClIntelligence.GetInstance().updatePresence(Pres);
                                gridControl1.DataSource = ClIntelligence.GetInstance().chargementPresence(UserSession.GetInstance().Annee);
                            }
                        }
                    }
                    else {
                        MessageBox.Show("L'heure de sortie doit etre superieur a l'heure d'arriver svp", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }


            }
            else if (i == 3)
            {
                if (txtcode.Text == "")
                {
                    MessageBox.Show("Selection l'Enregistrement a supprimer dans la liste ci-dessous !!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Pres.CodePres = int.Parse(txtcode.Text);
                    ClIntelligence.GetInstance().supprimerPresence(Pres);
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementPresence(UserSession.GetInstance().Annee);
                }
            }

        }


        private void FormPresence_Load(object sender, EventArgs e)
        {
            txtDatePres.Text = DateTime.Now.ToString();
            txtHeureArriver.Text = DateTime.Now.TimeOfDay.ToString();

            gridControl1.DataSource = ClIntelligence.GetInstance().chargementPresence(UserSession.GetInstance().Annee);
            gridControl2.DataSource = ClIntelligence.GetInstance().SearchInscriptionPresence(UserSession.GetInstance().Annee);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
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

        private void button2_Click(object sender, EventArgs e)
        {
            SaveUpdateDelete(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveUpdateDelete(2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveUpdateDelete(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtcode.Text = "";
            txtcodeEleve.Text = "";
            txtNomEleve.Text = "";
            //txtHeureArriver.Text = "";
            txtHeureSortie.Text = "";
            //txtDatePres.Text = "";
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            grid_view_clic_Presence();
        }
    }
}
