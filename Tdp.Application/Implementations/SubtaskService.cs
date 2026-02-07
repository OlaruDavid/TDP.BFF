using Tdp.Application.Contracts.Application;
using Tdp.Application.Contracts.Persistence;
using Tdp.Domain.Dtos.Board;
using Tdp.Domain.Dtos.Subtasks;
using Tdp.Domain.Models;
namespace Tdp.Application.Implementations
{
    public class SubtaskService : ISubtaskService
    {
        private readonly ISubtaskRepository _subtasks;
        private readonly ITaskService _tasks;         
        private readonly IProjectRepository _projects;

        public SubtaskService(ISubtaskRepository subtasks,IProjectRepository projects,ITaskService tasks )
        {
            _subtasks = subtasks;
            _tasks = tasks;
            _projects=projects;
        }

        public async Task<IEnumerable<SubtaskDto>> GetForTask(Guid taskId)
        {
            var items = await _subtasks.GetForTask(taskId);

            return items.Select(s => new SubtaskDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                ColumnName = s.ColumnName,
                CreatedAt = s.CreatedAt
            });
        }

        public async Task<SubtaskDto?> Create(CreateSubtaskRequest request)
        {
            var projectId = await _tasks.GetProjectId(request.TaskId);
            if (projectId == null)
                return null;

            var role = await _projects.GetUserRole(
                request.UserId,
                projectId.Value
            );

            if (role != "owner")
                return null;

            var subtask = new Subtask
            {
                TaskId = request.TaskId,
                Title = request.Title,
                Description = request.Description,
                ColumnName = "todo",
                CreatedBy = request.UserId
            };

            var created = await _subtasks.Create(subtask);

            return new SubtaskDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                ColumnName = created.ColumnName,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<bool> Move(Guid subtaskId, MoveSubtaskRequest request)
        {
            var subtask = await _subtasks.GetById(subtaskId);
            if (subtask == null)
                return false;

            var projectId = await _tasks.GetProjectId(subtask.TaskId);
            if (projectId == null)
                return false;

            var role = await _projects.GetUserRole(
                request.UserId,
                projectId.Value
            );

            if (role != "owner")
                return false;

            await _subtasks.Move(
                subtaskId,
                request.Column,
                request.Position
            );

            return true;
        }


        public async Task<bool> Update(Guid subtaskId, UpdateSubtaskRequest request)
        {
            var subtask = await _subtasks.GetById(subtaskId);
            if (subtask == null)
                return false;

            var projectId = await _tasks.GetProjectId(subtask.TaskId);
            if (projectId == null)
                return false;

            var role = await _projects.GetUserRole(
                request.UserId,
                projectId.Value
            );

            if (role != "owner")
                return false;

            await _subtasks.Update(subtaskId, request);
            return true;
        }
        public async Task<bool> DeleteSubtask(
            Guid subtaskId,
            Guid actorUserId
        )
        {
            // 1️⃣ luăm subtask-ul
            var subtask = await _subtasks.GetSubtaskById(subtaskId);
            if (subtask == null)
                throw new Exception("Subtask not found");

            // 2️⃣ luăm projectId DIRECT din task
            var projectId = await _tasks.GetProjectId(subtask.TaskId);
            if (projectId == null)
                throw new Exception("Task not found");

            // 3️⃣ verificăm owner pe proiect
            var role = await _projects.GetUserRole(
                actorUserId,
                projectId.Value
            );

            if (role != "owner")
                return false;

            // 4️⃣ delete
            await _subtasks.DeleteSubtask(subtaskId);

            return true;
        }



    }

}