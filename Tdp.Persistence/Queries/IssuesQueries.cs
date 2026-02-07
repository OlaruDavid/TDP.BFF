namespace Tdp.Persistence.Queries
{
    internal static class IssuesQueries
    {
        public const string CREATE = @"INSERT INTO issues (project_id, task_id, subtask_id, title, description, created_by)
                VALUES (@ProjectId, @TaskId, @SubtaskId, @Title, @Description, @CreatedBy)
                RETURNING 
                    id, 
                    project_id AS ProjectId,
                    task_id AS TaskId,
                    subtask_id AS SubtaskId,
                    title,
                    description,
                    created_by AS CreatedBy,
                    created_at AS CreatedAt;";

        public const string GETFORPROJECTVIEW= @"
                SELECT
                    i.id,
                    i.title,
                    i.description,
                    t.name AS ""TaskName"",
                    s.title AS ""SubtaskTitle""
                FROM issues i
                LEFT JOIN tasks t ON t.id = i.task_id
                LEFT JOIN subtasks s ON s.id = i.subtask_id
                WHERE i.project_id = @ProjectId
                ORDER BY i.created_at DESC;
            ";
        public const string GETISSUEBYID = @"
                SELECT
                    id,
                    project_id AS ProjectId,
                    task_id AS TaskId,
                    subtask_id AS SubtaskId,
                    title,
                    description,
                    created_by AS CreatedBy,
                    created_at AS CreatedAt
                FROM issues
                WHERE id = @issueId;
            ";
        public const string DELETEISSUE= @"
                DELETE FROM issues
                WHERE id = @issueId;
            ";
    }
}
