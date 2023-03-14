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

        public async IAsyncEnumerable<GetBoardModel> GetAllUsersBoardsAsync(int id, CancellationToken cancellationToken)
        {
            var allBoards = _dataContext.Board.Where(i => i.UserId == id).AsAsyncEnumerable();

            await foreach (var board in allBoards.WithCancellation(cancellationToken))
            {
                yield return GetBoardModel.Map(board);
            }
        }
    }
}
