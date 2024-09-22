using Kanban.Server.Controllers.Models;
using Kanban.Server.Models;

namespace Kanban.Server.Services.Interfaces
{
    public interface IUserService
    {
        public string GenerateToken(UserDataModel user);

        public Task CreateUserAsync(UserClientRegisterModel userClientFullModel, CancellationToken cancellationToken = default);

        public Task<UserDataModel> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default);

        public Task UpdateUserAsync(UserNameEmailModel user, CancellationToken cancellationToken = default);
    }
}
