using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class ColumnCreateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.ColumnNameIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.BoardIdIsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.BoardIdMustBeGreaterThanZero)]
        public int BoardId { get; set; }

        public int Position { get; set; }
    }
}
