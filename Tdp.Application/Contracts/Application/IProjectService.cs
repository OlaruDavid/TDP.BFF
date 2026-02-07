using Tdp.Domain.Dtos.Members;
using Tdp.Domain.Dtos.Projects;

namespace Tdp.Application.Contracts.Application
{
    public interface IProjectService
    {
        Task<ProjectDto> Create(Guid ownerId, CreateProjectRequest request);
        Task<IEnumerable<ProjectDto>> GetForUser(Guid userId);
        Task<ProjectDto?> GetBySlug(Guid userId, string slug);
        Task<bool> AddMember(Guid projectId,Guid actorUserId,AddProjectMemberRequest request);
        Task<IEnumerable<ProjectMemberDto>> GetMembers(Guid projectId);
        Task<bool> RemoveMember(Guid memberId, Guid currentUserId);
        Task<bool> DeleteProject(Guid projectId,Guid userId);
    }
}
