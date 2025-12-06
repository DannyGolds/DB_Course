using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;



namespace ManageSpacesOfInstitute
{
    public partial class MainFrame : Form
    {
        private DataTable _originalDataTable;
        private bool isAuthorizedAdmin = true;
        private DataGridViewRow row;
        private int prevAction;

        private struct LastState
        {
            public string buildingName;
            public string buildingType;
            public string buildingAdress;
        };
        private LastState lastState;

        public MainFrame()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            gridview_foundroomsinfo.ReadOnly = true;
            UpdateTabsAsync();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Загружаем списки из таблиц
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_BUILDINGS", fpl_chbuild);
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_EQUIPMENT_LIST", fpl_cheq);
            await LoadDataToComboBoxFromTableAsync("TYPE", "GET_BUILDINGS", fpl_chtypebuild);
            await LoadDataToComboBoxFromTableAsync("TYPE", "ROOMS", fpl_chtyperoom);
            await LoadDataToComboBoxFromTableAsync("PURPOSE", "ROOMS", fpl_chpurproom);

            // Загружаем основные данные из процедуры
            await LoadDataToTableAsync(Shared.Partial.info, Shared.Partial.proc, Shared.Partial.to_hide, gridview_foundroomsinfo, Shared.Partial.naming);

            // Подписка на изменения фильтров
            fpl_chbuild.SelectedIndexChanged += (s, _) => filterData();
            fpl_cheq.SelectedIndexChanged += (s, _) => filterData();
            fpl_chtypebuild.SelectedIndexChanged += (s, _) => filterData();
            fpl_chtyperoom.SelectedIndexChanged += (s, _) => filterData();
            fpl_chpurproom.SelectedIndexChanged += (s, _) => filterData();
        }

        private async Task UpdateTabsAsync()
        {
            if (isAuthorizedAdmin)
            {
                if (!tabs.TabPages.Contains(page_edit))
                {
                    tabs.TabPages.Add(page_edit);
                }
                await LoadDataToEditPageAsync();
            }
            else
            {
                tabs.TabPages.Remove(page_edit);
            }
        }

        async Task LoadDataToEditPageAsync()
        {
            await LoadDataToComboBoxFromTableAsync("TYPE", "GET_BUILDING_TYPES", comboBox1);
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_BUILDINGS", comboBox2);
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_BUILDINGS", comboBox3);
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_EQUIPMENT_LIST", comboBox7);
            await LoadDataToComboBoxFromTableAsync("TYPE", "ROOMS", comboBox4);
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_RESPONSIBLES", comboBox6);
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_CHAIRS", comboBox5);
            await LoadDataToTableAsync(Shared.Responsible.info, Shared.Responsible.proc, Shared.Responsible.to_hide, dataGridView4, Shared.Responsible.naming);
            await LoadDataToTableAsync(Shared.Equipment.info, Shared.Equipment.proc, Shared.Equipment.to_hide, dataGridView3, Shared.Equipment.naming);
            await LoadDataToTableAsync(Shared.RoomFull.info, Shared.RoomFull.proc, Shared.RoomFull.to_hide, dataGridView2, Shared.RoomFull.naming);
            await LoadDataToTableAsync(Shared.Buildings.info, Shared.Buildings.proc, Shared.Buildings.to_hide, dataGridView1, Shared.Buildings.naming);
            await LoadDataToTableAsync(Shared.Chairs.info, Shared.Chairs.proc, Shared.Chairs.to_hide, dataGridView5, Shared.Chairs.naming);
            await LoadDataToTableAsync(Shared.Faculties.info, Shared.Faculties.proc, Shared.Faculties.to_hide, dataGridView6, Shared.Faculties.naming);
            comboBox2.SelectedIndexChanged += (s, _) => filterEditRoom();
            comboBox7.SelectedIndexChanged += (s, _) => filterEditEquipment();
        }

        private async Task LoadDataToComboBoxFromTableAsync(string columnName, string tableName, System.Windows.Forms.ComboBox comboBox)
        {
            try
            {
                await using var db = new DBOperations();
                string sql = $"SELECT DISTINCT {columnName} FROM {tableName} ORDER BY {columnName}";
                var dt = await db.GetDataTableAsync(sql);

                var list = new List<string> { "Не выбрано" };
                list.AddRange(dt.AsEnumerable()
                    .Select(r => r[columnName]?.ToString() ?? string.Empty)
                    .Where(s => !string.IsNullOrEmpty(s)));

                comboBox.DataSource = list;
                comboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Ошибка при загрузке данных из таблицы {tableName}:\r\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task LoadDataToTableAsync(List<string> col_list, string proc_name, List<string> unvisible_cols, DataGridView grid, List<string> cols_naming)
        {
            try
            {
                await using var db = new DBOperations();

                var dt = await db.CallProcedureAsync(proc_name, col_list);
                for (var index = 0; index < col_list.Count; index++)  // Или index < dt.Columns.Count
                {
                    dt.Columns[index].ColumnName = cols_naming[index];
                }

                _originalDataTable = dt.Copy();

                var dv = new DataView(_originalDataTable);
                grid.DataSource = dv;

                for (var index = 0; index < unvisible_cols.Count; index++)
                {
                    if (grid.Columns.Contains($"{unvisible_cols[index]}"))
                        grid.Columns[$"{unvisible_cols[index]}"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Ошибка при загрузке данных:\r\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void filterData()
        {
            bool anythingSelected =
                fpl_chbuild.SelectedIndex > 0 ||
                fpl_cheq.SelectedIndex > 0 ||
                fpl_chtypebuild.SelectedIndex > 0 ||
                fpl_chtyperoom.SelectedIndex > 0 ||
                fpl_chpurproom.SelectedIndex > 0;

            var dv = gridview_foundroomsinfo.DataSource as DataView;
            if (dv == null) return;

            if (!anythingSelected)
            {
                dv.RowFilter = string.Empty;
                return;
            }

            var filters = new System.Text.StringBuilder();

            if (fpl_chbuild.SelectedIndex > 0)
                filters.Append($"[Корпус] = '{fpl_chbuild.SelectedItem.ToString().Replace("'", "''")}' AND ");

            if (fpl_cheq.SelectedIndex > 0)
                filters.Append($"[Оборудование] LIKE '%{fpl_cheq.SelectedItem.ToString().Replace("'", "''")}%' AND ");

            if (fpl_chtypebuild.SelectedIndex > 0)
                filters.Append($"[Тип корпуса] = '{fpl_chtypebuild.SelectedItem.ToString().Replace("'", "''")}' AND ");

            if (fpl_chtyperoom.SelectedIndex > 0)
                filters.Append($"[Тип аудитории] = '{fpl_chtyperoom.SelectedItem.ToString().Replace("'", "''")}' AND ");
            if (fpl_chpurproom.SelectedIndex > 0)
                filters.Append($"[Назначение аудитории] = '{fpl_chpurproom.SelectedItem.ToString().Replace("'", "''")}' AND ");
            string filter = filters.ToString();
            if (filter.EndsWith(" AND "))
                filter = filter[..^5]; // удаляем последние 5 символов (" AND ")

            dv.RowFilter = filter;
            gridview_foundroomsinfo.Refresh();
        }

        private void filterEditRoom()
        {
            bool anythingSelected =
                comboBox2.SelectedIndex > 0;

            var dv = dataGridView2.DataSource as DataView;
            if (dv == null) return;

            if (!anythingSelected)
            {
                dv.RowFilter = string.Empty;
                return;
            }

            var filters = new System.Text.StringBuilder();

            if (comboBox2.SelectedIndex > 0)
                filters.Append($"[Корпус] = '{comboBox2.SelectedItem.ToString().Replace("'", "''")}' AND ");
            string filter = filters.ToString();
            if (filter.EndsWith(" AND "))
                filter = filter[..^5]; // удаляем последние 5 символов (" AND ")

            dv.RowFilter = filter;
            dataGridView2.Refresh();

        }

        private void filterEditEquipment()
        {
            bool anythingSelected =
                comboBox7.SelectedIndex > 0;

            var dv = dataGridView3.DataSource as DataView;
            if (dv == null) return;

            if (!anythingSelected)
            {
                dv.RowFilter = string.Empty;
                return;
            }

            var filters = new System.Text.StringBuilder();

            if (comboBox7.SelectedIndex > 0)
                filters.Append($"[Название оборудования] = '{comboBox7.SelectedItem.ToString().Replace("'", "''")}' AND ");
            string filter = filters.ToString();
            if (filter.EndsWith(" AND "))
                filter = filter[..^5]; // удаляем последние 5 символов (" AND ")

            dv.RowFilter = filter;
            dataGridView3.Refresh();

        }


        ///______________________________________________________________________________
        private async Task ExecuteEditAsync(FbParameter[] param, string proc)
        {
            await using var db = new DBOperations();
            await db.ExecProcedureAsync(proc, param);
            await LoadDataToEditPageAsync();
            clearBuildingsItems();
        }

        private static byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;

            using var ms = new MemoryStream();
            using var bmp = new Bitmap(image);               // ← это спасает от всех ошибок GDI+
            bmp.Save(ms, ImageFormat.Jpeg);                    // или Jpeg — как хочешь
            return ms.ToArray();
        }

        // ======================
        // Обработчики событий (оставлены без изменений)
        // ======================

        private void tabPage1_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void fpl_chwidth_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void fpl_chbuild_SelectedIndexChanged(object sender, EventArgs e) { }
        private void fpl_chnumberroom_SelectedIndexChanged(object sender, EventArgs e) { }
        private void fpl_cheq_SelectedIndexChanged(object sender, EventArgs e) { }
        private void btn_applyfilter_Click(object sender, EventArgs e) { }
        private void page_struct_Click(object sender, EventArgs e) { }
        ///______________________________________________________________________________
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnChFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Выберите изображение";
                ofd.Filter = "JPEG файлы (*.jpg;*.jpeg)|*.jpg;*.jpeg|Все файлы (*.*)|*.*";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Загружаем выбранное изображение в PictureBox
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                }
            }

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void btn_auth_Click(object sender, EventArgs e)
        {
            using var authForm = new Auth();
            if (authForm.ShowDialog() == DialogResult.OK)
            {
                label_username.Text = authForm.loggedUser;
                if (authForm.accessLevel == "ADMIN")
                {
                    isAuthorizedAdmin = true;
                    UpdateTabsAsync();
                }
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }
        ///__________________________________editBuildingsPage____________________________________________
        private async void button1_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            EnableBuildingsItems();
            clearBuildingsItems();
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            EnableBuildingsItems();
            lastState.buildingName = textBox1.Text;
            lastState.buildingType = comboBox1.SelectedItem.ToString();
            lastState.buildingAdress = textBox2.Text;
        }


        private void EnableBuildingsItems()
        {
            textBox1.Enabled = true;
            comboBox1.Enabled = true;
            textBox2.Enabled = true;
            btnChFile.Enabled = true;
            button4.Enabled = true;
        }

        private void DisableBuildingsItems()
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            textBox2.Enabled = false;
            btnChFile.Enabled = false;
            button4.Enabled = false;
        }

        private void clearBuildingsItems()
        {
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            textBox2.Text = "";
            pictureBox1.Image = null;
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            if (prevAction == 1)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите добавить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (textBox1.Text.Length == 0 || comboBox1.SelectedIndex == 0 || textBox2.Text.Length == 0)
                    {
                        MessageBox.Show("Поля не должны быть пустыми, а в списке должен быть выбран элемент.");
                        return;
                    }
                    await ExecuteEditAsync(new[]
            {
    new FbParameter("NAME",   FbDbType.VarChar) { Value = textBox1.Text ?? (object)DBNull.Value },
    new FbParameter("TYPE",   FbDbType.VarChar) { Value = comboBox1.SelectedItem?.ToString() ?? (object)DBNull.Value },
    new FbParameter("ADRESS", FbDbType.VarChar) { Value = textBox2.Text ?? (object)DBNull.Value },
    new FbParameter("IMG", FbDbType.Binary) {Value = ImageToByteArray(pictureBox1.Image) ?? (object)DBNull.Value }}, "INSERT_TO_BUILDINGS");

                }
            }
            else if (prevAction == 2)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите удалить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if (dataGridView1.CurrentRow == null)
                    {
                        MessageBox.Show("Нет выбранной строки для удаления.");
                        return;
                    }

                    var row = dataGridView1.CurrentRow;

                    if (!dataGridView1.Columns.Contains("BUILDINGID"))
                    {
                        MessageBox.Show("Колонка BUILDINGID не найдена.");
                        return;
                    }

                    var idValue = row.Cells["BUILDINGID"].Value;
                    if (idValue == null)
                    {
                        MessageBox.Show("У выбранной строки нет значения BUILDINGID.");
                        return;
                    }

                    await ExecuteEditAsync(new[]
                    {
            new FbParameter("ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
        }, "DELETE_BUILDING");
                }
            }
            else if (prevAction == 5)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите обновить таблицу?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if ((textBox1.Text == lastState.buildingName && comboBox1.SelectedItem.ToString() == lastState.buildingType && textBox2.Text == lastState.buildingAdress) || (comboBox2.SelectedIndex == 0))
                    {
                        MessageBox.Show("Измените хотябы одно поле для применения изменений. Тип корпуса также должен быть определен.");
                        return;
                    }
                    var imgBytes = ImageToByteArray(pictureBox1.Image);

                    await ExecuteEditAsync(new[]
            {
        new FbParameter("ID",     FbDbType.Integer) { Value = Convert.ToInt32(row.Cells["BUILDINGID"].Value) },
        new FbParameter("NAME",   FbDbType.VarChar) { Value = textBox1.Text ?? (object)DBNull.Value },
        new FbParameter("TYPE",   FbDbType.VarChar) { Value = comboBox1.SelectedItem?.ToString() ?? (object)DBNull.Value },
        new FbParameter("ADRESS", FbDbType.VarChar) { Value = textBox2.Text ?? (object)DBNull.Value },
           new FbParameter
{
    ParameterName = "IMG",           // точно так же как в процедуре (регистр важен!)
    FbDbType = FbDbType.Binary,      // явно!
    Value = (object)imgBytes ?? DBNull.Value
}
                }, "UPDATE_BUILDINGS");
                }
            }

            DisableBuildingsItems();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            prevAction = 2;
            button4_Click(sender, e);
            clearBuildingsItems();
        }

        private async void button3_Click(object sender, EventArgs e)
        {

        }

        private void roomsEditGrid_OnSelect(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;

            button18.Enabled = true;
            var row = dataGridView2.CurrentRow;

            textBox3.Text = row.Cells["Номер аудитории"].Value?.ToString() ?? "";
            textBox4.Text = row.Cells["Длина"].Value?.ToString() ?? "";
            textBox5.Text = row.Cells["Ширина"].Value?.ToString() ?? "";
        }
        ///____________________________mainPage__________________________________________________
        private void dtg1_cdblclk(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = gridview_foundroomsinfo.Rows[e.RowIndex];
            var roomId = row.Cells["ROOM_ID"].Value;

            if (roomId == null || roomId == DBNull.Value)
            {
                MessageBox.Show("Не удалось определить кабинет.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var detailsForm = new RoomDetails(Convert.ToInt32(roomId));
            detailsForm.ShowDialog(this);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        ///______________________________________________________________________________
        private void dtgv3_onselch(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            button17.Enabled = true;
            var row = dataGridView1.CurrentRow;

            textBox1.Text = row.Cells["Корпус"].Value?.ToString() ?? "";
            comboBox1.SelectedItem = row.Cells["Тип корпуса"].Value?.ToString() ?? "";
            textBox2.Text = row.Cells["Адрес"].Value?.ToString() ?? "";

            var imageData = row.Cells["IMAGE"].Value;
            if (imageData != null)
                Shared.LoadImageFromBlob(pictureBox1, imageData);
            else
                pictureBox1.Image = null;
        }
        

    }
}