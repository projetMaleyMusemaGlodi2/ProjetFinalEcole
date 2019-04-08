using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole.Classe
{
    class clsBase
    {
        string NomTable;
        string NomChamp;
        int Code;
        public string NomTable1
        {
            get
            {
                return NomTable;
            }

            set
            {
                NomTable = value;
            }
        }

        public string NomChamp1
        {
            get
            {
                return NomChamp;
            }

            set
            {
                NomChamp = value;
            }
        }

        public int Code1
        {
            get
            {
                return Code;
            }

            set
            {
                Code = value;
            }
        }
    }
}
