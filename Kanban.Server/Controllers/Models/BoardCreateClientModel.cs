using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class BoardCreateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.BoardNameIsRequired)]
        public string Name { get; set; }
    }
}
