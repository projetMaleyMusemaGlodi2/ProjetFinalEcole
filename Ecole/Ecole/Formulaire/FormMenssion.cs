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
    public partial class FormMenssion : Form
    {
        public FormMenssion()
        {
            InitializeComponent();
        }

        ClsMenssion mension = new ClsMenssion();

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtcode.Text = "";
            txtdesignation.Text = "";
        }

        private void FormMenssion_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = ClIntelligence.GetInstance().chargementMenssion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcode.Text == "" || txtdesignation.Text == "")
                {
                    MessageBox.Show("Completez tous les champs svp !!!");
                }
                else {
                    mension.Code = txtcode.Text;
                    mension.Designation1 = txtdesignation.Text;
                    ClIntelligence.GetInstance().insertMenssion(mension);
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementMenssion();
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcode.Text == "" )
                {
                    MessageBox.Show("Entrez le code de la mension a supprimer svp !!!");
                }
                else
                {
                    mension.Code = txtcode.Text;                    
                    ClIntelligence.GetInstance().supprimerMenssion(mension);
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementMenssion();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            gridControl1.DataSource = ClIntelligence.GetInstance().rechercheMenssion(textBox3.Text);
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                txtcode.Text = gridView1.GetFocusedRowCellValue("code").ToString();
                txtdesignation.Text = gridView1.GetFocusedRowCellValue("Designation").ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
