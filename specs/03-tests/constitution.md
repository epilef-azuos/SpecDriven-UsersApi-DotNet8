# CONSTITUIÇÃO DA SUÍTE DE TESTES (REGRAS IMUTÁVEIS)

1. **Isolamento Absoluto (Zero I/O)**: Testes unitários NUNCA devem acessar arquivos em disco, conexões de banco de dados ou serviços externos. Qualquer dependência deve ser mockada com `Moq`.
2. **Padrão AAA (Arrange, Act, Assert)**: Todo método de teste deve ser dividido explicitamente em três blocos bem definidos, mantendo a leitura clara e padronizada.
3. **Padrão de Nomenclatura**: Os métodos de teste devem seguir rigorosamente o padrão:
   `NomeDoMetodo_CenarioDeEntrada_ResultadoEsperado`
   *(Exemplo: `Handle_ValidCredentials_ShouldReturnAuthResponse`)*
4. **Legibilidade com FluentAssertions**: Utilizar exclusivamente a sintaxe do `FluentAssertions` para validações (`result.Should().NotBeNull()`, `result.Token.Should().NotBeEmpty()`).
5. **Intocabilidade do Domínio**: É proibido alterar visibilidade ou propriedades de classes de produção apenas para facilitar os testes, a menos que isso seja estritamente necessário para injeção de dependência via construtor.
specs/03-tests/TASK.md
