using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecole.Classe
{
    class ClassNotification
    {

        private static NotifyIcon notifyIcon;

        public void showNotification(String title, String body, String path)
        {

            try
            {
                notifyIcon = new NotifyIcon
                {
                    Visible = true

                };
                if (title != null)
                {
                    notifyIcon.BalloonTipTitle = title;
                }
                if (body != null)
                {
                    notifyIcon.BalloonTipText = body;

                }
                notifyIcon.Icon = new System.Drawing.Icon(path);

                notifyIcon.ShowBalloonTip(500);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void Display(String title, String body, String type)
        {
            switch (type)
            {
                case "Error":
                    MessageBox.Show(body, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "Success":
                    MessageBox.Show(body, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

        }
    }
}
