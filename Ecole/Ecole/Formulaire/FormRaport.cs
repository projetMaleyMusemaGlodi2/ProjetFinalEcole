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
    public partial class FormRaport : Form
    {
        public FormRaport()
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
        private void simpleButton14_Click(object sender, EventArgs e)
        {
            try
            {
                TesteListeEns rpt = new TesteListeEns();
                rpt.DataSource = clreport.GetInstance().liste_identite_tout("viewListeEnseignants", "NomEns");
                using(ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        cours cr = new cours();

        private void FormRaport_Load(object sender, EventArgs e)
        {
            par1.chargementcombo_section_designe(cmbSectionBulletin);
            //par1.chargementcombo_option_designe(cmbOptionBulletin);            
            ClIntelligence.GetInstance().chargementcombo_classe_designe(cmbClasseBulletin);
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbanneeBulletin);
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbAnneeAffect);
            cr.chargementcombo_enseignant_designe(cmbEnseignant);
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

        private void cmbanneeBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbanneeBulletin, txtcomboAnneeBulletin, cmbanneeBulletin.Text);
        }

        private void cmbOptionBulletin_SelectedIndexChanged(object sender, EventArgs e)
        {
            par1.chargementcombo_option_saisir(cmbOptionBulletin, txtcomboOptionBulletin, cmbOptionBulletin.Text);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                La_liste_des_cours rpt = new La_liste_des_cours();
                rpt.DataSource = clreport.GetInstance().liste_cours_final("liste_affectation", cmbClasseBulletin.Text, cmbSectionBulletin.Text,cmbOptionBulletin.Text, txtcomboAnneeBulletin.Text);
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            try
            {
                RapportInscription rpt = new RapportInscription();
                rpt.DataSource = clreport.GetInstance().RapportTrie("liste_inscription", "Classe", "codesection", "codeoption", "annee", txtcomboClasseBulletin.Text,txtComboSectionBulletin.Text,txtcomboOptionBulletin.Text,txtcomboAnneeBulletin.Text, "nom");
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtdate1.Text == "" || txtdate2.Text == "")
            {
                MessageBox.Show("Entrez les date svp !!!");
            }
            else {
                try
                {
                    RapportInscription be = new RapportInscription();
                    be.DataSource = clreport.GetInstance().get_Report_Trier("liste_inscription", "DateInscription", DateTime.Parse(txtdate1.Text.Trim()), DateTime.Parse(txtdate2.Text.Trim()));
                    using (ReportPrintTool printTool = new ReportPrintTool(be))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            try
            {
                Liste_Eleve_dossiers rpt = new Liste_Eleve_dossiers();
                rpt.DataSource = clreport.GetInstance().liste_identite_tout("ViewIdentiteEleve", "nom");
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {
                RapportAffectation rpt = new RapportAffectation();
                rpt.DataSource = clreport.GetInstance().RapportTrie("liste_affectation", "Code_classe", "code_section", "codeop", "codeanne", txtcomboClasseBulletin.Text, txtComboSectionBulletin.Text, txtcomboOptionBulletin.Text, txtcomboAnneeBulletin.Text, "NomEns");
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                livre rpt = new livre();
                rpt.DataSource = clreport.GetInstance().livre(DateTime.Parse(txtdate1.Text), DateTime.Parse(txtdate2.Text));

                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtdate1.Text == "" || txtdate2.Text == "")
            {
                MessageBox.Show("Entrez les date svp !!!");
            }
            else
            {
                try
                {
                    RapportPaiementFrais be = new RapportPaiementFrais();
                    be.DataSource = clreport.GetInstance().get_Report_Trier("view_paiement_vrai", "datepay", DateTime.Parse(txtdate1.Text.Trim()), DateTime.Parse(txtdate2.Text.Trim()));
                    using (ReportPrintTool printTool = new ReportPrintTool(be))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (txtdate1.Text == "" || txtdate2.Text == "")
            {
                MessageBox.Show("Entrez les date svp !!!");
            }
            else
            {
                try
                {
                    RapportEmpruntBib be = new RapportEmpruntBib();
                    be.DataSource = clreport.GetInstance().get_Report_Trier("emprunt_bibliotheque", "date_retrait", DateTime.Parse(txtdate1.Text.Trim()), DateTime.Parse(txtdate2.Text.Trim()));
                    using (ReportPrintTool printTool = new ReportPrintTool(be))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            try
            {
                RapportListeLivre rpt = new RapportListeLivre();
                rpt.DataSource = clreport.GetInstance().TesteListeEns("viewListeLivre");
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            try
            {
                RapportListePresence rpt = new RapportListePresence();
                rpt.DataSource = clreport.GetInstance().RapportTrie("liste_inscription", "Classe", "codesection", "codeoption", "annee", txtcomboClasseBulletin.Text, txtComboSectionBulletin.Text, txtcomboOptionBulletin.Text, txtcomboAnneeBulletin.Text,"nom");
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (txtdate1.Text == "" || txtdate2.Text == "")
            {
                MessageBox.Show("Entrez les date svp !!!");
            }
            else
            {
                try
                {
                    RapportPresence be = new RapportPresence();
                    be.DataSource = clreport.GetInstance().get_Report_Trier("viewPresence", "DatePresnce", DateTime.Parse(txtdate1.Text.Trim()), DateTime.Parse(txtdate2.Text.Trim()));
                    using (ReportPrintTool printTool = new ReportPrintTool(be))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
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
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbEnseignant_SelectedIndexChanged(object sender, EventArgs e)
        {
            cr.chargementcombo_enseignant_saisir(cmbEnseignant, txtcomboEnseignant, cmbEnseignant.Text);
        }

        private void cmbAnneeAffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbAnneeAffect, txtcomboAnneeAffect, cmbAnneeAffect.Text);
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            if (txtcomboAnneeAffect.Text == "" || txtcomboEnseignant.Text == "")
            {
                MessageBox.Show("Selectionnez l'Enseignant et l'année svp !!!");
            }
            else {
                try
                {
                    RapportAffectation rpt = new RapportAffectation();
                    rpt.DataSource = clreport.GetInstance().RapportTrie("liste_affectation", "codeens", "codeanne", txtcomboEnseignant.Text, txtcomboAnneeAffect.Text);
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }

          
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            if (txtdate1.Text == "" || txtdate2.Text == "")
            {
                MessageBox.Show("Entrez les date svp !!!");
            }
            else
            {
                try
                {
                    RapprtMessagerie be = new RapprtMessagerie();
                    be.DataSource = clreport.GetInstance().get_Report_Trier("viewRapportMessagerie", "DateEnvoie", DateTime.Parse(txtdate1.Text.Trim()), DateTime.Parse(txtdate2.Text.Trim()));
                    using (ReportPrintTool printTool = new ReportPrintTool(be))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            try
            {
                ListeOption rpt = new ListeOption();
                rpt.DataSource = clreport.GetInstance().TesteListeEns("viewOption");
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

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            if (txtcomboAnneeAffect.Text == "")
            {
                MessageBox.Show("Selectionnez l'année svp !!!");
            }
            else {
                try
                {
                    RapportAffectationListe rpt = new RapportAffectationListe();
                    rpt.DataSource = clreport.GetInstance().RapportTrie("liste_affectation", "codeanne", txtcomboAnneeAffect.Text, "NomEns");
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }

           
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            if (txtcomboAnneeAffect.Text == "")
            {
                MessageBox.Show("Selectionnez l'année svp !!!");
            }
            else
            {
                try
                {
                    RapportAffectationListe rpt = new RapportAffectationListe();
                    rpt.DataSource = clreport.GetInstance().RapportTrie("liste_affectation", "codeanne", txtcomboAnneeAffect.Text, "NomEns");
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
        }
    }
}
