using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole.Classe
{
    class ClsPresence
    {
        int codePres;
        int RefEleve;
        string HeuereArriver;
        string HeuerSortie;
        DateTime DatePresence;
        string Utilisateur;

        public int CodePres
        {
            get
            {
                return codePres;
            }

            set
            {
                codePres = value;
            }
        }

        public int RefEleve1
        {
            get
            {
                return RefEleve;
            }

            set
            {
                RefEleve = value;
            }
        }

        public string HeuereArriver1
        {
            get
            {
                return HeuereArriver;
            }

            set
            {
                HeuereArriver = value;
            }
        }

        public string HeuerSortie1
        {
            get
            {
                return HeuerSortie;
            }

            set
            {
                HeuerSortie = value;
            }
        }

        public DateTime DatePresence1
        {
            get
            {
                return DatePresence;
            }

            set
            {
                DatePresence = value;
            }
        }

        public string Utilisateur1
        {
            get
            {
                return Utilisateur;
            }

            set
            {
                Utilisateur = value;
            }
        }
    }
}
