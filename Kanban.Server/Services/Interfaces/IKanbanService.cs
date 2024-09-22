using Kanban.Server.Controllers.Models;
using Kanban.Server.Models;

namespace Kanban.Server.Services.Interfaces
{
    public interface IKanbanService
    {
        public Task<IEnumerable<BoardModel>> GetAllUserBoardsAsync(int userId, CancellationToken cancellationToken = default);

        public Task<BoardExtendedModel> GetBoardAsync(int id, CancellationToken cancellationToken = default);

        public Task AddBoardAsync(int userId, BoardCreateClientModel boardModel, CancellationToken cancellationToken = default);

        public Task<BoardModel> UpdateBoardAsync(BoardUpdateClientModel boardModel, CancellationToken cancellationToken = default);

        public Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default);

        public Task AddColumnAsync(ColumnCreateClientModel columnModel, CancellationToken cancellationToken = default);

        public Task<ColumnModel> UpdateColumnAsync(ColumnUpdateClientModel columnModel, CancellationToken cancellationToken = default);

        public Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default);

        public Task AddCardAsync(CardCreateClientModel cardModel, CancellationToken cancellationToken = default);

        public Task<CardModel> UpdateCardAsync(CardUpdateClientModel cardModel, CancellationToken cancellationToken = default);

        public Task DeleteCardAsync(int id, CancellationToken cancellationToken = default);
    }
}
