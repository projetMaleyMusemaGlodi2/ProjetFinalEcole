using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Ecole.Formulaire
{
    public partial class FormSplash : SplashScreen
    {
        public FormSplash()
        {
            InitializeComponent();
        }

        private int cpteur = 0;

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                cpteur++;
                lblCpteur.Text = cpteur.ToString();
                if (cpteur == 100)
                {
                    this.Hide();
                    timer1.Enabled = false;
                    Ecole1 frb = new Ecole1();
                    frb.Show();

                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
