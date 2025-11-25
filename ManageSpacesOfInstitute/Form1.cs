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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Запретить ручной ввод во всех ComboBox-ах
            fpl_chbuild.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chnumberroom.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_cheq.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            fpl_chwidth.DropDownStyle = ComboBoxStyle.DropDownList;

            await LoadBuildingsComboBoxAsync();      // загружаем корпусы в комбобокс
            await LoadEquipmentComboBoxAsync();      // загружаем оборудование в комбобокс
            await LoadBuildingsAsync();              // загружаем данные таблицы
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

                string roomNumber = getVal("ROOMNUMBER")?.ToString() ?? string.Empty;
                int buildingId = getVal("BUILDINGID") is int bi ? bi : (getVal("BUILDINGID") != null ? Convert.ToInt32(getVal("BUILDINGID")) : 0);
                string buildingName = getVal("BUILDINGNAME")?.ToString() ?? string.Empty;
                string roomType = getVal("ROOMTYPE")?.ToString() ?? string.Empty;
                string buildingType = getVal("BUILDINGTYPE")?.ToString() ?? string.Empty;
                string equipmentList = getVal("EQUIPMENTLIST")?.ToString() ?? string.Empty;

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
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DataSource = display;
                dataGridView1.ClearSelection();
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

                // Отключаем событие перед привязкой данных
                fpl_chbuild.SelectedIndexChanged -= fpl_chbuild_SelectedIndexChanged;

                fpl_chbuild.DataSource = dt;
                fpl_chbuild.DisplayMember = "NAME";

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

                // Отключаем событие перед привязкой данных
                fpl_cheq.SelectedIndexChanged -= fpl_cheq_SelectedIndexChanged;

                fpl_cheq.DataSource = dt;
                fpl_cheq.DisplayMember = "NAME";

                // Подключаем событие обратно
                fpl_cheq.SelectedIndexChanged += fpl_cheq_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке оборудования:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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