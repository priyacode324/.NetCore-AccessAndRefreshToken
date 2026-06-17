using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.BLL.Configuration
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public int ExpiryMinutes { get; set; }
        public int RefreshTokenExpiryDays { get; set; }
    }
}
