using ApiUsuarios.Application.Users.Commands.DeleteUser;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ApiUsuarios.UnitTests.Application.Users;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _handler = new DeleteUserCommandHandler(_repository.Object);
    }

    [Fact]
    public async Task Handle_WhenUserExists_ShouldDeleteUserAndReturnTrue()
    {
        // Arrange
        var user = new User("Ana", "ana@exemplo.com");
        var command = new DeleteUserCommand(user.Id);
        _repository.Setup(r => r.GetById(user.Id)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        _repository.Verify(r => r.Delete(user), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldReturnFalseWithoutDeleting()
    {
        // Arrange
        var command = new DeleteUserCommand(Guid.NewGuid());
        _repository.Setup(r => r.GetById(command.Id)).ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        _repository.Verify(r => r.Delete(It.IsAny<User>()), Times.Never);
    }
}
