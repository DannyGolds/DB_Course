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
        private DataTable _originalDataTable;
        private bool isAuthorizedAdmin = false;

        public MainFrame()
        {
            InitializeComponent();
            gridview_foundroomsinfo.CellDoubleClick += dataGridView1_CellContentClick_1;
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
            await LoadDataToComboBoxFromTableAsync("NAME", "BUILDINGS", fpl_chbuild);
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_EQUIPMENT_LIST", fpl_cheq);
            await LoadDataToComboBoxFromTableAsync("TYPE", "BUILDINGS", fpl_chtypebuild);
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
            await LoadDataToComboBoxFromTableAsync("NAME", "BUILDINGS", comboBox2);
            await LoadDataToComboBoxFromTableAsync("NAME", "GET_EQUIPMENT_LIST", comboBox7);
            await LoadDataToTableAsync(Shared.Responsible.info, Shared.Responsible.to_hide, Shared.Responsible.proc, dataGridView4, Shared.Responsible.naming);
            await LoadDataToTableAsync(Shared.Equipment.info, Shared.Equipment.proc, Shared.Equipment.to_hide, dataGridView3, Shared.Equipment.naming);
            await LoadDataToTableAsync(Shared.RoomFull.info, Shared.RoomFull.proc, Shared.RoomFull.to_hide, dataGridView2, Shared.RoomFull.naming);
            await LoadDataToTableAsync(Shared.Buildings.info, Shared.Buildings.proc, Shared.Buildings.to_hide, dataGridView1, Shared.Buildings.naming);
            await LoadDataToTableAsync(Shared.Chairs.info, Shared.Chairs.proc, Shared.Chairs.to_hide, dataGridView5, Shared.Chairs.naming);
            await LoadDataToTableAsync(Shared.Faculties.info, Shared.Faculties.proc, Shared.Faculties.to_hide, dataGridView6, Shared.Faculties.naming);
            comboBox2.SelectedIndexChanged += (s, _) => filterEditRoom();
            comboBox7.SelectedIndexChanged += (s, _) => filterEditEquipment();
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

        private void dataGridView1_CellContentClick_3(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }
    }
}