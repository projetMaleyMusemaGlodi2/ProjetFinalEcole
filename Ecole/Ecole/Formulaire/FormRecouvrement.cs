using DevExpress.XtraReports.UI;
using Ecole.Classe;
using Ecole.report;
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
    public partial class FormRecouvrement : Form
    {
        public FormRecouvrement()
        {
            InitializeComponent();
        }

        parametre par1 = new parametre();
        eleve el1 = new eleve();
        private void FormRecouvrement_Load(object sender, EventArgs e)
        {
            par1.chargementcombo_section_designe(cmbSectionBulletin);
            par1.chargementcombo_option_designe(cmbOptionBulletin);
            el1.chargementcombotypefrais(CmbFrais);
            ClIntelligence.GetInstance().chargementcombo_classe_designe(cmbClasseBulletin);
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbanneeBulletin);
        }

        private void cmbClasseBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_classe_text(cmbClasseBulletin, txtcomboClasseBulletin, cmbClasseBulletin.Text);
        }

        private void cmbSectionBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            par1.chargementcombo_section_saisir(cmbSectionBulletin, txtComboSectionBulletin, cmbSectionBulletin.Text);
        }

        private void cmbOptionBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool teste = ClIntelligence.GetInstance().teste_Option(cmbOptionBulletin.Text, txtComboSectionBulletin.Text);

            if (teste == true)
            {
                par1.chargementcombo_option_saisir(cmbOptionBulletin, txtcomboOptionBulletin, cmbOptionBulletin.Text);
            }
            else
            {
                MessageBox.Show("Option est invalide a la section !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void cmbanneeBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbanneeBulletin, txtcomboAnneeBulletin, cmbanneeBulletin.Text);
        }

        private void CmbFrais_SelectedIndexChanged(object sender, EventArgs e)
        {
            el1.sairie_code_frais(txtcomboFrais, CmbFrais.Text);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                bool teste = ClIntelligence.GetInstance().teste_Option(cmbOptionBulletin.Text, txtComboSectionBulletin.Text);
                if (teste == true)
                {
                    ListeRecouvrement rpt = new ListeRecouvrement();
                    rpt.DataSource = clreport.GetInstance().Recouvrement("viewTotalRecouvrement", txtcomboClasseBulletin.Text, txtComboSectionBulletin.Text, txtcomboOptionBulletin.Text, txtcomboAnneeBulletin.Text, txtcomboFrais.Text,double.Parse(txtmontant.Text));
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                else
                {
                    MessageBox.Show("L'option est invalide a la section svp !!!!", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {          

        }
    }
}
