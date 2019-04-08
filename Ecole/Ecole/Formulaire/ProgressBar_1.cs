using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecole
{
    public partial class ProgressBar_1 : Form
    {
        public ProgressBar_1()
        {
            InitializeComponent();
        }

        Ecole1 f2 = new Ecole1();
        private Task ProcessData(List<string> list, IProgress<ProgressReport> progress)
        {
            int index = 1;
            int totalProcess = list.Count;
            var progressReport = new ProgressReport();
            return Task.Run(() =>

            {
                for (int i = 0; i < totalProcess; i++)
                {
                    progressReport.percentcomplete = index++ * 100 / totalProcess;
                    progress.Report(progressReport);
                    Thread.Sleep(10);
                }
            });
        }

        private async void progression()
        {

            List<string> list = new List<string>();
            for (int i = 0; i < 100; i++)
                list.Add(i.ToString());
            label1.Text = "Working......";
            var progress = new Progress<ProgressReport>();
            //var report = new ProgressReport();
            progress.ProgressChanged += (o, report) => {
                label1.Text = string.Format("Progressing...{0}% ", report.percentcomplete);
                progressBar1.Value = report.percentcomplete;
                progressBar1.Update();
            };

            await ProcessData(list, progress);
            label1.Text = "BIENVENUE !!!";
            
            f2.Show();
            Visible = false;

        }




        private void ProgressBar_1_Load(object sender, EventArgs e)
        {
            progression();
            Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
    }

   
    }
