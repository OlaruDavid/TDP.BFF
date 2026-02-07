namespace Tdp.Domain.Dtos.Members;

public class AddProjectMemberRequest
{
public string Email { get; set; } = null!;
public string Role { get; set; } = "member";
 public Guid UserId { get; set; }
}