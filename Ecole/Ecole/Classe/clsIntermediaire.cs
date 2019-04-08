using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole.Classe
{
    class clsIntermediaire
    {
        private static clsIntermediaire i;
        public static clsIntermediaire GetInstance()
        {
            if (i == null)
                i = new clsIntermediaire();
            return i;
        }
        string nomTable, valChamp1, valchamp2;

        public string NomTable
        {
            get
            {
                return nomTable;
            }

            set
            {
                nomTable = value;
            }
        }

        public string ValChamp1
        {
            get
            {
                return valChamp1;
            }

            set
            {
                valChamp1 = value;
            }
        }

        public string Valchamp2
        {
            get
            {
                return valchamp2;
            }

            set
            {
                valchamp2 = value;
            }
        }
    }
}
