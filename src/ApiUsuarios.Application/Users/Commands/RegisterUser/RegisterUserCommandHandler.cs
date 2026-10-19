using ApiUsuarios.Application.DTOs;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using MediatR;

namespace ApiUsuarios.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserCommand, UserDto>
{
    private static readonly string[] AllowedRoles = ["Admin", "User"];

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("O campo 'password' é obrigatório.", nameof(request.Password));
        }

        if (!AllowedRoles.Contains(request.Role))
        {
            throw new ArgumentException("O perfil informado é inválido.", nameof(request.Role));
        }

        var existingUser = await repository.GetByEmail(request.Email);

        if (existingUser is not null)
        {
            throw new ArgumentException("O e-mail informado já está cadastrado.", nameof(request.Email));
        }

        var user = new User(request.Name, request.Email, passwordHasher.Hash(request.Password), request.Role);

        await repository.Add(user);

        return UserDto.FromEntity(user);
    }
}
