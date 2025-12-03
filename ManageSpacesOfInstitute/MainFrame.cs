using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManageSpacesOfInstitute
{
    public partial class MainFrame : Form
    {
        /// <summary>
        /// Сохраняем исходную таблицу для фильтрации
        /// </summary>
        private DataTable _originalDataTable;
        bool isAuthorizedAdmin = true;

        public MainFrame()
        {
            InitializeComponent();
            gridview_foundroomsinfo.CellDoubleClick += dataGridView1_CellContentClick_1;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            gridview_foundroomsinfo.ReadOnly = true;
            UpdateTabs();
        }

        public struct FilterCriteria
        {
            public string BuildingName;
            public string Equipment;
            public string BuildingType;
            public string RoomType;
            public string RoomNumber;
            public string RoomWidth;
            public string RoomLength;
        }
        private void UpdateTabs()
        {
            if (isAuthorizedAdmin)
            {
                if (!tabs.TabPages.Contains(page_edit))
                {
                    tabs.TabPages.Add(page_edit);
                }
            }
            else
            {
                tabs.TabPages.Remove(page_edit);
            }
            this.Refresh();  // Перерисовка для верности
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Загружаем списки из таблиц (не процедур!)
            await LoadDataToComboBoxFromTableAsync("NAME", "BUILDINGS", fpl_chbuild);
            await LoadDataToComboBoxFromTableAsync("NAME", "EQUIPMENT", fpl_cheq);
            await LoadDataToComboBoxFromTableAsync("TYPE", "BUILDINGS", fpl_chtypebuild);
            await LoadDataToComboBoxFromTableAsync("TYPE", "ROOMS", fpl_chtyperoom);
            await LoadDataToComboBoxFromTableAsync("PURPOSE", "ROOMS", fpl_chpurproom);

            // Загружаем основные данные из процедуры
            await LoadDataToTableAsync();

            // Подписка на изменения фильтров
            fpl_chbuild.SelectedIndexChanged += (s, _) => filterData();
            fpl_cheq.SelectedIndexChanged += (s, _) => filterData();
            fpl_chtypebuild.SelectedIndexChanged += (s, _) => filterData();
            fpl_chtyperoom.SelectedIndexChanged += (s, _) => filterData();
            fpl_chpurproom.SelectedIndexChanged += (s, _) => filterData();
        }

        private async Task LoadDataToComboBoxFromTableAsync(string columnName, string tableName, ComboBox comboBox)
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

        private async Task LoadDataToTableAsync()
        {
            try
            {
                await using var db = new DBOperations();
                var col_list = new List<string>
                {
                    "ROOM_ID",
                    "ROOMNUMBER",
                    "BUILDINGNAME",
                    "ROOMTYPE",
                    "BUILDINGTYPE",
                    "EQUIPMENTLIST",
                    "ROOMPURPOSE"
                };

                var dt = await db.CallProcedureAsync("GET_PARTIAL_ROOM_INFO", col_list);

                // Переименовываем столбцы для отображения
                dt.Columns["ROOM_ID"].ColumnName = "ROOM_ID";
                dt.Columns["ROOMNUMBER"].ColumnName = "Номер кабинета";
                dt.Columns["BUILDINGNAME"].ColumnName = "Корпус";
                dt.Columns["ROOMTYPE"].ColumnName = "Тип кабинета";
                dt.Columns["BUILDINGTYPE"].ColumnName = "Тип корпуса";
                dt.Columns["EQUIPMENTLIST"].ColumnName = "Оборудование";
                dt.Columns["ROOMPURPOSE"].ColumnName = "Назначение";

                _originalDataTable = dt.Copy();

                var dv = new DataView(_originalDataTable);
                gridview_foundroomsinfo.DataSource = dv;

                if (gridview_foundroomsinfo.Columns.Contains("ROOM_ID"))
                    gridview_foundroomsinfo.Columns["ROOM_ID"].Visible = false;

                gridview_foundroomsinfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                filters.Append($"[Тип кабинета] = '{fpl_chtyperoom.SelectedItem.ToString().Replace("'", "''")}' AND ");
            if (fpl_chpurproom.SelectedIndex > 0)
                filters.Append($"[Назначение] = '{fpl_chpurproom.SelectedItem.ToString().Replace("'", "''")}' AND ");
            string filter = filters.ToString();
            if (filter.EndsWith(" AND "))
                filter = filter[..^5]; // удаляем последние 5 символов (" AND ")

            dv.RowFilter = filter;
            gridview_foundroomsinfo.Refresh();
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
                    UpdateTabs();
                }
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}