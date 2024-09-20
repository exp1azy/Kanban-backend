using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class BoardUpdateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.BoardNameIsRequired)]
        public string Name { get; set; }
    }
}
