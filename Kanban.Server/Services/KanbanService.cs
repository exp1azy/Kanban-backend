using Kanban.Server.Controllers.Models;
using Kanban.Server.Models;
using Kanban.Server.Repositories.Interfaces;
using Kanban.Server.Resources;
using Kanban.Server.Services.Interfaces;

namespace Kanban.Server.Services
{
    public class KanbanService(
        IBoardRepository boardRepository,
        IColumnRepository columnRepository,
        ICardRepository cardRepository) : IKanbanService
    {
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly IColumnRepository _columnRepository = columnRepository;
        private readonly ICardRepository _cardRepository = cardRepository;

        public async Task AddBoardAsync(BoardCreateClientModel boardModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddCardAsync(CardCreateUpdateClientModel cardModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task AddColumnAsync(ColumnCreateClientModel columnModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCardAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BoardModel>> GetAllUserBoardsAsync(int userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BoardExtendedModel> GetBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<BoardModel?> UpdateBoardAsync(BoardUpdateClientModel boardModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<CardModel?> UpdateCardAsync(CardCreateUpdateClientModel cardModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ColumnModel?> UpdateColumnAsync(ColumnUpdateClientModel columnModel, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
