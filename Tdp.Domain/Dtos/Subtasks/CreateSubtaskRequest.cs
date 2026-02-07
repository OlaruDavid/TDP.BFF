namespace Tdp.Domain.Dtos.Subtasks;

public class CreateSubtaskRequest
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
