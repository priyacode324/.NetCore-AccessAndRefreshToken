using Microsoft.Extensions.Options;
using Myntra.BLL.Configuration;
using Myntra.BLL.Helper;
using Myntra.BLL.RefreshServices;
using Myntra.DAL.Entities;
using Myntra.DAL.Repositories;
using Myntra.Shared.DTOs;


namespace Myntra.BLL.AccountServices
{
    public class AccountService:IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly JwtOptions _jwtOptions;
        public AccountService(IJwtTokenService jwtTokenService,IOptions<JwtOptions> jwtOptions,IUnitOfWork unitOfWork)
        {
            _jwtTokenService = jwtTokenService;
            _jwtOptions = jwtOptions.Value;
            _unitOfWork = unitOfWork;
        }

        // 1. Add CancellationToken to the method signature (default to default if needed)
        public async Task<LoginResponseDTO?> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            // 2. Pass cancellationToken to the transaction startup
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                // 3. Pass it to the database query
                var user = await _unitOfWork.AccountRepository.GetUserByEmailAsync(email, cancellationToken);
                if (user == null)
                {
                    return null;
                }
                if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
                {
                    return null;
                }

                UserDTO userDto = new UserDTO();
                userDto.Id = user.Id;
                userDto.Email = user.Email;
                userDto.FirstName = user.FirstName;
                userDto.LastName = user.LastName;
                userDto.Role = user.Role.ToString();

                var response = _jwtTokenService.GenerateToken(userDto);
                var hashedRefreshToken =RefreshTokenHasher.HashToken(response.RefreshToken);
                var refreshTokenEntity = new RefreshToken
                {
                    Token = hashedRefreshToken,
                    UserId = user.Id,
                    ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryDays)
                };

                // 4. Pass it to the insert operation
                await _unitOfWork.RefreshTokenRepository.AddRefreshTokenToDbAsync(refreshTokenEntity, cancellationToken);

                // 5. Pass it to SaveChanges and Commit
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return response;
            }
            catch (Exception ex)
            {
                // Note: We generally don't pass cancellationToken to Rollback 
                // because we want the rollback to succeed even if the user disconnected.
                await _unitOfWork.RollbackTransactionAsync();

                throw;
            }
        }
    }
}

