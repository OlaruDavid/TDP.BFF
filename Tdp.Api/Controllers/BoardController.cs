using Microsoft.AspNetCore.Mvc;
using Tdp.Application.Contracts.Application;
namespace Tdp.Api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _board;

        public BoardController(IBoardService board)
        {
            _board = board;
        }
    [HttpGet("{projectId}/board")]
    public async Task<IActionResult> GetBoard(Guid projectId)
    {
        var items = await _board.GetBoard(projectId);
        return Ok(items);
    }
    }
}