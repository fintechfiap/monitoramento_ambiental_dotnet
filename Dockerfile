FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app/MonitoramentoAmbiental

# Copiar apenas os arquivos necessários para restore
COPY MonitoramentoAmbiental/MonitoramentoAmbiental.csproj .
RUN dotnet restore

# Copiar o resto dos arquivos do projeto
COPY MonitoramentoAmbiental/. .
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Criar usuário não-root para segurança
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

COPY --from=build /out .

# Configurar healthcheck
HEALTHCHECK --interval=30s --timeout=3s \
    CMD curl -f http://localhost/health || exit 1

ENTRYPOINT ["dotnet", "MonitoramentoAmbiental.dll"]
