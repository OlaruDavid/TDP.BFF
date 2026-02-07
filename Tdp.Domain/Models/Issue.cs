namespace Tdp.Domain.Models;

public class Issue
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }
    public Guid? TaskId { get; set; }
    public Guid? SubtaskId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
