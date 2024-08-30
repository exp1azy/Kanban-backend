namespace Kanban.Server.Models
{
    public class BoardFullModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public List<ColumnFullModel> Columns { get; set; }
    }
}
