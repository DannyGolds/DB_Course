using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSpacesOfInstitute
{
    public partial class Structure : Form
    {
        public Structure(List<string> structure)
        {
            InitializeComponent();
            richTextBox1.Text = $"Аудитория {structure[0]} ->\n  {structure[1]} ->\n {structure[2]}";
        }

        private void Structure_Load(object sender, EventArgs e)
        {

        }
    }
}
