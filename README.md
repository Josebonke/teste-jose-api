## teste-jose-api

## Tecnologias
- Projeto criado em .net core 8
- Entity FrameWork Core
- Identity

# Base De Dados
  - SqlServer (Banco localizado na raiz do projeto bin\Debug\net8.0) para que não haja nessecidade de configurar string de conexão
  - Nome do Banco UserManager

## Apontamentos
-- Foi escolhido ef core para trabalhar com base de dados pois em um breve estudo foi identificado que o Identity trabalha de maneira mas eficiente 
-- Foi criado uma classe nomeada SeedUsuario para criar usuário assim que a api é "buildada" as entidades nomes,UserName,Email e senha estão em hardcode 
para Cadastra um novo usuário primeiro na ln 20  é verificado se o email já existe na base de dados 
"_userManager.FindByEmailAsync("insiraAqui um email para verificar a existen de um usuário")

entra a ln 24 e 30 é onde se coloca nome,email,userName


