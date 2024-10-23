using ExpenseAPI.Application.DTOs.Account;
using ExpenseAPI.Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.Net.Mime;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var authenticate = await _accountService.AuthenticationAsync(request);
            return Ok(authenticate);
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterBasiucUserAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"].ToString(); 
            return Ok(await _accountService.RegisterBasicUserAsync(request, origin));
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdminUserAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"].ToString();
            return Ok(await _accountService.RegisterAdminUser(request,origin));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmAccount(string userId, string token)
        {
            var confirmAccount = await _accountService.ConfirmAccountAsync(userId, token);
            return Ok(confirmAccount);
        }

    }
}
