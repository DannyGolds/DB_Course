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
        /// <summary>
        /// Сохраняем исходную таблицу для фильтрации
        /// </summary>
        private DataTable _originalDataTable;

        public MainFrame()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadDataToComboBoxAsync("NAME", "BUILDINGS", fpl_chbuild);
            await LoadDataToComboBoxAsync("NAME", "EQUIPMENT", fpl_cheq);
            await LoadDataToComboBoxAsync("TYPE", "BUILDINGS", fpl_chtypebuild);
            await LoadDataToComboBoxAsync("TYPE", "ROOMS", fpl_chtyperoom);
            await LoadDataToComboBoxAsync("NUMBER", "ROOMS", fpl_chnumberroom);
            await LoadDataToComboBoxAsync("WIDTH", "ROOMS", fpl_chwidth);
            await LoadDataToComboBoxAsync("LENGTH", "ROOMS", fpl_chlength);
            await LoadDataToTableAsync();
        }

        private async Task LoadDataToComboBoxAsync(string selectable_col, string selectable_table, ComboBox fpl)
        {
            try
            {
                await using var db = new DBOperations();
                string sql = $"SELECT {selectable_col} FROM {selectable_table} ORDER BY {selectable_col}";
                var dt = await db.GetDataTableAsync(sql);

                var list = new List<string> { "Не выбрано" };
                list.AddRange(dt.AsEnumerable().Select(r => r[selectable_col].ToString()));

                fpl.DataSource = list;
                fpl.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке корпусов:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDataToTableAsync()
        {
            try
            {
                await using var db = new DBOperations();
                List<string> col_list = new List<string>
                {
                    "ROOMNUMBER",
                    "BUILDINGNAME",
                    "ROOMTYPE",
                    "BUILDINGTYPE",
                    "EQUIPMENTLIST",
                };

                var dt = await db.CallProcedureAsync("GETROOMFULLINFO", col_list);
                dt.Columns["ROOMNUMBER"].ColumnName = "Номер кабинета";
                dt.Columns["BUILDINGNAME"].ColumnName = "Корпус";
                dt.Columns["ROOMTYPE"].ColumnName = "Тип кабинета";
                dt.Columns["BUILDINGTYPE"].ColumnName = "Тип корпуса";
                dt.Columns["EQUIPMENTLIST"].ColumnName = "Оборудование";
                gridview_foundroomsinfo.DataSource = dt;
                gridview_foundroomsinfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке данных:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }


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
        private void fpl_chbuild_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void fpl_chnumberroom_SelectedIndexChanged(object sender, EventArgs e) { }
        private void fpl_cheq_SelectedIndexChanged(object sender, EventArgs e) { }

        private void btn_applyfilter_Click(object sender, EventArgs e)
        {
        }

        private void page_struct_Click(object sender, EventArgs e)
        {

        }
    }
}