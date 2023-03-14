using KanbanBackend.Data;

namespace KanbanBackend.Models
{
    public class GetBoardModel
    {
        public int BoardId { get; set; }

        public string BoardName { get; set; }

        public static GetBoardModel? Map(Board board) => board == null ? null : new GetBoardModel
        {
            BoardId = board.BoardId,
            BoardName = board.BoardName
        };
    }
}
