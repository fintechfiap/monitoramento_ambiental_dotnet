# Use .NET runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file and restore dependencies
COPY ["MonitoramentoAmbiental.csproj", "./"]
RUN dotnet restore "./MonitoramentoAmbiental.csproj"

# Copy the rest of the source code and build the application
COPY . .
RUN dotnet build "./MonitoramentoAmbiental.csproj" -c $BUILD_CONFIGURATION

# Publish the application
RUN dotnet publish "./MonitoramentoAmbiental.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: Use runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files from the build stage
COPY --from=build /app/publish .

# Use 'app' user for running the application
USER app

# Define entry point
ENTRYPOINT ["dotnet", "MonitoramentoAmbiental.dll"]
