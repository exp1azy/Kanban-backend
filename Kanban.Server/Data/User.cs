using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kanban.Server.Data
{
    [Table("kanban_user")]
    public class User
    {
        [Key][Column("id")] public int Id { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("email")] public string Email { get; set; }

        [Column("password")] public string Password { get; set; }
    }
}