FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ${PROJECT_DIR}/${PROJECT_DIR}.csproj .
RUN dotnet restore
COPY ${PROJECT_DIR}/. .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "${PROJECT_DIR}.dll"]
