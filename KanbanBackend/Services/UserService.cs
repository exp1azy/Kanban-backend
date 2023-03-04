using KanbanBackend.Data;
using KanbanBackend.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Cryptography;

namespace KanbanBackend.Services
{
    public class UserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<AddUserModel> AddUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            var response = new AddUserModel();

            if (_dataContext.users.Any(u => u.user_name == user.User_name))
            {
                response.ErrorMessage = "Пользователь с таким логином уже существует!";
            }
            else if (_dataContext.users.Any(u => u.user_email == user.User_email))
            {
                response.ErrorMessage = "Пользователь с такой почтой уже существует!";
            }
            else
            {
                response.User = user;

                var newUser = new users
                {
                    user_name = user.User_name,
                    user_password = PasswordHasher.Hash(user.User_password),
                    user_email = user.User_email
                };

                await _dataContext.users.AddAsync(newUser, cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }

            return response;
        }
    }
}
