using Tdp.Domain.Dtos.Subtasks;
using Tdp.Domain.Models;

namespace Tdp.Application.Contracts.Persistence
{
    public interface ISubtaskRepository
    {
        Task<IEnumerable<Subtask>> GetForTask(Guid taskId);
        Task<Subtask> Create(Subtask subtask);
        Task Move(Guid id, string column, int position);
        Task Update(Guid id, UpdateSubtaskRequest request);
        Task<Subtask?> GetById(Guid id);
        Task<Subtask?> GetSubtaskById(Guid subtaskId);
        Task DeleteSubtask(Guid subtaskId);
    }
}
