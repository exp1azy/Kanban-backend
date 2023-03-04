using KanbanBackend.Data;
using KanbanBackend.Models;
using KanbanBackend.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("register")]
        public async Task<IActionResult> AddUser(UserModel user, CancellationToken cancellationToken)
        {            
            return Ok(await _userService.AddUserAsync(user, cancellationToken));
        }
    }
}
