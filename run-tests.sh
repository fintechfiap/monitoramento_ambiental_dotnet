#!/bin/bash

# Cores para output
GREEN='\033[0;32m'
RED='\033[0;31m'
NC='\033[0m'

echo -e "${GREEN}🚀 Iniciando execução dos testes...${NC}"

# Verifica se o Docker está rodando
if ! docker info > /dev/null 2>&1; then
    echo -e "${RED}❌ Docker não está rodando. Por favor, inicie o Docker Desktop.${NC}"
    exit 1
fi

# Build da imagem Docker
echo -e "${GREEN}📦 Construindo imagem Docker...${NC}"
docker build -t monitoramento-ambiental-tests -f Dockerfile.tests .

# Executa os testes
echo -e "${GREEN}🧪 Executando testes...${NC}"
docker run monitoramento-ambiental-tests

# Verifica o status de saída
if [ $? -eq 0 ]; then
    echo -e "${GREEN}✅ Testes executados com sucesso!${NC}"
else
    echo -e "${RED}❌ Falha na execução dos testes.${NC}"
    exit 1
fi 