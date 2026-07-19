using Microsoft.EntityFrameworkCore.Storage;
using NPE.Core.Common.UnitOfWork;

namespace NPE.Infrastructure.Common.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BeginAsync()
        {
            if (_transaction != null)
                throw new InvalidOperationException("transaction already started.");

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();

            if (_transaction == null)
                return;

            await _transaction!.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
                return;

            await _transaction!.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
