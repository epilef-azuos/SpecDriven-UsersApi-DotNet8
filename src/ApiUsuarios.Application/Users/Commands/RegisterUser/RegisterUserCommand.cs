using ApiUsuarios.Application.DTOs;
using MediatR;

namespace ApiUsuarios.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Name, string Email, string Password) : IRequest<UserDto>;
