using DevExpress.XtraEditors;
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
    public partial class FormPalmaress : Form
    {
        public FormPalmaress()
        {
            InitializeComponent();
        }

        void chargeChat()
        {
            try
            {
                chart1.Series["Nombre"].XValueMember = "Classe";
                chart1.Series["Nombre"].YValueMembers = "NombreInscription";
                chart1.DataSource = ClIntelligence.GetInstance().chargementStatistique(cmbAnnee.Text); 
                chart1.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormPalmaress_Load(object sender, EventArgs e)
        {
            //chargeChat();
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbAnnee);

            GridStatistique.DataSource = ClIntelligence.GetInstance().chargementStatistique();
        }

        private void cmbAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbAnnee, txtcomboAnnee, cmbAnnee.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cmbAnnee.Text == "")
            {
                MessageBox.Show("Entrez l'annee svp !!!!!");
            }
            else
            {

                try
                {
                    StatistiqueInscription rpt = new StatistiqueInscription();
                    rpt.DataSource = ClIntelligence.GetInstance().get_Report_X("viewStatistiqueInscription", "Annee", cmbAnnee.Text);
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            chargeChat();
        }
    }
}
