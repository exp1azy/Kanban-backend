using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class UserUpdateClientModel
    {
        [Required(ErrorMessage = ErrorMessage.UserIdIsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.UserIdMustBeGreaterThanZero)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessage.UserNameIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.EmailIsRequired)]
        [EmailAddress(ErrorMessage = ErrorMessage.InvalidEmail)]
        public string Email { get; set; }
    }
}
