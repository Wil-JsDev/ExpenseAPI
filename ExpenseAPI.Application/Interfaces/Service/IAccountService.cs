using ExpenseAPI.Application.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.Interfaces.Service
{
    public interface IAccountService
    {
       Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request);

       Task<string> ConfirmAccountAsync(string userId, string token);

       //Task<RegisterResponse> RegisterAdminUser(RegisterRequest request, string origin);

       Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);

    }
}
