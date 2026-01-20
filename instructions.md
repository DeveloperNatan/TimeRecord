//Add new migration
dotnet ef migrations add "name migration"

//Apply migrations
dotnet ef database update

//Verify migrations
dotnet ef migrations list
