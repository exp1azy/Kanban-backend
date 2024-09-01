using Kanban.Server.Data;

namespace Kanban.Server.Models
{
    public class ColumnFullModel
    {
        public int Id { get; set; }

        public int BoardId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public List<CardModel> Cards { get; set; }

        public static ColumnFullModel? Map(Column column) => column == null ? null : new ColumnModel
        {
            Id = column.Id,
            BoardId = column.BoardId,
            Name = column.Name,
            Position = column.Position,
            Cards = new List<CardModel>().AddRange(column.Cards.Select(CardModel.Map))
        };
    }
}
