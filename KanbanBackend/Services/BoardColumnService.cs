using KanbanBackend.Data;
using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBackend.Services
{
    public class BoardColumnService
    {
        private readonly DataContext _dataContext;

        public BoardColumnService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async IAsyncEnumerable<ColumnModel> GetAllBoardsColumnAsync(int boardId, CancellationToken cancellationToken)
        {
            var allColumns = _dataContext.BoardColumn.Where(i => i.BoardId == boardId).AsAsyncEnumerable();

            await foreach (var column in allColumns.WithCancellation(cancellationToken))
            {
                yield return ColumnModel.Map(column);
            }
        }

        public async Task AddBoardColumnAsync(int boardId, string title, CancellationToken cancellationToken)
        {
            var existBoard = await _dataContext.Board.FirstOrDefaultAsync(i => i.BoardId == boardId, cancellationToken);

            if (existBoard != null && !string.IsNullOrEmpty(title))
            {
                var newColumn = new BoardColumn()
                {
                    BoardId = boardId,
                    ColumnTitle = title
                };

                await _dataContext.BoardColumn.AddAsync(newColumn, cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("Ошибка при создании колонки");
            }
        }

        public async Task UpdateBoardColumnAsync(int id, string title, CancellationToken cancellationToken)
        {
            var existColumn = await _dataContext.BoardColumn.FirstOrDefaultAsync(i => i.ColumnId == id, cancellationToken);

            if (existColumn != null && !string.IsNullOrEmpty(title))
            {
                await _dataContext.BoardColumn.Where(i => i.ColumnId == id).ExecuteUpdateAsync(s => s.SetProperty(c => c.ColumnTitle, c => title), cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при обновлении колонки");
            }
        }

        public async Task DeleteBoardColumnAsync(int id, CancellationToken cancellationToken)
        {
            var existColumn = await _dataContext.BoardColumn.FirstOrDefaultAsync(i => i.ColumnId == id, cancellationToken);

            if (existColumn != null)
            {
                await _dataContext.BoardColumn.Where(i => i.ColumnId == id).ExecuteDeleteAsync(cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при удалении колонки");
            }
        }
    }
}