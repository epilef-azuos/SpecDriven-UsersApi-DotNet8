using ApiUsuarios.Application.DTOs;
using ApiUsuarios.Domain.Interfaces;
using MediatR;

namespace ApiUsuarios.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository repository) : IRequestHandler<UpdateUserCommand, UserDto?>
{
    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetById(request.Id);

        if (user is null)
        {
            return null;
        }

        user.Update(request.Name, request.Email);

        await repository.Update(user);

        return UserDto.FromEntity(user);
    }
}
