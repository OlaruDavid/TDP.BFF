namespace Tdp.Domain.Dtos.Issues;

public class IssueViewDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? TaskName { get; set; }
    public string? SubtaskTitle { get; set; }
}