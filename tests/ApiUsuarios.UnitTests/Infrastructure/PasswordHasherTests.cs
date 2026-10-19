using ApiUsuarios.Infrastructure.Security;
using FluentAssertions;

namespace ApiUsuarios.UnitTests.Infrastructure;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher = new();

    [Fact]
    public void Hash_SamePasswordTwice_ShouldProduceDifferentHashes()
    {
        // Arrange
        const string password = "Senha123!";

        // Act
        var firstHash = _passwordHasher.Hash(password);
        var secondHash = _passwordHasher.Hash(password);

        // Assert
        firstHash.Should().NotBeNullOrEmpty();
        secondHash.Should().NotBeNullOrEmpty();
        firstHash.Should().NotBe(password);
        firstHash.Should().NotBe(secondHash);
    }

    [Fact]
    public void Verify_CorrectPassword_ShouldReturnTrue()
    {
        // Arrange
        const string password = "Senha123!";
        var hash = _passwordHasher.Hash(password);

        // Act
        var result = _passwordHasher.Verify(password, hash);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Verify_IncorrectPassword_ShouldReturnFalse()
    {
        // Arrange
        var hash = _passwordHasher.Hash("Senha123!");

        // Act
        var result = _passwordHasher.Verify("senha-errada", hash);

        // Assert
        result.Should().BeFalse();
    }
}
