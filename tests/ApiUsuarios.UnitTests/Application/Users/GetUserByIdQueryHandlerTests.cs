using ApiUsuarios.Application.Users.Queries.GetUserById;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ApiUsuarios.UnitTests.Application.Users;

public class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUserRepository> _repository = new();
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _handler = new GetUserByIdQueryHandler(_repository.Object);
    }

    [Fact]
    public async Task Handle_WhenUserExists_ShouldReturnUserDto()
    {
        // Arrange
        var user = new User("Ana", "ana@exemplo.com");
        var query = new GetUserByIdQuery(user.Id);
        _repository.Setup(r => r.GetById(user.Id)).ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(user.Id);
        result.Name.Should().Be("Ana");
        result.Email.Should().Be("ana@exemplo.com");
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var query = new GetUserByIdQuery(Guid.NewGuid());
        _repository.Setup(r => r.GetById(query.Id)).ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
