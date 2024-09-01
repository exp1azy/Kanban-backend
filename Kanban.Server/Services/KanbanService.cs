using Kanban.Server.Controllers.Models;
using Kanban.Server.Models;
using Kanban.Server.Repositories.Interfaces;
using Kanban.Server.Resources;
using Kanban.Server.Services.Interfaces;

namespace Kanban.Server.Services
{
    public class KanbanService : IKanbanService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IColumnRepository _columnRepository;
        private readonly ICardRepository _cardRepository;

        public KanbanService(
            IBoardRepository boardRepository,
            IColumnRepository columnRepository,
            ICardRepository cardRepository)
        {
            _boardRepository = boardRepository;
            _columnRepository = columnRepository;
            _cardRepository = cardRepository;
        }

        public async Task<IEnumerable<BoardModel>> GetAllUserBoardsAsync(int userId, CancellationToken cancellationToken = default)
        {
            if (userId <= 0)
                throw new ApplicationException(Error.UserIdLessOrEqualZero);

            var boards = await _boardRepository.GetAllUserBoardsAsync(userId, cancellationToken);

            foreach (var board in boards)
                yield return BoardModel.Map(board);
        }

        public async Task<BoardExtendedModel> GetBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ApplicationException(Error.BoardIdLessOrEqualZero);

            var board = await _boardRepository.GetBoardAsync(id, cancellationToken);
            if (board == null)
                throw new ApplicationException(Error.BoardIsNull);

            return BoardExtendedModel.Map(board);
        }

        public async Task AddBoardAsync(BoardClientCreateModel board, CancellationToken cancellationToken = default)
        {
            if (board == null)
                throw new ApplicationException(Error.BoardCreateModelIsNull);
            if (board.UserId <= 0)
                throw new ApplicationException(Error.UserIdLessOrEqualZero);
            if (string.IsNullOrEmpty(board.Name))
                throw new ApplicationException(Error.BoardNameIsNullOrEmpty);

            await _boardRepository.AddBoardAsync(board, cancellationToken);
        }

        public async Task<BoardModel?> UpdateBoardAsync(BoardClientUpdateModel board, CancellationToken cancellationToken = default)
        {
            if (board == null)
                throw new ApplicationException(Error.BoardUpdateModelIsNull);
            if (board.Id <= 0)
                throw new ApplicationException(Error.BoardIdLessOrEqualZero);
            if (string.IsNullOrEmpty(board.Name))
                throw new ApplicationException(Error.BoardNameIsNullOrEmpty)

            var updatedBoard = await _boardRepository.UpdateBoardAsync(board, cancellationToken);
            return updatedBoard;
        }

        public async Task DeleteBoardAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ApplicationException(Error.BoardIdLessOrEqualZero);

            await _boardRepository.DeleteBoardAsync(id, cancellationToken);
        }

        public async Task AddColumnAsync(ColumnClientCreateModel column, CancellationToken cancellationToken = default)
        {
            if (column == null)
                throw new ApplicationException(Error.ColumnCreateModelIsNull);
            if (string.IsNullOrEmpty(column.Name))
                throw new ApplicationException(Error.ColumnNameIsNullOrEmpty);
            if (column.Position < 0)
                throw new ApplicationException(Error.ColumnPositionLessThanZero);

            await _columnRepository.AddColumnAsync(column, cancellationToken);
        }

        public async Task<ColumnModel?> UpdateColumnNameAsync(ColumnClientUpdateNameModel column, CancellationToken cancellationToken = default)
        {
            if (column == null)
                throw new ApplicationException(Error.ColumnUpdateModelIsNull);
            if (column.Id <= 0)
                throw new ApplicationException(Error.ColumnIdLessOrEqualZero);
            if (string.IsNullOrEmpty(column.Name))
                throw new ApplicationException(Error.ColumnNameIsNullOrEmpty);

            var updatedColumn = await _columnRepository.UpdateColumnNameAsync(column, cancellationToken);
            return updatedColumn;
        }

        public async Task<ColumnModel?> UpdateColumnPositionAsync(ColumnClientUpdatePositionModel column, CancellationToken cancellationToken = default)
        {
            if (column == null)
                throw new ApplicationException(Error.ColumnUpdateModelIsNull);
            if (column.Id <= 0)
                throw new ApplicationException(Error.ColumnIdLessOrEqualZero);
            if (column.Position < 0)
                throw new ApplicationException(Error.ColumnPositionLessThanZero);

            var updatedColumn = await _columnRepository.UpdateColumnPositionAsync(column, cancellationToken);
            return updatedColumn;
        }

        public async Task DeleteColumnAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ApplicationException(Error.ColumnIdLessOrEqualZero);

            await _columnRepository.DeleteColumnAsync(id, cancellationToken);
        }

        public Task AddCardAsync(CardClientCreateModel card, CancellationToken cancellationToken = default)
        {

        }

        public Task<CardModel?> UpdateCardNameAsync(CardClientUpdateNameModel card, CancellationToken cancellationToken = default)
        {

        }

        public Task<CardModel?> UpdateCardContentAsync(CardClientUpdateContentModel card, CancellationToken cancellationToken = default)
        {

        }

        public Task DeleteCardAsync(int id, CancellationToken cancellationToken = default)
        {

        }
    }
}
