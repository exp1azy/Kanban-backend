using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Repositories.Interfaces;
using Kanban.Server.Resources;
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

        public async Task AddBoardAsync(int userId, BoardCreateClientModel board, CancellationToken cancellationToken = default)
        {
            var boardToCreate = new Board
            {
                UserId = userId,
                Name = board.Name
            };

            await _dataContext.Boards.AddAsync(boardToCreate, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            using var trans = await _dataContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var boardToDelete = await _dataContext.Boards
                    .Include(b => b.Columns)
                    .ThenInclude(c => c.Cards)
                    .FirstOrDefaultAsync(b => b.Id == id, cancellationToken)
                    ?? throw new ApplicationException(Error.BoardNotFound);

                if (boardToDelete.Columns.Count != 0)
                {
                    foreach (var column in boardToDelete.Columns)
                    {
                        if (column.Cards.Count != 0)
                            _dataContext.Cards.RemoveRange(column.Cards);
                    }

                    _dataContext.Columns.RemoveRange(boardToDelete.Columns);
                }               
               
                _dataContext.Boards.Remove(boardToDelete);
                await _dataContext.SaveChangesAsync(cancellationToken);

                await trans.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await trans.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Board> UpdateBoardAsync(BoardUpdateClientModel board, CancellationToken cancellationToken = default)
        {
            var boardToUpdate = await _dataContext.Boards.FirstOrDefaultAsync(b => b.Id == board.Id, cancellationToken)
                ?? throw new ApplicationException(Error.BoardNotFound);
            
            boardToUpdate.Name = board.Name;
            
            await _dataContext.SaveChangesAsync(cancellationToken);

            return boardToUpdate;
        }
    }
}
