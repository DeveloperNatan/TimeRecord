 EF CORE commands

//Add new migration
dotnet ef migrations add "name migration"

//Apply migrations
dotnet ef database update

//Verify migrations
dotnet ef migrations list


.NET commands
//Restore dependencies
dotnet restore

//Build
dotnet build



//Docker
docker build -t timerecord:dev .

//Run container
docker run --rm -it -p 8080:8080 --name timerecord timerecord:dev

