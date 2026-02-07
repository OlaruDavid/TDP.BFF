using Tdp.Domain.Dtos.Issues;
using Tdp.Domain.Models;

namespace Tdp.Application.Contracts.Persistence
{
    public interface IIssuesRepository
    {
        Task<Issue> Create(Issue issue);
        Task<IEnumerable<IssueViewDto>> GetForProjectView(Guid projectId);
        Task<Issue?> GetIssueById(Guid issueId);
        Task DeleteIssue(Guid issueId);
    }
}
