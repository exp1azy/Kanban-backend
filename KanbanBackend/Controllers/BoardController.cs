using KanbanBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanbanBackend.Extensions;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("kanban")]
    [Authorize]
    public class BoardController : Controller
    {
        private readonly BoardService _boardService;

        public BoardController(BoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("users-boards")]
        public async Task<IActionResult> AllUsersBoards(CancellationToken cancellationToken)
        {            
            var boards = _boardService.GetAllUsersBoardsAsync(HttpContext.GetUser().Id, cancellationToken);

            return Ok(boards);
        }

    }
}
