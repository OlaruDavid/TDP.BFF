namespace Tdp.Persistence.Queries
{
    internal static class TaskQueries
    {
        public const string GETFORPROJECT = @"
                SELECT *
                FROM tasks
                WHERE project_id = @ProjectId
                ORDER BY created_at ASC;
            ";
        public const string CREATE= @"
                INSERT INTO tasks (project_id, name, color, created_by)
                VALUES (@ProjectId, @Name, @Color, @CreatedBy)
                RETURNING id, project_id AS ProjectId, name, color, created_at AS CreatedAt;
            ";
        public const string UPDATE= @"
                UPDATE tasks
                SET name = @Name,
                    color = @Color,
                    updated_at = NOW()
                WHERE id = @Id;
            ";
        public const string GETPROJECTIDBYTASKID= @"
                SELECT project_id
                FROM tasks
                WHERE id = @TaskId
                LIMIT 1;
            ";
        public const string GETTASKBYID= @"
                SELECT
                    id,
                    project_id AS ProjectId,
                    name,
                    color,
                    created_at AS CreatedAt,
                    created_by AS CreatedBy,
                    updated_at AS UpdatedAt,
                    updated_by AS UpdatedBy
                FROM tasks
                WHERE id = @taskId;
            ";
        public const string DELETETASK= @"
                DELETE FROM tasks
                WHERE id = @taskId;
            ";
    }
}
