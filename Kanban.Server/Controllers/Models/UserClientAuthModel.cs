using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class UserClientAuthModel
    {
        [Required(ErrorMessage = ErrorMessage.EmailIsRequired)]
        [EmailAddress(ErrorMessage = ErrorMessage.InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMessage.PasswordIsRequired)]
        public string Password { get; set; }
    }
}
