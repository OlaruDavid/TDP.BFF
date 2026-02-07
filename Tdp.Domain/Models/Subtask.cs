namespace Tdp.Domain.Models;

public class Subtask
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public string ColumnName { get; set; } = "todo"; 
    public int Position { get; set; }

    public Guid? AssigneeId { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}
