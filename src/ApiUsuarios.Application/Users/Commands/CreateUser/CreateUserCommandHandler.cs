using ApiUsuarios.Application.DTOs;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using MediatR;

namespace ApiUsuarios.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email);

        await repository.Add(user);

        return UserDto.FromEntity(user);
    }
}
