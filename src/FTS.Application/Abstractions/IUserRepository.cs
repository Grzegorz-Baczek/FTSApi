using FTS.Core.Entities;

namespace FTS.Application.Abstractions;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByUsernameAsync(string name);
}
