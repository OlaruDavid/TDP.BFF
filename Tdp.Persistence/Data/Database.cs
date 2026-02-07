using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Tdp.Persistence.Data
{
    public class Database
    {
        private readonly string _connectionString;

        public Database(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default")!;
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
