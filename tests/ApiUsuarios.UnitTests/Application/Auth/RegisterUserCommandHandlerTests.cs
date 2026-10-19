using ApiUsuarios.Application.Users.Commands.RegisterUser;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ApiUsuarios.UnitTests.Application.Auth;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();
    private readonly Mock<IPasswordHasher> _passwordHasher = new();
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _handler = new RegisterUserCommandHandler(_repository.Object, _passwordHasher.Object);
    }

    [Fact]
    public async Task Handle_WhenEmailAlreadyExists_ShouldThrowArgumentException()
    {
        // Arrange
        var command = new RegisterUserCommand("Ana", "ana@exemplo.com", "Senha123!");
        var existingUser = new User("Ana Antiga", "ana@exemplo.com");
        _repository.Setup(r => r.GetByEmail("ana@exemplo.com")).ReturnsAsync(existingUser);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("Email");
        _repository.Verify(r => r.Add(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateUserAndReturnSuccess()
    {
        // Arrange
        var command = new RegisterUserCommand("Ana", "ana@exemplo.com", "Senha123!");
        _repository.Setup(r => r.GetByEmail("ana@exemplo.com")).ReturnsAsync((User?)null);
        _passwordHasher.Setup(h => h.Hash("Senha123!")).Returns("hash-gerado");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be("Ana");
        result.Email.Should().Be("ana@exemplo.com");
        _repository.Verify(
            r => r.Add(It.Is<User>(u =>
                u.Email == "ana@exemplo.com" &&
                u.PasswordHash == "hash-gerado" &&
                u.Role == "User")),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldAlwaysAssignUserRole()
    {
        // Arrange
        var command = new RegisterUserCommand("Ana", "ana@exemplo.com", "Senha123!");
        _repository.Setup(r => r.GetByEmail("ana@exemplo.com")).ReturnsAsync((User?)null);
        _passwordHasher.Setup(h => h.Hash("Senha123!")).Returns("hash-gerado");

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repository.Verify(r => r.Add(It.Is<User>(u => u.Role == "User")), Times.Once);
    }
}
