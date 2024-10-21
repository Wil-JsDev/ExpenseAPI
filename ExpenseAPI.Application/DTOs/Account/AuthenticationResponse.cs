using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpenseAPI.Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public bool IsVerified { get; set; }

        public bool HasError { get; set; }

        public string Error { get; set; }

        public string JWTToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
