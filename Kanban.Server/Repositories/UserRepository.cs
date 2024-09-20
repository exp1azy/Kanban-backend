using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Server.Repositories
{
    public class UserRepository(DataContext dataContext) : IUserRepository
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task AddUserAsync(UserClientRegisterModel user, CancellationToken cancellationToken = default)
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };

            await _dataContext.Users.AddAsync(newUser, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUserAsync(UserNameEmailModel user, CancellationToken cancellationToken = default)
        {
            var userToUpdate = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email, cancellationToken);

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public async Task<User?> GetUserByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(user => user.Name == name, cancellationToken);
        }
    }
}
