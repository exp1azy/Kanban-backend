using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class CardCreateUpdateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.CardNameIsRequired)]
        public string Name { get; set; }

        public string? Content { get; set; }

        [Required(ErrorMessage = ErrorMessage.ColumnIdIsRequired)]
        [Range(1, int.MaxValue)]
        public int ColumnId { get; set; }
    }
}
