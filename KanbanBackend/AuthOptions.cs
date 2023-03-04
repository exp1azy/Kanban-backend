using Microsoft.IdentityModel.Tokens;
using System.Text;

public class AuthOptions
{
    public const string ISSUER = "AuthServer";

    public const string AUDIENCE = "AuthClient";

    const string KEY = "kanban_dashboard_key123";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}