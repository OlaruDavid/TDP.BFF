using System;
using System.Collections.Generic;
using System.Text;
using Tdp.Domain.Dtos.Board;

namespace Tdp.Application.Contracts.Application
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardItemDto>> GetBoard(Guid projectId);
    }
}
