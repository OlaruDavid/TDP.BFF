using Dapper;
using Tdp.Domain.Models;
using Tdp.Domain.Dtos.Tasks;
using Tdp.Persistence.Data;
using Tdp.Application.Contracts.Persistence;
using Tdp.Persistence.Queries;


namespace Tdp.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly Database _db;

        public TaskRepository(Database db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TaskItem>> GetForProject(Guid projectId)
        {
            using var conn = _db.GetConnection();

            const string sql = TaskQueries.GETFORPROJECT;

            return await conn.QueryAsync<TaskItem>(sql, new { ProjectId = projectId });
        }

        public async Task<TaskItem> Create(TaskItem task)
        {
            using var conn = _db.GetConnection();

            const string sql = TaskQueries.CREATE;

            return await conn.QuerySingleAsync<TaskItem>(sql, task);
        }

        public async Task Update(Guid id, UpdateTaskRequest request)
        {
            using var conn = _db.GetConnection();

            const string sql = TaskQueries.UPDATE;

            await conn.ExecuteAsync(sql, new {
                Id = id,
                request.Name,
                request.Color
            });
        }

        public async Task<Guid?> GetProjectIdByTaskId(Guid taskId)
        {
            using var conn = _db.GetConnection();

            const string sql = TaskQueries.GETPROJECTIDBYTASKID;

            return await conn.QueryFirstOrDefaultAsync<Guid?>(
                sql,
                new { TaskId = taskId }
            );
        }

        public async Task<TaskItem?> GetTaskById(Guid taskId)
        {
            using var conn = _db.GetConnection();

            const string sql = TaskQueries.GETTASKBYID;

            return await conn.QueryFirstOrDefaultAsync<TaskItem>(
                sql,
                new { taskId }
            );
        }

        public async Task DeleteTask(Guid taskId)
        {
            using var conn = _db.GetConnection();

            const string sql = TaskQueries.DELETETASK;

            await conn.ExecuteAsync(sql, new { taskId });
        }
    }
}