using Kanban.Server.Controllers.Models;
using Kanban.Server.Models;
using Kanban.Server.Repositories.Interfaces;
using Kanban.Server.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Kanban.Server.Resources;

namespace Kanban.Server.Services
{
    public partial class UserService(IUserRepository userRepository, IConfiguration config) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _config = config;

        public string GenerateToken(string name, string email)
        {
            var jwtConfig = _config.GetSection("Jwt");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email)
            };

            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"], 
                audience: jwtConfig["Audience"], 
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(7)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserModel> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            if (username == null || password == null)
                throw new ApplicationException(Error.AuthUserDataIsNull);

            var existUser = await _userRepository.GetUserByNameAsync(username, cancellationToken) 
                ?? throw new ApplicationException(Error.UserWithSpecifiedNameDoesNotExist);

            var isPasswordCorrect = PasswordHashHelper.VerifyPassword(password, existUser.Password);
            if (!isPasswordCorrect)
                throw new ApplicationException(Error.PasswordsDoNotMatch);

            return UserModel.Map(existUser)!;
        }

        public async Task CreateUserAsync(UserClientRegisterModel userModel, CancellationToken cancellationToken = default)
        {
            if (userModel == null) 
                throw new ApplicationException(Error.RegisterUserDataIsNull);

            var existUserByName = await _userRepository.GetUserByNameAsync(userModel.Name, cancellationToken);
            if (existUserByName != null)
                throw new ApplicationException(Error.UserWithSpecifiedNameAlreadyExist);

            var existUserByEmail = await _userRepository.GetUserByEmailAsync(userModel.Email, cancellationToken);
            if (existUserByEmail != null)
                throw new ApplicationException(Error.UserWithSpecifiedEmailAlreadyExist);

            if (!UsernameRegex().IsMatch(userModel.Name))
                throw new ApplicationException(Error.UsernameRegexDoNotMatch);

            if (!PasswordRegex().IsMatch(userModel.Password))
                throw new ApplicationException(Error.PasswordRegexDoNotMatch);

            if (!EmailRegex().IsMatch(userModel.Email))
                throw new ApplicationException(Error.EmailRegexDoNotMatch);

            userModel.Password = PasswordHashHelper.HashPassword(userModel.Password);

            await _userRepository.AddUserAsync(userModel, cancellationToken);
        }

        [GeneratedRegex(@"^[a-zA-Z]{4,}$")]
        private static partial Regex UsernameRegex();

        [GeneratedRegex(@"^(?=.*[0-9])(?=.*[a-zA-Z]).{6,}$")]
        private static partial Regex PasswordRegex();

        [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        private static partial Regex EmailRegex();
    }
}
