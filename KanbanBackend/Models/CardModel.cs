using KanbanBackend.Data;

namespace KanbanBackend.Models
{
    public class CardModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Text { get; set; }

        public DateOnly Date { get; set; }

        public int ColumnId { get; set; }

        public static CardModel? Map(Card card) => card == null ? null : new CardModel()
        {
            Id = card.CardId,
            Name = card.CardName,
            Text = card.CardText,
            Date = card.CardDate,
            ColumnId = card.ColumnId
        };
    }
}
