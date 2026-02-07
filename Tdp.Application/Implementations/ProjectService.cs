using Tdp.Domain.Models;
using Tdp.Domain.Dtos.Members;
using Tdp.Domain.Dtos.Projects;
using Tdp.Application.Contracts.Application;
using Tdp.Application.Contracts.Persistence;


namespace Tdp.Application.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projects;

        public ProjectService(IProjectRepository projects)
        {
            _projects = projects;
        }

        public async Task<ProjectDto> Create(Guid ownerId, CreateProjectRequest request)
        {
            var slug = Slugify(request.Name);

            var project = new Project
            {
                Name = request.Name,
                Slug = slug,
                OwnerId = ownerId
            };

            var created = await _projects.Create(project);

            return new ProjectDto
            {
                Id = created.Id,
                Name = created.Name,
                Slug = created.Slug,
                CreatedAt = created.CreatedAt
            };
        }

        private static string Slugify(string text)
        {
            return text
                .ToLowerInvariant()
                .Trim()
                .Replace(" ", "-");
        }
        public async Task<IEnumerable<ProjectDto>> GetForUser(Guid userId)
        {
            var items = await _projects.GetForUser(userId);

            return items.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                CreatedAt = p.CreatedAt,
                Role = p.Role
            });
        }
        public async Task<ProjectDto?> GetBySlug(Guid userId, string slug)
            {
                var p = await _projects.GetBySlug(userId, slug);
                if (p == null) return null;

                return new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    CreatedAt = p.CreatedAt,
                    Role = p.Role
                };
            }
        public async Task<bool> AddMember(
            Guid projectId,
            Guid actorUserId,
            AddProjectMemberRequest request
        )
        {
            var role = await _projects.GetUserRole(actorUserId, projectId);
            if (role != "owner")
                return false;

            var userId = await _projects.GetUserIdByEmail(request.Email);
            if (userId == null)
                throw new Exception("User not found");

            var exists = await _projects.ProjectMemberExists(projectId, userId.Value);
            if (exists)
                throw new Exception("User already in project");

            await _projects.AddProjectMember(projectId, userId.Value, request.Role);

            return true;
        }


        public async Task<IEnumerable<ProjectMemberDto>> GetMembers(Guid projectId)
        {
            return await _projects.GetProjectMembers(projectId);
        }

        public async Task<bool> RemoveMember(
            Guid memberId,
            Guid currentUserId
        )
        {
            var member = await _projects.GetProjectMemberById(memberId);

            if (member == null)
                throw new Exception("Project member not found");

            var currentUserRole = await _projects.GetUserRole(
                currentUserId,
                member.ProjectId
            );

            if (currentUserRole != "owner")
                return false;

            if (member.Role == "owner")
                throw new Exception("Cannot remove project owner");

            await _projects.DeleteProjectMember(memberId);

            return true;
        }
        public async Task<bool> DeleteProject(
            Guid projectId,
            Guid userId
        )
        {
            var role = await _projects.GetUserRole(
                userId,
                projectId
            );

            if (role != "owner")
                return false;

            await _projects.DeleteProject(projectId);

            return true;
        }





    }
}
