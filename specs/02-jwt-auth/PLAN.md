# PLANO TÉCNICO E ARQUITETURA (JWT)

## BIBLIOTECAS ADICIONAIS
- `BCrypt.Net-Next` (no projeto `ApiUsuarios.Infrastructure` para hashing salgado de passwords).
- `Microsoft.AspNetCore.Authentication.JwtBearer` (no projeto `ApiUsuarios.API`).
- `System.IdentityModel.Tokens.Jwt` (no projeto `ApiUsuarios.Infrastructure`).

## EVOLUÇÃO DAS CAMADAS

1. **ApiUsuarios.Domain**:
   - Evolução da entidade `User.cs`: adicionar propriedades `PasswordHash` (string) e `Role` (string).
   - Definição das interfaces de serviço (Ports): `IPasswordHasher` e `IJwtTokenService`.

2. **ApiUsuarios.Application**:
   - `RegisterUserCommand`: recebe Name, Email, Password, Role.
   - `LoginCommand`: recebe Email, Password. Retorna DTO com Token e Expiração.
   - Handlers usando `IPasswordHasher`, `IJwtTokenService` e `IUserRepository`.

3. **ApiUsuarios.Infrastructure**:
   - Implementação de `PasswordHasher` com BCrypt.
   - Implementação de `JwtTokenService` gerando claims (Sub, Email, Role) assinado com chave simétrica.
   - Migração incremental no EF Core: `AddAuthFieldsToUser`.

4. **ApiUsuarios.API**:
   - Configuração de JWT no `appsettings.json` (Issuer, Audience, SecretKey).
   - Configuração de autenticação no `Program.cs` (`AddAuthentication` + `AddJwtBearer`).
   - Configuração do Swagger com `AddSecurityDefinition("Bearer", ...)` e `AddSecurityRequirement`.
   - Adição do atributo `[Authorize]` no `UsersController`.
   - Criação do `AuthController` expondo `POST /api/auth/register` e `POST /api/auth/login`.