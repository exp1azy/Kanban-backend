using System.ComponentModel.DataAnnotations;

namespace KanbanBackend.Data
{
    public class users
    {
        [Key]
        public int user_id { get; set; }

        public string user_name { get; set; }

        public string user_email { get; set; }

        public string user_password { get; set; }
    }
}