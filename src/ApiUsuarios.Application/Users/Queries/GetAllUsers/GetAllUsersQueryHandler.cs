using ApiUsuarios.Application.DTOs;
using ApiUsuarios.Domain.Interfaces;
using MediatR;

namespace ApiUsuarios.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDto>>
{
    public async Task<IReadOnlyList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await repository.GetAll();

        return users.Select(UserDto.FromEntity).ToList();
    }
}
