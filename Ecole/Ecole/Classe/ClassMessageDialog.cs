using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecole.Classe
{
    class ClassMessageDialog
    {
        public static ClassMessageDialog objet;
        Boolean test = false;

        public static ClassMessageDialog GetInstance()
        {
            if (objet == null)
                objet = new ClassMessageDialog();
            return objet;
        }

        public Boolean showDialog(String titre, String Message)
        {

            if (DialogResult.Yes == MessageBox.Show(Message, titre, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                test = true;
            return test;
        }


    }
}
