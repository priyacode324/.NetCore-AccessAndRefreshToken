using Microsoft.EntityFrameworkCore;
using Myntra.DAL.Entities;
using Myntra.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.BLL.RefreshServices
{
    public class RefreshService:IRefreshService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public RefreshService(IRefreshTokenRepository refreshTokenRepository) { 
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task AddRefreshTokenToDb(RefreshToken refreshToken)
        {
            await _refreshTokenRepository.AddRefreshTokenToDbAsync(refreshToken);

        }
    }
}
