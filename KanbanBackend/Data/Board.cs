using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBackend.Data
{
    [Table("boards")]
    public class Board
    {
        [Key]
        [Column("board_id")]
        public int BoardId { get; set; }

        [Column("board_name")]
        public string BoardName { get; set; }

        [Column("column_id")]
        public int? ColumnId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
    }
}