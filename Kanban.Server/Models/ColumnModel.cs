using Kanban.Server.Data;

namespace Kanban.Server.Models
{
    public class ColumnModel
    {
        public int Id { get; set; }

        public int BoardId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public static ColumnModel? Map(Column column) => column == null ? null : new ColumnModel
        {
            Id = column.Id,
            BoardId = column.BoardId,
            Name = column.Name,
            Position = column.Position
        };
    }
}
