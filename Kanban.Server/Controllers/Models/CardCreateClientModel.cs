using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class CardCreateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.CardNameIsRequired)]
        public string Name { get; set; }

        public string? Content { get; set; }

        [Required(ErrorMessage = ErrorMessage.ColumnIdIsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.ColumnIdMustBeGreaterThanZero)]
        public int ColumnId { get; set; }
    }
}
