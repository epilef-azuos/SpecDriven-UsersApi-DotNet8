using ApiUsuarios.Application.DTOs;
using MediatR;

namespace ApiUsuarios.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;
