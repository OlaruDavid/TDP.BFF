using Tdp.Domain.Dtos.Board;
using Tdp.Application.Contracts.Application;
using Tdp.Application.Contracts.Persistence;


namespace Tdp.Application.Implementations
{
    internal class BoardService : IBoardService
    {
        private readonly IBoardRepository _projects;

            public BoardService(IBoardRepository projects)
            {
                _projects = projects;
            }

        public async Task<IEnumerable<BoardItemDto>> GetBoard(Guid projectId)
        {
            return await _projects.GetBoard(projectId);
        }
    }
}