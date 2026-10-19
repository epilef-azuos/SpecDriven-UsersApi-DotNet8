# FEATURE: Autenticação e Autorização via JWT

## OBJETIVO
Proteger a API existente implementando autenticação baseada em JSON Web Tokens (JWT) e hashing seguro de palavras-passe, garantindo controle de acesso aos recursos de utilizadores.

## REQUISITOS DE NEGÓCIO
- O sistema deve permitir que novos utilizadores se registem informando Nome, Email, Palavra-passe e Perfil (Role: "Admin" ou "User").
- O sistema deve disponibilizar um endpoint de Login que valide as credenciais e retorne um Bearer Token JWT.
- A palavra-passe nunca pode ser armazenada ou trafegada em texto limpo.
- Todos os endpoints existentes de `/api/Users` devem ser protegidos contra acessos não autenticados (retornando HTTP 401 Unauthorized).

## REQUISITOS DE NÃO-REGRESSÃO
- Os 5 endpoints existentes do CRUD de utilizadores devem manter as suas assinaturas, payloads e rotas originais, apenas passando a exigir o token de autenticação.

## CRITÉRIOS DE ACEITAÇÃO
- Tentativa de acesso a `GET /api/Users` sem token retorna 401 Unauthorized.
- Login com credenciais válidas retorna status 200 com token JWT e tempo de expiração.
- Login com credenciais inválidas retorna 401 Unauthorized com mensagem clara de erro.