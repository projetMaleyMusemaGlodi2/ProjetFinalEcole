using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole.Classe
{
    class ClsAdresse
    {
        string Code;
        string designation;
        int reference;

        public string Code1
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

        public string Designation
        {
            get
            {
                return designation;
            }

            set
            {
                designation = value;
            }
        }

        public int Reference
        {
            get
            {
                return reference;
            }

            set
            {
                reference = value;
            }
        }
    }
}
