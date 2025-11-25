using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSpacesOfInstitute
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadBuildingsComboBoxAsync();       // загружаем корпусы
            await LoadEquipmentComboBoxAsync();       // загружаем оборудование
            await LoadTypeBuildingComboBoxAsync();    // загружаем типы корпусов
            await LoadTypeRoomComboBoxAsync();        // загружаем типы аудиторий
            await LoadRoomNumberComboBoxAsync();      // загружаем номера аудиторий
            await LoadRoomWidthComboBoxAsync();       // загружаем ширины
            await LoadRoomLengthComboBoxAsync();      // загружаем длины
            await LoadBuildingsAsync();               // загружаем данные таблицы
        }

        // Загружает данные из хранимой процедуры GETROOMFULLINFO(ROOMID)
        private async Task LoadBuildingsAsync()
        {
            try
            {
                // Получаем roomId из UI или тестовое значение
                int roomId = 1;

                await using var db = new DBOperations();

                var parameters = new FbParameter[]
                {
                    new FbParameter("ROOMID", FbDbType.Integer) { Value = roomId, Direction = ParameterDirection.Input },

                    // выходные параметры
                    new FbParameter("ROOMNUMBER", FbDbType.VarChar) { Size = 20, Direction = ParameterDirection.Output },
                    new FbParameter("BUILDINGID", FbDbType.Integer) { Direction = ParameterDirection.Output },
                    new FbParameter("BUILDINGNAME", FbDbType.VarChar) { Size = 100, Direction = ParameterDirection.Output },
                    new FbParameter("ROOMTYPE", FbDbType.VarChar) { Size = 50, Direction = ParameterDirection.Output },
                    new FbParameter("BUILDINGTYPE", FbDbType.VarChar) { Size = 50, Direction = ParameterDirection.Output },
                    new FbParameter("EQUIPMENTLIST", FbDbType.VarChar) { Size = 1000, Direction = ParameterDirection.Output },

                    // BLOB выходные параметры
                    new FbParameter("EQUIPMENTIMAGES", FbDbType.Binary) { Direction = ParameterDirection.Output },
                    new FbParameter("BUILDINGIMAGE", FbDbType.Binary) { Direction = ParameterDirection.Output }
                };

                var (procTable, outputsRaw) = await db.ExecuteProcedureAsync("GETROOMFULLINFO", parameters);

                // Нормализуем ключи выходных параметров для безопасного доступа (без @, верхний регистр)
                var outputs = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
                foreach (var kv in outputsRaw)
                {
                    var key = kv.Key?.TrimStart('@').ToUpperInvariant() ?? string.Empty;
                    outputs[key] = kv.Value;
                }

                // Вспомогательная локальная лямбда для чтения параметра по имени
                object? getVal(string name)
                {
                    outputs.TryGetValue(name.ToUpperInvariant(), out var v);
                    return v;
                }

                string roomNumber = getVal("ROOMNUMBER")?.ToString() ?? "Не указан номер комнаты";
                int buildingId = getVal("BUILDINGID") is int bi ? bi : (getVal("BUILDINGID") != null ? Convert.ToInt32(getVal("BUILDINGID")) : 0);
                string buildingName = getVal("BUILDINGNAME")?.ToString() ?? "Не указано наименование корпуса";
                string roomType = getVal("ROOMTYPE")?.ToString() ?? "Не указан тип аудитории";
                string buildingType = getVal("BUILDINGTYPE")?.ToString() ?? "Не указан тип корпуса";
                string equipmentList = getVal("EQUIPMENTLIST")?.ToString() ?? "Нет оборудования";

                // Собираем таблицу для отображения
                var display = new DataTable();
                display.Columns.Add("Номер аудитории", typeof(string));
                display.Columns.Add("Название корпуса", typeof(string));
                display.Columns.Add("Тип аудитории", typeof(string));
                display.Columns.Add("Тип корпуса", typeof(string));
                display.Columns.Add("Список оборудования", typeof(string));


                if (procTable != null && procTable.Rows.Count > 0)
                {
                    foreach (DataRow row in procTable.Rows)
                    {
                        var rn = procTable.Columns.Contains("ROOMNUMBER") && row["ROOMNUMBER"] != DBNull.Value ? row["ROOMNUMBER"].ToString()! : roomNumber;
                        var bn = procTable.Columns.Contains("BUILDINGNAME") && row["BUILDINGNAME"] != DBNull.Value ? row["BUILDINGNAME"].ToString()! : buildingName;
                        var rt = procTable.Columns.Contains("ROOMTYPE") && row["ROOMTYPE"] != DBNull.Value ? row["ROOMTYPE"].ToString()! : roomType;
                        var bt = procTable.Columns.Contains("BUILDINGTYPE") && row["BUILDINGTYPE"] != DBNull.Value ? row["BUILDINGTYPE"].ToString()! : buildingType;
                        var el = procTable.Columns.Contains("EQUIPMENTLIST") && row["EQUIPMENTLIST"] != DBNull.Value ? row["EQUIPMENTLIST"].ToString()! : equipmentList;

                        display.Rows.Add(rn, bn, rt, bt, el);
                    }
                }
                else
                {
                    display.Rows.Add(roomNumber, buildingName, roomType, buildingType, equipmentList);
                }

                // Настройки DataGridView и привязка
                gridview_foundroomsinfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridview_foundroomsinfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                gridview_foundroomsinfo.AllowUserToAddRows = false;
                gridview_foundroomsinfo.Dock = DockStyle.Fill;
                gridview_foundroomsinfo.ReadOnly = true;
                gridview_foundroomsinfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                gridview_foundroomsinfo.DataSource = display;
                gridview_foundroomsinfo.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при вызове процедуры GETROOMFULLINFO:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadBuildingsComboBoxAsync()
        {
            try
            {
                await using var db = new DBOperations();
                const string sql = "SELECT NAME FROM BUILDINGS ORDER BY NAME";
                var dt = await db.GetDataTableAsync(sql);

                // Добавляем строку по умолчанию в начало
                var row = dt.NewRow();
                row["NAME"] = "Выберите корпус";
                dt.Rows.InsertAt(row, 0);

                // Отключаем событие перед привязкой данных
                fpl_chbuild.SelectedIndexChanged -= fpl_chbuild_SelectedIndexChanged;

                fpl_chbuild.DataSource = dt;
                fpl_chbuild.DisplayMember = "NAME";
                fpl_chbuild.SelectedIndex = 0; // выбираем элемент по умолчанию

                // Подключаем событие обратно
                fpl_chbuild.SelectedIndexChanged += fpl_chbuild_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке корпусов:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadEquipmentComboBoxAsync()
        {
            try
            {
                await using var db = new DBOperations();
                const string sql = "SELECT NAME FROM EQUIPMENT ORDER BY NAME";
                var dt = await db.GetDataTableAsync(sql);

                // Добавляем строку по умолчанию в начало
                var row = dt.NewRow();
                row["NAME"] = "Выберите оборудование";
                dt.Rows.InsertAt(row, 0);

                // Отключаем событие перед привязкой данных
                fpl_cheq.SelectedIndexChanged -= fpl_cheq_SelectedIndexChanged;

                fpl_cheq.DataSource = dt;
                fpl_cheq.DisplayMember = "NAME";
                fpl_cheq.SelectedIndex = 0; // выбираем элемент по умолчанию

                // Подключаем событие обратно
                fpl_cheq.SelectedIndexChanged += fpl_cheq_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке оборудования:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTypeBuildingComboBoxAsync()
        {
            try
            {
                await using var db = new DBOperations();
                const string sql = "SELECT TYPE FROM BUILDINGS ORDER BY NAME";
                var dt = await db.GetDataTableAsync(sql);

                // Добавляем строку по умолчанию в начало
                var row = dt.NewRow();
                row["TYPE"] = "Выберите тип корпуса";
                dt.Rows.InsertAt(row, 0);

                // Отключаем событие перед привязкой данных
                fpl_chtypebuild.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

                fpl_chtypebuild.DataSource = dt;
                fpl_chtypebuild.DisplayMember = "TYPE";
                fpl_chtypebuild.SelectedIndex = 0; // выбираем элемент по умолчанию

                // Подключаем событие обратно
                fpl_chtypebuild.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке оборудования:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTypeRoomComboBoxAsync()
        {
            try
            {
                await using var db = new DBOperations();
                // Замените на реальное имя таблицы/столбца если отличается
                const string sql = "SELECT DISTINCT TYPE FROM ROOMS ORDER BY TYPE";
                var dt = await db.GetDataTableAsync(sql);

                var row = dt.NewRow();
                row["TYPE"] = "Выберите тип аудитории";
                dt.Rows.InsertAt(row, 0);

                fpl_chtyperoom.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
                fpl_chtyperoom.DataSource = dt;
                fpl_chtyperoom.DisplayMember = "TYPE";
                fpl_chtyperoom.SelectedIndex = 0;
                fpl_chtyperoom.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке типов аудиторий:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadRoomNumberComboBoxAsync()
        {
            try
            {
                await using var db = new DBOperations();
                const string sql = "SELECT NUMBER FROM ROOMS ORDER BY NUMBER";
                var dt = await db.GetDataTableAsync(sql);

                // Создаём новую таблицу с увеличенной длиной столбца для placeholder
                var dtWithDefault = new DataTable();
                dtWithDefault.Columns.Add("NUMBER", typeof(string));

                // Добавляем реальные данные
                foreach (DataRow originalRow in dt.Rows)
                {
                    var newRow = dtWithDefault.NewRow();
                    newRow["NUMBER"] = originalRow["NUMBER"];
                    dtWithDefault.Rows.Add(newRow);
                }

                fpl_chnumberroom.SelectedIndexChanged -= fpl_chnumberroom_SelectedIndexChanged;
                fpl_chnumberroom.DataSource = dtWithDefault;
                fpl_chnumberroom.DisplayMember = "NUMBER";
                fpl_chnumberroom.SelectedIndex = 0;
                fpl_chnumberroom.SelectedIndexChanged += fpl_chnumberroom_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке номеров аудиторий:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadRoomWidthComboBoxAsync()
        {
            try
            {
                await using var db = new DBOperations();
                const string sql = "SELECT DISTINCT WIDTH FROM ROOMS ORDER BY WIDTH";
                var dt = await db.GetDataTableAsync(sql);

                // Для опциональных параметров не добавляем placeholder, просто загружаем данные
                fpl_chwidth.SelectedIndexChanged -= fpl_chwidth_SelectedIndexChanged;
                fpl_chwidth.DataSource = dt;
                fpl_chwidth.DisplayMember = "WIDTH";
                // Не выбираем ничего (или выбираем null)
                fpl_chwidth.SelectedIndex = -1;
                fpl_chwidth.SelectedIndexChanged += fpl_chwidth_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке ширин:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadRoomLengthComboBoxAsync()
        {
            try
            {
                await using var db = new DBOperations();
                const string sql = "SELECT DISTINCT LENGTH FROM ROOMS ORDER BY LENGTH";
                var dt = await db.GetDataTableAsync(sql);

                // Для опциональных параметров не добавляем placeholder, просто загружаем данные
                fpl_chlength.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                fpl_chlength.DataSource = dt;
                fpl_chlength.DisplayMember = "LENGTH";
                // Не выбираем ничего (или выбираем null)
                fpl_chlength.SelectedIndex = -1;
                fpl_chlength.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке длин:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверяет, выбран ли элемент в ComboBox (не на placeholder)
        /// </summary>
        private bool IsComboBoxSelected(ComboBox combo, string placeholderText = "---")
        {
            return combo.SelectedIndex > 0 ||
                   (combo.SelectedItem != null && !combo.SelectedItem.ToString()!.Contains(placeholderText));
        }

        /// <summary>
        /// Проверяет все обязательные ComboBox-ы (ширина и длина — опциональны)
        /// </summary>
        private bool ValidateComboBoxes()
        {
            if (!IsComboBoxSelected(fpl_chbuild))
            {
                MessageBox.Show(this, "Пожалуйста, выберите корпус", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsComboBoxSelected(fpl_chnumberroom))
            {
                MessageBox.Show(this, "Пожалуйста, выберите номер аудитории", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsComboBoxSelected(fpl_chtyperoom))
            {
                MessageBox.Show(this, "Пожалуйста, выберите тип аудитории", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Ширина и длина — опциональны, не проверяем
            return true;
        }

        // Остальные обработчики оставлены без изменений
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
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }

        private void tabPage2_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void fpl_chbuild_SelectedIndexChanged(object sender, EventArgs e) {

        }
        private void fpl_chnumberroom_SelectedIndexChanged(object sender, EventArgs e) { }
        private void fpl_cheq_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}