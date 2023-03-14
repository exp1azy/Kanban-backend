using KanbanBackend.Data;
using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace KanbanBackend.Services
{
    public class UserService
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public UserService(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _config = config;
        }
    
        public string GenerateToken(UserLoginModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.User_name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"], claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task AddUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            if (Regex.IsMatch(user.Name, @"^[a-zA-Z]{4,}$"))
            {
                if (Regex.IsMatch(user.Password, @"^(?=.*[0-9])(?=.*[a-zA-Z]).{6,}$"))
                {
                    if (Regex.IsMatch(user.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    {
                        if (await _dataContext.User.AnyAsync(u => u.UserName == user.Name, cancellationToken))
                        {
                            throw new ApplicationException("Пользователь с таким логином уже существует!");
                        }
                        if (await _dataContext.User.AnyAsync(u => u.UserEmail == user.Email, cancellationToken))
                        {
                             throw new ApplicationException("Пользователь с такой почтой уже существует!");
                        }
                        
                        var newUser = new User()
                        {
                            UserName = user.Name,
                            UserPassword = PasswordHasher.Hash(user.Password, _config["Salt"]),
                            UserEmail = user.Email
                        };

                        await _dataContext.User.AddAsync(newUser, cancellationToken);
                        await _dataContext.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        throw new ApplicationException("Некорректный email-адрес!");
                    }
                }
                else
                {
                    throw new ApplicationException("Пароль должен содержать только символы английского алфавита и хотя бы одну цифру, а также состоять хотя бы из 6 символов!");
                }
            }
            else
            {
                throw new ApplicationException("Никнейм должен содержать только символы английского алфавита и состоять хотя бы из четырёх символов!");
            }
        }

        public async Task<UserLoginModel> AuthenticateUserAsync(string username, string password, CancellationToken cancellationToken)
        {
            var currentUser = new UserLoginModel();

            var dalUser = await _dataContext.User.FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);

            if (dalUser != null)
            {
                password = PasswordHasher.Hash(password, _config["Salt"]);

                if (password == dalUser.UserPassword)
                {
                    currentUser.Id = dalUser.UserId;
                    currentUser.User_name = username;
                    currentUser.User_password = password;
                }
                else
                {
                    throw new ApplicationException("Неверный пароль! Проверьте введённые данные");
                }
            }
            else
            {
                throw new ApplicationException("Такого пользователя не существует! Проверьте введённые данные");
            }
            
            return currentUser;           
        }
    }
}