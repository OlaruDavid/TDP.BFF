namespace Tdp.Domain.Dtos.Tasks;

public class UpdateTaskRequest
{
    public Guid UserId{get;set;}
    public string Name { get; set; } = string.Empty;
    public string? Color { get; set; }
}