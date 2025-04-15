# Monitoramento Ambiental API

API REST para monitoramento ambiental desenvolvida com .NET 8, seguindo princípios de DDD (Domain-Driven Design) e Clean Architecture.

## 🚀 Tecnologias

- .NET 8.0
- Entity Framework Core 8.0
- Oracle Database
- Docker & Docker Compose
- JWT Authentication
- Swagger/OpenAPI
- AutoMapper
- Health Checks

## 📋 Pré-requisitos

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) (apenas para desenvolvimento)

## 🔧 Configuração e Inicialização

### 1. Clonando o Repositório

```bash
git clone https://github.com/FintechFIAP/monitoramento_ambiental_dotnet.git
cd monitoramento_ambiental_dotnet
```

### 2. Construindo a Imagem Docker

```bash
# Constrói a imagem da API
docker build -t monitoramento-api .

# Verifica se a imagem foi criada
docker images | grep monitoramento-api
```

### 4. Iniciando os Serviços

```bash
# Inicia todos os serviços com Docker Compose
docker compose up -d

# Verifica se os containers estão rodando
docker ps

# Acompanha os logs em tempo real
docker compose logs -f
```

### 5. Verificando a Instalação

```bash
# Testa o health check
curl http://localhost:8080/health

# Verifica o status do banco de dados
docker compose logs db
```

### 6. Parando os Serviços

```bash
# Para todos os serviços
docker compose down

# Para e remove volumes (útil para reset completo)
docker compose down -v
```

A API estará disponível em:
- Swagger: http://localhost:8080/swagger
- Health Check: http://localhost:8080/health

### Troubleshooting

Se encontrar problemas:

1. **Erro de Conexão com Banco:**
```bash
# Reinicie o container do banco
docker compose restart db

# Verifique os logs
docker compose logs db
```

2. **API não Responde:**
```bash
# Reinicie o container da API
docker compose restart api

# Verifique os logs
docker compose logs api
```

3. **Problemas de Permissão:**
```bash
# Verifique as permissões dos volumes
ls -la $(docker volume inspect monitoramento_pgdata -f '{{ .Mountpoint }}')
```

## 📚 Documentação da API

### Endpoints Principais

#### Health Check
- GET `/health` - Verifica a saúde da aplicação
- GET `/Health` - Verifica a saúde da aplicação (detalhado)

#### Alertas
- GET `/api/alerta` - Lista todos os alertas
- GET `/api/alerta/{id}` - Busca alerta por ID
- POST `/api/alerta` - Cria novo alerta
- PUT `/api/alerta/{id}` - Atualiza alerta existente
- DELETE `/api/alerta/{id}` - Remove alerta

#### Usuários
- POST `/api/usuario/registrar` - Registra novo usuário
- POST `/api/usuario/login` - Realiza login

### Autenticação

A API utiliza autenticação JWT Bearer. Para acessar endpoints protegidos:

1. Faça login para obter o token
2. Inclua o token no header das requisições:
```
Authorization: Bearer seu_token_aqui
```

## 🛠️ Desenvolvimento

### Estrutura do Projeto

```
MonitoramentoAmbiental/
├── Controllers/         # Controllers da API
├── Models/             # Modelos de domínio
├── ViewModels/         # DTOs e modelos de visualização
├── Services/           # Regras de negócio
├── Repositories/       # Acesso a dados
├── Data/              # Contexto e configurações do EF Core
└── Interfaces/        # Contratos e interfaces
```

### Comandos Úteis

```bash
# Visualizar logs
docker compose logs -f

# Reiniciar serviços
docker compose restart

# Parar serviços
docker compose down

# Limpar volumes
docker compose down -v
```

## 🔍 Monitoramento

### Health Checks

A API possui endpoints de health check para monitoramento:

- `/health` - Status básico
- `/Health` - Status detalhado incluindo:
  - Conexão com banco de dados
  - Status da API
  - Timestamp da verificação

### Logs

Os logs da aplicação podem ser acessados via:

```bash
# Logs da API
docker compose logs api

# Logs do banco de dados
docker compose logs db
```

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 🎯 Status do Projeto

- [x] Configuração inicial
- [x] Autenticação JWT
- [x] CRUD de Alertas
- [x] Gestão de Usuários
- [x] Documentação Swagger
- [X] CI/CD
- [x] Containerização