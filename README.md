# CP4 Livraria

Projeto de exemplo de API RESTful em C# com ASP.NET Core usando MongoDB para gerenciar livros e autores.
A API suporta operações CRUD (Criar, Ler, Atualizar e Deletar) com endpoints prontos para testes no Swagger.

---
## 👥 Integrantes

- Caroline Assis Silva - RM 557596  
- Enzo de Moura Silva - RM 556532  
- Luis Henrique Gomes Cardoso - RM 558883  
---

## 📁 Estrutura do Projeto

```bash
├── Controllers/
│   └── LivrosController.cs
│
├── Models/
│   ├── Autor.cs
│   └── Livro.cs
│
├── DTOs/
│   └── LivroDTO.cs
│
├── Data/
│   └── MongoDbContext.cs
│
├── Program.cs
├── appsettings.json
├── CP4_livraria.sln
└── CP4_livraria.csproj
```
## 🧪 Como Executar o Projeto

### 1. Clone o repositório
```bash
git clone https://github.com/codecrazes/CP_4.NET.git
```
```bash
cd CP4_livraria
```
### 2. Restaurar dependências
```bash
dotnet restore
```
### 3. Executar o projeto
```bash
dotnet run
```
---
## 🔄 Exemplos de Requisições (JSON para Teste)

🌐 URL Base da API
http://localhost:5037/swagger/index.html

POST

```json
{
  "titulo": "Aprendendo C#",
  "anoPublicacao": 2025,
  "autores": [
    {
      "nome": "Caroline Assis",
      "nacionalidade": "BR"
    }
  ]
}
```
```json
{
  "titulo": "C# Avançado",
  "anoPublicacao": 2025,
  "autores": [
    {
      "nome": "Caroline Assis",
      "nacionalidade": "BR"
    },
    {
      "nome": "Enzo Silva",
      "nacionalidade": "BR"
    }
  ]
}
```

PUT

```json
{
  "titulo": "Aprendendo JAVA",
  "anoPublicacao": 2025,
  "autores": [
    {
      "nome": "Enzo de Moura",
      "nacionalidade": "BR"
    }
  ]
}

```
