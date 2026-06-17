using Myntra.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.DAL.Repositories
{
    public interface IRefreshTokenRepository
    {
        public Task AddRefreshTokenToDbAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    }
}
