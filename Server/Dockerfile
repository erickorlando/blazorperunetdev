FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["/src/ECommerceWeb.DataAccess/ECommerceWeb.DataAccess.csproj", "ECommerceWeb.DataAccess/"]
COPY ["/src/ECommerceWeb.Entities/ECommerceWeb.Entities.csproj", "ECommerceWeb.Entities/"]
COPY ["/src/ECommerceWeb.Repositories/ECommerceWeb.Repositories.csproj", "ECommerceWeb.Repositories/"]
COPY ["/src/Server/ECommerceWeb.Server.csproj", "Server/"]
COPY ["/src/Client/ECommerceWeb.Client.csproj", "Client/"]
COPY ["/src/Shared/ECommerceWeb.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/ECommerceWeb.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "ECommerceWeb.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceWeb.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceWeb.Server.dll"]