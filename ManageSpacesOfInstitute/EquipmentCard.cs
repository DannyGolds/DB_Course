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
    public partial class EquipmentCard : UserControl
    {
        public EquipmentCard(string equipmentText, string descriptionText)
        {
            InitializeComponent();
            lblEqNm.Text = equipmentText;
            richTxt.Text = descriptionText;
            pcEq.SizeMode = PictureBoxSizeMode.Zoom;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblEqNm_Click(object sender, EventArgs e)
        {

        }
    }
}
