using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface ICardRepository
    {
        public Task<IEnumerable<Card>> GetAllCardsAsync(int columnId, CancellationToken cancellationToken = default);

        public Task AddCardAsync(CardClientCreateModel card, CancellationToken cancellationToken = default);

        public Task<Card?> UpdateCardAsync(CardClientUpdateModel card, CancellationToken cancellationToken = default);

        public Task DeleteCardAsync(int id, CancellationToken cancellationToken = default);
    }
}
