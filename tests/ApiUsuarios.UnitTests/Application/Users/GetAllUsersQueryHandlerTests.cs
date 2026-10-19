using ApiUsuarios.Application.Users.Queries.GetAllUsers;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ApiUsuarios.UnitTests.Application.Users;

public class GetAllUsersQueryHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();
    private readonly GetAllUsersQueryHandler _handler;

    public GetAllUsersQueryHandlerTests()
    {
        _handler = new GetAllUsersQueryHandler(_repository.Object);
    }

    [Fact]
    public async Task Handle_WhenNoUsersExist_ShouldReturnEmptyList()
    {
        // Arrange
        _repository.Setup(r => r.GetAll()).ReturnsAsync(new List<User>());

        // Act
        var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_WhenUsersExist_ShouldReturnAllUsersAsDtos()
    {
        // Arrange
        var ana = new User("Ana", "ana@exemplo.com");
        var bruno = new User("Bruno", "bruno@exemplo.com");
        _repository.Setup(r => r.GetAll()).ReturnsAsync(new List<User> { ana, bruno });

        // Act
        var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);
        result.Select(u => u.Id).Should().BeEquivalentTo(new[] { ana.Id, bruno.Id });
        result.Select(u => u.Name).Should().BeEquivalentTo(new[] { "Ana", "Bruno" });
    }
}
