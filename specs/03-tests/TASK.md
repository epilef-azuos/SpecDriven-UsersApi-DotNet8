# TAREFAS DE EXECUÇÃO (TESTES UNITÁRIOS)

- [x] **T1: Setup do Projeto de Testes.** Criar o projeto `tests/ApiUsuarios.UnitTests` utilizando o template xUnit no .NET 8. Instalar os pacotes `Moq` e `FluentAssertions`. Adicionar referências aos projetos `Domain`, `Application` e `Infrastructure`. Adicionar o projeto à solution `ApiUsuarios.sln`.
- [x] **T2: Testes de Autenticação (Application).** Criar os testes unitários para `RegisterUserCommandHandler` (cenários de sucesso e e-mail duplicado) e `LoginCommandHandler` (sucesso com retorno de token e falha por credenciais inválidas) utilizando mocks para `IUserRepository`, `IPasswordHasher` e `IJwtTokenService`.
- [x] **T3: Testes do CRUD de Usuários (Application).** Implementar testes unitários para `CreateUserCommandHandler`, `GetUserByIdQueryHandler`, `UpdateUserCommandHandler` e `DeleteUserCommandHandler`, cobrindo tanto retornos válidos quanto cenários em que o usuário não é localizado.
- [x] **T4: Testes de Segurança (Infrastructure).** Criar testes para o `PasswordHasher` (garantindo que senhas idênticas geram hashes diferentes com salt e que a verificação funciona) e para o `JwtTokenService` (garantindo geração de token válido e presença de claims).
- [x] **T5: Execução, Validação e Ajustes.** Executar `dotnet test` na raiz da solução, validar que todos os testes passam sem falhas e atualizar a documentação de cobertura.
  - **Resultado final:** 22 testes aprovados, 0 falhas, em 9 classes de teste (`dotnet test`, 1,96 s).

| Área | Classe de teste | Testes aprovados |
|---|---|---|
| Application/Auth | `RegisterUserCommandHandlerTests` | 3 |
| Application/Auth | `LoginCommandHandlerTests` | 3 |
| Application/Users | `CreateUserCommandHandlerTests` | 1 |
| Application/Users | `GetUserByIdQueryHandlerTests` | 2 |
| Application/Users | `GetAllUsersQueryHandlerTests` | 2 |
| Application/Users | `UpdateUserCommandHandlerTests` | 2 |
| Application/Users | `DeleteUserCommandHandlerTests` | 2 |
| Infrastructure | `PasswordHasherTests` | 3 |
| Infrastructure | `JwtTokenServiceTests` | 4 |
| **Total** | **9 classes** | **22** |