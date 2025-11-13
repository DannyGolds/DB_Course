using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ManageSpacesOfInstitute
{
    public partial class Form1 : Form
    {
        // Путь к файлу БД — без префикса хоста, DataSource задаём отдельно
        private readonly string connection_string = "User=SYSDBA;Password=masterkey;" +
 "Database=localhost:C:\\RedDB\\Course.fdb;" +
 "DataSource=localhost;Port=3050;" +
 "Dialect=3;Charset=UTF8;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataFromDb();
        }

        private void LoadDataFromDb()
        {
            try
            {
                using (var connection = new FbConnection(connection_string))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Building";
                    using (var cmd = new FbCommand(sql, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Загружаем результат в DataTable (удобно для привязки к WinForms)
                        var table = new DataTable();
                        table.Load(reader);

                        // Найдём существующий DataGridView или создадим новый
                        var dgv = this.Controls.Find("dataGridViewBuildings", true).FirstOrDefault() as DataGridView;
                        if (dgv == null)
                        {
                            dgv = new DataGridView
                            {
                                Name = "dataGridViewBuildings",
                                Dock = DockStyle.Fill,
                                ReadOnly = true,
                                AutoGenerateColumns = true,
                                AllowUserToAddRows = false,
                                SelectionMode = DataGridViewSelectionMode.FullRowSelect
                            };

                            // Очистим tabPage1 и добавим таблицу
                            tabPage1.Controls.Clear();
                            tabPage1.Controls.Add(dgv);
                        }

                        // Привязываем данные
                        dgv.DataSource = table;

                        // Если строк нет — уведомим пользователя
                        if (table.Rows.Count == 0)
                            MessageBox.Show(this, "Записей не найдено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка при загрузке данных:\r\n" + ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
