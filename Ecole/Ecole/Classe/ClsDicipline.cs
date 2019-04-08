using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole.Classe
{
    class ClsDicipline
    {
        int code;
        int RefEleve;
        string periode;
        string RefMession;
        string userCession;
        

        public int Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
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

        public string Periode
        {
            get
            {
                return periode;
            }

            set
            {
                periode = value;
            }
        }

      
        public string UserCession
        {
            get
            {
                return userCession;
            }

            set
            {
                userCession = value;
            }
        }

        public string RefMession1
        {
            get
            {
                return RefMession;
            }

            set
            {
                RefMession = value;
            }
        }
    }
}
