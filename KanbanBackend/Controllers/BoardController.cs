using KanbanBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KanbanBackend.Extensions;
using Microsoft.IdentityModel.Tokens;
using KanbanBackend.Data;

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

        [HttpGet("boards")]
        public async Task<IActionResult> AllUsersBoards(CancellationToken cancellationToken)
        {            
            var boards = _boardService.GetAllUsersBoardsAsync(HttpContext.GetUser().Id, cancellationToken);

            if (boards != null)
            {
                return Ok(boards);
            }

            return NoContent();
        }

        [HttpPost("add-board")]
        public async Task<IActionResult> AddNewBoard(string boardName, CancellationToken cancellationToken)
        {
            try
            {
               await _boardService.AddBoardAsync(HttpContext.GetUser().Id, boardName, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("update-board/{id}")]
        public async Task<IActionResult> UpdateBoard(int id, string name, CancellationToken cancellationToken)
        {
            try
            {
                await _boardService.UpdateBoardAsync(id, name, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("delete-board/{id}")]
        public async Task<IActionResult> DeleteBoard(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _boardService.DeleteBoardAsync(id, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }
    }
}
