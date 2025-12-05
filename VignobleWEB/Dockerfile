FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copier les fichiers de projet et restaurer les dépendances
COPY ["VignobleWEB/VignobleWEB.csproj", "VignobleWEB/"]
COPY ["VignobleWEB.Core.Application/VignobleWEB.Core.Application.csproj", "VignobleWEB.Core.Application/"]
COPY ["VignobleWEB.Core.Infrastructure/VignobleWEB.Core.Infrastructure.csproj", "VignobleWEB.Core.Infrastructure/"]
COPY ["VignobleWEB.Core.Interfaces/VignobleWEB.Core.Interfaces.csproj", "VignobleWEB.Core.Interfaces/"]
COPY ["VignobleWEB.Core.Models/VignobleWEB.Core.Models.csproj", "VignobleWEB.Core.Models/"]

RUN dotnet restore "VignobleWEB/VignobleWEB.csproj"

# Copier tout le code source
COPY . .

# Build du projet principal
WORKDIR "/src/VignobleWEB"
RUN dotnet build "VignobleWEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VignobleWEB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VignobleWEB.dll"]