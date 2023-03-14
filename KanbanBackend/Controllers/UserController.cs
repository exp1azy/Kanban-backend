using KanbanBackend.Models;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            try
            {
                var response = await _userService.AuthenticateUserAsync(username, password, cancellationToken);
                var token = _userService.GenerateToken(response);

                return Ok(token);
            }
            catch (ApplicationException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel user, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.AddUserAsync(user, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }     
    }
}
