using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ManageSpacesOfInstitute
{
    // Поверхностная модель для таблицы BUILDING — используйте существующую модель, если она уже есть.
    public sealed record BuildingSpace(int Id, string Name, string Type);

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

        // Валидация имени процедуры (предотвращает простую инъекцию при подстановке в SELECT)
        private static void ValidateIdentifier(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя процедуры не задано.", nameof(name));

            foreach (var c in name)
            {
                if (!(char.IsLetterOrDigit(c) || c == '_' ))
                    throw new ArgumentException("Имя процедуры содержит недопустимые символы.", nameof(name));
            }
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

        // ----- ВЫЗОВ ХРАНИМЫХ ПРОЦЕДУР -----

        // Синхронно: вызывает процедуру (selectable или executable).
        // Возвращает таблицу (если есть result set) и словарь выходных параметров.
        public (DataTable Table, Dictionary<string, object?> OutputParameters) ExecuteProcedure(string procName, params FbParameter[]? parameters)
        {
            ValidateIdentifier(procName);

            // 1) Попробуем вызвать как selectable: SELECT * FROM procName
            try
            {
                var sql = $"SELECT * FROM {procName}";
                var table = GetDataTable(sql, parameters ?? Array.Empty<FbParameter>());
                if (table != null && (table.Rows.Count > 0 || table.Columns.Count > 0))
                    return (table, new Dictionary<string, object?>());
            }
            catch (FbException)
            {
                // игнорируем — попробуем вызвать как executable
            }

            // 2) Вызов как CommandType.StoredProcedure
            var outputs = new Dictionary<string, object?>();
            using var cn = new FbConnection(_connectionString);
            cn.Open();

            using var cmd = new FbCommand(procName, cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            using var reader = cmd.ExecuteReader();
            var resultTable = new DataTable();
            if (reader.HasRows)
                resultTable.Load(reader);
            reader.Close();

            foreach (FbParameter p in cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input)
                    outputs[p.ParameterName] = p.Value == DBNull.Value ? null : p.Value;
            }

            return (resultTable, outputs);
        }

        // Асинхронно: вызывает процедуру (selectable или executable).
        public async Task<(DataTable Table, Dictionary<string, object?> OutputParameters)> ExecuteProcedureAsync(string procName, params FbParameter[]? parameters)
        {
            ValidateIdentifier(procName);

            // 1) Попробуем как SELECT * FROM procName
            try
            {
                var sql = $"SELECT * FROM {procName}";
                var table = await GetDataTableAsync(sql, parameters ?? Array.Empty<FbParameter>());
                if (table != null && (table.Rows.Count > 0 || table.Columns.Count > 0))
                    return (table, new Dictionary<string, object?>());
            }
            catch (FbException)
            {
                // игнорируем и пробуем вызвать как stored procedure
            }

            var outputs = new Dictionary<string, object?>();

            await using var cn = new FbConnection(_connectionString);
            await cn.OpenAsync();

            await using var cmd = new FbCommand(procName, cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            await using var reader = await cmd.ExecuteReaderAsync();
            var result = new DataTable();
            if (reader.HasRows)
                result.Load(reader);
            await reader.DisposeAsync();

            foreach (FbParameter p in cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input)
                    outputs[p.ParameterName] = p.Value == DBNull.Value ? null : p.Value;
            }

            return (result, outputs);
        }

        // Выполнить INSERT/UPDATE/DELETE (синхронно)
        public int ExecuteNonQuery(string sql, params FbParameter[]? parameters)
        {
            using var cn = new FbConnection(_connectionString);
            cn.Open();

            using var cmd = new FbCommand(sql, cn);
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            return cmd.ExecuteNonQuery();
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