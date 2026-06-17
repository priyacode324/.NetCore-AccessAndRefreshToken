using Microsoft.EntityFrameworkCore;
using Myntra.DAL.Data;
using Myntra.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.DAL.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly MyntraDbContext _context;
       
        public AccountRepository(MyntraDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            // Pass it directly into Entity Framework's FirstOrDefaultAsync
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email,cancellationToken);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
       
    }
}
