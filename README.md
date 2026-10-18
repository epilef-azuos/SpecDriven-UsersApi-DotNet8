# ApiUsuarios - Clean Architecture & Spec-Driven Development (.NET 8)

API RESTful para gestão e manutenção de utilizadores desenvolvida em **.NET 8**, adotando os princípios de **Clean Architecture**, **SOLID** e o padrão **CQRS** com **MediatR**, persistida em **SQLite** através do **Entity Framework Core**.

O projeto foi totalmente concebido e implementado utilizando a metodologia **Spec-Driven Development (SDD)**, estabelecendo governança arquitetural estrita sobre agentes de IA por meio de especificações versionadas em Markdown.

---

## 🏛️ Arquitetura da Solução

A estrutura do projeto respeita a divisão em quatro camadas concêntricas com inversão de dependências:

* **`ApiUsuarios.Domain`**: O núcleo isolado da aplicação. Contém a entidade `User` e a interface `IUserRepository`. Sem qualquer dependência de frameworks externos ou bibliotecas de persistência.
* **`ApiUsuarios.Application`**: Contém os casos de uso orquestrados pelo padrão CQRS via **MediatR** (`Commands`, `Queries`, `Handlers`) e DTOs de transporte. Depende exclusivamente do Domínio.
* **`ApiUsuarios.Infrastructure`**: Implementação técnica de persistência com **Entity Framework Core** e **SQLite** (`AppDbContext`, migrações e a implementação concreta de `UserRepository`).
* **`ApiUsuarios.API`**: Camada de apresentação com controladores RESTful expondo as operações do CRUD e efetuando a injeção de dependências nativa.

```text
ApiUsuarios/
├── src/
│   ├── ApiUsuarios.Domain/         # Entidades puras e Interfaces (Ports)
│   ├── ApiUsuarios.Application/    # Use Cases (Commands, Queries, DTOs via MediatR)
│   ├── ApiUsuarios.Infrastructure/ # EF Core, SQLite Context e Repositórios (Adapters)
│   └── ApiUsuarios.API/            # Controladores RESTful e Configuração
└── specs/
    └── api-usuarios/               # Artefatos SDD (SPEC, PLAN, constitution, TASKS)
```

---

## 🛠️ Tecnologias e Bibliotecas

* **.NET 8 SDK** (C# 12)
* **Entity Framework Core 8** (Code-First Migrations)
* **SQLite** (Base de dados relacional embutida)
* **MediatR** (Desacoplamento de Casos de Uso e CQRS)
* **Swagger / OpenAPI** (Documentação interativa de API)

---

## 🚀 Como Executar o Projeto

### Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado no ambiente.

### Passos de Execução

1. **Clonar o repositório:**
   ```bash
   git clone https://github.com/epilef-azuos/ApiUsuarios-CleanArch-SDD.git
   cd ApiUsuarios-CleanArch-SDD
   ```

2. **Restaurar dependências e compilar a solução:**
   ```bash
   dotnet restore
   dotnet build
   ```

3. **Aplicar as migrações na base de dados SQLite:**
   ```bash
   dotnet ef database update --project src/ApiUsuarios.Infrastructure --startup-project src/ApiUsuarios.API
   ```

4. **Executar a API:**
   ```bash
   dotnet run --project src/ApiUsuarios.API
   ```

5. **Acessar a documentação interativa:**
   Abra o navegador em: `https://localhost:7143/swagger` (ou a porta correspondente exibida no terminal).

---

## 📑 Endpoints Disponíveis

| Método | Rota | Descrição | Resposta de Sucesso |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/Users` | Cria um novo utilizador | `201 Created` |
| `GET` | `/api/Users` | Lista todos os utilizadores cadastrados | `200 OK` |
| `GET` | `/api/Users/{id}` | Busca utilizador por identificador (GUID) | `200 OK` / `404 Not Found` |
| `PUT` | `/api/Users/{id}` | Atualiza dados cadastrais de um utilizador | `204 No Content` / `404 Not Found` |
| `DELETE` | `/api/Users/{id}` | Remove um utilizador da base de dados | `204 No Content` / `404 Not Found` |

---

## 🧠 Metodologia: Spec-Driven Development (SDD)

Em vez de geração direta e não supervisionada de código por IA, o desenvolvimento foi orientado por quatro artefatos de controle:

1. **`SPEC.md`**: Definição da intenção de negócio, escopo funcional e critérios de aceite.
2. **`PLAN.md`**: Escolhas de arquitetura técnica, padrões de projeto e divisão física de módulos.
3. **`constitution.md`**: Regras imutáveis de governança (ex.: isolamento total do Domínio e proibição de acoplamento direto nos Controllers).
4. **`TASKS.md`**: Roteiro atômico, incremental e sequencial de execução operacional (T1 a T5).

---

## 🗺️ Roadmap de Evolução

- [x] CRUD básico com Clean Architecture e SQLite
- [ ] Implementação de Autenticação e Autorização via JWT (Role-Based)
- [ ] Cifragem de senhas com algoritmo BCrypt
- [ ] Pipeline de CI/CD automatizada com GitHub Actions
- [ ] Suíte de testes automatizados com xUnit e FluentAssertions
