using Myntra.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.BLL.Configuration
{
    public interface IJwtTokenService
    {
        LoginResponseDTO GenerateToken(UserDTO user);
    }
}
