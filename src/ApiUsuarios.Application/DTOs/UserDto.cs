using ApiUsuarios.Domain.Entities;

namespace ApiUsuarios.Application.DTOs;

public record UserDto(Guid Id, string Name, string Email)
{
    public static UserDto FromEntity(User user) => new(user.Id, user.Name, user.Email);
}
