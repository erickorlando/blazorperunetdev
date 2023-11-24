# Utiliza la imagen base de .NET 7 SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia el archivo de proyecto y restaura las dependencias
COPY *.csproj .
RUN dotnet build ./ECommerceWeb.Server.csproj

# Copia el código fuente y compila la aplicación
COPY . .
RUN dotnet publish ./ECommerceWeb.Server.csproj -c Release -o out

# Utiliza una imagen base más ligera para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Exponer los puertos 80 y 443
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "ECommerceWeb.Server.dll"]