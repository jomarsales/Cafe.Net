# CafeDotNet (em desenvolvimento)
==============

**Site oficial do portal Café.Net, utilizando Clean Architecture e Domain‑Driven Design (DDD) Test Driven Design (TDD), Multi Database Privider, Asp Net Code MVC.**

---

##  Visão Geral

Este projeto se aplica os princípios de Clean Architecture e DDD para promover um código modular, fácil de testar e bem organizado. A estrutura separa claramente a lógica de domínio, aplicação e infraestrutura. Na infraestrutura dedicada a acesso a dados, está sendo usado o pattern para multi providers dinâmicos além de utilização do Entity Framrwork como ORM. Aqui estão as melhores práticas de desenvolvimento .Net mesmo sendo um projeto de pequeno porte, cujo objetivo é solidificar conceitos técnicos.

---

##  Tecnologias (sugeridas)

- ASP.NET CORE MVC
- C#, Razor
- Clean Architecture (ex: camadas Domain, Application, Infrastructure)
- DDD (entidades, agregados, value objects, repositórios, etc.)
- Multi Banco de dados (ex.: My SQL, SQL Server, PostgreSQL)
- TDD (xUnit)

---

##  Estrutura Sugerida do Projeto

```
CafeDotNet/
├── src/
│   ├── Core/           
│   ├── Infra.Bootstrapper/
│   ├── Infra.Data.Common/
│   ├── Infra.Data.MySql/
│   ├── Infra.Data.PostgreSql/
│   ├── Infra.Data.SqlServer/
│   ├── Infra.Logging/
│   ├── Infra.Mail/
│   ├── Web/
│   ├── Manager/    
│   └── Tests/           
├── CafeDotNet.sln      
└── README.md             
```

---

## ▶ Como Executar (sugestões)

1. Clone o repositório:
   ```bash
   git clone https://github.com/jomarsales/Cafe.Net.git
   ```
2. Abra a solução com o Visual Studio ou usando a CLI .NET:
   ```bash
   cd CafeDotNet
   dotnet restore
   ```
---

##  Boas Práticas Aplicadas

- Separação clara de responsabilidades entre domínio, aplicação e infraestrutura.
- Lógica de negócios centralizada na camada de domínio (DDD).
- Camada de aplicação para orquestração de casos de uso.
- Infraestrutura abstraída por interfaces e injeção de dependência.
- Preparado para testes automatizados.

---
