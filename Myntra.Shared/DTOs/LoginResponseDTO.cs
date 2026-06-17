using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.Shared.DTOs
{
    public class LoginResponseDTO
    {
       
            public string AccessToken { get; set; } = string.Empty;

            public string RefreshToken { get; set; } = string.Empty;

            public int AccessTokenExpiresIn { get; set; }

            public UserDTO User { get; set; } = null!;
       
    }
}
