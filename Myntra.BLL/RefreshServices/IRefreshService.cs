using Myntra.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.BLL.RefreshServices
{
    public interface IRefreshService
    {
        public Task AddRefreshTokenToDb(RefreshToken refreshToken);
    }
}
