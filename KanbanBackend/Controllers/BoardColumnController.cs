using KanbanBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("kanban")]
    [Authorize]
    public class BoardColumnController : Controller
    {
        private readonly BoardColumnService _boardColumnService;

        public BoardColumnController(BoardColumnService boardColumnService)
        {
            _boardColumnService = boardColumnService;
        }

        [HttpGet("board-columns/{boardId}")]
        public async Task<IActionResult> AllColumns(int boardId, CancellationToken cancellationToken)
        {
            var columns = _boardColumnService.GetAllBoardsColumnAsync(boardId, cancellationToken);

            if (columns != null)
            {
                return Ok(columns);
            }

            return NoContent();
        }

        [HttpPost("add-column")]
        public async Task<IActionResult> AddBoardColumn(int boardId, string title, CancellationToken cancellationToken)
        {
            try
            {
                await _boardColumnService.AddBoardColumnAsync(boardId, title, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("update-column/{columnId}")]
        public async Task<IActionResult> UpdateBoard(int columnId, string title, CancellationToken cancellationToken)
        {
            try
            {
                await _boardColumnService.UpdateBoardColumnAsync(columnId, title, cancellationToken);
            }
            catch (ApplicationException e)
            {
                BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("delete-column/{columnId}")]
        public async Task<IActionResult> DeleteBoard(int columnId, CancellationToken cancellationToken)
        {
            try
            {
                await _boardColumnService.DeleteBoardColumnAsync(columnId, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }
    }
}
