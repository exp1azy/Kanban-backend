using Kanban.Server.Data;

namespace Kanban.Server.Models
{
    public class CardModel
    {
        public int Id { get; set; }

        public int ColumnId { get; set; }

        public string Name { get; set; }

        public string? Content { get; set; }

        public static CardModel? Map(Card card) => card == null ? null : new CardModel
        {
            Id = card.Id,
            ColumnId = card.ColumnId,
            Name = card.Name,
            Content = card.Content
        };
    }
}
