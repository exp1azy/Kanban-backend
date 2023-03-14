using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBackend.Data
{
    [Table("cards")]
    public class Card
    {
        [Key]
        [Column("card_id")]
        public int CardId { get; set; }

        [Column("card_name")]
        public string CardName { get; set; }

        [Column("card_text")]
        public string? CardText { get; set; }

        [Column("card_date")]
        public DateOnly CardDate { get; set;}
    }
}