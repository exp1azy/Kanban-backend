using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class BoardUpdateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.BoardIdMustBeGreaterThanZero)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.UserIdMustBeGreaterThanZero)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessage.BoardNameIsRequired)]
        public string Name { get; set; }
    }
}
