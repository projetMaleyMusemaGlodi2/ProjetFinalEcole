using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecole.Classe
{
    class ClsParametre
    {
        string code;
        string designation;
        string refCode;

        public string Code
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

        public string RefCode
        {
            get
            {
                return refCode;
            }

            set
            {
                refCode = value;
            }
        }
    }
}
