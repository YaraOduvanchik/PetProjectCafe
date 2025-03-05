FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetProjectCafe.API/PetProjectCafe.API.csproj", "src/PetProjectCafe.API/"]
COPY ["src/PetProjectCafe.Domain/PetProjectCafe.Domain.csproj", "src/PetProjectCafe.Domain/"]
COPY ["src/PetProjectCafe.Application/PetProjectCafe.Application.csproj", "src/PetProjectCafe.Application/"]
COPY ["src/PetProjectCafe.Infrastructure/PetProjectCafe.Infrastructure.csproj", "src/PetProjectCafe.Infrastructure/"]
RUN dotnet restore "src/PetProjectCafe.API/PetProjectCafe.API.csproj"
COPY . .
WORKDIR "/src/src/PetProjectCafe.API"
RUN dotnet build "PetProjectCafe.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetProjectCafe.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetProjectCafe.API.dll"]
