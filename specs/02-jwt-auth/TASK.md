# TAREFAS DE EXECUÇÃO (JWT AUTH)

- [x] **T1: Evolução do Domínio e Migração EF Core.** Atualizar a entidade `User` com `PasswordHash` e `Role` sem quebrar construtores existentes. Criar as interfaces `IPasswordHasher` e `IJwtTokenService` em `ApiUsuarios.Domain`. Gerar e aplicar a migração incremental do EF Core no SQLite (`AddAuthFieldsToUser`).
- [x] **T2: Implementação de Infraestrutura.** Adicionar `BCrypt.Net-Next` e gerar as classes concretas `PasswordHasher` e `JwtTokenService` em `ApiUsuarios.Infrastructure`. Registar os serviços na injeção de dependências da Infraestrutura.
- [x] **T3: Casos de Uso na Application (MediatR).** Criar Commands, Handlers e DTOs para `RegisterUser` e `Login` em `ApiUsuarios.Application`.
- [x] **T4: Configuração da API e Segurança no Swagger.** Configurar `appsettings.json` com a seção `Jwt`. Configurar middlewares no `Program.cs` (`UseAuthentication`, `UseAuthorization`) e habilitar suporte a Bearer Token no SwaggerGen.
- [x] **T5: Controllers e Bloqueio de Rotas.** Criar `AuthController` com rotas públicas de `/register` e `/login`. Proteger o `UsersController` com `[Authorize]` e validar o fluxo completo.