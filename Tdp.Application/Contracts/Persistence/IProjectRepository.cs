using Tdp.Domain.Dtos.Members;
using Tdp.Domain.Models;

namespace Tdp.Application.Contracts.Persistence
{
    public interface IProjectRepository
    {
        Task<Project> Create(Project project);
        Task<IEnumerable<ProjectWithRole>> GetForUser(Guid userId);
        Task<ProjectWithRole?> GetBySlug(Guid userId, string slug);
        Task<Guid?> GetUserIdByEmail(string email);
        Task<bool> ProjectMemberExists(Guid projectId, Guid userId);
        Task AddProjectMember(Guid projectId, Guid userId, string role);
        Task<IEnumerable<ProjectMemberDto>> GetProjectMembers(Guid projectId);
        Task<string?> GetUserRole(Guid userId, Guid projectId);
        Task<ProjectMember?> GetProjectMemberById(Guid memberId);
        Task DeleteProjectMember(Guid memberId);
        Task DeleteProject(Guid projectId);
        }
}
