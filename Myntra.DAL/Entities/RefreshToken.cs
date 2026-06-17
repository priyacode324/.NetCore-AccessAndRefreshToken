using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Myntra.DAL.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string? ReplacedByToken { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public bool IsActive =>
            RevokedAt == null &&
            ExpiresAt > DateTime.UtcNow;
    }
}
