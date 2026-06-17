using Myntra.DAL.Data;
using Myntra.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.DAL.Repositories
{
    public class RefreshTokenRepository:IRefreshTokenRepository
    {
        private readonly MyntraDbContext _context;
        public RefreshTokenRepository(MyntraDbContext context)
        {
            _context = context;
        }
        public async Task AddRefreshTokenToDbAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _context.RefreshTokens.AddAsync(refreshToken,cancellationToken);

            await _context.SaveChangesAsync();
        }
    }
  
}
