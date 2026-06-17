using BC = BCrypt.Net.BCrypt;
namespace Myntra.BLL.Helper
{
    public static class PasswordHasher
    {
        private const int WorkFactor = 12;

        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            // EnhancedHashPassword includes salt + work factor automatically
            return BC.EnhancedHashPassword(password, WorkFactor);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            return BC.EnhancedVerify(password, hashedPassword);
        }
    }
}
