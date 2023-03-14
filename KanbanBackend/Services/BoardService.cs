using KanbanBackend.Data;
using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Services
{
    public class BoardService
    {
        private readonly DataContext _dataContext;

        public BoardService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async IAsyncEnumerable<BoardModel> GetAllUsersBoardsAsync(int id, CancellationToken cancellationToken)
        {
            var allBoards = _dataContext.Board.Where(i => i.UserId == id).AsAsyncEnumerable();

            await foreach (var board in allBoards.WithCancellation(cancellationToken))
            {
                yield return BoardModel.Map(board);
            }
        }

        public async Task AddBoardAsync(int id, string name, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var newBoard = new Board()
                {
                    BoardName = name,
                    UserId = id
                };

                await _dataContext.Board.AddAsync(newBoard, cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при создании доски");
            }
        }

        public async Task UpdateBoardAsync(int id, string name, CancellationToken cancellationToken)
        {
            var existBoard = await _dataContext.Board.FirstOrDefaultAsync(i => i.BoardId == id, cancellationToken);

            if (existBoard != null && !string.IsNullOrEmpty(name))
            {
                await _dataContext.Board.Where(i => i.BoardId == id).ExecuteUpdateAsync(s => s.SetProperty(c => c.BoardName, c => name), cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при обновлении доски");
            }
        }

        public async Task DeleteBoardAsync(int id, CancellationToken cancellationToken)
        {
            var existBoard = await _dataContext.Board.FirstOrDefaultAsync(i => i.BoardId == id, cancellationToken);

            if (existBoard != null)
            {
                await _dataContext.Board.Where(i => i.BoardId == id).ExecuteDeleteAsync(cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при удалении доски");
            }
        }
    }
}
