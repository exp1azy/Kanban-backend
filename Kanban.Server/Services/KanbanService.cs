using Kanban.Server.Controllers.Models;
using Kanban.Server.Models;
using Kanban.Server.Repositories.Interfaces;
using Kanban.Server.Resources;
using Kanban.Server.Services.Interfaces;

namespace Kanban.Server.Services
{
    public class KanbanService(IBoardRepository boardRepository, IColumnRepository columnRepository, ICardRepository cardRepository) : IKanbanService
    {
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly IColumnRepository _columnRepository = columnRepository;
        private readonly ICardRepository _cardRepository = cardRepository;

        public async Task AddBoardAsync(BoardCreateClientModel boardModel, CancellationToken cancellationToken = default)
        {
            await _boardRepository.AddBoardAsync(boardModel, cancellationToken);
        }

        public async Task AddCardAsync(CardCreateClientModel cardModel, CancellationToken cancellationToken = default)
        {
            await _cardRepository.AddCardAsync(cardModel, cancellationToken);
        }

        public async Task AddColumnAsync(ColumnCreateClientModel columnModel, CancellationToken cancellationToken = default)
        {
            await _columnRepository.AddColumnAsync(columnModel, cancellationToken);
        }

        public async Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _boardRepository.DeleteBoardAsync(id, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteCardAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cardRepository.DeleteCardAsync(id, cancellationToken);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _columnRepository.DeleteColumnAsync(id, cancellationToken);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BoardModel>> GetAllUserBoardsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var boards = await _boardRepository.GetAllUserBoardsAsync(userId, cancellationToken);

            return boards.Any() ? boards.Select(BoardModel.Map) : [];
        }

        public async Task<BoardExtendedModel> GetBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            var board = await _boardRepository.GetBoardAsync(id, cancellationToken)
                ?? throw new ApplicationException(Error.BoardNotFound);

            return BoardExtendedModel.Map(board)!;
        }

        public async Task<BoardModel> UpdateBoardAsync(BoardUpdateClientModel boardModel, CancellationToken cancellationToken = default)
        {
            try
            {
                var updatedBoard = await _boardRepository.UpdateBoardAsync(boardModel, cancellationToken);
                return BoardModel.Map(updatedBoard)!;
            }
            catch (ApplicationException) 
            { 
                throw; 
            }
        }

        public async Task<CardModel> UpdateCardAsync(CardUpdateClientModel cardModel, CancellationToken cancellationToken = default)
        {
            try
            {
                var updatedCard = await _cardRepository.UpdateCardAsync(cardModel, cancellationToken);
                return CardModel.Map(updatedCard)!;
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<ColumnModel> UpdateColumnAsync(ColumnUpdateClientModel columnModel, CancellationToken cancellationToken = default)
        {
            try
            {
                var updatedColumn = await _columnRepository.UpdateColumnAsync(columnModel, cancellationToken);
                return ColumnModel.Map(updatedColumn)!;
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }
}
