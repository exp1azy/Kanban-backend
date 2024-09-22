using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class UserRegisterClientModel
    {
        [Required(ErrorMessage = ErrorMessage.UserNameIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.EmailIsRequired)]
        [EmailAddress(ErrorMessage = ErrorMessage.InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessage.PasswordIsRequired)]
        public string Password { get; set; }
    }
}
