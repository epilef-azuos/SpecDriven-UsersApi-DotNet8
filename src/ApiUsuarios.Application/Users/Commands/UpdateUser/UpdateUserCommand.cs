using ApiUsuarios.Application.DTOs;
using MediatR;

namespace ApiUsuarios.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid Id, string Name, string Email) : IRequest<UserDto?>;
