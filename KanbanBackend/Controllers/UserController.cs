using KanbanBackend.Data;
using KanbanBackend.Models;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("kanban")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password, CancellationToken cancellationToken)
        {
            var user = await _userService.AuthenticateUserAsync(username, password, cancellationToken);

            if (user.ErrorMessage.IsNullOrEmpty())
            {
                var token = _userService.GenerateToken(user);
                return Ok(token);
            }

            return Unauthorized(user.ErrorMessage);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Add(UserModel user, CancellationToken cancellationToken)
        {
            var response = await _userService.AddUserAsync(user, cancellationToken);

            if (!response.ErrorMessage.IsNullOrEmpty())
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response);
        }     
    }
}
