Instalar dependencias

1 - dotnet add package Microsoft.EntityFrameworkCore
2 - dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL (Provedor Banco)
3 - dotnet add package Microsoft.EntityFrameworkCore.Design (migrations)
4 - dotnet add package Microsoft.EntityFrameworkCore.Tools ("caixa de ferramentas")

criar migrations
1 - dotnet ef migrations add InitialCreate
2 - dontet ef database update

//em casa
"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123456;SSL Mode=Disable;Trust Server Certificate=true"
}
//empresa
"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=amnesia;SSL Mode=Disable;Trust Server Certificate=true"
}

puxar projeto
dotnet restore
dotnet build
dotnet run
