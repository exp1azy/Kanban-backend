namespace Kanban.Server.Models
{
    public class ColumnFullModel
    {
        public int Id { get; set; }

        public int BoardId { get; set; }

        public string Name { get; set; }

        public int Position { get; set; }

        public List<CardModel> Cards { get; set; }
    }
}
