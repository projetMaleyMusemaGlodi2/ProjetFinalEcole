using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Ecole.report
{
    public partial class BulletinPage : DevExpress.XtraReports.UI.XtraReport
    {
        public BulletinPage()
        {
            InitializeComponent();
            //TestePourcent();
        }

        void TestePourcent() {
            if (double.Parse(txtPourcentP1.Text) >= 70)
            {
                txtApplicationP1.Text = "E";
            }
            else {
                txtApplicationP1.Text = "B";
            }
        }

        private void BulletinPage_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //
        }

        private void BulletinPage_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            //TestePourcent();
        }

        private void BulletinPage_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
        {
            //TestePourcent();
        }
    }
}
