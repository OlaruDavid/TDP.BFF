using Tdp.Domain.Dtos.Tasks;
using Tdp.Domain.Models;

namespace Tdp.Application.Contracts.Persistence
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetForProject(Guid projectId);
        Task<TaskItem> Create(TaskItem task);
        Task Update(Guid id, UpdateTaskRequest request);
        Task<Guid?> GetProjectIdByTaskId(Guid taskId);
        Task<TaskItem?> GetTaskById(Guid taskId);
        Task DeleteTask(Guid taskId);
    }
}
