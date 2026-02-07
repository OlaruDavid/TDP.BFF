using Tdp.Domain.Dtos.Board;

namespace Tdp.Application.Contracts.Persistence
{
    public interface IBoardRepository
    {
        Task<IEnumerable<BoardItemDto>> GetBoard(Guid projectId);
    }
}
