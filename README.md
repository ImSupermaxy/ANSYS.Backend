# Projeto ANSYS (Asa Noturna System) - Backend (trabalho-api)

## Descrição

Um sistema integrado para gerenciamentos de pedidos, cardápio e usuários (clientes / funcionários).
Este é o projeto do backend, com configurações das rotas (endpoints) e regras para inserção, edição e obtenção de dados.

## Tecnologias Utilizadas

- C# .NET 8.0
- Swagger
- ASP.NET MVC
- Swashbuckle.AspNetCore 6.6.2
- MediatR 13.0.0 (em ANSYS.API e ANSYS.Application)
- Entity Framework Core v9.0.10 (em ANSYS.Domain)
- Dapper 2.1.66 (em ANSYS.Infrastructure)
- Microsoft.Extensions.Configuration.Abstractions 9.0.10 (em ANSYS.Infrastructure)
- Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4 (caso utilize Posgres, em ANSYS.Infrastructure)

## Pré-requisitos

- sdk .net 8
- IDE (a sua escolha) que rode o sdk do .net

(Observação: é recomendável utilizar o Visual Studio Community por ser mais versátil na instalação das tecnologias)

## Instalação

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/ImSupermaxy/ANSYS.Backend.git
   ```

2. **Navegue até o diretório do projeto:**

   ```bash
   cd ANSYS.Backend
   ```

3. **Configure o banco de dados:**
   
   Edite o arquivo [appsettings.Development.json](ANSYS.API/appsettings.Development.json) na sessão "ConnectionStrings" com as configurações do seu banco de dados (Postgress).

   Edite também a sua escolha no arquivo [DependencyInjection.cs](ANSYS.Infrastructure/Dependency/DependencyInjection.cs) descomente o banco de dados a sua preferência dentro do método de "AddPersistence".
   Caso queira utilize no lugar do banco com conexão do entity framework, utilize o banco de dados em tempo de execução, ainda no arquivo de [DependencyInjection.cs](ANSYS.Infrastructure/Dependency/DependencyInjection.cs), altere a linha 53 de "false" para "true".

5. **Compile e execute o projeto:**

  - No Visual Studio Community
    "Ctrl + Shift + b" e logo após "Ctrl + f5"
    ou
    Clique com o botão direito do mouse na solução do projeto ("Solution 'ANSYS.Backend'"), a direita e "Build Solution", após isso, no menu acima, com o ícone de "play" verde, verifique se ao lado a opção "IIS Express" está selecionada, caso não selecione-a, e então clique no botão verde de play.
    A solução está dentro do menu "Solution Explorer", caso não a veja o menu vá em "View" no menu acima, e selecione a opção "Solution Explorer" ou aperte "Ctrl + alt + l"
    
    A API estará disponível em `http://localhost:44388`.

5. **Instale as dependências do projeto**

   - No terminal execute o comando `dotnet tool install --global dotnet-ef`
   - No terminal execute o comando `dotnet restore` para baixar as dependências de pacotes do projeto.
   - Ainda no terminal execute o comando `dotnet ef database update --project ANSYS.Infrastructure --startup-project ANSYS.API` para atualizar o banco de dados vinculado ao EntityFramework

     **Observação**
     Configure uma conexão existente com um banco de dados, o EntityFramework, configurará as tabelas necessárias após isso.
  

## Documentação da API (Swagger)

A documentação da API pode ser acessada por meio do Swagger. Após iniciar o backend, você pode acessar a documentação por meio da seguinte URL:

[/swagger/index.html](http://localhost:44388//swagger/index.html)

## Endpoints

Abaixo está a descrição dos principais endpoints da API:

{Alterar os endpoints conforme os endpoints do projeto}

### **1. GET /api/v1/usuario**

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

### **2. POST /api/v1/usuario**

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

### **3. GET /api/v1/usuario/{id}**

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

### **4. PUT /api/v1/usuario/{id}**

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

### **5. DELETE /api/v1/usuario/{id}**

- **Descrição:** Remove um usuário pelo ID.
- **Parâmetros de Caminho:**
  - `id`: ID do usuário.
- **Resposta:**
  - **204 No Content**
  - **404 Not Found** (se o usuário não for encontrado)
