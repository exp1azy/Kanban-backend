using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Models;

namespace Kanban.Server.Services.Interfaces
{
    public interface IKanbanService
    {
        public Task<IEnumerable<BoardModel>> GetAllUserBoardsAsync(int userId, CancellationToken cancellationToken = default);

        public Task<BoardExtendedModel> GetBoardAsync(int id, CancellationToken cancellationToken = default);

        public Task AddBoardAsync(BoardClientCreateModel board, CancellationToken cancellationToken = default);

        public Task<BoardModel?> UpdateBoardAsync(BoardClientUpdateModel board, CancellationToken cancellationToken = default);

        public Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default);

        public Task AddColumnAsync(ColumnClientCreateModel column, CancellationToken cancellationToken = default);

        public Task<ColumnModel?> UpdateColumnNameAsync(ColumnClientUpdateNameModel column, CancellationToken cancellationToken = default);

        public Task<ColumnModel?> UpdateColumnPositionAsync(ColumnClientUpdatePositionModel column, CancellationToken cancellationToken = default);

        public Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default);

        public Task AddCardAsync(CardClientCreateModel card, CancellationToken cancellationToken = default);

        public Task<CardModel?> UpdateCardNameAsync(CardClientUpdateNameModel card, CancellationToken cancellationToken = default);

        public Task<CardModel?> UpdateCardContentAsync(CardClientUpdateContentModel card, CancellationToken cancellationToken = default);

        public Task DeleteCardAsync(int id, CancellationToken cancellationToken = default);
    }
}
