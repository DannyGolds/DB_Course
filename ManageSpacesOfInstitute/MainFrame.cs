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
        private struct LastEditBuildingState
        {
            public string buildingName;
            public int buildingTypeID;
            public string buildingAdress;
        };
        private struct lastEditRoomState
        {
            public string room;
            public int buildingid;
            public int roomTypeid;
            public int chairid;
            public int respid;
            public string purpose;
            public decimal width;
            public decimal length;
        };
        private LastEditBuildingState lastBuildingState;
        private lastEditRoomState lastRoomState;

        public MainFrame()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            gridview_foundroomsinfo.ReadOnly = true;
            using var _ = UpdateTabsAsync();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Загружаем списки из таблиц
            await LoadDataToComboBoxFromTableAsync("BUILDINGID", "NAME", "GET_BUILDINGS", fpl_chbuild);
            await LoadDataToComboBoxFromTableAsync("EQUIPMENTID", "NAME", "GET_EQUIPMENT_LIST", fpl_cheq);
            await LoadDataToComboBoxFromTableAsync("BUILDINGID", "TYPE", "GET_BUILDINGS", fpl_chtypebuild);
            await LoadDataToComboBoxFromTableAsync("TYPEID", "TYPE", "ROOM_TYPES", fpl_chtyperoom);
            await LoadDataToComboBoxFromTableAsync("ROOMID", "PURPOSE", "ROOMS", fpl_chpurproom);

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
            await LoadDataToComboBoxFromTableAsync("ID", "TYPE", "GET_BUILDING_TYPES", comboBox1);
            await LoadDataToComboBoxFromTableAsync("BUILDINGID", "NAME", "GET_BUILDINGS", comboBox2);
            await LoadDataToComboBoxFromTableAsync("BUILDINGID", "NAME", "GET_BUILDINGS", comboBox3);
            await LoadDataToComboBoxFromTableAsync("EQUIPMENTID", "NAME", "GET_EQUIPMENT_LIST", comboBox7);
            await LoadDataToComboBoxFromTableAsync("TYPEID", "TYPE", "ROOM_TYPES", comboBox4);
            await LoadDataToComboBoxFromTableAsync("PERSONID", "NAME", "GET_RESPONSIBLES", comboBox6);
            await LoadDataToComboBoxFromTableAsync("ID", "NAME", "GET_CHAIRS", comboBox5);
            await LoadDataToTableAsync(Shared.Responsible.info, Shared.Responsible.proc, Shared.Responsible.to_hide, dataGridView4, Shared.Responsible.naming);
            await LoadDataToTableAsync(Shared.Equipment.info, Shared.Equipment.proc, Shared.Equipment.to_hide, dataGridView3, Shared.Equipment.naming);
            await LoadDataToTableAsync(Shared.RoomFull.info, Shared.RoomFull.proc, Shared.RoomFull.to_hide, dataGridView2, Shared.RoomFull.naming);
            await LoadDataToTableAsync(Shared.Buildings.info, Shared.Buildings.proc, Shared.Buildings.to_hide, dataGridView1, Shared.Buildings.naming);
            await LoadDataToTableAsync(Shared.Chairs.info, Shared.Chairs.proc, Shared.Chairs.to_hide, dataGridView5, Shared.Chairs.naming);
            await LoadDataToTableAsync(Shared.Faculties.info, Shared.Faculties.proc, Shared.Faculties.to_hide, dataGridView6, Shared.Faculties.naming);
            comboBox2.SelectedIndexChanged += (s, _) => filterEditRoom();
            comboBox7.SelectedIndexChanged += (s, _) => filterEditEquipment();
        }

        private async Task LoadDataToComboBoxFromTableAsync(
    string idColumnName,
    string displayColumnName,
    string tableName,
    System.Windows.Forms.ComboBox comboBox,
    bool useMinusOneForNone = false) // если true — вставляем -1 вместо DBNull
        {
            try
            {
                await using var db = new DBOperations();
                string sql = $"SELECT {idColumnName}, {displayColumnName} FROM {tableName} ORDER BY {displayColumnName}";
                var dt = await db.GetDataTableAsync(sql);

                // Проверки: таблица и колонки
                if (dt == null)
                    dt = new DataTable();

                if (!dt.Columns.Contains(idColumnName) || !dt.Columns.Contains(displayColumnName))
                {
                    // Попробуем создать нужные колонки, если их нет
                    if (!dt.Columns.Contains(idColumnName))
                        dt.Columns.Add(idColumnName, typeof(int)); // предполагаем int, при необходимости поменяй тип
                    if (!dt.Columns.Contains(displayColumnName))
                        dt.Columns.Add(displayColumnName, typeof(string));
                }

                // Сделаем колонку id допускающей DBNull, чтобы можно было вставить "Не выбрано"
                var idCol = dt.Columns[idColumnName];
                if (idCol != null && !idCol.AllowDBNull)
                    idCol.AllowDBNull = true;

                // Если используем -1 как "Не выбрано", убедимся, что тип колонки это числовой
                if (useMinusOneForNone)
                {
                    if (idCol.DataType != typeof(int) && idCol.DataType != typeof(long) && idCol.DataType != typeof(short))
                    {
                        // если тип не числовой, переключаем на DBNull-режим
                        useMinusOneForNone = false;
                    }
                }

                // Вставляем строку "Не выбрано"
                var newRow = dt.NewRow();
                newRow[displayColumnName] = "Не выбрано";
                if (useMinusOneForNone)
                    newRow[idColumnName] = -1;
                else
                    newRow[idColumnName] = DBNull.Value;

                dt.Rows.InsertAt(newRow, 0);

                comboBox.DataSource = dt;
                comboBox.DisplayMember = displayColumnName;
                comboBox.ValueMember = idColumnName;
                comboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Ошибка при загрузке данных из таблицы {tableName}:\r\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                filters.Append($"[Корпус] = '{fpl_chbuild.Text.Replace("'", "''")}' AND ");

            if (fpl_cheq.SelectedIndex > 0)
                filters.Append($"[Оборудование] LIKE '%{fpl_cheq.Text.Replace("'", "''")}%' AND ");

            if (fpl_chtypebuild.SelectedIndex > 0)
                filters.Append($"[Тип корпуса] = '{fpl_chtypebuild.Text.Replace("'", "''")}' AND ");

            if (fpl_chtyperoom.SelectedIndex > 0)
                filters.Append($"[Тип аудитории] = '{fpl_chtyperoom.Text.Replace("'", "''")}' AND ");

            if (fpl_chpurproom.SelectedIndex > 0)
                filters.Append($"[Назначение аудитории] = '{fpl_chpurproom.Text.Replace("'", "''")}' AND ");

            string filter = filters.ToString();
            if (filter.EndsWith(" AND "))
                filter = filter[..^5];

            dv.RowFilter = filter;
            gridview_foundroomsinfo.Refresh();
        }

        private void filterEditRoom()
        {
            bool anythingSelected = comboBox2.SelectedIndex > 0;

            var dv = dataGridView2.DataSource as DataView;
            if (dv == null) return;

            if (!anythingSelected)
            {
                dv.RowFilter = string.Empty;
                return;
            }

            var filters = new System.Text.StringBuilder();

            if (comboBox2.SelectedIndex > 0)
                filters.Append($"[Корпус] = '{comboBox2.Text.Replace("'", "''")}' AND ");

            string filter = filters.ToString();
            if (filter.EndsWith(" AND "))
                filter = filter[..^5];

            dv.RowFilter = filter;
            dataGridView2.Refresh();
        }

        private void filterEditEquipment()
        {
            bool anythingSelected = comboBox7.SelectedIndex > 0;

            var dv = dataGridView3.DataSource as DataView;
            if (dv == null) return;

            if (!anythingSelected)
            {
                dv.RowFilter = string.Empty;
                return;
            }

            var filters = new System.Text.StringBuilder();

            if (comboBox7.SelectedIndex > 0)
                filters.Append($"[Название оборудования] = '{comboBox7.Text.Replace("'", "''")}' AND ");

            string filter = filters.ToString();
            if (filter.EndsWith(" AND "))
                filter = filter[..^5];

            dv.RowFilter = filter;
            dataGridView3.Refresh();
        }

        private async Task ExecuteEditAsync(FbParameter[] param, string proc)
        {
            await using var db = new DBOperations();
            await db.ExecProcedureAsync(proc, param);
            await LoadDataToEditPageAsync();
        }

        private static byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;

            using var ms = new MemoryStream();
            using var bmp = new Bitmap(image);               // ← это спасает от всех ошибок GDI+
            bmp.Save(ms, ImageFormat.Jpeg);                    // или Jpeg — как хочешь
            return ms.ToArray();
        }

        //START Обработчики событий. (НЕИСПОЛЬЗУЕМЫЕ)

        private void tabPage1_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
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
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e) { }
        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e) { }
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) { }
        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e) { }
        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e) { }
        private void label20_Click(object sender, EventArgs e) { }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void splitContainer1_Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        //START ОБРАБОТЧИКИ СОБЫТИЙ (MAIN_PAGE)
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







        //START ОБРАБОТЧИКИ СОБЫТИЙ (EDIT -> BUILDINGS)

        private void dtgv3_onselch(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            button17.Enabled = true;
            var row = dataGridView1.CurrentRow;

            // простые текстовые поля
            textBox1.Text = row.Cells["Корпус"].Value?.ToString() ?? "";
            textBox2.Text = row.Cells["Адрес"].Value?.ToString() ?? "";

            // теперь работаем по ID-шнику
            if (row.Cells["TYPEID"].Value != null && row.Cells["TYPEID"].Value != DBNull.Value)
            {
                comboBox1.SelectedValue = row.Cells["TYPEID"].Value;
            }
            else
            {
                comboBox1.SelectedIndex = 0; // "Не выбрано"
            }

            // картинка
            var imageData = row.Cells["IMAGE"].Value;
            if (imageData != null && imageData != DBNull.Value)
                Shared.LoadImageFromBlob(pictureBox1, imageData);
            else
                pictureBox1.Image = null;
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

        //ОБРАБОТЧИК ВЫБОРА КАРТИНКИ
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

        //ОБРАБОТЧИК КНОПКИ ДОБАВЛЕНИЯ ЗАПИСИ
        private async void button1_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            EnableBuildingsItems();
            clearBuildingsItems();
        }

        //ОБРАБОТЧИК КНОПКИ УДАЛЕНИЯ ЗАПИСИ
        private async void button2_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            prevAction = 2;
            button4_Click(sender, e);
            clearBuildingsItems();
        }

        //ОБРАБОТЧИК КНОПКИ СОХРАНЕНИЯ
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
                    //MessageBox.Show($"{comboBox1.SelectedValue} {comboBox1.Text}");
                    await ExecuteEditAsync(new[]
            {
    new FbParameter("NAME",   textBox1.Text.ToString()),
    //new FbParameter("ADRESS", textBox2.Text.ToString()),
    new FbParameter("TYPEID", Convert.ToInt32(comboBox1.SelectedValue.ToString()))
                    }, "INSERT_TO_BUILDINGS");
                    clearBuildingsItems();
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
                    clearBuildingsItems();
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
                    MessageBox.Show($"{lastBuildingState.buildingName} - {textBox1.Text}, {lastBuildingState.buildingAdress} - {textBox2.Text}, { lastBuildingState.buildingTypeID} - {comboBox1.SelectedValue}");
                    if ((textBox1.Text == lastBuildingState.buildingName && Convert.ToInt32(comboBox1.SelectedValue) == lastBuildingState.buildingTypeID && textBox2.Text == lastBuildingState.buildingAdress) || (comboBox1.SelectedIndex == 0))
                    {
                        MessageBox.Show("Измените хотябы одно поле для применения изменений. Тип корпуса также должен быть определен.");
                        return;
                    }

                    try
                    {
                        await ExecuteEditAsync(new[]
                        {
        new FbParameter("ID", FbDbType.Integer)
        {
            Value = row?.Cells["BUILDINGID"]?.Value ??
                   throw new Exception("Не выбран ID здания")
        },
        new FbParameter("NAME", FbDbType.VarChar)
        {
            Value = string.IsNullOrWhiteSpace(textBox1.Text) ?
                   throw new Exception("Название не может быть пустым") :
                   textBox1.Text.Trim()
        },
        new FbParameter("TYPEID", FbDbType.Integer)
        {
            Value = comboBox1.SelectedValue ??
                   throw new Exception("Не выбран тип здания")
        },
        new FbParameter("ADRESS", FbDbType.VarChar)
        {
            Value = string.IsNullOrWhiteSpace(textBox2.Text) ?
                   DBNull.Value :
                   textBox2.Text.Trim()
        }
    }, "UPDATE_BUILDINGS");

                        clearBuildingsItems();
                        MessageBox.Show("Данные успешно обновлены!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                    clearBuildingsItems();
                }
            }

            DisableBuildingsItems();
            Form1_Load(sender, e);
        }


        //ОБРАБОТЧИК КНОПКИ РЕДАКТИРОВАНИЯ ЗАПИСИ
        private void button17_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            EnableBuildingsItems();
            lastBuildingState.buildingName = textBox1.Text;
            lastBuildingState.buildingTypeID = Convert.ToInt32(comboBox1.SelectedValue);
            lastBuildingState.buildingAdress = textBox2.Text;
        }

        private async void button3_Click(object sender, EventArgs e)
        {

        }










        //START ОБРАБОТЧИКИ (EDIT -> ROOOMS)
        private void roomsEditGrid_OnSelect(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;

            button18.Enabled = true;
            var row = dataGridView2.CurrentRow;

            textBox3.Text = row.Cells["Номер аудитории"].Value?.ToString() ?? "";
            numericUpDown2.Value = Convert.ToDecimal(row.Cells["Длина"].Value.ToString());
            numericUpDown1.Value = Convert.ToDecimal(row.Cells["Ширина"].Value.ToString());
            comboBox3.Text = row.Cells["Корпус"].Value?.ToString();
            textBox13.Text = row.Cells["Назначение аудитории"].Value?.ToString();
            comboBox4.Text = row.Cells["Тип аудитории"].Value?.ToString();
            comboBox6.Text = row.Cells["Ответственный"].Value?.ToString();
            comboBox5.Text = row.Cells["Кафедра"].Value?.ToString();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            EnableRoomsItems();
            lastRoomState.room = textBox3.Text;
            lastRoomState.buildingid = Convert.ToInt32(comboBox3.SelectedValue);
            lastRoomState.width = numericUpDown1.Value;
            lastRoomState.length = numericUpDown2.Value;
            lastRoomState.purpose = textBox13.Text;
            lastRoomState.respid = Convert.ToInt32(comboBox6.SelectedValue);
            lastRoomState.chairid = Convert.ToInt32(comboBox5.SelectedValue);
            lastRoomState.roomTypeid = Convert.ToInt32(comboBox4.SelectedValue);
        }

        private void EnableRoomsItems()
        {
            textBox3.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            comboBox5.Enabled = true;
            comboBox6.Enabled = true;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            textBox13.Enabled = true;
            btnRep.Enabled = true;
        }

        private void DisableRoomsItems()
        {
            textBox3.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            textBox13.Enabled = false;
            btnRep.Enabled = false;
        }

        private void clearRoomsItems()
        {
            textBox3.Text = "";
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            textBox13.Text = "";
        }

        private async void btnRep_Click(object sender, EventArgs e)
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
                    if (textBox3.Text.Length == 0 || comboBox3.SelectedIndex == 0 || comboBox4.SelectedIndex == 0 || comboBox5.SelectedIndex == 0 || comboBox6.SelectedIndex == 0 || textBox13.Text.Length == 0)
                    {
                        MessageBox.Show("Поля не должны быть пустыми, а в списке должен быть выбран элемент.");
                        return;
                    }
                    MessageBox.Show($"{comboBox3.SelectedValue}, {textBox3.Text}, {numericUpDown1.Value}, {numericUpDown2.Value}, {textBox13.Text}, {comboBox5.SelectedValue}, {comboBox4.SelectedValue}, {comboBox6.SelectedValue}");
                    await ExecuteEditAsync(new[]
            {
    new FbParameter("BID",   Convert.ToInt32(comboBox3.SelectedValue)) ,
    new FbParameter("NUM",   textBox3.Text.ToString()),
    new FbParameter("WD", numericUpDown1.Value),
    new FbParameter("LEN", numericUpDown2.Value),
    new FbParameter("PURP", textBox13.Text),
    new FbParameter("CHID", Convert.ToInt32(comboBox5.SelectedValue)),
    new FbParameter("TYPE_ID", Convert.ToInt32(comboBox4.SelectedValue)),
    new FbParameter("RESPID", Convert.ToInt32(comboBox6.SelectedValue))}, "INSERT_TO_ROOMS");

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
                    if (dataGridView2.CurrentRow == null)
                    {
                        MessageBox.Show("Нет выбранной строки для удаления.");
                        return;
                    }

                    var row = dataGridView2.CurrentRow;

                    if (!dataGridView2.Columns.Contains("ROOM_ID"))
                    {
                        MessageBox.Show("Колонка ROOMID не найдена.");
                        return;
                    }

                    var idValue = row.Cells["ROOM_ID"].Value;
                    if (idValue == null)
                    {
                        MessageBox.Show("У выбранной строки нет значения ROOMID.");
                        return;
                    }

                    await ExecuteEditAsync(new[]
                    {
                        new FbParameter("ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
                    }, "DELETE_ROOM");
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
                    if ((textBox3.Text == lastRoomState.room && Convert.ToInt32(comboBox3.SelectedValue) == lastRoomState.buildingid && Convert.ToInt32(comboBox4.SelectedValue) == lastRoomState.roomTypeid && Convert.ToInt32(comboBox5.SelectedValue) == lastRoomState.chairid && textBox13.Text == lastRoomState.purpose && numericUpDown1.Value == lastRoomState.width && numericUpDown2.Value == lastRoomState.length && Convert.ToInt32(comboBox6.SelectedValue) == lastRoomState.respid) || (comboBox3.SelectedIndex == 0 || comboBox4.SelectedIndex == 0 || comboBox5.SelectedIndex == 0 || comboBox6.SelectedIndex == 0))
                    {
                        MessageBox.Show("Измените хотябы одно поле для применения изменений. Тип корпуса также должен быть определен.");
                        return;
                    }

                    await ExecuteEditAsync(new[]
            {
                    new FbParameter("P_ROOOMID",     FbDbType.Integer) { Value = (object)Convert.ToInt32(row.Cells["ROOMID"].Value) ?? (object)DBNull.Value},
                    new FbParameter("P_BID",   FbDbType.Integer) { Value =  (object)Convert.ToInt32(comboBox3.SelectedValue) ?? (object)DBNull.Value},
                    new FbParameter("P_NUM",   FbDbType.VarChar) { Value = (object)textBox3.Text ?? DBNull.Value},
                    new FbParameter("P_WD", FbDbType.Decimal) { Value = (object)numericUpDown1.Value ?? DBNull.Value},
                    new FbParameter("P_LNGTH", FbDbType.Decimal) { Value = (object)numericUpDown2.Value ?? DBNull.Value},
                    new FbParameter("P_PURP", FbDbType.VarChar) { Value = (object)textBox13.Text ?? DBNull.Value},
                    new FbParameter("P_RESPID", FbDbType.Integer) { Value = comboBox6.SelectedValue ?? DBNull.Value},
                    new FbParameter("P_CHID", FbDbType.Integer) { Value = comboBox3.SelectedValue ?? DBNull.Value},
                    new FbParameter("P_TYPE_ID", FbDbType.Integer) { Value = comboBox6.SelectedValue ?? DBNull.Value},
                    }, "UPDATE_ROOMS");
                }
            }
            Form1_Load(sender, e);
            DisableRoomsItems();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            EnableRoomsItems();
            clearRoomsItems();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            prevAction = 2;
            btnRep_Click(sender, e);
            clearRoomsItems();
        }

        private void splitContainer1_Panel2_Paint_2(object sender, PaintEventArgs e)
        {

        }
        //END
    }
}