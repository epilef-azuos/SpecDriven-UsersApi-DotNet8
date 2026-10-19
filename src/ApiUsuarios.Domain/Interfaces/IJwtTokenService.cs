using ApiUsuarios.Domain.Entities;

namespace ApiUsuarios.Domain.Interfaces;

public interface IJwtTokenService
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}
