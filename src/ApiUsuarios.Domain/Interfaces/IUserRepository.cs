using ApiUsuarios.Domain.Entities;

namespace ApiUsuarios.Domain.Interfaces;

public interface IUserRepository
{
    Task Add(User user);

    Task<User?> GetById(Guid id);

    Task<IReadOnlyList<User>> GetAll();

    Task Update(User user);

    Task Delete(User user);
}
