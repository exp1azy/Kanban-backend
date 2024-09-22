using Kanban.Server.Controllers.Models;
using Kanban.Server.Extensions;
using Kanban.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Server.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController(IUserService userService) : Controller
    {
        private readonly IUserService _userService = userService;

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserAuthClientModel user, CancellationToken cancellationToken)
        {
            try
            {
                var userModel = await _userService.AuthenticateAsync(user.Email, user.Password, cancellationToken);
                var jwtToken = _userService.GenerateToken(userModel);

                return Ok(jwtToken);
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

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserRegisterClientModel user, CancellationToken cancellationToken)
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

        [Authorize]
        [HttpGet("users/me")]
        public IActionResult GetUser()
        {
            try
            {
                return Ok(HttpContext.GetCurrentUser());
            }
            catch (ApplicationException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("users/me")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateClientModel user, CancellationToken cancellationToken)
        {
            try
            {
                await _userService.UpdateUserAsync(user, cancellationToken);

                return Ok(user);
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
    }
}
