using System;
using System.Collections.Generic;
using System.Text;
using Tdp.Domain.Dtos.Tasks;

namespace Tdp.Application.Contracts.Application
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetForProject(Guid projectId);
        Task<TaskDto?> Create(CreateTaskRequest request);
        Task<bool> Update(Guid taskId, UpdateTaskRequest request);
        Task<Guid?> GetProjectId(Guid taskId);
        Task<bool> DeleteTask(
                Guid taskId,
                Guid actorUserId
            );
    }
}
