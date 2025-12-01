using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSpacesOfInstitute
{
    public partial class RoomDetails : Form
    {
        private int _roomId;

        public RoomDetails(int roomId)
        {
            InitializeComponent();
            _roomId = roomId;
            // Запуск async без ConfigureAwait(false) — иначе UI не обновится!
            _ = LoadRoomDetailsAsync(); // Используем "fire-and-forget" с подавлением предупреждения
        }

        public class EquipmentData
        {
            public int EquipmentId { get; set; }
            public string Name { get; set; }
            public byte[] ImageData { get; set; }
            public Image Image { get; set; }
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

        private async Task LoadEquipmentDetailAsync (int _roomId)
        {
            await using var db = new DBOperations();

            var dt = await db.CallProcedureAsync("GETEQUIPMENTIMGS", new List<string>
            {
                "EQUIPMENTID",
                "EQUIPMENTNAME",
                "EQUIPMENTIMAGE"
            }, new FbParameter("ROOMID", _roomId));

            foreach (DataRow row in dt.Rows)
            {
                var card = new EquipmentCard();
                card.EquipmentName = $"{row["EQUIPMENTNAME"]}";

                // Если нужно загрузить картинку:
                if (row["EQUIPMENTIMAGE"] is byte[] imageBytes && imageBytes.Length > 0)
                {
                    using var ms = new MemoryStream(imageBytes);
                    card.EquipmentImage = Image.FromStream(ms);
                }

                flpEq.Controls.Add(card);
            }


        }

        private async Task LoadRoomDetailsAsync()
        {
            try
            {
                await using var db = new DBOperations();

                var dt = await db.CallProcedureAsync(
                    "GETROOMFULLINFO",
                    new List<string>
                    {
                "ROOM_ID",
                "ROOMNUMBER",
                "BUILDINGNAME",
                "BUILDINGTYPE",
                "ROOMTYPE",
                "WIDTH",
                "LENGTH",
                "BUILDINGIMAGE",
                "BUILDINGADRESS",
                "DepName"
                    },
                    new FbParameter("ROOM_ID", _roomId)
                );

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Кабинет не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                var row = dt.Rows[0];
                lblRoomNumber.Text = $"{row["ROOMNUMBER"]}";
                lblBuilding.Text = $"{row["BUILDINGNAME"]}";
                lblRoomType.Text = $"{row["ROOMTYPE"]}";
                lblWidth.Text = $"{row["LENGTH"]}м x {row["WIDTH"]}м";
                lblBuildingType.Text = $"{row["BUILDINGTYPE"]}";
                lblDep.Text = $"{row["DepName"]}";
                lblAdress.Text = $"{row["BUILDINGADRESS"]}";

                decimal width = Convert.ToDecimal(row["WIDTH"] ?? 0);
                decimal length = Convert.ToDecimal(row["LENGTH"] ?? 0);
                lblArea.Text = $"{width * length:F2} м²";

                Text = $"Информация о кабинете {row["ROOMNUMBER"]} ({row["BUILDINGNAME"]})";
                LoadImageFromBlob(BuildingImage, row["BUILDINGIMAGE"]);
                _ = LoadEquipmentDetailAsync(_roomId);

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка загрузки данных:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void lblArea_Click(object sender, EventArgs e)
        {

        }

        private void lblRoomNumber_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}