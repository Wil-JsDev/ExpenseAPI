using ExpenseAPI.Application.DTOs.Account;
using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Domain.Settings;
using ExpenseAPI.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ExpenseAPI.Domain.Enum;
using Microsoft.AspNetCore.WebUtilities;
using ExpenseAPI.Application.DTOs.Email;
using Resend;

namespace ExpenseAPI.Infraestructure.Identity.Service
{
    
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IEmailService _emailService;

        private readonly JWTSettings _jWTSettings;


        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
           IEmailService emailService, IOptions<JWTSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jWTSettings = options.Value;
        }


        public async Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            response.Id = user.Id;
            response.Username = user.UserName;
            response.Email = user.Email;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.JWTToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new RegisterResponse()
            {
                HasError = false
            };

            var userWithSameUsername = await _userManager.FindByNameAsync(request.Username);
            if (userWithSameUsername != null)
            {
                response.HasError = true;
                response.Error = $"Username '{request.Username}' is already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered";
                return response;
            }

            var user = new User()
            {
                Email = request.Email,
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.Phone,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                var verification = await SendVerificationEmilUrl(user, origin);
                await _emailService.Execute(new EmailRequestDto
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this url {verification}",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = "An error ocurred trying to registed the user";
                return response;
            }
            return response;
        }

        //public async Task<RegisterResponse> RegisterAdminUser(RegisterRequest request, string origin)
        //{
        //    RegisterResponse response = new RegisterResponse()
        //    {
        //        HasError = false
        //    };

        //    var userName = await _userManager.FindByNameAsync(request.Username);
        //    if (userName != null)
        //    {
        //        response.HasError = true;
        //        response.Error = $"Username '{userName}' already taken";
        //        return response;
        //    }

        //    var userWithEmail = await _userManager.FindByEmailAsync(request.Email);
        //    if (userWithEmail != null)
        //    {
        //        response.HasError = true;
        //        response.Error = $"Email '{request.Email}' already taken";
        //        return response;
        //    }

        //    User userAdmin = new User
        //    {
        //        Email = request.Email,
        //        UserName = request.Username,
        //        FirstName = request.FirstName,
        //        LastName = request.LastName,
        //        PhoneNumber = request.Phone,
        //    };

        //    var result = await _userManager.CreateAsync(userAdmin, Roles.Admin.ToString());
        //    if (result.Succeeded)
        //    {
        //        await _userManager.AddToRoleAsync(userAdmin, Roles.Admin.ToString());
        //        var verification = await SendVerificationEmilUrl(userAdmin, origin);
        //        await _emailService.Execute(new EmailRequestDto()
        //        {
        //            To = userAdmin.Email,
        //            Body = $"<p>Please confirm your account by visiting this URL:</p><p><a href=\"{verification}\">{verification}</a></p>",
        //            Subject = "Confirm registration"
        //        });
        //    }
        //    else
        //    {
        //        response.HasError = true;
        //        response.Error = "An error ocurred trying to registed the user admin";
        //        return response;
        //    }

        //    return response;
        //}

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return $"No Account registered with this user";
            }


            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirm for {user.Email}. you can now use the app";
            }
            else
            {
                return $"An error occurred while confirming{user.Email}";
            }


        }

        #region Private Method

        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var rolesClaims = new List<Claim>();

            foreach (var role in roles)
            {
                rolesClaims.Add(new Claim("roles", role));
            }

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(rolesClaims);

            //Generar la llave simetrica
            var symmectricSecutityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecutityKey, SecurityAlgorithms.HmacSha256);

            //Token ya creado
            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jWTSettings.Issuer,
                audience: _jWTSettings.Audience,
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(_jWTSettings.DurationInMinutes),
                signingCredentials: signingCredetials
            );

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expired = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomByte = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomByte);

            return BitConverter.ToString(randomByte).Replace("-", " ");
        }


        private async Task<string> SendVerificationEmilUrl(User user, string origin)
        {
            //Origin es el localhost desde donde se ejectua el API, eso se obtiene desde el controller, porque puede variar
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user); //Para crear un token de validaciones con Identity
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); //Esto es para condificarlo el token generado por Identity

            var route = "User/ConfirmEmail"; //
            var Uri = new Uri(string.Concat($"{origin}/", route));

            //Al Uri le voy añadir esos parametros, eso se se hace con estas funciones  QueryHelpers.AddQueryString(), userId.Id es el valor
            var verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUrl;
        }

        #endregion

    }
}
