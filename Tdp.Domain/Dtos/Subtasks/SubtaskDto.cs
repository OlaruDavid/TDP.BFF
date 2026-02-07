namespace Tdp.Domain.Dtos.Subtasks;

 public class SubtaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ColumnName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
