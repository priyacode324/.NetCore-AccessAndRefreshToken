using Myntra.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.DAL.Repositories
{
    public interface IAccountRepository
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task AddAsync(User user);
        
    }
}
