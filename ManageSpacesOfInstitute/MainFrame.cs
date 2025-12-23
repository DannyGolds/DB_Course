using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Networking.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace ManageSpacesOfInstitute
{
    public partial class MainFrame : Form
    {
        private DataTable _originalDataTable;
        private string AuthorizedType = "";
        private int prevAction;
        private struct LastEditBuildingState
        {
            public string buildingName;
            public int buildingTypeID;
            public string buildingAdress;
            public byte[] binaryImg;
        }
        private struct lastEditChState
        {
            public string chairname;
            public int faculty;
        }
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
        private struct lastEditEquipmentState
        {
            public int categoryId;
            public string naming;
            public string serial;
            public int roomId;
            public string description;
            public byte[] imageBinary;
        };
        private struct lastEditRState
        {
            public string name;
            public string position;
            public string phone;
        };
        private LastEditBuildingState lastBuildingState;
        private lastEditRoomState lastRoomState;
        private lastEditEquipmentState lastEquipmentState;
        private lastEditRState lastRState;
        private lastEditChState lastChState;
        private string lastFacState;
        private string lastBTState;





        //ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ.
        public MainFrame()
        {
            InitializeComponent();
            this.MinimumSize = new Size(893, 520);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            gridview_foundroomsinfo.ReadOnly = true;
            //using var _ = UpdateTabsAsync();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            // Загружаем списки из таблиц
            await LoadDataToComboBoxFromTableAsync("BUILDINGID", "NAME", "GET_BUILDINGS", fpl_chbuild);
            await LoadDataToComboBoxFromTableAsync("EQUIPMENTID", "NAME", "GET_EQUIPMENT_LIST", fpl_cheq);
            await LoadDataToComboBoxFromTableAsync("ID", "TYPE", "GET_BUILDING_TYPES", fpl_chtypebuild);
            await LoadDataToComboBoxFromTableAsync("TYPEID", "TYPE", "ROOM_TYPES", fpl_chtyperoom);

            // Загружаем основные данные из процедуры
            await LoadDataToTableAsync(Shared.Partial.info, Shared.Partial.proc, Shared.Partial.to_hide, gridview_foundroomsinfo, Shared.Partial.naming);

            // Подписка на изменения фильтров
            fpl_chbuild.SelectedIndexChanged += (s, _) => filterData();
            fpl_cheq.SelectedIndexChanged += (s, _) => filterData();
            fpl_chtypebuild.SelectedIndexChanged += (s, _) => filterData();
            fpl_chtyperoom.SelectedIndexChanged += (s, _) => filterData();
            await UpdateTabsAsync();
        }
        private List<TabPage> allowedPages = new List<TabPage>();

        private void ConfigureSubTabs(string role)
        {
            allowedPages.Clear();

            switch (role)
            {
                case "ADMIN":
                    allowedPages.AddRange(new[] { tabPage1, tabPage3, tabPage4, tabPage5, tabChairs, tabFacult, tabPage6, tabPage7 });
                    break;

                case "DIRECTOR":
                    tabControl1.SelectedIndex = 4;
                    allowedPages.AddRange(new[] { tabChairs, tabFacult });
                    break;

                case "SUPMANAG":
                    allowedPages.AddRange(new[] { tabPage1, tabPage3, tabPage4 });
                    break;
            }
        }
        private async Task UpdateTabsAsync()
        {
            bool canAccessEditPage = AuthorizedType == "ADMIN" ||
                                     AuthorizedType == "DIRECTOR" ||
                                     AuthorizedType == "SUPMANAG";

            if (canAccessEditPage)
            {
                if (!tabs.TabPages.Contains(page_edit))
                {
                    tabs.TabPages.Add(page_edit);
                }

                try
                {
                    ConfigureSubTabs(AuthorizedType);
                    await LoadDataToEditPageAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Данные не загружены, но доступ открыт: {ex.Message}");
                }
            }
            else
            {
                tabs.TabPages.Remove(page_edit);
            }
        }
        async Task LoadDataToEditPageAsync()
        {
            await LoadDataToComboBoxFromTableAsync("ID", "TYPE", "GET_BUILDING_TYPES", cmbEdBuildType);
            await LoadDataToComboBoxFromTableAsync("BUILDINGID", "NAME", "GET_BUILDINGS", comboBox2);
            await LoadDataToComboBoxFromTableAsync("BUILDINGID", "NAME", "GET_BUILDINGS", cmbEdRoomBuild);
            await LoadDataToComboBoxFromTableAsync("ROOMID", "DISPLAYSTRING", "GET_ROOM_DISPLAY_INFO", cmbEdEqRoom);
            await LoadDataToComboBoxFromTableAsync("TYPEID", "TYPE", "ROOM_TYPES", cmbEdRoomType);
            await LoadDataToComboBoxFromTableAsync("PERSONID", "NAME", "GET_RESPONSIBLES", cmbEdRoomResp);
            await LoadDataToComboBoxFromTableAsync("ID", "NAME", "GET_CHAIRS", cmbEdRoomChair);
            await LoadDataToComboBoxFromTableAsync("ID", "NAME", "GET_FACULTIES", cmbEdChFac);
            await LoadDataToTableAsync(Shared.Responsible.info, Shared.Responsible.proc, Shared.Responsible.to_hide, dataGridView4, Shared.Responsible.naming);
            await LoadDataToTableAsync(Shared.Equipment.info, Shared.Equipment.proc, Shared.Equipment.to_hide, dataGridView3, Shared.Equipment.naming);
            await LoadDataToTableAsync(Shared.RoomFull.info, Shared.RoomFull.proc, Shared.RoomFull.to_hide, dataGridView2, Shared.RoomFull.naming);
            await LoadDataToTableAsync(Shared.Buildings.info, Shared.Buildings.proc, Shared.Buildings.to_hide, dataGridView1, Shared.Buildings.naming);
            await LoadDataToTableAsync(Shared.Chairs.info, Shared.Chairs.proc, Shared.Chairs.to_hide, dataGridView5, Shared.Chairs.naming);
            await LoadDataToTableAsync(Shared.Faculties.info, Shared.Faculties.proc, Shared.Faculties.to_hide, dataGridView6, Shared.Faculties.naming);
            await LoadDataToTableAsync(Shared.BuildingTypes.info, Shared.BuildingTypes.proc, Shared.BuildingTypes.to_hide, dataGridView7, Shared.BuildingTypes.naming);
            await LoadDataToTableAsync(Shared.User.info, Shared.User.proc, Shared.User.to_hide, dataGridView8, Shared.User.naming);
            comboBox2.SelectedIndexChanged += (s, _) => filterEditRoom();
        }
        private async Task LoadDataToComboBoxFromTableAsync(string idColumnName, string displayColumnName, string tableName, System.Windows.Forms.ComboBox comboBox, bool useMinusOneForNone = false)
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
                fpl_chtyperoom.SelectedIndex > 0;

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
        bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^8[0-9]{10}$");
        }
        private async Task ExecuteEditAsync(FbParameter[] param, string proc)
        {
            await using var db = new DBOperations();
            await db.ExecProcedureAsync(proc, param);
        }
        private static byte[] ImageToBlob(Image image)
        {
            if (image == null) return null;

            // Клонируем изображение в новый Bitmap — это спасает от GDI+ ошибок
            using var bmp = new Bitmap(image);
            using var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);  // PNG — лучший выбор для качества
            return ms.ToArray();
        }


        //START ОБРАБОТЧИКИ СОБЫТИЙ (MAIN_PAGE)
        private async void btn_auth_Click(object sender, EventArgs e)
        {
            // Проверяем, авторизован ли пользователь (если AuthorizedType пустой — значит не залогинен)
            if (string.IsNullOrEmpty(AuthorizedType))
            {
                using var authForm = new Auth();
                if (authForm.ShowDialog() == DialogResult.OK)
                {
                    label_username.Text = authForm.loggedUser;

                    // Получаем уровень доступа из формы (например: "Admin", "Директор" или "Завхоз")
                    AuthorizedType = authForm.accessLevel;

                    // Обновляем интерфейс
                    await UpdateTabsAsync();

                    btn_auth.Text = "Выйти";
                    Shared.ShowNotify("Уведомление", $"Вы вошли как {AuthorizedType}. Доступ обновлен.");
                }
            }
            else
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите выйти?",
                    "Подтверждение выхода.",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Сбрасываем тип авторизации
                    AuthorizedType = null;
                    label_username.Text = "Гость";

                    // Метод UpdateTabsAsync сам скроет page_edit, так как AuthorizedType теперь null
                    await UpdateTabsAsync();

                    btn_auth.Text = "Авторизоваться";
                    Shared.ShowNotify("Действия с аккаунтом", "Вы успешно вышли из системы!");
                }
            }
        }
        private int GetSafeInt(object value)
        {
            if (value == null || value == DBNull.Value || !int.TryParse(value.ToString(), out int result))
                return -1;
            return result;
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

            var detailsForm = new RoomDetails(Convert.ToInt32(roomId));
            detailsForm.Show();
        }
        private void button27_Click(object sender, EventArgs e)
        {
            fpl_chbuild.SelectedIndex = 0;
            fpl_cheq.SelectedIndex = 0;
            fpl_chtypebuild.SelectedIndex = 0;
            fpl_chtyperoom.SelectedIndex = 0;
        }





        //START ОБРАБОТЧИКИ СОБЫТИЙ (EDIT -> BUILDINGS)

        private void dtgv3_onselch(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            prevAction = 0;
            btnEdBldEd.Enabled = true;
            btnEdBldDel.Enabled = true;
            btnEdBldSv.Enabled = false;
            var row = dataGridView1.CurrentRow;

            txtEdBuildName.Text = row.Cells["Корпус"].Value?.ToString() ?? "";
            txtEdBuildAddress.Text = row.Cells["Адрес"].Value?.ToString() ?? "";

            // Тип корпуса
            if (row.Cells["TYPEID"].Value != null && row.Cells["TYPEID"].Value != DBNull.Value)
                cmbEdBuildType.SelectedValue = row.Cells["TYPEID"].Value;
            else
                cmbEdBuildType.SelectedIndex = 0;

            // Загрузка изображения
            var imageData = row.Cells["IMAGE"].Value;
            if (imageData != null && imageData != DBNull.Value)
                Shared.LoadImageFromBlob(pictureBox1, imageData);
            else
                pictureBox1.Image = null;
            DisableBuildingsItems();
            // СОХРАНЯЕМ БАЙТЫ, А НЕ ССЫЛКУ!
            lastBuildingState.buildingName = txtEdBuildName.Text;
            lastBuildingState.buildingAdress = txtEdBuildAddress.Text;
            lastBuildingState.buildingTypeID = cmbEdBuildType.SelectedIndex > 0
                ? Convert.ToInt32(cmbEdBuildType.SelectedValue)
                : -1;
            lastBuildingState.binaryImg = ImageToBlob(pictureBox1.Image);  // Сохраняем копию в байтах
        }
        private void EnableBuildingsItems()
        {
            txtEdBuildName.Enabled = true;
            cmbEdBuildType.Enabled = true;
            txtEdBuildAddress.Enabled = true;
            btnEdBuildFile.Enabled = true;
        }
        private void DisableBuildingsItems()
        {
            txtEdBuildName.Enabled = false;
            cmbEdBuildType.Enabled = false;
            txtEdBuildAddress.Enabled = false;
            btnEdBuildFile.Enabled = false;
        }
        private void clearBuildingsItems()
        {
            txtEdBuildName.Text = "";
            cmbEdBuildType.SelectedIndex = 0;
            txtEdBuildAddress.Text = "";
            pictureBox1.Image = Properties.Resources.LoadImage;
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
        private void button1_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            EnableBuildingsItems();
            clearBuildingsItems();
            btnEdBldSv.Enabled = true;
            btnEdBldDel.Enabled = false;
            btnEdBldEd.Enabled = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            button4_Click(sender, e);
            clearBuildingsItems();
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
                    if (txtEdBuildName.Text.Length == 0 || cmbEdBuildType.SelectedIndex == 0 || txtEdBuildAddress.Text.Length == 0)
                    {
                        Shared.ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    //MessageBox.Show($"{comboBox1.SelectedValue} {comboBox1.Text}");
                    await ExecuteEditAsync(new[]
{
    new FbParameter("NAME", txtEdBuildName.Text.Trim()),
    new FbParameter("TYPEID", Convert.ToInt32(cmbEdBuildType.SelectedValue)),
    new FbParameter("ADRESS", txtEdBuildAddress.Text.Trim()),
    new FbParameter("IMG",  ImageToBlob(pictureBox1.Image))
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
                        Shared.ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
                        return;
                    }

                    var row = dataGridView1.CurrentRow;
                    var idValue = row.Cells["BUILDINGID"].Value;
                    await ExecuteEditAsync(new[]
                    {
            new FbParameter("ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
        }, "DELETE_BUILDING");
                    clearBuildingsItems();
                }
            }
            else if (prevAction == 5)
            {
                var row = dataGridView1.CurrentRow;
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите обновить таблицу?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    if ((txtEdBuildName.Text == lastBuildingState.buildingName && Convert.ToInt32(cmbEdBuildType.SelectedValue) == lastBuildingState.buildingTypeID && txtEdBuildAddress.Text == lastBuildingState.buildingAdress && lastBuildingState.binaryImg == ImageToBlob(pictureBox1.Image)) || (cmbEdBuildType.SelectedIndex == 0))
                    {
                        Shared.ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                        return;
                    }
                    try
                    {
                        await ExecuteEditAsync(new[]
                        {
        new FbParameter("ID", FbDbType.Integer)
        {
            Value = Convert.ToInt32(row.Cells["BUILDINGID"].Value)
        },
        new FbParameter("NAME", FbDbType.VarChar)
        {
            Value = txtEdBuildName.Text
        },
        new FbParameter("ADRESS", FbDbType.VarChar)
        {
            Value = txtEdBuildAddress.Text.Trim()
        },
                new FbParameter("TYPEID", FbDbType.Integer)
        {
            Value = cmbEdBuildType.SelectedValue
        },
                new FbParameter("IMG", FbDbType.Binary)
                {
                    Value = ImageToBlob(pictureBox1.Image)
                }
    }, "UPDATE_BUILDINGS");

                        clearBuildingsItems();
                        Shared.ShowNotify("Изменение записи", "Информация о корпусах обновлена.");
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
            prevAction = 0;
            await LoadDataToEditPageAsync();
            btnEdBldDel.Enabled = false;
            btnEdBldEd.Enabled = false;
            btnEdBldSv.Enabled = false;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            EnableBuildingsItems();
            btnEdBldDel.Enabled = false;
            btnEdBldSv.Enabled = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }










        //START ОБРАБОТЧИКИ (EDIT -> ROOOMS)
        private void roomsEditGrid_OnSelect(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;
            prevAction = 0;
            btnEdRoomEd.Enabled = true;
            btnEdRoomDel.Enabled = true;
            btnEdRoomRep.Enabled = false;
            var row = dataGridView2.CurrentRow;
            txtEdRoomNum.Text = row.Cells["Номер аудитории"].Value?.ToString() ?? "";
            numericUpDown2.Value = Convert.ToDecimal(row.Cells["Длина"].Value.ToString());
            numericUpDown1.Value = Convert.ToDecimal(row.Cells["Ширина"].Value.ToString());
            cmbEdRoomBuild.Text = row.Cells["Корпус"].Value?.ToString();
            txtEdRoomPurpose.Text = row.Cells["Назначение аудитории"].Value?.ToString();
            cmbEdRoomType.Text = row.Cells["Тип аудитории"].Value?.ToString();
            cmbEdRoomResp.Text = row.Cells["Ответственный"].Value?.ToString();
            cmbEdRoomChair.Text = row.Cells["Кафедра"].Value?.ToString();
            lastRoomState.room = txtEdRoomNum.Text;
            lastRoomState.buildingid = Convert.ToInt32(cmbEdRoomBuild.SelectedValue);
            lastRoomState.width = numericUpDown1.Value;
            lastRoomState.length = numericUpDown2.Value;
            lastRoomState.purpose = txtEdRoomPurpose.Text;
            lastRoomState.respid = Convert.ToInt32(cmbEdRoomResp.SelectedValue);
            lastRoomState.chairid = Convert.ToInt32(cmbEdRoomChair.SelectedValue);
            lastRoomState.roomTypeid = Convert.ToInt32(cmbEdRoomType.SelectedValue);
            DisableRoomsItems();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            EnableRoomsItems();
            btnEdRoomDel.Enabled = false;
            btnEdRoomRep.Enabled = true;
        }
        private void EnableRoomsItems()
        {
            txtEdRoomNum.Enabled = true;
            cmbEdRoomBuild.Enabled = true;
            cmbEdRoomType.Enabled = true;
            cmbEdRoomChair.Enabled = true;
            cmbEdRoomResp.Enabled = true;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            txtEdRoomPurpose.Enabled = true;
            btnEdRoomRep.Enabled = true;
        }
        private void DisableRoomsItems()
        {
            txtEdRoomNum.Enabled = false;
            cmbEdRoomBuild.Enabled = false;
            cmbEdRoomType.Enabled = false;
            cmbEdRoomChair.Enabled = false;
            cmbEdRoomResp.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            txtEdRoomPurpose.Enabled = false;
            btnEdRoomRep.Enabled = false;
        }
        private void clearRoomsItems()
        {
            txtEdRoomNum.Text = "";
            cmbEdRoomBuild.SelectedIndex = 0;
            cmbEdRoomType.SelectedIndex = 0;
            cmbEdRoomChair.SelectedIndex = 0;
            cmbEdRoomResp.SelectedIndex = 0;
            numericUpDown1.Value = numericUpDown1.Minimum;
            numericUpDown2.Value = numericUpDown2.Minimum;
            txtEdRoomPurpose.Text = "";
        }
        private async void btnRep_Click(object sender, EventArgs e)
        {
            var row = dataGridView2.CurrentRow;
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
                    if (txtEdRoomNum.Text.Length == 0 || cmbEdRoomBuild.SelectedIndex == 0 || cmbEdRoomType.SelectedIndex == 0 || cmbEdRoomChair.SelectedIndex == 0 || cmbEdRoomResp.SelectedIndex == 0 || txtEdRoomPurpose.Text.Length == 0)
                    {
                        Shared.ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(new[]
            {
    new FbParameter("BID",   Convert.ToInt32(cmbEdRoomBuild.SelectedValue)) ,
    new FbParameter("NUM",   txtEdRoomNum.Text.ToString()),
    new FbParameter("WD", numericUpDown1.Value),
    new FbParameter("LEN", numericUpDown2.Value),
    new FbParameter("PURP", txtEdRoomPurpose.Text),
    new FbParameter("RESPID", Convert.ToInt32(cmbEdRoomResp.SelectedValue)),
    new FbParameter("CHID", Convert.ToInt32(cmbEdRoomChair.SelectedValue)),
    new FbParameter("TYPE_ID", Convert.ToInt32(cmbEdRoomType.SelectedValue)),
    }, "INSERT_TO_ROOMS");

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
                        Shared.ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
                        return;
                    }
                    var idValue = row.Cells["ROOM_ID"].Value;
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
                    "Вы уверены, что хотите обновить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                // Проверка, что ничего не изменилось или не выбраны обязательные поля
                bool nothingChanged =
                    txtEdRoomNum.Text.Trim() == lastRoomState.room.Trim() &&
                    GetSafeInt(cmbEdRoomBuild.SelectedValue) == lastRoomState.buildingid &&
                    GetSafeInt(cmbEdRoomType.SelectedValue) == lastRoomState.roomTypeid &&
                    GetSafeInt(cmbEdRoomChair.SelectedValue) == lastRoomState.chairid &&
                    GetSafeInt(cmbEdRoomResp.SelectedValue) == lastRoomState.respid &&
                    txtEdRoomPurpose.Text.Trim() == lastRoomState.purpose.Trim() &&
                    numericUpDown1.Value == lastRoomState.width &&
                    numericUpDown2.Value == lastRoomState.length;

                if (nothingChanged || cmbEdRoomBuild.SelectedIndex <= 0 || cmbEdRoomType.SelectedIndex <= 0 ||
                    cmbEdRoomChair.SelectedIndex <= 0 || cmbEdRoomResp.SelectedIndex <= 0)
                {
                    Shared.ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(new[]
                {
        new FbParameter("P_ROOOMID", FbDbType.Integer)   { Value = Convert.ToInt32(dataGridView2.CurrentRow.Cells["ROOM_ID"].Value) },
        new FbParameter("P_BID",     FbDbType.Integer)   { Value = Convert.ToInt32(cmbEdRoomBuild.SelectedValue) },  // Корпус
        new FbParameter("P_NUM",     FbDbType.VarChar)   { Value = txtEdRoomNum.Text.Trim() },
        new FbParameter("P_WD",      FbDbType.Decimal)   { Value = numericUpDown1.Value },
        new FbParameter("P_LNGTH",   FbDbType.Decimal)   { Value = numericUpDown2.Value },
        new FbParameter("P_PURP",    FbDbType.VarChar)   { Value = txtEdRoomPurpose.Text.Trim() },
        new FbParameter("P_RESPID",  FbDbType.Integer)   { Value = Convert.ToInt32(cmbEdRoomResp.SelectedValue) },  // Ответственный
        new FbParameter("P_CHID",    FbDbType.Integer)   { Value = Convert.ToInt32(cmbEdRoomChair.SelectedValue) },  // Кафедра — ИСПРАВЛЕНО!
        new FbParameter("P_TYPE_ID", FbDbType.Integer)   { Value = Convert.ToInt32(cmbEdRoomType.SelectedValue) }   // Тип аудитории — ИСПРАВЛЕНО!
    }, "UPDATE_ROOMS");

                Shared.ShowNotify("Изменение записи", "Информация об аудиториях обновлена.");
                DisableRoomsItems();
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            DisableRoomsItems();
            await LoadDataToEditPageAsync();
            btnEdRoomDel.Enabled = false;
            btnEdRoomEd.Enabled = false;
            btnEdRoomRep.Enabled = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            btnEdRoomRep.Enabled = false;
            btnEdRoomDel.Enabled = false;
            EnableRoomsItems();
            clearRoomsItems();
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            btnRep_Click(sender, e);
            clearRoomsItems();
        }
        private void splitContainer1_Panel2_Paint_2(object sender, PaintEventArgs e)
        {

        }







        //START ОБРАБОТЧИКИ (EDIT -> EQUIPMENT)
        private void EnableEqItems()
        {
            txtEdBuildName.Enabled = true;
            txtEdEqName.Enabled = true;
            txtEdEqNum.Enabled = true;
            cmbEdEqRoom.Enabled = true;
            r_txtEdEqDesc.Enabled = true;
            btnEdBuildFile.Enabled = true;
        }
        private void DisableEqItems()
        {
            txtEdBuildName.Enabled = false;
            txtEdEqName.Enabled = false;
            txtEdEqNum.Enabled = false;
            cmbEdEqRoom.Enabled = false;
            r_txtEdEqDesc.Enabled = false;
            btnEdBuildFile.Enabled = false;
        }
        private void clearEqItems()
        {
            txtEdBuildName.Text = "";
            txtEdEqName.Text = "";
            txtEdEqNum.Text = "";
            cmbEdEqRoom.SelectedIndex = 0;
            r_txtEdEqDesc.Text = "";
            pictureBox2.Image = Properties.Resources.LoadImage;
        }
        private void dataGridView3_Select(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow == null) return;
            prevAction = 0;
            btnEdEqEd.Enabled = true;
            btnEdEqDel.Enabled = true;
            btnEdEqSv.Enabled = false;

            var row = dataGridView3.CurrentRow;

            txtEdEqName.Text = row.Cells["Название оборудования"].Value?.ToString() ?? "";
            txtEdEqNum.Text = row.Cells["Серийный номер"].Value?.ToString() ?? "";
            r_txtEdEqDesc.Text = row.Cells["Описание оборудования"].Value?.ToString() ?? "";
            if (row.Cells["ROOMID"].Value != null &&
    int.TryParse(row.Cells["ROOMID"].Value.ToString(), out int roomId))
            {
                cmbEdEqRoom.SelectedValue = roomId;
            }
            else
            {
                cmbEdEqRoom.SelectedIndex = -1; // или оставить пустым
            }

            Shared.LoadImageFromBlob(pictureBox2, row.Cells["IMAGE"].Value);

            lastEquipmentState.roomId = Convert.ToInt32(cmbEdEqRoom.SelectedValue);
            lastEquipmentState.naming = txtEdEqName.Text.Trim();
            lastEquipmentState.serial = txtEdEqNum.Text.Trim();
            lastEquipmentState.description = r_txtEdEqDesc.Text;
            lastEquipmentState.imageBinary = ImageToBlob(pictureBox2.Image);
            DisableEqItems();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            btnEdEqDel.Enabled = false;
            btnEdEqSv.Enabled = true;
            EnableEqItems();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Выберите изображение";
                ofd.Filter = "JPEG файлы (*.jpg;*.jpeg)|*.jpg;*.jpeg|Все файлы (*.*)|*.*";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Загружаем выбранное изображение в PictureBox
                    pictureBox2.Image = Image.FromFile(ofd.FileName);
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            btnEdEqDel.Enabled = false;
            btnEdEqSv.Enabled = true;
            btnEdEqEd.Enabled = false;
            EnableEqItems();
            clearEqItems();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            button8_Click(sender, e);

        }
        private async void button8_Click(object sender, EventArgs e)
        {
            var row = dataGridView3.CurrentRow;
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
                    if (txtEdEqNum.Text.Length == 0 || cmbEdEqRoom.SelectedIndex == 0 || txtEdEqName.Text.Length == 0 || txtEdEqNum.Text.Length == 0 || r_txtEdEqDesc.Text.Length == 0 || pictureBox2.Image is null)
                    {
                        Shared.ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("ROOM_ID", FbDbType.Integer) { Value = Convert.ToInt32(cmbEdEqRoom.SelectedValue)},
    new FbParameter("NAME", FbDbType.VarChar) { Value = txtEdEqName.Text },
    new FbParameter("SERIAL", FbDbType.VarChar) { Value = txtEdEqNum.Text },
    new FbParameter("IMG", FbDbType.Binary) { Value = ImageToBlob(pictureBox2.Image) },
    new FbParameter("NOTES", FbDbType.VarChar) { Value = r_txtEdEqDesc.Text}
}, "INSERT_TO_EQUIPMENT");

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
                    if (dataGridView3.CurrentRow == null)
                    {
                        Shared.ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
                        return;
                    }
                    var idValue = row.Cells["EQUIPMENTID"].Value;
                    await ExecuteEditAsync(new[]
                    {
                        new FbParameter("EQUIPMENTID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
                    }, "DELETE_EQUIPMENT");
                }
            }
            else if (prevAction == 5)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите обновить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                // Проверка, что ничего не изменилось или не выбраны обязательные поля
                bool nothingChanged =
                    txtEdEqName.Text.Trim() == lastEquipmentState.naming.Trim() &&
                    GetSafeInt(cmbEdEqRoom.SelectedValue) == lastEquipmentState.roomId &&
                    txtEdEqNum.Text.Trim() == lastEquipmentState.serial.Trim() &&
                    r_txtEdEqDesc.Text == lastEquipmentState.description;
                Shared.LoadImageFromBlob(pictureBox2, lastEquipmentState.imageBinary);

                if (nothingChanged || cmbEdEqRoom.SelectedIndex <= 0)
                {
                    Shared.ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("EID", FbDbType.Integer) {Value = Convert.ToInt32(row.Cells["EQUIPMENTID"].Value)},
    new FbParameter("ROOM_ID", FbDbType.Integer) { Value = Convert.ToInt32(cmbEdEqRoom.SelectedValue)},
    new FbParameter("NAME", FbDbType.VarChar) { Value = txtEdEqName.Text },
    new FbParameter("SERIAL", FbDbType.VarChar) { Value = txtEdEqNum.Text },
    new FbParameter("IMG", FbDbType.Binary) { Value = ImageToBlob(pictureBox2.Image) },
    new FbParameter("NOTES", FbDbType.VarChar) { Value = r_txtEdEqDesc.Text}
}, "UPDATE_EQUIPMENT");

                Shared.ShowNotify("Изменение записи", "Информация об оборудовании обновлена.");
                DisableRoomsItems();
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearEqItems();
            DisableEqItems();
            await LoadDataToEditPageAsync();
            btnEdEqDel.Enabled = false;
            btnEdEqEd.Enabled = false;
            btnEdEqSv.Enabled = false;
        }





        //START ОБРАБОТЧИКИ (EDIT -> RESPONSIBLES)
        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow == null) return;
            prevAction = 0;
            btnEdRespDel.Enabled = true;
            btnEdRespSv.Enabled = false;
            btnEdRespEd.Enabled = true;
            var row = dataGridView4.CurrentRow;

            txtEdRespFIO.Text = row.Cells["Полное имя"].Value?.ToString() ?? "";
            txtEdRespPos.Text = row.Cells["Должность"].Value?.ToString() ?? "";
            txtEdRespCont.Text = row.Cells["Номер телефона"].Value?.ToString() ?? "";

            lastRState.name = txtEdRespFIO.Text.Trim();
            lastRState.position = txtEdRespPos.Text.Trim();
            lastRState.phone = txtEdRespCont.Text.Trim();
            DisableRItems();
        }
        private void EnableRItems()
        {
            txtEdRespFIO.Enabled = true;
            txtEdRespPos.Enabled = true;
            txtEdRespCont.Enabled = true;

        }
        private void DisableRItems()
        {
            txtEdRespFIO.Enabled = false;
            txtEdRespPos.Enabled = false;
            txtEdRespCont.Enabled = false;
        }
        private void clearRItems()
        {
            txtEdRespFIO.Text = "";
            txtEdRespPos.Text = "";
            txtEdRespCont.Text = "";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            btnEdRespDel.Enabled = false;
            btnEdRespEd.Enabled = false;
            btnEdRespSv.Enabled = true;
            EnableRItems();
            clearRItems();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            button12_Click(sender, e);
        }
        private void button20_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            btnEdRespDel.Enabled = false;
            btnEdRespSv.Enabled = true;
            EnableRItems();
        }
        private async void button12_Click(object sender, EventArgs e)
        {
            var row = dataGridView4.CurrentRow;
            if (prevAction == 1)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите добавить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                string input = txtEdRespCont.Text.Trim();

                if (!IsValidPhone(input))
                {
                    Shared.ShowNotify("Добавление записи", "Введите номр по типу 8xxxxxxxxxx.");
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    if (txtEdRespFIO.Text.Length == 0 || txtEdRespPos.Text.Length == 0 || txtEdRespCont.Text.Length == 0)
                    {
                        Shared.ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new[] {

  new FbParameter("NAME", FbDbType.VarChar) {Value = txtEdRespFIO.Text},
    new FbParameter("JOBPOSITION", FbDbType.VarChar) { Value = txtEdRespPos.Text},
    new FbParameter("PHONE", FbDbType.VarChar) { Value = txtEdRespCont.Text }
}, "ADD_ROOMRESPONSIBLE");
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
                    if (dataGridView4.CurrentRow == null)
                    {
                        Shared.ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
                        return;
                    }
                    var idValue = row.Cells["PERSONID"].Value;
                    await ExecuteEditAsync(new[]
                    {
                        new FbParameter("ROOMRESPONSIBLE", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
                    }, "DELETE_ROOMRESPONSIBLE");
                }
            }
            else if (prevAction == 5)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите обновить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                // Проверка, что ничего не изменилось или не выбраны обязательные поля
                bool nothingChanged =
                    txtEdRespFIO.Text.Trim() == lastRState.name.Trim() &&
                    txtEdRespPos.Text.Trim() == lastRState.position.Trim() &&
                    txtEdRespCont.Text.Trim() == lastRState.phone.Trim();
                string input = txtEdRespCont.Text.Trim();

                if (!IsValidPhone(input))
                {


                    Shared.ShowNotify("Изменение записи", "Введите номер по типу 8xxxxxxxxxx");
                    return;
                }
                if (nothingChanged)
                {
                    Shared.ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(
                       new FbParameter[]
{   new FbParameter("ROOMRESPONSIBLEID", FbDbType.Integer) {Value = Convert.ToInt32(row.Cells["PERSONID"].Value)},
    new FbParameter("NAME", FbDbType.VarChar) {Value = txtEdRespFIO.Text},
    new FbParameter("JOBPOSITION", FbDbType.VarChar) { Value = txtEdRespPos.Text},
    new FbParameter("PHONE", FbDbType.VarChar) { Value = txtEdRespCont.Text }
}, "UPDATE_ROOMRESPONSIBLE");

                Shared.ShowNotify("Изменение записи", "Информация об ответственных обновлена.");
                DisableRItems();
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearRItems();
            DisableRItems();
            await LoadDataToEditPageAsync();
            btnEdRespEd.Enabled = false;
            btnEdRespDel.Enabled = false;
            btnEdRespSv.Enabled = false;
        }





        //START ОБРАБОТЧИКИ (EDIT -> CHAIRS)
        private void EnableChItems()
        {
            txtEdChName.Enabled = true;
            cmbEdChFac.Enabled = true;


        }
        private void DisableChItems()
        {
            txtEdChName.Enabled = false;
            cmbEdChFac.Enabled = false;

        }
        private void clearChItems()
        {
            txtEdChName.Text = "";
            cmbEdChFac.Text = "";
        }
        private async void button16_Click(object sender, EventArgs e)
        {
            var row = dataGridView5.CurrentRow;
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
                    if (txtEdChName.Text.Length == 0 || cmbEdChFac.SelectedIndex == 0)
                    {
                        Shared.ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("NAME", FbDbType.VarChar) {Value = txtEdChName.Text},
    new FbParameter("FACULTYID", FbDbType.Integer) { Value = Convert.ToInt32(cmbEdChFac.SelectedValue)},
}, "ADD_CHAIR");

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
                    if (dataGridView5.CurrentRow == null)
                    {
                        Shared.ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
                        return;
                    }
                    var idValue = row.Cells["ID"].Value;
                    await ExecuteEditAsync(new[]
                    {
                        new FbParameter("CHAIRID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
                    }, "DELETE_CHAIR");
                }
            }
            else if (prevAction == 5)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите обновить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                // Проверка, что ничего не изменилось или не выбраны обязательные поля
                bool nothingChanged =
                    txtEdChName.Text.Trim() == lastChState.chairname.Trim() &&
                    GetSafeInt(cmbEdChFac.SelectedValue) == lastChState.faculty;

                if (nothingChanged || cmbEdChFac.SelectedIndex <= 0)
                {
                    Shared.ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("CHAIRID", FbDbType.Integer) {Value = Convert.ToInt32(row.Cells["ID"].Value)},
    new FbParameter("NAME", FbDbType.VarChar) { Value = txtEdChName.Text},
    new FbParameter("FACULTYID", FbDbType.Integer) { Value = Convert.ToInt32(cmbEdChFac.SelectedValue) },
}, "UPDATE_CHAIR");

                Shared.ShowNotify("Изменение записи", "Информация о кафедрах обновлена.");
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearChItems();
            DisableChItems();
            await LoadDataToEditPageAsync();
            btnEdChEd.Enabled = false;
            btnEdChDel.Enabled = false;
            btnEdChSv.Enabled = false;
        }
        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow == null) return;
            prevAction = 0;
            btnEdChDel.Enabled = true;
            btnEdChSv.Enabled = false;
            btnEdChEd.Enabled = true;

            var row = dataGridView5.CurrentRow;

            txtEdChName.Text = row.Cells["Кафедра"].Value?.ToString() ?? "";
            cmbEdChFac.Text = row.Cells["Факультет"].Value?.ToString() ?? "";

            lastChState.chairname = txtEdChName.Text.Trim();
            lastChState.faculty = cmbEdChFac.SelectedValue != null && cmbEdChFac.SelectedValue != DBNull.Value ? Convert.ToInt32(cmbEdChFac.SelectedValue) : 0;

            DisableChItems();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            btnEdChDel.Enabled = false;
            btnEdChEd.Enabled = false;
            btnEdChSv.Enabled = true;
            EnableChItems();
            clearChItems();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            button16_Click(sender, e);
        }
        private void button21_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            btnEdChDel.Enabled = false;
            btnEdChSv.Enabled = true;
            EnableChItems();
        }
        private void bldCanc(object sender, EventArgs e)
        {
        }





        //START ОБРАБОТЧИКИ (EDIT -> FACULTIES)
        private void EnableFacItems()
        {
            txtEdFacName.Enabled = true;


        }
        private void DisableFacItems()
        {
            txtEdFacName.Enabled = false;

        }
        private void clearFacItems()
        {
            txtEdFacName.Text = "";
        }
        private void button23_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            btnEdFacDel.Enabled = false;
            btnEdFacEd.Enabled = false;
            btnEdFacSv.Enabled = true;
            EnableFacItems();
            clearFacItems();
        }
        private void button24_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            button26_Click(sender, e);
        }
        private async void button26_Click(object sender, EventArgs e)
        {
            var row = dataGridView6.CurrentRow;
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
                    if (txtEdFacName.Text.Length == 0)
                    {
                        Shared.ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("NAME", FbDbType.VarChar) {Value = txtEdFacName.Text}
}, "ADD_FACULTY");

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
                    var idValue = row.Cells["ID"].Value;
                    await ExecuteEditAsync(new[]
                    {
                        new FbParameter("FACULTY_ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
                    }, "DELETE_FACULTY");
                }
            }
            else if (prevAction == 5)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите обновить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                var idValue = row.Cells["ID"].Value;
                if (result != DialogResult.Yes) return;

                // Проверка, что ничего не изменилось или не выбраны обязательные поля
                bool nothingChanged =
                    txtEdFacName.Text.Trim() == lastFacState.Trim();

                if (nothingChanged)
                {
                    Shared.ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(new[]
                   {
                        new FbParameter("FACULTY_ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) },
                        new FbParameter("NAME", FbDbType.VarChar) {Value = txtEdFacName.Text}
                    }, "UPDATE_FACULTY");

                Shared.ShowNotify("Изменение записи", "Информация о факультетах обновлена.");
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearFacItems();
            DisableFacItems();
            await LoadDataToEditPageAsync();
            btnEdFacEd.Enabled = false;
            btnEdFacDel.Enabled = false;
            btnEdFacSv.Enabled = false;
        }
        private void button22_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            btnEdFacDel.Enabled = false;
            btnEdFacSv.Enabled = true;
            EnableFacItems();
        }
        private void dataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView6.CurrentRow == null) return;
            prevAction = 0;
            btnEdFacDel.Enabled = true;
            btnEdFacSv.Enabled = false;
            btnEdFacEd.Enabled = true;
            var row = dataGridView6.CurrentRow;

            txtEdFacName.Text = row.Cells["Факультет"].Value?.ToString() ?? "";

            lastFacState = txtEdFacName.Text.Trim();
            DisableFacItems();
        }







        //START ОБРАБОТЧИКИ (EDIT -> BUILDING_TYPES)
        private void EnableBTItems()
        {
            txtEdBldtypeType.Enabled = true;


        }
        private void DisableBTItems()
        {
            txtEdBldtypeType.Enabled = false;

        }
        private void clearBTItems()
        {
            txtEdBldtypeType.Text = "";
        }
        private void button11_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            btnEdBldtypeDel.Enabled = false;
            btnEdBldtypeEd.Enabled = false;
            btnEdBldtypeSv.Enabled = true;
            EnableBTItems();
            clearBTItems();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            button25_Click(sender, e);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            btnEdBldtypeDel.Enabled = false;
            btnEdBldtypeSv.Enabled = true;
            EnableBTItems();
        }
        private async void button25_Click(object sender, EventArgs e)
        {
            var row = dataGridView7.CurrentRow;
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
                    if (txtEdBldtypeType.Text.Length == 0)
                    {
                        Shared.ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("TYPE", FbDbType.VarChar) {Value = txtEdBldtypeType.Text}
}, "ADD_BUILDING_TYPE");

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
                    if (dataGridView7.CurrentRow == null)
                    {
                        Shared.ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
                        return;
                    }

                    var idValue = row.Cells["ID"].Value;

                    await ExecuteEditAsync(new[]
                    {
                        new FbParameter("ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
                    }, "DELETE_BUILDING_TYPE");
                }
            }
            else if (prevAction == 5)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите обновить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                var idValue = row.Cells["ID"].Value;
                if (result != DialogResult.Yes) return;

                // Проверка, что ничего не изменилось или не выбраны обязательные поля
                bool nothingChanged =
                    txtEdBldtypeType.Text.Trim() == lastBTState.Trim();
                if (nothingChanged)
                {
                    Shared.ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(new[]
                   {
                        new FbParameter("ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) },
                        new FbParameter("TYPE", FbDbType.VarChar) {Value = txtEdBldtypeType.Text}
                    }, "UPDATE_BUILDING_TYPE");

                Shared.ShowNotify("Изменение записи", "Информация о типе корпуса обновлена.");
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearBTItems();
            DisableBTItems();
            await LoadDataToEditPageAsync();
            btnEdBldtypeEd.Enabled = false;
            btnEdBldtypeDel.Enabled = false;
            btnEdBldtypeSv.Enabled = false;
        }
        private void dataGridView7_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView7.CurrentRow == null) return;
            prevAction = 0;
            btnEdBldtypeDel.Enabled = true;
            btnEdBldtypeSv.Enabled = false;
            btnEdBldtypeEd.Enabled = true;
            var row = dataGridView7.CurrentRow;

            txtEdBldtypeType.Text = row.Cells["Тип корпуса"].Value?.ToString() ?? "";

            lastBTState = txtEdBldtypeType.Text.Trim();
            DisableBTItems();
        }







        private void EnableUsrItems()
        {
            txtEdUsrLg.Enabled = true;
            txtEdUsrPswd.Enabled = true;
            txtEdUsrType.Enabled = true;
            chkEdUsrAct.Enabled = true;


        }
        private void DisableUsrItems()
        {
            txtEdUsrLg.Enabled = false;
            txtEdUsrPswd.Enabled = false;
            txtEdUsrType.Enabled = false;
            chkEdUsrAct.Enabled = false;

        }
        private void clearUsrItems()
        {
            txtEdUsrLg.Text = "";
            txtEdUsrPswd.Text = "";
            txtEdUsrType.Text = "";
            chkEdUsrAct.Checked = false;
        }
        private void dataGridView8_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView8.CurrentRow == null) return;
            prevAction = 0;
            btnEdUsrDel.Enabled = true;
            btnEdUsrSv.Enabled = false;
            btnEdUsrEd.Enabled = true;
            var row = dataGridView8.CurrentRow;

            txtEdUsrLg.Text = row.Cells["Логин"].Value?.ToString() ?? "";
            txtEdUsrType.Text = row.Cells["Тип УЗ"].Value?.ToString() ?? "";
            chkEdUsrAct.Checked = (bool)row.Cells["УЗ активна"].Value;

            DisableUsrItems();
        }

        private void btnEdUsrAdd_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            btnEdUsrDel.Enabled = false;
            btnEdUsrEd.Enabled = false;
            btnEdUsrSv.Enabled = true;
            EnableUsrItems();
            clearUsrItems();
        }

        private void btnEdUsrDel_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            btnEdUsrSv_Click(sender, e);
        }

        private async void btnEdUsrSv_Click(object sender, EventArgs e)
        {
            var row = dataGridView8.CurrentRow;
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
                    if (txtEdUsrType.Text.Length == 0 && txtEdUsrLg.Text.Length == 0 && txtEdUsrPswd.Text.Length == 0)
                    {
                        Shared.ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("P_LOGIN", FbDbType.VarChar) {Value = txtEdUsrLg.Text},
new FbParameter("P_PASSWORDHASH", FbDbType.VarChar) {Value = BCrypt.Net.BCrypt.HashPassword( txtEdUsrPswd.Text, workFactor: 12)},
new FbParameter("P_ACCESSLEVEL", FbDbType.VarChar) {Value = txtEdUsrType.Text},
new FbParameter("P_ISACTIVE", FbDbType.Boolean) {Value = chkEdUsrAct.Checked},
}, "ADD_USER");

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
                    if (dataGridView8.CurrentRow == null)
                    {
                        Shared.ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
                        return;
                    }

                    var idValue = row.Cells["USER_ID"].Value;

                    await ExecuteEditAsync(new[]
                    {
                        new FbParameter("USER_ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) }
                    }, "DELETE_USER");
                }
            }
            else if (prevAction == 5)
            {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите обновить запись?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                var idValue = row.Cells["USER_ID"].Value;
                if (result != DialogResult.Yes) return;

                await ExecuteEditAsync(new[]
                   {
                        new FbParameter("USER_ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) },
                        new FbParameter("LOGIN", FbDbType.VarChar) {Value = txtEdUsrLg.Text},
                        new FbParameter("HASHPASS", FbDbType.VarChar) {Value = BCrypt.Net.BCrypt.HashPassword( txtEdUsrPswd.Text, workFactor: 12)},
                        new FbParameter("ACCESSLEVEL", FbDbType.VarChar) {Value =txtEdUsrType.Text},
                        new FbParameter("ISACTIVE", FbDbType.Boolean) {Value = chkEdUsrAct.Checked}
                    }, "UPDATE_USER");

                Shared.ShowNotify("Изменение записи", "Информация о типе корпуса обновлена.");
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearUsrItems();
            DisableUsrItems();
            await LoadDataToEditPageAsync();
            btnEdUsrEd.Enabled = false;
            btnEdUsrDel.Enabled = false;
            btnEdUsrSv.Enabled = false;
        }

        private void btnEdUsrEd_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            btnEdUsrDel.Enabled = false;
            btnEdUsrSv.Enabled = true;
            EnableUsrItems();
        }










        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            prevAction = 0;
            clearBTItems();
            clearBuildingsItems();
            clearChItems();
            clearEqItems();
            clearFacItems();
            clearRItems();
            clearRoomsItems();
            btnEdBldDel.Enabled = false;
            btnEdBldSv.Enabled = false;
            btnEdBldEd.Enabled = false;

            btnEdRoomEd.Enabled = false;
            btnEdRoomDel.Enabled = false;
            btnEdRoomRep.Enabled = false;

            btnEdEqDel.Enabled = false;
            btnEdEqSv.Enabled = false;
            btnEdEqEd.Enabled = false;

            btnEdRespDel.Enabled = false;
            btnEdRespSv.Enabled = false;
            btnEdRespEd.Enabled = false;

            btnEdChDel.Enabled = false;
            btnEdChSv.Enabled = false;
            btnEdChEd.Enabled = false;

            btnEdFacDel.Enabled = false;
            btnEdFacSv.Enabled = false;
            btnEdFacEd.Enabled = false;

            btnEdBldtypeDel.Enabled = false;
            btnEdBldtypeEd.Enabled = false;
            btnEdBldtypeSv.Enabled = false;
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

        private void splitContainer8_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (!allowedPages.Contains(tabControl1.SelectedTab))
            {
                // Насильно возвращаем его на первую разрешенную вкладку
                if (allowedPages.Count > 0)
                {
                    tabControl1.SelectedTab = allowedPages[0];
                    Shared.ShowNotify("Доступ к этому разделу ограничен.", "Доступ");
                }
            }
        }
    }
}