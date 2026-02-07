using Dapper;
using Tdp.Domain.Models;
using Tdp.Persistence.Data;
using Tdp.Application.Contracts.Persistence;
using Tdp.Persistence.Queries;


namespace Tdp.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Database _db;

        public UserRepository(Database db)
        {
            _db = db;
        }

        public async Task<User?> GetByEmail(string email)
        {
            using var conn = _db.GetConnection();

            const string sql = UserQueries.GETBYEMAIL;

            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task<User> Create(User user)
        {
            using var conn = _db.GetConnection();

            const string sql = UserQueries.CREATE;

            return await conn.QuerySingleAsync<User>(sql, user);
        }
        public async Task DeleteUser(Guid userId)
        {
            using var conn = _db.GetConnection();

            const string sql = UserQueries.DELETEUSER;

            await conn.ExecuteAsync(sql, new { userId });
        }
        public async Task<bool> UserExists(Guid userId)
        {
            using var conn = _db.GetConnection();

            const string sql = UserQueries.USEREXISTS;

            var result = await conn.QueryFirstOrDefaultAsync<int?>(
                sql,
                new { userId }
            );

            return result.HasValue;
        }
    }
}
