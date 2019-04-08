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
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Ecole.report;

namespace Ecole.Formulaire
{
    public partial class FormRapportComptabilite : Form
    {
        public FormRapportComptabilite()
        {
            InitializeComponent();
        }

        private void FormRapportComptabilite_Load(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmbprevision);
        }

        private void cmbprevision_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_text(cmbprevision, txtcomboprevision, cmbprevision.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtcomboprevision.Text == "")
            {
                MessageBox.Show("Entrez l'annee svp !!!!!");
            }
            else {
               
                try
                {
                    RapportPrevison rpt = new RapportPrevison();
                    rpt.DataSource = clreport.GetInstance().ListePrevision("liste_prevision", txtcomboprevision.Text);
                    using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                    {
                        printTool.ShowPreviewDialog();
                    }
                }
                catch (Exception ex)
                { XtraMessageBox.Show(ex.Message); }
            }
        }
    }
}
