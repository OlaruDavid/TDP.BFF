namespace Tdp.Domain.Dtos.Projects;

public class CreateProjectRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}
