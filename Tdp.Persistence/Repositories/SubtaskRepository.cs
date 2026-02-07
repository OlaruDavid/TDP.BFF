using Dapper;
using Tdp.Application.Contracts.Persistence;
using Tdp.Domain.Dtos.Subtasks;
using Tdp.Domain.Models;
using Tdp.Persistence.Data;
using Tdp.Persistence.Queries;


namespace Tdp.Persistence.Repositories
{
    public class SubtaskRepository : ISubtaskRepository
{
    private readonly Database _db;

    public SubtaskRepository(Database db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Subtask>> GetForTask(Guid taskId)
    {
        using var conn = _db.GetConnection();

            const string sql = SubtaskQueries.GETFORTASK;

        return await conn.QueryAsync<Subtask>(sql, new { TaskId = taskId });
    }

    public async Task<Subtask> Create(Subtask subtask)
    {
        using var conn = _db.GetConnection();

            const string sql = SubtaskQueries.CREATE;

        return await conn.QuerySingleAsync<Subtask>(sql, subtask);
    }
    public async Task Move(Guid id, string column, int position)
    {
        using var conn = _db.GetConnection();

            const string sql = SubtaskQueries.MOVE;

        await conn.ExecuteAsync(sql, new
        {
            Id = id,
            Column = column,
            Position = position
        });
    }

    public async Task Update(Guid id, UpdateSubtaskRequest request)
    {
        using var conn = _db.GetConnection();

            const string sql = SubtaskQueries.UPDATE;

        await conn.ExecuteAsync(sql, new {
            Id = id,
            request.Title,
            request.Description,
            request.Column
        });
    }

    public async Task<Subtask?> GetById(Guid id)
    {
        using var conn = _db.GetConnection();

            const string sql = SubtaskQueries.GETBYID;
        return await conn.QueryFirstOrDefaultAsync<Subtask>(
            sql,
            new { Id = id }
        );
    }

    public async Task<Subtask?> GetSubtaskById(Guid subtaskId)
    {
        using var conn = _db.GetConnection();

            const string sql = SubtaskQueries.GETSUBTASKBYID;

        return await conn.QueryFirstOrDefaultAsync<Subtask>(
            sql,
            new { subtaskId }
        );
    }   
    public async Task DeleteSubtask(Guid subtaskId)
    {
        using var conn = _db.GetConnection();

            const string sql = SubtaskQueries.DELETESUBTASK;

        await conn.ExecuteAsync(sql, new { subtaskId });
    }

}

}