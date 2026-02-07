namespace Tdp.Domain.Models;

public class Project
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public Guid OwnerId { get; set; }

    public DateTime CreatedAt { get; set; }
}
