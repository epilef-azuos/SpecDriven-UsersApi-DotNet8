# CONSTITUIÇÃO DO PROJETO - SEGURANÇA E JWT

1. **Segredo Seguro**: Chaves criptográficas do JWT NUNCA podem ser codificadas diretamente nas classes (*hardcoded*). Devem ser lidas do `appsettings.json`.
2. **Criptografia Obrigatória**: NUNCA comparar ou salvar passwords em texto plano. Usar exclusivamente `BCrypt.Verify` e `BCrypt.HashPassword`.
3. **Preservação de Código**: É proibido reescrever ficheiros funcionais do zero. Faça alterações cirúrgicas e preserve a lógica do CRUD existente.
4. **Isolamento de Apresentação**: O `AuthController` e o `UsersController` não contêm regras de negócio nem geração direta de tokens; tudo deve ser despachado via MediatR (`ISender`).
5. **Erros Neutros**: Em falhas de autenticação, utilize mensagens genéricas (ex.: "Credenciais inválidas") para evitar enumeração de contas.