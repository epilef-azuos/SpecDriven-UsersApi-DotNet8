# PLANO TÉCNICO E ARQUITETURA DE TESTES

## STACK DE TESTES
- **Framework de Testes**: `xUnit`
- **Biblioteca de Mocks**: `Moq`
- **Biblioteca de Asserções**: `FluentAssertions`
- **Test Runner**: `Microsoft.NET.Test.Sdk` + `xunit.runner.visualstudio`

## ESTRUTURA DO PROJETO DE TESTES
O projeto será criado sob o diretório `tests/` e registrado na solução `ApiUsuarios.sln`:

```text
ApiUsuarios/
├── src/
│   ├── ApiUsuarios.Domain/
│   ├── ApiUsuarios.Application/
│   ├── ApiUsuarios.Infrastructure/
│   └── ApiUsuarios.API/
└── tests/
    └── ApiUsuarios.UnitTests/
        ├── Application/
        │   ├── Auth/
        │   │   ├── LoginCommandHandlerTests.cs
        │   │   └── RegisterUserCommandHandlerTests.cs
        │   └── Users/
        │       ├── CreateUserCommandHandlerTests.cs
        │       ├── GetUserByIdQueryHandlerTests.cs
        │       ├── UpdateUserCommandHandlerTests.cs
        │       └── DeleteUserCommandHandlerTests.cs
        └── Infrastructure/
            ├── PasswordHasherTests.cs
            └── JwtTokenServiceTests.cs

## DEPENDÊNCIAS DO PROJETO

O ApiUsuarios.UnitTests referenciará:

ApiUsuarios.Domain

ApiUsuarios.Application

ApiUsuarios.Infrastructure