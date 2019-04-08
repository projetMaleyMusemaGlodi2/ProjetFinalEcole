using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecole.Classe
{
    class clsDatebaseBackupRestor
    {

        private static string backupPath = "";
        public String getBackupPath(RadioButton rb, TextEdit txt)
        {
            if (rb.Checked == true)
            {
                //backupPath = @"C:\BackupEcole";
                backupPath = ClassConstantes.Table.cheminBackup;
                try
                {
                    if (Directory.Exists(backupPath))
                    {
                        return backupPath;
                    }
                    DirectoryInfo di = Directory.CreateDirectory(backupPath);
                    backupPath = di.FullName;
                }
                catch (Exception exc)
                {
                    XtraMessageBox.Show(exc.Message);
                }
            }
            else
            {
                backupPath = txt.Text;
            }
            return backupPath;
        }//Pour le selectionner un Chemin d
    }
}
