using Kanban.Server.Data;

namespace Kanban.Server.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public static UserModel? Map(User user) => user == null ? null : new UserModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }
}
