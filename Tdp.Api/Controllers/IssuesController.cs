using Microsoft.AspNetCore.Mvc;
using Tdp.Application.Contracts.Application;
using Tdp.Domain.Dtos.Issues;

namespace Tdp.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class IssuesController : ControllerBase
    {
        private readonly IIssuesService _issues;
        private readonly ILogger<IssuesController> logger;
        public IssuesController(IIssuesService issues, ILogger<IssuesController> logger)
        {
            _issues = issues;
            this.logger = logger;
        }

        [HttpPost("issues")]
        public async Task<IActionResult> Create(CreateIssueRequest request)
        {
            var issue = await _issues.Create(request);

            if (issue == null)
                return Forbid();

             return Ok(issue);
        }

        [HttpGet("projects/{projectId}/issues")]
        public async Task<IActionResult> GetForProjectView(Guid projectId)
        {
            try
            {
                var items = await _issues.GetForProjectView(projectId);
                return Ok(items);
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                return StatusCode(500);
            }
           
        }

        [HttpDelete("issues/{issueId}")]
        public async Task<IActionResult> DeleteIssue(
            Guid issueId,
            [FromBody] DeleteIssueRequest request
        )
        {
            var ok = await _issues.DeleteIssue(
                issueId,
                request.UserId
            );

            if (!ok)
                return StatusCode(403);

            return NoContent();
        }
    }
}
