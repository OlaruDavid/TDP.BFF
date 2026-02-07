namespace Tdp.Domain.Models;

public class TaskItem
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}
