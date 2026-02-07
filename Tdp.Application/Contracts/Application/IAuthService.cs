using Tdp.Domain.Dtos.Auth;

namespace Tdp.Application.Contracts.Application;

public interface IAuthService
{
    Task<AuthResponse> Register(RegisterRequest request);
    Task<AuthResponse> Login(LoginRequest request);
    Task DeleteUser(Guid userId);
    Task<bool> UserExists(Guid userId);
}
