using Dapper;
using Tdp.Application.Contracts.Persistence;
using Tdp.Domain.Dtos.Issues;
using Tdp.Domain.Models;
using Tdp.Persistence.Data;
using Tdp.Persistence.Queries;

namespace Tdp.Persistence.Repositories
{
    public class IssuesRepository : IIssuesRepository
    {
        private readonly Database _db;

        public IssuesRepository(Database db)
        {
            _db = db;
        }

        public async Task<Issue> Create(Issue issue)
        {
            using var conn = _db.GetConnection();

            const string sql = IssuesQueries.CREATE;

            return await conn.QuerySingleAsync<Issue>(sql, issue);
        }

        public async Task<IEnumerable<IssueViewDto>> GetForProjectView(Guid projectId)
        {
            using var conn = _db.GetConnection();

            const string sql = IssuesQueries.GETFORPROJECTVIEW;

            return await conn.QueryAsync<IssueViewDto>(sql, new { ProjectId = projectId });
        }

        public async Task<Issue?> GetIssueById(Guid issueId)
        {
            using var conn = _db.GetConnection();

            const string sql = IssuesQueries.GETISSUEBYID;

            return await conn.QueryFirstOrDefaultAsync<Issue>(
                sql,
                new { issueId }
            );
        }
        public async Task DeleteIssue(Guid issueId)
        {
            using var conn = _db.GetConnection();

            const string sql = IssuesQueries.DELETEISSUE;

            await conn.ExecuteAsync(sql, new { issueId });
        }
    }
}
