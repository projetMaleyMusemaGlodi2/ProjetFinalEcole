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
    public partial class FormStatReussiteEchec : Form
    {
        public FormStatReussiteEchec()
        {
            InitializeComponent();
        }

        private void FormStatReussiteEchec_Load(object sender, EventArgs e)
        {
            ClIntelligence.GetInstance().chargementcombo_annee_designe(cmAnnee);
            //gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatistique();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPriode.Text == "1erePeriode")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriode1", cmAnnee.Text);
                }
                else if (cmbPriode.Text == "2emePeriode")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriode2", cmAnnee.Text);
                }
                else if (cmbPriode.Text == "3emePeriode")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriode3", cmAnnee.Text);
                }
                else if (cmbPriode.Text == "4emePeriode")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriode4", cmAnnee.Text);
                }
                else if (cmbPriode.Text == "Examen1")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriodeExamen1", cmAnnee.Text);
                }
                else if (cmbPriode.Text == "Examen2")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriodeExamen2", cmAnnee.Text);
                }
                else if (cmbPriode.Text == "Total1")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriodeTotal1", cmAnnee.Text);
                }
                else if (cmbPriode.Text == "Total2")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriodeTotal2", cmAnnee.Text);
                }
                else if (cmbPriode.Text == "TotalGeneral")
                {
                    gridControl1.DataSource = ClIntelligence.GetInstance().chargementStatReussite("StatPeriodeTG", cmAnnee.Text);
                }
                else
                {
                    MessageBox.Show("Choisissez une periode svp !!!", "Section Obligatoire", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Selection la periode et l'annee svp !!!", "Sections Obligatoires", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmbPriode.Text == "1erePeriode")
                {
                    try
                    {
                        StatPeriode1 rpt = new StatPeriode1();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriode1");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (cmbPriode.Text == "2emePeriode")
                {
                    try
                    {
                        StatPeriode2 rpt = new StatPeriode2();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriode2");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (cmbPriode.Text == "3emePeriode")
                {
                    try
                    {
                        StatPeriode3 rpt = new StatPeriode3();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriode3");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (cmbPriode.Text == "4emePeriode")
                {
                    try
                    {
                        StatPeriode4 rpt = new StatPeriode4();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriode4");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (cmbPriode.Text == "Examen1")
                {
                    try
                    {
                        StatExamen1 rpt = new StatExamen1();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriodeExamen1");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (cmbPriode.Text == "Examen2")
                {
                    try
                    {
                        StatExemen2 rpt = new StatExemen2();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriodeExamen2");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (cmbPriode.Text == "Total1")
                {
                    try
                    {
                        StatTotal1 rpt = new StatTotal1();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriodeTotal1");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (cmbPriode.Text == "Total2")
                {
                    try
                    {
                        StatTotal2 rpt = new StatTotal2();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriodeTotal2");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (cmbPriode.Text == "TotalGeneral")
                {
                    try
                    {
                        StatTG rpt = new StatTG();
                        rpt.DataSource = ClIntelligence.GetInstance().StatistiqueP1(cmAnnee.Text, "StatPeriodeTG");
                        using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                        {
                            printTool.ShowPreviewDialog();
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else
                {
                    MessageBox.Show("Choisissez une periode svp !!!", "Section Obligatoire", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Selection la periode et l'annee svp !!!", "Sections Obligatoires", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }

            
        }
    }
}
