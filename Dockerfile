FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app/MonitoramentoAmbiental

# Copiar apenas o arquivo .csproj do projeto principal
COPY MonitoramentoAmbiental/MonitoramentoAmbiental.csproj .
RUN dotnet restore

# Copiar o resto dos arquivos do projeto
COPY MonitoramentoAmbiental/. .
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "MonitoramentoAmbiental.dll"]
