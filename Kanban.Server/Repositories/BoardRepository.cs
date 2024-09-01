using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Server.Repositories
{
    public class BoardRepository(DataContext dataContext) : IBoardRepository
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task<IEnumerable<Board>> GetAllUserBoardsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var boards = await _dataContext.Boards
                .AsNoTracking()
                .Where(b => b.UserId == userId)
                .ToListAsync(cancellationToken);

            return boards;
        }

        public async Task<Board?> GetBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            var board = await _dataContext.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Cards)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            return board;
        }

        public async Task AddBoardAsync(BoardClientCreateModel board, CancellationToken cancellationToken = default)
        {
            await _dataContext.Boards.AddAsync(new Board
            {
                UserId = board.UserId,
                Name = board.Name,
                Created = DateTime.UtcNow
            }, cancellationToken);

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            var boardToDelete = await _dataContext.Boards.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            _dataContext.Boards.Remove(boardToDelete);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Board?> UpdateBoardAsync(BoardClientUpdateModel board, CancellationToken cancellationToken = default)
        {
            var boardToUpdate = await _dataContext.Boards.FirstOrDefaultAsync(b => b.Id == board.Id, cancellationToken);
            boardToUpdate.Name = board.Name;

            await _dataContext.SaveChangesAsync(cancellationToken);

            return boardToUpdate;
        }
    }
}
