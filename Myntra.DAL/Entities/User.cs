using Myntra.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Myntra.DAL.Entities
{
    public class User:BaseEntity
    {
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Customer;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();//// Initialize to avoid null reference
    }
}
