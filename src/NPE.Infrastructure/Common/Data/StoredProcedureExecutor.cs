using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace NPE.Infrastructure.Common.Data
{
    public interface IStoredProcedureExecutor
    {
        Task<List<T>> ExecuteAsync<T>(
            string procedureName,
            Action<DbCommand> parameterBuilder) where T : new();

        Task<(List<T1>, List<T2>)> ExecuteMultipleAsync<T1, T2>(
    string procedureName,
    Action<DbCommand> parameterBuilder)
    where T1 : new()
    where T2 : new();

        Task<SpResult<T>> ExecuteWithOutputAsync<T>(
    string procedureName,
    Action<DbCommand> parameterBuilder)
    where T : new();
    }

    public class StoredProcedureExecutor : IStoredProcedureExecutor
    {
        private readonly ApplicationDbContext _context;

        public StoredProcedureExecutor(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> ExecuteAsync<T>(
            string procedureName,
            Action<DbCommand> parameterBuilder) where T : new()
        {
            using var command = _context.Database.GetDbConnection().CreateCommand();

            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            await _context.Database.OpenConnectionAsync();

            // 🔥 add params
            parameterBuilder(command);

            using var reader = await command.ExecuteReaderAsync();

            return FlexibleMapper.MapToList<T>(reader);
        }

        public async Task<(List<T1>, List<T2>)> ExecuteMultipleAsync<T1, T2>(
    string procedureName,
    Action<DbCommand> parameterBuilder)
    where T1 : new()
    where T2 : new()
        {
            using var command = _context.Database.GetDbConnection().CreateCommand();

            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            await _context.Database.OpenConnectionAsync();

            parameterBuilder(command);

            using var reader = await command.ExecuteReaderAsync();

            var result1 = FlexibleMapper.MapToList<T1>(reader);

            await reader.NextResultAsync();

            var result2 = FlexibleMapper.MapToList<T2>(reader);

            return (result1, result2);
        }

        public async Task<SpResult<T>> ExecuteWithOutputAsync<T>(
    string procedureName,
    Action<DbCommand> parameterBuilder)
    where T : new()
        {
            using var command = _context.Database.GetDbConnection().CreateCommand();

            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            await _context.Database.OpenConnectionAsync();

            // 🔥 Build parameters
            parameterBuilder(command);

            using var reader = await command.ExecuteReaderAsync();

            var data = FlexibleMapper.MapToList<T>(reader);

            // 🔥 Collect output params AFTER execution
            var output = command.Parameters
                .Cast<DbParameter>()
                .Where(p => p.Direction == ParameterDirection.Output
                         || p.Direction == ParameterDirection.InputOutput
                         || p.Direction == ParameterDirection.ReturnValue)
                .ToDictionary(p => p.ParameterName, p => p.Value);

            return new SpResult<T>
            {
                Data = data,
                OutputParameters = output
            };
        }
    }
}
