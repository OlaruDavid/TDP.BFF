using Tdp.Application.Contracts.Application;
using Tdp.Application.Contracts.Persistence;


namespace Tdp.Application.Implementations
{
    public class ProjectPermissionService : IProjectPermissionService
    {
        private readonly IProjectRepository _projects;

        public ProjectPermissionService(IProjectRepository projects)
        {
            _projects = projects;
        }

        public async Task<bool> IsOwner(Guid userId, Guid projectId)
        {
            var role = await _projects.GetUserRole(userId, projectId);
            return role == "owner";
        }
    }

}