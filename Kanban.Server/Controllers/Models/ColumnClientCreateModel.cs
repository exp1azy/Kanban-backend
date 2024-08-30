namespace Kanban.Server.Controllers.Models
{
    public class ColumnClientCreateModel
    {
        public int BoardId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }
    }
}
