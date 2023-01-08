FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
# Set the working directory witin the container
WORKDIR /build

# Copy the sln and csproj files. These are the only files
# required in order to restore
COPY ./dwCheckApi.Common/dwCheckApi.Common.csproj ./dwCheckApi.Common/dwCheckApi.Common.csproj
COPY ./dwCheckApi.DAL/dwCheckApi.DAL.csproj ./dwCheckApi.DAL/dwCheckApi.DAL.csproj
COPY ./dwCheckApi.DTO/dwCheckApi.DTO.csproj ./dwCheckApi.DTO/dwCheckApi.DTO.csproj
COPY ./dwCheckApi.Entities/dwCheckApi.Entities.csproj ./dwCheckApi.Entities/dwCheckApi.Entities.csproj
COPY ./dwCheckApi.Persistence/dwCheckApi.Persistence.csproj ./dwCheckApi.Persistence/dwCheckApi.Persistence.csproj
COPY ./dwCheckApi.Tests/dwCheckApi.Tests.csproj ./dwCheckApi.Tests/dwCheckApi.Tests.csproj
COPY ./dwCheckApi/dwCheckApi.csproj ./dwCheckApi/dwCheckApi.csproj
COPY ./dwCheckApi.sln ./dwCheckApi.sln
COPY ./global.json ./global.json

# Restore all packages
RUN dotnet restore --force --no-cache

# Copy the remaining source
COPY ./dwCheckApi.Common/ ./dwCheckApi.Common/
COPY ./dwCheckApi.DAL/ ./dwCheckApi.DAL/
COPY ./dwCheckApi.DTO/ ./dwCheckApi.DTO/
COPY ./dwCheckApi.Entities/ ./dwCheckApi.Entities/
COPY ./dwCheckApi.Persistence/ ./dwCheckApi.Persistence/
COPY ./dwCheckApi.Tests/ ./dwCheckApi.Tests/
COPY ./dwCheckApi/ ./dwCheckApi/

# Build the source code
RUN dotnet build --configuration Release --no-restore

# Install the dotnet ef global tool
## The following was taken from https://itnext.io/database-development-in-docker-with-entity-framework-core-95772714626f
RUN dotnet tool install -g dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools

# Ensure that we generate and migrate the database 
WORKDIR ./dwCheckApi.Persistence
RUN dotnet ef database update

# # Run all tests
WORKDIR ..
RUN dotnet test dwCheckApi.sln

# Publish application
RUN dotnet publish ./dwCheckApi/dwCheckApi.csproj --configuration Release --no-restore --no-build --output "../dist"

# Copy the created database
RUN cp ./dwCheckApi.Persistence/dwDatabase.db ../dist/dwDatabase.db

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS app
WORKDIR /app
COPY --from=build /dist .
ENV ASPNETCORE_URLS http://+:5000

ENTRYPOINT ["dotnet", "dwCheckApi.dll"]