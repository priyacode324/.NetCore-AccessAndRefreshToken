using Microsoft.Extensions.Logging;
using Myntra.BLL.Helper;
using Myntra.DAL.Entities;
using Myntra.DAL.Enum;
using Myntra.DAL.Repositories;


namespace Myntra.DAL.Data
{
    public class DatabaseSeederService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<DatabaseSeederService>? _logger;
        public DatabaseSeederService(IAccountRepository accountRepository, ILogger<DatabaseSeederService>? logger = null)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }
   
        public async Task SeedAdminUserAsync()
        {
            const string adminEmail = "admin@myntra.com";

            var existingAdmin = await _accountRepository.GetUserByEmailAsync(adminEmail);
            if (existingAdmin != null)
            {
                _logger?.LogInformation("Admin user already exists.");
                return;
            }

            var admin = new User
            {
                Id = Guid.NewGuid(),
                Email = adminEmail,
                FirstName = "System",
                LastName = "Administrator",
                Role = UserRole.Admin,
                PasswordHash = PasswordHasher.HashPassword("Admin@12345!"),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            await _accountRepository.AddAsync(admin);

            _logger?.LogInformation("Admin user seeded successfully! Email: {Email}", adminEmail);
        }
    }
}
