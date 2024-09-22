using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        public Task<IEnumerable<Board>> GetAllUserBoardsAsync(int userId, CancellationToken cancellationToken = default);

        public Task<Board?> GetBoardAsync(int id, CancellationToken cancellationToken = default);

        public Task AddBoardAsync(BoardCreateClientModel board, CancellationToken cancellationToken = default);

        public Task<Board> UpdateBoardAsync(BoardUpdateClientModel board, CancellationToken cancellationToken = default);

        public Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default);
    }
}
