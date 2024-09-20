using Kanban.Server.Controllers.Models;
using Kanban.Server.Data;
using Kanban.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Server.Repositories
{
    public class CardRepository(DataContext dataContext) : ICardRepository
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task AddCardAsync(CardClientCreateModel card, CancellationToken cancellationToken = default)
        {
            await _dataContext.Cards.AddAsync(new Card
            {
                ColumnId = card.ColumnId,
                Name = card.Name,
                Content = card.Content
            }, cancellationToken);

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Card?> UpdateCardNameAsync(CardClientUpdateNameModel card, CancellationToken cancellationToken = default)
        {
            var cardToUpdate = await _dataContext.Cards.FirstOrDefaultAsync(c => c.Id == card.Id, cancellationToken);
            if (cardToUpdate != null)
            {
                cardToUpdate.Name = card.Name;
                await _dataContext.SaveChangesAsync(cancellationToken);
            }            

            return cardToUpdate;
        }

        public async Task<Card?> UpdateCardContentAsync(CardClientUpdateContentModel card, CancellationToken cancellationToken = default)
        {
            var cardToUpdate = await _dataContext.Cards.FirstOrDefaultAsync(c => c.Id == card.Id, cancellationToken);
            if (cardToUpdate != null)
            {
                cardToUpdate.Content = card.Content;
                await _dataContext.SaveChangesAsync(cancellationToken);
            }        

            return cardToUpdate;
        }

        public async Task DeleteCardAsync(int id, CancellationToken cancellationToken = default)
        {
            var cardToDelete = await _dataContext.Cards.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (cardToDelete != null)
            {
                _dataContext.Cards.Remove(cardToDelete);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }            
        }
    }
}
