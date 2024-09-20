using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kanban.Server.Data
{
    [Table("board_card")]
    public class Card
    {
        [Key][Column("id")] public int Id { get; set; }

        [Column("column_id")] public int ColumnId { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("content")] public string? Content { get; set; }
    }
}