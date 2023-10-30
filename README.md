# API de Filmes

Esta simples API oferece funcionalidades para criar, listar, atualizar e excluir informações de filmes, bem como realizar o registro, login e criação de funções de usuário, em um banco de dados SQL Server.

# Recursos Principais

## Operações de Filmes:

- Recuperar informações de filmes por ID ou listar todos os filmes disponíveis.
- Adicionar novos filmes com validação de dados.
- Atualizar informações de filmes existentes.
- Excluir filmes com base em seus IDs.

## Operações de Autenticação:

- Registrar novos usuários com atribuição de funções.
- Realizar login de usuários e fornecer tokens de autenticação.
- Criar funções de usuário personalizadas.

## Autenticação

Algumas operações da API de Filmes e Autenticação requerem autenticação, principalmente aquelas relacionadas à criação, atualização e exclusão de filmes. A autenticação é realizada por meio de tokens JWT (JSON Web Tokens) que devem ser incluídos nos cabeçalhos das solicitações.

## Endpoints Disponíveis

### Criar Função

Cria uma nova função.

- URL: /api/v1/create-role
- Método HTTP: POST
- Cabeçalho de Autenticação: Não é necessário autenticação.
- Corpo da Solicitação (JSON):

```
"roleName": "Nome da Nova Função"
```

- Resposta de Sucesso (200 OK):

```
{
  "Message": "Função criada com sucesso."
}
```

- Resposta de Erro (400 Bad Request):

```
{
  "Message": "Ocorreu um erro ao criar a função."
}
```

- Resposta de Erro (500 Internal Server Error):

```
{
  "Message": "Ocorreu um erro durante a criação da função",
  "Error": "Mensagem de erro específica"
}
```

### Registrar Usuário

Registra um novo usuário.

- URL: /api/v1/register
- Método HTTP: POST
- Cabeçalho de Autenticação: Não é necessário autenticação.
- Corpo da Solicitação (JSON):

```
{
"UserName": "Nome do Usuário",
"Email": "email@example.com",
"Password": "senha",
"Role": "Nome da Função"
}
```

- Resposta de Sucesso (200 OK):

```
{
"token": "Token de Autenticação"
}
```

- Resposta de Erro (400 Bad Request):

```
{
"Message": "Os campos não foram corretamente preenchidos",
"Errors": ["Erro 1", "Erro 2", ...]
}
```

- Resposta de Erro (500 Internal Server Error):

```
{
"Message": "Ocorreu um erro durante o registro.",
"Error": "Mensagem de erro específica"
}
```

### Login de Usuário

- URL: /api/v1/login
- Método HTTP: POST
- Cabeçalho de Autenticação: Não é necessário autenticação.
- Corpo da Solicitação (JSON):

```
{
  "Email": "email@example.com",
  "Password": "senha"
}
```

- Resposta de Sucesso (200 OK):

```
{
"token": "Token de Autenticação"
}
```

- Resposta de Erro (401 Unauthorized):

```
{
"Message": "Credenciais inválidas."
}
```

- Resposta de Erro (500 Internal Server Error):

```
{
"Message": "Ocorreu um erro durante o login.",
"Error": "Mensagem de erro específica"
}
```

### Recuperar Filme por ID

Recupera um filme com base em seu ID.

- URL: /api/v1/movie/{id}
- Método HTTP: GET
- Parâmetros: id (int) - O ID do filme que deseja recuperar.

- Resposta de Sucesso (200 OK):

```
{
"Id": "Id do filme",
"Name": "Nome do filme",
"Director": "Nome do diretor",
"Category": "Categoria",
"Year": Ano do filme,
"CreatedAt": "2023-10-27T23:21:22.3866667",
"UpdatedAt": "2023-10-29T18:31:31.74"
}
```

- Resposta de Erro (404 Not Found):

```
{
"Message": "Nenhum filme encontrado com o ID fornecido."
}
```

### Recuperar Todos os Filmes

Recupera todos os filmes disponíveis.

- URL: /api/v1/movies
- Método HTTP: GET

- Resposta de Sucesso (200 OK):

```
[
{
"Id": "Id do filme",
"Name": "Nome do filme",
"Director": "Nome do diretor",
"Category": "Categoria",
"Year": Ano do filme,
"CreatedAt": "2023-10-27T23:21:22.3866667",
"UpdatedAt": "2023-10-29T18:31:31.74"
},
{
"Id": "Id do filme",
"Name": "Nome do filme",
"Director": "Nome do diretor",
"Category": "Categoria",
"Year": Ano do filme,
"CreatedAt": "2023-10-27T23:21:22.3866667",
"UpdatedAt": "2023-10-29T18:31:31.74"
}
]
```

- Resposta de Erro (404 Not Found):

```
{
"Message": "Nenhum filme encontrado."
}
```

### Adicionar Filme (Requer Autenticação)

Adiciona um novo filme.

- URL: /api/v1/add-movie
- Método HTTP: POST
- Cabeçalho de Autenticação: Requer autenticação com a política "Admin".
- Corpo da Solicitação (JSON):

```
{
"Name": "Nome do filme",
"Director": "Nome do diretor",
"Category": "Categoria",
"Year": Ano do filme,
}
```

- Resposta de Sucesso (200 OK):

```
{
"Message": "O filme foi adicionado com sucesso",
"Movie": {
"Id": "Id do filme",
"Name": "Nome do filme",
"Director": "Nome do diretor",
"Category": "Categoria",
"Year": Ano do filme,
"CreatedAt": "2023-10-27T23:21:22.3866667",
"UpdatedAt": "2023-10-29T18:31:31.74"
}
}
```

- Resposta de Erro (400 Bad Request):

{
"Message": "Os campos não foram corretamente preenchidos",
"Erros": ["Erro 1", "Erro 2", ...]
}

- Resposta de Erro (500 Internal Server Error):

```
{
"Message": "Ocorreu um erro durante adição do filme.",
"Error": "Mensagem de erro específica"
}
```

### Atualizar Filme por ID (Requer Autenticação)

Atualiza um filme existente com base em seu ID.

- URL: /api/v1/update-movie/{id}
- Método HTTP: PUT
- Cabeçalho de Autenticação: Requer autenticação com a política "Admin".
- Parâmetros: id (int) - O ID do filme que deseja atualizar.
- Corpo da Solicitação (JSON):

```
{
"Name": "Novo nome do filme",
"Director": "Novo nome do diretor",
"Category": "Nova categoria",
"Year": Novo ano do filme,
}
```

- Resposta de Sucesso (200 OK):

```
{
"Message": "O filme foi atualizado com sucesso",
"Movie": {
"Name": "Novo nome do filme",
"Director": "Novo nome do diretor",
"Category": "Nova categoria",
"Year": Novo ano do filme,
}
}
```

- Resposta de Erro (400 Bad Request):

```
{
"Message": "Os campos não foram corretamente preenchidos",
"Erros": ["Erro 1", "Erro 2", ...]
}
```

- Resposta de Erro (500 Internal Server Error):

```
{
"Message": "Ocorreu um erro durante a atualização do filme.",
"Error": "Mensagem de erro específica"
}
```

### Deletar Filme por ID (Requer Autenticação)

Exclui um filme com base em seu ID.

- URL: /api/v1/delete-movie/{id}
- Método HTTP: DELETE
- Cabeçalho de Autenticação: Requer autenticação com a política "Admin".
- Parâmetros: id (int) - O ID do filme que deseja deletar.

- Resposta de Sucesso (200 OK):

```
{
"Message": "O filme com o ID fornecido foi deletado com sucesso."
}
```

- Resposta de Erro (500 Internal Server Error):

```
{
"Message": "Erro ao tentar deletar o filme com o ID fornecido",
"Error": "Mensagem de erro específica"
}
```
