using ApiUsuarios.Application.DTOs;
using MediatR;

namespace ApiUsuarios.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;
