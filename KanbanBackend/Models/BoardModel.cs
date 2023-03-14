using KanbanBackend.Data;

namespace KanbanBackend.Models
{
    public class BoardModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static BoardModel? Map(Board board) => board == null ? null : new BoardModel()
        {
            Id = board.BoardId,
            Name = board.BoardName
        };
    }
}
