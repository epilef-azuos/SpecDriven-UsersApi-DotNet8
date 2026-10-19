# FEATURE: Suíte de Testes Unitários Automatizados

## OBJETIVO
Garantir a integridade das regras de negócio, a confiabilidade dos casos de uso (CQRS) e a robustez dos componentes de segurança (JWT e Criptografia), prevenindo regressões futuras no código de produção.

## ESCOPO DOS TESTES
- **Camada Application**:
  - Testar fluxos de sucesso e falha de todos os Handlers de Commands e Queries.
  - Validar tratamento de erros e lançamentos de exceções esperadas (ex.: credenciais inválidas, usuário não encontrado, conflito de e-mail).
- **Camada Infrastructure**:
  - Validar a lógica de geração e verificação de hash do serviço `PasswordHasher`.
  - Validar a emissão de tokens e inclusão de claims pelo `JwtTokenService`.

## RESTRIÇÕES E CRITÉRIOS DE ACEITAÇÃO
- Testes unitários devem rodar de forma isolada e em memória, sem tocar no banco de dados SQLite real ou abrir portas de rede.
- Dependências externas e contratos de repositório devem ser simulados via mocks.
- O comando `dotnet test` deve executar com 100% de sucesso e zero falhas na solução.