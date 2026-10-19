using ApiUsuarios.Application.DTOs;
using ApiUsuarios.Domain.Interfaces;
using MediatR;

namespace ApiUsuarios.Application.Auth.Commands.Login;

public class LoginCommandHandler(
    IUserRepository repository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService) : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByEmail(request.Email);

        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Credenciais inválidas.");
        }

        var (token, expiresAt) = jwtTokenService.GenerateToken(user);

        return new AuthResponse(token, expiresAt);
    }
}
