using Myntra.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Myntra.BLL.AccountServices
{
    public interface IAccountService
    {
        public Task<LoginResponseDTO?> Login(string email, string password,CancellationToken cancellationToken=default);
    }
}
