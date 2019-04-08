using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerEcoleSoft.Classes
{
    class connexion
    {
        public string chemin;

        public void connect()
        {

            pubCon.testFile();
            chemin = File.ReadAllText(ClassConstantes.Table.chemin).Trim();

        }
    }
}
