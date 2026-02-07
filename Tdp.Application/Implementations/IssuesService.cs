using Tdp.Application.Contracts.Application;
using Tdp.Application.Contracts.Persistence;
using Tdp.Domain.Dtos.Issues;
using Tdp.Domain.Models;


namespace Tdp.Application.Implementations
{
    internal class IssuesService : IIssuesService
    {
        private readonly IIssuesRepository _issues;
        private readonly IProjectRepository _projects;

        public IssuesService(IIssuesRepository issues, IProjectRepository projects)
        {
            _issues = issues;
            _projects = projects;
        }

        public async Task<Issue?> Create(CreateIssueRequest request)
        {
            var role = await _projects.GetUserRole(
                request.UserId,
                request.ProjectId
            );

            if (role != "owner")
                return null; 

            var issue = new Issue
            {
                ProjectId = request.ProjectId,
                TaskId = request.TaskId,
                SubtaskId = request.SubtaskId,
                Title = request.Title,
                Description = request.Description,
                CreatedBy = request.UserId
            };

            return await _issues.Create(issue);
        }


        public async Task<IEnumerable<IssueViewDto>> GetForProjectView(Guid projectId)
        {
            return await _issues.GetForProjectView(projectId);
        }

        public async Task<bool> DeleteIssue(
            Guid issueId,
            Guid actorUserId
        )
        {
            var issue = await _issues.GetIssueById(issueId);
            if (issue == null)
                throw new Exception("Issue not found");

            var role = await _projects.GetUserRole(
                actorUserId,
                issue.ProjectId
            );

            if (role != "owner")
                return false;

            await _issues.DeleteIssue(issueId);

            return true;
        }


    }
}
