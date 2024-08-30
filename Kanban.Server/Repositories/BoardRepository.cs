using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Server.Repositories
{
    public class BoardRepository(DataContext dataContext) : IBoardRepository
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task<IEnumerable<Board>> GetAllUsersBoardsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var boards = await _dataContext.Boards.AsNoTracking().Where(b => b.UserId == userId).ToListAsync(cancellationToken);

            return boards.AsEnumerable();
        }

        public Task<Board?> GetBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddBoardAsync(BoardClientModel board, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Board?> UpdateBoardAsync(string name, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
