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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            flp.Visible = true;
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

        private async Task LoadEquipmentDetailAsync(int _roomId)
        {
            await using var db = new DBOperations();

            var dt = await db.CallProcedureAsync("GET_EQUIPMENT_INFO", new List<string>
    {
        "EQUIPMENTID",
        "EQUIPMENTNAME",
        "EQUIPMENTIMAGE",
        "EQUIPMDESCRIPTION"
    }, new FbParameter("ROOMID", _roomId));

            // Очистите flp перед добавлением, чтобы избежать дубликатов при повторных вызовах
            flp.Controls.Clear();
            if (dt.Rows.Count == 0)
            {
                var testCard = new EquipmentCard("Нет оборудования", "Нет описания");
                flp.Controls.Add(testCard);
            }
            // Добавьте карточки из БД
            foreach (DataRow row in dt.Rows)
            {
                var eqName = row["EQUIPMENTNAME"].ToString();
                var eqDesc = row["EQUIPMDESCRIPTION"].ToString();
                var eqImage = row["EQUIPMENTIMAGE"];  // BLOB-данные

                var card = new EquipmentCard(eqName, eqDesc);

                // Если нужно загрузить изображение (используйте ваш метод LoadImageFromBlob)
                // Предполагаю, что в EquipmentCard есть PictureBox pcEq
                LoadImageFromBlob(card.pcEq, eqImage);  // Добавьте доступ к pcEq, если нужно (сделайте public или property)

                flp.Controls.Add(card);
            }

            // Если dt пустая, добавьте тестовую или сообщение
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
                "DepName",
                "ROOMPURPOSE"
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
                dataGridView1.Rows.Add();
                decimal width = Convert.ToDecimal(row["WIDTH"] ?? 0);
                decimal length = Convert.ToDecimal(row["LENGTH"] ?? 0);
                dataGridView1.Rows[0].Cells[0].Value = $"{row["ROOMNUMBER"]}";
                dataGridView1.Rows[0].Cells[1].Value = $"{row["ROOMTYPE"]}";
                dataGridView1.Rows[0].Cells[2].Value = $"{row["ROOMPURPOSE"]}";
                dataGridView1.Rows[0].Cells[3].Value = $"{width * length:F2} м²";
                dataGridView1.Rows[0].Cells[4].Value = $"{row["LENGTH"]}м x {row["WIDTH"]}м";
                dataGridView1.Rows[0].Cells[5].Value = $"{row["DepName"]}";
                dataGridView1.Rows[0].Cells[6].Value = $"{row["BUILDINGNAME"]}";
                dataGridView1.Rows[0].Cells[7].Value = $"{row["BUILDINGTYPE"]}";
                dataGridView1.Rows[0].Cells[8].Value = $"{row["BUILDINGADRESS"]}";


                Text = $"Информация о кабинете {row["ROOMNUMBER"]} ({row["BUILDINGNAME"]})";
                LoadImageFromBlob(BuildingImage, row["BUILDINGIMAGE"]);
                _ = LoadEquipmentDetailAsync(_roomId);
            }
            catch (Exception ex)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() => MessageBox.Show(this, "Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                }
                else
                {
                    MessageBox.Show(this, "Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void flp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDep_Click(object sender, EventArgs e)
        {

        }
    }
}