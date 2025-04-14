FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY MonitoramentoAmbiental/MonitoramentoAmbiental.csproj ./MonitoramentoAmbiental/
RUN dotnet restore ./MonitoramentoAmbiental/MonitoramentoAmbiental.csproj

COPY MonitoramentoAmbiental/. ./MonitoramentoAmbiental/
WORKDIR /app/MonitoramentoAmbiental
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "MonitoramentoAmbiental.dll"]
