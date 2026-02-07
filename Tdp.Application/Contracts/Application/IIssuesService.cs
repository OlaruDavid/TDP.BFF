using System;
using System.Collections.Generic;
using System.Text;
using Tdp.Domain.Dtos.Issues;
using Tdp.Domain.Models;

namespace Tdp.Application.Contracts.Application
{
    public interface IIssuesService
    {
        Task<Issue?> Create(CreateIssueRequest request);
        Task<IEnumerable<IssueViewDto>> GetForProjectView(Guid projectId);
        Task<bool> DeleteIssue(Guid issueId,Guid actorUserId);
    }
}
