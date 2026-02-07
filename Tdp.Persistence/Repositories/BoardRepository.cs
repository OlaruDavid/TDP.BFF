using Dapper;
using Tdp.Application.Contracts.Persistence;
using Tdp.Domain.Dtos.Board;
using Tdp.Persistence.Data;
using Tdp.Persistence.Queries;

namespace Tdp.Persistence.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly Database _db;

            public BoardRepository(Database db)
            {
                _db = db;
            }

        public async Task<IEnumerable<BoardItemDto>> GetBoard(Guid projectId)
        => await _db.GetConnection().QueryAsync<BoardItemDto>(BoardQueries.GET_BOARDS, new { ProjectId = projectId });
    }
}