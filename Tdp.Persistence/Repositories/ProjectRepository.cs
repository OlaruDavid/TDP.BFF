using Dapper;
using Tdp.Application.Contracts.Persistence;
using Tdp.Domain.Dtos.Members;
using Tdp.Domain.Models;
using Tdp.Persistence.Data;
using Tdp.Persistence.Queries;


namespace Tdp.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly Database _db;

        public ProjectRepository(Database db)
        {
            _db = db;
        }

        public async Task<Project> Create(Project project)
        {
            using var conn = _db.GetConnection();

                const string sql = ProjectQueries.CREATE;

            return await conn.QuerySingleAsync<Project>(sql, project);
        }

       public async Task<IEnumerable<ProjectWithRole>> GetForUser(Guid userId)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.GETFORUSER;

            return await conn.QueryAsync<ProjectWithRole>(sql, new { UserId = userId });
        }

        
        public async Task<ProjectWithRole?> GetBySlug(Guid userId, string slug)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.GETBYSLUG;

            return await conn.QueryFirstOrDefaultAsync<ProjectWithRole>(sql, new
            {
                UserId = userId,
                Slug = slug
            });
        }


        public async Task<Guid?> GetUserIdByEmail(string email)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.GETUSERIDBYEMAIL;

            return await conn.QueryFirstOrDefaultAsync<Guid?>(sql, new { email });
        }

        public async Task<bool> ProjectMemberExists(Guid projectId, Guid userId)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.PROJECTMEMBERTEXISTS;

            var result = await conn.QueryFirstOrDefaultAsync<int?>(sql, new
            {
                projectId,
                userId
            });

            return result.HasValue;
        }

        public async Task AddProjectMember(Guid projectId, Guid userId, string role)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.ADDPROJECTMEMBER;

            await conn.ExecuteAsync(sql, new
            {
                projectId,
                userId,
                role
            });
        }

        public async Task<IEnumerable<ProjectMemberDto>> GetProjectMembers(Guid projectId)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.GETPROJECTMEMBERS;

            return await conn.QueryAsync<ProjectMemberDto>(sql, new { projectId });
        }

        public async Task<string?> GetUserRole(Guid userId, Guid projectId)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.GETUSERROLE;

            return await conn.QueryFirstOrDefaultAsync<string>(
                sql,
                new { UserId = userId, ProjectId = projectId }
            );
        }

        public async Task<ProjectMember?> GetProjectMemberById(Guid memberId)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.GETPROJECTMEMBERBYID;

            return await conn.QueryFirstOrDefaultAsync<ProjectMember>(
                sql,
                new { memberId }
            );
        }

        public async Task DeleteProjectMember( Guid memberId )
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.DELETEPROJECTMEMBER;

            await conn.ExecuteAsync(sql, new { memberId  });
        }

        public async Task DeleteProject(Guid projectId)
        {
            using var conn = _db.GetConnection();

            const string sql = ProjectQueries.DELETEPROJECT;

            await conn.ExecuteAsync(sql, new { projectId });
        }
    }
}
