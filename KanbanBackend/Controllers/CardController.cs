using KanbanBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBackend.Controllers
{
    [ApiController]
    [Route("kanban")]
    [Authorize]
    public class CardController : Controller
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("column-cards/{columnId}")]
        public async Task<IActionResult> AllCards(int columnId, CancellationToken cancellationToken)
        {
            var cards = _cardService.GetAllColumnCardsAsync(columnId, cancellationToken);

            if (cards != null)
            {
                return Ok(cards);
            }

            return NoContent();
        }

        [HttpPost("add-card")]
        public async Task<IActionResult> AddCard(string name, int columnId, CancellationToken cancellationToken)
        {
            try
            {
                await _cardService.AddCardAsync(name, columnId, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("update-card-text/{cardId}")]
        public async Task<IActionResult> UpdateCardText(int cardId, string text, CancellationToken cancellationToken)
        {
            try
            {
                await _cardService.UpdateCardTextAsync(cardId, text, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("update-card-name/{cardId}")]
        public async Task<IActionResult> UpdateCardName(int cardId, string name, CancellationToken cancellationToken)
        {
            try
            {
                await _cardService.UpdateCardNameAsync(cardId, name, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("delete-card/{cardId}")]
        public async Task<IActionResult> DeleteCard(int cardId, CancellationToken cancellationToken)
        {
            try
            {
                await _cardService.DeleteCardAsync(cardId, cancellationToken);
            }
            catch (ApplicationException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }
    }
}
