using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NPE.Core.Common.Identity;

namespace NPE.Infrastructure.Common.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _context;

        public IdentityService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Operational Identity

        public async Task<T> ConsumeAsync<T>(int centerCode, string identityType, CancellationToken cancellationToken = default) where T : struct
        {
            centerCode = 1001;
            ValidateNumericType<T>();

            var identity = await _context.Identities.FromSqlRaw(@"SELECT * FROM [Identity] WITH (UPDLOCK, ROWLOCK) WHERE CenterCode = @Center AND Type = @type",
                        new SqlParameter("@Center", centerCode),
                        new SqlParameter("@type", identityType))
                    .SingleOrDefaultAsync(cancellationToken);

            if (identity == null)
            {
                throw new InvalidOperationException($"Identity not configured. Center={centerCode}, Type={identityType}");
            }

            var next = identity.CurrentValue + 1;

            if (next > identity.EndValue)
            {
                throw new InvalidOperationException($"Identity range exhausted. Center={centerCode}, Type={identityType}");
            }

            identity.CurrentValue = next;

            // UnitOfWork handles SaveChanges

            return ConvertValue<T>(next);
        }

        public async Task<T> GetNextAsync<T>(int centerCode, string identityType, CancellationToken cancellationToken = default) where T : struct
        {
            centerCode = 1001;
            ValidateNumericType<T>();

            var identity = await _context.Identities.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CenterCode == centerCode && x.Type == identityType, cancellationToken);

            if (identity == null)
            {
                throw new InvalidOperationException($"Identity not configured. Center={centerCode}, Type={identityType}");
            }

            var next = identity.CurrentValue + 1;

            if (next > identity.EndValue)
            {
                throw new InvalidOperationException($"Identity range exhausted. Center={centerCode}, Type={identityType}");
            }

            return ConvertValue<T>(next);
        }

        #endregion

        #region iLock Identity

        public async Task<int> ConsumeIlockAsync(string identityType, CancellationToken cancellationToken = default)
        {
            var identity = await _context.ILockIdentities
                    .FromSqlRaw(@"SELECT * FROM iLock_identity WITH (UPDLOCK, ROWLOCK) WHERE Type = @type",
                        new SqlParameter("@type", identityType))
                    .SingleOrDefaultAsync(cancellationToken);

            if (identity == null)
            {
                throw new InvalidOperationException($"Identity not configured. Type={identityType}");
            }

            var nextId = identity.NextId;
            identity.NextId = identity.NextId + 1;

            // UnitOfWork handles SaveChanges

            return nextId;
        }

        #endregion

        #region Helpers

        private static void ValidateNumericType<T>() where T : struct
        {
            var type = typeof(T);

            if (type != typeof(int) && type != typeof(long))
            {
                throw new InvalidOperationException($"Identity type '{type.Name}' is not supported.");
            }
        }

        private static T ConvertValue<T>(long value) where T : struct
        {
            if (typeof(T) == typeof(int))
            {
                if (value > int.MaxValue)
                {
                    throw new OverflowException($"Identity value {value} exceeds Int32 range.");
                }

                return (T)(object)(int)value;
            }

            if (typeof(T) == typeof(long))
            {
                return (T)(object)value;
            }

            throw new InvalidOperationException($"Unsupported identity type {typeof(T).Name}");
        }

        #endregion
    }
}