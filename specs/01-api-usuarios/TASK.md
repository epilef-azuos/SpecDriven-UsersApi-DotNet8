# TAREFAS DE EXECUÇÃO

- [x] **T1: Criar a Solução e Projetos.** Criar a solution `ApiUsuarios.sln` e os 4 projetos (Domain, Application, Infrastructure, API). Fazer as referências corretas entre os projetos conforme o PLAN.md.
- [x] **T2: Implementar o Domain.** Criar a entidade `User` (Id, Name, Email) e a interface `IUserRepository` (Add, GetById, GetAll, Update, Delete).
- [x] **T3: Implementar o Application (MediatR).** Adicionar o pacote MediatR. Criar os DTOs e os Handlers para CreateUser, GetUserById, GetAllUsers, UpdateUser e DeleteUser.
- [x] **T4: Implementar a Infrastructure (SQLite).** Adicionar os pacotes do EF Core e EF Core SQLite. Criar o `AppDbContext`, configurar a connection string e implementar o `UserRepository`.
- [x] **T5: Implementar a API e Testar.** Configurar o Program.cs (DI, SQLite, MediatR), criar o `UsersController` com os endpoints REST e rodar o `dotnet ef migrations add InitialCreate` e `database update`.