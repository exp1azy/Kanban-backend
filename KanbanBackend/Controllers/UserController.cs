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
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
       
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password, CancellationToken cancellationToken)
        {
            var response = await _userService.AuthenticateUserAsync(username, password, cancellationToken);

            if (response.ErrorMessage.IsNullOrEmpty())
            {
                var token = _userService.GenerateToken(response);
                return Ok(token);
            }

            return Unauthorized(response.ErrorMessage);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel user, CancellationToken cancellationToken)
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
