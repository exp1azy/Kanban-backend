using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBackend.Data
{
    [Table("board_columns")]
    public class BoardColumn
    {
        [Key]
        [Column("column_id")]
        public int ColumnId { get; set; }

        [Column("column_title")]
        public string ColumnTitle { get; set; }

        [Column("card_id")]
        public int? CardId { get; set; }

        [Column("board_id")]
        public int BoardId { get; set; }
    }
}