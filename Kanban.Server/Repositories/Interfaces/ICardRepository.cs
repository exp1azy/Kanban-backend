using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface ICardRepository
    {
        public Task AddCardAsync(CardCreateClientModel card, CancellationToken cancellationToken = default);

        public Task<Card> UpdateCardAsync(CardUpdateClientModel card, CancellationToken cancellationToken = default);

        public Task DeleteCardAsync(int id, CancellationToken cancellationToken = default);
    }
}
