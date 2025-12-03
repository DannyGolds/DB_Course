using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ManageSpacesOfInstitute
{

    // Единственное место для всех операций с БД в приложении.
    internal sealed class DBOperations : IDisposable, IAsyncDisposable
    {
        private readonly string _connectionString;
        private bool _disposed;

        public DBOperations(string? connectionString = null)
        {
            // Если строка не передана, используем разумный дефолт — исправьте под свою среду.
            _connectionString = connectionString ?? new FbConnectionStringBuilder
            {
                UserID = "SYSDBA",
                Password = "masterkey",
                Database = @"C:/Users/PC/3D Objects/DB/INSTITUTEDBNEW.FDB",
                DataSource = "127.0.0.1",
                Port = 3050,
                Dialect = 3
            }.ToString();
        }

        // Синхронное получение DataTable
        public DataTable GetDataTable(string sql, params FbParameter[]? parameters)
        {
            var table = new DataTable();

            using var cn = new FbConnection(_connectionString);
            cn.Open();

            using var cmd = new FbCommand(sql, cn);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            using var reader = cmd.ExecuteReader();
            table.Load(reader);
            return table;
        }

        // Асинхронное получение DataTable
        public async Task<DataTable> GetDataTableAsync(string sql, params FbParameter[]? parameters)
        {
            var table = new DataTable();

            await using var cn = new FbConnection(_connectionString);
            await cn.OpenAsync();

            await using var cmd = new FbCommand(sql, cn);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await using var reader = await cmd.ExecuteReaderAsync();
            table.Load(reader); // Load — синхронный, но это приемлемо для небольших таблиц
            return table;
        }

        public async Task<DataTable> CallProcedureAsync(string procName, List<string> col_list, params FbParameter[]? parameters)
        {
            var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "GETROOMFULLINFO",
        "GET_EQUIPMENT_INFO",
        "INSERTUSER",
        "GET_USER_BY_USERNAME",
        "GET_PARTIAL_ROOM_INFO"

    };
            if (!allowed.Contains(procName))
                throw new InvalidOperationException($"Недопустимое имя процедуры: {procName}");

            string sql;
            if (parameters != null && parameters.Length > 0)
            {
                var placeholders = string.Join(", ", parameters.Select(_ => "?"));
                sql = $"SELECT {string.Join(", ", col_list)} FROM {procName}({placeholders})";
            }
            else
            {
                // НЕТ параметров → НЕТ скобок!
                sql = $"SELECT {string.Join(", ", col_list)} FROM {procName}";
            }

            var table = new DataTable();
            await using var cn = new FbConnection(_connectionString);
            await cn.OpenAsync();

            await using var cmd = new FbCommand(sql, cn);
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    cmd.Parameters.Add(p);
                }
            }

            await using var reader = await cmd.ExecuteReaderAsync();
            table.Load(reader);

            return table;
        }

        // Асинхронный ExecuteNonQuery
        public async Task<int> ExecuteNonQueryAsync(string sql, params FbParameter[]? parameters)
        {
            await using var cn = new FbConnection(_connectionString);
            await cn.OpenAsync();

            await using var cmd = new FbCommand(sql, cn);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            return await cmd.ExecuteNonQueryAsync();
        }

        // Dispose — на будущее, если добавите состояние/кэширование
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        // Реализация асинхронного освобождения — для совместимости с await using.
        public ValueTask DisposeAsync()
        {
            // Если в будущем появятся async-ресурсы, освободите их асинхронно здесь.
            Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
// Пример использования