using Ecole.Classe;
using Ecole.Formulaire;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole
{
    public class connexion
    {        
               
        public string chemin;

        public void connect()
        {          
           
                pubCon.testFile();
                chemin = File.ReadAllText(ClassConstantes.Table.chemin).Trim();
           
        }


    }
}
