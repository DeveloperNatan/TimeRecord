FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.csproj .
COPY *.sln .
RUN dotnet restore RegistrarPonto.sln

COPY . .
RUN dotnet publish RegistrarPonto.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "RegistrarPonto.dll"]
