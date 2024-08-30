using Kanban.Server.Controllers.Models;
using Kanban.Server.Models;

namespace Kanban.Server.Services.Interfaces
{
    public interface IUserService
    {
        public string GenerateToken(string name, string email);

        public Task CreateUserAsync(UserClientRegisterModel userClientFullModel, CancellationToken cancellationToken = default);

        public Task<UserModel> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default);
    }
}
