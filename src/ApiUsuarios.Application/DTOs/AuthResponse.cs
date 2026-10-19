namespace ApiUsuarios.Application.DTOs;

public record AuthResponse(string Token, DateTime ExpiresAt);
