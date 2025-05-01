# language: pt-BR
Funcionalidade: Gerenciamento de Alertas Climáticos
    Como um usuário do sistema
    Eu quero gerenciar alertas climáticos
    Para monitorar condições ambientais críticas

Cenário: Criar um novo alerta climático com sucesso
    Dado que tenho um novo alerta climático válido para criar
    Quando eu enviar uma requisição POST para criar o alerta
    Então o sistema deve retornar status code 201
    E retornar os dados do alerta criado com um ID
    E o alerta deve estar disponível para consulta

Cenário: Tentar criar um alerta climático com dados inválidos
    Dado que tenho dados inválidos de um alerta
    Quando eu enviar uma requisição POST para criar o alerta
    Então o sistema deve retornar status code 400
    E retornar as mensagens de validação apropriadas

Cenário: Buscar um alerta climático por ID
    Dado que existe um alerta climático cadastrado
    Quando eu enviar uma requisição GET para buscar o alerta pelo ID
    Então o sistema deve retornar status code 200
    E retornar os dados completos do alerta

Cenário: Tentar buscar um alerta climático inexistente
    Quando eu enviar uma requisição GET para buscar um alerta com ID inexistente
    Então o sistema deve retornar status code 404
    E retornar uma mensagem informando que o alerta não foi encontrado 