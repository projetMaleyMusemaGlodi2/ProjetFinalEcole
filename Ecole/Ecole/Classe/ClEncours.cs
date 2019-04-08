using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole.Classe
{
    class ClEncours
    {

        int codeEncours;
        int RefAnnee;
        string Refperiode;
        string Refclasse;

        public int CodeEncours
        {
            get
            {
                return codeEncours;
            }

            set
            {
                codeEncours = value;
            }
        }

        public int RefAnnee1
        {
            get
            {
                return RefAnnee;
            }

            set
            {
                RefAnnee = value;
            }
        }

        public string Refperiode1
        {
            get
            {
                return Refperiode;
            }

            set
            {
                Refperiode = value;
            }
        }

        public string Refclasse1
        {
            get
            {
                return Refclasse;
            }

            set
            {
                Refclasse = value;
            }
        }
    }
}
