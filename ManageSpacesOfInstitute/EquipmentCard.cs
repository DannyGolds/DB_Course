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
        public EquipmentCard()
        {
            InitializeComponent();
        }
        private Image equipmentImage;

        [Category("Data")]
        [Description("Image shown on the card")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image EquipmentImage
        {
            get => equipmentImage;
            set
            {
                equipmentImage = value;
                pcEq.Image = value;
                pcEq.SizeMode = PictureBoxSizeMode.Zoom;
                pcEq.Visible = value != null;
            }
        }

        // Чтобы дизайнер не пытался сериализовать это свойство
        public bool ShouldSerializeEquipmentImage() => false;
        public void ResetEquipmentImage() => EquipmentImage = null;

        // Остальные свойства (строки/числа) можно оставлять сериализуемыми
        private string equipmentName;
        [Category("Data")]
        [Description("Equipment name text")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EquipmentName
        {
            get => equipmentName;
            set
            {
                equipmentName = value;
                lblEqNm.Text = value;
            }
        }

    }
}
