using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;

namespace Kanban.Server.Repositories.Interfaces
{
    public interface ICardRepository
    {
        public Task AddCardAsync(CardClientCreateModel card, CancellationToken cancellationToken = default);

        public Task<Card?> UpdateCardNameAsync(CardClientUpdateNameModel card, CancellationToken cancellationToken = default);

        public Task<Card?> UpdateCardContentAsync(CardClientUpdateContentModel card, CancellationToken cancellationToken = default);

        public Task DeleteCardAsync(int id, CancellationToken cancellationToken = default);
    }
}
