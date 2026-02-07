using Tdp.Application.Contracts.Application;
using Tdp.Application.Contracts.Persistence;
using Tdp.Domain.Dtos.Auth;
using Tdp.Domain.Models;


namespace Tdp.Application.Implementations;

internal class AuthService(IUserRepository users) : IAuthService
{
    private readonly IUserRepository _users = users;

    public async Task<AuthResponse> Register(RegisterRequest request)
    {
        var existing = await _users.GetByEmail(request.Email);
        if (existing != null)
            throw new Exception("Email already in use");

        var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Email = request.Email,
            PasswordHash = hash,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var created = await _users.Create(user);

        return new AuthResponse
        {
            UserId = created.Id,
            Email = created.Email,
            FirstName = created.FirstName,
            LastName = created.LastName,
            Token = "later-jwt"
        };
    }
    public async Task<AuthResponse> Login(LoginRequest request)
    {
        var user = await _users.GetByEmail(request.Email);
        if (user == null)
            throw new Exception("Invalid credentials");

        var ok = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!ok)
            throw new Exception("Invalid credentials");

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = "later-jwt"
        };
    }
    public async Task DeleteUser(Guid userId)
    {
        await _users.DeleteUser(userId);
    }
    public async Task<bool> UserExists(Guid userId)
    {
        return await _users.UserExists(userId);
    }

}
