namespace Tdp.Domain.Dtos.Members;

public class ProjectMemberDto
{
    public Guid Id { get; set; }          
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Role { get; set; } = null!;
}
