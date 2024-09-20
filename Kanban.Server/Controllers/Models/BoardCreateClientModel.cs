using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class BoardCreateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.BoardNameIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.UserIdIsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.UserIdMustBeGreaterThanZero)]
        public int UserId { get; set; }
    }
}
