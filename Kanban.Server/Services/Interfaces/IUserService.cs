using Kanban.Server.Controllers.Models;
using Kanban.Server.Models;

namespace Kanban.Server.Services.Interfaces
{
    public interface IUserService
    {
        public string GenerateToken(UserDataModel user);

        public Task CreateUserAsync(UserRegisterClientModel userClientFullModel, CancellationToken cancellationToken = default);

        public Task<UserDataModel> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);

        public Task UpdateUserAsync(UserUpdateClientModel user, CancellationToken cancellationToken = default);
    }
}
