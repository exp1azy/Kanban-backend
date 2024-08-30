using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Data
{
    [Table("board_column")]
    public class Column
    {
        [Key][Column("id")] public int Id { get; set; }

        [Column("board_id")] public int BoardId { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("position")] public int Position { get; set; }
    }
}