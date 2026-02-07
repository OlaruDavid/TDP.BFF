namespace Tdp.Persistence.Queries
{
    internal static class ProjectQueries
    {
        public const string CREATE= @"
            WITH created_project AS (
                INSERT INTO projects (name, slug, owner_id)
                VALUES (@Name, @Slug, @OwnerId)
                RETURNING id, name, slug, owner_id, created_at
            )
            INSERT INTO project_members (project_id, user_id, role)
            SELECT id, owner_id, 'owner'
            FROM created_project
            RETURNING
                (SELECT id FROM created_project) AS id,
                (SELECT name FROM created_project) AS name,
                (SELECT slug FROM created_project) AS slug,
                (SELECT owner_id FROM created_project) AS ""OwnerId"",
                (SELECT created_at FROM created_project) AS ""CreatedAt"";
        ";
        public const string GETFORUSER= @"
                SELECT
                    p.id,
                    p.name,
                    p.slug,
                    p.created_at AS CreatedAt,
                    pm.role
                FROM project_members pm
                JOIN projects p ON p.id = pm.project_id
                WHERE pm.user_id = @UserId
                ORDER BY p.created_at DESC;
            ";
        public const string GETBYSLUG= @"
                SELECT
                    p.id,
                    p.name,
                    p.slug,
                    p.created_at AS CreatedAt,
                    pm.role
                FROM project_members pm
                JOIN projects p ON p.id = pm.project_id
                WHERE pm.user_id = @UserId
                AND p.slug = @Slug
                LIMIT 1;
            ";
        public const string GETUSERIDBYEMAIL= @"
                SELECT id
                FROM users
                WHERE email = @email;
            ";
        public const string PROJECTMEMBERTEXISTS= @"
                SELECT 1
                FROM project_members
                WHERE project_id = @projectId
                AND user_id = @userId;
            ";
        public const string ADDPROJECTMEMBER= @"
                INSERT INTO project_members (project_id, user_id, role)
                VALUES (@projectId, @userId, @role);
            ";
        public const string GETPROJECTMEMBERS= @"
                -- members
                SELECT 
                    pm.id AS Id,
                    pm.user_id AS UserId,
                    u.email AS Email,
                    u.first_name AS FirstName,
                    u.last_name AS LastName,
                    pm.role AS Role
                FROM project_members pm
                JOIN users u ON u.id = pm.user_id
                WHERE pm.project_id = @projectId
            ";
        public const string GETUSERROLE= @"
                SELECT role
                FROM project_members
                WHERE user_id = @UserId
                AND project_id = @ProjectId
                LIMIT 1;
            ";
        public const string GETPROJECTMEMBERBYID= @"
                SELECT
                    id,
                    project_id AS ProjectId,
                    user_id AS UserId,
                    role,
                    joined_at AS JoinedAt
                FROM project_members
                WHERE id = @memberId;
            ";
        public const string DELETEPROJECTMEMBER= @"
                DELETE FROM project_members
                WHERE id = @memberId
            ";
        public const string DELETEPROJECT= @"
                DELETE FROM projects
                WHERE id = @projectId;
            ";
    }
}
