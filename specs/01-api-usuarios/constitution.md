# CONSTITUIÇÃO DO PROJETO (REGRAS IMUTÁVEIS)

1. **Clean Architecture Estrita**: A camada `Domain` NUNCA pode ter referências a pacotes do Entity Framework, ASP.NET ou qualquer framework externo.
2. **Isolamento de Banco de Dados**: NUNCA aceder ao `DbContext` ou a entidades do EF Core diretamente nos Controllers da API. Usar sempre o MediatR.
3. **Injeção de Dependências**: Todos os serviços e repositórios devem ser registados no contentor de DI nativo do .NET.
4. **Respostas HTTP Padrão**: Os Controllers devem retornar códigos HTTP corretos (200 OK, 201 Created, 404 Not Found, 400 Bad Request).
5. **Nomenclatura**: Usar o idioma Inglês para o código (Classes, métodos, variáveis) e Português para mensagens de erro/logs.