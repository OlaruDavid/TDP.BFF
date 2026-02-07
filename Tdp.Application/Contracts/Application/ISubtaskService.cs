using System;
using System.Collections.Generic;
using System.Text;
using Tdp.Domain.Dtos.Board;
using Tdp.Domain.Dtos.Subtasks;

namespace Tdp.Application.Contracts.Application
{
    public interface ISubtaskService
    {
        Task<IEnumerable<SubtaskDto>> GetForTask(Guid taskId);
        Task<SubtaskDto?> Create(CreateSubtaskRequest request);
        Task<bool> Move(Guid subtaskId, MoveSubtaskRequest request);
        Task<bool> Update(Guid subtaskId, UpdateSubtaskRequest request);
        Task<bool> DeleteSubtask(Guid subtaskId,Guid actorUserId);
    }
}
