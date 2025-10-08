

# **API Fornecedor Completo**

Este repositório contém a solução completa para o projeto **API Fornecedor**, que gerencia fornecedores e seus produtos associados. A arquitetura da aplicação é dividida em três projetos principais: **APIFornecedor**, **Business** e **Data**, seguindo o princípio de separação de responsabilidades.

---

## **Estrutura da Solução**

A solução é organizada da seguinte forma:

- **APIFornecedor**:
  - Ponto de entrada da API.
  - Configurações para **AutoMapper** e **Injeção de Dependência**.
  - **Controllers**: Lidam com as requisições HTTP (GET, POST, PUT, DELETE).
  - **ViewModels**: Modelos de dados utilizados na comunicação entre a API e o cliente.

- **Business**:
  - Contém a **lógica de negócio**.
  - **Interfaces**: Definições de contratos para serviços e repositórios.
  - **Models**: Entidades do domínio (e.g., Fornecedor, Produto).
  - **Services**: Implementação da lógica de negócio.
  - **Notificacoes**: Sistema de notificações para validações e erros.

- **Data**:
  - Camada de **acesso a dados**.
  - **Context**: Representa o banco de dados (usando **Entity Framework Core**).
  - **Mappings**: Configurações de mapeamento para as entidades.
  - **Migrations**: Gerenciamento das migrações do banco de dados.
  - **Repository**: Padrão de repositório para abstrair o acesso a dados.

---

## **Endpoint Principal**

O endpoint principal para gerenciar fornecedores é:

`https://localhost:7202/api/fornecedores`

---

## **Exemplo de Requisição (POST)**

Para criar um novo fornecedor, utilize o método **POST** enviando o seguinte corpo da requisição no formato JSON:

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "nome": "Nome do Fornecedor",
  "documento": "09678595737",
  "tipoFornecedor": 1,
  "endereco": {
    "id": "1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d",
    "logradouro": "Rua do Comércio",
    "numero": "123",
    "complemento": "Bloco A",
    "bairro": "Centro",
    "cep": "12345678",
    "cidade": "São Paulo",
    "estado": "SP",
    "fornecedorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "ativo": true,
  "produtos": []
}
```

## Instalação do Angular
- instalar o VSCode  - https://code.visualstudio.com/
- instalar o NodeJs - https://nodejs.org/en/download

  Valide no terminal do VSCode as instalações:
  ```bash
  node -v
  npm -v
  ```
  obs: Caso não encontre o node terá que criar a variável de ambiente do windows.
  Já o npm utlizar o seguinte comando:
  ```bash
    Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
  ```
  Continuação da instalação :
  
  ```bash
  npm install -g @angular/cli
  ```
  ```bash
  $env:NODE_OPTIONS = "--openssl-legacy-provider"
  ```
  Para compilar 
  ```bash
    ng serve
   ```
Rodar a aplicação no Visual Studio e trocar a porta no caminho abaixo:

APIFornecedor\wwwroot\app\demo-webapi\src\app\base\baseService.ts

## Visualização do Sistema

<img width="1231" height="460" alt="Lista de produtos" src="https://github.com/user-attachments/assets/be819c60-381a-4b4a-8ba3-4c39a52bd20e" />

## Criação do Usuário

Novos métodos criados /api/nova_conta ou /api/entrar

```json
{
  "email": "usuario.novo@exemplo.com.br",
  "password": "SenhaForte123!",
  "confirmPassword": "SenhaForte123!"
}
```

## 🔐 Autenticação e Autorização (JSON Web Token - JWT)

Nossa API utiliza **Tokens JWT** no esquema **Bearer** para todas as requisições autenticadas e autorizadas. O token é composto por três partes (Header, Payload e Signature) e contém as permissões (*Claims*) que definem o acesso do usuário.

### Estrutura do Payload (Claims Decodificadas) 🕵️‍♀️

O `Payload` decodificado abaixo é um exemplo de um token válido gerado após um login bem-sucedido. Ele inclui *claims* de segurança padrão e *claims* de autorização específicas do domínio.

<img width="1257" height="683" alt="Clains" src="https://github.com/user-attachments/assets/ebf7f999-c954-4a5a-932c-a0c76ef129db" />

 controle de acesso granular na API é implementado utilizando um **atributo customizado de autorização** que inspeciona as *claims* de domínio do token.

O atributo `[ClaimsAuthorize("Tipo da Claim", "Valor da Claim")]` é o *gatekeeper*: ele garante que o usuário tenha a permissão necessária antes de executar o método do *controller*.

**Exemplo de Proteção no `FornecedoresController`:**

```csharp
[Authorize]
[Route("api/fornecedores")]
public class FornecedoresController : MainController
{
    // Requer que o token contenha a claim 'Fornecedor' com o valor 'Excluir'
    [ClaimsAuthorize("Fornecedor", "Excluir")] 
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
    {
        // Lógica de exclusão só é executada se a claim for válida
        await _fornecedorService.Remover(id);
        return CustomResponse();
    }
    
    // Requer que o token contenha a claim 'Fornecedor' com o valor 'Atualizar'
    [ClaimsAuthorize("Fornecedor", "Atualizar")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
    {
        // ...
        return CustomResponse(fornecedorViewModel);
    }
}
```
## 📄 Documentação e Testes (Swagger/OpenAPI)

Nossa API utiliza o padrão **OpenAPI (Swagger)** para documentação e testes interativos dos *endpoints* em ambiente de desenvolvimento.

### Acesso e Configuração

A documentação está disponível ao rodar a aplicação em ambiente de desenvolvimento (local) no seguinte endereço:

* **URL Base:** `https://localhost:[PORTA]/swagger`

| Recurso | Descrição |
| :--- | :--- |
| **API Versioning** | A versão é controlada pelo atributo `[ApiVersion(x,y)]` nos *Controllers*. |
| **Documentos Swagger** | É gerado um documento **SwaggerDoc** (ex: `v1`) para cada versão, mantendo a documentação organizada. |
| **Testes (Autorização)** | O Swagger UI está configurado para **Bearer Token** (`JWT`). Use o botão **Authorize** e insira o token para testar *endpoints* protegidos. |

### Exemplo de Configuração de Versões

A classe `ConfigureSwaggerOptions` garante que o Swagger consiga ler as versões configuradas (`v1`, `v2`, etc.) e as exiba corretamente na interface:

```csharp
// Configuração no Program.cs
builder.Services.AddApiVersioning(options =>
{
    // ...
}).AddVersionedApiExplorer(options => 
{
    options.GroupNameFormat = "'v'VVV";
    // ...
});

// Registro da classe de configuração para versionamento do Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
```

<img width="1343" height="731" alt="Swagger" src="https://github.com/user-attachments/assets/7c3e8487-760a-402e-a6dc-fa504e02300f" />


