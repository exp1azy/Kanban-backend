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

        public string GenerateToken(UserDataModel user)
        {
            var jwtConfig = _config.GetSection("Jwt");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
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

        public async Task<UserDataModel> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var existUser = await _userRepository.GetUserByEmailAsync(email, cancellationToken) 
                ?? throw new ApplicationException(Error.UserWithSpecifiedNameDoesNotExist);

            var isPasswordCorrect = PasswordHashHelper.VerifyPassword(password, existUser.Password);
            if (!isPasswordCorrect)
                throw new ApplicationException(Error.PasswordsDoNotMatch);

            return new UserDataModel
            {
                Id = existUser.Id,
                Name = existUser.Name,
                Email = existUser.Email
            };
        }

        public async Task<UserNameEmailModel> CreateUserAsync(UserClientRegisterModel userModel, CancellationToken cancellationToken = default)
        {
            if (userModel == null) 
                throw new ApplicationException(Error.UserModelIsNull);

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

            userModel.Password = PasswordHashHelper.HashPassword(userModel.Password);

            await _userRepository.AddUserAsync(userModel, cancellationToken);

            return new UserNameEmailModel
            { 
                Email = userModel.Email, 
                Name = userModel.Name
            };
        }

        public async Task UpdateUserAsync(UserNameEmailModel userModel, CancellationToken cancellationToken = default)
        {
            if (userModel == null)
                throw new ApplicationException(Error.UserModelIsNull);

            await _userRepository.UpdateUserAsync(userModel, cancellationToken);
        }

        [GeneratedRegex(@"^[a-zA-Z]{4,}$")]
        private static partial Regex UsernameRegex();

        [GeneratedRegex(@"^(?=.*[0-9])(?=.*[a-zA-Z]).{6,}$")]
        private static partial Regex PasswordRegex();
    }
}
