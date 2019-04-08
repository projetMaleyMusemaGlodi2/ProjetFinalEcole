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
using Ecole.report;
using DevExpress.XtraReports.UI;

namespace Ecole.Formulaire
{
    public partial class FormHoraire : Form
    {
        public FormHoraire()
        {
            InitializeComponent();
        }

        parametre par1 = new parametre();
        
        clsBase deb;
        ClIntelligence glo;
        private void chargerComboAdresse2(string nomTable, string ChampDesigne, System.Windows.Forms.ComboBox combo, string champ, string valeur)
        {
            glo = new ClIntelligence();
            deb = new clsBase();
            deb.NomTable1 = nomTable;
            deb.NomChamp1 = ChampDesigne;
            glo.loadCombo(deb, combo, champ, valeur);
        }
        private void FormHoraire_Load(object sender, EventArgs e)
        {
            par1.chargementcombo_section_designe(cmbSectionBulletin);
            //par1.chargementcombo_option_designe(cmbOptionBulletin);
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

            chargerComboAdresse2("option1", "optioneleve", cmbOptionBulletin, "codesect", txtComboSectionBulletin.Text);
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                bool teste = ClIntelligence.GetInstance().teste_Option(cmbOptionBulletin.Text, txtComboSectionBulletin.Text);
                if (teste == true)
                {
                    HoaraireCours rpt = new HoaraireCours();
                    rpt.DataSource = clreport.GetInstance().HoraireClasse("horaire", txtcomboClasseBulletin.Text, txtComboSectionBulletin.Text, txtcomboOptionBulletin.Text, txtcomboAnneeBulletin.Text,comboBox1.Text);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                HoaraireCours rpt = new HoaraireCours();
                rpt.DataSource = clreport.GetInstance().TesteListeEns("horaire");
                //rpt.DataSource = clreport.GetInstance().HoraireClasse("horaire", txtcomboClasseBulletin.Text, txtComboSectionBulletin.Text, txtcomboOptionBulletin.Text, txtcomboAnneeBulletin.Text, comboBox1.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
