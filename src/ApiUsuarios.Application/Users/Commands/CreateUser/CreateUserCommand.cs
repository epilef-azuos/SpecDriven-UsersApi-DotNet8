using ApiUsuarios.Application.DTOs;
using MediatR;

namespace ApiUsuarios.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string Email) : IRequest<UserDto>;
