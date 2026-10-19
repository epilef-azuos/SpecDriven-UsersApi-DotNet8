using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApiUsuarios.Domain.Entities;
using ApiUsuarios.Infrastructure.Security;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiUsuarios.UnitTests.Infrastructure;

public class JwtTokenServiceTests
{
    private const string Key = "chave-de-teste-com-mais-de-32-caracteres-123456";
    private const string Issuer = "ApiUsuarios";
    private const string Audience = "ApiUsuarios";

    private readonly JwtTokenService _jwtTokenService;

    public JwtTokenServiceTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Key"] = Key,
                ["Jwt:Issuer"] = Issuer,
                ["Jwt:Audience"] = Audience,
                ["Jwt:ExpiresInMinutes"] = "60"
            })
            .Build();

        _jwtTokenService = new JwtTokenService(configuration);
    }

    [Fact]
    public void GenerateToken_ValidUser_ShouldReturnNonEmptyReadableJwt()
    {
        // Arrange
        var user = new User("Ana", "ana@exemplo.com", "hash-qualquer", "Admin");

        // Act
        var (token, _) = _jwtTokenService.GenerateToken(user);

        // Assert
        token.Should().NotBeNullOrWhiteSpace();
        new JwtSecurityTokenHandler().CanReadToken(token).Should().BeTrue();
    }

    [Fact]
    public void GenerateToken_ValidUser_ShouldIncludeSubEmailAndRoleClaims()
    {
        // Arrange
        var user = new User("Ana", "ana@exemplo.com", "hash-qualquer", "Admin");

        // Act
        var (token, _) = _jwtTokenService.GenerateToken(user);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        // Assert
        jwt.Subject.Should().Be(user.Id.ToString());
        jwt.Claims.Should().Contain(c => c.Type == "email" && c.Value == "ana@exemplo.com");
        jwt.Claims.Should().Contain(c => c.Type == "role" && c.Value == "Admin");
        jwt.Issuer.Should().Be(Issuer);
        jwt.Audiences.Should().Contain(Audience);
    }

    [Fact]
    public void GenerateToken_ValidUser_ShouldBeSignedWithConfiguredKey()
    {
        // Arrange
        var user = new User("Ana", "ana@exemplo.com", "hash-qualquer", "User");
        var (token, _) = _jwtTokenService.GenerateToken(user);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))
        };

        // Act
        var act = () => new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void GenerateToken_ValidUser_ShouldExpireAtReturnedExpiration()
    {
        // Arrange
        var user = new User("Ana", "ana@exemplo.com", "hash-qualquer", "User");

        // Act
        var (token, expiresAt) = _jwtTokenService.GenerateToken(user);
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        // Assert
        jwt.ValidTo.Should().BeCloseTo(expiresAt, TimeSpan.FromSeconds(2));
    }
}
