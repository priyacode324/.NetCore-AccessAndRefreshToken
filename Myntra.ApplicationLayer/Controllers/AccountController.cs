using Microsoft.AspNetCore.Mvc;
using Myntra.BLL.AccountServices;
using Myntra.BLL.Configuration;
using Myntra.Shared.DTOs;

namespace Myntra.ApplicationLayer.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;    
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDTO>>Login(LoginRequestDTO request,CancellationToken cancellationToken=default)
        {
            var response = await _accountService.Login(request.Email,request.Password,cancellationToken);

            if (response == null)
            {
                return Unauthorized("Invalid email or password");
            }
            return Ok(response);
        }
    }
}
