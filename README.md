

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
  "endereco": null,
  "ativo": true,
  "produtos": []
}
```
