namespace Tdp.Persistence.Queries
{
    internal static class BoardQueries
    {
        public const string GET_BOARDS = @"
                SELECT
                    s.id,
                    s.task_id AS ""TaskId"",
                    s.title,
                    s.description,
                    s.column_name AS ""Column"",
                    t.name AS ""TaskName"",
                    t.color AS ""TaskColor""
                FROM subtasks s
                JOIN tasks t ON t.id = s.task_id
                WHERE t.project_id = @ProjectId
                ORDER BY s.position ASC;
            ";

    }
}
