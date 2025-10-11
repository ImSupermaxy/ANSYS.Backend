# Projeto ANSYS (Asa Noturna System) - Backend (trabalho-api)

## Descrição

Um sistema integrado para gerenciamentos de pedidos, cardápio e usuários (clientes / funcionários).
Este é o projeto do backend, com configurações das rotas (endpoints) e regras para inserção, edição e obtenção de dados.

## Tecnologias Utilizadas

- C# .NET 8.0
- swagger
- ASP.NET MVC
- MediatR (em ANSYS.API e ANSYS.Application)
- Npgsql (caso utilize Posgres, em ANSYS.Infrastructure)
- Mysql.Data (caso utilize Mysql, em ANSYS.Infrastructure)
- Dapper (em ANSYS.Infrastructure)

## Pré-requisitos

- sdk .net 8
- IDE (a sua escolha) que rode o sdk do .net

(Observação: é recomendável utilizar o Visual Studio Community por ser mais versátil na instalação das tecnologias)

## Instalação

1. **Clone o repositório:**

   ```bash
   git clone https://link-do-repo.git
   ```

2. **Navegue até o diretório do projeto:**

   ```bash
   cd diretorio-do-projeto
   ```

3. **Configure o banco de dados:**

   Edite o arquivo [appsettings.Development.json](ANSYS.API/appsettings.Development.json) na sessão "ConnectionStrings" com as configurações do seu banco de dados (Mysql ou Postgress).

   Edite também a sua escolha no arquivo [DependencyInjection.cs](ANSYS.API/Dependency/DependencyInjection.cs) descomente o banco de dados a sua preferência dentro do método de "AddPersistence".

4. **Compile e execute o projeto:**

  - No Visual Studio Community
    Ctrl + Shift + b
    ou
    Clique com o botão direito do mouse na solução do projeto ("Solution 'ANSYS.Backend'"), a direita. 
    A solução está dentro do menu "Solution Explorer", caso não veja o menu vá em "View" no menu acima, e selecione a opção "Solution Explorer" ou aperte "Ctrl + alt + l"
    
    A API estará disponível em `http://localhost:7064`.

5. **Instale as dependências do projeto**

  - Abra o "Nuget Package Manager" no menu acima em "Tools", e selecione a opção "Nuget Package Manager" e "Nuget Package Manager for solution" ou clique com o botão direito na solução do projeto, e selecione a opção "Nuget Package Manager for solution"
  - Instale as bibliotecas de acordo com o projeto expecificado em "Tecnologias Utilizadas"

## Documentação da API (Swagger)

A documentação da API pode ser acessada por meio do Swagger. Após iniciar o backend, você pode acessar a documentação por meio da seguinte URL:

[/swagger/index.html](http://localhost:44388//swagger/index.html)

## Endpoints

Abaixo está a descrição dos principais endpoints da API:

{Alterar os endpoints conforme os endpoints do projeto}

### **1. GET /api/usuarios**

- **Descrição:** Retorna uma lista de usuários.
- **Parâmetros de Consulta:**
  - `page` (opcional): Número da página.
  - `size` (opcional): Número de itens por página.
- **Resposta:**
  - **200 OK**
    ```json
    [
      {
        "id": 1,
        "nome": "João",
        "email": "joao@exemplo.com"
      },
      // ...
    ]
    ```

### **2. POST /api/usuarios**

- **Descrição:** Cria um novo usuário.
- **Corpo da Requisição:**
  ```json
  {
    "nome": "Maria",
    "email": "maria@exemplo.com"
  }
  ```
- **Resposta:**
  - **201 Created**
    ```json
    {
      "id": 2,
      "nome": "Maria",
      "email": "maria@exemplo.com"
    }
    ```

### **3. GET /api/usuarios/{id}**

- **Descrição:** Retorna um usuário específico pelo ID.
- **Parâmetros de Caminho:**
  - `id`: ID do usuário.
- **Resposta:**
  - **200 OK**
    ```json
    {
      "id": 1,
      "nome": "João",
      "email": "joao@exemplo.com"
    }
    ```
  - **404 Not Found** (se o usuário não for encontrado)

### **4. PUT /api/usuarios/{id}**

- **Descrição:** Atualiza um usuário existente.
- **Corpo da Requisição:**
  ```json
  {
    "nome": "João Atualizado",
    "email": "joaoatualizado@exemplo.com"
  }
  ```
- **Parâmetros de Caminho:**
  - `id`: ID do usuário.
- **Resposta:**
  - **200 OK**
    ```json
    {
      "id": 1,
      "nome": "João Atualizado",
      "email": "joaoatualizado@exemplo.com"
    }
    ```
  - **404 Not Found** (se o usuário não for encontrado)

### **5. DELETE /api/usuarios/{id}**

- **Descrição:** Remove um usuário pelo ID.
- **Parâmetros de Caminho:**
  - `id`: ID do usuário.
- **Resposta:**
  - **204 No Content**
  - **404 Not Found** (se o usuário não for encontrado)
