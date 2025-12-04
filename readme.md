Backend do Ponto Fácil

API REST para registro e gestão de ponto de funcionários, utilizada pelo frontend Ponto Fácil.
Tecnologias

    .NET 9 (ASP.NET Core Web API)

    C#

    Entity Framework Core

    PostgreSQL (Neon)

    Docker

Funcionalidades

    CRUD de funcionários

    Registro de marcações de ponto

    Consulta de histórico de marcações por funcionário

    Integração com o frontend (Next.js) do Ponto Fácil

Documentação da API (Swagger)

A API expõe documentação interativa via Swagger, com todos os endpoints, modelos e exemplos de requisição/resposta.

    Produção:
    https://timerecord-gymv.onrender.com/swagger/index.html

Como rodar localmente

    Clonar o repositório:

bash
git clone https://github.com/DeveloperNatan/TimeRecord.git
cd TimeRecord

    Configurar a connection string do PostgreSQL no appsettings.json ou via variável de ambiente (ex.: DEFAULT_CONNECTION).

    Aplicar as migrações (se estiver usando EF Core):

bash
dotnet ef database update

    Rodar a API:

bash
dotnet run

    Acessar o Swagger local:

text
http://localhost:5144/swagger/index.html

(ajuste a porta se sua API estiver configurada com outra)
Endpoints principais

Alguns endpoints importantes (mais detalhes no Swagger):

    GET /api/employees — lista de funcionários

    POST /api/employees — cria um funcionário

    GET /api/employees/{id} — detalhes de um funcionário

    GET /api/employees/{id}/markings — marcações de ponto de um funcionário

    POST /api/markings — registra uma nova marcação de ponto

Deploy

    Hospedado na Render como serviço Web API.

    Utilizado como backend pelo frontend Ponto Fácil (deploy na Vercel).
