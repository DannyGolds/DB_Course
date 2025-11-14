using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ManageSpacesOfInstitute
{
    // Простая модель для таблицы Building
    public sealed record BuildingSpace(int Id, string Name, string Type);

    // Класс- посредник для работы с БД
    internal sealed class Connection
    {
        private readonly string _connectionString;

        // По умолчанию подключение к локальной БД. При необходимости передавайте свою строку в конструктор.
        public Connection(string? connectionString = null)
        {
            _connectionString = connectionString ?? "User=SYSDBA;Password=masterkey;Database=C:\\RedDB\\Course.fdb;DataSource=127.0.0.1;Port=3050;Dialect=3;Charset=UTF8;Pooling=true;";
        }

        /// <summary>
        /// Выполнить SQL и вернуть результат в DataTable (синхронно).
        /// </summary>
        public DataTable GetDataTable(string sql, params FbParameter[]? parameters)
        {
            var table = new DataTable();

            using var cn = new FbConnection(_connectionString);
            cn.Open();

            using var cmd = new FbCommand(sql, cn);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            using var reader = cmd.ExecuteReader();
            table.Load(reader);

            return table;
        }

        /// <summary>
        /// Асинхронная версия получения DataTable.
        /// </summary>
        public async Task<DataTable> GetDataTableAsync(string sql, params FbParameter[]? parameters)
        {
            var table = new DataTable();

            await using var cn = new FbConnection(_connectionString);
            await cn.OpenAsync();

            await using var cmd = new FbCommand(sql, cn);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            await using var reader = await cmd.ExecuteReaderAsync();
            table.Load(reader); // DataTable.Load не имеет async-версии
            return table;
        }

        /// <summary>
        /// Получить список зданий как коллекцию моделей.
        /// </summary>
        public List<BuildingSpace> GetBuildings()
        {
            const string sql = "SELECT BUILDING_ID, BUILDING_NAME, BUILDING_TYPE FROM BUILDING";
            var result = new List<BuildingSpace>();
            var dt = GetDataTable(sql);

            foreach (DataRow r in dt.Rows)
            {
                var id = r["BUILDING_ID"] != DBNull.Value ? Convert.ToInt32(r["BUILDING_ID"]) : 0;
                var name = r["BUILDING_NAME"] != DBNull.Value ? r["BUILDING_NAME"].ToString()! : string.Empty;
                var type = r["BUILDING_TYPE"] != DBNull.Value ? r["BUILDING_TYPE"].ToString()! : string.Empty;
                result.Add(new BuildingSpace(id, name, type));
            }

            return result;
        }

        /// <summary>
        /// Асинхронный вариант получения списка зданий.
        /// </summary>
        public async Task<List<BuildingSpace>> GetBuildingsAsync()
        {
            const string sql = "SELECT BUILDING_ID, BUILDING_NAME, BUILDING_TYPE FROM BUILDING";
            var list = new List<BuildingSpace>();
            var dt = await GetDataTableAsync(sql);

            foreach (DataRow r in dt.Rows)
            {
                var id = r["BUILDING_ID"] != DBNull.Value ? Convert.ToInt32(r["BUILDING_ID"]) : 0;
                var name = r["BUILDING_NAME"] != DBNull.Value ? r["BUILDING_NAME"].ToString()! : string.Empty;
                var type = r["BUILDING_TYPE"] != DBNull.Value ? r["BUILDING_TYPE"].ToString()! : string.Empty;
                list.Add(new BuildingSpace(id, name, type));
            }

            return list;
        }

        /// <summary>
        /// Выполнить SQL-операцию без результата (INSERT/UPDATE/DELETE).
        /// Возвращает число затронутых строк.
        /// </summary>
        public int ExecuteNonQuery(string sql, params FbParameter[]? parameters)
        {
            using var cn = new FbConnection(_connectionString);
            cn.Open();

            using var cmd = new FbCommand(sql, cn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);

            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Асинхронный ExecuteNonQuery.
        /// </summary>
        public async Task<int> ExecuteNonQueryAsync(string sql, params FbParameter[]? parameters)
        {
            await using var cn = new FbConnection(_connectionString);
            await cn.OpenAsync();

            await using var cmd = new FbCommand(sql, cn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}

var db = new Connection();
var table = db.GetDataTable("SELECT * FROM BUILDING");
dataGridViewBuildings.DataSource = table;
