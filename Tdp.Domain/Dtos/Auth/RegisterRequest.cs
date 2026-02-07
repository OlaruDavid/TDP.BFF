namespace Tdp.Domain.Dtos.Auth;

public class RegisterRequest : LoginRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
