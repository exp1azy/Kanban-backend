using KanbanBackend.Data;
using KanbanBackend.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace KanbanBackend.Services
{
    public class CardService
    {
        private readonly DataContext _dataContext;

        public CardService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async IAsyncEnumerable<CardModel> GetAllColumnCardsAsync(int columnId, CancellationToken cancellationToken)
        {
            var allCards = _dataContext.Card.Where(i => i.ColumnId == columnId).AsAsyncEnumerable();

            await foreach (var card in allCards.WithCancellation(cancellationToken))
            {
                yield return CardModel.Map(card);
            }
        }

        public async Task AddCardAsync(string name, int id, CancellationToken cancellationToken)
        {
            var existColumn = await _dataContext.BoardColumn.FirstOrDefaultAsync(i => i.ColumnId == id, cancellationToken);

            if (existColumn != null && !string.IsNullOrEmpty(name))
            {
                var newCard = new Card()
                {
                    CardName = name,
                    CardDate = DateOnly.FromDateTime(DateTime.Now),
                    ColumnId = id
                };

                await _dataContext.Card.AddAsync(newCard, cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при создании карточки");
            }
        }

        public async Task UpdateCardTextAsync(int id, string text, CancellationToken cancellationToken)
        {
            var existCard = await _dataContext.Card.FirstOrDefaultAsync(i => i.CardId == id, cancellationToken);

            if (existCard != null && !string.IsNullOrEmpty(text))
            {
                await _dataContext.Card.Where(i => i.CardId == id).ExecuteUpdateAsync(s => s.SetProperty(c => c.CardText, c => text)
                    .SetProperty(c => c.CardDate, c => DateOnly.FromDateTime(DateTime.Now)), cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при обновлении карточки");
            }
        }

        public async Task UpdateCardNameAsync(int id, string name, CancellationToken cancellationToken)
        {
            var existCard = await _dataContext.Card.FirstOrDefaultAsync(i => i.CardId == id, cancellationToken);

            if (existCard != null && !string.IsNullOrEmpty(name))
            {
                await _dataContext.Card.Where(i => i.CardId == id).ExecuteUpdateAsync(s => s.SetProperty(c => c.CardName, c => name)
                .SetProperty(c => c.CardDate, c => DateOnly.FromDateTime(DateTime.Now)), cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при обновлении карточки");
            }
        }

        public async Task DeleteCardAsync(int id, CancellationToken cancellationToken)
        {
            var existCard = await _dataContext.Card.FirstOrDefaultAsync(i => i.CardId == id, cancellationToken);

            if (existCard != null)
            {
                await _dataContext.Card.Where(i => i.CardId == id).ExecuteDeleteAsync(cancellationToken);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new ApplicationException("Ошибка при удалении карточки");
            }
        }
    }
}
