using Kanban.Server.Models;
using System.Security.Claims;

namespace Kanban.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserDataModel GetCurrentUser(this HttpContext context)
        {
            var parsed = int.TryParse(context.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value, out var userId);
            if (!parsed)
                throw new ApplicationException();

            var userEmail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                ?? throw new ApplicationException();

            var userName = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value
                ?? throw new ApplicationException();

            return new UserDataModel
            {
                Id = userId,
                Name = userName,
                Email = userEmail
            };
        }
    }
}
