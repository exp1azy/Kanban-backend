using Kanban.Server.Controllers.Models;
using Kanban.Server.Extensions;
using Kanban.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class KanbanController(IKanbanService kanbanService) : Controller
    {
        private readonly IKanbanService _kanbanService = kanbanService;

        [HttpGet("boards")]
        public async Task<IActionResult> UserBoards(CancellationToken cancellationToken)
        {
            try
            {
                var boards = await _kanbanService.GetAllUserBoardsAsync(HttpContext.GetCurrentUser().Id, cancellationToken);

                return Ok(boards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("board/{id}")]
        public async Task<IActionResult> GetBoardData([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                var board = await _kanbanService.GetBoardAsync(id, cancellationToken);

                return Ok(board);
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

        [HttpPost("card")]
        public async Task<IActionResult> CreateCard([FromBody] CardCreateClientModel cardModel, CancellationToken cancellationToken)
        {
            try
            {
                await _kanbanService.AddCardAsync(cardModel, cancellationToken);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("card")]
        public async Task<IActionResult> UpdateCard([FromBody] CardUpdateClientModel cardModel, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCard = await _kanbanService.UpdateCardAsync(cardModel, cancellationToken);

                return Ok(updatedCard);
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

        [HttpDelete("card/{id}")]
        public async Task<IActionResult> DeleteCard([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                await _kanbanService.DeleteCardAsync(id, cancellationToken);

                return Ok("Карточка удалена.");
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

        [HttpPost("board")]
        public async Task<IActionResult> CreateBoard([FromBody] BoardCreateClientModel boardModel, CancellationToken cancellationToken)
        {
            try
            {
                await _kanbanService.AddBoardAsync(HttpContext.GetCurrentUser().Id, boardModel, cancellationToken);

                return Ok();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("board")]
        public async Task<IActionResult> UpdateBoard([FromBody] BoardUpdateClientModel boardModel, CancellationToken cancellationToken)
        {
            try
            {
                var updatedBoard = await _kanbanService.UpdateBoardAsync(boardModel, cancellationToken);

                return Ok(updatedBoard);
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

        [HttpDelete("board/{id}")]
        public async Task<IActionResult> DeleteBoard([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                await _kanbanService.DeleteBoardAsync(id, cancellationToken);

                return Ok("Доска удалена.");
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

        [HttpPost("column")]
        public async Task<IActionResult> CreateColumn([FromBody] ColumnCreateClientModel columnModel, CancellationToken cancellationToken)
        {
            try
            {
                await _kanbanService.AddColumnAsync(columnModel, cancellationToken);

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

        [HttpPatch("column")]
        public async Task<IActionResult> UpdateColumn([FromBody] ColumnUpdateClientModel columnModel, CancellationToken cancellationToken)
        {
            try
            {
                var updatedColumn = await _kanbanService.UpdateColumnAsync(columnModel, cancellationToken);

                return Ok(updatedColumn);
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

        [HttpDelete("column/{id}")]
        public async Task<IActionResult> DeleteColumn([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                await _kanbanService.DeleteColumnAsync(id, cancellationToken);

                return Ok("Колонка удалена.");
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
