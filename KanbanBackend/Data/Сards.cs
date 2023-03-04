using System.ComponentModel.DataAnnotations;

namespace KanbanBackend.Data
{
    public class сards
    {
        [Key]
        public int card_id { get; set; }

        public string card_name { get; set; }

        public string? card_text { get; set; }

        public DateOnly card_date { get; set;}
    }
}