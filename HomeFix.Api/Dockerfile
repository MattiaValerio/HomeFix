FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HomeFix.Api/HomeFix.Api.csproj", "HomeFix.Api/"]
COPY ["HomeFix.Shared/HomeFix.Shared.csproj", "HomeFix.Shared/"]
RUN dotnet restore "HomeFix.Api/HomeFix.Api.csproj"
COPY . .
WORKDIR "/src/HomeFix.Api"
RUN dotnet build "HomeFix.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HomeFix.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeFix.Api.dll"]
