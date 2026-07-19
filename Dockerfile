# ---------- Stage 1: Build ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only .csproj files first (maximizes layer caching on restore)
COPY NPE.sln .
COPY src/NPE.API/NPE.API.csproj src/NPE.API/
COPY src/NPE.Core/NPE.Core.csproj src/NPE.Core/
COPY src/NPE.Infrastructure/NPE.Infrastructure.csproj src/NPE.Infrastructure/

# Restore dependencies (this layer is cached unless a .csproj changes)
RUN dotnet restore src/NPE.API/NPE.API.csproj

# Now copy the rest of the source code
COPY src/ src/

# Build and publish
WORKDIR /src/src/NPE.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# ---------- Stage 2: Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Adjust if your API listens on a different port (check launchSettings.json / Program.cs)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "NPE.API.dll"]
