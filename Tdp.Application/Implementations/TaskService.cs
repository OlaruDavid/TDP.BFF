using Tdp.Application.Contracts.Application;
using Tdp.Application.Contracts.Persistence;
using Tdp.Domain.Dtos.Tasks;
using Tdp.Domain.Models;

namespace Tdp.Application.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _tasks;
        private readonly IProjectRepository _projects;

        public TaskService(ITaskRepository tasks, IProjectRepository projects)
        {
            _tasks = tasks;
            _projects=projects;
        }

        public async Task<IEnumerable<TaskDto>> GetForProject(Guid projectId)
        {
            var items = await _tasks.GetForProject(projectId);

            return items.Select(t => new TaskDto
            {
                Id = t.Id,
                Name = t.Name,
                Color = t.Color,
                CreatedAt = t.CreatedAt
            });
        }

        public async Task<TaskDto?> Create(CreateTaskRequest request)
        {
            var role = await _projects.GetUserRole(
                request.UserId,
                request.ProjectId
            );

            if (role != "owner")
                return null;

            var task = new TaskItem
            {
                ProjectId = request.ProjectId,
                Name = request.Name,
                Color = request.Color,
                CreatedBy = request.UserId
            };

            var created = await _tasks.Create(task);

            return new TaskDto
            {
                Id = created.Id,
                Name = created.Name,
                Color = created.Color,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<bool> Update(Guid taskId, UpdateTaskRequest request)
        {
            var projectId = await _tasks.GetProjectIdByTaskId(taskId);
            if (projectId == null)
                return false;

            var role = await _projects.GetUserRole(
                request.UserId,
                projectId.Value
            );

            if (role != "owner")
                return false;

            await _tasks.Update(taskId, request);
            return true;
        }

        public async Task<Guid?> GetProjectId(Guid taskId)
        {
            return await _tasks.GetProjectIdByTaskId(taskId);
        }
            public async Task<bool> DeleteTask(
                Guid taskId,
                Guid actorUserId
            )
            {
                var task = await _tasks.GetTaskById(taskId);
                if (task == null)
                    throw new Exception("Task not found");

                var role = await _projects.GetUserRole(
                    actorUserId,
                    task.ProjectId
                );

                if (role != "owner")
                    return false;

                await _tasks.DeleteTask(taskId);

                return true;
            }



    }


}