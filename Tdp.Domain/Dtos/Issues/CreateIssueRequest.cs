namespace Tdp.Domain.Dtos.Issues;

public class CreateIssueRequest
{
    public Guid ProjectId { get; set; }

    public Guid? TaskId { get; set; }
    public Guid? SubtaskId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
      public Guid UserId { get; set; }
}
