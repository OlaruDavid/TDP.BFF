namespace Tdp.Domain.Dtos.Tasks;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; }
}
