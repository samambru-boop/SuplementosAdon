FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["SuplementosAdon.API/SuplementosAdon.API.csproj", "SuplementosAdon.API/"]
RUN dotnet restore "SuplementosAdon.API/SuplementosAdon.API.csproj"

COPY . .
WORKDIR /src/SuplementosAdon.API
RUN dotnet publish "SuplementosAdon.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SuplementosAdon.API.dll"]