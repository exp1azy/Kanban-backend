using Kanban.Server.Controllers.Models;
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
            return Ok();
        }

        [HttpGet("board/{id}")]
        public async Task<IActionResult> GetBoardData([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("card")]
        public async Task<IActionResult> CreateCard([FromBody] CardCreateUpdateClientModel cardModel, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPatch("card")]
        public async Task<IActionResult> UpdateCard([FromBody] CardCreateUpdateClientModel cardModel, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete("card/{id}")]
        public async Task<IActionResult> DeleteCard([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("board")]
        public async Task<IActionResult> CreateBoard([FromBody] BoardCreateClientModel boardModel, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPatch("board")]
        public async Task<IActionResult> UpdateBoard([FromBody] BoardUpdateClientModel boardModel, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete("board/{id}")]
        public async Task<IActionResult> DeleteBoard([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("column")]
        public async Task<IActionResult> CreateColumn([FromBody] ColumnCreateClientModel columnModel, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPatch("column")]
        public async Task<IActionResult> UpdateColumn([FromBody] ColumnUpdateClientModel columnModel, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete("column/{id}")]
        public async Task<IActionResult> DeleteColumn([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
