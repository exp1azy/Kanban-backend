using KanbanBackend.Models;
using System.Security.Claims;

namespace KanbanBackend.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserLoginModel GetUser(this HttpContext context)
        {
            var idClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var nameClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (!int.TryParse(idClaim?.Value, out var id) || nameClaim == null)
            {
                throw new UnauthorizedAccessException();
            }

            return new UserLoginModel
            {
                Id = id,
                User_name = nameClaim.Value
            };
        }
    }
}