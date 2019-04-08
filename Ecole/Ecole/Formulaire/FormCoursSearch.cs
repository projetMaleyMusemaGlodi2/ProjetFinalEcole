using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ecole.Classe;

namespace Ecole.Formulaire
{
    public partial class FormCoursSearch : Form
    {
        public FormCoursSearch()
        {
            InitializeComponent();
        }

        private void FormCoursSearch_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = ClIntelligence.GetInstance().chargementGrid1("cours");
            clsIntermediaire.GetInstance().Valchamp2 = "";
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                clsIntermediaire.GetInstance().Valchamp2 = gridView1.GetFocusedRowCellValue(gridView1.Columns["cours"]).ToString();
                this.Close();
            }
            catch {

            }
        }
    }
}
