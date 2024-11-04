# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto y restaurar las dependencias
COPY *.sln ./
COPY Deportes.api/*.csproj ./Deportes.api/
COPY Deportes.Infra/*.csproj ./Deportes.Infra/
COPY Deportes.Modelo/*.csproj ./Deportes.Modelo/
COPY Deportes.Servicio/*.csproj ./Deportes.Servicio/
COPY Deportes.Test/*.csproj  ./Deportes.Test/
RUN dotnet restore 

# Copiar todo el contenido y construir la aplicación
COPY . .
WORKDIR /src/Deportes.api
RUN dotnet publish -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto en el que se ejecuta tu aplicación (ajusta si es necesario)
EXPOSE 80
EXPOSE 7067

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Deportes.api.dll"]
