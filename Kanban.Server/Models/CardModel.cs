namespace Kanban.Server.Models
{
    public class CardModel
    {
        public int Id { get; set; }

        public int ColumnId { get; set; }

        public string Name { get; set; }

        public string? Content { get; set; }
    }
}
