namespace Tdp.Persistence.Queries
{
    internal static class SubtaskQueries
    {
        public const string GETFORTASK= @"
            SELECT
                id,
                task_id AS TaskId,
                title,
                description,
                column_name AS ColumnName,
                created_at AS CreatedAt,
                created_by AS CreatedBy,
                updated_at AS UpdatedAt,
                updated_by AS UpdatedBy
            FROM subtasks
            WHERE task_id = @TaskId
            ORDER BY position ASC;
        ";
        public const string CREATE= @"
            INSERT INTO subtasks (task_id, title, description, column_name, position)
            VALUES (@TaskId, @Title, @Description, @ColumnName, 0)
            RETURNING
                id,
                task_id AS TaskId,
                title,
                description,
                column_name AS ColumnName,
                created_at AS CreatedAt,
                created_by AS CreatedBy,
                updated_at AS UpdatedAt,
                updated_by AS UpdatedBy;
        ";
        public const string MOVE= @"
            UPDATE subtasks
            SET column_name = @Column,
                position = @Position,
                updated_at = NOW()
            WHERE id = @Id;
        ";
        public const string UPDATE= @"
            UPDATE subtasks
            SET title = @Title,
                description = @Description,
                column_name = @Column,
                updated_at = NOW()
            WHERE id = @Id;
        ";
        public const string GETBYID= @"
            SELECT
                id,
                task_id AS TaskId,
                title,
                description,
                column_name AS ColumnName,
                created_at AS CreatedAt,
                created_by AS CreatedBy
            FROM subtasks
            WHERE id = @Id
            LIMIT 1;
        ";
        public const string GETSUBTASKBYID= @"
            SELECT
                id,
                task_id AS TaskId,
                title,
                description,
                column_name AS ColumnName,
                position,
                assignee_id AS AssigneeId,
                created_at AS CreatedAt,
                created_by AS CreatedBy,
                updated_at AS UpdatedAt,
                updated_by AS UpdatedBy
            FROM subtasks
            WHERE id = @subtaskId;
        ";
        public const string DELETESUBTASK= @"
            DELETE FROM subtasks
            WHERE id = @subtaskId;
        ";
    }
}
