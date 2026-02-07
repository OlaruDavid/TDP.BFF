using Microsoft.AspNetCore.Mvc;
using Tdp.Domain.Dtos.Subtasks;
using Tdp.Application.Contracts.Application;
using Tdp.Domain.Dtos.Board;
namespace Tdp.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class SubtasksController : ControllerBase
    {
        private readonly ISubtaskService _subtasks;

        public SubtasksController(ISubtaskService subtasks)
        {
            _subtasks = subtasks;
        }


        [HttpGet("tasks/{taskId}/subtasks")]
        public async Task<IActionResult> GetForTask(Guid taskId)
        {
            var items = await _subtasks.GetForTask(taskId);
            return Ok(items);
        }

        [HttpPost("subtasks")]
        public async Task<IActionResult> Create(CreateSubtaskRequest request)
        {
            var result = await _subtasks.Create(request);
            return Ok(result);
        }

        [HttpPatch("subtasks/{id}/move")]
        public async Task<IActionResult> Move(Guid id, MoveSubtaskRequest request)
        {
            await _subtasks.Move(id, request);
            return NoContent();
        }

        [HttpPatch("subtasks/{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSubtaskRequest request)
        {
            var ok = await _subtasks.Update(id, request);

            if (!ok)
                return Forbid(); 

            return NoContent();
        }
        [HttpDelete("{subtaskId}")]
        public async Task<IActionResult> DeleteSubtask(
            Guid subtaskId,
            [FromBody] DeleteSubtaskRequest request
        )
        {
            var ok = await _subtasks.DeleteSubtask(
                subtaskId,
                request.UserId
            );

            if (!ok)
                return StatusCode(403);

            return NoContent();
        }
    }

}