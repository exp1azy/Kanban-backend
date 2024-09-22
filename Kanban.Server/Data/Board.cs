using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kanban.Server.Data
{
    [Table("kanban_board")]
    public class Board
    {
        [Key][Column("id")] public int Id { get; set; }

        [Column("user_id")] public int UserId { get; set; }

        [Column("name")] public string Name { get; set; }

        public ICollection<Column> Columns { get; set; }
    }
}