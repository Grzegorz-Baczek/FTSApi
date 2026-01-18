using FTS.Application.Abstractions;
using FTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FTS.Infrastructure.DAL.Repositories;

internal sealed class UserRepository(FTSDbContext dbContext) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public Task<User> GetByEmailAsync(string email)
    {
        return dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public Task<User> GetByIdAsync(Guid id)
    {
        return dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
    }

    public Task<User> GetByUsernameAsync(string name)
    {
        return dbContext.Users.SingleOrDefaultAsync(u => u.Name == name);
    }
}
