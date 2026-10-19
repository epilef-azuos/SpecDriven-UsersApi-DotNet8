using ApiUsuarios.Application.Users.Commands.UpdateUser;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ApiUsuarios.UnitTests.Application.Users;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _handler = new UpdateUserCommandHandler(_repository.Object);
    }

    [Fact]
    public async Task Handle_WhenUserExists_ShouldUpdateUserAndReturnUserDto()
    {
        // Arrange
        var user = new User("Ana", "ana@exemplo.com");
        var command = new UpdateUserCommand(user.Id, "Ana Souza", "ana.souza@exemplo.com");
        _repository.Setup(r => r.GetById(user.Id)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(user.Id);
        result.Name.Should().Be("Ana Souza");
        result.Email.Should().Be("ana.souza@exemplo.com");
        _repository.Verify(r => r.Update(user), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldReturnNullWithoutUpdating()
    {
        // Arrange
        var command = new UpdateUserCommand(Guid.NewGuid(), "Ana Souza", "ana.souza@exemplo.com");
        _repository.Setup(r => r.GetById(command.Id)).ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _repository.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
    }
}
