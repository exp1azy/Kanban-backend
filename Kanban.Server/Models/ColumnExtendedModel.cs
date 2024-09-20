using Kanban.Server.Data;

namespace Kanban.Server.Models
{
    public class ColumnExtendedModel
    {
        public int Id { get; set; }

        public int BoardId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public List<CardModel> Cards { get; set; }

        public static ColumnExtendedModel? Map(Column column) => column == null ? null : new ColumnExtendedModel
        {
            Id = column.Id,
            BoardId = column.BoardId,
            Name = column.Name,
            Position = column.Position,
            Cards = column.Cards.Select(CardModel.Map).ToList()!
        };
    }
}
