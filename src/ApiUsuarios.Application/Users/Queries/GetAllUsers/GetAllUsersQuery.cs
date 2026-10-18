using ApiUsuarios.Application.DTOs;
using MediatR;

namespace ApiUsuarios.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>;
