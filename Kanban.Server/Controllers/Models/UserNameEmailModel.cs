using System.ComponentModel.DataAnnotations;

namespace Kanban.Server.Controllers.Models
{
    public class UserNameEmailModel
    {
        [Required(ErrorMessage = ErrorMessage.UserNameIsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.EmailIsRequired)]
        [EmailAddress(ErrorMessage = ErrorMessage.InvalidEmail)]
        public string Email { get; set; }
    }
}
