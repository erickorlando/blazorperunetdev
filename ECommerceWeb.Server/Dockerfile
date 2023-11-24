#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ECommerceWeb.Server/ECommerceWeb.Server.csproj", "ECommerceWeb.Server/"]
COPY ["ECommerceWeb.Client/ECommerceWeb.Client.csproj", "ECommerceWeb.Client/"]
COPY ["ECommerceWeb.Shared/ECommerceWeb.Shared.csproj", "ECommerceWeb.Shared/"]
COPY ["ECommerceWeb.DataAccess/ECommerceWeb.DataAccess.csproj", "ECommerceWeb.DataAccess/"]
COPY ["ECommerceWeb.Entities/ECommerceWeb.Entities.csproj", "ECommerceWeb.Entities/"]
COPY ["ECommerceWeb.Repositories/ECommerceWeb.Repositories.csproj", "ECommerceWeb.Repositories/"]
RUN dotnet restore "ECommerceWeb.Server/ECommerceWeb.Server.csproj"
COPY . .
WORKDIR "/src/ECommerceWeb.Server"
RUN dotnet build "ECommerceWeb.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceWeb.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceWeb.Server.dll"]