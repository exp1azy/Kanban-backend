using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class CardUpdateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.CardIdIsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.CardIdMustBeGreaterThanZero)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessage.CardNameIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.ColumnIdIsRequired)]
        [Range(1, int.MaxValue)]
        public int ColumnId { get; set; }

        public string? Content { get; set; }
    }
}
