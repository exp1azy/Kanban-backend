using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class ColumnUpdateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.ColumnIdIsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.ColumnIdMustBeGreaterThanZero)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessage.ColumnNameIsRequired)]
        public string Name { get; set; }

        public int Position { get; set; }
    } 
}
