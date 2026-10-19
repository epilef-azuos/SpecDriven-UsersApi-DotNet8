using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiUsuarios.Infrastructure.Persistence;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task Add(User user)
    {
        context.Users.Add(user);

        await context.SaveChangesAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IReadOnlyList<User>> GetAll()
    {
        return await context.Users.AsNoTracking().ToListAsync();
    }

    public async Task Update(User user)
    {
        context.Users.Update(user);

        await context.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        context.Users.Remove(user);

        await context.SaveChangesAsync();
    }
}
