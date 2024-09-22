using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);

        public Task<User?> GetUserByNameAsync(string name, CancellationToken cancellationToken = default);

        public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

        public Task AddUserAsync(UserRegisterClientModel user, CancellationToken cancellationToken = default);

        public Task UpdateUserAsync(UserUpdateClientModel user, CancellationToken cancellationToken = default);
    }
}
