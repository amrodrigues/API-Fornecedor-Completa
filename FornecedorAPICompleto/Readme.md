Markdown

# **API Fornecedor Completo**

Este reposit�rio cont�m a solu��o completa para o projeto **API Fornecedor**, que gerencia fornecedores e seus produtos associados. A arquitetura da aplica��o � dividida em tr�s projetos principais: **APIFornecedor**, **Business** e **Data**, seguindo o princ�pio de separa��o de responsabilidades.

---

## **Estrutura da Solu��o**

A solu��o � organizada da seguinte forma:

- **APIFornecedor**:
  - Ponto de entrada da API.
  - Configura��es para **AutoMapper** e **Inje��o de Depend�ncia**.
  - **Controllers**: Lidam com as requisi��es HTTP (GET, POST, PUT, DELETE).
  - **ViewModels**: Modelos de dados utilizados na comunica��o entre a API e o cliente.

- **Business**:
  - Cont�m a **l�gica de neg�cio**.
  - **Interfaces**: Defini��es de contratos para servi�os e reposit�rios.
  - **Models**: Entidades do dom�nio (e.g., Fornecedor, Produto).
  - **Services**: Implementa��o da l�gica de neg�cio.
  - **Notificacoes**: Sistema de notifica��es para valida��es e erros.

- **Data**:
  - Camada de **acesso a dados**.
  - **Context**: Representa o banco de dados (usando **Entity Framework Core**).
  - **Mappings**: Configura��es de mapeamento para as entidades.
  - **Migrations**: Gerenciamento das migra��es do banco de dados.
  - **Repository**: Padr�o de reposit�rio para abstrair o acesso a dados.

---

## **Endpoint Principal**

O endpoint principal para gerenciar fornecedores �:

`https://localhost:7202/api/fornecedores`

---

## **Exemplo de Requisi��o (POST)**

Para criar um novo fornecedor, utilize o m�todo **POST** enviando o seguinte corpo da requisi��o no formato JSON:

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "nome": "Nome do Fornecedor",
  "documento": "09678595737",
  "tipoFornecedor": 1,
  "endereco": null,
  "ativo": true,
  "produtos": []
}
```