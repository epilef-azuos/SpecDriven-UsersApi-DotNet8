using ApiUsuarios.Application.Auth.Commands.Login;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ApiUsuarios.UnitTests.Application.Auth;

public class LoginCommandHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();
    private readonly Mock<IPasswordHasher> _passwordHasher = new();
    private readonly Mock<IJwtTokenService> _jwtTokenService = new();
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        _handler = new LoginCommandHandler(_repository.Object, _passwordHasher.Object, _jwtTokenService.Object);
    }

    [Fact]
    public async Task Handle_WhenUserNotFound_ShouldThrowUnauthorizedAccessException()
    {
        // Arrange
        var command = new LoginCommand("ninguem@exemplo.com", "Senha123!");
        _repository.Setup(r => r.GetByEmail("ninguem@exemplo.com")).ReturnsAsync((User?)null);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Credenciais inválidas.");
        _jwtTokenService.Verify(s => s.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenPasswordIsInvalid_ShouldThrowUnauthorizedAccessException()
    {
        // Arrange
        var command = new LoginCommand("ana@exemplo.com", "senha-errada");
        var user = new User("Ana", "ana@exemplo.com", "hash-antigo", "User");
        _repository.Setup(r => r.GetByEmail("ana@exemplo.com")).ReturnsAsync(user);
        _passwordHasher.Setup(h => h.Verify("senha-errada", "hash-antigo")).Returns(false);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>().WithMessage("Credenciais inválidas.");
        _jwtTokenService.Verify(s => s.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidCredentials_ShouldReturnAuthResponseWithToken()
    {
        // Arrange
        var command = new LoginCommand("ana@exemplo.com", "Senha123!");
        var user = new User("Ana", "ana@exemplo.com", "hash-antigo", "User");
        var expiresAt = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        _repository.Setup(r => r.GetByEmail("ana@exemplo.com")).ReturnsAsync(user);
        _passwordHasher.Setup(h => h.Verify("Senha123!", "hash-antigo")).Returns(true);
        _jwtTokenService.Setup(s => s.GenerateToken(user)).Returns(("token-jwt", expiresAt));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be("token-jwt");
        result.ExpiresAt.Should().Be(expiresAt);
    }
}
