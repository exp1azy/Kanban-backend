namespace KanbanBackend.Models
{
    public class UserLoginModel
    {
        public int Id { get; set; }

        public string User_name { get; set; }

        public string User_password { get; set; }

        public string ErrorMessage { get; set; }
    }
}
