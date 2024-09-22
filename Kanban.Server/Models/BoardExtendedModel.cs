using Kanban.Server.Data;

namespace Kanban.Server.Models
{
    public class BoardExtendedModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public List<ColumnExtendedModel> Columns { get; set; }

        public static BoardExtendedModel? Map(Board board) => board == null ? null : new BoardExtendedModel
        {
            Id = board.Id,
            UserId = board.UserId,
            Name = board.Name,
            Columns = board.Columns.Select(ColumnExtendedModel.Map).ToList()!
        };
    }
}
