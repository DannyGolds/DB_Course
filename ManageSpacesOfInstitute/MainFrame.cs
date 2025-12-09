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
using System.Text.RegularExpressions;



namespace ManageSpacesOfInstitute
{
    public partial class MainFrame : Form
    {
        private DataTable _originalDataTable;
        private bool isAuthorizedAdmin = false;
        private DataGridViewRow row;
        private int prevAction;
        private struct LastEditBuildingState
        {
            public string buildingName;
            public int buildingTypeID;
            public string buildingAdress;
            public byte[] binaryImg;  // Храним байты, а не Image!
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
            await LoadDataToComboBoxFromTableAsync("ROOMID", "DISPLAYSTRING", "GET_ROOM_DISPLAY_INFO", comboBox10);
            await LoadDataToComboBoxFromTableAsync("TYPEID", "TYPE", "ROOM_TYPES", comboBox4);
            await LoadDataToComboBoxFromTableAsync("PERSONID", "NAME", "GET_RESPONSIBLES", comboBox6);
            await LoadDataToComboBoxFromTableAsync("ID", "NAME", "GET_CHAIRS", comboBox5);
            await LoadDataToComboBoxFromTableAsync("ID", "NAME", "GET_FACULTIES", comboBox9);
            await LoadDataToTableAsync(Shared.Responsible.info, Shared.Responsible.proc, Shared.Responsible.to_hide, dataGridView4, Shared.Responsible.naming);
            await LoadDataToTableAsync(Shared.Equipment.info, Shared.Equipment.proc, Shared.Equipment.to_hide, dataGridView3, Shared.Equipment.naming);
            await LoadDataToTableAsync(Shared.RoomFull.info, Shared.RoomFull.proc, Shared.RoomFull.to_hide, dataGridView2, Shared.RoomFull.naming);
            await LoadDataToTableAsync(Shared.Buildings.info, Shared.Buildings.proc, Shared.Buildings.to_hide, dataGridView1, Shared.Buildings.naming);
            await LoadDataToTableAsync(Shared.Chairs.info, Shared.Chairs.proc, Shared.Chairs.to_hide, dataGridView5, Shared.Chairs.naming);
            await LoadDataToTableAsync(Shared.Faculties.info, Shared.Faculties.proc, Shared.Faculties.to_hide, dataGridView6, Shared.Faculties.naming);
            await LoadDataToTableAsync(Shared.BuildingTypes.info, Shared.BuildingTypes.proc, Shared.BuildingTypes.to_hide, dataGridView7, Shared.BuildingTypes.naming);
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
        bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^8[0-9]{10}$");
        }
        private async Task ExecuteEditAsync(FbParameter[] param, string proc)
        {
            await using var db = new DBOperations();
            await db.ExecProcedureAsync(proc, param);
            await LoadDataToEditPageAsync();
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

        private void ShowNotify(string text, string title)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information; // можно поставить свою иконку
            notifyIcon.Visible = true;

            // Показать уведомление
            notifyIcon.ShowBalloonTip(3000, title, text, ToolTipIcon.Info);

        }



        //START ОБРАБОТЧИКИ СОБЫТИЙ (MAIN_PAGE)
        private void btn_auth_Click(object sender, EventArgs e)
        {
            if (!isAuthorizedAdmin)
            {
                using var authForm = new Auth();
                if (authForm.ShowDialog() == DialogResult.OK)
                {
                    label_username.Text = authForm.loggedUser;
                    if (authForm.accessLevel == "ADMIN")
                    {
                        isAuthorizedAdmin = true;
                        UpdateTabsAsync();
                        btn_auth.Text = "Выйти";
                        ShowNotify("Уведомление", "Вы получили функционал редактирования базы.");
                    }
                }
                return;
            }
            else {
                var result = MessageBox.Show(
                    this,
                    "Вы уверены, что хотите выйти?",
                    "Подтверждение выхода.",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes) {
                    isAuthorizedAdmin = false;
                    UpdateTabsAsync();
                    btn_auth.Text = "Авторизоваться";
                    ShowNotify("Действия с аккаунтом", "Вы успешно вышли из аккаунта администратора!");
                }
                return;
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

            using var detailsForm = new RoomDetails(Convert.ToInt32(roomId));
            detailsForm.ShowDialog(this);
        }







        //START ОБРАБОТЧИКИ СОБЫТИЙ (EDIT -> BUILDINGS)

        private void dtgv3_onselch(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            button17.Enabled = true;
            var row = dataGridView1.CurrentRow;

            textBox1.Text = row.Cells["Корпус"].Value?.ToString() ?? "";
            textBox2.Text = row.Cells["Адрес"].Value?.ToString() ?? "";

            // Тип корпуса
            if (row.Cells["TYPEID"].Value != null && row.Cells["TYPEID"].Value != DBNull.Value)
                comboBox1.SelectedValue = row.Cells["TYPEID"].Value;
            else
                comboBox1.SelectedIndex = 0;

            // Загрузка изображения
            var imageData = row.Cells["IMAGE"].Value;
            if (imageData != null && imageData != DBNull.Value)
                Shared.LoadImageFromBlob(pictureBox1, imageData);
            else
                pictureBox1.Image = null;
            DisableBuildingsItems();
            // СОХРАНЯЕМ БАЙТЫ, А НЕ ССЫЛКУ!
            lastBuildingState.buildingName = textBox1.Text;
            lastBuildingState.buildingAdress = textBox2.Text;
            lastBuildingState.buildingTypeID = comboBox1.SelectedIndex > 0
                ? Convert.ToInt32(comboBox1.SelectedValue)
                : -1;
            lastBuildingState.binaryImg = ImageToBlob(pictureBox1.Image);  // Сохраняем копию в байтах
        }
        private void EnableBuildingsItems()
        {
            textBox1.Enabled = true;
            comboBox1.Enabled = true;
            textBox2.Enabled = true;
            btnChFile.Enabled = true;
        }
        private void DisableBuildingsItems()
        {
            textBox1.Enabled = false;
            comboBox1.Enabled = false;
            textBox2.Enabled = false;
            btnChFile.Enabled = false;
        }
        private void clearBuildingsItems()
        {
            textBox1.Text = "";
            comboBox1.SelectedIndex = 0;
            textBox2.Text = "";
            pictureBox1.Image = null;
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
                    if (textBox1.Text.Length == 0 || comboBox1.SelectedIndex == 0 || textBox2.Text.Length == 0)
                    {
                        ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    //MessageBox.Show($"{comboBox1.SelectedValue} {comboBox1.Text}");
                    await ExecuteEditAsync(new[]
{
    new FbParameter("NAME", textBox1.Text.Trim()),
    new FbParameter("TYPEID", Convert.ToInt32(comboBox1.SelectedValue)),
    new FbParameter("ADRESS", textBox2.Text.Trim()),
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
                        ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
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
                    if ((textBox1.Text == lastBuildingState.buildingName && Convert.ToInt32(comboBox1.SelectedValue) == lastBuildingState.buildingTypeID && textBox2.Text == lastBuildingState.buildingAdress && lastBuildingState.binaryImg == ImageToBlob(pictureBox1.Image)) || (comboBox1.SelectedIndex == 0))
                    {
                        ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
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
            Value = textBox1.Text
        },
        new FbParameter("ADRESS", FbDbType.VarChar)
        {
            Value = textBox2.Text.Trim()
        },
                new FbParameter("TYPEID", FbDbType.Integer)
        {
            Value = comboBox1.SelectedValue
        },
                new FbParameter("IMG", FbDbType.Binary)
                {
                    Value = ImageToBlob(pictureBox1.Image)
                }
    }, "UPDATE_BUILDINGS");

                        clearBuildingsItems();
                        ShowNotify("Изменение записи", "Информация о корпусах обновлена.");
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
            ShowNotify("Сохранение изменений", "Изменения сохранены!");
        }
        private void button17_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            EnableBuildingsItems();
        }
        private void button3_Click(object sender, EventArgs e)
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
            lastRoomState.room = textBox3.Text;
            lastRoomState.buildingid = Convert.ToInt32(comboBox3.SelectedValue);
            lastRoomState.width = numericUpDown1.Value;
            lastRoomState.length = numericUpDown2.Value;
            lastRoomState.purpose = textBox13.Text;
            lastRoomState.respid = Convert.ToInt32(comboBox6.SelectedValue);
            lastRoomState.chairid = Convert.ToInt32(comboBox5.SelectedValue);
            lastRoomState.roomTypeid = Convert.ToInt32(comboBox4.SelectedValue);
            DisableRoomsItems();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            EnableRoomsItems();
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
                    if (textBox3.Text.Length == 0 || comboBox3.SelectedIndex == 0 || comboBox4.SelectedIndex == 0 || comboBox5.SelectedIndex == 0 || comboBox6.SelectedIndex == 0 || textBox13.Text.Length == 0)
                    {
                        ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(new[]
            {
    new FbParameter("BID",   Convert.ToInt32(comboBox3.SelectedValue)) ,
    new FbParameter("NUM",   textBox3.Text.ToString()),
    new FbParameter("WD", numericUpDown1.Value),
    new FbParameter("LEN", numericUpDown2.Value),
    new FbParameter("PURP", textBox13.Text),
    new FbParameter("RESPID", Convert.ToInt32(comboBox6.SelectedValue)),
    new FbParameter("CHID", Convert.ToInt32(comboBox5.SelectedValue)),    
    new FbParameter("TYPE_ID", Convert.ToInt32(comboBox4.SelectedValue)),
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
                        ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
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
                    textBox3.Text.Trim() == lastRoomState.room.Trim() &&
                    GetSafeInt(comboBox3.SelectedValue) == lastRoomState.buildingid &&
                    GetSafeInt(comboBox4.SelectedValue) == lastRoomState.roomTypeid &&
                    GetSafeInt(comboBox5.SelectedValue) == lastRoomState.chairid &&
                    GetSafeInt(comboBox6.SelectedValue) == lastRoomState.respid &&
                    textBox13.Text.Trim() == lastRoomState.purpose.Trim() &&
                    numericUpDown1.Value == lastRoomState.width &&
                    numericUpDown2.Value == lastRoomState.length;

                if (nothingChanged || comboBox3.SelectedIndex <= 0 || comboBox4.SelectedIndex <= 0 ||
                    comboBox5.SelectedIndex <= 0 || comboBox6.SelectedIndex <= 0)
                {
                    ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(new[]
                {
        new FbParameter("P_ROOOMID", FbDbType.Integer)   { Value = Convert.ToInt32(dataGridView2.CurrentRow.Cells["ROOM_ID"].Value) },
        new FbParameter("P_BID",     FbDbType.Integer)   { Value = Convert.ToInt32(comboBox3.SelectedValue) },  // Корпус
        new FbParameter("P_NUM",     FbDbType.VarChar)   { Value = textBox3.Text.Trim() },
        new FbParameter("P_WD",      FbDbType.Decimal)   { Value = numericUpDown1.Value },
        new FbParameter("P_LNGTH",   FbDbType.Decimal)   { Value = numericUpDown2.Value },
        new FbParameter("P_PURP",    FbDbType.VarChar)   { Value = textBox13.Text.Trim() },
        new FbParameter("P_RESPID",  FbDbType.Integer)   { Value = Convert.ToInt32(comboBox6.SelectedValue) },  // Ответственный
        new FbParameter("P_CHID",    FbDbType.Integer)   { Value = Convert.ToInt32(comboBox5.SelectedValue) },  // Кафедра — ИСПРАВЛЕНО!
        new FbParameter("P_TYPE_ID", FbDbType.Integer)   { Value = Convert.ToInt32(comboBox4.SelectedValue) }   // Тип аудитории — ИСПРАВЛЕНО!
    }, "UPDATE_ROOMS");

                ShowNotify("Изменение записи", "Информация об аудиториях обновлена.");
                DisableRoomsItems();
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            DisableRoomsItems();
            ShowNotify("Сохранение изменений", "Изменения сохранены!");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            prevAction = 1;
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


        private void EnableEqItems()
        {
            textBox1.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            comboBox10.Enabled = true;
            richTextBox1.Enabled = true;

        }
        private void DisableEqItems()
        {
            textBox1.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            comboBox10.Enabled = false;
            richTextBox1.Enabled = false;
        }
        private void clearEqItems()
        {
            textBox1.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox10.SelectedIndex = 0;
            richTextBox1.Text = "";
            pictureBox2.Image = null;
        }
        private void dataGridView3_Select(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow == null) return;

            button19.Enabled = true;
            var row = dataGridView3.CurrentRow;

            textBox6.Text = row.Cells["Название оборудования"].Value?.ToString() ?? "";
            textBox7.Text = row.Cells["Серийный номер"].Value?.ToString() ?? "";
            richTextBox1.Text = row.Cells["Описание оборудования"].Value?.ToString() ?? "";
            comboBox10.SelectedValue = row.Cells["ROOMID"].Value.ToString();
            Shared.LoadImageFromBlob(pictureBox2, row.Cells["IMAGE"].Value);

            lastEquipmentState.roomId = Convert.ToInt32(comboBox10.SelectedValue);
            lastEquipmentState.naming = textBox6.Text.Trim();
            lastEquipmentState.serial = textBox7.Text.Trim();
            lastEquipmentState.description = richTextBox1.Text;
            lastEquipmentState.imageBinary = ImageToBlob(pictureBox2.Image);
            DisableEqItems();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            prevAction = 5;
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
                    if (textBox7.Text.Length == 0 || comboBox10.SelectedIndex == 0 || textBox6.Text.Length == 0 || textBox7.Text.Length == 0 || richTextBox1.Text.Length == 0 || pictureBox2.Image is null)
                    {
                        ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("ROOM_ID", FbDbType.Integer) { Value = Convert.ToInt32(comboBox10.SelectedValue)},
    new FbParameter("NAME", FbDbType.VarChar) { Value = textBox6.Text },
    new FbParameter("SERIAL", FbDbType.VarChar) { Value = textBox7.Text },
    new FbParameter("IMG", FbDbType.Binary) { Value = ImageToBlob(pictureBox2.Image) },
    new FbParameter("NOTES", FbDbType.VarChar) { Value = richTextBox1.Text}
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
                        ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
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
                    textBox6.Text.Trim() == lastEquipmentState.naming.Trim() &&
                    GetSafeInt(comboBox10.SelectedValue) == lastEquipmentState.roomId &&
                    textBox7.Text.Trim() == lastEquipmentState.serial.Trim() &&
                    richTextBox1.Text == lastEquipmentState.description;
                Shared.LoadImageFromBlob(pictureBox2, lastEquipmentState.imageBinary);

                if (nothingChanged || comboBox10.SelectedIndex <= 0)
                {
                    ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("EID", FbDbType.Integer) {Value = Convert.ToInt32(row.Cells["EQUIPMENTID"].Value)},
    new FbParameter("ROOM_ID", FbDbType.Integer) { Value = Convert.ToInt32(comboBox10.SelectedValue)},
    new FbParameter("NAME", FbDbType.VarChar) { Value = textBox6.Text },
    new FbParameter("SERIAL", FbDbType.VarChar) { Value = textBox7.Text },
    new FbParameter("IMG", FbDbType.Binary) { Value = ImageToBlob(pictureBox2.Image) },
    new FbParameter("NOTES", FbDbType.VarChar) { Value = richTextBox1.Text}
}, "UPDATE_EQUIPMENT");

                ShowNotify("Изменение записи", "Информация об оборудовании обновлена.");
                DisableRoomsItems();
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearEqItems();
            DisableEqItems();
            ShowNotify("Сохранение изменений", "Изменения сохранены!");
        }






        private void EnableRItems()
        {
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;

        }
        private void DisableRItems()
        {
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
        }
        private void clearRItems()
        {
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            prevAction = 1;
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

                string input = textBox10.Text.Trim();

                if (!IsValidPhone(input))
                {
                    ShowNotify("Добавление записи", "Введите номр по типу 8xxxxxxxxxx.");
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    if (textBox8.Text.Length == 0 || comboBox9.SelectedIndex == 0 || textBox10.Text.Length == 0)
                    {
                        ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new[] {

  new FbParameter("NAME", FbDbType.VarChar) {Value = textBox8.Text},
    new FbParameter("JOBPOSITION", FbDbType.VarChar) { Value = textBox9.Text},
    new FbParameter("PHONE", FbDbType.VarChar) { Value = textBox10.Text }
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
                        ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
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
                    textBox8.Text.Trim() == lastRState.name.Trim() &&
                    textBox9.Text.Trim() == lastRState.position.Trim() &&
                    textBox10.Text.Trim() == lastRState.phone.Trim();
                string input = textBox10.Text.Trim();

                if (!IsValidPhone(input))
                {


                    ShowNotify("Изменение записи", "Введите номер по типу 8xxxxxxxxxx");
                    return;
                }
                if (nothingChanged)
                {
                    ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(
                       new FbParameter[]
{   new FbParameter("ROOMRESPONSIBLEID", FbDbType.Integer) {Value = Convert.ToInt32(row.Cells["PERSONID"].Value)},
    new FbParameter("NAME", FbDbType.VarChar) {Value = textBox8.Text},
    new FbParameter("JOBPOSITION", FbDbType.VarChar) { Value = textBox9.Text},
    new FbParameter("PHONE", FbDbType.VarChar) { Value = textBox10.Text }
}, "UPDATE_ROOMRESPONSIBLE");

                ShowNotify("Изменение записи", "Информация об ответственных обновлена.");
                DisableRItems();
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearRItems();
            DisableRItems();
            ShowNotify("Сохранение изменений", "Изменения сохранены!");
        }
        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow == null) return;

            button19.Enabled = true;
            var row = dataGridView4.CurrentRow;

            textBox8.Text = row.Cells["Полное имя"].Value?.ToString() ?? "";
            textBox9.Text = row.Cells["Должность"].Value?.ToString() ?? "";
            textBox10.Text = row.Cells["Номер телефона"].Value?.ToString() ?? "";

            lastRState.name = textBox8.Text.Trim();
            lastRState.position = textBox9.Text.Trim();
            lastRState.phone = textBox10.Text.Trim();
            DisableRItems();
        }






        private void EnableChItems()
        {
            textBox11.Enabled = true;
            comboBox9.Enabled = true;


        }
        private void DisableChItems()
        {
            textBox11.Enabled = false;
            comboBox9.Enabled = false;

        }
        private void clearChItems()
        {
            textBox11.Text = "";
            comboBox9.Text = "";
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
                    if (textBox11.Text.Length == 0 || comboBox9.SelectedIndex == 0)
                    {
                        ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("NAME", FbDbType.VarChar) {Value = textBox11.Text},
    new FbParameter("FACULTYID", FbDbType.Integer) { Value = Convert.ToInt32(comboBox9.SelectedValue)},
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
                        ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
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
                    textBox11.Text.Trim() == lastChState.chairname.Trim() &&
                    GetSafeInt(comboBox9.SelectedValue) == lastChState.faculty;

                if (nothingChanged || comboBox9.SelectedIndex <= 0)
                {
                    ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("CHAIRID", FbDbType.Integer) {Value = Convert.ToInt32(row.Cells["ID"].Value)},
    new FbParameter("NAME", FbDbType.VarChar) { Value = textBox11.Text},
    new FbParameter("FACULTYID", FbDbType.Integer) { Value = Convert.ToInt32(comboBox9.SelectedValue) },
}, "UPDATE_CHAIR");

                ShowNotify("Изменение записи", "Информация о кафедрах обновлена.");
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearChItems();
            DisableChItems();
            ShowNotify("Сохранение изменений", "Изменения сохранены!");
        }
        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView5.CurrentRow == null) return;

            button20.Enabled = true;
            var row = dataGridView5.CurrentRow;

            textBox11.Text = row.Cells["Кафедра"].Value?.ToString() ?? "";
            comboBox9.Text = row.Cells["Факультет"].Value?.ToString() ?? "";

            lastChState.chairname = textBox11.Text.Trim();
            lastChState.faculty = Convert.ToInt32(comboBox9.SelectedValue);
            DisableChItems();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            prevAction = 1;
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
            EnableChItems();
        }
        private void bldCanc(object sender, EventArgs e)
        {
        }






        private void EnableFacItems()
        {
            textBox12.Enabled = true;


        }
        private void DisableFacItems()
        {
            textBox12.Enabled = false;

        }
        private void clearFacItems()
        {
            textBox12.Text = "";
        }
        private void button23_Click(object sender, EventArgs e)
        {
            prevAction = 1;
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
                    if (textBox12.Text.Length == 0)
                    {
                        ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("NAME", FbDbType.VarChar) {Value = textBox12.Text}
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
                    textBox12.Text.Trim() == lastFacState.Trim();

                if (nothingChanged)
                {
                    ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(new[]
                   {
                        new FbParameter("FACULTY_ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) },
                        new FbParameter("NAME", FbDbType.VarChar) {Value = textBox12.Text}
                    }, "UPDATE_FACULTY");

                ShowNotify("Изменение записи", "Информация о факультетах обновлена.");
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearFacItems();
            DisableFacItems();
            ShowNotify("Сохранение изменений", "Изменения сохранены!");
        }
        private void button22_Click(object sender, EventArgs e)
        {
            prevAction = 5;
            EnableFacItems();
        }
        private void dataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView6.CurrentRow == null) return;

            button22.Enabled = true;
            var row = dataGridView6.CurrentRow;

            textBox12.Text = row.Cells["Факультет"].Value?.ToString() ?? "";

            lastFacState = textBox12.Text.Trim();
            DisableFacItems();
        }








        private void EnableBTItems()
        {
            textBox4.Enabled = true;


        }
        private void DisableBTItems()
        {
            textBox4.Enabled = false;

        }
        private void clearBTItems()
        {
            textBox4.Text = "";
        }
        private void button11_Click(object sender, EventArgs e)
        {
            prevAction = 1;
            EnableBTItems();
            clearBTItems();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            prevAction = 2;
            button26_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            prevAction = 5;
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
                    if (textBox4.Text.Length == 0)
                    {
                        ShowNotify("Добавление записи", "Поля не должны быть пустыми!");
                        return;
                    }
                    await ExecuteEditAsync(
                        new FbParameter[]
{   new FbParameter("TYPE", FbDbType.VarChar) {Value = textBox4.Text}
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
                        ShowNotify("Удаление записи", "Нет выбранной строки для удаления.");
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
                    textBox4.Text.Trim() == lastBTState.Trim();
                if (nothingChanged)
                {
                    ShowNotify("Изменение записи", "Измените хотя бы одно поле!");
                    return;
                }

                // ИСПРАВЛЕННЫЙ вызов процедуры — теперь всё в правильных местах!
                await ExecuteEditAsync(new[]
                   {
                        new FbParameter("ID", FbDbType.Integer) { Value = Convert.ToInt32(idValue) },
                        new FbParameter("TYPE", FbDbType.VarChar) {Value = textBox4.Text}
                    }, "UPDATE_BUILDING_TYPE");

                ShowNotify("Изменение записи", "Информация о типе корпуса обновлена.");
                await LoadDataToEditPageAsync(); // или Form1_Load
            }
            Form1_Load(sender, e);
            clearBTItems();
            DisableBTItems();
            ShowNotify("Сохранение изменений", "Изменения сохранены!");
        }

        private void dataGridView7_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView7.CurrentRow == null) return;

            button25.Enabled = true;
            var row = dataGridView7.CurrentRow;

            textBox4.Text = row.Cells["Тип корпуса"].Value?.ToString() ?? "";

            lastBTState = textBox4.Text.Trim();
            DisableBTItems();
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


    }
}