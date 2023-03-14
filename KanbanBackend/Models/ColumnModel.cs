using KanbanBackend.Data;

namespace KanbanBackend.Models
{
    public class ColumnModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int? CardId { get; set; }

        public int BoardId { get; set; }

        public static ColumnModel? Map(BoardColumn boardColumn) => boardColumn == null ? null : new ColumnModel()
        {
            Id = boardColumn.ColumnId,
            Title = boardColumn.ColumnTitle,
            CardId = boardColumn.CardId,
            BoardId = boardColumn.BoardId
        };
    }
}
