using Kanban.Server.Controllers.Models;
using Kanban.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("kanban")]
    public class KanbanController(IKanbanService kanbanService) : Controller
    {
        private readonly IKanbanService _kanbanService = kanbanService;

        [HttpGet("board/{id}")]
        public async Task<IActionResult> GetBoard([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("board")]
        public async Task<IActionResult> CreateBoard([FromBody] BoardClientModel board, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPatch("board")]
        public async Task<IActionResult> UpdateBoardName([FromBody] string newBoardName, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete("board/{id}")]
        public async Task<IActionResult> DeleteBoard([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("column")]
        public async Task<IActionResult> CreateColumn([FromBody] ColumnClientCreateModel column, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPatch("column")]
        public async Task<IActionResult> UpdateColumn([FromBody] ColumnClientUpdateModel column, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete("column/{id}")]
        public async Task<IActionResult> DeleteColumn([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("card")]
        public async Task<IActionResult> CreateCard([FromBody] CardClientCreateModel card, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPatch("card")]
        public async Task<IActionResult> UpdateCard([FromBody] CardClientUpdateModel card, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete("card/{id}")]
        public async Task<IActionResult> DeleteCard([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
