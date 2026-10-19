using ApiUsuarios.Application.Users.Commands.CreateUser;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ApiUsuarios.UnitTests.Application.Users;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _handler = new CreateUserCommandHandler(_repository.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldCreateUserAndReturnUserDto()
    {
        // Arrange
        var command = new CreateUserCommand("Ana", "ana@exemplo.com");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be("Ana");
        result.Email.Should().Be("ana@exemplo.com");
        _repository.Verify(
            r => r.Add(It.Is<User>(u => u.Name == "Ana" && u.Email == "ana@exemplo.com")),
            Times.Once);
    }
}
