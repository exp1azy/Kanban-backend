using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        public Task<IEnumerable<Board>> GetAllUsersBoardsAsync(int userId, CancellationToken cancellationToken = default);

        public Task<Board?> GetBoardAsync(int id, CancellationToken cancellationToken = default);

        public Task AddBoardAsync(BoardClientModel board, CancellationToken cancellationToken = default);

        public Task<Board?> UpdateBoardAsync(string name, CancellationToken cancellationToken = default);

        public Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default);
    }
}
