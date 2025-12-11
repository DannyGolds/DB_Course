using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        string connStr = ConfigurationManager.ConnectionStrings["RedDbConnection"].ConnectionString;

        public DBOperations(string? connectionString = null)
        {
            // Если строка не передана, используем разумный дефолт — исправьте под свою среду.
            _connectionString = connectionString ?? connStr;
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

        public async Task<int> ExecProcedureAsync(string procName, params FbParameter[] parameters)
        {
            await using var cn = new FbConnection(_connectionString);
            await cn.OpenAsync();

            var placeholders = string.Join(", ", parameters.Select(_ => "?"));
            var sql = $"EXECUTE PROCEDURE {procName}({placeholders})";

            await using var cmd = new FbCommand(sql, cn);
            cmd.Parameters.AddRange(parameters);

            return await cmd.ExecuteNonQueryAsync(); // вернёт число затронутых строк
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

        internal async Task CallProcedureAsync(string v, FbParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
// Пример использования