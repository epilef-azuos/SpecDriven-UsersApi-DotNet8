# PLANO TÉCNICO E ARQUITETURA

## STACK TECNOLÓGICA
- .NET 8 (Web API)
- Banco de Dados: SQLite
- ORM: Entity Framework Core
- Padrão de Design: CQRS com MediatR

## ARQUITETURA (Clean Architecture)
A solução deve ser dividida em 4 camadas físicas (Projetos C#):
1. **ApiUsuarios.Domain**: Entidades (User.cs) e Interfaces (IUserRepository.cs). Sem dependências externas.
2. **ApiUsuarios.Application**: Casos de uso (Commands/Queries usando MediatR) e DTOs. Depende apenas do Domain.
3. **ApiUsuarios.Infrastructure**: Contexto do EF Core, Migrations e implementação do UserRepository. Depende do Application e Domain.
4. **ApiUsuarios.API**: Controllers RESTful e injeção de dependências. Depende do Application e Infrastructure.