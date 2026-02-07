using System;
using System.Collections.Generic;
using System.Text;

namespace Tdp.Persistence.Queries
{
    internal static class UserQueries
    {
        public const string GETBYEMAIL= @"
                SELECT id, email, password_hash AS PasswordHash,
                       first_name AS FirstName, last_name AS LastName,
                       created_at AS CreatedAt
                FROM users
                WHERE email = @Email
                LIMIT 1;
            ";
        public const string CREATE= @"
                INSERT INTO users (email, password_hash, first_name, last_name)
                VALUES (@Email, @PasswordHash, @FirstName, @LastName)
                RETURNING id, email, password_hash AS PasswordHash,
                          first_name AS FirstName, last_name AS LastName,
                          created_at AS CreatedAt;
            ";
        public const string DELETEUSER= @"
                DELETE FROM users
                WHERE id = @userId;
            ";
        public const string USEREXISTS= @"
                SELECT 1
                FROM users
                WHERE id = @userId;
            ";
    }
}
