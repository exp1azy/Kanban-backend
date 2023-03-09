using KanbanBackend.Data;
using KanbanBackend.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
    
        public string GenerateToken(UserLogin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.User_name)
            };

            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"], claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AddUserModel> AddUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            var addUser = new AddUserModel();

            if (Regex.IsMatch(user.User_name, @"^[a-zA-Z]{4,}$"))
            {
                if (Regex.IsMatch(user.User_password, @"^(?=.*[0-9])(?=.*[a-zA-Z]).{6,}$"))
                {
                    if (Regex.IsMatch(user.User_email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    {
                        if (await _dataContext.users.AnyAsync(u => u.user_name == user.User_name, cancellationToken))
                        {
                            addUser.ErrorMessage = "Пользователь с таким логином уже существует!";
                        }
                        else if (await _dataContext.users.AnyAsync(u => u.user_email == user.User_email, cancellationToken))
                        {
                            addUser.ErrorMessage = "Пользователь с такой почтой уже существует!";
                        }
                        else
                        {
                            addUser.User = user;

                            var newUser = new users
                            {
                                user_name = user.User_name,
                                user_password = PasswordHasher.Hash(user.User_password),
                                user_email = user.User_email
                            };

                            await _dataContext.users.AddAsync(newUser, cancellationToken);
                            await _dataContext.SaveChangesAsync(cancellationToken);
                        }
                    }
                    else
                    {
                        addUser.ErrorMessage = "Некорректный email-адрес!";
                    }
                }
                else
                {
                    addUser.ErrorMessage = "Пароль должен содержать только символы английского алфавита и хотя бы одну цифру, а также состоять хотя бы из 6 символов!";
                }
            }
            else
            {
                addUser.ErrorMessage = "Никнейм должен содержать только символы английского алфавита и состоять хотя бы из четырёх символов!";
            }

            return addUser;
        }

        public async Task<UserLogin> AuthenticateUserAsync(string username, string password, CancellationToken cancellationToken)
        {
            var currentUser = new UserLogin();

            if (!await _dataContext.users.AnyAsync(u => u.user_name == username, cancellationToken))
            {
                currentUser.ErrorMessage = "Такого пользователя не существует! Проверьте введённые данные";
            }
            else if (!await _dataContext.users.AnyAsync(u => u.user_password == password, cancellationToken))
            {
                currentUser.ErrorMessage = "Неверный пароль! Проверьте введённые данные";
            }
            else
            {
                currentUser.User_name = username;
                currentUser.User_password = password;
            }

            return currentUser;           
        }
    }
}
