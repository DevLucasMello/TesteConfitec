# Aplicação Distribuída em APIs
 
## Tecnologias / Componentes implementados

    - .Net 5;
    - WebApi;
    - SOLID;
    - Clean Code;
    - Design Patterns;
    - DDD;
    - Arquitetura Hexagonal;
    - SqlServer;
    - EntityFrameworkCore 5;
    - Dapper;
    - Automapper;
    - Identity;
    - JWT;
    - Angular 11;
    - Docker;
    - Testes de Unidade;
    - Testes de Integração;
    - Bogus;
    - Moq;
    - Automocker;
    - FluentValidator
    - MediatR
    - Swagger UI with JWT support

## Recursos para rodar a aplicação

   - Docker instalado com conta ativa
   - Interface gráfica SqlServer


## Instruções de uso

   - 1° - Baixar projeto do GitHub
   - 2° - Criar uma pasta com nome ####dev no diretório C:
   - 3° - Extrair o projeto baixado na pasta #### dev
   - 4° - No cmd do windows digitar o seguinte comando para navegar até o diretório #### cd c:/dev/TesteConfitec/docker
   - 5° - Ainda no cmd, digitar o comando para criar imagem e rodar os containeres docker ### docker-compose -f tc_back-end.yml up --build
   - 6° - Abrir interface gráfica do SqlServer e verificar se os bancos de dados #### TesteIdentidadeConfitec #### TesteUsuarioConfitec foram criados

### Passo abaixo apenas se os bancos mencionados acima não foram criados
   - 7° - Abra a interface do docker e clique em restart no container ####tc-sql-server

   - 8° - Abrir o navegador de sua preferência e digitar a url ####localhost:8080 para carregar a aplicação.
