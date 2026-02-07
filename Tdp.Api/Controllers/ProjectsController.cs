using Microsoft.AspNetCore.Mvc;
using Tdp.Application.Contracts.Application;
using Tdp.Domain.Dtos.Members;
using Tdp.Domain.Dtos.Projects;

namespace Tdp.Api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projects;

        public ProjectsController(IProjectService projects)
        {
            _projects = projects;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProjects([FromQuery] Guid userId)
        {
            var result = await _projects.GetForUser(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectRequest request)
        {
            var result = await _projects.Create(request.UserId, request);
            return Ok(result);
        }
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug, [FromQuery] Guid userId)
        {
            var project = await _projects.GetBySlug(userId, slug);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost("{projectId}/members")]
        public async Task<IActionResult> AddMember(
            Guid projectId,
            AddProjectMemberRequest request
        )
        {
            if (!Request.Headers.TryGetValue("X-User-Id", out var userIdHeader))
                return BadRequest("Missing user");

            var actorUserId = Guid.Parse(userIdHeader);

            var ok = await _projects.AddMember(projectId, actorUserId, request);

            if (!ok)
                return StatusCode(403);

            return NoContent();
        }

        [HttpGet("{projectId}/members")]
        public async Task<IActionResult> GetMembers(Guid projectId)
        {
            var items = await _projects.GetMembers(projectId);
            return Ok(items);
        }

        [HttpDelete("{memberId}")]
        public async Task<IActionResult> RemoveMember(
            Guid memberId,
            [FromBody] DeleteProjectMember request
        )
        {
            var ok = await _projects.RemoveMember(memberId, request.UserId);
            if (!ok)
                return StatusCode(403);
            return NoContent();
        }

        [HttpDelete("delete/{projectId}")]
        public async Task<IActionResult> DeleteProject(
            Guid projectId,
            [FromBody] DeleteProjectRequest request
        )
        {
            var ok = await _projects.DeleteProject(
                projectId,
                request.UserId
            );

            if (!ok)
                return StatusCode(403);

            return NoContent();
        }
    }
}
