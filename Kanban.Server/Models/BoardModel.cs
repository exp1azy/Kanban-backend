using Kanban.Server.Data;

namespace Kanban.Server.Models
{
    public class BoardModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public static BoardModel? Map(Board board) => board == null ? null : new BoardModel
        {
            Id = board.Id,
            Name = board.Name,
            Created = DateTime.Now,
            UserId = board.UserId
        };
    }
}
