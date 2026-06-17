using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.DAL.Repositories
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }  

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync();
    }
}
