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

        private void LoadImageFromBlob(PictureBox pictureBox, object blobData)
        {
            // Скрываем PictureBox, если данных нет
            pictureBox.Visible = false;

            if (blobData == null || blobData == DBNull.Value)
                return;

            try
            {
                byte[] imageData = (byte[])blobData;

                // Проверка: не пустой массив?
                if (imageData.Length == 0)
                    return;

                using (var ms = new MemoryStream(imageData))
                {
                    pictureBox.Image = Image.FromStream(ms);
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // или AutoSize, StretchImage
                    pictureBox.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки (опционально)
                // Console.WriteLine($"Ошибка загрузки изображения: {ex.Message}");
                pictureBox.Visible = false;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblEqNm_Click(object sender, EventArgs e)
        {

        }
    }
}
