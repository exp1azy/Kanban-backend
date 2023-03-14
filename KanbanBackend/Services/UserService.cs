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

        public async Task<AddUserModel> AddUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            var addUser = new AddUserModel();

            if (Regex.IsMatch(user.User_name, @"^[a-zA-Z]{4,}$"))
            {
                if (Regex.IsMatch(user.User_password, @"^(?=.*[0-9])(?=.*[a-zA-Z]).{6,}$"))
                {
                    if (Regex.IsMatch(user.User_email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                    {
                        if (await _dataContext.User.AnyAsync(u => u.UserName == user.User_name, cancellationToken))
                        {
                            addUser.ErrorMessage = "Пользователь с таким логином уже существует!";
                        }
                        else if (await _dataContext.User.AnyAsync(u => u.UserEmail == user.User_email, cancellationToken))
                        {
                            addUser.ErrorMessage = "Пользователь с такой почтой уже существует!";
                        }
                        else
                        {
                            addUser.User = user;

                            var newUser = new User()
                            {
                                UserName = user.User_name,
                                UserPassword = PasswordHasher.Hash(user.User_password, _config["Salt"]),
                                UserEmail = user.User_email
                            };

                            await _dataContext.User.AddAsync(newUser, cancellationToken);
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
                    currentUser.ErrorMessage = "Неверный пароль! Проверьте введённые данные";
                }
            }
            else
            {
                currentUser.ErrorMessage = "Такого пользователя не существует! Проверьте введённые данные";
            }
            
            return currentUser;           
        }
    }
}