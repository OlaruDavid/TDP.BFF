using Tdp.Domain.Models;

namespace Tdp.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task<User> Create(User user);
        Task DeleteUser(Guid userId);
        Task<bool> UserExists(Guid userId);
    }
}
