namespace Kanban.Server
{
    public class PasswordHashHelper
    {
        public static string HashPassword(string password) =>       
             BCrypt.Net.BCrypt.HashPassword(password);
        
        public static bool VerifyPassword(string password, string hashedPassword) =>       
             BCrypt.Net.BCrypt.Verify(password, hashedPassword);        
    }
}
