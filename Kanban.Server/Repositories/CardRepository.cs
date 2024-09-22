using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Repositories.Interfaces;
using Kanban.Server.Resources;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Server.Repositories
{
    public class CardRepository(DataContext dataContext) : ICardRepository
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task AddCardAsync(CardCreateClientModel card, CancellationToken cancellationToken = default)
        {
            var cardToCreate = new Card
            {
                Name = card.Name,
                ColumnId = card.ColumnId,
                Content = card.Content
            };

            await _dataContext.Cards.AddAsync(cardToCreate, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCardAsync(int id, CancellationToken cancellationToken = default)
        {
            var cardToDelete = await _dataContext.Cards.FirstOrDefaultAsync(c => c.Id == id, cancellationToken)
                ?? throw new ApplicationException(Error.CardNotFound);
            
            _dataContext.Cards.Remove(cardToDelete);
            await _dataContext.SaveChangesAsync(cancellationToken);                    
        }

        public async Task<Card> UpdateCardAsync(CardUpdateClientModel card, CancellationToken cancellationToken = default)
        {
            var cardToUpdate = await _dataContext.Cards.FirstOrDefaultAsync(c => c.Id == card.Id, cancellationToken)
                ?? throw new ApplicationException(Error.CardNotFound);
            
            cardToUpdate.Name = card.Name;
            cardToUpdate.ColumnId = card.ColumnId;
            cardToUpdate.Content = card.Content;          

            await _dataContext.SaveChangesAsync(cancellationToken);

            return cardToUpdate;
        }
    }
}
