namespace Tdp.Domain.Dtos.Board;

public class BoardItemDto
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Column { get; set; } = string.Empty; 
    public string TaskName { get; set; } = string.Empty;
    public string? TaskColor { get; set; }
}
