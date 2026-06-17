using System.Security.Cryptography;
using System.Text;

namespace Myntra.BLL.Helper
{
    public static class RefreshTokenHasher
    {
        public static string HashToken(string refreshToken)
        {
            using var sha256 = SHA256.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(refreshToken);

            byte[] hash = sha256.ComputeHash(bytes);

            return Convert.ToHexString(hash);
        }
    }
}
