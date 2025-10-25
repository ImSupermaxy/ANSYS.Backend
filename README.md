# Projeto ANSYS (Asa Noturna System) - Backend

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
- Moq 4.20.72 (em CrudApplicationTests)

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
   Caso queira utilize no lugar do banco com conexão do entity framework, utilize o banco de dados em tempo de execução, ainda no arquivo de [DependencyInjection.cs](ANSYS.Infrastructure/Dependency/DependencyInjection.cs), altere a linha 49 de "false" para "true".

5. **Compile e execute o projeto:**

  - No Visual Studio Community
    "Ctrl + Shift + b" e logo após "Ctrl + f5"
    ou
    Clique com o botão direito do mouse na solução do projeto ("Solution 'ANSYS.Backend'"), a direita e "Build Solution", após isso, no menu acima, com o ícone de "play" verde, verifique se ao lado a opção "IIS Express" está selecionada, caso não selecione-a, e então clique no botão verde de play.
    A solução está dentro do menu "Solution Explorer", caso não a veja o menu vá em "View" no menu acima, e selecione a opção "Solution Explorer" ou aperte "Ctrl + alt + l"
    
    A API estará disponível em `http://localhost:44388`.

5. **Instale as dependências do projeto**

   - No terminal execute os comandos: `dotnet new tool-manifest` e após `dotnet tool install dotnet-ef`
   - No terminal execute o comando `dotnet restore` para baixar as dependências de pacotes do projeto.
   - Ainda No terminal execute o comando `dotnet ef` para visualizar se o command-line tools do entity framework foi baixado corretamente (caso sim irá aparecer um unicórinio no começo da mensagem).
   - Ainda no terminal execute o comando `dotnet ef database update --project ANSYS.Infrastructure --startup-project ANSYS.API` para atualizar o banco de dados vinculado ao EntityFramework

     **Observação**
     Configure uma conexão existente com um banco de dados, o EntityFramework, configurará as tabelas necessárias após isso. Ou utilize o banco de dados em tempo de execução.
     Caso de um erro na execução do último comando, utilize o `dotnet build`, e visualize o erro, caso ele seja por conta da .dll do ANSYS.API estar utilizando para um outro processo, pare a execução do projeto e ou reinicie o visual studio / ide  
  

## Documentação da API (Swagger)

A documentação da API pode ser acessada por meio do Swagger. Após iniciar o backend, você pode acessar a documentação por meio da seguinte URL:

[/swagger/index.html](http://localhost:44388//swagger/index.html)

## Endpoints

Abaixo está a descrição dos principais endpoints da API:

{Alterar os endpoints conforme os endpoints do projeto}

### **1. GET /api/v1/usuario**

- **Descrição:** Obtem todos os usuários cadastrados..
- **Resposta:**
  - **200 OK**
    ```json
    [
       {
          "nome": "Master",
          "email": "master@ansys.com",
          "perfil": 1,
          "id": 1
       },
       {
          "nome": "Administrador",
          "email": "admin@ansys.com",
          "perfil": 1,
          "id": 2
       },
       //...
    ]
    ```

### **2. POST /api/v1/usuario**

- **Descrição:** Insere um novo usuário.
- **Corpo da Requisição:**
  ```json
  {
    "nome": "Lucas",
    "email": "Lucas@exemplo.com"
  }
  ```
- **Resposta:**
  - **201 Created**
    ```json
    3
    ```
  - **400 Bad Request** (se der alguma exceção ou o email do usuário já estiver cadastrado)

### **3. GET /api/v1/usuario/{id}**

- **Descrição:** Obtem um usuário cadastrado pelo seu identificador.
- **Parâmetros de Caminho:**
  - `id`: Identificador do usuário.
- **Resposta:**
  - **200 OK**
    ```json
    {
       "nome": "Master",
        "email": "master@ansys.com",
        "perfil": 1,
        "id": 1
    }
    ```
  - **404 Not Found** (se o usuário não for encontrado)

### **4. POST /api/v1/pedido**

- **Descrição:** Insere um novo pedido.
- **Corpo da Requisição:**
  ```json
  {
     "clienteId": 1,
     "itens": [
       {
         "quantidade": 5,
         "subtotal": 10,
         "taxa": 5,
         "desconto": 1
       }
     ]
   }
  ```
- **Resposta:**
  - **201 Created**
    ```json
    1
    ```

### **5. GET /api/v1/pedido/{id}**

- **Descrição:** Obtem um pedido cadastrado pelo seu identificador.
- **Parâmetros de Caminho:**
  - `id`: Identificador do pedido.
- **Resposta:**
  - **200 OK**
    ```json
    {
       "clienteId": 1,
        "subtotal": 50,
        "taxa": 25,
        "desconto": 5,
        "total": 70,
        "status": 2,
        "dataInserido": "10:14:37.6682497",
        "dataModificado": "10:14:37.6682497",
        "cliente": null,
        "itens": null,
        "id": 1
    }
    ```
  - **404 Not Found** (se o pedido não for encontrado)
 
### **6. PUT /api/v1/aprovar**

- **Descrição:** Aprova um pedido existente.
- **Corpo da Requisição:**
  ```json
  {
    "id": "1",
  }
  ```
- **Resposta:**
  - **200 OK**
  - **400 Bad Request** (se o pedido não for encontrado ou se caso o status do pedido não puder ser alterado)
 
### **6. DELETE /api/v1/cancelar**

- **Descrição:** Cancela um pedido existente.
- **Corpo da Requisição:**
  ```json
  {
    "id": "1",
  }
  ```
- **Resposta:**
  - **200 OK**
  - **400 Bad Request** (se o pedido não for encontrado ou se caso o status do pedido não puder ser alterado)
