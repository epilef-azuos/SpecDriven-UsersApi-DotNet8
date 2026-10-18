using ApiUsuarios.Application.DTOs;
using ApiUsuarios.Domain.Interfaces;
using MediatR;

namespace ApiUsuarios.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository repository) : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetById(request.Id);

        return user is null ? null : UserDto.FromEntity(user);
    }
}
