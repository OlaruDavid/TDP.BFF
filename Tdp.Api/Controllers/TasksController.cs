using Microsoft.AspNetCore.Mvc;
using Tdp.Domain.Dtos.Tasks;
using Tdp.Application.Contracts.Application;

namespace Tdp.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _tasks;

        public TasksController(ITaskService tasks)
        {
            _tasks = tasks;
        }

        [HttpGet("/api/projects/{projectId}/tasks")]
        public async Task<IActionResult> GetForProject(Guid projectId)
        {
            var items = await _tasks.GetForProject(projectId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskRequest request)
        {
            var result = await _tasks.Create(request);
            return Ok(result);
        }

       [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskRequest request)
        {
            var ok = await _tasks.Update(id, request);

            if (!ok)
                return Forbid(); 

            return NoContent(); 
        }

       [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(
            Guid taskId,
            [FromBody] DeleteTaskRequest request
        )
        {
            if (request == null || request.UserId == Guid.Empty)
                return BadRequest();

            var ok = await _tasks.DeleteTask(taskId, request.UserId);

            if (!ok)
                return StatusCode(StatusCodes.Status403Forbidden);

            return NoContent();
        }  
    }

}