namespace Kanban.Server.Controllers.Models
{
    public class CardClientCreateModel
    {
        public int ColumnId { get; set; }

        public string Name { get; set; }

        public string? Content { get; set; }
    }
}
