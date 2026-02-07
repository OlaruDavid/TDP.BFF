namespace Tdp.Domain.Dtos.Subtasks;

public class UpdateSubtaskRequest
{
    public Guid UserId{get;set;}
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Column { get; set; } = "todo";
}