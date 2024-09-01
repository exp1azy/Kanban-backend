namespace Kanban.Server.Models
{
    public class ColumnModel
    {
        public int Id { get; set; }

        public int BoardId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }
    }
}
