using Kanban.Server.Controllers.Models;
using Kanban.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Server.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserClientRegisterModel user, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.CreateUserAsync(user, cancellationToken);

                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody] UserClientAuthModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userModel = await _userService.AuthenticateAsync(user.Name, user.Password, cancellationToken);
                var jwtToken = _userService.GenerateToken(userModel.Name, userModel.Email);

                return Ok(jwtToken);
            }
            catch (ApplicationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
