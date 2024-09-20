using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class ColumnUpdateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.ColumnNameIsRequired)]
        public string Name { get; set; }

        public int Position { get; set; }
    } 
}
